using Context;
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
        public List<VipUserModel> GetVipsByCity(UserLocation location)
        {
            using (var context = new DataBaseContext())
            {
                var list = new List<VipUserModel>();
                var query = context.Users
                    .Where(x => x.UserFullInfo.NowCityCode == location.CityCode &&
                            x.UserFullInfo.NowCountryCode == location.CountryCode && 
                            x.UserFullInfo.VipStatusExpiredDate != null &&
                            x.UserFullInfo.VipStatusExpiredDate > DateTime.Now)
                            .ToArray();
                foreach (Context.Tables.User item in query)
                {
                    string country = context.Countries
                        .FirstOrDefault(x=>x.CountryCode == item.UserFullInfo.NowCountryCode && x.Language == LanguageType.Ru)
                        .Title;
                    string city = context.Cities
                        .FirstOrDefault(x=>x.CitiesCode == item.UserFullInfo.NowCityCode && x.Language == LanguageType.Ru)
                        .Title;
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
                return list;
            }
        }
    }
}