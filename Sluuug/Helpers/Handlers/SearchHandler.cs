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
        private const int usersOnPage = 5;

        private readonly IDictionary<AgeEnum, DatesUserSearch> datesOfBirth =
            new Dictionary<AgeEnum, DatesUserSearch>()
            {
                { AgeEnum.from16to20, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-20), UserMaxDateOfBirth = DateTime.Now.AddYears(-16) }
                },

                { AgeEnum.from21to26, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-26), UserMaxDateOfBirth = DateTime.Now.AddYears(-21) }
                },

                { AgeEnum.from27to32, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-32), UserMaxDateOfBirth = DateTime.Now.AddYears(-27) }
                },

                { AgeEnum.from33to40, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-40), UserMaxDateOfBirth = DateTime.Now.AddYears(-33) }
                },

                { AgeEnum.from41to49, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-49), UserMaxDateOfBirth = DateTime.Now.AddYears(-41) }
                },

                { AgeEnum.from50to59, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-59), UserMaxDateOfBirth = DateTime.Now.AddYears(-50) }
                },

                { AgeEnum.from60to69, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-69), UserMaxDateOfBirth = DateTime.Now.AddYears(-60) }
                },

                { AgeEnum.morethan70, new DatesUserSearch()
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-200), UserMaxDateOfBirth = DateTime.Now.AddYears(-77) }
                }
            };

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

        public SearchUsersResponse SearchUsers(SearchUsersRequest request, int insteadUserID, int page)
        {
            if (page <= 0)
                page = 1;
            var responce = new SearchUsersResponse();

            using (var context = new DataBaseContext())
            {
                var maxDate = datesOfBirth[request.userSearchAge].UserMaxDateOfBirth;
                var minDate = datesOfBirth[request.userSearchAge].UserMinDateOfBirth;

                List<Context.Tables.User> result = context.Users
                    .Where(x =>
                    x.Id != insteadUserID &&
                    x.UserFullInfo.NowSityCode == request.userSearchSity &&
                    x.UserFullInfo.NowCountryCode == request.userSearchCountry &&
                    x.UserFullInfo.Sex == request.userSearchSex &&
                    x.UserFullInfo.DateOfBirth < maxDate &&
                    x.UserFullInfo.DateOfBirth > minDate &&
                    (x.UserFullInfo.Name + " " + x.UserFullInfo.SurName).Contains(request.userSearchName)
                    )
                    .ToList();

                int multipleCount = result.Count;
                decimal del = ((decimal)multipleCount / (decimal)usersOnPage);
                int resMultiple = Convert.ToInt32(Math.Ceiling(del));
                if (page > resMultiple)
                    page = resMultiple;

                responce.PagesCount = resMultiple;

                responce.Users = result
                    .Skip((page - 1) * usersOnPage)
                    .Take(usersOnPage)
                    .Select(collect => new Model.CutUserInfoModel()
                {
                    UserId = collect.Id,
                    AvatarUri = context.Avatars.First(ava => ava.Id == collect.AvatarId).ImgPath,
                    Country = context.Countries.First(country => collect.UserFullInfo.NowCountryCode == country.CountryCode && country.Language == LanguageType.Ru).Title,
                    Sity = context.Cities.First(cit => collect.UserFullInfo.NowSityCode == cit.CitiesCode && cit.Language == LanguageType.Ru).Title,
                    DateBirth = collect.UserFullInfo.DateOfBirth,
                    SurName = collect.UserFullInfo.SurName,
                    Name = collect.UserFullInfo.Name
                })
                    .ToList();

            }
            return responce;
        }
    }
}