using Context;
using Slug.Context.Dto.Search;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using Slug.ImageEdit;
using Slug.Model.Users;

namespace Slug.Helpers
{
    public class SearchHandler
    {
        private const int usersOnPage = 16;

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

        public List<SearchCityItem> CitiesByCountryID(int code)
        {
            using (var context = new DataBaseContext())
            {
                List<Cities> citiesCollection = context.Cities
                    .Where(x => x.CountryCode == code && x.Language == LanguageType.Ru)
                    .OrderBy(x => x.Title)
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

        public List<Countries> Countries()
        {
            using (var context = new DataBaseContext())
            {
                var countriesList = context.Countries.Where(x=> x.Language == LanguageType.Ru).ToList();
                return countriesList;
            }
        }

        public SearchUsersResponse SearchUsers(SearchUsersRequest request, int page)
        {
            if (page <= 0)
                page = 1;
            var responce = new SearchUsersResponse();

            using (var context = new DataBaseContext())
            {
                string predicate = string.Empty;
                if (request.userSex != -1)
                {
                    predicate = predicate + string.Format("UserFullInfo.Sex=={0}", request.userSex);
                }
                if (request.userCountry != -1)
                {
                    predicate = formatPredicate(predicate, 
                        string.Format("UserFullInfo.NowCountryCode=={0}", request.userCountry));
                }
                if (request.userCity != -1)
                {
                    predicate = formatPredicate(predicate, 
                        string.Format("UserFullInfo.NowCityCode=={0}", request.userCity));
                }
                if (request.userDatingPurpose != -1)
                {
                    predicate = formatPredicate(predicate, 
                        string.Format("UserFullInfo.DatingPurpose=={0}", (int)request.userDatingPurpose));
                }
                if (request.userAge != -1)
                {
                    var maxDate = datesOfBirth[(AgeEnum)request.userAge].UserMaxDateOfBirth;
                    var minDate = datesOfBirth[(AgeEnum)request.userAge].UserMinDateOfBirth;
                    string agePredicate =
                    string.Format(
                            "UserFullInfo.DateOfBirth>={0}&&" +
                            "UserFullInfo.DateOfBirth<={1}",
                            string.Format("DateTime({0})", minDate.ToString("yyyy,MM,dd")),
                            string.Format("DateTime({0})", maxDate.ToString("yyyy,MM,dd"))
                            );
                    predicate = formatPredicate(predicate, agePredicate);
                }
                if (!string.IsNullOrWhiteSpace(request.userName))
                {
                    int spaceIndex = request.userName.IndexOf(' ');
                    string name = string.Empty;
                    string surName = string.Empty;
                    if (spaceIndex > 0)
                    {
                        name = request.userName.Substring(0, spaceIndex);
                        surName = request.userName.Substring(spaceIndex + 1);

                        predicate = formatPredicate(predicate,
                            string.Format("(UserFullInfo.Name.Contains(\"{0}\")&&UserFullInfo.SurName.Contains(\"{1}\"))", name, surName));
                    }
                    else
                    {
                        predicate = formatPredicate(predicate,
                            string.Format("(UserFullInfo.Name.Contains(\"{0}\")||UserFullInfo.SurName.Contains(\"{0}\"))", request.userName));
                    }

                }
                if (request.userSearchSex != -1)
                {
                    predicate = formatPredicate(predicate, 
                        string.Format("UserFullInfo.userDatingSex=={0}", request.userSearchSex));
                }
                if (request.userSearchAge != -1)
                {
                    predicate = formatPredicate(predicate,
                        string.Format("UserFullInfo.userDatingAge=={0}", request.userSearchAge));
                }
                var result = new List<User>();
                int multipleCount = 0;

                if (predicate.Length > 0)
                {
                    result = context.Users
                        .AsQueryable()
                        .Where(predicate)
                        .OrderBy(x => x.Id)
                        .Skip((page - 1) * usersOnPage)
                        .Take(usersOnPage)
                        .ToList();
                    multipleCount = context.Users.AsQueryable().Where(predicate).Count();
                }
                else
                {
                    result = context.Users
                        .AsQueryable()
                        .OrderBy(x => x.Id)
                        .Skip((page - 1) * usersOnPage)
                        .Take(usersOnPage)
                        .ToList();
                    multipleCount = context.Users.AsQueryable().Count();
                }

                decimal del = ((decimal)multipleCount / (decimal)usersOnPage);
                int resMultiple = Convert.ToInt32(Math.Ceiling(del));
                if (page > resMultiple)
                    page = resMultiple;

                responce.PagesCount = resMultiple;
                responce.Users = new List<FoudUser>();

                foreach (var foundedUser in result)
                {
                    var userModel = new FoudUser();
                    userModel.Age = DateTime.Now.Year - foundedUser.UserFullInfo.DateOfBirth.Year;
                    userModel.UserId = foundedUser.Id;
                    userModel.Country = context.Countries.First(country => 
                    foundedUser.UserFullInfo.NowCountryCode == country.CountryCode && country.Language == LanguageType.Ru).Title;


                    if (foundedUser.UserFullInfo.NowCityCode != 0)
                    {
                        userModel.City = context.Cities.First(cit =>
                        foundedUser.UserFullInfo.NowCityCode == cit.CitiesCode && cit.Language == LanguageType.Ru).Title;
                    }
                    else
                        userModel.City = "не указан";

                    userModel.SurName = foundedUser.UserFullInfo.SurName;
                    userModel.Name = foundedUser.UserFullInfo.Name;
                    userModel.HelloMessage = foundedUser.UserFullInfo.HelloMessage;
                    userModel.purpose = (DatingPurposeEnum)foundedUser.UserFullInfo.DatingPurpose;
                    userModel.userSearchSex = (SexEnum)foundedUser.UserFullInfo.userDatingSex;
                    userModel.userSearchAge = (AgeEnum)foundedUser.UserFullInfo.userDatingAge;
                    var vipExpired = foundedUser.UserFullInfo.VipStatusExpiredDate;
                    if (vipExpired != null && vipExpired > DateTime.UtcNow)
                        userModel.Vip = true;

                    Avatars avatar = context.Avatars.First(ava => ava.Id == foundedUser.AvatarId);
                    userModel.LargeAvatar = avatar.LargeAvatar;

                    responce.Users.Add(userModel);
                }
            }
            return responce;
        }

        private string formatPredicate(string predicate, string additionCondition)
        {
            string formatedPredicate = string.Empty;
            if (predicate.Length <= 0)
                formatedPredicate = additionCondition;
            else
                formatedPredicate = predicate + "&&" + additionCondition;
            return formatedPredicate;
        }
    }
}