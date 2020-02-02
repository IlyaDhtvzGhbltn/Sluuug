using CloudinaryDotNet.Actions;
using Context;
using FakeUsers;
using SharedModels.Enums;
using SharedModels.UserInfo.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers.Handlers.SystemHandlers
{
    public class FakeUsersHandler
    {
        public async void CreateUserFromDirect(DataBaseContext context, BaseRegistrationModel realUser)
        {
            var fake = new CreateFakeUser();
            //fake.GetFakeUsersFromVk(new BaseRegistrationModel()
            //{
            //    CityTitle = "БЕЛГОРОД",
            //    CityCode = 722,
            //    CountryCode = 7,
            //    DateBirth = DateTime.Now.AddYears(-21),
            //    Sex = (int)SexEnum.woman
            //}, 500);
        }
    }
}