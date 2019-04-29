using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class FriendlyUserModel
    {
        public int UserId { get; set; }


        public string Name { get; set; }

        public string SurName { get; set; }

        public string Sity { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string AvatarPath { get; set; }
    }
}