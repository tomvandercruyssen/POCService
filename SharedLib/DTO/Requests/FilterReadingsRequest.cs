using System;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Requests
{
    public class FilterReadingsRequest
    {
        [JsonPropertyName("start")]
        public DateTime Start { get; set; }
        [JsonPropertyName("end")]
        public DateTime End { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("operand")]
        public string Operand { get; set; }
        [JsonPropertyName("filterValue")]
        public string FilterValue { get; set; }
    }
}
