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
        public string Password { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(2), MaxLength(20)]
        public string ForName { get; set; }

        [Required]
        public int CountryCode { get; set; }

        public string Sity { get; set; }

        public string MetroStation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public int UserStatus { get; set; }

        public int? AvatarId { get; set; }
    }
}