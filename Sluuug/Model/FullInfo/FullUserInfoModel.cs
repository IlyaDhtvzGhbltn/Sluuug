using Slug.Context.Dto.UserFullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class FullUserInfoModel : CutUserInfoModel
    {
        public List<EducationModel> Educations { get; set; }
        public List<LifePlacesModel> Places { get; set; }
        public List<MemorableEventsModel> Events { get; set; }
        public List<WorkPlacesModel> Works { get; set; }
    }
}