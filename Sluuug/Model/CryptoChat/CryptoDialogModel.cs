using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CryptoDialogModel
    {
        public Guid GuidId { get; set; }

        public List<CryptoMessage> Messages { get; set; }
    }

    public class CryptoMessage
    {
        public string AvatatURI { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Text { get; set; }

        public DateTime SendDate { get; set; }
    }
}