﻿using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class BaseUser
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public int Age { get; set; }

        public string HelloMessage { get; set; }

        public string AvatarResizeUri { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Sex { get; set; }

        public int SexCode { get; set; }

        public SexEnum userSearchSex { get; set; }

        public AgeEnum userSearchAge { get; set; }

        public DatingPurposeEnum purpose { get; set; }
    }
}