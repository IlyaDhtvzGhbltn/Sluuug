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

        public List<FriendModel> Users { get; set; }

        public DateTime OpenDate { get; set; }

        public bool ActiveStatus { get; set; }

        public bool Expired { get; set; }
    }
}