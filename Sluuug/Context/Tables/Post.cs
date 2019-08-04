using Slug.Context.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto
{
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public User UserPosted { get; set; }
        
        [Required]
        public DateTime PublicDateTime { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public List<PostComment> Comments { get; set; }
    }
}