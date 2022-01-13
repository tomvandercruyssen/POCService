using SharedLib.Data;
using SharedLib.Enums;
using System;
using System.Text.Json.Serialization;

namespace SharedLib.DTOSQLite
{
    public class LogEntryDTOS
    {
        [JsonPropertyName("logEntryId")]
        public Guid LogEntryId { get; set; } = new Guid();
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("process")]
        public ServiceProcesses Process { get; set; }
        [JsonPropertyName("category")]
        public LogCategories Category { get; set; }
        [JsonPropertyName("details")]
        public string Details { get; set; }
        [JsonPropertyName("notificationLevel")]
        public NotificationLevels NotificationLevel { get; set; }

        public LogEntryDTOS()
        {

        }

        public LogEntryDTOS(LogEntryS log)
        {
            LogEntryId = log.LogEntryId;
            Username = log.Username;
            Created = log.Created;
            Process = log.Process;
            Category = log.Category;
            Details = log.Details;
            NotificationLevel = log.NotificationLevel;
        }

        public LogEntryS FromDTO()
        {
            return new LogEntryS()
            {
                LogEntryId = LogEntryId,
                Username = Username,
                Created = Created,
                Process = Process,
                Category = Category,
                Details = Details,
                NotificationLevel = NotificationLevel
            };
        }
    }
}
