using Slug.Context.Dto.UserFullInfo;
using Slug.Context.Tables;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class FullUserInfoModel : CutUserInfoModel
    {
        public List<EducationModel> Educations { get; set; }
        public List<MemorableEventsModel> Events { get; set; }
        public List<LifePlacesModel> Places { get; set; }
        public List<WorkPlacesModel> Works { get; set; }

        public List<AlbumModel> Albums { get; set; }
    }
}