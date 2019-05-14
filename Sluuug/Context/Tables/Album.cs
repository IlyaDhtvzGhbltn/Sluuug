using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Album
    {
        [Key, DatabaseGenerated( DatabaseGeneratedOption.None )]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int CreateUserID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [MaxLength(5000)]
        public string AuthorComment { get; set; }

        [Required]
        public string AlbumLabelUrl { get; set; }

        public virtual List<Foto> Fotos { get; set; }

        public virtual User User { get; set; }
    }
}