using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class RegisteringUserModel
    {
        public string Login { get; set; }

        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [MinLength(2), MaxLength(20)]
        public string SurName { get; set; }

        public int CountryCode { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }

        public string PasswordHash { get; set; }
    }
}