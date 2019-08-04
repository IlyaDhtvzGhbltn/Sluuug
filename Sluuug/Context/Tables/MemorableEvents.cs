using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto
{
    public class ImportantEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid AlbumGuid { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public DateTime DateEvent { get; set; }

        [Required]
        public virtual User User { get; set; }

        public string TextEventDescription { get; set; }

        public virtual List<Foto> Photos { get; set; } 
    }
}