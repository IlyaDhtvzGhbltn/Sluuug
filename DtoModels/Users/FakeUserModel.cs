using SharedModels.Enums;
using SharedModels.Users.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Users
{
    public class FakeUserModel
    {
        public RegistrationTypeService UserType { get; set; }
        public AvatarTypesEnum AvatarType { get; set; }
        public string RemoteId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
        public DateTime DateBirth { get; set; }
        public string HelloMessage { get; set; }
        public string LargeAvatar { get; set; }
        public string MediumAvatar { get; set; }
        public string SmallAvatar { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public SexEnum SexCode { get; set; }
        public SexEnum userSearchSex { get; set; }
        public AgeEnum userSearchAge { get; set; }
        public DatingPurposeEnum purpose { get; set; }
        public bool Vip { get; set; }
    }
}
