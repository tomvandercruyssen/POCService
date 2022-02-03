using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.MySQL;
using SharedLib.Data;
using SharedLib.DTO;
using SharedLib.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers.MySQL
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
