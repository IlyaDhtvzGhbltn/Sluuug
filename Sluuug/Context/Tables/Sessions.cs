using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Slug.Context.Tables
{
    public class Session
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Number { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public bool Expired { get; set; }

        public int? UserId { get; set; }
    }
}