using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POCService.DataContexts.MySQL;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data;
using System.Collections.Generic;
using System.Linq;

namespace POCService.Controllers.MySQL
{

    public class ServerController : ControllerBase
    {
        private readonly Logger log = new Logger();
        public ActionResult<List<Server>> GetServer()
        {
            EdgeDataContext _context = new EdgeDataContext();
            List<Server> servers = _context.Server.Include(s => s.Credentials).Include(s => s.Tags).ToList();
            return servers;
        }

        public void AddServer(bool FirstTime)
        {
            int amountRecords;
            log.startTimer();
            using (EdgeDataContext _context = new EdgeDataContext())
            {
                Server s = new Server();
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Server> result = _context.Server.Add(s);
                amountRecords = _context.SaveChanges();
            }
            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.MySQL, FirstTime);
        }
    }
}
