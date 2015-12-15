﻿using log4net.Appender;
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
using System.Runtime.CompilerServices;

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
            LogMethodEntry();
            InitializeComponent();
            lbLog.DrawMode = DrawMode.OwnerDrawFixed;
            lbLog.DrawItem += HandleDrawLogItem;
            ((Hierarchy)LogManager.GetRepository()).Root.AddAppender(this);

            // Load options from configuration
            optionCleanupUnusedExtensionsToolStripMenuItem.Checked = Configuration.Instance.CleanupUnusedExtensions;
            optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Checked = Configuration.Instance.AutoDownloadPackageBeforeDeploy;
            flashApplicationToolStripMenuItem.Checked = Configuration.Instance.FlashWindowWhenDone;
            showNotificationWhenDoneToolStripMenuItem.Checked = Configuration.Instance.NotifyWhenDone;
        }

        private void HandleMainformShown(object sender, EventArgs e)
        {
            LogMethodEntry();
            UpdateFormState();
            AppVersion.CheckForUpdateAsync();
            lblLabelPreview.Text = GetRenderedLabel();
        }

        private void HandleChangelogClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            AppVersion.DisplayChanges();
        }

        private void HandleAboutClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            AppVersion.DisplayAbout();
        }

        private async void HandleAuthenticateClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            await PerformWorkAsync(this, null, async () =>
            {
                AuthenticationResult auth = null;
                try
                {
                    auth = AzureHelper.GetAuthentication(null, true);
                }
                catch (AdalException aex)
                {
                    Logger.Error(aex.Message);
                    return;
                }
                _authenticated = true;
                lblLoggedInUser.Text = auth.UserInfo.DisplayableId;
                await UpdateSubscriptions();
                UpdateFormState();
            });
        }

        private void HandleExitClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            Close();
        }

        private async Task UpdateSubscriptions()
        {
            LogMethodEntry();
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
            LogMethodEntry();
            await PerformWorkAsync(this, null, async () =>
            {
                var subscription = cbSubscriptions.SelectedItem as SubscriptionListOperationResponse.Subscription;
                if (subscription == null)
                    return;

                // Clear currently selected files
                ClearAllSelectedFiles();

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

        private void UpdateFormState()
        {
            LogMethodEntry();
            UpdateControlEnabledState(cbSubscriptions, _authenticated);
            var state = _authenticated && cbSubscriptions.SelectedItem != null;
            UpdateControlEnabledState(cbCloudservices, state);
            UpdateControlEnabledState(cbPackageStorage, state);
            UpdateControlEnabledState(cbSlot, state);
            UpdateControlEnabledState(cbUpgradePreference, state);
            UpdateControlEnabledState(btnBrowseCloudPackage, state);
            UpdateControlEnabledState(btnBrowseCloudConfig, state);
            UpdateControlEnabledState(btnBrowseDiagnostics, state);
            UpdateControlEnabledState(btnClearCloudPackage, state);
            UpdateControlEnabledState(btnClearCloudConfig, state);
            UpdateControlEnabledState(btnClearDiagConfig, state);
            UpdateControlEnabledState(cbDiagStorage, state);
            UpdateControlEnabledState(tbLabel, state);
            UpdateControlEnabledState(btnDeploy, state);
            UpdateControlEnabledState(btnDownloadExistingPackage, state);
            UpdateControlEnabledState(lblPreview, state);
            UpdateControlEnabledState(lblLabelPreview, state);
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
                lblLabelPreview.Text = GetRenderedLabel();
                var deploymentLabel = GetRenderedLabel();

                try
                {
                    if (Configuration.Instance.AutoDownloadPackageBeforeDeploy && !CheckAndChoosePackageDownloadLocation())
                        throw new ApplicationException("A download location for packages must be selected");
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
                    if (!File.Exists(_selectedPackage))
                        throw new ApplicationException("The specified .cspkg no longer exists on the filesystem: " + _selectedPackage);
                    if (!File.Exists(_selectedConfig))
                        throw new ApplicationException("The specified .cscfg no longer exists on the filesystem:" + _selectedConfig);
                    if (_selectedDiag != null && !File.Exists(_selectedDiag))
                        throw new ApplicationException("The specified diagnostics configuration file no longer exists on the filesystem: " + _selectedDiag);

                    try
                    {
                        if (Configuration.Instance.AutoDownloadPackageBeforeDeploy)
                            await AzureHelper.DownloadDeploymentAsync(subscription, service, slot, packageStorage, Configuration.Instance.PackageDownloadPath, true);

                        await AzureHelper.DeployAsync(subscription, service, packageStorage, slot, pref, _selectedPackage, _selectedConfig, _selectedDiag, diagstorage, deploymentLabel, Configuration.Instance.CleanupUnusedExtensions);

                        ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Successfully deployed", ToolTipIcon.Info);
                    }
                    catch (CloudException cex)
                    {
                        Logger.Error(cex.Message);
                        ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed deployment", ToolTipIcon.Error);
                    }
                }
                catch (ApplicationException aex)
                {
                    // We use ApplicationExceptions to indicate a custom error. All other errors will result in a ReportBug dialog.
                    MessageBox.Show(this, "Deployment failed: " + aex.Message);
                }
            });
        }

        #region Logstuff

        private string _LastLogMessage = null;
        private int _LastLogMessageCount = 0;

        private void AddToLog(LogItem logitem)
        {
            if (lbLog.InvokeRequired)
            {
                lbLog.Invoke((Action<LogItem>)AddToLog, logitem);
                return;
            }

            if (_LastLogMessage != logitem.Message)
            {
                _LastLogMessage = logitem.Message;
                _LastLogMessageCount = 1;
            }
            else
            {
                _LastLogMessageCount++;
            }

            if (_LastLogMessageCount > 1)
            {
                logitem.Message += " (message repeated " + _LastLogMessageCount + " times)";
                lbLog.Items.RemoveAt(0);
            }

            lbLog.Items.Insert(0, logitem);
            while (lbLog.Items.Count > 250)
            {
                lbLog.Items.RemoveAt(lbLog.Items.Count - 1);
            }
        }

        private void HandleDrawLogItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (e.Index < 0 || e.Index >= lb.Items.Count)
                return;

            Color backColor = lb.BackColor;
            LogItem li = lb.Items[e.Index] as LogItem;
            if (li != null)
            {
                if (li.Level == Level.Warn)
                    backColor = Color.Yellow;
                if (li.Level == Level.Error)
                    backColor = Color.LightPink;
            }

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                backColor = ChangeColorBrightness(backColor, -0.1f);
            }

            Graphics g = e.Graphics;

            string msg = lb.Items[e.Index].ToString();

            int hzSize = (int)g.MeasureString(msg, lb.Font).Width;
            if (lb.HorizontalExtent < hzSize)
                lb.HorizontalExtent = hzSize;

            e.DrawBackground();
            g.FillRectangle(new SolidBrush(backColor), e.Bounds);
            g.DrawString(msg, e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// http://www.pvladov.com/2012/09/make-color-lighter-or-darker.html
        /// </summary>
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            if (loggingEvent.Level >= Level.Info)
            {
                AddToLog(new LogItem { TimeStamp = loggingEvent.TimeStamp, Level = loggingEvent.Level, Message = loggingEvent.RenderedMessage });
            }
        }

        public class LogItem
        {
            public DateTime TimeStamp { get; set; }
            public Level Level { get; set; }
            public string Message { get; set; }

            public override string ToString()
            {
                return string.Format("{0}: {1}", TimeStamp.ToString("HH:mm:ss"), Message);
            }

            public string ToStringWithLevel()
            {
                return string.Format("{0} [{2}] {1}", TimeStamp.ToString("HH:mm:ss"), Message, Level.ToString());
            }
        }

        private void HandleLogCopyClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            if (lbLog.SelectedItems.Count == 0)
                return;

            string[] selectedLog = lbLog.SelectedItems.Cast<LogItem>().Select(li => li.ToStringWithLevel()).ToArray();
            Clipboard.SetText(string.Join("\r\n", selectedLog));
        }

        #endregion

        private void HandleDragEnter(object sender, DragEventArgs e)
        {
            LogMethodEntry();
            if (IsBusy)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void HandleDragDrop(object sender, DragEventArgs e)
        {
            LogMethodEntry();
            if (IsBusy)
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
                UpdateSelectedFiles(file);
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
            Logger.Debug("HandleClearCloudConfig");
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

        private string GetRenderedLabel()
        {
            string label = tbLabel.Text;
            label = label.Replace("[UTCDT]", DateTime.UtcNow.ToString("u"));
            label = label.Replace("[MACHINE]", Environment.MachineName);
            label = label.Replace("[USER]", Environment.UserName);
            return label;
        }

        private void HandleLabelKeyUp(object sender, KeyEventArgs e)
        {
            lblLabelPreview.Text = GetRenderedLabel();
        }

        private void HandleOptionCleanupUnusedDiagnosticsExtensions(object sender, EventArgs e)
        {
            LogMethodEntry();
            optionCleanupUnusedExtensionsToolStripMenuItem.Checked = !optionCleanupUnusedExtensionsToolStripMenuItem.Checked;
            Configuration.Instance.CleanupUnusedExtensions = optionCleanupUnusedExtensionsToolStripMenuItem.Checked;
            Configuration.Instance.Save();
        }

        private void HandleAutoDownloadExistingPackageOptionClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Checked = !optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Checked;
            Configuration.Instance.AutoDownloadPackageBeforeDeploy = optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Checked;
            Configuration.Instance.Save();
        }

        private async void HandleDownloadPackageClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            await PerformWorkAsync(this, null, async () =>
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
                        await AzureHelper.DownloadDeploymentAsync(subscription, service, slot, packageStorage, Configuration.Instance.PackageDownloadPath);

                        ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Package downloaded", ToolTipIcon.Info);
                    }
                    catch (CloudException cex)
                    {
                        Logger.Error(cex.Message);
                        ActionCompleted("ACD: " + service.ServiceName + "/" + slot, "Failed downloading package", ToolTipIcon.Error);
                    }
                }
                catch (ApplicationException aex)
                {
                    // We use ApplicationExceptions to indicate a custom error. All other errors will result in a ReportBug dialog.
                    MessageBox.Show(this, "Package download failed: " + aex.Message);
                }
            });
        }

        private bool CheckAndChoosePackageDownloadLocation()
        {
            LogMethodEntry();
            if (string.IsNullOrEmpty(Configuration.Instance.PackageDownloadPath) || !Directory.Exists(Configuration.Instance.PackageDownloadPath))
            {
                ChoosePackageDownloadLocation();
            }

            if (string.IsNullOrEmpty(Configuration.Instance.PackageDownloadPath) || !Directory.Exists(Configuration.Instance.PackageDownloadPath))
            {
                return false;
            }
            return true;
        }

        private void ChoosePackageDownloadLocation()
        {
            LogMethodEntry();
            var dialog = new FolderBrowserDialog();
            dialog.Description = "Select a location to download packages to";
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            Configuration.Instance.PackageDownloadPath = dialog.SelectedPath;
            Configuration.Instance.Save();
        }

        private void HandleConfigureDownloadPathOptionClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            ChoosePackageDownloadLocation();
        }

        private void HandleSubmitFeedbackClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            new FeedbackForm().ShowDialog(this);
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

        private void ActionCompleted(string title, string message, ToolTipIcon icon)
        {
            LogMethodEntry();
            if (title == null)
                title = AppVersion.AppName;

            if (Configuration.Instance.FlashWindowWhenDone)
            {
                FlashWindow.Flash(this);
            }

            if (Configuration.Instance.NotifyWhenDone)
            {
                notifyIcon1.Icon = Icon;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(5000, title, message, icon);
                notifyIcon1.Visible = false;
            }
        }

        private void HandleFlashApplicationWhenDoneClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            flashApplicationToolStripMenuItem.Checked = !flashApplicationToolStripMenuItem.Checked;
            Configuration.Instance.FlashWindowWhenDone = flashApplicationToolStripMenuItem.Checked;
            Configuration.Instance.Save();
        }

        private void HandleShowNotificationWhenDoneClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            showNotificationWhenDoneToolStripMenuItem.Checked = !showNotificationWhenDoneToolStripMenuItem.Checked;
            Configuration.Instance.NotifyWhenDone = showNotificationWhenDoneToolStripMenuItem.Checked;
            Configuration.Instance.Save();
        }

        private void LogMethodEntry([CallerFilePath] string file = "", [CallerMemberName] string member = "", [CallerLineNumber] int line = 0)
        {
            Logger.DebugFormat("=> {0}.{1}:{2}", Path.GetFileNameWithoutExtension(file), member, line);
        }
    }
}
