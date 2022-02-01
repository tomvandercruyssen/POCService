using POCService.Controllers.MySQL;
using System;

namespace POCService.Controllers
{
    public class MySQLController : BaseController
    {
        private TagController _tagController = new TagController();
        private ServerController _serverController = new ServerController();

        public void addReadings()
        {
            throw new NotImplementedException();
        }

        public void addServer()
        {
            throw new NotImplementedException();
        }

        public void removeReadings()
        {
            throw new NotImplementedException();
        }
    }
}
