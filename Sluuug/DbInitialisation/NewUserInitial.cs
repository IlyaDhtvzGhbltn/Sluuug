using Context;
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
        private static Random rnd = new Random((int)DateTime.Now.Ticks);

        public static void Initialize()
        {
            using (var context = new DataBaseContext())
            {
                var usersCollection = new List<User>();
                for (int i = 4; i < 300; i++)
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
                            PasswordHash = "4DFF4EA340F0A823F15D3F4F01AB62EAE0E5DA579CCB851F8DB9DFE84C58B2B37B89903A740E1EE172DA793A6E79D560E5F7F9BD058A12A280433ED6FA46510A",
                        },
                        UserFullInfo = new UserInfo
                        {
                            DateOfBirth = DateTime.Now.AddYears(- rnd.Next(17, 88)),
                            Name = getRandomTitle(names),
                            NowCountryCode = 7,
                            SurName = getRandomTitle(surNames),
                            NowSityCode = 495,
                            Sex = Context.Dto.Search.SexEnum.man
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
                    SityCode = 495,
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.School,
                    Comment = "комеентарий про школу.",
                    EntryId = new Guid()
                });
                user.UserFullInfo.Educations.Add(new Context.Dto.UserFullInfo.Education()
                {
                    Title = "Колледж 10",
                    Start = new DateTime().AddYears(2000),
                    End = new DateTime().AddYears(2004),
                    CountryCode = 7,
                    SityCode = 495,
                    Faculty = "Программирование на ++",
                    Specialty = "ИТ",
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.College,
                    Comment = "Колледж комент.",
                    EntryId = new Guid()

                });
                user.UserFullInfo.Educations.Add(new Context.Dto.UserFullInfo.Education()
                {
                    Title = "Универ 1",
                    Start = new DateTime().AddYears(2006),
                    End = new DateTime().AddYears(2010),
                    CountryCode = 7,
                    SityCode = 495,
                    Faculty = "ВчМат",
                    Specialty = "ИТиВТ",
                    EducationType = Context.Dto.UserFullInfo.EducationTypes.University,
                    Comment = "А теперь коммент про универ",
                    EntryId = new Guid()

                });

                user.UserFullInfo.Events = new List<Context.Dto.UserFullInfo.MemorableEvents>();
                user.UserFullInfo.Events.Add(new Context.Dto.UserFullInfo.MemorableEvents()
                {
                    EventTitle = "Свадьба",
                    EventComment = "тамада был отстой",
                    DateEvent = new DateTime().AddYears(2011),
                    EntryId = new Guid()

                });

                user.UserFullInfo.Places = new List<Context.Dto.UserFullInfo.LifePlaces>();
                user.UserFullInfo.Places.Add(new Context.Dto.UserFullInfo.LifePlaces()
                {
                     CountryCode = 7,
                     SityCode = 495,
                     Start = new DateTime().AddYears(2011),
                     End = new DateTime().AddYears(2012),
                     Comment = "неплохо пожил, но уехал",
                    EntryId = new Guid()

                });
                user.UserFullInfo.Places.Add(new Context.Dto.UserFullInfo.LifePlaces()
                {
                    CountryCode = 1,
                    SityCode = 718,
                    Start = new DateTime().AddYears(2012),
                    UntilNow = true,
                    Comment = "до сих пор тут живу",
                    EntryId = new Guid()

                });
                user.UserFullInfo.Works = new List<Context.Dto.UserFullInfo.WorkPlaces>();
                user.UserFullInfo.Works.Add(new Context.Dto.UserFullInfo.WorkPlaces()
                {
                    CompanyTitle = "Asp Company Moskow",
                    Start = new DateTime().AddYears(2011),
                    End = new DateTime().AddYears(2012),
                    CountryCode = 7,
                    SityCode = 495,
                    Comment = "отстой работа, зп никакой", 
                    Position = "разработчик unity",
                    EntryId = new Guid()

                });
                user.UserFullInfo.Works.Add(new Context.Dto.UserFullInfo.WorkPlaces()
                {
                    CompanyTitle = "Dot Net New-York",
                    Start = new DateTime().AddYears(2012),
                    UntilNow = true,
                    CountryCode = 1,
                    SityCode = 718,
                    Comment = "норм место",
                    Position = "Старший разработчик unity",
                    EntryId = new Guid()

                });

                context.SaveChanges();
            }
        }

        private static string getRandomTitle(string[] mass)
        {
            return mass[rnd.Next(0, mass.Length - 1)];
        }
    }
}