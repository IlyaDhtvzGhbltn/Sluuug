using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserFullInfo
{
    public class WorkPlaces : BaseLiveEpisode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CompanyTitle { get; set; }

        [Required]
        public string Position { get; set; }
    }
}