using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;
using SharedLib.DataS;

namespace SharedLib.DTOSQLite
{
    public class DestinationDTOS
    {
        [JsonPropertyName("destinationId")]
        public Guid DestinationId { get; set; } = new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("webServiceIds")]
        public List<Guid> WebServiceIds { get; set; } = new List<Guid>();

        public DestinationDTOS()
        {

        }
        public DestinationDTOS(DestinationS d)
        {
            DestinationId = d.DestinationId;
            Name = d.Name;
            Endpoint = d.Endpoint;
            Username = d.Username;
            Password = d.Password;
            WebServiceIds = d.WebServices.Select(ws => ws.WebServiceId).ToList();
        }

        public DestinationS FromDTO()
        {
            return new DestinationS()
            {
                DestinationId = DestinationId,
                Name = Name,
                Endpoint = Endpoint,
                Username = Username,
                Password = Password
            };
        }
    }
}
