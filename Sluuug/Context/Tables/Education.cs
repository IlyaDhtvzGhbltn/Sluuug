using SharedModels.Enums;
using Slug.Context.Tables;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slug.Context.Dto.UserFullInfo
{
    public class Education : BaseLiveEpisode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public EducationTypes EducationType { get; set; }

        [Required]
        public string Title { get; set; }

        public string Faculty { get; set; }

        public string Specialty { get; set; }

        public virtual User User { get; set; }

    }
}