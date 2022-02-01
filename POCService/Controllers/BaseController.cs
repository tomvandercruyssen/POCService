using System;
using System.Collections.Generic;
using System.Text;

namespace POCService.Controllers
{
    public interface BaseController
    {
        public void addReadings();
        public void removeReadings();
        public void addServer();
    }
}
