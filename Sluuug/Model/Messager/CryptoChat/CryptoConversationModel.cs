using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Messager.CryptoChat
{
    public class CryptoConversationModel : ConversationBaseModel
    {
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int RemainingMins { get; set; }
        public int RemainingSecs { get; set; }
        public bool Expired { get; set; }
        public bool InterlocutorVIP { get; set; }
    }
}