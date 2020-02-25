using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using SharedModels.Users;
using VkNet.Enums.Filters;
using VKServices.LocationAdapter;
using Slug.Extensions;
using SharedModels.Users.Registration;
using SharedModels.Enums;
using System.Text.RegularExpressions;

namespace RemoteServices
{
    public class RemoteUsersService
    {
        private int appId { get; }
        private string login { get; }
        private string password { get; }
        public VkApi service { get; set; }

        private Random rnd = new Random();

        Dictionary<VkNet.Enums.Sex, SexEnum> VkSexFakeUser = new Dictionary<VkNet.Enums.Sex, SexEnum>()
        {
            { VkNet.Enums.Sex.Female, SexEnum.woman },
            { VkNet.Enums.Sex.Male, SexEnum.man }
        };

        public RemoteUsersService(int appId, string login, string password)
        {
            this.appId = appId;
            this.login = login;
            this.password = password;
            ApiAuth();
        }

        private void ApiAuth()
        {
            var api = new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                ApplicationId = 7170448,
                Login = this.login,
                Password = this.password,
            });
            service = api;
        }

        public List<FakeUserModel> Search(int sex, int ageFrom, int ageTo, int localCity, int country, string cityTitle, string countryTitle,  uint offset)
        {
            UserSearchParams searchParams = new UserSearchParams()
            {
                Count = 10,
                Sex = (VkNet.Enums.Sex)sex,
                Fields = ProfileFields.All,
                Offset = offset,
                AgeFrom = (ushort)ageFrom,
                AgeTo = (ushort)ageTo,
                Country = country,
                City = localCity
            };
            var users = service.Users.SearchAsync(searchParams).GetAwaiter().GetResult().ToList();

            var fnUsers = new List<FakeUserModel>();
            users.ForEach(vkUser => 
            {
                DateTime dateBirth;
                DateTime.TryParse(vkUser.BirthDate, out dateBirth);
                if (vkUser.BirthDate == null || dateBirth == null || dateBirth.Year == 2020)
                {
                    dateBirth = DateTime.UtcNow.AddYears(rnd.Next(-ageTo, -ageFrom)).AddDays(rnd.Next(-20, 30));
                }
                string hello = !string.IsNullOrWhiteSpace(vkUser.Status) ? vkUser.Status : "Всем привет!";
                string helloNoNumbers = Regex.Replace(hello, @"[\d]", string.Empty);
                fnUsers.Add(new FakeUserModel()
                {
                    Age = dateBirth.FullYearsElapsed(),
                    SexCode = VkSexFakeUser[vkUser.Sex],
                    RemoteId = vkUser.Id.ToString(),
                    DateBirth = dateBirth, 
                    City = cityTitle,
                    Country = countryTitle,
                    AvatarType = AvatarTypesEnum.OutNetLoad,
                    SmallAvatar = vkUser.Photo50.AbsoluteUri,
                    MediumAvatar = vkUser.Photo100.AbsoluteUri,
                    LargeAvatar = vkUser.Photo200 != null ? vkUser.Photo200.AbsoluteUri : vkUser.Photo200Orig.AbsoluteUri,
                    Name = vkUser.FirstName,
                    SurName = vkUser.LastName,
                    HelloMessage = helloNoNumbers,
                    UserType = RegistrationTypeService.Vk
                });
            });
            return fnUsers;
        }
    }
}
