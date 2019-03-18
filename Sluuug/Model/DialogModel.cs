using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class DialogModel
    {
        public int DialogId { get; set; }

        public ICollection<DialogMessage> Messages { get; set; }
    }

    public class DialogMessage
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string AvatarPath { get; set; }

        public string Text { get; set; }

        public string SendTime { get; set; }
    }
}