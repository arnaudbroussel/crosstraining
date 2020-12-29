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

        [JsonProperty(PropertyName = "payload")]
        [JsonPropertyNameAttribute ("payload")]
        public List<ObligationItem> ObligationItems { get; set; }

        [JsonProperty(PropertyName = "$results.payload")]
        public List<ObligationItem> ObligationItemsByGet { get; set; }
    }
}
