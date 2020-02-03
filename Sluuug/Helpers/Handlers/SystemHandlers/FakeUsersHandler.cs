using CloudinaryDotNet.Actions;
using Context;
using FakeUsers;
using SharedModels.Enums;
using SharedModels.UserInfo.Registration;
using SharedModels.Users;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slug.Helpers.Handlers.SystemHandlers
{
    public class FakeUsersHandler
    {
        public async Task CreateUserFromDirect(DataBaseContext context, BaseRegistrationModel realUser)
        {
            await Task.Run(()=> 
            {
                var fake = new CreateFakeUser();
                var item = context.FakeUsers.FirstOrDefault(x => x.CityCode == realUser.CityCode && x.CountryCode == realUser.CountryCode);
                Cities cityItem = context.Cities.First(x => x.CitiesCode == realUser.CityCode);
                Countries countryItem = context.Countries.First(x => x.CountryCode == realUser.CountryCode);

                realUser.CityTitle = cityItem.Title;
                realUser.CountryTitle = countryItem.Title;
                uint offset = 500;
                if (item != null)
                    offset = (uint)item.UsersCount;
                List<BaseUser> fakeUsers = fake.GetFakeUsersFromVk(realUser, offset);
                fakeUsers.ForEach((user) => 
                {
                    if (user.Vip)
                    {
                    }
                });
                context.FakeUsers.Add(new FakeUser()
                {
                    UsersCount = 510,
                    CountryCode = realUser.CountryCode,
                    CityCode = realUser.CityCode
                });
            });
        }
    }
}