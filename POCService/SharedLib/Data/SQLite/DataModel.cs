using Microsoft.EntityFrameworkCore;
using SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;
namespace SharedLib.Data.SQLite
{
    public class AppUser
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LogEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogEntryId { get; set; } = new Guid();
        public DateTime Created { get; set; }
        public string Username { get; set; }
        public ServiceProcesses Process { get; set; }
        public LogCategories Category { get; set; }
        public string Details { get; set; }
        public NotificationLevels NotificationLevel { get; set; }
    }

    public class GlobalConfig
    {
        [Key]
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
    public class Reading
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ReadingId { get; set; } = new Guid();
        public DateTime Created { get; set; }
        public string Quality { get; set; }
        public string StringValue { get; set; }
        public long IntegerValue { get; set; }
        public ulong UnsignedIntegerValue { get; set; }
        public double FloatValue { get; set; }
        [Required]
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

        public string GetValueString()
        {
            string s = "";
            switch (Tag.Type.ToLower())
            {
                case "string":
                    s = StringValue;
                    break;
                case "uint":
                    s = UnsignedIntegerValue.ToString();
                    break;
                case "int":
                    s = IntegerValue.ToString();
                    break;
                case "float":
                    s = FloatValue.ToString();
                    break;
                default:
                    break;
            }
            return s;
        }
        public void AddValue(object value)
        {
            switch (Tag.Type.ToLower())
            {
                case "string":
                    StringValue = value as string;
                    break;
                case "uint":
                    UnsignedIntegerValue = ulong.Parse(value.ToString());
                    break;
                case "int":
                    IntegerValue = long.Parse(value.ToString());
                    break;
                case "float":
                    FloatValue = float.Parse(value.ToString());
                    break;
                default:
                    Debug.WriteLine("value is {0}, type is {1}", value.ToString(), value.GetType().Name);
                    break;
            }
        }

        public override string ToString()
        {
            return String.Format("[created={0},value={1}]", Created.ToString(), GetValueString());
        }
    }
    public class Server
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ServerId { get; set; } = new Guid();
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public string Protocol { get; set; }
        public string TimeZone { get; set; }
        public uint PublishingInterval { get; set; } = 1000;
        public uint MaxNotifications { get; set; } = 0;
        public uint SessionTimeOut { get; set; } = 10000;
        public uint MaxKeepAlive { get; set; } = 0;
        public uint LifetimeCount { get; set; } = 0;
        public bool ReconnectOnSubscriptionDelete { get; set; } = true;
        public bool Enabled { get; set; } = true;
        [Required]
        [ForeignKey("ServerCredentialsId")]
        public ServerCredentials Credentials { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
    public class ServerCredentials
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ServerCredentialsId { get; set; } = new Guid();
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TagId { get; set; } = new Guid();
        public string Name { get; set; }
        public string NodeId { get; set; }
        public string Type { get; set; }
        public uint BufferHours { get; set; }
        public uint SamplingInterval { get; set; }
        public uint QueueSize { get; set; }
        public bool DiscardOldest { get; set; }
        [Required]
        [ForeignKey("ServerId")]
        public Server Server { get; set; }
        public List<WebServiceElement> WebServiceElements { get; set; } = new List<WebServiceElement>();
        public List<Reading> Readings { get; set; } = new List<Reading>();
    }
    public class SupportedType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonPropertyName("supportedTypeId")]
        public int SupportedTypeId { get; set; }
        [JsonPropertyName("typeName")]
        public string TypeName { get; set; }
    }

    public class Destination
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid DestinationId { get; set; } = new Guid();
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<WebService> WebServices { get; set; } = new List<WebService>();
    }

    public class WebServiceGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceGroupId { get; set; } = new Guid();
        public string Name { get; set; }
        public List<WebService> WebServices { get; set; } = new List<WebService>();
    }

    public class WebService
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceId { get; set; } = new Guid();
        public string Name { get; set; }
        public uint Delay { get; set; }
        public bool Bundle { get; set; }
        public bool KeepSequence { get; set; }
        public uint RetryCount { get; set; }
        public uint RetryDelay { get; set; }
        public uint HistoryHours { get; set; }
        public bool Enabled { get; set; }
        [Required]
        [ForeignKey("DestinationId")]
        public Destination Destination { get; set; }
        [Required]
        [ForeignKey("WebServiceGroupId")]
        public WebServiceGroup Group { get; set; }
        public List<WebServiceElement> Elements { get; set; } = new List<WebServiceElement>();
        public List<WebServiceCall> Calls { get; set; } = new List<WebServiceCall>();
    }

    public class WebServiceElement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceElementId { get; set; } = new Guid();
        public string Name { get; set; }
        [Required]
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
        [Required]
        [ForeignKey("WebServiceId")]
        public WebService WebService { get; set; }
        public List<WebServiceElementValue> Values { get; set; } = new List<WebServiceElementValue>();

    }

    public class WebServiceElementValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceElementValueId { get; set; } = new Guid();
        public DateTime Created { get; set; }
        public string Value { get; set; }
        [Required]
        [ForeignKey("WebServiceElementId")]
        public WebServiceElement WebServiceElement { get; set; }
    }

    public class WebServiceCall
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceCallId { get; set; } = new Guid();
        public DateTime Created { get; set; }
        public string RawInput { get; set; }
        public string Result { get; set; } = "not final";
        [Required]
        [ForeignKey("WebServiceId")]
        public WebService WebService { get; set; }
        public List<WebServiceCallExecution> Executions { get; set; } = new List<WebServiceCallExecution>();
    }

    public class WebServiceCallExecution
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid WebServiceCallExecutionId { get; set; } = new Guid();
        public string RawOutput { get; set; }
        public string Status { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public DateTime FinishedTimeStamp { get; set; }
        [Required]
        [ForeignKey("WebServiceCallId")]
        public WebServiceCall WebServiceCall { get; set; }
    }
}
