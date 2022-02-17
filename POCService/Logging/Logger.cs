using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.Enums;
using POCService.SharedLib.DTOSQLite;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace POCService.Logging
{
    public class Logger: ControllerBase
    {
        System.Diagnostics.Stopwatch watch;
        public void startTimer()
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void stopTimer(int records, QueriesEnum query, TechnologiesEnum database, bool FirstTime)
        {
            watch.Stop();
            LogQuery(watch.ElapsedMilliseconds, records, query, database,FirstTime);
        }


        public void LogQuery(long timeElapsed, int records, QueriesEnum query, TechnologiesEnum database, bool FirstTime)
        {
            Log l = new Log();
            l.Database = (int)database;
            l.Query = (int)query;
            l.Time = (int)timeElapsed;
            l.AmountOfRecords = records;
            l.FirstTime = FirstTime;
            using (var _context = new LogsDataContext())
            {
                var result = _context.Logs.Add(l);
                _context.SaveChanges();
            }
        }

        public ActionResult<List<Log>> GetAllLogs()
        {
            var _context = new LogsDataContext();
            try
            {
                var logs = _context.Logs.ToList();
                return logs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
