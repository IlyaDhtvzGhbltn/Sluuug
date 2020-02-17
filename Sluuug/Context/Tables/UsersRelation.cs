using Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedModels.Enums;


namespace Slug.Context.Tables
{
    public class UsersRelation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OfferSendedDate { get; set; }
        [Required]
        public FriendshipItemStatus Status { get; set; }

        public virtual User UserOferFrienshipSender { get; set; }
        public virtual User UserConfirmer { get; set; }

        public bool IsInvitationSeen { get; set; }
        public Guid BlockEntrie { get; set; }
    }
}