using CloudinaryDotNet.Actions;
using Context;
using FakeUsers;
using SharedModels.Enums;
using SharedModels.Users;
using SharedModels.Users.Registration;
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
        public async Task CreateFakesUserFromDirect(BaseRegistrationModel realUser)
        {
            await Task.Run(async () =>
            {
               await createUser(realUser);
            });
        }

        private async Task createUser(BaseRegistrationModel realUser)
        {
            using (var context = new DataBaseContext())
            {
                var fake = new CreateFakeUser();
                var fakeUSersCityItem = context.FakeUsers.FirstOrDefault(x => x.CityCode == realUser.CityCode && x.CountryCode == realUser.CountryCode);
                Cities cityItem = context.Cities.First(x => x.CitiesCode == realUser.CityCode);
                Countries countryItem = context.Countries.First(x => x.CountryCode == realUser.CountryCode);

                realUser.CityTitle = cityItem.Title;
                realUser.CountryTitle = countryItem.Title;
                uint offset = 500;
                if (fakeUSersCityItem != null)
                    offset = (uint)fakeUSersCityItem.UsersCount;
                List<FakeUserModel> fakeUsers = fake.GetFakeUsersFromVk(realUser, offset);

                var fakeLocallUsers = new List<User>();
                var newFakeUsersAvatars = new List<Avatars>();
                fakeUsers.ForEach((fakeUser) =>
                {
                    var avatarGuid = Guid.NewGuid();
                    newFakeUsersAvatars.Add(new Avatars()
                    {
                        GuidId = avatarGuid,
                        AvatarType = AvatarTypesEnum.OutNetLoad,
                        IsStandart = false,
                        SmallAvatar = fakeUser.SmallAvatar,
                        MediumAvatar = fakeUser.MediumAvatar,
                        LargeAvatar = fakeUser.LargeAvatar,
                        UploadTime = DateTime.UtcNow
                    });
                    var user = new User()
                    {
                        Login = "vk_" + fakeUser.RemoteId,
                        UserStatus = (int)UserStatuses.Active,
                        AvatarGuidId = avatarGuid,
                        IsFakeBot = true,
                        RegisterDate = DateTime.UtcNow,
                        UserType = RegisterTypeEnum.VkUser
                    };

                    user.Settings = new UserSettings()
                    {
                        Email = "admin@friendlynet.ru",
                        NotificationType = Context.Dto.Settings.NotificationTypes.Never,
                        PasswordHash = "-2",
                        QuickMessage = false
                    };

                    user.UserFullInfo = new UserInfo()
                    {
                        DateOfBirth = fakeUser.DateBirth,
                        Name = fakeUser.Name,
                        SurName = fakeUser.SurName,
                        Sex = (int)fakeUser.SexCode,
                        NowCountryCode = countryItem.CountryCode,
                        NowCityCode = cityItem.CitiesCode,
                        HelloMessage = fakeUser.HelloMessage,
                        userDatingSex = (int)fakeUser.userSearchSex,
                        DatingPurpose = (int)fakeUser.purpose,
                        userDatingAge = (int)fakeUser.userSearchAge,
                        IdVkUser = long.Parse(fakeUser.RemoteId),
                    };
                    if (fakeUser.Vip)
                        user.UserFullInfo.VipStatusExpiredDate = DateTime.UtcNow.AddDays(5);

                    fakeLocallUsers.Add(user);
                });
                context.Avatars.AddRange(newFakeUsersAvatars);
                context.Users.AddRange(fakeLocallUsers);
                if (fakeUSersCityItem == null)
                {
                    context.FakeUsers.Add(new FakeUser()
                    {
                        UsersCount = 500 + fakeUsers.Count,
                        CountryCode = realUser.CountryCode,
                        CityCode = realUser.CityCode
                    });
                }
                else
                {
                    fakeUSersCityItem.UsersCount = fakeUSersCityItem.UsersCount + fakeUsers.Count;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}