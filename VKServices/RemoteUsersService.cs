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
using Slug.Extension;
using SharedModels.Users.Registration;
using SharedModels.Enums;

namespace RemoteServices
{
    public class RemoteUsersService
    {
        private int appId { get; }
        private string login { get; }
        private string password { get; }
        public VkApi service { get; set; }

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
                Login = "ssuper10",
                Password = "Quiputgbn12",
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
                Country = localCity,
                City = localCity
            };
            var users = service.Users.SearchAsync(searchParams).GetAwaiter().GetResult().ToList();

            var fnUsers = new List<FakeUserModel>();
            users.ForEach(vkUser => 
            {
                DateTime dateBirth;
                DateTime.TryParse(vkUser.BirthDate, out dateBirth);
                int age = dateBirth != null ? dateBirth.FullYearsElapsed() : ageFrom;
                fnUsers.Add(new FakeUserModel()
                {
                    Age = age > 0 ? age : ageFrom,
                    SexCode = VkSexFakeUser[vkUser.Sex],
                    RemoteId = vkUser.Id.ToString(),
                    DateBirth = dateBirth != null ? dateBirth : DateTime.UtcNow.AddYears(-ageFrom).AddDays(-5), 
                    City = cityTitle,
                    Country = countryTitle,
                    AvatarType = SharedModels.Enums.AvatarTypesEnum.OutNetLoad,
                    SmallAvatar = vkUser.Photo50.AbsoluteUri,
                    MediumAvatar = vkUser.Photo100.AbsoluteUri,
                    LargeAvatar = vkUser.Photo200 != null ? vkUser.Photo200.AbsoluteUri : vkUser.Photo200Orig.AbsoluteUri,
                    Name = vkUser.FirstName,
                    SurName = vkUser.LastName,
                    HelloMessage = !string.IsNullOrWhiteSpace(vkUser.Status) ? vkUser.Status : "Всем привет!",
                    UserType = RegistrationTypeService.Vk
                });
            });
            return fnUsers;
        }
    }
}
