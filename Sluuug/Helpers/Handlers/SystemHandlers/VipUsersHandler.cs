using Context;
using Slug.Context.Dto;
using Slug.Model.Users;
using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class VipUsersHandler
    {
        public VipResponce VipUsers(UserLocation location, int userId)
        {
            using (var context = new DataBaseContext())
            {
                VipResponce resp = new VipResponce();
                var list = new List<VipUserModel>();
                var query = context.Users
                    .Where(x => x.UserFullInfo.NowCityCode == location.CityCode &&
                            x.UserFullInfo.NowCountryCode == location.CountryCode && 
                            x.UserFullInfo.VipStatusExpiredDate != null &&
                            x.UserFullInfo.VipStatusExpiredDate > DateTime.UtcNow)
                            .ToArray();

                string country = context.Countries
                    .FirstOrDefault(x => x.CountryCode == location.CountryCode && x.Language == LanguageType.Ru)
                    .Title;
                string city = context.Cities
                    .FirstOrDefault(x => x.CitiesCode == location.CityCode && x.Language == LanguageType.Ru)
                    .Title;

                foreach (Context.Tables.User item in query)
                {

                    string avatar = context.Avatars
                        .First(x=>x.Id == item.AvatarId)
                        .MediumAvatar;
                    int age = (DateTime.Now.Year - item.UserFullInfo.DateOfBirth.Year);
                    if (DateTime.Now.DayOfYear < item.UserFullInfo.DateOfBirth.DayOfYear)
                        age--;

                    list.Add(new VipUserModel()
                    {
                        Id = item.Id,
                        Name = item.UserFullInfo.Name,
                        SurName = item.UserFullInfo.SurName,
                        Country = country == null ? "не указано" : country,
                        City = city == null ? "не указано" : city,
                        MidAvatarUri = avatar,
                        DatingPurpose = (Context.Dto.Search.DatingPurposeEnum)item.UserFullInfo.DatingPurpose,
                        Age = age
                    });
                }
                resp.Users = list;
                resp.City = city;
                resp.AlreadyVIP = userVipStatus(context, userId);
                return resp;
            }
        }


        private bool userVipStatus(DataBaseContext context, int userId)
        {
            var userInfo = context.Users.FirstOrDefault(x=>x.Id == userId).UserFullInfo;
            DateTime? vipExpired = userInfo.VipStatusExpiredDate;
            if (userInfo!= null && vipExpired != null)
            {
                if (vipExpired > DateTime.UtcNow)
                    return true;
            }
            return false;
        }
    }
}