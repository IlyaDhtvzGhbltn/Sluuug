using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.OAuth
{

    public class VkResponce
    {
        [JsonProperty("response")]
        public List<VkUserInfo> UserInfo { get; set; }
    }

    public class VkUserInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("bdate")]
        public string Bdate { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("country")]
        public City Country { get; set; }

        [JsonProperty("photo_200_orig")]
        public string Photo200_Orig { get; set; }

        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }

        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

}