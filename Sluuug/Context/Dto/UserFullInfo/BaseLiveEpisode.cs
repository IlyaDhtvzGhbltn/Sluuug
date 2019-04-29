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
        [Range(1,2000)]
        public int CountryCode { get; set; }

        [Required]
        [Range(1, 10000)]
        public string SityCode { get; set; }

        [Required]
        public bool UntilNow { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
        
        [MaxLength(500)]
        public string Comment { get; set; }

        [Range(1,5)]
        public int PersonalRating { get; set; }
    }
}