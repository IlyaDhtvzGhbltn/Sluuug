using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserFullInfo
{
    public class Education : BaseLiveEpisode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public EducationTypes EducationType { get; set; }

        [Required]
        public string Title { get; set; }

        public string Faculty { get; set; }

        public string Specialty { get; set; }
    }
}