using Newtonsoft.Json;
using Slug.Context.Dto.OAuth.Ok;
using Slug.Helpers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;


namespace Slug.Helpers.Handlers.OAuthHandlers
{
    public class OkOauthHandler
    {
        private string ApiSecret { get; set; } = "356D63BAAB1C8DCCF9FBB79F";
        private string ApiId { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.okAppId.ToString()];
        private string ApiPublicKey { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.okAppId.ToString()];
        private string ApiRedirectUrl { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.okAppRedirectUri.ToString()];

        public async Task<OkAccessToken> AccessToken(string code)
        {
            var oauthHandler = new OauthHandler();
            string access_post =
                string.Format(
                    "https://api.ok.ru/oauth/token.do?code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", 
                    code, 
                    ApiId, 
                    ApiSecret, 
                    ApiRedirectUrl );

            string result = await oauthHandler.SendRequest(access_post, false);
            var accesstoken = JsonConvert.DeserializeObject<OkAccessToken>(result);
            return accesstoken;
        }
    }
}