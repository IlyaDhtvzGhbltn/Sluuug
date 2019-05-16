﻿using Context;
using Slug.Context.Dto.Search;
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

        [Required]
        public SexEnum Sex { get; set; }

        public int NowCountryCode { get; set; }

        public int NowSityCode { get; set; }

        public PrivateStatus PrivateStatus { get; set; }

        public virtual List<Education> Educations { get; set; }

        public virtual List<LifePlaces> Places { get; set; }

        public virtual List<MemorableEvents> Events { get; set; }

        public virtual List<WorkPlaces> Works { get; set; }
    }
}