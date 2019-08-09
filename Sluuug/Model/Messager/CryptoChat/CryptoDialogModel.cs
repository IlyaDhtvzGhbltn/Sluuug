using Slug.Model.Messager.CryptoChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model
{
    public class CryptoDialogModel
    {
        public Guid GuidId { get; set; }
        public int PagesCount { get; set; }
        public List<CryptoMessageModel> Messages { get; set; }
        public bool Expired { get; set; }
    }

}