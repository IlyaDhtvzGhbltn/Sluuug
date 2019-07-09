using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CryptoChatModel
    {
        public List<CryptoChat> CurrentChats { get; set; }
        public List<CryptoChat> SelfCreatedChats { get; set; }
        public List<CryptoChat> AcceptNeeded { get; set; }
        public List<FriendModel> FriendsICanInvite { get; set; }
    }

    public class CryptoChat
    {
        public Guid GuidId { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }

        public int RemainingMins { get; set; }

        public int RemainingSecs { get; set; }


        public string InterlocutorAvatar { get; set; }

        public string InterlocutorName { get; set; }

        public string InterlocutorSurName { get; set; }


        public string UserLastMessageSenderAvatar { get; set; }

        public string LastMessage { get; set; }

        public DateTime LastMessageSendDate { get; set; }


        public bool Expired { get; set; }
    }
}