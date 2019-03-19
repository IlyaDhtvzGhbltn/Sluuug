using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{

    public class MyFriendsModel
    {
        public ICollection<FriendModel> Friends { get; set; }
    }


    public class FriendModel
    {
        public int UserId { get; set; }

        public string AvatarPath { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }
    }
}