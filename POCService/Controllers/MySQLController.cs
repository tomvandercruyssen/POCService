using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.Controllers.MySQL;
using POCService.DataContexts.MySQL;
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
    public class MySQLController : BaseController
    {
        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

        public void addReadings(int numberOfReadings)
        {
            _tagController.addReadings(numberOfReadings);
        }        

        public void addServer()
        {
            _serverController.addServer();
        }

        //number gebruiken
        public void removeReadings(int number)
        {
            _tagController.removeReadings(number);
        }

        public void addTag()
        {
            _tagController.addTag();
        }
    }

}
