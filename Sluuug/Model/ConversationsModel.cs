﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class ConversationsModel
    {
        public ICollection<CutConversation> Cnv { get; set; }
    }

    public class CutConversation
    {
        public int Id { get; set; }

        public string InterlocutorName { get; set; }

        public string InterlocutorSurName { get; set; }

        public string AvatarPath { get; set; }

        public string LastMessage { get; set; }
    }
}