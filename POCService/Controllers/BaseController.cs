namespace POCService.Controllers
{
    public interface IBaseController
    {
        public void AddReadings(int number, bool FirstTime);
        public void RemoveReadings(int number, bool FirstTime);
        public void AddServer(bool FirstTime);
        public void AddTag(bool FirstTime);
    }
}
