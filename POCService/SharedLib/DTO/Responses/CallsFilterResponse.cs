using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Responses
{
    public class CallsFilterResponse
    {
        [JsonPropertyName("calls")]
        public List<WebServiceCallDTO> Calls { get; set; } = new List<WebServiceCallDTO>();
        [JsonPropertyName("uniqueResults")]
        public List<String> UniqueResults { get; set; } = new List<string>();
        [JsonPropertyName("totalAmount")]
        public int TotalAmount { get; set; }
    }
}
