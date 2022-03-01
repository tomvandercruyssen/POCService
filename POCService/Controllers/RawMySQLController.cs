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
    public class RawMySQLController : IBaseController
    {
        private readonly TagController _tagController = new TagController();
        private readonly ServerController _serverController = new ServerController();

        public void AddReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void AddServer(bool FirstTime)
        {
            _serverController.AddServer(FirstTime);
        }
        public void RemoveReadings(int number, bool FirstTime)
        {
            _tagController.RemoveReadings(number, FirstTime);
        }

        public void AddTag(bool FirstTime)
        {
            _tagController.AddTag(FirstTime);
        }
    }

}