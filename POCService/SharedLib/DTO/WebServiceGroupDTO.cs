using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SharedLib.Data;

namespace SharedLib.DTO
{
    public class WebServiceGroupDTO
    {
        [JsonPropertyName("webServiceGroupId")]
        public string WebServiceGroupId { get; set; } //= new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("webServiceIds")]
        public List<string> WebServiceIds { get; set; }// = new List<Guid>();
        public WebServiceGroupDTO()
        {

        }

        public WebServiceGroupDTO(WebServiceGroup g)
        {
            WebServiceGroupId = g.WebServiceGroupId;
            Name = g.Name;
            WebServiceIds = g.WebServices.Select(w => w.WebServiceId).ToList();
        }

        public WebServiceGroup FromDTO()
        {
            return new WebServiceGroup()
            {
                WebServiceGroupId = WebServiceGroupId,
                Name = Name
            };
        }
    }
}
