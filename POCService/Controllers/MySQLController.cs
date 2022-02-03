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

        private string query;

        public void addReadings(int numberOfReadings)
        {
            startTimer();
            _tagController.addReadings(numberOfReadings);
            query = "addReading n=" + numberOfReadings.ToString();
            stopTimer();
        }        

        public void addServer()
        {
            startTimer();
            _serverController.addServer();
            stopTimer();
        }

        //number gebruiken
        public void removeReadings(int number)
        {
            startTimer();
            query = "removeReadings n=" + number.ToString();
            stopTimer();
        }

        public void addTag()
        {
            _tagController.addTag();
        }

        System.Diagnostics.Stopwatch watch;
        public void startTimer()
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void stopTimer()
        {
            watch.Stop();
            LogQuery(watch.ElapsedMilliseconds);

        }
        public void LogQuery(long timeElapsed)
        {
            Log l = new Log();
            l.Database = "MySQL";
            l.Query = query;
            l.Time = (int)timeElapsed;

            using (var _context = new LogsDataContext())
            {
                var result = _context.Logs.Add(l);
                _context.SaveChanges();
            }
        }
    }

}
