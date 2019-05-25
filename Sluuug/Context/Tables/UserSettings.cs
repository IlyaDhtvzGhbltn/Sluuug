using Slug.Context.Dto.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class UserSettings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public NotificationTypes NotificationType { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType( DataType.EmailAddress )]
        public string Email { get; set; }

        [Required]
        [DataType( DataType.Password )]
        public string PasswordHash { get; set; }

        [Required]
        public bool QuickMessage { get; set; }
    }
}