# AzureCloudserviceDeployer

Deploying Azure Cloudservices including a diagnostics extension, without using Visual Studio or Powershell.

Used to streamline deployments within ICT (www.ict.eu).

## Installation

Binaries are available via a ClickOnce deployment on the following URL:
http://martijn.tikkie.net/apps/AzureCloudserviceDeployer/AzureCloudserviceDeployer.application

## Screenshots

![screenshot](http://i.snag.gy/BNfzG.jpg)

## Main features

* Deploy cloudservices using their .cspkg and .cscfg files
* Enable PaaS diagnostics based upon PubConfig.xml files (public part of the diagnostics.wadcfgx), and extract diagnostics storage account from .cscfg (or select manually)
* Automatically generate a semi-useful label
* Deploy using different strategies:
  * Upgrade respecting domains (or create if no deployment exists)
  * Upgrade all domains at once (or create if no deployment exists)
  * Delete/create deployment
  * Delete/create deployment, but keep in stopped state
* Download existing package for a cloudservice, including PaaSDiagnostics PubConfig if enabled for that cloudservice
* Automatically remove unused extensions still registered for that cloudservice

## Changelog

See changes.txt in the source code, or Help => Changelog within the application.

## Author

Martijn Stolk (www.netripper.nl)

## License

Creative Commons Attribution 3.0 Unported (CC BY 3.0)
http://creativecommons.org/licenses/by/3.0/

