using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Slug.Context.Tables
{
    public class Avatars
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType( DataType.DateTime )]
        public DateTime UploadTime { get; set; }

        [Required]
        public bool IsStandart { get; set; }

        public int? CountryCode { get; set; }

        [Required]
        public string ImgPath { get; set; }
    }
}