//using POCService.DataContexts.MySQL;
using SharedLib.Data;
using SharedLib.DTO;
using SharedLib.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using POCService.Logging;
using POCService.Enums;
using System.Data.SqlClient;

//namespace POCService.Controllers.RawMySQL
//{

//    public class ServerController 
//    {
//        Logger log = new Logger();
//        public List<Server> getServer()
//        {
//            SqlConnection conn = new SqlConnection();
//            conn.Open();

//            SqlCommand cmd = new SqlCommand("SELECT * FROM Person", conn);
//            SqlDataReader reader = cmd.ExecuteReader();

//            List<Server> servers = new List<Server>();

//            while (reader.Read())
//            {
//                Server p = new Server(reader, conn);
//                servers.Add(p);
//            }

//            return servers;
//        }

//        public void addServer()
//        {
//            int amountRecords;
//            log.startTimer();
//            using (var _context = new EdgeDataContext())
//            {
//                var s = new Server();
//                var result = _context.Server.Add(s);
//                amountRecords = _context.SaveChanges();
//            }
//            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.MySQL);
//        }
//    }
//}