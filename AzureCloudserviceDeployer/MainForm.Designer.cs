﻿namespace AzureCloudserviceDeployer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangeUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLoggedInUser = new System.Windows.Forms.Label();
            this.gbQuickDeploy = new System.Windows.Forms.GroupBox();
            this.cbDiagStorage = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.cbUpgradePreference = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSlot = new System.Windows.Forms.ComboBox();
            this.lblSelectedDiag = new System.Windows.Forms.Label();
            this.lblSelectedConfig = new System.Windows.Forms.Label();
            this.lblSelectedPackage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseDiagnostics = new System.Windows.Forms.Button();
            this.btnBrowseCloudConfig = new System.Windows.Forms.Button();
            this.btnBrowseCloudPackage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPackageStorage = new System.Windows.Forms.ComboBox();
            this.cbCloudservices = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSubscriptions = new System.Windows.Forms.ComboBox();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnGenerateLabel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbQuickDeploy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.HandleExitClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changelogToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // changelogToolStripMenuItem
            // 
            this.changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            this.changelogToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.changelogToolStripMenuItem.Text = "Changelog...";
            this.changelogToolStripMenuItem.Click += new System.EventHandler(this.HandleChangelogClicked);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
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
            // gbQuickDeploy
            // 
            this.gbQuickDeploy.Controls.Add(this.btnGenerateLabel);
            this.gbQuickDeploy.Controls.Add(this.label11);
            this.gbQuickDeploy.Controls.Add(this.tbLabel);
            this.gbQuickDeploy.Controls.Add(this.cbDiagStorage);
            this.gbQuickDeploy.Controls.Add(this.label10);
            this.gbQuickDeploy.Controls.Add(this.pictureBox1);
            this.gbQuickDeploy.Controls.Add(this.btnDeploy);
            this.gbQuickDeploy.Controls.Add(this.cbUpgradePreference);
            this.gbQuickDeploy.Controls.Add(this.label9);
            this.gbQuickDeploy.Controls.Add(this.label8);
            this.gbQuickDeploy.Controls.Add(this.cbSlot);
            this.gbQuickDeploy.Controls.Add(this.lblSelectedDiag);
            this.gbQuickDeploy.Controls.Add(this.lblSelectedConfig);
            this.gbQuickDeploy.Controls.Add(this.lblSelectedPackage);
            this.gbQuickDeploy.Controls.Add(this.label7);
            this.gbQuickDeploy.Controls.Add(this.label6);
            this.gbQuickDeploy.Controls.Add(this.label5);
            this.gbQuickDeploy.Controls.Add(this.btnBrowseDiagnostics);
            this.gbQuickDeploy.Controls.Add(this.btnBrowseCloudConfig);
            this.gbQuickDeploy.Controls.Add(this.btnBrowseCloudPackage);
            this.gbQuickDeploy.Controls.Add(this.label4);
            this.gbQuickDeploy.Controls.Add(this.cbPackageStorage);
            this.gbQuickDeploy.Controls.Add(this.cbCloudservices);
            this.gbQuickDeploy.Controls.Add(this.label3);
            this.gbQuickDeploy.Controls.Add(this.label2);
            this.gbQuickDeploy.Controls.Add(this.cbSubscriptions);
            this.gbQuickDeploy.Location = new System.Drawing.Point(12, 89);
            this.gbQuickDeploy.Name = "gbQuickDeploy";
            this.gbQuickDeploy.Size = new System.Drawing.Size(613, 366);
            this.gbQuickDeploy.TabIndex = 4;
            this.gbQuickDeploy.TabStop = false;
            this.gbQuickDeploy.Text = "Quick Deploy";
            // 
            // cbDiagStorage
            // 
            this.cbDiagStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiagStorage.FormattingEnabled = true;
            this.cbDiagStorage.Location = new System.Drawing.Point(113, 246);
            this.cbDiagStorage.Name = "cbDiagStorage";
            this.cbDiagStorage.Size = new System.Drawing.Size(283, 21);
            this.cbDiagStorage.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Diag. storage:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AzureCloudserviceDeployer.Properties.Resources.Info_icon;
            this.pictureBox1.Location = new System.Drawing.Point(402, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "This storage account is only used for temporary storage to upload the package. No" +
        "t for diagnostics. So it doesn\'t really matter what this is set to.");
            // 
            // btnDeploy
            // 
            this.btnDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeploy.Location = new System.Drawing.Point(113, 300);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(142, 53);
            this.btnDeploy.TabIndex = 19;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.HandleDeployClicked);
            // 
            // cbUpgradePreference
            // 
            this.cbUpgradePreference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpgradePreference.FormattingEnabled = true;
            this.cbUpgradePreference.Location = new System.Drawing.Point(113, 132);
            this.cbUpgradePreference.Name = "cbUpgradePreference";
            this.cbUpgradePreference.Size = new System.Drawing.Size(283, 21);
            this.cbUpgradePreference.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Upgrade preference:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Slot:";
            // 
            // cbSlot
            // 
            this.cbSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlot.FormattingEnabled = true;
            this.cbSlot.Location = new System.Drawing.Point(113, 105);
            this.cbSlot.Name = "cbSlot";
            this.cbSlot.Size = new System.Drawing.Size(283, 21);
            this.cbSlot.TabIndex = 15;
            // 
            // lblSelectedDiag
            // 
            this.lblSelectedDiag.AutoEllipsis = true;
            this.lblSelectedDiag.Location = new System.Drawing.Point(240, 222);
            this.lblSelectedDiag.Name = "lblSelectedDiag";
            this.lblSelectedDiag.Size = new System.Drawing.Size(367, 13);
            this.lblSelectedDiag.TabIndex = 14;
            this.lblSelectedDiag.Text = "<selected diag>";
            // 
            // lblSelectedConfig
            // 
            this.lblSelectedConfig.AutoEllipsis = true;
            this.lblSelectedConfig.Location = new System.Drawing.Point(240, 193);
            this.lblSelectedConfig.Name = "lblSelectedConfig";
            this.lblSelectedConfig.Size = new System.Drawing.Size(367, 13);
            this.lblSelectedConfig.TabIndex = 13;
            this.lblSelectedConfig.Text = "<selected config>";
            // 
            // lblSelectedPackage
            // 
            this.lblSelectedPackage.AutoEllipsis = true;
            this.lblSelectedPackage.Location = new System.Drawing.Point(240, 164);
            this.lblSelectedPackage.Name = "lblSelectedPackage";
            this.lblSelectedPackage.Size = new System.Drawing.Size(367, 13);
            this.lblSelectedPackage.TabIndex = 12;
            this.lblSelectedPackage.Text = "<selected package>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Diagnostics config:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Cloud config:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cloud package:";
            // 
            // btnBrowseDiagnostics
            // 
            this.btnBrowseDiagnostics.Location = new System.Drawing.Point(113, 217);
            this.btnBrowseDiagnostics.Name = "btnBrowseDiagnostics";
            this.btnBrowseDiagnostics.Size = new System.Drawing.Size(121, 23);
            this.btnBrowseDiagnostics.TabIndex = 8;
            this.btnBrowseDiagnostics.Text = "Browse...";
            this.btnBrowseDiagnostics.UseVisualStyleBackColor = true;
            this.btnBrowseDiagnostics.Click += new System.EventHandler(this.HandleSelectCloudDiag);
            // 
            // btnBrowseCloudConfig
            // 
            this.btnBrowseCloudConfig.Location = new System.Drawing.Point(113, 188);
            this.btnBrowseCloudConfig.Name = "btnBrowseCloudConfig";
            this.btnBrowseCloudConfig.Size = new System.Drawing.Size(121, 23);
            this.btnBrowseCloudConfig.TabIndex = 7;
            this.btnBrowseCloudConfig.Text = "Browse...";
            this.btnBrowseCloudConfig.UseVisualStyleBackColor = true;
            this.btnBrowseCloudConfig.Click += new System.EventHandler(this.HandleSelectCloudConfig);
            // 
            // btnBrowseCloudPackage
            // 
            this.btnBrowseCloudPackage.Location = new System.Drawing.Point(113, 159);
            this.btnBrowseCloudPackage.Name = "btnBrowseCloudPackage";
            this.btnBrowseCloudPackage.Size = new System.Drawing.Size(121, 23);
            this.btnBrowseCloudPackage.TabIndex = 6;
            this.btnBrowseCloudPackage.Text = "Browse...";
            this.btnBrowseCloudPackage.UseVisualStyleBackColor = true;
            this.btnBrowseCloudPackage.Click += new System.EventHandler(this.HandleSelectCloudPackage);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Package storage:";
            // 
            // cbPackageStorage
            // 
            this.cbPackageStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackageStorage.FormattingEnabled = true;
            this.cbPackageStorage.Location = new System.Drawing.Point(113, 78);
            this.cbPackageStorage.Name = "cbPackageStorage";
            this.cbPackageStorage.Size = new System.Drawing.Size(283, 21);
            this.cbPackageStorage.TabIndex = 4;
            // 
            // cbCloudservices
            // 
            this.cbCloudservices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCloudservices.FormattingEnabled = true;
            this.cbCloudservices.Location = new System.Drawing.Point(113, 51);
            this.cbCloudservices.Name = "cbCloudservices";
            this.cbCloudservices.Size = new System.Drawing.Size(283, 21);
            this.cbCloudservices.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cloudservice:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Subscription:";
            // 
            // cbSubscriptions
            // 
            this.cbSubscriptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubscriptions.FormattingEnabled = true;
            this.cbSubscriptions.Location = new System.Drawing.Point(113, 22);
            this.cbSubscriptions.Name = "cbSubscriptions";
            this.cbSubscriptions.Size = new System.Drawing.Size(283, 21);
            this.cbSubscriptions.TabIndex = 0;
            this.cbSubscriptions.SelectedIndexChanged += new System.EventHandler(this.HandleSubscriptionIndexChanged);
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(3, 16);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(607, 131);
            this.lbLog.TabIndex = 5;
            this.lbLog.Tag = "KEEP_ENABLED";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 461);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(613, 150);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "KEEP_ENABLED";
            this.groupBox3.Text = "Status";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // tbLabel
            // 
            this.tbLabel.Location = new System.Drawing.Point(207, 274);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(400, 20);
            this.tbLabel.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 277);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Label:";
            // 
            // btnGenerateLabel
            // 
            this.btnGenerateLabel.Location = new System.Drawing.Point(113, 272);
            this.btnGenerateLabel.Name = "btnGenerateLabel";
            this.btnGenerateLabel.Size = new System.Drawing.Size(88, 23);
            this.btnGenerateLabel.TabIndex = 26;
            this.btnGenerateLabel.Text = "Generate";
            this.btnGenerateLabel.UseVisualStyleBackColor = true;
            this.btnGenerateLabel.Click += new System.EventHandler(this.HandleGenerateLabelClicked);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 624);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbQuickDeploy);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Azure Cloudservice Deployer";
            this.Shown += new System.EventHandler(this.HandleMainformShown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbQuickDeploy.ResumeLayout(false);
            this.gbQuickDeploy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox gbQuickDeploy;
        private System.Windows.Forms.Label lblSelectedDiag;
        private System.Windows.Forms.Label lblSelectedConfig;
        private System.Windows.Forms.Label lblSelectedPackage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowseDiagnostics;
        private System.Windows.Forms.Button btnBrowseCloudConfig;
        private System.Windows.Forms.Button btnBrowseCloudPackage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPackageStorage;
        private System.Windows.Forms.ComboBox cbCloudservices;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSubscriptions;
        private System.Windows.Forms.ComboBox cbSlot;
        private System.Windows.Forms.ComboBox cbUpgradePreference;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbDiagStorage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.Button btnGenerateLabel;
    }
}

