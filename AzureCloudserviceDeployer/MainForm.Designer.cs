namespace AzureCloudserviceDeployer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionCleanupUnusedExtensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionConfigureDownloadPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.flashApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNotificationWhenDoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitFeedbacknotYetAvailableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangeUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLoggedInUser = new System.Windows.Forms.Label();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.presetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restorePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.presetsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(637, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleExitClicked);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionCleanupUnusedExtensionsToolStripMenuItem,
            this.toolStripSeparator1,
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem,
            this.optionConfigureDownloadPathToolStripMenuItem,
            this.toolStripSeparator2,
            this.flashApplicationToolStripMenuItem,
            this.showNotificationWhenDoneToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // optionCleanupUnusedExtensionsToolStripMenuItem
            // 
            this.optionCleanupUnusedExtensionsToolStripMenuItem.Checked = true;
            this.optionCleanupUnusedExtensionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.optionCleanupUnusedExtensionsToolStripMenuItem.Name = "optionCleanupUnusedExtensionsToolStripMenuItem";
            this.optionCleanupUnusedExtensionsToolStripMenuItem.Size = new System.Drawing.Size(369, 22);
            this.optionCleanupUnusedExtensionsToolStripMenuItem.Text = "Cleanup unused diagnostics extensions when deploying";
            this.optionCleanupUnusedExtensionsToolStripMenuItem.Click += new System.EventHandler(this.HandleOptionCleanupUnusedDiagnosticsExtensions);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(366, 6);
            // 
            // optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem
            // 
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Name = "optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem";
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Size = new System.Drawing.Size(369, 22);
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Text = "Auto-download existing package before deploying";
            this.optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem.Click += new System.EventHandler(this.HandleAutoDownloadExistingPackageOptionClicked);
            // 
            // optionConfigureDownloadPathToolStripMenuItem
            // 
            this.optionConfigureDownloadPathToolStripMenuItem.Name = "optionConfigureDownloadPathToolStripMenuItem";
            this.optionConfigureDownloadPathToolStripMenuItem.Size = new System.Drawing.Size(369, 22);
            this.optionConfigureDownloadPathToolStripMenuItem.Text = "Configure download path...";
            this.optionConfigureDownloadPathToolStripMenuItem.Click += new System.EventHandler(this.HandleConfigureDownloadPathOptionClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(366, 6);
            // 
            // flashApplicationToolStripMenuItem
            // 
            this.flashApplicationToolStripMenuItem.Checked = true;
            this.flashApplicationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.flashApplicationToolStripMenuItem.Name = "flashApplicationToolStripMenuItem";
            this.flashApplicationToolStripMenuItem.Size = new System.Drawing.Size(369, 22);
            this.flashApplicationToolStripMenuItem.Text = "Flash application when done";
            this.flashApplicationToolStripMenuItem.Click += new System.EventHandler(this.HandleFlashApplicationWhenDoneClicked);
            // 
            // showNotificationWhenDoneToolStripMenuItem
            // 
            this.showNotificationWhenDoneToolStripMenuItem.Checked = true;
            this.showNotificationWhenDoneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNotificationWhenDoneToolStripMenuItem.Name = "showNotificationWhenDoneToolStripMenuItem";
            this.showNotificationWhenDoneToolStripMenuItem.Size = new System.Drawing.Size(369, 22);
            this.showNotificationWhenDoneToolStripMenuItem.Text = "Show notification when done";
            this.showNotificationWhenDoneToolStripMenuItem.Click += new System.EventHandler(this.HandleShowNotificationWhenDoneClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.submitFeedbacknotYetAvailableToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.changelogToolStripMenuItem.Text = "Changelog...";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.HandleChangelogClicked);
            // 
            // submitFeedbacknotYetAvailableToolStripMenuItem
            // 
            this.submitFeedbacknotYetAvailableToolStripMenuItem.Name = "submitFeedbacknotYetAvailableToolStripMenuItem";
            this.submitFeedbacknotYetAvailableToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.submitFeedbacknotYetAvailableToolStripMenuItem.Text = "Submit feedback...";
            this.submitFeedbacknotYetAvailableToolStripMenuItem.Click += new System.EventHandler(this.HandleSubmitFeedbackClicked);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HandleAboutClicked);
            // 
            // btnChangeUser
            // 
            this.btnChangeUser.Location = new System.Drawing.Point(134, 3);
            this.btnChangeUser.Name = "btnChangeUser";
            this.btnChangeUser.Size = new System.Drawing.Size(107, 23);
            this.btnChangeUser.TabIndex = 1;
            this.btnChangeUser.Text = "Authenticate...";
            this.btnChangeUser.UseVisualStyleBackColor = true;
            this.btnChangeUser.Click += new System.EventHandler(this.HandleAuthenticateClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logged in as:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 55);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lblLoggedInUser);
            this.flowLayoutPanel1.Controls.Add(this.btnChangeUser);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(607, 36);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // lblLoggedInUser
            // 
            this.lblLoggedInUser.AutoSize = true;
            this.lblLoggedInUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoggedInUser.Location = new System.Drawing.Point(80, 8);
            this.lblLoggedInUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.lblLoggedInUser.Name = "lblLoggedInUser";
            this.lblLoggedInUser.Size = new System.Drawing.Size(48, 13);
            this.lblLoggedInUser.TabIndex = 3;
            this.lblLoggedInUser.Text = "nobody";
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(3, 16);
            this.lbLog.Name = "lbLog";
            this.lbLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbLog.Size = new System.Drawing.Size(607, 131);
            this.lbLog.TabIndex = 5;
            this.lbLog.Tag = "KEEP_ENABLED";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 523);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(613, 150);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "KEEP_ENABLED";
            this.groupBox3.Text = "Status";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 5500;
            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ReshowDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.UseAnimation = false;
            this.toolTip1.UseFading = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(619, 85);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(17, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "x";
            this.toolTip1.SetToolTip(this.btnClose, "Close Selected Tab");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.HandleClosePage);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "AzureCloudserviceDeployer";
            this.notifyIcon1.Visible = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(15, 88);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(607, 429);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "KEEP_ENABLED";
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.HandleDrawTab);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.HandleTabSelecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(599, 403);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "KEEP_ENABLED";
            this.tabPage1.Text = "#1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage2.Size = new System.Drawing.Size(599, 403);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "+";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // presetsToolStripMenuItem
            // 
            this.presetsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePresetToolStripMenuItem,
            this.restorePresetToolStripMenuItem,
            this.toolStripSeparator3});
            this.presetsToolStripMenuItem.Name = "presetsToolStripMenuItem";
            this.presetsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.presetsToolStripMenuItem.Text = "Presets";
            // 
            // savePresetToolStripMenuItem
            // 
            this.savePresetToolStripMenuItem.Name = "savePresetToolStripMenuItem";
            this.savePresetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.savePresetToolStripMenuItem.Text = "Save preset...";
            this.savePresetToolStripMenuItem.Click += new System.EventHandler(this.HandleSavePresetClicked);
            // 
            // restorePresetToolStripMenuItem
            // 
            this.restorePresetToolStripMenuItem.Name = "restorePresetToolStripMenuItem";
            this.restorePresetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.restorePresetToolStripMenuItem.Text = "Restore preset...";
            this.restorePresetToolStripMenuItem.Click += new System.EventHandler(this.HandleRestorePresetClicked);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 685);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Azure Cloudservice Deployer";
            this.Shown += new System.EventHandler(this.HandleMainformShown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnChangeUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLoggedInUser;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionCleanupUnusedExtensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitFeedbacknotYetAvailableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem optionAutodownloadExistingPackageBeforeDeployingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionConfigureDownloadPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem flashApplicationToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem showNotificationWhenDoneToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem presetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restorePresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

