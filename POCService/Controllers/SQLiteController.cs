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

        private string query;
        public ActionResult<List<ServerDTO>> GetAllServers
        {
            get
            {
                var _context = new EdgeDataContext();
                //return _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
                return _context.Server.Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            }
        }

        public void addReadings(int numberOfReadings)
        {
            startTimer();
            query = "addReading n=" + numberOfReadings.ToString();
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();

            Guid test = servers[0].TagIds[0];
            for (int i = 0; i < numberOfReadings; i++)
            {
                addReading(test);
            }
            stopTimer();
        }

        public void addReading(Guid tagid)
        {
            var _context = new EdgeDataContext();
            var t = _context.Tag.Find(tagid);

            Random rnd = new Random();
            Reading reading = new Reading();
            reading.Created = DateTime.UtcNow;
            reading.Quality = "TEST";
            reading.StringValue = "stringvalue";
            reading.IntegerValue = rnd.Next(0, 100000000);
            reading.UnsignedIntegerValue = 6874833;
            reading.FloatValue = 633.5423;

            if (t is null)
            {
                return;
            }
            reading.Tag = t;
            try
            {
                _context.Reading.Add(reading);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                addReading(tagid);
                return;
            }
        }

        public void addServer()
        {
            startTimer();
            query = "addServer";
            using (var _context = new EdgeDataContext())
            {
                var s = new Server();
                var result = _context.Server.Add(s);
                _context.SaveChanges();
            }
            stopTimer();
        }

        public void removeReadings(int number)
        {
            startTimer();
            query = "removeReadings n=" + number.ToString();
            var _context = new EdgeDataContext();
            foreach (var item in _context.Reading)
            {
                try
                {
                    if ((DateTime.UtcNow - item.Created).TotalSeconds > 3600)
                    {
                        _context.Reading.Remove(item);
                    }
                }
                catch (Exception)
                {
                }
            }
            _context.SaveChanges();
        }

        public void addTag()
        {
            throw new NotImplementedException();
        }

        public void addTag(Guid id, TagDTO req)
        {
            var _context = new EdgeDataContext();
            try
            {
                var s = _context.Server.Find(id);
                if (s is null)
                {
                    //return BadRequest("No server was found with the given id");
                }
                var t = req.FromDTO();
                t.Server = s;
                var result = _context.Tag.Update(t);

                _context.SaveChanges();
                var entity = result.Entity;
                //return Ok(new TagDTO(entity));
            }
            catch (AggregateException)
            {
                //return BadRequest(e.InnerException.Message);
            }
            catch (Exception)
            {
                //return BadRequest(e.Message);
            }
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
            l.Database = "SQLite";
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
