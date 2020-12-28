using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace crosstraining.SerializeDeserialize {

    public class Payment {
        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty(PropertyName = "payload")]
        public List<PaymentItem> PaymentItems { get; set; }
    }
}
