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
                for (int i = 4; i < 34; i++)
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


        private static string getRandomTitle(string[] mass)
        {
            return mass[rnd.Next(0, mass.Length - 1)];
        }
    }
}