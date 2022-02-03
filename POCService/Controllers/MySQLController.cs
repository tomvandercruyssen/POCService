using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.Controllers.MySQL;
using POCService.DataContexts.MySQL;
using SharedLib.Data;
using SharedLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers
{
    public class MySQLController : BaseController
    {
        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

        public ActionResult<List<ServerDTO>> GetAllServers { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        //mag weg
        ActionResult<List<SharedLib.DTOSQLite.ServerDTO>> BaseController.GetAllServers { 
            get => throw new NotImplementedException(); 
           // set => throw new NotImplementedException(); 
        }

        public void addReadings(int numberOfReadings)
        {
            Random rnd = new Random();
            Reading reading = new Reading();
            reading.Created = DateTime.UtcNow;
            reading.Quality = "quality";
            reading.StringValue = "stringvalue";
            reading.IntegerValue = rnd.Next(0, 100000000);
            reading.UnsignedIntegerValue = 6874833;
            reading.FloatValue = 633.5423;

            var context = new EdgeDataContext();
            var servers = context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            string test = servers[0].TagIds[0];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numberOfReadings; i++)
            {
                addReading(test, reading);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
        public void addReading(string tagid, Reading reading)
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

        //number gebruiken
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

        //tagcontroller?
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

        public void addTag(Guid id, SharedLib.DTOSQLite.TagDTO req)
        {
            throw new NotImplementedException();
        }
    }
}
