namespace AzureCloudserviceDeployer
{
    partial class DeployControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbForceUpgrade = new System.Windows.Forms.CheckBox();
            this.btnDownloadExistingPackage = new System.Windows.Forms.Button();
            this.lblLabelPreview = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnClearDiagConfig = new System.Windows.Forms.Button();
            this.btnClearCloudConfig = new System.Windows.Forms.Button();
            this.btnClearCloudPackage = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.cbDiagStorage = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbForceUpgrade
            // 
            this.cbForceUpgrade.AutoSize = true;
            this.cbForceUpgrade.Checked = true;
            this.cbForceUpgrade.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbForceUpgrade.Location = new System.Drawing.Point(144, 172);
            this.cbForceUpgrade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbForceUpgrade.Name = "cbForceUpgrade";
            this.cbForceUpgrade.Size = new System.Drawing.Size(343, 20);
            this.cbForceUpgrade.TabIndex = 68;
            this.cbForceUpgrade.Text = "Force upgrade if role size or number of roles changes";
            this.cbForceUpgrade.UseVisualStyleBackColor = true;
            // 
            // btnDownloadExistingPackage
            // 
            this.btnDownloadExistingPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadExistingPackage.Location = new System.Drawing.Point(529, 106);
            this.btnDownloadExistingPackage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDownloadExistingPackage.Name = "btnDownloadExistingPackage";
            this.btnDownloadExistingPackage.Size = new System.Drawing.Size(143, 59);
            this.btnDownloadExistingPackage.TabIndex = 67;
            this.btnDownloadExistingPackage.Text = "Download existing package...";
            this.btnDownloadExistingPackage.UseVisualStyleBackColor = true;
            this.btnDownloadExistingPackage.Click += new System.EventHandler(this.HandleDownloadPackageClicked);
            // 
            // lblLabelPreview
            // 
            this.lblLabelPreview.AutoSize = true;
            this.lblLabelPreview.Location = new System.Drawing.Point(212, 372);
            this.lblLabelPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabelPreview.Name = "lblLabelPreview";
            this.lblLabelPreview.Size = new System.Drawing.Size(69, 16);
            this.lblLabelPreview.TabIndex = 65;
            this.lblLabelPreview.Text = "<preview>";
            this.toolTip1.SetToolTip(this.lblLabelPreview, "Click here to copy the previewed label to clipboard");
            this.lblLabelPreview.Click += new System.EventHandler(this.HandleLabelPreviewClicked);
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(140, 372);
            this.lblPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(59, 16);
            this.lblPreview.TabIndex = 64;
            this.lblPreview.Text = "Preview:";
            this.toolTip1.SetToolTip(this.lblPreview, "Click here to copy the previewed label to clipboard");
            this.lblPreview.Click += new System.EventHandler(this.HandleLabelPreviewClicked);
            // 
            // btnClearDiagConfig
            // 
            this.btnClearDiagConfig.Location = new System.Drawing.Point(269, 272);
            this.btnClearDiagConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearDiagConfig.Name = "btnClearDiagConfig";
            this.btnClearDiagConfig.Size = new System.Drawing.Size(36, 28);
            this.btnClearDiagConfig.TabIndex = 63;
            this.btnClearDiagConfig.Text = "X";
            this.btnClearDiagConfig.UseVisualStyleBackColor = true;
            this.btnClearDiagConfig.Click += new System.EventHandler(this.HandleClearDiagConfig);
            // 
            // btnClearCloudConfig
            // 
            this.btnClearCloudConfig.Location = new System.Drawing.Point(269, 236);
            this.btnClearCloudConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearCloudConfig.Name = "btnClearCloudConfig";
            this.btnClearCloudConfig.Size = new System.Drawing.Size(36, 28);
            this.btnClearCloudConfig.TabIndex = 62;
            this.btnClearCloudConfig.Text = "X";
            this.btnClearCloudConfig.UseVisualStyleBackColor = true;
            this.btnClearCloudConfig.Click += new System.EventHandler(this.HandleClearCloudConfig);
            // 
            // btnClearCloudPackage
            // 
            this.btnClearCloudPackage.Location = new System.Drawing.Point(269, 201);
            this.btnClearCloudPackage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClearCloudPackage.Name = "btnClearCloudPackage";
            this.btnClearCloudPackage.Size = new System.Drawing.Size(36, 28);
            this.btnClearCloudPackage.TabIndex = 61;
            this.btnClearCloudPackage.Text = "X";
            this.btnClearCloudPackage.UseVisualStyleBackColor = true;
            this.btnClearCloudPackage.Click += new System.EventHandler(this.HandleClearCloudPackage);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 346);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 16);
            this.label11.TabIndex = 60;
            this.label11.Text = "Label:";
            // 
            // tbLabel
            // 
            this.tbLabel.Location = new System.Drawing.Point(144, 342);
            this.tbLabel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(376, 22);
            this.tbLabel.TabIndex = 59;
            this.tbLabel.Text = "<configured default>";
            this.tbLabel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.HandleLabelKeyUp);
            // 
            // cbDiagStorage
            // 
            this.cbDiagStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiagStorage.FormattingEnabled = true;
            this.cbDiagStorage.Location = new System.Drawing.Point(144, 308);
            this.cbDiagStorage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbDiagStorage.Name = "cbDiagStorage";
            this.cbDiagStorage.Size = new System.Drawing.Size(376, 24);
            this.cbDiagStorage.TabIndex = 58;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 311);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 16);
            this.label10.TabIndex = 57;
            this.label10.Text = "Diag. storage:";
            // 
            // btnDeploy
            // 
            this.btnDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeploy.Location = new System.Drawing.Point(144, 396);
            this.btnDeploy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(189, 43);
            this.btnDeploy.TabIndex = 55;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.HandleDeployClicked);
            // 
            // cbUpgradePreference
            // 
            this.cbUpgradePreference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpgradePreference.FormattingEnabled = true;
            this.cbUpgradePreference.Location = new System.Drawing.Point(144, 139);
            this.cbUpgradePreference.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbUpgradePreference.Name = "cbUpgradePreference";
            this.cbUpgradePreference.Size = new System.Drawing.Size(376, 24);
            this.cbUpgradePreference.TabIndex = 54;
            this.cbUpgradePreference.SelectedIndexChanged += new System.EventHandler(this.HandleDeploymentTypeChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 16);
            this.label9.TabIndex = 53;
            this.label9.Text = "Deployment type:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 110);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 16);
            this.label8.TabIndex = 52;
            this.label8.Text = "Slot:";
            // 
            // cbSlot
            // 
            this.cbSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlot.FormattingEnabled = true;
            this.cbSlot.Location = new System.Drawing.Point(144, 106);
            this.cbSlot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSlot.Name = "cbSlot";
            this.cbSlot.Size = new System.Drawing.Size(376, 24);
            this.cbSlot.TabIndex = 51;
            this.cbSlot.SelectedIndexChanged += new System.EventHandler(this.HandleSlotChanged);
            // 
            // lblSelectedDiag
            // 
            this.lblSelectedDiag.AutoEllipsis = true;
            this.lblSelectedDiag.Location = new System.Drawing.Point(313, 278);
            this.lblSelectedDiag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedDiag.Name = "lblSelectedDiag";
            this.lblSelectedDiag.Size = new System.Drawing.Size(489, 16);
            this.lblSelectedDiag.TabIndex = 50;
            this.lblSelectedDiag.Text = "<selected diag>";
            // 
            // lblSelectedConfig
            // 
            this.lblSelectedConfig.AutoEllipsis = true;
            this.lblSelectedConfig.Location = new System.Drawing.Point(313, 242);
            this.lblSelectedConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedConfig.Name = "lblSelectedConfig";
            this.lblSelectedConfig.Size = new System.Drawing.Size(489, 16);
            this.lblSelectedConfig.TabIndex = 49;
            this.lblSelectedConfig.Text = "<selected config>";
            // 
            // lblSelectedPackage
            // 
            this.lblSelectedPackage.AutoEllipsis = true;
            this.lblSelectedPackage.Location = new System.Drawing.Point(313, 207);
            this.lblSelectedPackage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedPackage.Name = "lblSelectedPackage";
            this.lblSelectedPackage.Size = new System.Drawing.Size(489, 16);
            this.lblSelectedPackage.TabIndex = 48;
            this.lblSelectedPackage.Text = "<selected package>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 278);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 47;
            this.label7.Text = "Diagnostics config:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 242);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 16);
            this.label6.TabIndex = 46;
            this.label6.Text = "Cloud config:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 207);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 45;
            this.label5.Text = "Cloud package:";
            // 
            // btnBrowseDiagnostics
            // 
            this.btnBrowseDiagnostics.Location = new System.Drawing.Point(144, 272);
            this.btnBrowseDiagnostics.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseDiagnostics.Name = "btnBrowseDiagnostics";
            this.btnBrowseDiagnostics.Size = new System.Drawing.Size(117, 28);
            this.btnBrowseDiagnostics.TabIndex = 44;
            this.btnBrowseDiagnostics.Text = "Browse...";
            this.btnBrowseDiagnostics.UseVisualStyleBackColor = true;
            this.btnBrowseDiagnostics.Click += new System.EventHandler(this.HandleSelectCloudDiag);
            // 
            // btnBrowseCloudConfig
            // 
            this.btnBrowseCloudConfig.Location = new System.Drawing.Point(144, 236);
            this.btnBrowseCloudConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseCloudConfig.Name = "btnBrowseCloudConfig";
            this.btnBrowseCloudConfig.Size = new System.Drawing.Size(117, 28);
            this.btnBrowseCloudConfig.TabIndex = 43;
            this.btnBrowseCloudConfig.Text = "Browse...";
            this.btnBrowseCloudConfig.UseVisualStyleBackColor = true;
            this.btnBrowseCloudConfig.Click += new System.EventHandler(this.HandleSelectCloudConfig);
            // 
            // btnBrowseCloudPackage
            // 
            this.btnBrowseCloudPackage.Location = new System.Drawing.Point(144, 201);
            this.btnBrowseCloudPackage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBrowseCloudPackage.Name = "btnBrowseCloudPackage";
            this.btnBrowseCloudPackage.Size = new System.Drawing.Size(117, 28);
            this.btnBrowseCloudPackage.TabIndex = 42;
            this.btnBrowseCloudPackage.Text = "Browse...";
            this.btnBrowseCloudPackage.UseVisualStyleBackColor = true;
            this.btnBrowseCloudPackage.Click += new System.EventHandler(this.HandleSelectCloudPackage);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 76);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 16);
            this.label4.TabIndex = 41;
            this.label4.Text = "Package storage:";
            // 
            // cbPackageStorage
            // 
            this.cbPackageStorage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPackageStorage.FormattingEnabled = true;
            this.cbPackageStorage.Location = new System.Drawing.Point(144, 73);
            this.cbPackageStorage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbPackageStorage.Name = "cbPackageStorage";
            this.cbPackageStorage.Size = new System.Drawing.Size(376, 24);
            this.cbPackageStorage.TabIndex = 40;
            this.cbPackageStorage.SelectedIndexChanged += new System.EventHandler(this.HandlePackagestorageChanged);
            // 
            // cbCloudservices
            // 
            this.cbCloudservices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCloudservices.FormattingEnabled = true;
            this.cbCloudservices.Location = new System.Drawing.Point(144, 39);
            this.cbCloudservices.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCloudservices.Name = "cbCloudservices";
            this.cbCloudservices.Size = new System.Drawing.Size(376, 24);
            this.cbCloudservices.TabIndex = 39;
            this.cbCloudservices.SelectedIndexChanged += new System.EventHandler(this.HandleCloudserviceChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Cloudservice:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "Subscription:";
            // 
            // cbSubscriptions
            // 
            this.cbSubscriptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSubscriptions.FormattingEnabled = true;
            this.cbSubscriptions.Location = new System.Drawing.Point(144, 4);
            this.cbSubscriptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSubscriptions.Name = "cbSubscriptions";
            this.cbSubscriptions.Size = new System.Drawing.Size(376, 24);
            this.cbSubscriptions.TabIndex = 36;
            this.cbSubscriptions.SelectedIndexChanged += new System.EventHandler(this.HandleSubscriptionIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::AzureCloudserviceDeployer.Properties.Resources.Infoicon;
            this.pictureBox2.Location = new System.Drawing.Point(529, 346);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 66;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "KEEP_ENABLED";
            this.toolTip1.SetToolTip(this.pictureBox2, "Label variables are updated when Deploy is clicked. Available tags: UTCDT, MACHIN" +
        "E, USER, ICTBUILDNUMBER, ICTBUILDDATE, ICTENVIRONMENT");
            this.pictureBox2.Click += new System.EventHandler(this.HandleTooltipClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::AzureCloudserviceDeployer.Properties.Resources.Infoicon;
            this.pictureBox1.Location = new System.Drawing.Point(529, 76);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "KEEP_ENABLED";
            this.toolTip1.SetToolTip(this.pictureBox1, "This storage account is only used for temporary storage to upload the package. No" +
        "t for diagnostics. So it doesn\'t really matter what this is set to.");
            this.pictureBox1.Click += new System.EventHandler(this.HandleTooltipClick);
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
            // DeployControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbForceUpgrade);
            this.Controls.Add(this.btnDownloadExistingPackage);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblLabelPreview);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.btnClearDiagConfig);
            this.Controls.Add(this.btnClearCloudConfig);
            this.Controls.Add(this.btnClearCloudPackage);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbLabel);
            this.Controls.Add(this.cbDiagStorage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.cbUpgradePreference);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbSlot);
            this.Controls.Add(this.lblSelectedDiag);
            this.Controls.Add(this.lblSelectedConfig);
            this.Controls.Add(this.lblSelectedPackage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnBrowseDiagnostics);
            this.Controls.Add(this.btnBrowseCloudConfig);
            this.Controls.Add(this.btnBrowseCloudPackage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPackageStorage);
            this.Controls.Add(this.cbCloudservices);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSubscriptions);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DeployControl";
            this.Size = new System.Drawing.Size(685, 450);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HandleDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HandleDragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbForceUpgrade;
        private System.Windows.Forms.Button btnDownloadExistingPackage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblLabelPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnClearDiagConfig;
        private System.Windows.Forms.Button btnClearCloudConfig;
        private System.Windows.Forms.Button btnClearCloudPackage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.ComboBox cbDiagStorage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.ComboBox cbUpgradePreference;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSlot;
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
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
