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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void HandleChangelogClicked(object sender, EventArgs e)
        {
            AppVersion.DisplayChanges();
        }

        private void HandleAboutClicked(object sender, EventArgs e)
        {
            AppVersion.DisplayAbout();
        }
    }
}
