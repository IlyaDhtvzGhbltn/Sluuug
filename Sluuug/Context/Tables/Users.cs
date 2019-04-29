using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slug.Context.Tables
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public int CountryCode { get; set; }

        [Required]
        public int UserStatus { get; set; }

        public int? AvatarId { get; set; }

        public virtual UserInfo UserFullInfo { get; set; }

        public virtual UserSettings Settings { get; set; }

    }
}