﻿using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCloudserviceDeployer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Action<string[]> mainMethod = (args) =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            };

            AppProgram.Start(
                "AzureCloudserviceDeployer",
                "Martijn Stolk",
                "http://martijn.tikkie.net/reportbug.php",
                mainMethod,
                arguments);
        }
    }
}
