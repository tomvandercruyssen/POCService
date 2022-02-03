using System;
using System.Collections.Generic;
using System.Text;

namespace POCService.Logging
{
    public class Logger
    {
        System.Diagnostics.Stopwatch watch;
        public void startTimer()
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void stopTimer(int records, int query)
        {
            watch.Stop();
            LogQuery(watch.ElapsedMilliseconds, records, query);
        }

        public void LogQuery(long timeElapsed, int records, int query)
        {
            Log l = new Log();
            l.Database = "MySQL";
            l.Query = query;
            l.Time = (int)timeElapsed;
            l.AmountOfRecords = records;
            using (var _context = new LogsDataContext())
            {
                var result = _context.Logs.Add(l);
                _context.SaveChanges();
            }
        }
    }
}
