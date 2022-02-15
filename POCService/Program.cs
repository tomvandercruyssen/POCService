using POCService.Controllers;
using POCService.Enums;
using POCService.Logging;
using System;
using System.Collections.Generic;

namespace POCService
{

    class Program
    {
        static void Main(string[] args)
        {
            BaseController _controller = new SQLiteController();
            bool _continue = true;

        Found:
            while (_continue)
            {
                try
                {
                    Console.WriteLine("Maak een keuze: [0] MySQL, [1] SQLite, [2] voor random data queries, [3] for analysis");
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
                        case TechnologiesEnum.RANDOM:
                            randomQuery();
                            break;
                        case TechnologiesEnum.ANALYSIS:
                            getLogs();
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
                    Console.WriteLine("choose: [0] Add Readings, [1] Remove Readings, [2] Add Tag, [3] Add Server, [4] To return");
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
                        case QueriesEnum.ESCAPE:
                            goto Found;
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

        static void randomQuery()
        {
            for (int i = 0; i < 100; i++)
            {
                BaseController _controller = new SQLiteController();
                Random rnd = new Random();
                string random1 = rnd.Next(2).ToString();
                TechnologiesEnum tech = Enum.Parse<TechnologiesEnum>(random1);
                switch (tech)
                {
                    case TechnologiesEnum.MySQL:
                        _controller = new MySQLController();
                        break;
                    case TechnologiesEnum.SQLite:
                        _controller = new SQLiteController();
                        break;
                    default:
                        break;
                }

                string random2 = rnd.Next(1).ToString();

                QueriesEnum query = Enum.Parse<QueriesEnum>(random2);
                int number = rnd.Next(2000, 100000);
                try
                {
                    switch (query)
                    {
                        case QueriesEnum.ADDREADINGS:
                            _controller.addReadings(number);
                            break;
                        case QueriesEnum.REMOVEREADINGS:
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
                    _controller.addReadings(100000);
                    Console.WriteLine(e);
                }
            }
        }

        static void getLogs()
        {
            Logger l = new Logger();
            var logs =  l.GetAllLogs();
            List<SpeedQuery> mysql = new List<SpeedQuery>();
            List<SpeedQuery> sqlite = new List<SpeedQuery>();
            foreach (var log in logs.Value)
            {
                SpeedQuery sq = new SpeedQuery();
                float avgSpeed = (float)log.Time / (float)log.AmountOfRecords;
                sq.avgSpeed = avgSpeed;
                sq.Query = log.Query;
                if(log.Database == 0)
                {
                    mysql.Add(sq);
                }
                else
                {
                    sqlite.Add(sq);
                }
            }
        }
    }
}


