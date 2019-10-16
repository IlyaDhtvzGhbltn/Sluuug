using Context;
using Newtonsoft.Json;
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
        private string appSecret { get; set; } = "OFuxinKsGFinAbvA73Xu";
        private string appId { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.vkAppId.ToString()];
        private string redirectUri { get; set; } = WebAppSettings.AppSettings[AppSettingsEnum.oauthVkRedirectUri.ToString()];
        private string vkFields { get; set; } = "country,city,bdate,sex,photo_200_orig";
        private string vkApiVersion { get; set; } = "5.102";

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

        public async Task<VkRegisteringUserModel> GetVkUserInfo(string vkOneTimeCode)
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
                    using (var client = new HttpClient())
                    {
                        HttpResponseMessage response = client.GetAsync(request).GetAwaiter().GetResult();
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                string result = await content.ReadAsStringAsync();
                                var vkInfo = JsonConvert.DeserializeObject<VkResponce>(result);
                                VkUserInfo vkUser = vkInfo.UserInfo[0];
                                var registerModel = new VkRegisteringUserModel();
                                registerModel.VkId = vkUser.Id;
                                registerModel.Name = vkUser.FirstName;
                                registerModel.SurName = vkUser.LastName;
                                registerModel.VkAvatar = vkUser.Photo200_Orig;
                                registerModel.Status = vkUser.Status;

                                registerModel.Sex = vkSexUserParse(vkUser.Sex);
                                registerModel.DateBirth = vkDateTimeParse(vkUser.Bdate);
                                registerModel.CountryCode = vkCountryCodeParse(context, vkUser.Country.Id, vkUser.Country.Title);
                                registerModel.CityCode = vkCityCodeParse(context, vkUser.City.Id, registerModel.CountryCode, vkUser.City.Title);
                                context.VkOAuthTokens.First(x => x.Id == entry.Id).IsExpired = true;
                                await context.SaveChangesAsync();
                                return registerModel;
                             }
                        }
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

        private int vkCountryCodeParse(DataBaseContext context, int vkCountryCode, string vkCountryTitle)
        {
            Countries countryEntry = context.Countries.FirstOrDefault(x => x.Title == vkCountryTitle);
            if (countryEntry != null)
                return countryEntry.CountryCode;
            else
            {
                context.Countries.Add(new Context.Tables.Countries()
                {
                     CountryCode = vkCountryCode,
                     Language = LanguageType.Ru,
                     Title = vkCountryTitle
                });
                context.SaveChanges();
                return vkCountryCode;
            }
        }

        private int vkCityCodeParse(DataBaseContext context, int vkCityCode, int vkCountryCode, string vkCityTitle)
        {
            Cities cityCode = context.Cities.FirstOrDefault(x => x.Title == vkCityTitle);
            if (cityCode != null)
                return cityCode.CitiesCode;
            Cities cityCodeUp = context.Cities.FirstOrDefault(x => x.Title == vkCityTitle.ToUpper());
            if (cityCodeUp != null)
                return cityCodeUp.CitiesCode;
            else
            {
                context.Cities.Add(new Cities()
                {
                     CitiesCode = vkCityCode,
                     CountryCode = vkCountryCode,
                     Title = vkCityTitle,
                     Language = LanguageType.Ru
                });
                context.SaveChanges();
                return vkCityCode;
            }
        }
    }
}