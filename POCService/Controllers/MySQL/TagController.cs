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

        public List<Tag> GetAllTags()
        {
            System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
            EdgeDataContext _context = new EdgeDataContext();
            List<Tag> tags = _context.Tag.ToList();
            return tags;
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
            List<Tag> tags = GetAllTags();
            Random rnd = new Random();
            for (int i = 0; i < numberOfReadings; i++)
            {
                string tagid = tags[rnd.Next(0, tags.Count)].TagId;
                string stringDate = DateTime.UtcNow.ToString();
                Reading reading = new Reading
                {
                    Created = DateTime.Parse(stringDate),
                    Quality = "MYSQL",
                    StringValue = "MYSQL",
                    IntegerValue = rnd.Next(0, 100000000),
                    UnsignedIntegerValue = 6874833,
                    FloatValue = 633.5423
                };

                if (tagid is null)
                {
                    return;
                }
                reading.TagId = tagid;
                try
                {
                    _context.Reading.Add(reading);
                }
                catch (DbUpdateException)
                {
                    return;
                }
            }
            int amountRecords = _context.SaveChanges();
            log.stopTimer(numberOfReadings, QueriesEnum.ADDREADINGS, TechnologiesEnum.MySQL, FirstTime);
        }

        //public void RemoveReadings(int number, bool FirstTime)
        //{
        //    log.startTimer();
        //    EdgeDataContext _context = new EdgeDataContext();
        //    List<Reading> readingList = _context.Reading.ToList();
        //    for (int i = 0; i < number; i++)
        //    {
        //        _context.Reading.Remove(readingList[i]);
        //    }
        //    int amountRecords = _context.SaveChanges();
        //    log.stopTimer(amountRecords, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.MySQL, FirstTime);
        //}
        public void RemoveReadings(int bufferTijd, bool FirstTime)
        {
            log.startTimer();
            Random rnd = new Random();
            EdgeDataContext _context = new EdgeDataContext();
            List<Reading> readingList = _context.Reading.ToList();
            List<Tag> tags = _context.Tag.ToList();
            string tagId = tags[rnd.Next(0, tags.Count)].TagId;
            foreach (Reading reading in readingList)
            {
                int timeAlive = DateTime.Now.Subtract(reading.Created).Hours;
                if ( reading.TagId == tagId && timeAlive > bufferTijd)
                {
                    _context.Reading.Remove(reading);
                }
            }
            int amountRecords = _context.SaveChanges();
            log.stopTimer(amountRecords, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.MySQL, FirstTime);
        }


    }
}
