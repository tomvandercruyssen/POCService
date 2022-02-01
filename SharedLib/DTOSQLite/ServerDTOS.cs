using System;
using SharedLib.DataS;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;
using SharedLib.Enums;

namespace SharedLib.DTOSQLite
{
    public class ServerDTOS
    {
        [JsonPropertyName("serverId")]
        public Guid ServerId { get; set; } = new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }
        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }
        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }
        [JsonPropertyName("publishingInterval")]
        public uint PublishingInterval { get; set; }
        [JsonPropertyName("maxNotifications")]
        public uint MaxNotifications { get; set; }
        [JsonPropertyName("sessionTimeOut")]
        public uint SessionTimeOut { get; set; }
        [JsonPropertyName("maxKeepAlive")]
        public uint MaxKeepAlive { get; set; }
        [JsonPropertyName("lifetimeCount")]
        public uint LifetimeCount { get; set; }
        [JsonPropertyName("reconnectOnSubscriptionDelete")]
        public bool ReconnectOnSubscriptionDelete { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        [JsonPropertyName("credentialsId")]
        public Guid CredentialsId { get; set; }
        [JsonPropertyName("tagIds")]
        public List<Guid> TagIds { get; set; } = new List<Guid>();

        [JsonPropertyName("tagcount")]
        public uint TagCount { get; set; }
        [JsonPropertyName("connectionState")]
        public ConnectionStates ConnectionState { get; set; }

        public ServerDTOS()
        {

        }

        public ServerDTOS(ServerS s)
        {
            ServerId = s.ServerId;
            Name = s.Name;
            Endpoint = s.Endpoint;
            Protocol = s.Protocol;
            TimeZone = s.TimeZone;
            PublishingInterval = s.PublishingInterval;
            MaxNotifications = s.MaxNotifications;
            SessionTimeOut = s.SessionTimeOut;
            MaxKeepAlive = s.MaxKeepAlive;
            LifetimeCount = s.LifetimeCount;
            ReconnectOnSubscriptionDelete = s.ReconnectOnSubscriptionDelete;
            Enabled = s.Enabled;
            //CredentialsId = s.Credentials.ServerCredentialsId;
            TagIds = s.Tags.Select(t => t.TagId).ToList();
            TagCount = (uint)s.Tags.Count();
        }
        public ServerS FromDTO()
        {
            return new ServerS()
            {
                ServerId = ServerId,
                Name = Name,
                Endpoint = Endpoint,
                Protocol = Protocol,
                TimeZone = TimeZone,
                PublishingInterval = PublishingInterval,
                MaxNotifications = MaxNotifications,
                SessionTimeOut = SessionTimeOut,
                MaxKeepAlive = MaxKeepAlive,
                LifetimeCount = LifetimeCount,
                ReconnectOnSubscriptionDelete = ReconnectOnSubscriptionDelete,
                Enabled = Enabled
            };
        }
    }
}
