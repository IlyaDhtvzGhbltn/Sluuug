using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class WorkPlacesModel : UserInfoItem
    {
        public string CompanyTitle { get; set; }

        public string Position { get; set; }
    }
}