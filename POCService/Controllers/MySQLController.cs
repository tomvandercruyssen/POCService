using POCService.Controllers.MySQL;

namespace POCService.Controllers
{
    public class MySQLController : IBaseController
    {
        private readonly TagController _tagController = new TagController();
        private readonly ServerController _serverController = new ServerController();

        public void AddReadings(int numberOfReadings, bool FirstTime)
        {
            _tagController.AddReadings(numberOfReadings, FirstTime);
        }

        public void AddServer(bool FirstTime)
        {
            _serverController.AddServer(FirstTime);
        }

        //number gebruiken
        public void RemoveReadings(int number, bool FirstTime)
        {
            _tagController.RemoveReadings(number, FirstTime);
        }

        public void AddTag(bool FirstTime)
        {
            _tagController.AddTag(FirstTime);
        }
    }

}
