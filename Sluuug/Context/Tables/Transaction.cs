using System;
using SharedModels.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Slug.Context.Tables
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public Guid InternalId { get; set; }
        [Required]
        [StringLength(120)]
        public string Session { get; set; }
        [Required]
        public VIPstatusTypes Type { get; set; }
        public virtual User PaySender { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public bool IsPaid { get; set; }
        public string Properties { get; set; }
    }
}