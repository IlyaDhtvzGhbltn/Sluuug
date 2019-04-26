using Context;
using Slug.Context.Dto.UserFullInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class UserInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string SurName { get; set; }

        public string NowCountry { get; set; }

        public string NowSity { get; set; }

        public string NowMetroStation { get; set; }

        public PrivateStatus PrivateStatus { get; set; }

        public List<Education> Educations { get; set; }

        public List<LifePlaces> Places { get; set; }

        public List<MemorableEvents> Events { get; set; }

        public List<WorkPlaces> Works { get; set; }

    }
}