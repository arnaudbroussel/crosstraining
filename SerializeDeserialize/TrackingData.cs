using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.SerializeDeserialize {

    public class TrackingData {
        [JsonProperty(PropertyName = "$tracking")]
        public Tracking Tracking { get; set; }
    }
}
