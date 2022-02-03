using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.Controllers.SQLite;
using POCService.DataContexts.SQLite;
using SharedLib.Data.SQLite;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POCService.Controllers
{
    public class SQLiteController : BaseController
    {

        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

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
            Random rnd = new Random();
            Reading reading = new Reading();
            reading.Created = DateTime.UtcNow;
            reading.Quality = "TEST";
            reading.StringValue = "stringvalue";
            reading.IntegerValue = rnd.Next(0, 100000000);
            reading.UnsignedIntegerValue = 6874833;
            reading.FloatValue = 633.5423;

            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();

            Guid test = servers[0].TagIds[0];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numberOfReadings; i++)
            {
                addReading(test, reading);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
        public void addReading(Guid tagid, Reading reading)
        {
            var _context = new EdgeDataContext();
            var t = _context.Tag.Find(tagid);
            if (t is null)
            {
                return;
            }
            reading.Tag = t;
            try
            {
                _context.Reading.Update(reading);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                addReading(tagid, reading);
                return;
            }
        }

        public void addServer()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            using (var _context = new EdgeDataContext())
            {
                var s = new Server();
                var result = _context.Server.Add(s);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
        }

        public void removeReadings(int number)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var ctx = new EdgeDataContext();
            foreach (var item in ctx.Reading)
            {
                try
                {
                    if ((DateTime.UtcNow - item.Created).TotalSeconds > 3600)
                    {
                        ctx.Reading.Remove(item);
                    }
                }
                catch (Exception)
                {
                }
            }

            ctx.SaveChanges(); //ctx.SaveChanges().Compareto //oldCTX of oldCTX.count
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
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
    }
}
