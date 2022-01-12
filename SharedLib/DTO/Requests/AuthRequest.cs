using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedLib.DTO.Requests
{
    public class AuthRequest
    {
        public string password { get; set; }
        public string uuid { get; set; }
    }
}
