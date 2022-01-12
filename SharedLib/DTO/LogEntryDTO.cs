using SharedLib.Data;
using SharedLib.Enums;
using System;
using System.Text.Json.Serialization;

namespace SharedLib.DTO
{
    public class LogEntryDTO
    {
        [JsonPropertyName("logEntryId")]
        public string LogEntryId { get; set; } //= new Guid();
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

        public LogEntryDTO()
        {

        }

        public LogEntryDTO(LogEntry log)
        {
            LogEntryId = log.LogEntryId;
            Username = log.Username;
            Created = log.Created;
            Process = log.Process;
            Category = log.Category;
            Details = log.Details;
            NotificationLevel = log.NotificationLevel;
        }

        public LogEntry FromDTO()
        {
            return new LogEntry()
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
