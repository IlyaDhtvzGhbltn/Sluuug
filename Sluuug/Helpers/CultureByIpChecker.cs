using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace Slug.Helpers
{
    public class CultureByIpChecker
    {
        private readonly string codeUrl;
        private readonly Regex regex = new Regex("[A-Z]{2,}");
        private readonly Dictionary<string, string> CultureDictionary
            = new Dictionary<string, string>()
            {
                        { "RU", "ru-RU" },
                        { "EN", "en-US"},
                        { "US", "en-US" }
            };

        public CultureByIpChecker(string ip)
        {
            this.codeUrl = string.Format("https://ipinfo.io/{0}/country", ip);
        }

        public CultureInfo GetCulture()
        {
            try
            {
                WebClient client = new WebClient();
                string ipResp = client.DownloadString(codeUrl);
                MatchCollection country = regex.Matches(ipResp);
                if (country.Count == 1)
                {
                    string code = country[0].Value;
                    var ruCulture = new System.Globalization.CultureInfo(CultureDictionary[code]);
                    return ruCulture;
                }
                else
                {
                    var ruCulture = new System.Globalization.CultureInfo("ru-RU");
                    return ruCulture;
                }
            }
            catch (Exception ex)
            {
                var ruCulture = new System.Globalization.CultureInfo("ru-RU");
                return ruCulture;
            }
        }
    }
}