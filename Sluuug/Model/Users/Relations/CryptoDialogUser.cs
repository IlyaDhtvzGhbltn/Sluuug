using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users.Relations
{
    public class CryptoDialogUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsOnline { get; set; }
        public bool Vip { get; set; }
        public bool AvaliableToVipContact { get; set; }
        public string LargeAvatar { get; set; }
    }
}