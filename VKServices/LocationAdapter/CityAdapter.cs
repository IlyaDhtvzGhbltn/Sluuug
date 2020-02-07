using System;
using System.Linq;
using System.Text;
using VkNet;
using System.Collections.Generic;
using VkNet.Model.RequestParams.Database;
using System.Threading.Tasks;

namespace VKServices.LocationAdapter
{
    public class CityAdapter
    {
        readonly Dictionary<int, int> LocalCountryIdToVkCountryId = new Dictionary<int, int>()
        {
            { 7, 1 },
            { 380, 2 },
            { 375, 3 }
        };

        public long? GetCityId(VkApi context, int localCountryId, string localCityTitle)
        {
            var cityParam = new GetCitiesParams()
            {
                Count = 10,
                CountryId = LocalCountryIdToVkCountryId.ContainsKey(localCountryId) ? LocalCountryIdToVkCountryId[localCountryId] : localCountryId,
                Query = localCityTitle
            };
            var city = context.Database.GetCities(cityParam);
            return city[0].Id;
        }


        public int GetCountryId(int localCountryId)
        {
            if (LocalCountryIdToVkCountryId.ContainsKey(localCountryId))
                return LocalCountryIdToVkCountryId[localCountryId];
            else return localCountryId;
        }

    }
}
