using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth.Ok
{
    public class OkUserInfo
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }
        [JsonProperty("birthday")]
        public string Birthday { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("birthdaySet")]
        public bool BirthdaySet { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("has_email")]
        public string HasEmail { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("online")]
        public string Online { get; set; }
        [JsonProperty("photo_id")]
        public string PhotoId { get; set; }

        [JsonProperty("pic_1")]
        public string SmallAvatar { get; set; }
        [JsonProperty("pic_2")]
        public string MediumAvatar { get; set; }
        [JsonProperty("pic_3")]
        public string LargeAvatar { get; set; }
    }

    public class Location
    {
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("countryName")]
        public string CountryName { get; set; }
    }
}