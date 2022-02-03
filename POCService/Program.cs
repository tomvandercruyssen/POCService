using System;
using POCService.Controllers;
using POCService.Enums;

namespace POCService
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseController _controller = new SQLiteController();
            bool _continue = true;

            while (_continue)
            {
                try
                {
                    Console.WriteLine("Maak een keuze: [0] MySQL of [1] SQLite");
                    string techQuery = Console.ReadLine();
                    TechnologiesEnum tech = Enum.Parse<TechnologiesEnum>(techQuery);
                    switch (tech)
                    {
                        case TechnologiesEnum.MySQL:
                            _controller = new MySQLController();
                            _continue = false;
                            break;
                        case TechnologiesEnum.SQLite:
                            _controller = new SQLiteController();
                            _continue = false;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Incorrect Input");
                }
            }

            _continue = true;
            while (_continue)
            {
                try
                {
                    Console.WriteLine("choose: [0] Add Readings, [1] Remove Readings, [2] Add Tag, [3] Add Server; [4] Show Servers");
                    string queryQuery = Console.ReadLine();
                    QueriesEnum query = Enum.Parse<QueriesEnum>(queryQuery);

                    switch (query)
                    {
                        case QueriesEnum.ADDREADINGS:
                            _controller.addReadings(500);
                            break;
                        case QueriesEnum.REMOVEREADINGS:
                            _controller.removeReadings(5000);
                            break;
                        case QueriesEnum.ADDTAG:
                            //_controller.addTag();
                            break;
                        case QueriesEnum.ADDSERVER:
                            _controller.addServer();
                            break;
                        case QueriesEnum.GETSERVER:
                            var servers = _controller.GetAllServers;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Incorrect Input");
                }
            }

        }

        

        
    }
}
