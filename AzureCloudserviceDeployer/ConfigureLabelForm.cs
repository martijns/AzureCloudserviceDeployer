using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCloudserviceDeployer
{
    public partial class ConfigureLabelForm : AppForm
    {
        public ConfigureLabelForm()
        {
            InitializeComponent();
            tbLabel.Text = Configuration.Instance.DefaultDeploymentLabel;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            tbLabel.Text = lblDefault.Text;
        }

        private void btnICT_Click(object sender, EventArgs e)
        {
            tbLabel.Text = lblICT.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Configuration.Instance.DefaultDeploymentLabel = tbLabel.Text;
            Configuration.Instance.Save();
            Close();
        }
    }
}
