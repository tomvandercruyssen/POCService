using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedLib.DTO;
using SharedLib.Data;
using POCService.DataContexts.MySQL;

namespace POCService.Controllers.MySQL
{
    public class TagController : ControllerBase
    {
        private ServerController _serverController = new ServerController();

        public ActionResult<List<TagDTO>> GetAllTags()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var _context = new EdgeDataContext();
            try
            {
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                return Ok(_context.Tag.Include(t => t.Server).Select(t => new TagDTO(t)).ToList());
            }
            catch (Exception e)
            {
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                return BadRequest(e.Message);
            }
        }
        public ActionResult<TagDTO> addTag()
        {
            var _context = new EdgeDataContext();
            try
            {
                var s = _serverController.getServer().Value[0];
                if (s is null)
                {
                    return BadRequest("No server was found with the given id");
                }
                var tag = new TagDTO()
                {
                    Name = "tag1",
                    NodeId = "ns=2;s=len009",
                    Type = "uint",
                    BufferHours = (uint)1,
                    SamplingInterval = (uint)1000,
                    QueueSize = (uint)1000,
                    DiscardOldest = true
                };
                var t = tag.FromDTO();
                t.Server = s;
                var result = _context.Tag.Update(t);

                _context.SaveChanges();
                var entity = result.Entity;
                return Ok(new TagDTO(entity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        Random rnd = new Random();
        public void addReadings(int numberOfReadings)
        {
            //query = "addReading n=" + numberOfReadings.ToString();
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            string id = servers[0].TagIds[0];
            for (int i = 0; i < numberOfReadings; i++)
            {
                addReading(id);
            }
        }
        public void addReading(string tagid)
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
                var result = _context.Reading.Add(reading);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                addReading(tagid);
                return;
            }
        }

        public void removeReadings()
        {

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
            var result = _context.SaveChanges();
        }

    }
}
