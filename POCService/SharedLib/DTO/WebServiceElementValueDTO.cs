using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO
{
    public class WebServiceElementValueDTO
    {
        [JsonPropertyName("webServiceElementValueId")]
        public string WebServiceElementValueId { get; set; }// = new Guid();
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("webServiceElementId")]
        public string WebServiceElementId { get; set; }

        public WebServiceElementValueDTO()
        {

        }

        public WebServiceElementValueDTO(WebServiceElementValue v)
        {
            WebServiceElementValueId = v.WebServiceElementValueId;
            Created = v.Created;
            Value = v.Value;
            WebServiceElementId = v.WebServiceElement.WebServiceElementId;
        }

        public WebServiceElementValue FromDTO()
        {
            return new WebServiceElementValue()
            {
                WebServiceElementValueId = WebServiceElementValueId,
                Created = Created,
                Value = Value
            };
        }
    }
}
