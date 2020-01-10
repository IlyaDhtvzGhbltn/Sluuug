using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto
{
    public class VipResponce
    {
        public List<VipUserModel> Users { get; set; }
        public string City { get; set; }
        public bool AlreadyVIP { get; set; }
    }
}