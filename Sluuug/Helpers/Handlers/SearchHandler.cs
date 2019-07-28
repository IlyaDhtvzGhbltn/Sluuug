using Context;
using Slug.Context.Dto.Search;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

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
                    { UserMinDateOfBirth = DateTime.Now.AddYears(-200), UserMaxDateOfBirth = DateTime.Now.AddYears(-70) }
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

        public SearchUsersResponse SearchUsers(SearchUsersRequest request, int page)
        {
            if (page <= 0)
                page = 1;
            var responce = new SearchUsersResponse();

            using (var context = new DataBaseContext())
            {
                var maxDate = datesOfBirth[request.userAge].UserMaxDateOfBirth;
                var minDate = datesOfBirth[request.userAge].UserMinDateOfBirth;

                string predicate = 
                    string.Format(
                        "UserFullInfo.NowCityCode=={0}&&" +
                        "UserFullInfo.NowCountryCode=={1}&&"+
                        "UserFullInfo.Sex=={2}&&"+
                        "UserFullInfo.DatingPurpose=={3}",
                        request.userCity, 
                        request.userCountry,
                        (int)request.userSex, 
                        (int)request.userDatingPurpose);
                //+@" &&
                //UserFullInfo.DateOfBirth < "+maxDate + @"&&
                //UserFullInfo.DateOfBirth > "+minDate;

                if (!string.IsNullOrWhiteSpace(request.userName))
                    predicate = predicate + string.Format("&&UserFullInfo.Name==\"{0}\"||UserFullInfo.SurName==\"{0}\"", request.userName); ;
                if (request.userSearchSex != -1)
                    predicate = predicate + string.Format("&&UserFullInfo.userDatingSex=={0}", request.userSearchSex);
                if (request.userSearchAge != -1)
                    predicate = predicate + string.Format("&&UserFullInfo.userDatingAge=={0}", request.userSearchAge);

                List<User> result = context.Users
                    .AsQueryable()
                    .Where(predicate)
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * usersOnPage)
                    .Take(usersOnPage)
                    .ToList();

                int multipleCount = context.Users.AsQueryable().Where(predicate).Count();

                decimal del = ((decimal)multipleCount / (decimal)usersOnPage);
                int resMultiple = Convert.ToInt32(Math.Ceiling(del));
                if (page > resMultiple)
                    page = resMultiple;

                responce.PagesCount = resMultiple;

                responce.Users = result
                    .Select(user => new Model.Users.BaseUser
                    {
                        Age = DateTime.Now.Year - user.UserFullInfo.DateOfBirth.Year, 
                        UserId = user.Id,
                        AvatarResizeUri = context.Avatars.First(ava => ava.Id == user.AvatarId).ImgPath,
                        Country = context.Countries.First(country => user.UserFullInfo.NowCountryCode == country.CountryCode && country.Language == LanguageType.Ru).Title,
                        City = context.Cities.First(cit => user.UserFullInfo.NowCityCode == cit.CitiesCode && cit.Language == LanguageType.Ru).Title,
                        SurName = user.UserFullInfo.SurName,
                        Name = user.UserFullInfo.Name,
                        HelloMessage = user.UserFullInfo.HelloMessage,
                        purpose = (DatingPurposeEnum) user.UserFullInfo.DatingPurpose,
                        userSearchSex = (SexEnum)user.UserFullInfo.userDatingSex,
                        userSearchAge = (AgeEnum)user.UserFullInfo.userDatingAge
                    })
                    .ToList();

            }
            return responce;
        }
    }
}