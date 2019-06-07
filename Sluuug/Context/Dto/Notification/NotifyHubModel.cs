using Slug.Context.Dto.CryptoConversation;
using Slug.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Messages
{
    public class NotifyHubModel
    {
        public CutUserInfoModel FromUser { get; set; }

        public IList<string> ConnectionIds { get; set; }

        public string Culture { get; set; }

        public dynamic PublicDataToExcange { get; set; }
    }
}