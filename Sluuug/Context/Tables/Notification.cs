using Slug.Context.Dto.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        [Required]
        public int UserRecipient { get; set; }

        [Required]
        public int UserSender { get; set; }

        public string Message { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}