using SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.DTO.Requests
{
    public class FilterLogRequest
    {
        public Dictionary<LogFilters, string> filters { get; set; }
        public int maxAmount { get; set; }
    }
}
