using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharedModels.Users;

namespace Slug.Model.Users
{

    public class ContactsModel
    {
        public ICollection<BaseUser> Friends { get; set; }

        public ICollection<BaseUser> IncommingInvitations { get; set; }

        public ICollection<BaseUser> OutCommingInvitations { get; set; }

        public ICollection<BlockedUser> BlockedUser { get; set; }
    }
}