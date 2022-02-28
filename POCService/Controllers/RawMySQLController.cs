using POCService.Controllers.RawMySQL;
using SharedLib.Data;
using SharedLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using POCService.Logging;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;

namespace POCService.Controllers
{
    public class RawMySQLController : BaseController
    {
        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

        public void addReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void addServer(bool FirstTime)
        {
            _serverController.AddServer(FirstTime);
        }
        public void removeReadings(int number, bool FirstTime)
        {
            _tagController.RemoveReadings(number, FirstTime);
        }

        public void addTag(bool FirstTime)
        {
            _tagController.AddTag(FirstTime);
        }
    }

}