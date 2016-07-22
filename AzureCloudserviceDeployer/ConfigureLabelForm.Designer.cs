namespace AzureCloudserviceDeployer
{
    partial class ConfigureLabelForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnICT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDefault = new System.Windows.Forms.Label();
            this.lblICT = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(696, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configure the default label to be used in a new tab. Note that labels loaded from" +
    " presets ignore these defaults.";
            // 
            // tbLabel
            // 
            this.tbLabel.Location = new System.Drawing.Point(15, 50);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(696, 22);
            this.tbLabel.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(465, 221);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 41);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(591, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 41);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(15, 109);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 4;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnICT
            // 
            this.btnICT.Location = new System.Drawing.Point(15, 138);
            this.btnICT.Name = "btnICT";
            this.btnICT.Size = new System.Drawing.Size(75, 23);
            this.btnICT.TabIndex = 5;
            this.btnICT.Text = "ICT";
            this.btnICT.UseVisualStyleBackColor = true;
            this.btnICT.Click += new System.EventHandler(this.btnICT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Suggested label formats:";
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(96, 112);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(187, 16);
            this.lblDefault.TabIndex = 7;
            this.lblDefault.Text = "[UTCDT] [MACHINE] [USER] ";
            // 
            // lblICT
            // 
            this.lblICT.AutoSize = true;
            this.lblICT.Location = new System.Drawing.Point(96, 141);
            this.lblICT.Name = "lblICT";
            this.lblICT.Size = new System.Drawing.Size(456, 16);
            this.lblICT.TabIndex = 8;
            this.lblICT.Text = "([ICTBUILDDATE]) ([ICTBUILDNUMBER]) ([ICTENVIRONMENT]) ([USER])";
            this.toolTip1.SetToolTip(this.lblICT, "Only works for packages built by ICT containing a \"versie.htm\" or \"version.htm\" i" +
        "n the correct format.");
            // 
            // ConfigureLabelForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(723, 274);
            this.Controls.Add(this.lblICT);
            this.Controls.Add(this.lblDefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnICT);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigureLabelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure default label";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLabel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnICT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.Label lblICT;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}