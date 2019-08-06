﻿using Slug.Context.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Slug.Context.Tables
{
    public class PostComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserCommenter { get; set; }

        [Required]
        [MaxLength(5000)]
        public string CommentText { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CommentWriteDate { get; set; }

        public virtual Post Post { get; set; }
    }
}