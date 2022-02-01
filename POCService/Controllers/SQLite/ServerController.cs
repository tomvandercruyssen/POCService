using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.SQLite;
using SharedLib.Data.SQLite;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers.SQLite
{

    public class ServerController : ControllerBase
    {
        public ActionResult<List<ServerDTO>> GetAllServers()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            return servers;
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
        public ActionResult<ServerDTO> UpdateServer(ServerDTO req)
        {
            var _context = new EdgeDataContext();
            try
            {
                var s = req.FromDTO();

                s.Credentials = new ServerCredentials()
                {
                    Username = "tom",
                    Password = "azerty123"
                };

                var result = _context.Server.Update(s);
                _context.SaveChanges();
                var entity = result.Entity;
                return Ok(new ServerDTO(result.Entity));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
