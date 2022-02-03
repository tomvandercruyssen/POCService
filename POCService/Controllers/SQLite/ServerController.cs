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
        public ActionResult<List<Server>> getServer()
        {
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).ToList();
            return servers;
        }

        public void addServer()
        {
            using (var _context = new EdgeDataContext())
            {
                var s = new Server();
                var result = _context.Server.Add(s);
                _context.SaveChanges();
            }
        }
    }
}
