using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace crosstraining.json
{
    public static class JsonTools
    {
        private static bool IsJsonArray(this string json)
        {

            if ((!json.StartsWith("['")) && (!json.EndsWith(']')))
                return false;
            try
            {
                JToken.Parse(json);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        public static List<string> ToArray(this string json)
        {
            json = json.Replace(" ", string.Empty);
            if (!IsJsonArray(json))
                return null;

            var l = new List<string>();

            foreach (var item in JsonConvert.DeserializeObject<dynamic>(json))
            {
                l.Add(item.ToString());
            }

            return l;
        }

        private static string json = "{\"items\":[{\"links\":{\"self\":\"//company//03434121\"},\"company_number\":\"03434121\",\"company_status\":\"active\",\"company_type\":\"ltd\",\"title\":\"SAGE LIMITED\",\"matches\":{\"title\":[1,4],\"snippet\":[]},\"snippet\":\"\",\"description\":\"03434121 - Incorporated on 15 September 1997\",\"kind\":\"searchresults#company\",\"date_of_creation\":\"1997-09-15\",\"address\":{\"postal_code\":\"IG11 7BZ\",\"locality\":\"Barking\",\"address_line_1\":\"10 Town Quay Wharf Abbey Road\",\"premises\":\"GARROD BECKETT AND CO LTD\",\"region\":\"Essex\"},\"address_snippet\":\"GARROD BECKETT AND CO LTD, 10 Town Quay Wharf Abbey Road, Barking, Essex, IG11 7BZ\",\"description_identifier\":[\"incorporated-on\"]},{\"title\":\"ECHO SKINCARE LTD\",\"matches\":{\"snippet\":[1,4]},\"snippet\":\"SAGE BEAUTY \",\"company_status\":\"active\",\"company_type\":\"ltd\",\"company_number\":\"09674471\",\"links\":{\"self\":\"//company//09674471\"},\"address\":{\"country\":\"England\",\"postal_code\":\"DN21 5BX\",\"address_line_1\":\"Castle Farm\",\"premises\":\"Castle Farm Offices Castle Farm Offices\",\"locality\":\"Fillingham\",\"region\":\"Lincs\"},\"date_of_creation\":\"2015-07-07\",\"address_snippet\":\"Castle Farm Offices Castle Farm Offices, Castle Farm, Fillingham, Lincs, England, DN21 5BX\",\"description_identifier\":[\"incorporated-on\"],\"description\":\"09674471 - Incorporated on  7 July 2015\",\"kind\":\"searchresults#company\"}]}";
        
        private static string jsonBig = "[{\"address\":{\"address_line_1\":\"p69Ww0oh5E\",\"address_line_2\":\"PzW0RQbnSy\",\"care_of\":\"pDr3x4Aoma\",\"country\":\"7b2X6XTqk6\",\"locality\":\"UIogKKs1m7\",\"po_box\":\"2KtSUU2hGP\",\"postal_code\":\"kHORtGo\",\"region\":\"fiz509OUHM\"},\"snippet\":\"Z9pucwjUjm\",\"company_number\":\"yvlbWxHHt2\",\"matches\":{\"snippet\":[\"poR\",\"1Wa\"]},\"kind\":\"searchresults#company\",\"description\":\"QdJAPhTuvfGnEZepnZzFIStgy8zjouSEgm7eSO5vmFlGdiSmS1\",\"date_of_creation\":\"1994-08-08\",\"title\":\"AAbQCJhJ9hYBc7sageVxyXVXFcez9L\",\"links\":{\"self\":\"/company/12234578\"},\"address_snippet\":\"lIMin5DVhjgnh5RoO0ssEJbpes0oF7wxX71sxb1BOKRWDFjCjt\",\"company_status\":\"active\",\"company_type\":\"ltd\",\"description_identifier\":[\"XeyTT\",\"5rrpS\"]},{\"address\":{\"address_line_1\":\"lrbpN4OqxP\",\"address_line_2\":\"hG4tn7qjah\",\"care_of\":\"sbxyi55qTf\",\"country\":\"FrryKwSxgx\",\"locality\":\"hREG1KM8nu\",\"po_box\":\"MTnuHUvoZG\",\"postal_code\":\"gwPOhHL\",\"region\":\"yNWan6ojOH\"},\"snippet\":\"1kSO6cJPGL\",\"company_number\":\"YI8P4s77AI\",\"matches\":{\"snippet\":[\"jtz\",\"DPe\"]},\"kind\":\"searchresults#company\",\"description\":\"S1fY4ZqFMtHTvdvwyhM1VCTBAHrBOj1Bz5DGMdfbl8vxruJ0LD\",\"date_of_creation\":\"1974-02-07\",\"title\":\"vG4fVGqahzeBc5sage28O67hD1j74s\",\"links\":{\"self\":\"/company/12234578\"},\"address_snippet\":\"3WskD7ocfldTozzRH3CNekd95BnDwaotONqHPAHGtdKRwEQll4\",\"company_status\":\"active\",\"company_type\":\"ltd\",\"description_identifier\":[\"9gHGh\",\"t2f5n\"]},{\"address\":{\"address_line_1\":\"hympnROKWO\",\"address_line_2\":\"zhJvdXAA4z\",\"care_of\":\"rB4mHqf2oj\",\"country\":\"kmjZv6Cbrr\",\"locality\":\"NcSznOrLsb\",\"po_box\":\"MBYywRL6rM\",\"postal_code\":\"bgPU1Pc\",\"region\":\"WEjIJCWZVQ\"},\"snippet\":\"9QoQWd2124\",\"company_number\":\"nnQKbCsTFR\",\"matches\":{\"snippet\":[\"txF\",\"0nH\"]},\"kind\":\"searchresults#company\",\"description\":\"w8Sua37svnSCktTbpe0WHUADjamCYrIKT93EvXBGrNcL7zhbNZ\",\"date_of_creation\":\"2010-09-11\",\"title\":\"jFtbmQMV0Rfzulsageea61MbZ8fmwX\",\"links\":{\"self\":\"/company/12234578\"},\"address_snippet\":\"jexHua8km9nTumJsZBdfXgwM3aOGeP2DXI5mETbVef0I5MRnZS\",\"company_status\":\"active\",\"company_type\":\"ltd\",\"description_identifier\":[\"wK6PU\",\"kwkF0\"]}]";
        ////private static string jsonBig = "{\"snippet\": \"Z9pucwjUjm\", \"company_number\": \"yvlbWxHHt2\", \"kind\": \"searchresults#company\", \"description\": \"QdJAPhTuvfGnEZepnZzFIStgy8zjouSEgm7eSO5vmFlGdiSmS1\", \"date_of_creation\": \"1994-08-08\", \"title\": \"AAbQCJhJ9hYBc7sageVxyXVXFcez9L\", \"address_snippet\": \"lIMin5DVhjgnh5RoO0ssEJbpes0oF7wxX71sxb1BOKRWDFjCjt\", \"company_status\": \"active\", \"company_type\": \"ltd\"  }";



        public static void CompanyLookup()
        {
            var jObjectBig = JArray.Parse(jsonBig);
            var big_company_number = jObjectBig[0].SelectToken("company_number");

            System.Console.WriteLine($"jObjectBig.Count={jObjectBig.Count}");
            System.Console.WriteLine($"links..self={jObjectBig[0].SelectToken("links..self")}");

            var jObject = JObject.Parse(json);
            var resultToken = jObject.SelectToken("items");
            var company_number = resultToken[0].SelectToken("company_number");
            var address = resultToken[0].SelectToken("address");
            var locality = address.SelectToken("locality");
            var address_line_1 = address.SelectToken("address_line_1");
            var links_self = resultToken[0].SelectToken("links..self");
            var description_identifier = resultToken[0].SelectToken("description_identifier");
            var matches_snippet = resultToken[0].SelectToken("matches..snippet");
            var matches_title = resultToken[0].SelectToken("matches..title");
            var matches_title_array = resultToken[0].SelectTokens("matches..title");
        }
    }
}