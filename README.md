# AzureCloudserviceDeployer

Deploying Azure Cloudservices including a diagnostics extension, without using Visual Studio or Powershell.

Used to streamline deployments within ICT (www.ict.eu).

## Installation

Binaries are available via a ClickOnce deployment on the following URL:
http://martijn.tikkie.net/apps/AzureCloudserviceDeployer/AzureCloudserviceDeployer.application

## Screenshots

![screenshot](http://www.tikkie.net/p/2016-05-20_101154.png)

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
* Save and load presets in JSON format with a MRU list
* Multiple tabs/deployments simultaneously
* Extract label from cspkg on supported format

## Extract version from .cspkg

As employee of [ICT](http://www.ict.eu) there is some special support for the way we include versioning information in our builds and thus end up in the cspkg files. When the labels [ICTBUILDDATE], [ICTBUILDNUMBER] or [ICTENVIRONMENT] are being used, the cspkg is scanned for a "versie.htm" or "version.htm" file with a certain format. If found, the tags are replaced with the proper values from that file. An example of this file:

```html
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
  <head>
    <title></title>
  </head>
  <style>body, table, tr, td
  {font-family:Arial;font-size:12px;}</style>
  <body>
    <table>
      <tr>
        <td>build number</td>
        <td>: $/Customer/Product/Dev/Branchname@12345</td>
      </tr>
      <tr>
        <td>datum</td>
        <td>: 2016-06-13 03:21:22.954</td>
      </tr>
      <tr>
        <td>omgeving</td>
        <td>: WA-P</td>
      </tr>
    </table>
  </body>
</html>
```

## Changelog

See changes.txt in the source code, or Help => Changelog within the application.

## Author

Martijn Stolk (www.netripper.nl)

## License

Creative Commons Attribution 3.0 Unported (CC BY 3.0)
http://creativecommons.org/licenses/by/3.0/

