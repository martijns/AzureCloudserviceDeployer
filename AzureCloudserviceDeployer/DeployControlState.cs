using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCloudserviceDeployer
{
    public class DeployControlState
    {
        public string SubscriptionName { get; set; }
        public string CloudServiceName { get; set; }
        public string PackageStorageAccountName { get; set; }
        public string DeploymentSlot { get; set; }
        public string DeploymentType { get; set; }
        public bool ForceUpgrade { get; set; }
        public string CloudPackage { get; set; }
        public string CloudConfig { get; set; }
        public string DiagConfig { get; set; }
        public string DiagStorageAccountName { get; set; }
        public string Label { get; set; }
    }
}