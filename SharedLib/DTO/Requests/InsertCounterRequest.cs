using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.DTO.Requests
{
    public class InsertCounterRequest
    {
        public List<Counter> counters { get; set; } = new List<Counter>();
    }
}
