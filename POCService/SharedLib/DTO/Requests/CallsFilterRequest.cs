using SharedLib.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Requests
{
    public class CallsFilterRequest
    {
        [JsonPropertyName("filters")]
        public Dictionary<CallFilters, string> Filters { get; set; }
        [JsonPropertyName("maxAmount")]
        public int MaxAmount { get; set; }
    }
}
