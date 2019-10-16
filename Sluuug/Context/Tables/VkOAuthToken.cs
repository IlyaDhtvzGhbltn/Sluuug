using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class VkOAuthToken
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string Code { get; set; }

        [Required]
        [MaxLength(85)]
        public string Token { get; set; }

        [Required]
        public long VkUserId { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [Required]
        public bool IsExpired { get; set; }
    }
}