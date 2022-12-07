using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace crosstraining.encodeurl
{
    public static class EncodeUrlExamples
    {
        public static void ToUrl()
        {

            var a = HttpUtility.UrlEncode("Stack Overflow");
            var b = Uri.EscapeUriString("Stack Overflow");
            var c = Uri.EscapeDataString("Stack +  Overflow");

            var k = "[\"08ba261f-ceee-d383-8559-45c8b4491461\", \"ec32df32-2867-082e-3d73-c4a3e368a323\", \"01573cd3-7ad7-c29b-790f-26bca231a8dc\"]";
            var d = "/tax-schemes/mappings/assigned-tax-scheme=" + HttpUtility.UrlEncode($"{k}");

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);

            var z = HttpUtility.UrlDecode(d).ToString();
            var w = z.Replace("\"", "");

            var p = "AddVatStandard, AddFlatCashAccountingVat";
            var json0 = System.Text.Json.JsonSerializer.Serialize(p.Replace(" ", string.Empty).Split(','));
            var json1 = System.Text.Json.JsonSerializer.Serialize(p.Replace(" ", string.Empty).Split(',')).Replace("\"","");
            Console.WriteLine(json0);
            Console.WriteLine(json1);
        }
    }
}