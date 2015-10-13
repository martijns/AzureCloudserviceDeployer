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
        RemoveCreateDeploymentIfExists = 1,
        UpgradeIfDeploymentExists = 2
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
            string pathToCscfg, string pathToDiagExtensionConfig, StorageAccount diagStorage = null)
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

            // Check if diag extension already exists for this service. If so, delete it.
            Logger.InfoFormat("Retrieving current extensions for {0}...", service.ServiceName);
            var currentExtensions = await computeClient.HostedServices.ListExtensionsAsync(service.ServiceName);
            foreach (var currentDiagExtension in currentExtensions.Where(d => d.Type == availableDiagnosticsExtension.Type))
            {
                Logger.InfoFormat("Deleting diagnostics extension named {0}...", currentDiagExtension.Id);
                try
                {
                    await computeClient.HostedServices.DeleteExtensionAsync(service.ServiceName, currentDiagExtension.Id);
                }
                catch (Exception ex)
                {
                    Logger.WarnFormat("Couldn't delete extension {0}, might be in use...", currentDiagExtension.Id);
                }
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
            var deploymentLabel = DateTime.UtcNow.ToString("u") + " " + Environment.MachineName + " " + Environment.UserName;
            var deployParams = new DeploymentCreateParameters
            {
                StartDeployment = true,
                Name = Guid.NewGuid().ToString("N"),
                Configuration = csCfg,
                PackageUri = blobRef.Uri,
                Label = deploymentLabel,
                ExtensionConfiguration = extensionConfiguration
            };
            var upgradeParams = new DeploymentUpgradeParameters
            {
                Configuration = csCfg,
                PackageUri = blobRef.Uri,
                Label = deploymentLabel,
                ExtensionConfiguration = extensionConfiguration,
                Mode = DeploymentUpgradeMode.Auto
            };

            switch (upgradePreference)
            {
                case UpgradePreference.RemoveCreateDeploymentIfExists:
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
                case UpgradePreference.UpgradeIfDeploymentExists:
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
    }
}
