using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class FriendModel
    {
        public int UserId { get; set; }

        public string AvatarPath { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public UserOnlineStatus Status { get; set; }
    }
}