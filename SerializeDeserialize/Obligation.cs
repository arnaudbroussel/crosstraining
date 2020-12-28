using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace crosstraining.SerializeDeserialize {

    public class Obligation {
        [JsonProperty(PropertyName = "statusCode")]
        [JsonPropertyNameAttribute("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty(PropertyName = "payload")]
        [JsonPropertyNameAttribute ("payload")]
        public List<ObligationItem> ObligationItems { get; set; }
    }
}
