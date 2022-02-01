using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data;
using SharedLib.DataS;

namespace SharedLib.DTOSQLite
{
    public class ReadingDTOS
    {
        [JsonPropertyName("readingId")]
        public Guid ReadingId { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("quality")]
        public string Quality { get; set; }
        [JsonPropertyName("stringValue")]
        public string StringValue { get; set; }
        [JsonPropertyName("integerValue")]
        public long IntegerValue { get; set; }
        [JsonPropertyName("unsignedIntegerValue")]
        public ulong UnsignedIntegerValue { get; set; }
        [JsonPropertyName("floatValue")]
        public double FloatValue { get; set; }
        /*
        [JsonPropertyName("tagId")]
        public Guid TagId { get; set; }
        */

        public ReadingDTOS()
        {

        }

        public ReadingDTOS(ReadingSql r)
        {
            ReadingId = r.ReadingId;
            Created = r.Created;
            Quality = r.Quality;
            StringValue = r.StringValue;
            IntegerValue = r.IntegerValue;
            UnsignedIntegerValue = r.UnsignedIntegerValue;
            FloatValue = r.FloatValue;
            // TagId = r.Tag.TagId;
        }

        public ReadingSql FromDTO()
        {
            return new ReadingSql()
            {
                ReadingId = ReadingId,
                Created = Created,
                Quality = Quality,
                StringValue = StringValue,
                IntegerValue = IntegerValue,
                UnsignedIntegerValue = UnsignedIntegerValue,
                FloatValue = FloatValue
            };
        }
    }
}
