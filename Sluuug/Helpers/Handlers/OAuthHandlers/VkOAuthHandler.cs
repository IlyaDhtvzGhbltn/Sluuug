using Context;
using Newtonsoft.Json;
using NLog;
using Slug.Context.Dto.OAuth;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using Slug.Model;
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
        private string appId { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.vkAppId.ToString()];
        private string redirectUri { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.oauthVkRedirectUri.ToString()];
        private string appSecret { get; set; } = "OFuxinKsGFinAbvA73Xu";
        private string vkFields { get; set; } = "country,city,bdate,sex,photo_200_orig,status,photo_100,photo_50";
        private string vkApiVersion { get; set; } = "5.102";

        public async Task<AccessTokenModel> GetAccessToken(string code)
        {
            string vkOAuthTokenString = string.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code={3}", appId, appSecret, redirectUri, code);
            var client = new OauthHandler();
            string result = await client.SendRequest(vkOAuthTokenString);
            if (!string.IsNullOrWhiteSpace(result))
            {
                var user = JsonConvert.DeserializeObject<AccessTokenModel>(result);
                return user;
            }
            return null;
        }

        public async Task<OutRegisteringUserModel> GetVkUserInfo(string vkOneTimeCode)
        {
            using (var context = new DataBaseContext())
            {
                var entry = context.VkOAuthTokens
                    .Where(x => x.Code == vkOneTimeCode && x.IsExpired == false)
                    .Select(x => new { x.Token, x.VkUserId, x.Id  })
                    .FirstOrDefault();
                if (entry == null)
                    return null;
                else
                {
                    string request = string.Format(
                        "https://api.vk.com/method/users.get?user_ids={0}&fields={1}&access_token={2}&v={3}",
                        entry.VkUserId, vkFields, entry.Token, vkApiVersion);

                    var client = new OauthHandler();
                    string result = await client.SendRequest(request);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var vkInfo = JsonConvert.DeserializeObject<VkResponce>(result);
                        var oauthHand = new OauthHandler();
                        VkUserInfo vkUser = vkInfo.UserInfo[0];
                        var registerModel = new OutRegisteringUserModel();
                        registerModel.OutId = vkUser.Id;
                        registerModel.Name = vkUser.FirstName;
                        registerModel.SurName = vkUser.LastName;
                        registerModel.Avatar200 = vkUser.Photo200_Orig;
                        registerModel.Avatar100 = vkUser.Photo100;
                        registerModel.Avatar50 = vkUser.Photo50;
                        registerModel.Status = vkUser.Status;

                        registerModel.Sex = vkSexUserParse(vkUser.Sex);
                        registerModel.DateBirth = vkDateTimeParse(vkUser.Bdate);
                        if (vkUser.Country != null && vkUser.City != null)
                        {
                            registerModel.CountryCode = oauthHand.CountryCodeParse(context, vkUser.Country.Id, vkUser.Country.Title, true);
                            registerModel.CityCode = oauthHand.CityCodeParse(context, vkUser.City.Id, vkUser.City.Title, registerModel.CountryCode, true);
                        }
                        else
                        {
                            registerModel.CountryCode = oauthHand.CountryCodeParse(context, vkUser.Country.Id, vkUser.Country.Title, false);
                            registerModel.CityCode = oauthHand.CityCodeParse(context, vkUser.City.Id, vkUser.City.Title, registerModel.CountryCode, false);
                        }
                        context.VkOAuthTokens.First(x => x.Id == entry.Id).IsExpired = true;
                        await context.SaveChangesAsync();
                        return registerModel;
                    }
                }
            }
            return null;
        }

        private DateTime vkDateTimeParse(string vkDateTime)
        {
            DateTime date = DateTime.ParseExact(vkDateTime, "d.M.yyyy", null);
            return date;
        }

        private int vkSexUserParse(int vkUserSex)
        {
            if (vkUserSex == 2)
                return  1;
            else
                return 0;
        }
    }
}