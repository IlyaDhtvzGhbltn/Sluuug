using Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Countries
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(1, 10000)]
        public int CountryCode { get; set; }

        [Required]
        [Range(1, 10000)]
        public LanguageType Language { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}