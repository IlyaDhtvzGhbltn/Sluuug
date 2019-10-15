using Newtonsoft.Json;
using Slug.Helpers.BaseController;
using Slug.Model.VkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;

namespace Slug.Helpers.Handlers.OAuthHandlers
{
    public class VkOAuthHandler
    {
        private string appSecret { get; set; } = "OFuxinKsGFinAbvA73Xu";
        private string appId { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.vkAppId.ToString()];
        private string redirectUri { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.oauthVkRedirectUri.ToString()];

        public async Task<AccessTokenModel> GetVkAccessToken(string code)
        {
            string vkOAuthTokenString = string.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code={3}", appId, appSecret, redirectUri, code);
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(vkOAuthTokenString).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<AccessTokenModel>(result);
                        return user;
                    }
                }
            }
            return null;
        }

    }
}