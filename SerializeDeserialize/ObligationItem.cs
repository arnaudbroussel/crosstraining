using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace crosstraining.SerializeDeserialize {

    public class ObligationItem {
        [JsonProperty(PropertyName = "start")]
        public string StartDate { get; set; }

        [JsonProperty(PropertyName = "end")]
        public string EndDate { get; set; }

        [JsonProperty(PropertyName = "due")]
        public string DueDate { get; set; }

        [JsonProperty(PropertyName = "status")]
        public ObligationStatus Status { get; set; }

        [JsonProperty(PropertyName = "periodKey")]
        public string PeriodKey { get; set; }

        [JsonProperty(PropertyName = "received")]
        public string ReceivedDate { get; set; }
    }
}