using POCService.Logging;
using SharedLib.Data.SQLite;
using System;
using System.Text.Json.Serialization;
namespace POCService.SharedLib.DTOSQLite
{
    public class LogDTO
    {
        [JsonPropertyName("LogId")]
        public Guid LogId { get; set; } = new Guid();
        [JsonPropertyName("name")]
        public int Query { get; set; }
        [JsonPropertyName("nodeId")]
        public int Time { get; set; }
        [JsonPropertyName("type")]
        public int Database { get; set; }
        public int AmountOfRecords { get; set; }

        public LogDTO()
        {

        }

        public LogDTO(Log l)
        {
            LogId = l.LogId;
            Query = l.Query;
            Time = l.Time;
            Database = l.Database;
            AmountOfRecords = l.AmountOfRecords;

        }

        public Log FromDTO()
        {
            return new Log()
            {
                LogId = LogId,
                Query = Query,
                Time = Time,
                Database = Database,
                AmountOfRecords = AmountOfRecords
            };
        }
    }
}
