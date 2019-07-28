using Slug.Context.Dto.FeedBack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class Feedback
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public FeedbackSubjectEnums Subject { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(1000)]
        public string Message { get; set; }

        [DataType(DataType.EmailAddress)]
        public string BackEmeil { get; set; }



        [Range(-1, 99999999)]
        public int? UserID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}