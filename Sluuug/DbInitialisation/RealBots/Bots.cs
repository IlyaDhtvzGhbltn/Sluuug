using Context;
using Slug.Context.Dto.Search;
using Slug.Context.Tables;
using Slug.ImageEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Users;
using SharedModels.Enums;

namespace FN.Bot
{
    public static class Bots
    {
        private readonly static string[] mnames = new string[]
        {
            "Иван", "Андрей", "Дима", "Илья", "Аркадий", "Алексей", "Петр", "Василий", "Женя", "Вася", "Николай", "Борис",
            "Генадий", "Евгений", "Зураб", "Коля", "Леонид", "Лёня", "Марик", "Носок", "Рустам", "Саня", "Толик", "Андрейс"
        };
        private readonly static string[] msurNames = new string[]
        {
            "Володин", "Пригожин", "Фролов", "Карпов", "Навальный", "Иванов", "Петров", "Сашинский", "Проъодько", "Адамовичус",
            "Панчин", "Гаврилов", "Николаев", "Борисов", "Кузьминков", "Стариков", "Рогов", "Андреев", "Богдан", "Богданов",
            "Хомчак", "Приходько", "Фомин", "Фоменко", "Агапов", "Галкин", "Пугачев", "Семенченко", "Наумов", "Назаров",
            "Павлов", "Павленко", "Ленкевичус", "Строгий", "Дзержинский", "__Андриенко__", "Соловьев", "Мамонтов", "Василевский"
        };
        private readonly static string[] wnames = new string[]
        {
            "Ирина", "Ксения", "Олеся", "Настя", "Аня", "Алена", "Анфиса", "Ольга", "Варвара", "Вера", "Галя", "Дина", "Даша",
            "Ксюша", "Лена", "Леночка", "Маруся", "Маша", "Марина", "Ника", "Рита", "Риточка", "Света", "Светлана", "Таиссия"
        };
        private readonly static string[] wsurNames = new string[]
        {
            "Володина", "Пригожина", "Фролова", "Карпова", "Фиртешева", "Андреева", "Васильева", "Василевская", "Рождественская",
            "Пугачева", "Алексеева", "Борисова", "Воронина", "Варваровна", "Грязнова", "Димидова", "Дмитриева", "Дмитриченко",
            "Козловская", "Шевченко", "Щукина", "Харитонова", "Касперская", "Полякова", "Смирнова", "Соловьева", "Фоменко"
        };

        private readonly static int[] countryCodes = new int[] { 7, 375, 380 };
        private readonly static Dictionary<int, int[]> cityCodesByCountryKey = new Dictionary<int, int[]>
        {
            { 7, new int[] { 495, 812, 3832, 8312, 8432, 3512, 3812, 8462, 3472, 3912, 732, 3422, 8442 } },
            { 380, new int[] { 44, 57, 48, 6122, 32, 512, 382 } },
            { 375, new int[] { 17, 162, 1522, 2322, 212, 222, 225 } }
        };

        private readonly static string standartStatus = "Всем привет, я на связи!";

        private readonly static DatingPurposeEnum[] DatingPurposes = new DatingPurposeEnum[] { DatingPurposeEnum.Communication, DatingPurposeEnum.SeriousRelationship, DatingPurposeEnum.Sex };
        private readonly static AgeEnum[] UserSearchAge = new AgeEnum[] { AgeEnum.from16to20, AgeEnum.from21to26, AgeEnum.from27to32, AgeEnum.from33to40, AgeEnum.from41to49, AgeEnum.from50to59, AgeEnum.from60to69, AgeEnum.morethan70 };


        private static Random rnd = new Random((int)DateTime.Now.Ticks);

        //private static Dictionary<string, Action<string>> avatarBot = new Dictionary<string, Action<string>>()
        //{     };

        public static void AddBots()
        {
            string[] filesNames = Directory.GetFiles(@"D:\MyUtilits\friendlynet\Slug\Resources\tmpUsers").Select(Path.GetFileName).ToArray();
            string fullAvatarsUrlTemplate = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1573744001/UploadUsers/";

            var usersCollection = new List<User>();

            using (var context = new DataBaseContext())
            {
                foreach (string name in filesNames)
                {
                    BotTypeEnum botType = parceBotType(name);
                    string fullAvatarUrl = string.Format("{0}{1}", fullAvatarsUrlTemplate, name);
                    string mediumAvatarUrl = Resize.ResizedAvatarUri(fullAvatarUrl, ModTypes.c_scale, 100, 100);
                    string smallAvatarUrl = Resize.ResizedAvatarUri(fullAvatarUrl, ModTypes.c_scale, 50, 50);

                    Avatars userAvatar = new Avatars()
                    {
                        UploadTime = DateTime.Now,
                        IsStandart = true,
                        LargeAvatar = fullAvatarUrl,
                        MediumAvatar = mediumAvatarUrl,
                        SmallAvatar = smallAvatarUrl
                    };
                    context.Avatars.Add(userAvatar);
                    context.SaveChanges();
                    int avatarId = context.Avatars.First(x => x.LargeAvatar == fullAvatarUrl).Id;
                    SexEnum userSex = SexEnum.man;

                    UserInfo userInfo = new UserInfo();
                    if ((int)botType >= 20 && (int)botType <= 28)
                        userSex = SexEnum.woman;
                    if ((int)botType >= 10 && (int)botType <= 18)
                        userSex = SexEnum.man;



                    var user = new User()
                    {
                        UserFullInfo = userInfo,
                        AvatarId = avatarId,
                        Login = string.Format("login0{0}", 1),
                        UserStatus = 1,
                        Settings = new UserSettings()
                        {
                            Email = "test@gmail.com",
                            NotificationType = Slug.Context.Dto.Settings.NotificationTypes.Never,
                            PasswordHash = "a1bd4e0efc7ce8bd1d63433a0baa87e3a486fbfe2729d73d1dbf7d2822d201ee8726c6d94da1f09f1a53554e440ad6041ecab545b2085dc28c6f6849f0fcea23",
                        }
                    };



                }
            }
        }

        private static BotTypeEnum parceBotType(string fileName)
        {
            if (fileName[0] == 'b' && fileName[2] == 'g')
                return BotTypeEnum.BotGirl;
            if (fileName[0] == 'b' && fileName[2] == 'm')
                return BotTypeEnum.BotBoy;
            if (fileName[0] == 'm')
            {
                string age = fileName.Substring(2, 2);
                if (age == "16")
                    return BotTypeEnum.RealBoy16;
                if (age == "21")
                    return BotTypeEnum.RealBoy21;
                if (age == "27")
                    return BotTypeEnum.RealBoy27;
                if (age == "33")
                    return BotTypeEnum.RealBoy33;
                if (age == "41")
                    return BotTypeEnum.RealBoy41;
                if (age == "50")
                    return BotTypeEnum.RealBoy50;
                if (age == "60")
                    return BotTypeEnum.RealBoy60;
                if (age == "70")
                    return BotTypeEnum.RealBoy70;
            }
            if (fileName[0] == 'w')
            {
                string age = fileName.Substring(2, 2);
                if (age == "16")
                    return BotTypeEnum.RealGir16;
                if (age == "21")
                    return BotTypeEnum.RealGir21;
                if (age == "27")
                    return BotTypeEnum.RealGir27;
                if (age == "33")
                    return BotTypeEnum.RealGir33;
                if (age == "41")
                    return BotTypeEnum.RealGir41;
                if (age == "50")
                    return BotTypeEnum.RealGir50;
                if (age == "60")
                    return BotTypeEnum.RealGir60;
                if (age == "70")
                    return BotTypeEnum.RealGir70;
            }

            return BotTypeEnum.Random;
        }

        private static void RegisterReal(BotTypeEnum type, string avatarUrl)
        {

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
