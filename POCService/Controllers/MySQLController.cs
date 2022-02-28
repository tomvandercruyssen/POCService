using POCService.Controllers.MySQL;

namespace POCService.Controllers
{
    public class MySQLController : BaseController
    {
        private readonly TagController _tagController = new TagController();
        private readonly ServerController _serverController = new ServerController();

        public void addReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void addServer(bool FirstTime)
        {
            _serverController.AddServer(FirstTime);
        }

        //number gebruiken
        public void removeReadings(int number, bool FirstTime)
        {
            _tagController.RemoveReadings(number, FirstTime);
        }

        public void addTag(bool FirstTime)
        {
            _tagController.AddTag(FirstTime);
        }
    }

}
