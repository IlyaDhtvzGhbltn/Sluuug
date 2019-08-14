using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class BlockedUsersEntries
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid BlockEntryId { get; set; }

        [Required]
        public int UserBlocker { get; set; }

        [Required]
        public int UserBlocked { get; set; }

        [Required]
        public DateTime BlockDate { get; set; }

        public string HateMessage { get; set; }
    }
}