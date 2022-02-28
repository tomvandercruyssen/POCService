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

        public void addReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void addServer(bool FirstTime)
        {
            _serverController.addServer(FirstTime);
        }

        //number gebruiken
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
