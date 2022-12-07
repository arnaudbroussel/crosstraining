using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace crosstraining.regex
{
    public static class RegexSuite
    {
        public static void BusinessId(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            value = value.Replace(" ", string.Empty).ToUpperInvariant();

            ////const string regexPattern = @"(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\s*\d\s*){6}([A-D]|\s)$";
            const string regexPattern = @"^X[a-zA-Z0-9]{1}IS[0-9]{11}$";
            var regex = new Regex(regexPattern, RegexOptions.Compiled);

            Console.WriteLine($"Check for {value} = {regex.IsMatch(value)}");
        }
    }
}