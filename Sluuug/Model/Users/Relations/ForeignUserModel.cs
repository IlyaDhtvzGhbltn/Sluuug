﻿using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class ForeignUserModel : BaseUser
    {
        public string HateMessage { get; set; }
    }
}