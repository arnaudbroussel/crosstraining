namespace crosstraining.enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum PipelineName
    {
        /// <summary>
        /// Pipeline as Dimensions
        /// </summary>
        Dimensions = 0,

        /// <summary>
        /// Pipeline as DimensionTags
        /// </summary>
        DimensionTags = 1
    }
}