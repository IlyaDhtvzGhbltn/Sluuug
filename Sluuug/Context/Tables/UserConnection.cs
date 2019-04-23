using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class UserConnection
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid ConnectionID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public bool ConnectionActiveStatus { get; set; }

        [Required]
        public DateTime ConnectionTime { get; set; }

        public DateTime? ConnectionOff { get; set; }
    }
}