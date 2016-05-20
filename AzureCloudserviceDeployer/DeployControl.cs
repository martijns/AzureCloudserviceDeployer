﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MsCommon.ClickOnce;
using MsCommon.ClickOnce.Extensions;
using log4net;
using Microsoft.WindowsAzure.Subscriptions.Models;
using Microsoft.WindowsAzure.Management.Compute.Models;
using Microsoft.WindowsAzure.Management.Storage.Models;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace AzureCloudserviceDeployer
{
    public partial class DeployControl : AppControl
    {
        private static ILog Logger = LogManager.GetLogger(typeof(DeployControl));

        private MainForm _parent;
        private string _selectedPackage;
        private string _selectedConfig;
        private string _selectedDiag;
        private string _id;

        public DeployControl(MainForm parent, string id)
        {
            _parent = parent;
            _id = "#" + id;
            InitializeComponent();
            HandleDeploymentTypeChanged(this, EventArgs.Empty);
            UpdateFormState();
            lblLabelPreview.Text = GetRenderedLabel();
            Tag = "KEEP_ENABLED";
        }

        private void HandleTooltipClick(object sender, EventArgs e)
        {
            LogMethodEntry();
            var control = sender as Control;
            if (control == null)
                return;

            string tooltipMsg = toolTip1.GetToolTip(control);
            toolTip1.Show(tooltipMsg, control);
        }

        private void HandleLabelPreviewClicked(object sender, EventArgs e)
        {
            Clipboard.SetText(GetRenderedLabel());
        }

        private void ClearAllSelectedFiles()
        {
            LogMethodEntry();
            HandleClearDiagConfig(this, EventArgs.Empty);
            HandleClearCloudConfig(this, EventArgs.Empty);
            HandleClearCloudPackage(this, EventArgs.Empty);
        }

        private void HandleClearDiagConfig(object sender, EventArgs e)
        {
            LogMethodEntry();
            if (_selectedDiag != null)
            {
                _selectedDiag = null;
                lblSelectedDiag.Text = "<cleared>";
            }
        }

        private void HandleClearCloudConfig(object sender, EventArgs e)
        {
            Logger.Debug("[" + _id + "] " + "HandleClearCloudConfig");
            if (_selectedConfig != null)
            {
                _selectedConfig = null;
                lblSelectedConfig.Text = "<cleared>";
            }
        }

        private void HandleClearCloudPackage(object sender, EventArgs e)
        {
            LogMethodEntry();
            if (_selectedPackage != null)
            {
                _selectedPackage = null;
                lblSelectedPackage.Text = "<cleared>";
            }
        }

        private void HandleLabelKeyUp(object sender, KeyEventArgs e)
        {
            lblLabelPreview.Text = GetRenderedLabel();
        }

        private async void HandleDeployClicked(object sender, EventArgs e)
        {
            Logger.Debug("[" + _id + "] " + "HandleDeployClicked");
            await PerformWorkAsync(bgwork: null, fgwork: async () =>
            {
                try
                {
                    //await Task.Delay(5000);
                    //return;

                    var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                    var service = cbCloudservices.SelectedItem as HostedServiceListResponse.HostedService;
                    var packageStorage = cbPackageStorage.SelectedItem as StorageAccount;
                    var slot = (DeploymentSlot)cbSlot.SelectedItem;
                    var pref = ((KeyValuePair<UpgradePreference, string>)cbUpgradePreference.SelectedItem).Key;
                    var diagstorage = cbDiagStorage.SelectedItem as StorageAccount;
                    lblLabelPreview.Text = GetRenderedLabel();
                    var deploymentLabel = GetRenderedLabel();

                    if (Configuration.Instance.AutoDownloadPackageBeforeDeploy && !CheckAndChoosePackageDownloadLocation())
                        throw new ApplicationException("A download location for packages must be selected");
                    if (subscription == null)
                        throw new ApplicationException("Subscription must be selected");
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
                    if (!File.Exists(_selectedPackage))
                        throw new ApplicationException("The specified .cspkg no longer exists on the filesystem: " + _selectedPackage);
                    if (!File.Exists(_selectedConfig))
                        throw new ApplicationException("The specified .cscfg no longer exists on the filesystem:" + _selectedConfig);
                    if (_selectedDiag != null && !File.Exists(_selectedDiag))
                        throw new ApplicationException("The specified diagnostics configuration file no longer exists on the filesystem: " + _selectedDiag);

                    try
                    {
                        if (Configuration.Instance.AutoDownloadPackageBeforeDeploy)
                            await AzureHelper.DownloadDeploymentAsync(_id, subscription, service, slot, packageStorage, Configuration.Instance.PackageDownloadPath, true);

                        await AzureHelper.DeployAsync(_id, subscription, service, packageStorage, slot, pref, _selectedPackage, _selectedConfig, _selectedDiag, diagstorage, deploymentLabel, Configuration.Instance.CleanupUnusedExtensions, cbForceUpgrade.Checked);

                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Successfully deployed", ToolTipIcon.Info);
                        _parent.ColorTab(_id, Color.LightGreen);
                    }
                    catch (CloudException cex)
                    {
                        Logger.Error("[" + _id + "] " + cex.Message);
                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed deployment", ToolTipIcon.Error);
                        _parent.ColorTab(_id, Color.LightPink);
                    }
                    catch (StorageException sex)
                    {
                        Logger.Error("[" + _id + "] " + sex.Message);
                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed deployment", ToolTipIcon.Error);
                        _parent.ColorTab(_id, Color.LightPink);
                    }
                }
                catch (ApplicationException aex)
                {
                    // We use ApplicationExceptions to indicate a custom error. All other errors will result in a ReportBug dialog.
                    MessageBox.Show(this, "[" + _id + "] " + "Deployment failed: " + aex.Message);
                }
            });
        }


        private void HandleDeploymentTypeChanged(object sender, EventArgs e)
        {
            LogMethodEntry();
            if (cbUpgradePreference.SelectedIndex == -1)
            {
                this.UpdateControlEnabledState(cbForceUpgrade, false);
                return;
            }
            var pref = ((KeyValuePair<UpgradePreference, string>)cbUpgradePreference.SelectedItem).Key;
            switch (pref)
            {
                case UpgradePreference.UpgradeSimultaneously:
                case UpgradePreference.UpgradeWithUpdateDomains:
                    this.UpdateControlEnabledState(cbForceUpgrade, true);
                    break;
                default:
                    this.UpdateControlEnabledState(cbForceUpgrade, false);
                    break;
            }
        }

        private void HandleCloudserviceChanged(object sender, EventArgs e)
        {
            LogMethodEntry();
            ClearAllSelectedFiles();
        }

        private void HandlePackagestorageChanged(object sender, EventArgs e)
        {
            LogMethodEntry();
            ClearAllSelectedFiles();
        }

        private void HandleSlotChanged(object sender, EventArgs e)
        {
            LogMethodEntry();
            ClearAllSelectedFiles();
        }

        private void HandleSelectCloudPackage(object sender, EventArgs e)
        {
            LogMethodEntry();
            UpdateSelectedFiles(SelectFile("Cloud Package|*.cspkg|All Files|*.*"));
        }

        private void HandleSelectCloudConfig(object sender, EventArgs e)
        {
            LogMethodEntry();
            UpdateSelectedFiles(SelectFile("Cloud Config|*.cscfg|All Files|*.*"));
        }

        private void HandleSelectCloudDiag(object sender, EventArgs e)
        {
            LogMethodEntry();
            UpdateSelectedFiles(SelectFile("Cloud Diag Config|*.PubConfig.xml|All Files|*.*"));
        }

        private string SelectFile(string filter)
        {
            LogMethodEntry();
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

        private void UpdateSelectedFiles(string path)
        {
            LogMethodEntry();
            if (path == null)
                return;
            if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                foreach (var file in Directory.EnumerateFileSystemEntries(path))
                    UpdateSelectedFiles(file);
            }
            if (path.EndsWith(".cspkg", StringComparison.OrdinalIgnoreCase))
                _selectedPackage = path;
            if (path.EndsWith(".cscfg", StringComparison.OrdinalIgnoreCase))
                _selectedConfig = path;
            if (path.IndexOf(".pubconfig", StringComparison.OrdinalIgnoreCase) >= 0 && path.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                _selectedDiag = path;
            lblSelectedPackage.Text = Path.GetFileName(_selectedPackage);
            lblSelectedConfig.Text = Path.GetFileName(_selectedConfig);
            lblSelectedDiag.Text = Path.GetFileName(_selectedDiag);
        }

        public async Task UpdateSubscriptions(string userAccount)
        {
            LogMethodEntry();
            await PerformWorkAsync(null, async () =>
            {
                var subscriptions = await AzureHelper.GetSubscriptionsAsync(_id, userAccount);
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
            await HandleSubscriptionIndexChangedAsync(sender, e);
        }

        private async Task HandleSubscriptionIndexChangedAsync(object sender, EventArgs e)
        {
            LogMethodEntry();
            await PerformWorkAsync(null, async () =>
            {
                var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                if (subscription == null)
                    return;

                // Clear currently selected files
                ClearAllSelectedFiles();

                // Load cloudservices
                var hostedservices = await AzureHelper.GetCloudservicesAsync(_id, subscription);
                //AzureHelper.Test(subscription);
                cbCloudservices.Items.Clear();
                foreach (var service in hostedservices.OrderBy(s => s.ServiceName))
                {
                    cbCloudservices.Items.Add(service);
                }
                cbCloudservices.ValueMember = "ServiceName";

                // Load storageaccounts
                var storageaccounts = await AzureHelper.GetStorageAccountsAsync(_id, subscription);
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
                cbUpgradePreference.Items.Add(new KeyValuePair<UpgradePreference, string>(UpgradePreference.DeleteAndCreateDeploymentInitiallyStopped, UpgradePreference.DeleteAndCreateDeploymentInitiallyStopped.GetDescription()));
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
            });
        }

        private async void HandleDownloadPackageClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            await PerformWorkAsync(null, async () =>
            {
                var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                var service = cbCloudservices.SelectedItem as HostedServiceListResponse.HostedService;
                var packageStorage = cbPackageStorage.SelectedItem as StorageAccount;
                var slot = (DeploymentSlot)cbSlot.SelectedItem;

                try
                {
                    if (!CheckAndChoosePackageDownloadLocation())
                        throw new ApplicationException("A download location for packages must be selected");
                    if (subscription == null)
                        throw new ApplicationException("Subscription must be selectered");
                    if (service == null)
                        throw new ApplicationException("Cloudservice must be selected");
                    if (packageStorage == null)
                        throw new ApplicationException("Package storage account must be selected");

                    try
                    {
                        await AzureHelper.DownloadDeploymentAsync(_id, subscription, service, slot, packageStorage, Configuration.Instance.PackageDownloadPath);

                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Package downloaded", ToolTipIcon.Info);
                        _parent.ColorTab(_id, Color.LightGreen);
                    }
                    catch (CloudException cex)
                    {
                        Logger.Error("[" + _id + "] " + cex.Message);
                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed downloading package", ToolTipIcon.Error);
                        _parent.ColorTab(_id, Color.LightPink);
                    }
                    catch (StorageException sex)
                    {
                        Logger.Error("[" + _id + "] " + sex.Message);
                        _parent.ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed downloading package", ToolTipIcon.Error);
                        _parent.ColorTab(_id, Color.LightPink);
                    }
                }
                catch (ApplicationException aex)
                {
                    // We use ApplicationExceptions to indicate a custom error. All other errors will result in a ReportBug dialog.
                    MessageBox.Show(this, "[" + _id + "] " + "Package download failed: " + aex.Message);
                }
            });
        }

        private string GetRenderedLabel()
        {
            string label = tbLabel.Text;
            label = label.Replace("[UTCDT]", DateTime.UtcNow.ToString("u"));
            label = label.Replace("[MACHINE]", Environment.MachineName);
            label = label.Replace("[USER]", Environment.UserName);
            return label;
        }

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            LogMethodEntry();
            if (_parent.IsBusy() || this.IsBusy())
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            LogMethodEntry();
            if (_parent.IsBusy() || this.IsBusy())
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
                UpdateSelectedFiles(file);
        }

        private void UpdateFormState()
        {
            LogMethodEntry();
            this.UpdateControlEnabledState(cbSubscriptions, _parent.IsAuthenticated);
            var state = _parent.IsAuthenticated && cbSubscriptions.SelectedItem != null;
            this.UpdateControlEnabledState(cbCloudservices, state);
            this.UpdateControlEnabledState(cbPackageStorage, state);
            this.UpdateControlEnabledState(cbSlot, state);
            this.UpdateControlEnabledState(cbUpgradePreference, state);
            this.UpdateControlEnabledState(btnBrowseCloudPackage, state);
            this.UpdateControlEnabledState(btnBrowseCloudConfig, state);
            this.UpdateControlEnabledState(btnBrowseDiagnostics, state);
            this.UpdateControlEnabledState(btnClearCloudPackage, state);
            this.UpdateControlEnabledState(btnClearCloudConfig, state);
            this.UpdateControlEnabledState(btnClearDiagConfig, state);
            this.UpdateControlEnabledState(cbDiagStorage, state);
            this.UpdateControlEnabledState(tbLabel, state);
            this.UpdateControlEnabledState(btnDeploy, state);
            this.UpdateControlEnabledState(btnDownloadExistingPackage, state);
            this.UpdateControlEnabledState(lblPreview, state);
            this.UpdateControlEnabledState(lblLabelPreview, state);
        }

        private bool CheckAndChoosePackageDownloadLocation()
        {
            LogMethodEntry();
            if (string.IsNullOrEmpty(Configuration.Instance.PackageDownloadPath) || !Directory.Exists(Configuration.Instance.PackageDownloadPath))
            {
                _parent.ChoosePackageDownloadLocation();
            }

            if (string.IsNullOrEmpty(Configuration.Instance.PackageDownloadPath) || !Directory.Exists(Configuration.Instance.PackageDownloadPath))
            {
                return false;
            }
            return true;
        }

        public DeployControlState PersistState()
        {
            LogMethodEntry();
            return new DeployControlState
            {
                SubscriptionName = (cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription)?.SubscriptionName,
                CloudServiceName = (cbCloudservices.SelectedItem as HostedServiceListResponse.HostedService)?.ServiceName,
                PackageStorageAccountName = (cbPackageStorage.SelectedItem as StorageAccount)?.Name,
                DeploymentSlot = cbSlot.Enabled ? cbSlot.SelectedItem.ToString() : null,
                DeploymentType = cbUpgradePreference.Enabled ? ((KeyValuePair<UpgradePreference, string>)cbUpgradePreference.SelectedItem).Key.ToString() : null,
                ForceUpgrade = cbForceUpgrade.Checked,
                CloudPackage = _selectedPackage,
                CloudConfig = _selectedConfig,
                DiagConfig = _selectedDiag,
                DiagStorageAccountName = (cbDiagStorage.SelectedItem as StorageAccount)?.Name,
                Label = tbLabel.Text
            };
        }

        public async Task RestoreState(DeployControlState state)
        {
            LogMethodEntry();
            cbSubscriptions.SelectedItem = cbSubscriptions.Items.OfType<SubscriptionListOperationResponse.Subscription>().Where(s => s.SubscriptionName == state.SubscriptionName).FirstOrDefault();
            if (cbSubscriptions.SelectedItem == null)
                Logger.Warn("[" + _id + "] " + "Cannot restore state as the subscription cannot be selected. Are you logged in with the correct user?");
            await HandleSubscriptionIndexChangedAsync(this, EventArgs.Empty);
            cbCloudservices.SelectedItem = cbCloudservices.Items.OfType<HostedServiceListResponse.HostedService>().Where(s => s.ServiceName == state.CloudServiceName).FirstOrDefault();
            cbPackageStorage.SelectedItem = cbPackageStorage.Items.OfType<StorageAccount>().Where(s => s.Name == state.PackageStorageAccountName).FirstOrDefault();
            cbSlot.SelectedItem = cbSlot.Items.OfType<DeploymentSlot>().Where(s => s.ToString() == state.DeploymentSlot).FirstOrDefault();
            cbUpgradePreference.SelectedItem = cbUpgradePreference.Items.OfType<KeyValuePair<UpgradePreference, string>>().Where(s => s.Key.ToString() == state.DeploymentType).FirstOrDefault();
            HandleDeploymentTypeChanged(this, EventArgs.Empty);
            cbDiagStorage.SelectedItem = cbDiagStorage.Items.OfType<StorageAccount>().Where(s => s.Name == state.DiagStorageAccountName).FirstOrDefault();
            if (cbDiagStorage.SelectedItem == null && cbDiagStorage.Items.Count > 0)
                cbDiagStorage.SelectedItem = cbDiagStorage.Items[0];
            cbForceUpgrade.Checked = state.ForceUpgrade;
            tbLabel.Text = state.Label;
            UpdateSelectedFiles(state.CloudPackage);
            UpdateSelectedFiles(state.CloudConfig);
            UpdateSelectedFiles(state.DiagConfig);
        }
    }
}
