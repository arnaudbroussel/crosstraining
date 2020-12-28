using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace crosstraining.SerializeDeserialize {
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ObligationStatus {
        /// <summary>
        /// Fullfilled
        /// </summary>
        [EnumMember(Value = "F")]
        Fullfilled,

        /// <summary>
        /// Open
        /// </summary>
        [EnumMember(Value = "O")]
        Open
    }
}