using Context;
using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class SearchHandler
    {
        public ICollection<SearchCityItem> CitiesByCountryID(int code)
        {
            using (var context = new DataBaseContext())
            {
                List<Context.Tables.Cities> citiesCollection = context.Cities
                    .Where(x => x.CountryCode == code && x.Language == LanguageType.Ru)
                    .ToList();
                List<SearchCityItem> itemCollection = citiesCollection
                    .Select(x => new SearchCityItem
                    {
                         CityCode = x.CitiesCode,
                         Title = x.Title
                    }).ToList();
                return itemCollection;
            }
        }

        public SearchUsersResponse SearchUsers(SearchUsersRequest request)
        {

            return new SearchUsersResponse();
        }
    }
}