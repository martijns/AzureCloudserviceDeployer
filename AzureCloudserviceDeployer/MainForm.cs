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
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AzureCloudserviceDeployer
{
    public partial class MainForm : AppForm
    {
        private static ILog Logger = LogManager.GetLogger(typeof(MainForm));
        private static int TabCounter = 1;

        internal bool IsAuthenticated { get; set; }

        public MainForm()
        {
            LogMethodEntry();
            InitializeComponent();
            SetLoggingListBox(lbLog);

            // Load options from configuration
            optionCleanupUnusedExtensionsToolStripMenuItem.Checked = Configuration.Instance.CleanupUnusedExtensions;
            optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Checked = Configuration.Instance.AutoDownloadPackageBeforeDeploy;
            flashApplicationToolStripMenuItem.Checked = Configuration.Instance.FlashWindowWhenDone;
            showNotificationWhenDoneToolStripMenuItem.Checked = Configuration.Instance.NotifyWhenDone;
        }

        internal string LoggedInUserEmail
        {
            get
            {
                return lblLoggedInUser.Text;
            }
        }

        private void HandleMainformShown(object sender, EventArgs e)
        {
            LogMethodEntry();
            AppVersion.CheckForUpdateAsync();
            tabControl1.TabPages.Clear();
            var deploy = new DeployControl(this, TabCounter.ToString());
            deploy.Dock = DockStyle.Fill;
            var page = new TabPage("#" + TabCounter++);
            page.Controls.Add(deploy);
            tabControl1.TabPages.Add(page);
            tabControl1.TabPages.Add(new TabPage("+"));
            UpdatePresetsMRU();
            HandleAuthenticateClicked(this, EventArgs.Empty);
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
            await PerformWorkAsync(null, async () =>
            {
                AuthenticationResult auth = null;
                try
                {
                    auth = AzureHelper.GetAuthentication("main", null, true);
                }
                catch (AdalException aex)
                {
                    Logger.Error(aex.Message);
                    return;
                }
                IsAuthenticated = true;
                lblLoggedInUser.Text = auth.UserInfo.DisplayableId;
                foreach (var control in tabControl1.TabPages.OfType<TabPage>().SelectMany(p => p.Controls.OfType<DeployControl>()))
                {
                    this.UpdateControlEnabledState(control, true);
                    await control.UpdateSubscriptions(lblLoggedInUser.Text);
                }
            });
        }

        private void HandleExitClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            Close();
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


        internal void ChoosePackageDownloadLocation()
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

        internal void ActionCompleted(string title, string message, ToolTipIcon icon)
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

        internal void ColorTab(string id, Color? color)
        {
            var tab = tabControl1.TabPages.Cast<TabPage>().Where(t => t.Text == id).FirstOrDefault();
            if (tab != null)
            {
                tab.Tag = color;
            }
            tabControl1.Invalidate();
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

        private async void HandleTabSelecting(object sender, TabControlCancelEventArgs e)
        {
            LogMethodEntry();
            if (e.TabPage == null)
                return;

            if (e.Action == TabControlAction.Selecting && e.TabPage.Text == "+")
            {
                var deploy = new DeployControl(this, TabCounter.ToString());
                deploy.Dock = DockStyle.Fill;
                e.TabPage.Text = "#" + TabCounter++;
                e.TabPage.Controls.Add(deploy);
                if (IsAuthenticated)
                    await deploy.UpdateSubscriptions(lblLoggedInUser.Text);

                tabControl1.TabPages.Add(new TabPage("+"));
            }
            if (e.TabPage.Tag != null)
            {
                e.TabPage.Tag = null;
                tabControl1.Invalidate();
            }
        }

        private void HandleClosePage(object sender, EventArgs e)
        {
            LogMethodEntry();
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void HandleDrawTab(object sender, DrawItemEventArgs e)
        {
            // This event is called once for each tab button in your tab control

            // First paint the background with a color based on the current tab

            // e.Index is the index of the tab in the TabPages collection.
            var color = tabControl1.TabPages[e.Index].Tag as Color?;
            if (!color.HasValue)
                color = Control.DefaultBackColor;
            e.Graphics.FillRectangle(new SolidBrush(color.Value), e.Bounds);

            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, this.Font, SystemBrushes.ControlText, paddedBounds);
        }

        private void HandleSavePresetClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            List<DeployControlState> states = new List<DeployControlState>();
            foreach (var control in tabControl1.TabPages.OfType<TabPage>().SelectMany(p => p.Controls.OfType<DeployControl>()))
            {
                var state = control.PersistState();
                states.Add(state);
            }
            string statesJson = JsonConvert.SerializeObject(states.ToArray(), Formatting.Indented);

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "ACD Preset File (*.acdpreset)|*.acdpreset|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(dialog.FileName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(statesJson);
                    }
                }
                UpdatePresetsMRU(dialog.FileName);
            }
        }

        private void HandleRestorePresetClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            var dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = "ACD Preset File (*.acdpreset)|*.acdpreset|All files (*.*)|*.*";
            dialog.Multiselect = false;
            dialog.ReadOnlyChecked = true;
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            RestorePresetFromFile(dialog.FileName);
        }

        private async void RestorePresetFromFile(string filepath)
        {
            LogMethodEntry();
            DeployControlState[] states = new DeployControlState[0];
            try
            {
                string statesJson = File.ReadAllText(filepath);
                states = JsonConvert.DeserializeObject<DeployControlState[]>(statesJson);
                UpdatePresetsMRU(filepath);
                if (states == null || states.Length == 0)
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to restore state: " + ex.Message);
                return;
            }

            tabControl1.Visible = false;
            tabControl1.SuspendDraw();
            try
            {
                TabCounter = 1;
                tabControl1.TabPages.Clear();
                foreach (var state in states)
                {
                    var deploy = new DeployControl(this, TabCounter.ToString());
                    deploy.Dock = DockStyle.Fill;
                    var page = new TabPage("#" + TabCounter++);
                    page.Controls.Add(deploy);
                    if (IsAuthenticated)
                        await deploy.UpdateSubscriptions(lblLoggedInUser.Text);
                    await deploy.RestoreState(state);
                    tabControl1.TabPages.Add(page);
                }
                tabControl1.TabPages.Add(new TabPage("+"));
            }
            finally
            {
                tabControl1.ResumeDraw();
                tabControl1.Visible = true;
            }
        }

        private void UpdatePresetsMRU(string lastAccessedFile = null)
        {
            LogMethodEntry();
            if (lastAccessedFile != null)
            {
                if (Configuration.Instance.MostRecentlyUsedPresets.Contains(lastAccessedFile))
                    Configuration.Instance.MostRecentlyUsedPresets.Remove(lastAccessedFile);
                Configuration.Instance.MostRecentlyUsedPresets.Insert(0, lastAccessedFile);

                while (Configuration.Instance.MostRecentlyUsedPresets.Count > 10)
                    Configuration.Instance.MostRecentlyUsedPresets.RemoveAt(10);

                Configuration.Instance.Save();
            }

            while (presetsToolStripMenuItem.DropDownItems.Count > 6)
                presetsToolStripMenuItem.DropDownItems.RemoveAt(6);

            foreach (string path in Configuration.Instance.MostRecentlyUsedPresets)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Path.GetFileNameWithoutExtension(path));
                item.Click += HandleMRUPresetClicked;
                item.Tag = path;
                item.ToolTipText = path;
                presetsToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void HandleMRUPresetClicked(object sender, EventArgs e)
        {
            LogMethodEntry();
            var item = sender as ToolStripMenuItem;
            if (item == null)
                return;
            RestorePresetFromFile(item.Tag as string);
        }

        private void HandleOptionConfigureDefaultLabelClicked(object sender, EventArgs e)
        {
            new ConfigureLabelForm().ShowDialog(this);
        }

        private void HandleClearMRUList(object sender, EventArgs e)
        {
            Configuration.Instance.MostRecentlyUsedPresets.Clear();
            Configuration.Instance.Save();
            UpdatePresetsMRU();
        }
    }
}
