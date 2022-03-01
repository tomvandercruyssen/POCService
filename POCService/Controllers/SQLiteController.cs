﻿using Microsoft.AspNetCore.Mvc;
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
    public class SQLiteController : IBaseController
    {

        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

        public void AddReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void AddServer(bool FirstTime)
        {
            _serverController.addServer(FirstTime);
        }

        //number gebruiken
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
