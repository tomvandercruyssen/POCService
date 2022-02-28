using MySqlConnector;
using POCService.Enums;
//using POCService.DataContexts.MySQL;
using POCService.Logging;
using SharedLib.Data;
using System;
using System.Collections.Generic;
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
                List<string> s = _serverController.GetServers();
                string cmdStr = @"INSERT INTO tag (TagId, Name, NodeId, Type, BufferHours, SamplingInterval, QueueSize, DiscardOldest, ServerId)
                                VALUES (@TagId, @Name, @NodeId, @Type, @BufferHours, @SamplingInterval, @QueueSize, @DiscardOldest, @ServerId);";
                Console.WriteLine(cmdStr);
                using MySqlConnection mConnection = new MySqlConnection(conn);
                mConnection.Open();
                using (MySqlCommand myCmd = new MySqlCommand(cmdStr, mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    myCmd.Parameters.AddWithValue("@TagId", Guid.NewGuid());
                    myCmd.Parameters.AddWithValue("@Name", t.Name);
                    myCmd.Parameters.AddWithValue("@NodeId", Guid.NewGuid());
                    myCmd.Parameters.AddWithValue("@Type", t.Type);
                    myCmd.Parameters.AddWithValue("@BufferHours", t.BufferHours);
                    myCmd.Parameters.AddWithValue("@SamplingInterval", t.SamplingInterval);
                    myCmd.Parameters.AddWithValue("@QueueSize", t.QueueSize);
                    myCmd.Parameters.AddWithValue("@DiscardOldest", t.DiscardOldest);
                    myCmd.Parameters.AddWithValue("@ServerId", s[0]);
                    myCmd.ExecuteNonQuery();
                }
                log.stopTimer(amountRecords, QueriesEnum.ADDTAG, TechnologiesEnum.RAWMYSQL, firsttime);
                mConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddReadings(int numberOfReadings, bool firstime)
        {
            log.startTimer();
            StringBuilder sCommand = new StringBuilder("INSERT INTO reading (ReadingId, Created, Quality, StringValue, TagId, IntegerValue, UnsignedIntegerValue, FloatValue) VALUES");
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                List<string> Rows = new List<string>();
                for (int i = 0; i < numberOfReadings; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}' ,'{2}','{3}','{4}','{5}','{6}','{7}')", MySqlHelper.EscapeString(Guid.NewGuid().ToString()), MySqlHelper.EscapeString(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")),
                        MySqlHelper.EscapeString("good"), MySqlHelper.EscapeString("stringvalue"), MySqlHelper.EscapeString("'2fcc3b10-3697-465d-9bcb-395752754415'"), MySqlHelper.EscapeString("123789"),
                        MySqlHelper.EscapeString("5233"), MySqlHelper.EscapeString("415.23")));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");
                mConnection.Open();
                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    myCmd.ExecuteNonQuery();
                }
                mConnection.Close();
            }
            log.stopTimer(numberOfReadings, QueriesEnum.ADDREADINGS, TechnologiesEnum.RAWMYSQL, firstime);
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
            log.stopTimer(number, QueriesEnum.REMOVEREADINGS, TechnologiesEnum.RAWMYSQL, FirstTime);
        }
    }
}
