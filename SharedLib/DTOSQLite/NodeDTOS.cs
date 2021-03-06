using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTOSQLite
{
    public class NodeDTOS
    {
        [JsonPropertyName("children")]
        public List<NodeDTOS> Children { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; }
        [JsonPropertyName("dataType")]
        public string Datatype { get; set; }
    }
}
