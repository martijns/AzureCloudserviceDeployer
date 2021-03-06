﻿using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AzureCloudserviceDeployer
{
    /// <summary>
    /// The Configuration class.
    /// </summary>
    /// <remarks>
    /// In some cases we want the default to be 'true' and to make that happen we use a negative setting,
    /// as a non-existing xml element will default to its default value (false). The public
    /// property is used to negate the private one, so that it is more convenient in its use.
    /// </remarks>
    [Serializable]
    public class Configuration : AppConfiguration<Configuration>
    {
        [XmlElement]
        protected bool NoCleanupUnusedExtensions { get; set; }
        public bool CleanupUnusedExtensions
        {
            get { return !NoCleanupUnusedExtensions; }
            set { NoCleanupUnusedExtensions = !value; }
        }

        [XmlElement]
        public bool AutoDownloadPackageBeforeDeploy { get; set; }

        [XmlElement]
        public string PackageDownloadPath { get; set; }

        [XmlElement]
        public bool FlashWindowWhenDone { get; set; } = true;

        [XmlElement]
        public bool NotifyWhenDone { get; set; } = true;

        [XmlElement]
        public List<string> MostRecentlyUsedPresets { get; set; } = new List<string>();

        [XmlElement]
        public string DefaultDeploymentLabel { get; set; } = "[UTCDT] [MACHINE] [USER] ";
    }
}
