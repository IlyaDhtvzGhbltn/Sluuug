using Context;
using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly static string[] statuses = new string[] 
        {
            "Привет",
            "Чертовски скучно сегодня",
            "Завтра еду в Москву",
            "Есть кто из Хабаровска ?",
            "__Завтра будет хуже____",
            "Меня трудно найти и тд"
        }; 

        private readonly static DatingPurposeEnum[] DatingPurposes = new DatingPurposeEnum[] { DatingPurposeEnum.Communication, DatingPurposeEnum.SeriousRelationship, DatingPurposeEnum.Sex };
        private readonly static AgeEnum[] UserSearchAge = new AgeEnum[] { AgeEnum.from16to20, AgeEnum.from21to26, AgeEnum.from27to32, AgeEnum.from33to40, AgeEnum.from41to49, AgeEnum.from50to59, AgeEnum.from60to69, AgeEnum.morethan70 };
        private static Random rnd = new Random((int)DateTime.Now.Ticks);


        public static void AddBots(DataBaseContext context = null)
        {
            string[] filesNames = Directory.GetFiles(@"D:\MyUtilits\friendlynet\Sluuug\Resources\tmpUsers").Select(Path.GetFileName).ToArray();
            string fullAvatarsUrlTemplate = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1573744001/UploadUsers/";
            var fullAvatars = new List<string>();
            foreach (string name in filesNames)
            {
                fullAvatars.Add(string.Format("{0}{1}", fullAvatarsUrlTemplate, name));
            }
        }
    }
}
