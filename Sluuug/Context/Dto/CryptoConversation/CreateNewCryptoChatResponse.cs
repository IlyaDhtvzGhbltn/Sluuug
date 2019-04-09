using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.CryptoConversation
{
    public class CreateNewCryptoChatResponse
    {
        public Guid CryptoGuidId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}