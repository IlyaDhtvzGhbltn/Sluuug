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
        [Index(IsUnique = true)]
        public Guid GuidId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public string Offer { get; set; }

        public string Answer { get; set; }

        [Required]
        public int ConferenceCreatorUserId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}