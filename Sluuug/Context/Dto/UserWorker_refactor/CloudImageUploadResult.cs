using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.UserWorker
{
    public class CloudImageUploadResult
    {
        [JsonProperty("public_id")]
        public string PublicId { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("resource_type")]
        public string ResourceType { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("bytes")]
        public long Bytes { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("placeholder")]
        public bool Placeholder { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("secure_url")]
        public Uri SecureUrl { get; set; }

        [JsonProperty("original_filename")]
        public string OriginalFilename { get; set; }
    }

}