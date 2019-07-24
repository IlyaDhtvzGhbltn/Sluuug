using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{

    public class MyFriendsModel
    {
        public ICollection<FriendModel> Friends { get; set; }

        public ICollection<FriendModel> IncommingInvitations { get; set; }

        public ICollection<FriendModel> OutCommingInvitations { get; set; }
    }
}