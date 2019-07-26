using Slug.Context.Dto.UserFullInfo;
using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class EducationModel : UserInfoItem
    {
        public EducationTypes EducationType { get; set; }

        public string Title { get; set; }

        //public string Faculty { get; set; }

        public string Specialty { get; set; }

    }
}