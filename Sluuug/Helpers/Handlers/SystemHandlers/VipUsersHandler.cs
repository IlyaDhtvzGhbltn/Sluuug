using Context;
using Slug.Context.Dto;
using Slug.Model.Users;
using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedModels.Enums;

namespace Slug.Helpers
{
    public class VipUsersHandler
    {
        public VipResponce VipUsers(UserLocation location, int userId)
        {
            using (var context = new DataBaseContext())
            {
                VipResponce resp = new VipResponce();
                var list = new List<VipUserModel>();
                var query = context.Users
                    .Where(x => x.UserFullInfo.NowCityCode == location.CityCode &&
                            x.UserFullInfo.NowCountryCode == location.CountryCode && 
                            x.UserFullInfo.VipStatusExpiredDate != null &&
                            x.UserFullInfo.VipStatusExpiredDate > DateTime.UtcNow)
                            .ToArray();

                string country = context.Countries
                    .FirstOrDefault(x => x.CountryCode == location.CountryCode && x.Language == LanguageType.Ru)
                    .Title;
                string city = context.Cities
                    .FirstOrDefault(x => x.CitiesCode == location.CityCode && x.Language == LanguageType.Ru)
                    .Title;

                foreach (Context.Tables.User item in query)
                {

                    string avatar = context.Avatars
                        .First(x=>x.Id == item.AvatarId)
                        .MediumAvatar;
                    int age = (DateTime.Now.Year - item.UserFullInfo.DateOfBirth.Year);
                    if (DateTime.Now.DayOfYear < item.UserFullInfo.DateOfBirth.DayOfYear)
                        age--;

                    list.Add(new VipUserModel()
                    {
                        Id = item.Id,
                        Name = item.UserFullInfo.Name,
                        SurName = item.UserFullInfo.SurName,
                        Country = country == null ? "не указано" : country,
                        City = city == null ? "не указано" : city,
                        MidAvatarUri = avatar,
                        DatingPurpose = (DatingPurposeEnum)item.UserFullInfo.DatingPurpose,
                        Age = age
                    });
                }
                resp.Users = list;
                resp.City = city;
                resp.AlreadyVIP = userVipStatus(context, userId);
                return resp;
            }
        }

        public bool SenderAvaliableContact(int userSenderId, int userRecipientId)
        {
            using (var context = new DataBaseContext())
            {
                return senderAvaliableContact(context, userSenderId, userRecipientId);
            }
        }
        public bool SenderAvaliableContact(DataBaseContext context, int userSenderId, int userRecipientId)
        {
            return senderAvaliableContact(context, userSenderId, userRecipientId);
        }

        private bool senderAvaliableContact(DataBaseContext context, int userSenderId, int userRecipientId)
        {
            DateTime? senderExpireVip = context.UsersInfo.First(x => x.Id == userSenderId).VipStatusExpiredDate;
            DateTime? recipientExpireVip = context.UsersInfo.First(x => x.Id == userRecipientId).VipStatusExpiredDate;

            if (senderExpireVip == null && recipientExpireVip != null)
            {
                long recipientLeftVipTime = recipientExpireVip.Value.Ticks - DateTime.UtcNow.Ticks;
                if (recipientLeftVipTime > 0)
                    return false;
            }
            if (senderExpireVip != null && recipientExpireVip != null)
            {
                long leftVipSender = senderExpireVip.Value.Ticks - DateTime.UtcNow.Ticks;
                long leftVipRecipient = recipientExpireVip.Value.Ticks - DateTime.UtcNow.Ticks;

                if (leftVipSender < 0 && leftVipRecipient > 0)
                    return false;
            }
            return true;
        }

        private bool userVipStatus(DataBaseContext context, int userId)
        {
            var userInfo = context.Users.FirstOrDefault(x=>x.Id == userId).UserFullInfo;
            DateTime? vipExpired = userInfo.VipStatusExpiredDate;
            if (userInfo!= null && vipExpired != null)
            {
                if (vipExpired > DateTime.UtcNow)
                    return true;
            }
            return false;
        }
    }
}