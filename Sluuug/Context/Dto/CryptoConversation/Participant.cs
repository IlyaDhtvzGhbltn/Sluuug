using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.CryptoConversation
{
    public class Participant
    {
        public int UserId { get; set; }
        public string PublicKey { get; set; }
    }
}