using Context;
using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class CryptoChatWorker
    {
        private readonly IDictionary<CryptoChatType, TimeSpan> CryptoChatDestroyDates =
            new Dictionary<CryptoChatType, TimeSpan>()
            {
                { CryptoChatType.Hour, new TimeSpan().Add(new TimeSpan(1,0,0)) },
                { CryptoChatType.Hours_6, new TimeSpan().Add(new TimeSpan(6,0,0)) },
                { CryptoChatType.Day, new TimeSpan().Add(new TimeSpan(1,0,0,0)) },
                { CryptoChatType.Month, new TimeSpan().Add(new TimeSpan(30,0,0,0,0)) },
                { CryptoChatType.Year, new TimeSpan().Add(new TimeSpan(365,0,0,0,0)) },

            };

        public Guid CreateNewCryptoChat(CryptoChatType type, List<string> UserIds, int Inviter)
        {
            var newSecretChat = new SecretChat();
            newSecretChat.Create = DateTime.Now;
            newSecretChat.Destroy = DateTime.Now.Add( CryptoChatDestroyDates[type] );
            newSecretChat.PartyGuid = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                context.SecretChat.Add(newSecretChat);
                context.SaveChanges();
                foreach (var userInvited in UserIds)
                {
                    int userId = Convert.ToInt32(userInvited);
                    var secretGroups = new SecretChatGroup();
                    secretGroups.PartyGUID = newSecretChat.PartyGuid;
                    secretGroups.UserId = userId;
                    context.SecretChatGroup.Add(secretGroups);
                }
                var secretGroup = new SecretChatGroup();
                secretGroup.PartyGUID = newSecretChat.PartyGuid;
                secretGroup.UserId = Inviter;
                context.SecretChatGroup.Add(secretGroup);
                context.SaveChanges();

                return newSecretChat.PartyGuid;
            }
        }
    }
}