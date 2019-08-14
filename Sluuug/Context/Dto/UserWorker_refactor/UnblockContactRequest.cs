using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserWorker_refactor
{
    public class UnblockContactRequest
    {
        public Guid EntriId { get; set; }

        public int UserNeedUnblockId { get; set; }
    }
}