using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;

namespace SharedLib.DTO
{
    public class WebServiceCallDTO
    {
        [JsonPropertyName("webServiceCallId")]
        public string WebServiceCallId { get; set; } //= new Guid();
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("rawInput")]
        public string RawInput { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("executionsIds")]
        public List<string> ExecutionIds { get; set; } = new List<string>();

        public WebServiceCallDTO()
        {

        }

        public WebServiceCallDTO(WebServiceCall c)
        {
            WebServiceCallId = c.WebServiceCallId;
            Created = c.Created;
            RawInput = c.RawInput;
            Result = c.Result;
            ExecutionIds = c.Executions.Select(e => e.WebServiceCallExecutionId).ToList();
        }

        public WebServiceCall FromDTO()
        {
            return new WebServiceCall()
            {
                WebServiceCallId = WebServiceCallId,
                Created = Created,
                RawInput = RawInput,
                Result = Result
            };
        }
    }
}
