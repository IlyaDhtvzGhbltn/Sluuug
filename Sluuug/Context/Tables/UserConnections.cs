using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class UserConnections
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int Id { get; set; }

        [Required]
        public Guid ConnectionId { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string CultureCode { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime UpdateTime { get; set; }

        public DateTime? ConnectionOff { get; set; }
    }
}