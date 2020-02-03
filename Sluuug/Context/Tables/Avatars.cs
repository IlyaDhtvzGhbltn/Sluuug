using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedModels.Enums;


namespace Slug.Context.Tables
{
    public class Avatars
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid GuidId { get; set; }

        [Required]
        [DataType( DataType.DateTime )]
        public DateTime UploadTime { get; set; }

        [Required]
        public bool IsStandart { get; set; }

        public int? CountryCode { get; set; }

        [Required]
        public string LargeAvatar { get; set; }

        public string MediumAvatar { get; set; }

        public string SmallAvatar { get; set; }

        [Required]
        public AvatarTypesEnum AvatarType { get; set; }
    }
}