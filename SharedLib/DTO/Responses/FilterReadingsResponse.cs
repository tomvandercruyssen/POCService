using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Responses
{
    public class FilterReadingsResponse
    {
        [JsonPropertyName("readings")]
        public List<ReadingDTO> Readings { get; set; }
        [JsonPropertyName("resultString")]
        public string ResultString { get; set; }
        [JsonPropertyName("max")]
        public decimal Max { get; set; }
    }
}
