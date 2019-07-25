using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Foto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid FotoGUID { get; set; }

        [Required]
        public Guid AlbumID { get; set; }

        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }

        [Required]
        public int UploadUserID { get; set; }

        [Required]
        public string ImagePublicID { get; set; }

        public long Height { get; set; }

        public long Width { get; set; }

        public int PositiveRating { get; set; }

        public int NegativeRating { get; set; }

        public string Description { get; set; }

        public virtual List<FotoComment> UserComments { get; set; }

    }
}