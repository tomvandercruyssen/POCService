using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.SQLite;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data.SQLite;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers.SQLite
{
    public class TagController : ControllerBase
    {

        private readonly ServerController _serverController = new ServerController();
        readonly Logger log = new Logger();

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

        public ActionResult<TagDTO> AddTag(bool FirstTime)
        {
            log.startTimer();
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

                int amountRecords = _context.SaveChanges();
                var entity = result.Entity;
                log.stopTimer(amountRecords, QueriesEnum.ADDTAG, TechnologiesEnum.SQLite, FirstTime);
                return Ok(new TagDTO(entity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public void AddReadings(int numberOfReadings, bool FirstTime)
        {
            log.startTimer();
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            Guid id = servers[0].TagIds[0];
            for (int i = 0; i < numberOfReadings; i++)
            {
                AddReading(id, FirstTime);
            }
            log.stopTimer(numberOfReadings, QueriesEnum.ADDREADINGS, TechnologiesEnum.SQLite, FirstTime);
        }

        public void AddReading(Guid tagid, bool FirstTime)
        {
            var _context = new EdgeDataContext();
            var t = _context.Tag.Find(tagid);

            Random rnd = new Random();
            Reading reading = new Reading
            {
                Created = DateTime.UtcNow,
                Quality = "TEST",
                StringValue = "stringvalue",
                IntegerValue = rnd.Next(0, 100000000),
                UnsignedIntegerValue = 6874833,
                FloatValue = 633.5423
            };

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
                AddReading(tagid, FirstTime);
                return;
            }
        }

        public void RemoveReadings(int number, bool FirstTime)
        {
            log.startTimer();
            var _context = new EdgeDataContext();
            var readingList = _context.Reading.ToList();
            for (int i = 0; i < number; i++)
            {
                _context.Reading.Remove(readingList[i]);
            }
            int amountRecords = _context.SaveChanges();
            log.stopTimer(amountRecords, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.SQLite, FirstTime);
        }

    }
}
