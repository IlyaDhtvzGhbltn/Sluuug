using Context;
using Slug.Context.Dto.Search;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.DbInitialisation
{
    public static class NewUserInitial
    {
        private readonly static string[] names = new string[] { "Иван", "Андрей", "Дима", "Илья" };
        private readonly static string[] surNames = new string[] { "Володин", "Пригожин", "Фролов", "Карпов", "Навальный" };
        private readonly static DatingPurposeEnum[] DatingPurposes = new DatingPurposeEnum[] { DatingPurposeEnum.Communication, DatingPurposeEnum.SeriousRelationship, DatingPurposeEnum.Sex };
        private readonly static AgeEnum[] UserSearchAge = new AgeEnum[] { AgeEnum.from16to20, AgeEnum.from21to26, AgeEnum.from27to32, AgeEnum.from33to40, AgeEnum.from41to49, AgeEnum.from50to59, AgeEnum.from60to69, AgeEnum.morethan70 };
        private static Random rnd = new Random((int)DateTime.Now.Ticks);

        public static void Initialize(int item)
        {
            using (var context = new DataBaseContext())
            {
                var usersCollection = new List<User>();
                for (int i = 0; i < item; i++)
                {
                    var user = new User()
                    {
                        AvatarId = rnd.Next(1, 3),
                        CountryCode = 7,
                        Login = string.Format("login{0}", i),
                        UserStatus = 1,
                        Settings = new UserSettings()
                        {
                            Email = "alter.22.04@gmail.com",
                            NotificationType = Context.Dto.Settings.NotificationTypes.Never,
                            PasswordHash = "a1bd4e0efc7ce8bd1d63433a0baa87e3a486fbfe2729d73d1dbf7d2822d201ee8726c6d94da1f09f1a53554e440ad6041ecab545b2085dc28c6f6849f0fcea23",
                        },
                        UserFullInfo = new UserInfo
                        {
                            DateOfBirth = DateTime.Now.AddYears(- rnd.Next(17, 88)),
                            Name = getRandomTitle(names),
                            NowCountryCode = 7,
                            SurName = getRandomTitle(surNames),
                            NowCityCode = 495,
                            DatingPurpose = (int)DatingPurposes[getRandomIndex(0, 2)],
                            Sex = (int)SexEnum.man,
                            userDatingAge = (int)UserSearchAge[getRandomIndex(0, 7)],
                            userDatingSex = (int)SexEnum.woman
                        }
                    };
                    usersCollection.Add(user);
                }

                context.Users.AddRange(usersCollection);
                context.SaveChanges();
            }

        }

        public static void UserFullInfo(string userLogin)
        {
            using (var context = new DataBaseContext())
            {
                User user = context.Users.First(x=>x.Login == userLogin);
                user.UserFullInfo.Educations = new List<Context.Dto.UserFullInfo.Education>();
                user.UserFullInfo.Educations.Add(new Context.Dto.UserFullInfo.Education()
                {
                    Title = "Начальная школа 133",
                    Start = new DateTime().AddYears(1990),
                    End = new DateTime().AddYears(1999),
                    CountryCode = 7,
                    CityCode = 495,
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.School,
                    Comment = "комеентарий про школу.",
                    Id = Guid.NewGuid(),
                    User = user
                });
                user.UserFullInfo.Educations.Add(new Context.Dto.UserFullInfo.Education()
                {
                    Title = "Колледж 10",
                    Start = new DateTime().AddYears(2000),
                    End = new DateTime().AddYears(2004),
                    CountryCode = 7,
                    CityCode = 495,
                    Faculty = "Программирование на ++",
                    Specialty = "ИТ",
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.College,
                    Comment = "Колледж комент.",
                    Id = Guid.NewGuid(),
                    User = user
                });
                user.UserFullInfo.Educations.Add(new Context.Dto.UserFullInfo.Education()
                {
                    Title = "Универ 1",
                    Start = new DateTime().AddYears(2006),
                    End = new DateTime().AddYears(2010),
                    CountryCode = 7,
                    CityCode = 495,
                    Faculty = "ВчМат",
                    Specialty = "ИТиВТ",
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.UniverCity,
                    Comment = "А теперь коммент про универ",
                    Id = Guid.NewGuid(),
                    User = user
                });

                user.UserFullInfo.Events = new List<Context.Dto.UserFullInfo.MemorableEvents>();
                user.UserFullInfo.Events.Add(new Context.Dto.UserFullInfo.MemorableEvents()
                {
                    EventTitle = "Свадьба",
                    EventComment = "тамада был отстой",
                    DateEvent = new DateTime().AddYears(2011),
                    Id = Guid.NewGuid(),
                    User = user
                });

                user.UserFullInfo.Places = new List<Context.Dto.UserFullInfo.LifePlaces>();
                user.UserFullInfo.Places.Add(new Context.Dto.UserFullInfo.LifePlaces()
                {
                    CountryCode = 7,
                    CityCode = 495,
                    Start = new DateTime().AddYears(2011),
                    End = new DateTime().AddYears(2012),
                    Comment = "неплохо пожил, но уехал",
                    Id = Guid.NewGuid(),
                    User = user
                });
                user.UserFullInfo.Places.Add(new Context.Dto.UserFullInfo.LifePlaces()
                {
                    CountryCode = 1,
                    CityCode = 718,
                    Start = new DateTime().AddYears(2012),
                    UntilNow = true,
                    Comment = "до сих пор тут живу",
                    Id = Guid.NewGuid(),
                    User = user
                });
                user.UserFullInfo.Works = new List<Context.Dto.UserFullInfo.WorkPlaces>();
                user.UserFullInfo.Works.Add(new Context.Dto.UserFullInfo.WorkPlaces()
                {
                    CompanyTitle = "Asp Company Moskow",
                    Start = new DateTime().AddYears(2011),
                    End = new DateTime().AddYears(2012),
                    CountryCode = 7,
                    CityCode = 495,
                    Comment = "отстой работа, зп никакой", 
                    Position = "разработчик unity",
                    Id = Guid.NewGuid(),
                    User = user
                });
                user.UserFullInfo.Works.Add(new Context.Dto.UserFullInfo.WorkPlaces()
                {
                    CompanyTitle = "Dot Net New-York",
                    Start = new DateTime().AddYears(2012),
                    UntilNow = true,
                    CountryCode = 1,
                    CityCode = 718,
                    Comment = "норм место",
                    Position = "Старший разработчик unity",
                    Id = Guid.NewGuid(),
                    User = user
                });

                context.SaveChanges();
            }
        }

        private static string getRandomTitle(string[] mass)
        {
            return mass[rnd.Next(0, mass.Length - 1)];
        }

        private static int getRandomIndex(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}