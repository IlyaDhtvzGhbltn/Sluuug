using SharedModels.Enums;
using Slug.Model.FullInfo;

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