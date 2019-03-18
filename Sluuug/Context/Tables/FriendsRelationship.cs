using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Slug.Context.Tables
{
    public class FriendsRelationship
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OfferSendedDate { get; set; }

        [Required]
        public long UserSender { get; set; }

        [Required]
        public long UserConfirmer { get; set; }

        [Required]
        public bool Accepted { get; set; }
    }
}