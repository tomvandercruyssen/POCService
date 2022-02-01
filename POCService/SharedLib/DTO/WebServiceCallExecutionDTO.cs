using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLib.DTO
{
    public class WebServiceCallExecutionDTO
    {
        [JsonPropertyName("webServiceCallExecutionId")]
        public string WebServiceCallExecutionId { get; set; }// = new Guid();
        [JsonPropertyName("rawOutput")]
        public string RawOutput { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("executionTime")]
        public TimeSpan ExecutionTime { get; set; }
        [JsonPropertyName("finishedTimeStamp")]
        public DateTime FinishedTimeStamp { get; set; }
        [JsonPropertyName("webServiceCallId")]
        public string WebServiceCallId { get; set; }
        public WebServiceCallExecutionDTO()
        {

        }

        public WebServiceCallExecutionDTO(WebServiceCallExecution e)
        {
            WebServiceCallExecutionId = e.WebServiceCallExecutionId;
            RawOutput = e.RawOutput;
            Status = e.Status;
            ExecutionTime = e.ExecutionTime;
            FinishedTimeStamp = e.FinishedTimeStamp;
            WebServiceCallId = e.WebServiceCall.WebServiceCallId;
        }

        public WebServiceCallExecution FromDTO()
        {
            return new WebServiceCallExecution()
            {
                RawOutput = RawOutput,
                Status = Status,
                ExecutionTime = ExecutionTime,
                FinishedTimeStamp = FinishedTimeStamp
            };
        }
    }
}
