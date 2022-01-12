using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;

namespace SharedLib.DTO
{
    public class WebServiceDTO
    {
        [JsonPropertyName("webServiceId")]
        public string WebServiceId { get; set; } //= new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("delay")]
        public uint Delay { get; set; }
        [JsonPropertyName("bundle")]
        public bool Bundle { get; set; }
        [JsonPropertyName("keepSequence")]
        public bool KeepSequence { get; set; }
        [JsonPropertyName("retryCount")]
        public uint RetryCount { get; set; }
        [JsonPropertyName("retryDelay")]
        public uint RetryDelay { get; set; }
        [JsonPropertyName("historyHours")]
        public uint HistoryHours { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        [JsonPropertyName("destinationId")]
        public string DestinationId { get; set; }
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }
        [JsonPropertyName("elementIds")]
        public List<string> ElementIds { get; set; } = new List<string>();
        [JsonPropertyName("callIds")]
        public List<string> CallIds { get; set; } = new List<string>();

        public WebServiceDTO()
        {

        }
        public WebServiceDTO(WebService w)
        {
            WebServiceId = w.WebServiceId;
            Name = w.Name;
            Delay = w.Delay;
            Bundle = w.Bundle;
            KeepSequence = w.KeepSequence;
            RetryCount = w.RetryCount;
            RetryDelay = w.RetryDelay;
            HistoryHours = w.HistoryHours;
            Enabled = w.Enabled;
            DestinationId = w.Destination.DestinationId;
            GroupId = w.Group.WebServiceGroupId;
            if (w.Elements != null || w.Elements.Count != 0)
            {
                ElementIds = w.Elements.Select(e => e.WebServiceElementId).ToList();
            }
            if (w.Calls != null || w.Calls.Count != 0)
            {
                CallIds = w.Calls.Select(c => c.WebServiceCallId).ToList();
            }
        }

        public WebService FromDTO()
        {
            return new WebService()
            {
                WebServiceId = WebServiceId,
                Name = Name,
                Delay = Delay,
                Bundle = Bundle,
                KeepSequence = KeepSequence,
                RetryCount = RetryCount,
                RetryDelay = RetryDelay,
                HistoryHours = HistoryHours,
                Enabled = Enabled
            };
        }
    }
}
