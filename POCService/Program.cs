using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using POCService.ControllersMysql;
using SharedLib.Data;
using SharedLib.DTO;
using SharedLib.DTO.Requests;

namespace POCService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Maak een keuze: sqlite of mysql");
            ServerControllerS sc = new ServerControllerS();
            TagControllerS tc = new TagControllerS();
            Console.WriteLine("choose: addReadings2, removeReadings, addTag, AddServer");            
            //StartUp su = new StartUp();
            //var context = new EdgeDataContextMysql();
            //var servers = context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            //var servers = context.Server.Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            //Guid test = servers[0].TagIds[0];

            Console.WriteLine("start waarden voltooid");
            string query = Console.ReadLine();
            switch (query)
            {
                case "addReadings2":
                    tc.addReadings2();
                    break;

                case "removeReadings":
                    tc.removeReadings();
                    break;

                case "addServer":
                    sc.addServer();
                    break;

                default:
                    Console.WriteLine("can't find query");
                    break;
            }
        }     

        
    }
}
