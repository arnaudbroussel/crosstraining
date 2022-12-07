namespace crosstraining.MD5 {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;

    public static class MD5Extensions {
        /// <summary>
        /// Returns Md5 hashed Guid of string value
        /// </summary>
        /// <param name="value">String to hash</param>
        public static Guid ToMD5Hash(this string value) {
            return value.ToMD5Hash(false);
        }

        /// <summary>
        /// Returns Md5 hashed Guid of string value
        /// </summary>
        /// <param name="value">String to hash</param>
        /// <param name="preserveCase">Preserve original case of string, defaults to false</param>
        public static Guid ToMD5Hash(this string value, bool preserveCase) {
#pragma warning disable CA5351
            var valueToHash = preserveCase ? value : value.ToLower();
            using (MD5 md5Hash = MD5.Create()) {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));
                return new Guid(data);
            }
#pragma warning restore CA5351
        }

        /// <summary>
        /// Returns Md5 hashed Guid of a list of strings
        /// </summary>
        /// <param name="values">List of Strings to hash</param>
        public static Guid ToMD5Hash(this List<string> values) {
            return ToMD5Hash(values, false);
        }

        /// <summary>
        /// Returns Md5 hashed Guid of a list of strings
        /// </summary>
        /// <param name="values">List of Strings to hash</param>
        /// <param name="preserveCase">Preserve original case of string, defaults to false</param>
        public static Guid ToMD5Hash(this List<string> values, bool preserveCase) {
            return string.Join("-", values).ToMD5Hash(preserveCase);
        }

        /// <summary>
        /// Returns Md5 hashed Guid of a Json Serialized dictionary
        /// </summary>
        /// <param name="values">Dictionary to hash</param>
        public static Guid ToMD5Hash(this Dictionary<string, object> values) {
            return ToMD5Hash(values, false);
        }

        /// <summary>
        /// Returns Md5 hashed Guid of Json Serialized dictionary
        /// </summary>
        /// <param name="values">Dictionary to hash</param>
        /// <param name="preserveCase">Preserve original case of string, defaults to false</param>
        public static Guid ToMD5Hash(this Dictionary<string, object> values, bool preserveCase) {
            return JsonConvert.SerializeObject(
                values.OrderBy(
                    c => c.Key
                ).ToDictionary(
                    pair => pair.Key, pair => pair.Value
                )
            ).ToMD5Hash(preserveCase);
        }
    }
}