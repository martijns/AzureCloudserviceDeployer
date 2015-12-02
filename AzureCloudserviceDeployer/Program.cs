using log4net;
using log4net.Config;
using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCloudserviceDeployer
{
    static class Program
    {
        private static ILog Logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Action<string[]> mainMethod = (args) =>
            {
                XmlConfigurator.Configure();
                Logger.Info("Starting...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            };

            AppProgram.Start(
                "AzureCloudserviceDeployer",
                "Martijn Stolk",
                "http://martijn.tikkie.net/reportbug.php",
                "http://martijn.tikkie.net/feedback.php",
                mainMethod,
                arguments);
        }
    }
}
