using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedModels.Enums;
using SharedModels.Users.Registration;

namespace Slug.Context.Tables
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public int UserStatus { get; set; }
        public Guid AvatarGuidId { get; set; }
        public RegistrationTypeService UserType { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? ReferalUserId { get; set; }
        public bool IsFakeBot { get; set; }

        public virtual UserInfo UserFullInfo { get; set; }
        public virtual UserSettings Settings { get; set; }

    }
}