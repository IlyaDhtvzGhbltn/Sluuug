using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid ConvarsationGuidId { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(5000)]
        public string Text { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime SendingDate { get; set; }
        [Required]
        public bool IsReaded { get; set; }
    }
}