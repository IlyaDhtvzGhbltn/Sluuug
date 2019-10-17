using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class VkRegisteringUserModel
    {
        public long VkId { get; set; }
        public string Vk200Avatar { get; set; }
        public string Vk100Avatar { get; set; }
        public string Vk50Avatar { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Sex { get; set; }
        public int CountryCode { get; set; }
        public int CityCode { get; set; }
        public DateTime DateBirth { get; set; }
        public string Status { get; set; }
    }
}