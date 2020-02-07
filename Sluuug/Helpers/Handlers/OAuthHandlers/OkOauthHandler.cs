using Newtonsoft.Json;
using NLog;
using Slug.Context.Dto.OAuth.Ok;
using Slug.Crypto;
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
        private string OkAppPublickKey { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.okAppPublickKey.ToString()];

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

            //Logger loggerInternal = LogManager.GetLogger("info_log");
            //loggerInternal.Error(string.Format("{0}", access_post));
            //loggerInternal.Error(string.Format("{0}", result));

            var accesstoken = JsonConvert.DeserializeObject<OkAccessToken>(result);
            return accesstoken;
        }

        public async Task<OkUserInfo> UserInfo(OkAccessToken token)
        {
            var sigModel = new OkSignatureModel()
            {
                AccessToken = token.access_token,
                Format = "json",
                Method = "users.getCurrentUser",
                AppPublicKey = WebAppSettings.AppSettings[AppSettingsEnum.okAppPublickKey.ToString()],
                ApplicationSecretKey = "356D63BAAB1C8DCCF9FBB79F"
            };
            OkApiMD5Params okSecretParams = Encryption.OkSecretParams(sigModel);

            string request = string.Format(
                "https://api.ok.ru/fb.do?application_key={0}&format={1}&method={2}&sig={3}&access_token={4}",
                OkAppPublickKey, sigModel.Format, sigModel.Method, okSecretParams.Signature, token.access_token);

            var oauthHandler = new OauthHandler();
            string result = await oauthHandler.SendRequest(request, false);

            Logger loggerInternal = LogManager.GetLogger("info_log");
            loggerInternal.Info(string.Format("{0}", result));

            OkUserInfo okUser = JsonConvert.DeserializeObject<OkUserInfo>(result);
            return okUser;
        }

    }
}