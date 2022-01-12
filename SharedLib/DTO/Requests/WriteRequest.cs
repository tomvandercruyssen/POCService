using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.DTO.Requests
{
    public class WriteRequest
    {
        public string serverid { get; set; }
        public string tagid { get; set; }
        public uint value { get; set; }
    }
}
