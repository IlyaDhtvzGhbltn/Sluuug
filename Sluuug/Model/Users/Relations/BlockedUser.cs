using System;
using SharedModels.Users;


namespace Slug.Model.Users.Relations
{
    public class BlockedUser : BaseUser
    {
        public string HateMessage { get; set; }

        public DateTime BlockDate { get; set; }
    }
}