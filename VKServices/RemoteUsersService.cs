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

namespace RemoteServices
{
    public class RemoteUsersService
    {
        private int appId { get; }
        private string login { get; }
        private string password { get; }
        VkApi service { get; set; }

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

        public List<BaseUser> Search(int sex, int ageFrom, int ageTo, int localCity, int country, string cityQuery,  uint offset)
        {
            var vkAdapter = new CityAdapter();
            int vkCityCode = (int)vkAdapter.GetCityId(service, country, cityQuery);

            UserSearchParams searchParams = new UserSearchParams()
            {
                Count = 10,
                Sex = (VkNet.Enums.Sex)sex,
                Fields = ProfileFields.All,
                Offset = offset,
                AgeFrom = (ushort)ageFrom,
                AgeTo = (ushort)ageTo,
                Country = vkAdapter.LocalCountryIdToVkCountryId[country],
                City = vkCityCode
            };
            var users = service.Users.Search(searchParams).ToList();

            var fnUsers = new List<BaseUser>();
            users.ForEach(vkUser => 
            {
                DateTime dateBirth;
                DateTime.TryParse(vkUser.BirthDate, out dateBirth);
                fnUsers.Add(new BaseUser()
                {
                    Age = dateBirth.FullYearsElapsed(),
                    City = vkUser.City.Title,
                    Country = vkUser.Country.Title,
                    AvatarType = SharedModels.Enums.AvatarTypesEnum.OutNetLoad,
                    SmallAvatar = vkUser.Photo50.AbsoluteUri,
                    MediumAvatar = vkUser.Photo100.AbsoluteUri,
                    LargeAvatar = vkUser.Photo200.AbsoluteUri,
                    Name = vkUser.FirstName,
                    SurName = vkUser.LastName,
                    HelloMessage = !string.IsNullOrWhiteSpace(vkUser.Status) ? vkUser.Status : "Всем привет!",
                    UserType = SharedModels.Enums.RegisterTypeEnum.VkUser
                });
            });
            return fnUsers;
        }
    }

    public static class Ext
    {
        public static void dateExt(this DateTime date)
        {

        }
    }
}
