using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace crosstraining.SerializeDeserialize {

    public class Obligation {
        [JsonProperty(PropertyName = "$title")]
        [JsonPropertyNameAttribute("$title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "statusCode")]
        [JsonPropertyNameAttribute("statusCode")]
        public string StatusCode { get; set; }                  

        [JsonProperty(PropertyName = "$results:headers:Receipt-Signature")]
        [JsonPropertyNameAttribute("$results:headers:Receipt-Signature")]
        public string ReceiptSignature { get; set; }

        [JsonProperty(PropertyName = "$results.headers.Receipt-Signature")]
        [JsonPropertyNameAttribute("$results.headers.Receipt-Signature")]
        public string ReceiptSignature2 { get; set; }

        [JsonProperty(PropertyName = "$results/headers/Receipt-Signature")]
        [JsonPropertyNameAttribute("$results/headers/Receipt-Signature")]
        public string ReceiptSignature3 { get; set; }

        [JsonProperty(PropertyName = "payload")]
        [JsonPropertyNameAttribute ("payload")]
        public List<ObligationItem> ObligationItems { get; set; }

        [JsonProperty(PropertyName = "$results:payload")]
        [JsonPropertyNameAttribute("$results:payload")]
        public List<ObligationItem> ObligationItemsByGet { get; set; }
    }
}
