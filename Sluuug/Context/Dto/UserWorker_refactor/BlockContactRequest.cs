using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserWorker_refactor
{
    public class BlockContactRequest
    {
        public int BlockedUserId { get; set; }

        public string HateMessage { get; set; }
    }
}