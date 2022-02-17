using Microsoft.AspNetCore.Mvc;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace POCService.Controllers
{
    public interface BaseController
    {
        public void addReadings(int number, bool FirstTime);
        public void removeReadings(int number, bool FirstTime);
        public void addServer(bool FirstTime);
        public void addTag(bool FirstTime);
    }
}
