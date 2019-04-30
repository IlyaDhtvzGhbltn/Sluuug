using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CutUserInfoModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public DateTime DateBirth { get; set; }

        public string Country { get; set; }

        public string Sity { get; set; }

        public string AvatarUri { get; set; }

        public int FullAges { get; set; }
    }
}