using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserFullInfo
{
    public class MemorableEvents : EntryBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string EventComment { get; set; }

        public DateTime? DateEvent { get; set; }

        public virtual User User { get; set; }

    }
}