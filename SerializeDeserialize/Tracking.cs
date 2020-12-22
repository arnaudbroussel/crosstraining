namespace crosstraining.SerializeDeserialize {
    using Newtonsoft.Json;

    public class Tracking {
        [JsonProperty(PropertyName = "$phase")]
        public string Phase { get; set; }

        [JsonProperty(PropertyName = "$phaseDetails")]
        public string PhaseDetails { get; set; }

        [JsonProperty(PropertyName = "$pollingMillis")]
        public int PollingMillis { get; set; }
    }
}