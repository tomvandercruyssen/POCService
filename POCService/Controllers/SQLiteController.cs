using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.Controllers.SQLite;
using POCService.DataContexts.SQLite;
using SharedLib.Data.SQLite;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using POCService.Logging;
namespace POCService.Controllers
{
    public class SQLiteController : BaseController
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
