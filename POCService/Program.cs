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
                    Console.WriteLine("choose: [0] Add Readings, [1] Remove Readings, [2] Add Tag, [3] Add Server");
                    string queryQuery = Console.ReadLine();
                    QueriesEnum query = Enum.Parse<QueriesEnum>(queryQuery);
                    int number;                   

                    switch (query)
                    {
                        case QueriesEnum.ADDREADINGS:
                            Console.WriteLine("Hoeveel readings?");
                            number = int.Parse(Console.ReadLine());
                            _controller.addReadings(number);                            
                            break;
                        case QueriesEnum.REMOVEREADINGS:
                            Console.WriteLine("Hoeveel readings?");
                            number = int.Parse(Console.ReadLine());
                            _controller.removeReadings(number);
                            break;
                        case QueriesEnum.ADDTAG:
                            _controller.addTag();
                            break;
                        case QueriesEnum.ADDSERVER:
                            _controller.addServer();
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
