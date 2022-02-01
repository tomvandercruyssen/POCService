using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO.Responses.Containers
{
    public class TagLatestValuePair
    {
        [JsonPropertyName("tag")]
        public TagDTO Tag { get; set; }
        [JsonPropertyName("latestValue")]
        public ReadingDTO LatestValue { get; set; }
    }
}
