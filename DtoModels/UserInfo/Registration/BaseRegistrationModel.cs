using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Registration
{
    public class BaseRegistrationModel
    {
        public int Sex { get; set; }
        public int CountryCode { get; set; }
        public int CityCode { get; set; }
        public DateTime DateBirth { get; set; }
    }
}