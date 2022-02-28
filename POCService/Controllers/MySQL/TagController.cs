using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.MySQL;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data;
using SharedLib.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers.MySQL
{
    public class TagController : ControllerBase
    {
        private readonly ServerController _serverController = new ServerController();
        private readonly Logger log = new Logger();

        public ActionResult<List<TagDTO>> GetAllTags()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            EdgeDataContext _context = new EdgeDataContext();
            try
            {
                watch.Stop();
                long elapsedMs = watch.ElapsedMilliseconds;
                return Ok(_context.Tag.Include(t => t.ServerId).Select(t => new TagDTO(t)).ToList());
            }
            catch (Exception e)
            {
                watch.Stop();
                long elapsedMs = watch.ElapsedMilliseconds;
                return BadRequest(e.Message);
            }
        }

        public ActionResult<TagDTO> AddTag(bool FirstTime)
        {
            log.startTimer();
            EdgeDataContext _context = new EdgeDataContext();
            try
            {
                Server s = _serverController.GetServer().Value[0];
                if (s is null)
                {
                    return BadRequest("No server was found with the given id");
                }
                TagDTO tag = new TagDTO()
                {
                    Name = "tag1",
                    NodeId = "ns=2;s=len009",
                    Type = "uint",
                    BufferHours = 1,
                    SamplingInterval = 1000,
                    QueueSize = 1000,
                    DiscardOldest = true
                };
                Tag t = tag.FromDTO();
                t.ServerId = s.ServerId;
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Tag> result = _context.Tag.Update(t);

                int amountRecords = _context.SaveChanges();
                Tag entity = result.Entity;
                log.stopTimer(amountRecords, QueriesEnum.ADDTAG, TechnologiesEnum.MySQL, FirstTime);
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
            EdgeDataContext _context = new EdgeDataContext();
            List<ServerDTO> servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            string id = servers[0].TagIds[0];
            for (int i = 0; i < numberOfReadings; i++)
            {
                AddReading(id);
            }
            log.stopTimer(numberOfReadings, QueriesEnum.ADDREADINGS, TechnologiesEnum.MySQL, FirstTime);
        }

        public void AddReading(string tagid)
        {
            EdgeDataContext _context = new EdgeDataContext();
            Tag t = _context.Tag.Find(tagid);
            string stringDate = DateTime.UtcNow.ToString();

            Random rnd = new Random();
            Reading reading = new Reading
            {
                Created = DateTime.Parse(stringDate),
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
                AddReading(tagid);
                return;
            }
        }

        public void RemoveReadings(int number, bool FirstTime)
        {
            log.startTimer();
            EdgeDataContext _context = new EdgeDataContext();
            DbSet<Reading> readings = _context.Reading;
            List<Reading> readingList = readings.ToList();
            for (int i = 0; i < number; i++)
            {
                _context.Reading.Remove(readingList[i]);
            }
            int amountRecords = _context.SaveChanges();
            log.stopTimer(amountRecords, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.MySQL, FirstTime);
        }

    }
}
