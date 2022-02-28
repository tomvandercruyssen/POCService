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
            bool FirstTime = true;
        Found:
            while (_continue)
            {
                try
                {
                    Console.WriteLine("Maak een keuze: [0] MySQL, [1] SQLite, [2] RawMysql, [3] voor random data queries, [4] for analysis");
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
                        case TechnologiesEnum.RAWMYSQL:
                            _controller = new RawMySQLController();
                            _continue = false;
                            break;
                        case TechnologiesEnum.RANDOM:
                            RandomQuery();
                            break;
                        case TechnologiesEnum.ANALYSIS:
                            GetLogs();
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
                            _controller.addReadings(number, FirstTime);
                            break;
                        case QueriesEnum.REMOVEREADINGS:
                            Console.WriteLine("Hoeveel readings?");
                            number = int.Parse(Console.ReadLine());
                            _controller.removeReadings(number, FirstTime);
                            break;
                        case QueriesEnum.ADDTAG:
                            _controller.addTag(FirstTime);
                            break;
                        case QueriesEnum.ADDSERVER:
                            _controller.addServer(FirstTime);
                            break;
                        case QueriesEnum.ESCAPE:
                            goto Found;
                        default:
                            break;
                    }
                    FirstTime = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void RandomQuery()
        {
            bool FirstTime = true;
            for (int i = 0; i < 100; i++)
            {
                BaseController _controller = new SQLiteController();
                Random rnd = new Random();
                string random1 = rnd.Next(3).ToString();
                TechnologiesEnum tech = Enum.Parse<TechnologiesEnum>(random1);
                switch (tech)
                {
                    case TechnologiesEnum.MySQL:
                        _controller = new MySQLController();
                        break;
                    case TechnologiesEnum.SQLite:
                        _controller = new SQLiteController();
                        break;
                    case TechnologiesEnum.RAWMYSQL:
                        _controller = new RawMySQLController();
                        break;
                    default:
                        break;
                }

                string random2 = rnd.Next(4).ToString();

                QueriesEnum query = Enum.Parse<QueriesEnum>(random2);
                int number = rnd.Next(100, 50000);
                try
                {
                    switch (query)
                    {
                        case QueriesEnum.ADDREADINGS:
                            _controller.addReadings(number, FirstTime);
                            break;
                        case QueriesEnum.REMOVEREADINGS:
                            _controller.removeReadings(number, FirstTime);
                            break;
                        case QueriesEnum.ADDTAG:
                            _controller.addTag(FirstTime);
                            break;
                        case QueriesEnum.ADDSERVER:
                            _controller.addServer(FirstTime);
                            break;
                        default:
                            break;
                    }
                }

                
                catch (Exception e)
                {
                    _controller.addReadings(100000, FirstTime);
                    Console.WriteLine(e);
                }
                FirstTime = false;
            }
        }

        static void GetLogs()
        {
            Logger l = new Logger();
            var logs =  l.GetAllLogs();
            List<SpeedQuery> mysql = new List<SpeedQuery>();
            List<SpeedQuery> sqlite = new List<SpeedQuery>();
            List<SpeedQuery> rawsql = new List<SpeedQuery>();
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
                else if(log.Database == 1)
                {
                    sqlite.Add(sq);
                }
                else
                {
                    rawsql.Add(sq);
                }
            }

            int i0 = 0, i1 = 0, i2 = 0, i3 = 0;
            float avgQuery0 = 0, avgQuery1 = 0, avgQuery2 = 0, avgQuery3 = 0;

            foreach (var item in mysql)
            {
                switch (item.Query)
                {
                    case 0:
                        i0++;
                        avgQuery0 += item.avgSpeed;
                        break;
                    case 1:
                        i1++;
                        avgQuery1 += item.avgSpeed;
                        break;
                    case 2:
                        i2++;
                        avgQuery2 += item.avgSpeed;
                        break;
                    case 3:
                        i3++;
                        avgQuery3 += item.avgSpeed;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("De gemiddelde snelheid van Query 0 in MysSQL is: " + (avgQuery0 / i0));
            Console.WriteLine("De gemiddelde snelheid van Query 1 in MysSQL is: " + (avgQuery1 / i1));
            Console.WriteLine("De gemiddelde snelheid van Query 2 in MysSQL is: " + (avgQuery2 / i2));
            Console.WriteLine("De gemiddelde snelheid van Query 3 in MysSQL is: " + (avgQuery3 / i3));

            i0 = 0; i1 = 0;i2 = 0; i3 = 0;
            avgQuery0 = 0; avgQuery1 = 0; avgQuery2 = 0; avgQuery3 = 0;
            foreach (var item in sqlite)
            {
                switch (item.Query)
                {
                    case 0:
                        i0++;
                        avgQuery0 += item.avgSpeed;
                        break;
                    case 1:
                        i1++;
                        avgQuery1 += item.avgSpeed;
                        break;
                    case 2:
                        i2++;
                        avgQuery2 += item.avgSpeed;
                        break;
                    case 3:
                        i3++;
                        avgQuery3 += item.avgSpeed;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("De gemiddelde snelheid van Query 0 in SQLite is: " + (avgQuery0 / i0));
            Console.WriteLine("De gemiddelde snelheid van Query 1 in SQLite is: " + (avgQuery1 / i1));
            Console.WriteLine("De gemiddelde snelheid van Query 2 in SQLite is: " + (avgQuery2 / i2));
            Console.WriteLine("De gemiddelde snelheid van Query 3 in SQLite is: " + (avgQuery3 / i3));

            i0 = 0; i1 = 0; i2 = 0; i3 = 0;
            avgQuery0 = 0; avgQuery1 = 0; avgQuery2 = 0; avgQuery3 = 0;
            foreach (var item in rawsql)
            {
                switch (item.Query)
                {
                    case 0:
                        i0++;
                        avgQuery0 += item.avgSpeed;
                        break;
                    case 1:
                        i1++;
                        avgQuery1 += item.avgSpeed;
                        break;
                    case 2:
                        i2++;
                        avgQuery2 += item.avgSpeed;
                        break;
                    case 3:
                        i3++;
                        avgQuery3 += item.avgSpeed;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("De gemiddelde snelheid van Query 0 in rawsql is: " + (avgQuery0 / i0));
            Console.WriteLine("De gemiddelde snelheid van Query 1 in rawsql is: " + (avgQuery1 / i1));
            Console.WriteLine("De gemiddelde snelheid van Query 2 in rawsql is: " + (avgQuery2 / i2));
            Console.WriteLine("De gemiddelde snelheid van Query 3 in rawsql is: " + (avgQuery3 / i3));
        }
    }
}


