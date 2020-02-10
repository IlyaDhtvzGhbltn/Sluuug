using System;


namespace SharedModels.Users.Registration
{
    public class BaseRegistrationModel
    {
        public int Sex { get; set; }
        public int CountryCode { get; set; }
        public int CityCode { get; set; }
        public string CityTitle { get; set; }
        public string CountryTitle { get; set; }
        public RegistrationTypeService RegistrationType { get; set; }
        public DateTime DateBirth { get; set; }
    }
}