using Context;
using Newtonsoft.Json;
using NLog;
using Slug.Context.Dto.OAuth;
using Slug.Context.Dto.OAuth.Fb;
using Slug.Context.Dto.Search;
using Slug.Helpers.BaseController;
using Slug.Model;
using Slug.Model.Registration;
using Slug.Model.VkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using SharedModels.Enums;


namespace Slug.Helpers.Handlers.OAuthHandlers
{
    public class FbOAuthHandler
    {
        private string AppId { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.fbAppId.ToString()];
        private string RedirectUri { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.oauthFbRedirectUri.ToString()];
        private string AppSecret { get; set; } = "f416805e86e40affdd69864095e09432";
        private string FbFields { get; set; } = "id,first_name,last_name,birthday,gender,picture";

        public async Task<FbToken> GetAccessToken(string code)
        {
            string tokenRequestUri = 
                string.Format(
                    "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                    this.AppId, this.RedirectUri, this.AppSecret, code
                );
            var client = new OauthHandler();
            string result = await client.SendRequest(tokenRequestUri);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var user = JsonConvert.DeserializeObject<FbToken>(result);
                return user;
            }
            return null;
        }

        public async Task<FBUserInfo> GetUserInfo(string token)
        {
            string userInfoRequest = string.Format(
                "https://graph.facebook.com/v4.0/me?fields={0}&access_token={1}", this.FbFields, token);
            var client = new OauthHandler();
            string result = await client.SendRequest(userInfoRequest);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var user = JsonConvert.DeserializeObject<FBUserInfo>(result);
                return user;
            }
            return null;
        }

        public OutRegisteringUserModel Convert(FBUserInfo fbUser)
        {
            var localUser = new OutRegisteringUserModel();
            localUser.OutId = fbUser.id;
            localUser.Name = fbUser.first_name;
            localUser.SurName = fbUser.last_name;
            localUser.Sex = fbUser.gender == "male" ? (int)SexEnum.man : (int)SexEnum.woman;
            localUser.DateBirth = DateTime.ParseExact(fbUser.birthday, "MM/dd/yyyy", null);
            localUser.Avatar50 = fbUser.picture.data.url;
            localUser.Avatar100 = string.Format("https://graph.facebook.com/{0}/picture?height=100&width=100", fbUser.id);
            localUser.Avatar200 = string.Format("https://graph.facebook.com/{0}/picture?height=200&width=200", fbUser.id);
            localUser.CountryCode = 7;
            localUser.CityCode = 495;
            return localUser;
        }
    }
}