using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace crosstraining.SerializeDeserialize {
    public class DeserializeStringJson {

        public static void DoTheThing() {
            string jsonString = "{\"$tracking\": {\"$phase\": \"Submiting VAT return\",\"$phaseDetail\": \"Connecting to HMRC\",\"$pollingMillis\": 500}}";
            TrackingData content = JsonConvert.DeserializeObject<TrackingData>(jsonString);

            string jsonStringUrl = "{\"$navigateToUrl\": \"https://test-api.service.hmrc.gov.uk/oauth/authorize?response_type=code&client_id=lSlExyGJ4eAuB4bbmVD2zmMyJhUa&scope=write%3Avat+read%3Avat&state=Gm5JtPYQEUigIOuhu9gctg&redirect_uri=https%3A%2F%2Flzhozjlorh.execute-api.eu-west-2.amazonaws.com%2Fdev%2Foauth%2Fcallback\"}";
            NavigateUrl contentRul = JsonConvert.DeserializeObject<NavigateUrl>(jsonStringUrl);

            string jsonStringResponse = "{\"headers\": \"{}\", \"statusCode\": \"HMRC.StatusCode\", \"payload\":[{\"start\": \"01/02/03\",\"end\": \"04/05/06\", \"status\": \"O\",\"periodKey\": \"AZERTY\",\"received\": \"17/02/19\"},{\"start\": \"01/02/03\",\"end\": \"04/05/06\", \"status\": \"F\",\"periodKey\": \"QWERTY\",\"received\": \"17/02/19\"},{\"start\": \"01/02/03\",\"end\": \"04/05/06\", \"status\": \"F\",\"periodKey\": \"OTRO MAS\",\"received\": \"17/02/19\"}]}";
            Obligation obligation = JsonConvert.DeserializeObject<Obligation>(jsonStringResponse);

            var obligationToStr = JsonConvert.SerializeObject(obligation);

            string jsonStringResponsePay = "{\"headers\": {},\"statusCode\": \"HMRC.StatusCode\",\"payload\": [	{\"payments\": {\"amount\": 12.1,\"received\": \"yyyy-mm-dd\"}},{\"payments\": {\"amount\": 45.12,\"received\": \"yyyy-mm-dd\"}}]}";
            Payment payment = JsonConvert.DeserializeObject<Payment>(jsonStringResponsePay);

            string jsonStringResponseByGet = "{	\"$title\": \"Submission results\",	\"$statusCode\": 200,	\"$status\": \"success\",	\"$results\": {		\"headers\": {			\"X-CorrelationID\": \"5968c22d620000620096db5f\",			\"Receipt-ID\": \"2dd537bc-4244-4ebf-bac9-96321be13cdc\",			\"Receipt-Timestamp\": \"2018-02-14T09:32:15.000Z\",			\"Receipt-Signature\": \"b9a365c4d2675d5fd8ec77672a2bc29474184c3805b550b987b630c83c200c9c\"		},		\"payload\": [			{				\"start\": \"01/02/03\",				\"end\": \"04/05/06\",				\"status\": \"O\",				\"periodKey\": \"AZERTY\",				\"received\": \"17/02/19\"			},			{				\"start\": \"01/02/03\",				\"end\": \"04/05/06\",				\"status\": \"F\",				\"periodKey\": \"QWERTY\",				\"received\": \"17/02/19\"			},			{				\"start\": \"01/02/03\",				\"end\": \"04/05/06\",				\"status\": \"F\",				\"periodKey\": \"OTRO MAS\",				\"received\": \"17/02/19\"			}		]	}}";
            Obligation obligationByGet = JsonConvert.DeserializeObject<Obligation>(jsonStringResponseByGet);

            var obligationByGetObj = JsonConvert.DeserializeObject<object>(jsonStringResponseByGet);

            var jsonObject = (JObject) JsonConvert.DeserializeObject(jsonStringResponseByGet);
            var results = jsonObject["$results"].ToString();
            var resultsObject = (JObject) JsonConvert.DeserializeObject(results);
            var payload = resultsObject["payload"].ToString();
            var payloadObject = (JArray) JsonConvert.DeserializeObject(payload);
            var l = payloadObject.Select(item => new ObligationItem() { 
                StartDate = (string)item["start"],
                EndDate = (string) item["end"],
                DueDate = (string) item["due"],
                PeriodKey = (string) item["periodkey"],
                ReceivedDate = (string) item["received"]
            }).ToList();
        }
    }
}