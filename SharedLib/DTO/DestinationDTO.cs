using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;

namespace SharedLib.DTO
{
    public class DestinationDTO
    {
        [JsonPropertyName("destinationId")]
        public string DestinationId { get; set; }// = new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("webServiceIds")]
        public List<string> WebServiceIds { get; set; } = new List<string>();

        public DestinationDTO()
        {

        }
        public DestinationDTO(Destination d)
        {
            DestinationId = d.DestinationId;
            Name = d.Name;
            Endpoint = d.Endpoint;
            Username = d.Username;
            Password = d.Password;
            WebServiceIds = d.WebServices.Select(ws => ws.WebServiceId).ToList();
        }

        public Destination FromDTO()
        {
            return new Destination()
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
