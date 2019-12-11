using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserFullInfo
{
    public class BaseLiveEpisode
    {
        [Required]
        public int CountryCode { get; set; }

        [Required]
        public int CityCode { get; set; }

        [Required]
        public bool UntilNow { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End { get; set; }
        
        [MaxLength(500)]
        public string Comment { get; set; }

        [Range(0,5)]
        public int PersonalRating { get; set; }
    }
}