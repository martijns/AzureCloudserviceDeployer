using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AzureCloudserviceDeployer.Helpers
{
    public class ICTVersionHelper
    {
        private static Dictionary<string, ICTVersionData> _cache = new Dictionary<string, ICTVersionData>();

        private static string GetKey(string filepath)
        {
            var sanatizedpath = Regex.Replace(filepath, "[^a-zA-Z0-9_-]", "");
            var lastwritetime = File.GetLastWriteTime(filepath).Ticks;
            var filelength = new FileInfo(filepath).Length;
            var key = sanatizedpath + "_" + lastwritetime + "_" + filelength;
            return key;
        }

        public static ICTVersionData FindAndParseVersion(string filepath)
        {
            var key = GetKey(filepath);
            lock (_cache)
            {
                if (_cache.ContainsKey(key))
                    return _cache[key];
            }

            ICTVersionData vdata = null;
            using (var archive = ZipFile.OpenRead(filepath))
            {
                foreach (var entry in archive.Entries)
                {
                    // In the .cspkg there's a .cssx file, also a zip, which contains the actual deployment
                    if (entry.Name.EndsWith(".cssx"))
                    {
                        var temp = Path.GetTempFileName();
                        entry.ExtractToFile(temp, true);
                        vdata = FindAndParseVersion(temp);
                        if (File.Exists(temp))
                            File.Delete(temp);
                        if (vdata != null)
                            break;
                    }

                    // When we find the versie.htm, parse and return results
                    if (entry.Name.Equals("versie.htm") || entry.Name.Equals("version.htm"))
                    {
                        using (var reader = new StreamReader(entry.Open()))
                        {
                            var contents = reader.ReadToEnd();
                            var matches = Regex.Matches(contents, @"<td>(.*?)</td>", RegexOptions.Singleline);
                            if (matches.Count > 5)
                            {
                                vdata = new ICTVersionData
                                {
                                    BuildNumber = matches[1].Groups[1].Value.Trim('\r', '\n', ':', ' ', '\t'),
                                    Date = matches[3].Groups[1].Value.Trim('\r', '\n', ':', ' ', '\t'),
                                    Environment = matches[5].Groups[1].Value.Trim('\r', '\n', ':', ' ', '\t')
                                };
                                break;
                            }
                        }
                    }
                }
            }

            lock (_cache)
            {
                if (!_cache.ContainsKey(key))
                    _cache.Add(key, vdata);
            }

            return vdata;
        }
    }

    public class ICTVersionData
    {
        public string BuildNumber { get; set; }
        public string BuildNumberSimplified { get { return BuildNumber != null && BuildNumber.Contains("/") ? BuildNumber.Substring(BuildNumber.LastIndexOf('/') + 1) : BuildNumber; } }
        public string Date { get; set; }
        public string Environment { get; set; }
    }
}
