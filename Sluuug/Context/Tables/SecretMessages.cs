using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class SecretMessages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(36)]
        [Index(IsUnique = true)]
        public string PartyId { get; set; }

        [Required]
        public int UserSender { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SendingDate { get; set; }
    }
}