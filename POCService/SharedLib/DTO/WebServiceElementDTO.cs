using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;

namespace SharedLib.DTO
{
    public class WebServiceElementDTO
    {
        [JsonPropertyName("webServiceElementId")]
        public string WebServiceElementId { get; set; }// = new Guid();
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("tagId")]
        public string TagId { get; set; }
        [JsonPropertyName("webServiceId")]
        public string WebServiceId { get; set; }
        [JsonPropertyName("valueIds")]
        public List<string> ValueIds { get; set; } = new List<string>();

        public WebServiceElementDTO()
        {

        }

        public WebServiceElementDTO(WebServiceElement e)
        {
            WebServiceElementId = e.WebServiceElementId;
            Name = e.Name;
            TagId = e.Tag.TagId;
            WebServiceId = e.WebService.WebServiceId;
            if (e.Values != null && e.Values.Any())
            {
                ValueIds = e.Values.Select(v => v.WebServiceElementValueId).ToList();
            }
        }

        public WebServiceElement FromDTO()
        {
            return new WebServiceElement()
            {
                WebServiceElementId = WebServiceElementId,
                Name = Name
            };
        }
    }
}
