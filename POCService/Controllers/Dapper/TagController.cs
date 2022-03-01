using Dapper;
using MySqlConnector;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data;
using System;
using System.Collections.Generic;
using Z.Dapper.Plus;
using System.Data;
using System.Text;

namespace POCService.Controllers.Dapper
{
    public class TagController
    {
        private readonly ServerController _serverController = new ServerController();
        private readonly string conn = "server = localhost; port = 3306; user = root; password = Azerty123; database = poc; default command timeout = 100";
        private readonly Logger log = new Logger();

        public void AddTag(bool firsttime)
        {
            log.startTimer();
            Tag t = new Tag();
            int amountRecords = 1;
            try
            {
                List<string> serverIDs = _serverController.GetServersIDs();

                Guid TagId = Guid.NewGuid();
                string Name = t.Name;
                Guid NodeId = Guid.NewGuid();
                string Type = t.Type;
                uint BufferHours = t.BufferHours;
                uint SamplingInterval = t.SamplingInterval;
                uint QueueSize = t.QueueSize;
                bool DiscardOldest = t.DiscardOldest;
                string ServerId = serverIDs[0];

                using (MySqlConnection mConnection = new MySqlConnection(conn))
                {
                    mConnection.Open();
                    mConnection.Execute(@"INSERT INTO tag (TagId, Name, NodeId, Type, BufferHours, SamplingInterval, QueueSize, DiscardOldest, ServerId)
                                VALUES (@TagId, @Name, @NodeId, @Type, @BufferHours, @SamplingInterval, @QueueSize, @DiscardOldest, @ServerId);", new { TagId, Name, NodeId, Type, BufferHours, SamplingInterval, QueueSize, DiscardOldest, ServerId });
                }
                log.stopTimer(amountRecords, QueriesEnum.ADDTAG, TechnologiesEnum.DAPPER, firsttime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddReadings(int numberOfReadings, bool firstime)
        {
            log.startTimer();
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                List<Reading> Rows = new List<Reading>();
                string stringDate = DateTime.UtcNow.ToString();
                Random rnd = new Random();
                
                for (int i = 0; i < numberOfReadings; i++)
                {
                    Reading reading = new Reading
                    {
                        ReadingId = Guid.NewGuid().ToString(),
                        Created = DateTime.Parse(stringDate),
                        Quality = "MYSQL",
                        StringValue = "MYSQL",
                        IntegerValue = rnd.Next(0, 100000000),
                        UnsignedIntegerValue = 6874833,
                        FloatValue = 633.5423
                    };
                    Rows.Add(reading);
                }
                mConnection.BulkInsert(Rows);
            }
            log.stopTimer(numberOfReadings, QueriesEnum.ADDREADINGS, TechnologiesEnum.DAPPER, firstime);
        }

        public void RemoveReadings(int number, bool FirstTime)
        {
            log.startTimer();
            string cmdStr = @"DELETE FROM reading limit " + number;
            Console.WriteLine(cmdStr);
            using MySqlConnection mConnection = new MySqlConnection(conn);
            mConnection.Open();
            using (MySqlCommand myCmd = new MySqlCommand(cmdStr, mConnection))
            {
                myCmd.ExecuteNonQuery();
            }
            mConnection.Close();
            log.stopTimer(number, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.DAPPER, FirstTime);
        }
    }
}
