using Newtonsoft.Json;

namespace crosstraining.SerializeDeserialize {

    public class PaymentItemData {
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "received")]
        public string Received { get; set; }
    }
}