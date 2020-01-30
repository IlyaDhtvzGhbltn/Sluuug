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

namespace RemoteServices
{
    public class FakeUsersService
    {
        private int appId { get; }
        private string login { get; }
        private string password { get; }
        VkApi service { get; set; }

        public FakeUsersService(int appId, string login, string password)
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

        public List<BaseUser> Search(int sex, int ageFrom, int ageTo, int city, int country, uint offset)
        {
            var fields = ProfileFields.Photo200 | ProfileFields.Photo50;

            UserSearchParams searchParams = new UserSearchParams()
            {
                Count = 10,
                Sex = (VkNet.Enums.Sex)sex,
                Fields = fields,
                Offset = offset,
                AgeFrom = (ushort)ageFrom,
                AgeTo = (ushort)ageTo,
                Country = country,
                City = city
            };
            var users = service.Users.Search(searchParams).ToList();


            var fnUsers = new List<BaseUser>();
            users.ForEach(vkUser => 
            {

                
            });
            return fnUsers;
        }
    }
}
