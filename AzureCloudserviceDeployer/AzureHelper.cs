using log4net;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;
using Microsoft.WindowsAzure.Management.Storage;
using Microsoft.WindowsAzure.Management.Storage.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Subscriptions;
using Microsoft.WindowsAzure.Subscriptions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AzureCloudserviceDeployer
{
    public enum UpgradePreference
    {
        [Description("Delete existing deployment, then create a new one")]
        DeleteAndCreateDeployment = 1,
        [Description("Upgrade deployment respecting update domains")]
        UpgradeWithUpdateDomains = 2,
        [Description("Upgrade deployment with all update domains simultaneously")]
        UpgradeSimultaneously = 3
    }

    public class AzureHelper
    {
        private static ILog Logger = LogManager.GetLogger(typeof(AzureHelper));

        public async static Task<AuthenticationResult> GetAuthentication(string tenantId = null, bool forceRefresh = false)
        {
            var login = ConfigurationManager.AppSettings["login"];
            tenantId = tenantId ?? ConfigurationManager.AppSettings["tenantId"]; // Use the tenant of the selected subscription, or the default
            var apiEndpoint = ConfigurationManager.AppSettings["apiEndpoint"];
            var clientId = ConfigurationManager.AppSettings["clientId"];
            var redirectUri = ConfigurationManager.AppSettings["redirectUri"];

            var prompt = forceRefresh ? PromptBehavior.RefreshSession : PromptBehavior.Auto;

            var context = new AuthenticationContext(string.Format(login, tenantId));
            var result = context.AcquireToken(apiEndpoint, clientId, new Uri(redirectUri), prompt);
            return result;
        }

        public async static Task<TokenCloudCredentials> GetCredentialsAsync(SubscriptionListOperationResponse.Subscription subscription = null)
        {
            TokenCloudCredentials token = null;
            if (subscription == null)
                token = new TokenCloudCredentials((await GetAuthentication()).AccessToken);
            else
                token = new TokenCloudCredentials(subscription.SubscriptionId, (await GetAuthentication(subscription.ActiveDirectoryTenantId)).AccessToken);
            return token;
        }

        public static async Task<SubscriptionListOperationResponse> GetSubscriptionsAsync()
        {
            var subscriptionClient = new SubscriptionClient(await GetCredentialsAsync());
            Logger.Info("Retrieving subscriptions...");
            return await subscriptionClient.Subscriptions.ListAsync();
        }

        public static async Task<HostedServiceListResponse> GetCloudservicesAsync(SubscriptionListOperationResponse.Subscription subscription)
        {
            var computeClient = new ComputeManagementClient(await GetCredentialsAsync(subscription));
            Logger.Info("Retrieving hosted services...");
            return await computeClient.HostedServices.ListAsync();
        }

        public static async Task<StorageAccountListResponse> GetStorageAccountsAsync(SubscriptionListOperationResponse.Subscription subscription)
        {
            var storageClient = new StorageManagementClient(await GetCredentialsAsync(subscription));
            Logger.Info("Retrieving storage accounts...");
            return await storageClient.StorageAccounts.ListAsync();
        }

        public static async Task DeployAsync(SubscriptionListOperationResponse.Subscription subscription, HostedServiceListResponse.HostedService service,
            StorageAccount storage, DeploymentSlot slot, UpgradePreference upgradePreference, string pathToCspkg,
            string pathToCscfg, string pathToDiagExtensionConfig, StorageAccount diagStorage, string deploymentLabel,
            bool cleanupUnusedExtensions)
        {
            Logger.InfoFormat("Preparing for deployment of {0}...", service.ServiceName);
            var computeClient = new ComputeManagementClient(await GetCredentialsAsync(subscription));
            var storageClient = new StorageManagementClient(await GetCredentialsAsync(subscription));

            // Load csdef
            var csCfg = File.ReadAllText(pathToCscfg);

            // Load diag config
            var diagConfig = pathToDiagExtensionConfig != null ? File.ReadAllText(pathToDiagExtensionConfig) : null;

            // Upgrade cspkg to storage
            Logger.InfoFormat("Fetching key for storage account {0}...", storage.Name);
            var keys = await storageClient.StorageAccounts.GetKeysAsync(storage.Name);
            var csa = new CloudStorageAccount(new StorageCredentials(storage.Name, keys.PrimaryKey), true);
            var blobClient = csa.CreateCloudBlobClient();
            var containerRef = blobClient.GetContainerReference("acd-deployments");
            if (!await containerRef.ExistsAsync())
            {
                Logger.InfoFormat("Creating container {0}...", containerRef.Name);
                await containerRef.CreateIfNotExistsAsync();
            }
            var filename = service.ServiceName + "-" + slot.ToString() + ".cspkg";
            var blobRef = containerRef.GetBlockBlobReference(filename);
            Logger.InfoFormat("Uploading package to {0}...", blobRef.Uri);
            await blobRef.UploadFromFileAsync(pathToCspkg, FileMode.Open);

            // Private diagnostics config
            string diagStorageName = null, diagStorageKey = null;
            if (diagStorage == null)
            {
                Logger.InfoFormat("Using storage account credentials from .cscfg for diagnostics...");
                Regex regex = new Regex("Diagnostics.ConnectionString.*?DefaultEndpointsProtocol.*?AccountName=(.+?);.*?AccountKey=([a-zA-Z0-9/+=]+)", RegexOptions.Singleline);
                Match m = regex.Match(csCfg);
                if (m.Success)
                {
                    diagStorageName = m.Groups[1].Value;
                    diagStorageKey = m.Groups[2].Value;
                    Logger.InfoFormat("Extracted storage account {0} from .cscfg for diagnostics...", diagStorageName);
                }
                else
                {
                    throw new ApplicationException("Couldn't extract Diagnostics ConnectionString from .cscfg");
                }
            }
            else
            {
                Logger.InfoFormat("Using storage account {0} for diagnostics...", diagStorage.Name);
                if (storage.Name == diagStorage.Name)
                {
                    diagStorageName = diagStorage.Name;
                    diagStorageKey = keys.PrimaryKey;
                }
                else
                {
                    Logger.InfoFormat("Fetching keys for storage account {0}...", diagStorage.Name);
                    var diagKeys = await storageClient.StorageAccounts.GetKeysAsync(diagStorage.Name);
                    diagStorageName = diagStorage.Name;
                    diagStorageKey = diagKeys.PrimaryKey;
                }
            }
            var privateDiagConfig = string.Format(@"<PrivateConfig xmlns=""http://schemas.microsoft.com/ServiceHosting/2010/10/DiagnosticsConfiguration"">
    <StorageAccount name=""{0}"" key=""{1}"" />
  </PrivateConfig>", diagStorageName, diagStorageKey);

            // Get diagnostics extension template
            Logger.InfoFormat("Retrieving available extensions...");
            var availableExtensions = await computeClient.HostedServices.ListAvailableExtensionsAsync();
            var availableDiagnosticsExtension = availableExtensions.Where(d => d.Type == "PaaSDiagnostics").First();

            // Cleanup of existing extensions
            if (cleanupUnusedExtensions)
            {
                await CleanupExistingExtensions(service, computeClient, availableDiagnosticsExtension);
            }
            else
            {
                Logger.InfoFormat("Skip cleaning unused extensions as configured");
            }

            // Extensions config
            var extensionConfiguration = new ExtensionConfiguration();

            // Create the extension with new configuration
            if (diagConfig != null)
            {
                var diagnosticsId = "acd-diagnostics-" + Guid.NewGuid().ToString("N");
                Logger.InfoFormat("Adding new diagnostics extension {0}...", diagnosticsId);
                var createExtensionOperation = await computeClient.HostedServices.AddExtensionAsync(service.ServiceName, new HostedServiceAddExtensionParameters()
                {
                    ProviderNamespace = availableDiagnosticsExtension.ProviderNameSpace,
                    Type = availableDiagnosticsExtension.Type,
                    Id = diagnosticsId,
                    Version = availableDiagnosticsExtension.Version,
                    PublicConfiguration = diagConfig,
                    PrivateConfiguration = privateDiagConfig
                });
                var createExtensionStatus = await computeClient.GetOperationStatusAsync(createExtensionOperation.RequestId);
                while (createExtensionStatus.Status == OperationStatus.InProgress)
                {
                    Logger.InfoFormat("Waiting for operation to finish...");
                    await Task.Delay(5000);
                    createExtensionStatus = await computeClient.GetOperationStatusAsync(createExtensionOperation.RequestId);
                }

                // Extension configuration
                extensionConfiguration.AllRoles.Add(new ExtensionConfiguration.Extension
                {
                    Id = diagnosticsId
                });
            }

            // Create deployment parameters
            var deployParams = new DeploymentCreateParameters
            {
                StartDeployment = true,
                Name = Guid.NewGuid().ToString("N"),
                Configuration = csCfg,
                PackageUri = blobRef.Uri,
                Label = deploymentLabel ?? DateTime.UtcNow.ToString("u") + " " + Environment.UserName,
                ExtensionConfiguration = extensionConfiguration
            };
            var upgradeParams = new DeploymentUpgradeParameters
            {
                Configuration = csCfg,
                PackageUri = blobRef.Uri,
                Label = deploymentLabel ?? DateTime.UtcNow.ToString("u") + " " + Environment.UserName,
                ExtensionConfiguration = extensionConfiguration,
                Mode = upgradePreference == UpgradePreference.UpgradeSimultaneously ? DeploymentUpgradeMode.Simultaneous : DeploymentUpgradeMode.Auto
            };
            Logger.InfoFormat("Label for deployment: {0}", deployParams.Label);

            switch (upgradePreference)
            {
                case UpgradePreference.DeleteAndCreateDeployment:
                    {
                        // Is there a deployment in this slot?
                        Logger.InfoFormat("Fetching detailed service information...");
                        var detailedService = await computeClient.HostedServices.GetDetailedAsync(service.ServiceName);
                        if (detailedService.Deployments.Where(s => s.DeploymentSlot == slot).Any())
                        {
                            // Yes, there is. Delete it.
                            Logger.InfoFormat("Deployment in {0} slot exists, deleting...", slot);
                            var deleteOperation = await computeClient.Deployments.DeleteBySlotAsync(service.ServiceName, slot);
                            var deleteStatus = await computeClient.GetOperationStatusAsync(deleteOperation.RequestId);
                            while (deleteStatus.Status == OperationStatus.InProgress)
                            {
                                Logger.InfoFormat("Waiting for operation to finish...");
                                await Task.Delay(5000);
                                deleteStatus = await computeClient.GetOperationStatusAsync(deleteOperation.RequestId);
                            }
                        }

                        // Create a new deployment in this slot
                        Logger.InfoFormat("Creating deployment in {0} slot...", slot);
                        var createOperation = await computeClient.Deployments.CreateAsync(service.ServiceName, slot, deployParams);
                        var createStatus = await computeClient.GetOperationStatusAsync(createOperation.RequestId);
                        while (createStatus.Status == OperationStatus.InProgress)
                        {
                            Logger.InfoFormat("Waiting for operation to finish...");
                            await Task.Delay(5000);
                            createStatus = await computeClient.GetOperationStatusAsync(createOperation.RequestId);
                        }
                    }

                    break;
                case UpgradePreference.UpgradeWithUpdateDomains:
                case UpgradePreference.UpgradeSimultaneously:
                    {
                        // Is there a deployment in this slot?
                        Logger.InfoFormat("Fetching detailed service information...");
                        var detailedService = await computeClient.HostedServices.GetDetailedAsync(service.ServiceName);
                        if (detailedService.Deployments.Where(s => s.DeploymentSlot == slot).Any())
                        {
                            // Yes, there is. Upgrade it.
                            Logger.InfoFormat("Deployment in {0} slot exists, upgrading...", slot);
                            var upgradeOperation = await computeClient.Deployments.UpgradeBySlotAsync(service.ServiceName, slot, upgradeParams);
                            var upgradeStatus = await computeClient.GetOperationStatusAsync(upgradeOperation.RequestId);
                            while (upgradeStatus.Status == OperationStatus.InProgress)
                            {
                                Logger.InfoFormat("Waiting for operation to finish...");
                                await Task.Delay(5000);
                                upgradeStatus = await computeClient.GetOperationStatusAsync(upgradeOperation.RequestId);
                            }
                        }
                        else
                        {
                            // No, there isn't. Create.
                            Logger.InfoFormat("No deployment in {0} slot yet, creating...", slot);
                            var createOperation = await computeClient.Deployments.CreateAsync(service.ServiceName, slot, deployParams);
                            var createStatus = await computeClient.GetOperationStatusAsync(createOperation.RequestId);
                            while (createStatus.Status == OperationStatus.InProgress)
                            {
                                Logger.InfoFormat("Waiting for operation to finish...");
                                await Task.Delay(5000);
                                createStatus = await computeClient.GetOperationStatusAsync(createOperation.RequestId);
                            }
                        }
                    }

                    break;
            }

            Logger.InfoFormat("Deployment succesful");
        }

        private static async Task CleanupExistingExtensions(HostedServiceListResponse.HostedService service, ComputeManagementClient computeClient, ExtensionImage availableDiagnosticsExtension)
        {

            // Get whatever is currently on Production
            Logger.InfoFormat("Retrieving current deployment details and currently used extensions...");
            DeploymentGetResponse currentProductionSlotDetails = null, currentStagingSlotDetails = null;
            var details = computeClient.HostedServices.GetDetailed(service.ServiceName);
            if (details.Deployments.Where(s => s.DeploymentSlot == DeploymentSlot.Production).Any())
                currentProductionSlotDetails = await computeClient.Deployments.GetBySlotAsync(service.ServiceName, DeploymentSlot.Production);
            if (details.Deployments.Where(s => s.DeploymentSlot == DeploymentSlot.Staging).Any())
                currentStagingSlotDetails = await computeClient.Deployments.GetBySlotAsync(service.ServiceName, DeploymentSlot.Staging);

            // Compile a list of diagnostic id's still in use
            List<string> diagIdsInUse = new List<string>();
            if (currentProductionSlotDetails != null && currentProductionSlotDetails.ExtensionConfiguration != null)
            {
                diagIdsInUse.AddRange(currentProductionSlotDetails.ExtensionConfiguration.AllRoles.Select(s => s.Id));
                diagIdsInUse.AddRange(currentProductionSlotDetails.ExtensionConfiguration.NamedRoles.SelectMany(s => s.Extensions).Select(s => s.Id));
            }
            if (currentStagingSlotDetails != null && currentStagingSlotDetails.ExtensionConfiguration != null)
            {
                diagIdsInUse.AddRange(currentStagingSlotDetails.ExtensionConfiguration.AllRoles.Select(s => s.Id));
                diagIdsInUse.AddRange(currentStagingSlotDetails.ExtensionConfiguration.NamedRoles.SelectMany(s => s.Extensions).Select(s => s.Id));
            }

            // Check if diag extension already exists for this service. If so, delete it.
            Logger.InfoFormat("Retrieving all extensions for {0}...", service.ServiceName);
            var currentExtensions = await computeClient.HostedServices.ListExtensionsAsync(service.ServiceName);
            foreach (var currentDiagExtension in currentExtensions.Where(d => d.Type == availableDiagnosticsExtension.Type))
            {
                if (diagIdsInUse.Contains(currentDiagExtension.Id))
                {
                    Logger.InfoFormat("Skip deleting diagnostics extension {0}, because it is in use by the deployment", currentDiagExtension.Id);
                }
                else
                {
                    Logger.InfoFormat("Deleting unused diagnostics extension named {0}...", currentDiagExtension.Id);
                    try
                    {
                        await computeClient.HostedServices.DeleteExtensionAsync(service.ServiceName, currentDiagExtension.Id);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("Couldn't delete extension {0}, might be in use...", currentDiagExtension.Id);
                    }
                }
            }
        }

        public static async Task DownloadDeploymentAsync(SubscriptionListOperationResponse.Subscription subscription, HostedServiceListResponse.HostedService service, DeploymentSlot slot, StorageAccount temporaryStorage, string downloadToPath, bool warnInsteadOfThrow = false)
        {
            Logger.InfoFormat("Preparing to download {0}/{1}...", service.ServiceName, slot);
            var computeClient = new ComputeManagementClient(await GetCredentialsAsync(subscription));
            var storageClient = new StorageManagementClient(await GetCredentialsAsync(subscription));

            Logger.InfoFormat("Checking existing deployment in {0} slot...", slot);
            var detailedService = await computeClient.HostedServices.GetDetailedAsync(service.ServiceName);
            if (!detailedService.Deployments.Where(s => s.DeploymentSlot == slot).Any())
            {
                if (warnInsteadOfThrow)
                {
                    Logger.WarnFormat("No deployment found in selected slot, not downloading...");
                    return;
                }
                else
                {
                    throw new ApplicationException("No deployment found in selected slot");
                }
            }

            Logger.InfoFormat("Getting current diagnostics extension data (if any)...");
            var detailedSlot = await computeClient.Deployments.GetBySlotAsync(service.ServiceName, slot);
            string pubConfig = null;
            if (detailedSlot.ExtensionConfiguration != null)
            {
                foreach (var ext in detailedSlot.ExtensionConfiguration.AllRoles.Union(detailedSlot.ExtensionConfiguration.NamedRoles.SelectMany(s => s.Extensions)))
                {
                    Logger.InfoFormat("Fetching extension with id {0}...", ext.Id);
                    var extension = await computeClient.HostedServices.GetExtensionAsync(service.ServiceName, ext.Id);
                    if (extension.Type == "PaaSDiagnostics")
                    {
                        Logger.InfoFormat("Found extension of type {0}, which should be the diagnostics PubConfig...", extension.Type);
                        pubConfig = extension.PublicConfiguration;
                    }
                }
            }
            if (pubConfig == null)
            {
                Logger.InfoFormat("No PaaSDiagnostics extension found...");
            }

            Logger.InfoFormat("Preparing temp storage account {0}...", temporaryStorage.Name);
            var keys = await storageClient.StorageAccounts.GetKeysAsync(temporaryStorage.Name);
            var csa = new CloudStorageAccount(new StorageCredentials(temporaryStorage.Name, keys.PrimaryKey), true);
            var client = csa.CreateCloudBlobClient();
            var container = client.GetContainerReference("acd-temp-" + Guid.NewGuid().ToString("N").ToLower());

            Logger.InfoFormat("Creating temp container {0}...", container.Name);
            await container.CreateIfNotExistsAsync();

            Logger.InfoFormat("Downloading package to storage account {0}...", temporaryStorage.Name);
            var getPackageOperation = await computeClient.Deployments.GetPackageBySlotAsync(service.ServiceName, slot, new DeploymentGetPackageParameters
            {
                ContainerUri = container.Uri
            });
            while (getPackageOperation.Status == OperationStatus.InProgress)
            {
                Logger.InfoFormat("Waiting for operation to finish...");
                await Task.Delay(5000);
                getPackageOperation = await computeClient.GetOperationStatusAsync(getPackageOperation.RequestId);
            }

            Logger.InfoFormat("Downloading from storage to {0}...", downloadToPath);
            var result = await container.ListBlobsSegmentedAsync(null);
            var prefix = string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH-mm"), service.ServiceName, slot);
            foreach (var item in result.Results)
            {
                var cloudblob = client.GetBlobReferenceFromServer(item.StorageUri);
                string filename = prefix + " " + cloudblob.Name;
                Logger.Info("Downloading: " + filename);
                await cloudblob.DownloadToFileAsync(Path.Combine(downloadToPath, filename), FileMode.Create);
            }

            if (pubConfig != null)
            {
                Logger.InfoFormat("Writing PubConfig to {0}...", prefix + ".PubConfig.xml");
                string pubconfigFilename = Path.Combine(downloadToPath, prefix + ".PubConfig.xml");
                File.WriteAllText(pubconfigFilename, pubConfig);
            }

            Logger.Info("Cleaning up temp storage...");
            await container.DeleteAsync();

            Logger.Info("Package download succesful");
        }
    }
}
