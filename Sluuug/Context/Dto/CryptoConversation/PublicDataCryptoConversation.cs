using Context;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.CryptoConversation
{
    public class PublicDataCryptoConversation
    {
        [JsonProperty(PropertyName = "convGuidId")]
        public Guid ConvGuidId { get; set; }
        public string p { get; set; }
        public string g { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "expireDate")]
        public DateTime ExpireDate { get; set; }


        [JsonProperty(PropertyName = "participants")]
        public List<Participant> Participants { get; set; }

        [JsonProperty(PropertyName = "type")]
        public CryptoChatType Type { get; set; }

        [JsonProperty(PropertyName = "creatorUserId")]
        public int CreatorUserId { get; set; }

        [JsonProperty(PropertyName = "creatorAvatar")]
        public string CreatorAvatar { get; set; }

        [JsonProperty(PropertyName = "creatorName")]
        public string CreatorName { get; set; }
    }
}