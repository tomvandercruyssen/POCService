using Microsoft.AspNetCore.Mvc;
using SharedLib.DTOSQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace POCService.Controllers
{
    public interface BaseController
    {
        public void addReadings(int number);
        public void removeReadings(int number);
        public void addServer();

        public void addTag(Guid id, TagDTO req);

        public ActionResult<List<ServerDTO>> GetAllServers { get; }
    }
}
