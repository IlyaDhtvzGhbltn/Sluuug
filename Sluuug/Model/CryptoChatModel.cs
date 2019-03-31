using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CryptoChatModel
    {
        public List<cryptoChat> CurrentChats { get; set; }

        public List<FriendModel> FriendsICanInvite { get; set; }
    }

    public class cryptoChat
    {
        public int Id { get; set; }

        public List<FriendModel> Users { get; set; }

        public DateTime OpenDate { get; set; }

        public bool ActiveStatus { get; set; }
    }
}