using Newtonsoft.Json;

namespace crosstraining.SerializeDeserialize {
    public class NavigateUrl {
        [JsonProperty(PropertyName = "$navigateToUrl")]
        public string Url { get; set; }
    }
}
