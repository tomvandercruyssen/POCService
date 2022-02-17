using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.MySQL;
using SharedLib.Data;
using SharedLib.DTO;
using SharedLib.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using POCService.Logging;
using POCService.Enums;

namespace POCService.Controllers.MySQL
{
    
    public class ServerController : ControllerBase
    {
        Logger log = new Logger();
        public ActionResult<List<Server>> getServer()
        {
            var _context = new EdgeDataContext();
            var servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).ToList();
            return servers;
        }

        public void addServer(bool FirstTime)
        {
            int amountRecords;
            log.startTimer();
            using (var _context = new EdgeDataContext())
            {
                var s = new Server();
                var result = _context.Server.Add(s);
                amountRecords = _context.SaveChanges();
            }            
            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.MySQL, FirstTime);
        }
    }
}
