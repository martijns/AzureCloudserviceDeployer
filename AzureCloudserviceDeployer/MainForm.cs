using log4net.Appender;
using log4net.Repository.Hierarchy;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute.Models;
using Microsoft.WindowsAzure.Management.Storage.Models;
using Microsoft.WindowsAzure.Subscriptions.Models;
using MsCommon.ClickOnce;
using MsCommon.ClickOnce.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Core;
using log4net;

namespace AzureCloudserviceDeployer
{
    public partial class MainForm : BaseForm, IAppender
    {
        private static ILog Logger = LogManager.GetLogger(typeof(MainForm));

        private bool _authenticated;
        private string _selectedPackage;
        private string _selectedConfig;
        private string _selectedDiag;

        public MainForm()
        {
            Logger.Debug("MainForm");
            InitializeComponent();
            ((Hierarchy)LogManager.GetRepository()).Root.AddAppender(this);
        }

        private void HandleMainformShown(object sender, EventArgs e)
        {
            Logger.Debug("HandleMainformShown");
            UpdateFormState();
            AppVersion.CheckForUpdateAsync();
        }

        private void HandleChangelogClicked(object sender, EventArgs e)
        {
            Logger.Debug("HandleChangelogClicked");
            AppVersion.DisplayChanges();
        }

        private void HandleAboutClicked(object sender, EventArgs e)
        {
            Logger.Debug("HandleAboutClicked");
            AppVersion.DisplayAbout();
        }

        private async void HandleAuthenticateClicked(object sender, EventArgs e)
        {
            Logger.Debug("HandleAuthenticateClicked");
            await PerformWorkAsync(this, null, async () =>
            {
                var auth = await AzureHelper.GetAuthentication(null, true);
                _authenticated = true;
                lblLoggedInUser.Text = auth.UserInfo.DisplayableId;
                await UpdateSubscriptions();
                UpdateFormState();
            });
        }

        private void HandleExitClicked(object sender, EventArgs e)
        {
            Logger.Debug("HandleExitClicked");
            Close();
        }

        private async Task UpdateSubscriptions()
        {
            Logger.Debug("UpdateSubscriptions");
            await PerformWorkAsync(this, null, async () =>
            {
                var subscriptions = await AzureHelper.GetSubscriptionsAsync();
                cbSubscriptions.Items.Clear();
                cbSubscriptions.Items.Add("");
                foreach (var sub in subscriptions.OrderBy(s => s.SubscriptionName))
                {
                    cbSubscriptions.Items.Add(sub);
                }
                cbSubscriptions.ValueMember = "SubscriptionName";

                UpdateFormState();
            });
        }

        private async void HandleSubscriptionIndexChanged(object sender, EventArgs e)
        {
            Logger.Debug("HandleSubscriptionIndexChanged");
            await PerformWorkAsync(this, null, async () =>
            {
                var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                if (subscription == null)
                    return;

                // Load cloudservices
                var hostedservices = await AzureHelper.GetCloudservicesAsync(subscription);
                //AzureHelper.Test(subscription);
                cbCloudservices.Items.Clear();
                foreach (var service in hostedservices.OrderBy(s => s.ServiceName))
                {
                    cbCloudservices.Items.Add(service);
                }
                cbCloudservices.ValueMember = "ServiceName";

                // Load storageaccounts
                var storageaccounts = await AzureHelper.GetStorageAccountsAsync(subscription);
                cbPackageStorage.Items.Clear();
                foreach (var account in storageaccounts.OrderBy(s => s.Name))
                {
                    cbPackageStorage.Items.Add(account);
                }
                cbPackageStorage.ValueMember = "Name";
                cbPackageStorage.SelectedIndex = 0;

                // Load slots
                cbSlot.Items.Clear();
                cbSlot.Items.Add(DeploymentSlot.Production);
                cbSlot.Items.Add(DeploymentSlot.Staging);
                cbSlot.SelectedIndex = 0;

                // Load upgrade preferences
                cbUpgradePreference.Items.Clear();
                cbUpgradePreference.Items.Add(new KeyValuePair<UpgradePreference, string>(UpgradePreference.UpgradeWithUpdateDomains, UpgradePreference.UpgradeWithUpdateDomains.GetDescription()));
                cbUpgradePreference.Items.Add(new KeyValuePair<UpgradePreference, string>(UpgradePreference.UpgradeSimultaneously, UpgradePreference.UpgradeSimultaneously.GetDescription()));
                cbUpgradePreference.Items.Add(new KeyValuePair<UpgradePreference, string>(UpgradePreference.DeleteAndCreateDeployment, UpgradePreference.DeleteAndCreateDeployment.GetDescription()));
                cbUpgradePreference.ValueMember = "Key";
                cbUpgradePreference.DisplayMember = "Value";
                cbUpgradePreference.SelectedIndex = 0;

                // Load diagnostics storage
                cbDiagStorage.Items.Clear();
                cbDiagStorage.Items.Add("Extract from .cscfg diag connection string");
                foreach (var account in storageaccounts.OrderBy(s => s.Name))
                {
                    cbDiagStorage.Items.Add(account);
                }
                cbDiagStorage.ValueMember = "Name";
                cbDiagStorage.SelectedIndex = 0;

                UpdateFormState();
                HandleGenerateLabelClicked(this, EventArgs.Empty);
            });
        }

        private void UpdateFormState()
        {
            UpdateControlEnabledState(cbSubscriptions, _authenticated);
            var state = _authenticated && cbSubscriptions.SelectedItem != null;
            UpdateControlEnabledState(cbCloudservices, state);
            UpdateControlEnabledState(cbPackageStorage, state);
            UpdateControlEnabledState(cbSlot, state);
            UpdateControlEnabledState(cbUpgradePreference, state);
            UpdateControlEnabledState(btnBrowseCloudPackage, state);
            UpdateControlEnabledState(btnBrowseCloudConfig, state);
            UpdateControlEnabledState(btnBrowseDiagnostics, state);
            UpdateControlEnabledState(cbDiagStorage, state);
            UpdateControlEnabledState(btnGenerateLabel, state);
            UpdateControlEnabledState(tbLabel, state);
            UpdateControlEnabledState(btnDeploy, state);
        }

        private string SelectFile(string filter)
        {
            Logger.Debug("SelectFile");
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = filter;
            dialog.Multiselect = false;
            dialog.ReadOnlyChecked = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return null;
            return dialog.FileName;
        }

        private void HandleSelectCloudPackage(object sender, EventArgs e)
        {
            Logger.Debug("HandleSelectCloudPackage");
            UpdateSelectedFiles(SelectFile("Cloud Package|*.cspkg|All Files|*.*"));
        }

        private void HandleSelectCloudConfig(object sender, EventArgs e)
        {
            Logger.Debug("HandleSelectCloudConfig");
            UpdateSelectedFiles(SelectFile("Cloud Config|*.cscfg|All Files|*.*"));
        }

        private void HandleSelectCloudDiag(object sender, EventArgs e)
        {
            Logger.Debug("HandleSelectCloudDiag");
            UpdateSelectedFiles(SelectFile("Cloud Diag Config|*.PubConfig.xml|All Files|*.*"));
        }

        private void UpdateSelectedFiles(string path)
        {
            Logger.Debug("UpdateSelectedFiles");
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach (var file in Directory.EnumerateFileSystemEntries(path))
                    UpdateSelectedFiles(file);
            }
            if (path == null)
                return;
            if (path.EndsWith(".cspkg", StringComparison.OrdinalIgnoreCase))
                _selectedPackage = path;
            if (path.EndsWith(".cscfg", StringComparison.OrdinalIgnoreCase))
                _selectedConfig = path;
            if (path.EndsWith(".pubconfig.xml", StringComparison.OrdinalIgnoreCase))
                _selectedDiag = path;
            lblSelectedPackage.Text = Path.GetFileName(_selectedPackage);
            lblSelectedConfig.Text = Path.GetFileName(_selectedConfig);
            lblSelectedDiag.Text = Path.GetFileName(_selectedDiag);
        }

        private async void HandleDeployClicked(object sender, EventArgs e)
        {
            Logger.Debug("HandleDeployClicked");
            await PerformWorkAsync(this, null, async () =>
            {
                var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                var service = cbCloudservices.SelectedItem as HostedServiceListResponse.HostedService;
                var packageStorage = cbPackageStorage.SelectedItem as StorageAccount;
                var slot = (DeploymentSlot)cbSlot.SelectedItem;
                var pref = ((KeyValuePair<UpgradePreference,string>)cbUpgradePreference.SelectedItem).Key;
                var diagstorage = cbDiagStorage.SelectedItem as StorageAccount;
                var deploymentLabel = tbLabel.Text;

                try
                {
                    if (subscription == null)
                        throw new ApplicationException("Subscription must be selectered");
                    if (service == null)
                        throw new ApplicationException("Cloudservice must be selected");
                    if (packageStorage == null)
                        throw new ApplicationException("Package storage account must be selected");
                    if (_selectedPackage == null)
                        throw new ApplicationException("A .cspkg file must be selected");
                    if (_selectedConfig == null)
                        throw new ApplicationException("A .cscfg file must be selected");
                    if (_selectedDiag == null)
                        if (MessageBox.Show("Diagnostics configuration is not selected. Are you sure you want to continue?", "Diagnostics Configuration", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;
                    
                    await AzureHelper.DeployAsync(subscription, service, packageStorage, slot, pref, _selectedPackage, _selectedConfig, _selectedDiag, diagstorage, deploymentLabel);
                }
                catch (ApplicationException aex)
                {
                    // We use ApplicationExceptions to indicate a custom error. All other errors will result in a ReportBug dialog.
                    MessageBox.Show("Deployment failed: " + aex.Message);
                }
            });
        }

        private void AddToLog(string message)
        {
            if (lbLog.InvokeRequired)
            {
                lbLog.Invoke((Action<string>)AddToLog, message);
                return;
            }
            string time = DateTime.Now.ToString("HH:mm:ss");
            lbLog.Items.Insert(0, time + ": " + message);
            while (lbLog.Items.Count > 250)
            {
                lbLog.Items.RemoveAt(lbLog.Items.Count - 1);
            }
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level >= Level.Info)
            {
                AddToLog(loggingEvent.RenderedMessage);
            }
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
                UpdateSelectedFiles(file);
        }

        private void HandleGenerateLabelClicked(object sender, EventArgs e)
        {
            tbLabel.Text = DateTime.UtcNow.ToString("u") + " " + Environment.MachineName + " " + Environment.UserName + " ";
            if (btnGenerateLabel.ContainsFocus)
                tbLabel.Focus();
            tbLabel.Select(tbLabel.Text.Length, 0);
        }
    }
}
