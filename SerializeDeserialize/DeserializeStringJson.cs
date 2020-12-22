using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace crosstraining.SerializeDeserialize {
    public class DeserializeStringJson {

        public static void DoTheThing() {
            string jsonString = "{\"$tracking\": {\"$phase\": \"Submiting VAT return\",\"$phaseDetail\": \"Connecting to HMRC\",\"$pollingMillis\": 500}}";
            TrackingData content = JsonConvert.DeserializeObject<TrackingData>(jsonString);

            string jsonStringUrl = "{\"$navigateToUrl\": \"https://test-api.service.hmrc.gov.uk/oauth/authorize?response_type=code&client_id=lSlExyGJ4eAuB4bbmVD2zmMyJhUa&scope=write%3Avat+read%3Avat&state=Gm5JtPYQEUigIOuhu9gctg&redirect_uri=https%3A%2F%2Flzhozjlorh.execute-api.eu-west-2.amazonaws.com%2Fdev%2Foauth%2Fcallback\"}";
            NavigateUrl contentRul = JsonConvert.DeserializeObject<NavigateUrl>(jsonStringUrl);
        }
    }
}