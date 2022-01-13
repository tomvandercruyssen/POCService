using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using SharedLib.DTO;
using SharedLib.DTO.Requests;
using SharedLib.DTO.Responses;
using SharedLib.Data;
using System.Diagnostics;
using System.Timers;
using SQLite;
using SharedLib.DTOSQLite;

namespace POCService.ControllersMysql
{
    public class TagControllerS : ControllerBase
    {
        
        public ActionResult<List<TagDTO>> GetAllTags()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var _context = new EdgeDataContextMysql();
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
        public ActionResult<TagDTO> UpdateTag(Guid id, TagDTO req)
        {
            var _context = new EdgeDataContextMysql();
            try
            {
                var s = _context.Server.Find(id);
                if (s is null)
                {
                    return BadRequest("No server was found with the given id");
                }
                var t = req.FromDTO();
                t.Server = s;
                var result = _context.Tag.Update(t);

                _context.SaveChanges();
                var entity = result.Entity;
                return Ok(new TagDTO(entity));
            }
            catch (AggregateException e)
            {
                return BadRequest(e.InnerException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        public void addReading(Guid tagid, Reading reading)
        {
            var ctx = new EdgeDataContextMysql();
            var t = ctx.Tag.Find(tagid);
            if (t is null)
            {
                return;
            }
            reading.Tag = t;
            try
            {
                ctx.Reading.Update(reading);
                ctx.SaveChanges();
            }
            catch (DbUpdateException)
            {
                addReading(tagid, reading);
                return;
            }
        }
        public void deleteOldReadings()
        {
            var ctx = new EdgeDataContextMysql();
            //var range = ctx.Reading.Where()
            //var range = ctx.WebServiceCall.Where(c => c.WebService.WebServiceId.Equals(item.WebServiceId)).OrderBy(c => c.Created).Take(1000).Where(c => c.Created.CompareTo(DateTime.UtcNow.AddHours(-item.HistoryHours)) < 0);
        }

        Random rnd = new Random();
        public void addReadings2()
        {            
            Reading reading = new Reading();
            reading.Created = DateTime.UtcNow;
            reading.Quality = "quality";
            reading.StringValue = "stringvalue";
            reading.IntegerValue = rnd.Next(0,100000000);
            reading.UnsignedIntegerValue = 6874833;
            reading.FloatValue = 633.5423;

            var context = new EdgeDataContextSqlite();
            var servers = context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTOS(s)).ToList();
            Guid test = servers[0].TagIds[0];
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 400000; i++)
            {
                addReading(test, reading);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        public void removeReadings()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var ctx = new EdgeDataContextMysql();
            int count = 0;
            foreach (var item in ctx.Reading)
            {
                try
                {
                    if((DateTime.UtcNow - item.Created).TotalSeconds > 3600) {
                        ctx.Reading.Remove(item);
                    }
                }
                catch (Exception e)
                {
                }
            }
            
            ctx.SaveChanges(); //ctx.SaveChanges().Compareto //oldCTX of oldCTX.count
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

    }
}
