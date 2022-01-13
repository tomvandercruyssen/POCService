using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;

namespace SharedLib.DTOSQLite
{
    public class TagDTOS
    {
        [JsonPropertyName("tagId")]
        public Guid TagId { get; set; } = new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("bufferHours")]
        public uint BufferHours { get; set; }
        [JsonPropertyName("samplingInterval")]
        public uint SamplingInterval { get; set; }
        [JsonPropertyName("queueSize")]
        public uint QueueSize { get; set; }
        [JsonPropertyName("discardOldest")]
        public bool DiscardOldest { get; set; }
        [JsonPropertyName("serverId")]
        public Guid ServerId { get; set; }

        public TagDTOS()
        {

        }

        public TagDTOS(TagS t)
        {
            TagId = t.TagId;
            Name = t.Name;
            NodeId = t.NodeId;
            Type = t.Type;
            BufferHours = t.BufferHours;
            SamplingInterval = t.SamplingInterval;
            QueueSize = t.QueueSize;
            DiscardOldest = t.DiscardOldest;
            ServerId = t.Server.ServerId;
        }

        public TagS FromDTO()
        {
            return new TagS()
            {
                TagId = TagId,
                Name = Name,
                NodeId = NodeId,
                Type = Type,
                BufferHours = BufferHours,
                SamplingInterval = SamplingInterval,
                QueueSize = QueueSize,
                DiscardOldest = DiscardOldest
            };
        }
    }
}
