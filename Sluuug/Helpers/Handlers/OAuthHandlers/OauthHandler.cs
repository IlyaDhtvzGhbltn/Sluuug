using Context;
using NLog;
using Slug.Context.Dto.OAuth;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers.Handlers
{
    public class OauthHandler
    {
        public async Task SaveTokenEntry(VkToken token)
        {
            using (var context = new DataBaseContext())
            {
                context.VkOAuthTokens.Add(new Context.Tables.VkOAuthToken()
                {
                    Code = token.Code,
                    Token = token.Token,
                    VkUserId = token.VkUserId,
                    ReceivedDate = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task<string> SendRequest(string url, bool get = true)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response;
                    if (get)
                        response = client.GetAsync(url).GetAwaiter().GetResult();
                    else
                        response = client.PostAsync(url, null).GetAwaiter().GetResult();

                    if(response.IsSuccessStatusCode)
                    {
                        using (HttpContent content = response.Content)
                        {
                            string result = await content.ReadAsStringAsync();
                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger loggerInternal = LogManager.GetLogger("internal_error_logger");
                    loggerInternal.Error(ex);
                }
            }
            return null;
        }

        public int VkUserRegistredId(long vkUserId)
        {
            using (var context = new DataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserFullInfo.IdVkUser == vkUserId);
                if (user == null)
                    return 0;
                else
                    return user.Id;
            }
        }

        public int FBUserRegisterId(long fbUser)
        {
            using (var context = new DataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserFullInfo.IdFBUser == fbUser);
                if (user == null)
                    return 0;
                else
                    return user.Id;
            }
        }

        public int CountryCodeParse(DataBaseContext context, int outCountryCode, string outCountryTitle, bool fieldAvailable)
        {
            if (fieldAvailable)
            {
                Countries countryEntry = context.Countries.FirstOrDefault(x => x.Title == outCountryTitle);
                if (countryEntry != null)
                    return countryEntry.CountryCode;
                else
                {
                    context.Countries.Add(new Context.Tables.Countries()
                    {
                        CountryCode = outCountryCode,
                        Language = LanguageType.Ru,
                        Title = outCountryTitle
                    });
                    context.SaveChanges();
                    return outCountryCode;
                }
            }
            else
            {
                return 7;
            }
        }

        public int CityCodeParse(DataBaseContext context, int outCityCode, string outCityTitle, int outCountryCode, bool fieldAvailable)
        {
            if (fieldAvailable)
            {
                Cities cityCode = context.Cities.FirstOrDefault(x => x.Title == outCityTitle);
                if (cityCode != null)
                    return cityCode.CitiesCode;
                Cities cityCodeUp = context.Cities.FirstOrDefault(x => x.Title == outCityTitle.ToUpper());
                if (cityCodeUp != null)
                    return cityCodeUp.CitiesCode;
                else
                {
                    context.Cities.Add(new Cities()
                    {
                        CitiesCode = outCityCode,
                        CountryCode = outCountryCode,
                        Title = outCityTitle,
                        Language = LanguageType.Ru
                    });
                    context.SaveChanges();
                    return outCityCode;
                }
            }
            else
            {
                return 495;
            }
        }
    }
}