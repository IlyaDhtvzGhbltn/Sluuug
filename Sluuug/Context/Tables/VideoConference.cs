using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class VideoConference
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid GuidId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public string Offer { get; set; }

        public string Answer { get; set; }

        [Required]
        public int OfferSenderUser { get; set; }

        [Required]
        public int AnswerSenderUser { get; set; }

        [Required]
        public int ConferenceCreatorUserId { get; set; }
    }
}