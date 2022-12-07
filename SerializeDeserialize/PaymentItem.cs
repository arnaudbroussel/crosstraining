using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace crosstraining.SerializeDeserialize {

    public class PaymentItem {
        [JsonProperty(PropertyName = "payload/payments")]
        public PaymentItemData paymentItemData { get; set; }
    }
}