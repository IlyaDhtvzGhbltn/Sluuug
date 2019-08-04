using Slug.Context.Dto;
using Slug.Model.Albums;
using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users.Relations
{
    public class MyProfileModel : BaseUser
    {
        public ICollection<AlbumModel> Albums { get; set; }
        public ICollection<EducationModel> Educations { get; set; }
        public ICollection<WorkPlacesModel> Works { get; set; }

        public ICollection<MemorableEventsModel> Events { get; set; }
        public ICollection<PostModel> Posts { get; set; }
    }
}