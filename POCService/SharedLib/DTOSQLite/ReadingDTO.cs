using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SharedLib.Data.SQLite;

namespace SharedLib.DTOSQLite
{
    public class ReadingDTO
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

        public ReadingDTO()
        {

        }

        public ReadingDTO(Reading r)
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

        public Reading FromDTO()
        {
            return new Reading()
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
