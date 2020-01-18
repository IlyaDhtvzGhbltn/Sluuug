using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slug.Model.Registration
{
    public class RegisteringUserModel : BaseRegistrationModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}