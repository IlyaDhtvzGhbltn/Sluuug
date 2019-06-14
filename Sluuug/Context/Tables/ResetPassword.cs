using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class ResetPassword
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserRequestedId { get; set; }

        [Required]
        [DataType( DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(120)]
        public string ResetParameter { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateRequestDate { get; set; }

        [Required]
        public bool IsExpired { get; set; }
    }
}