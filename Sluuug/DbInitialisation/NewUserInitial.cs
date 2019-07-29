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

        private readonly static string[] wnames = new string[] { "Ирина", "Ксения", "Олеся", "Настя" };
        private readonly static string[] wsurNames = new string[] { "Володина", "Пригожина", "Фролова", "Карпова", "Фиртешева" };

        private readonly static int[] countryCodes = new int[] { 1, 7 };
        private readonly static Dictionary<int,int> cityCodes = new Dictionary<int, int> { { 1, 718 }, { 7, 495 } };

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
                    var userSex = getRandomIndex(0, 1);
                    var userSearchDatingSex = rnd.Next(0, 1); ;
                    var userDatingPurpose = getRandomIndex(0, 2);
                    var userDatingAge = getRandomIndex(0, 7);
                    var countryCode = countryCodes[getRandomIndex(0, 1)];
                    var cityCode = cityCodes[countryCode];
                    var avatar = 1;
                    string name = "";
                    string surname = "";
                    if (userSex == 0)
                    {
                        name = getRandomTitle(wnames);
                        surname = getRandomTitle(wsurNames);
                        avatar = 1003;
                        userSearchDatingSex = 1;
                    }
                    else
                    {
                        name = getRandomTitle(names);
                        surname = getRandomTitle(surNames);
                        avatar = 1008;
                        userSearchDatingSex = 0;
                    }

                    var user = new User()
                    {

                        AvatarId = avatar,
                        Login = string.Format("login0{0}", i),
                        UserStatus = 1,
                        Settings = new UserSettings()
                        {
                            Email = "alter.22.04@gmail.com",
                            NotificationType = Context.Dto.Settings.NotificationTypes.Never,
                            PasswordHash = "a1bd4e0efc7ce8bd1d63433a0baa87e3a486fbfe2729d73d1dbf7d2822d201ee8726c6d94da1f09f1a53554e440ad6041ecab545b2085dc28c6f6849f0fcea23",
                        },

                        UserFullInfo = new UserInfo
                        {
                            HelloMessage = "Всем привет, я на связи!",
                            DateOfBirth = DateTime.Now.AddYears(-rnd.Next(17, 88)),
                            Name = name,
                            SurName = surname,
                            NowCountryCode = countryCode,
                            NowCityCode = cityCode,
                            Sex = userSex,
                            DatingPurpose = userDatingPurpose,
                            userDatingSex = userSearchDatingSex,
                            userDatingAge = userDatingAge,
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

        private static int getRandomIndex(int min, int max)
        {
            return rnd.Next(min, max + 1);
        }
    }
}