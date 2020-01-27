using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharedModels.Enums;

namespace Slug.Model.Users.Relations
{
    public class VipUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DatingPurposeEnum DatingPurpose { get; set; }
        public string MidAvatarUri { get; set; }
    }
}