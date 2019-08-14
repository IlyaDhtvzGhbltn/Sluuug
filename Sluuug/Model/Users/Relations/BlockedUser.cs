using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users.Relations
{
    public class BlockedUser : BaseUser
    {
        public string HateMessage { get; set; }

        public DateTime BlockDate { get; set; }
    }
}