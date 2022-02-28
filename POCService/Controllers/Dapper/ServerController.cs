//using POCService.DataContexts.MySQL;
using MySqlConnector;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace POCService.Controllers.Dapper
{

    public class ServerController
    {
        private readonly Logger log = new Logger();

        private readonly string conn = "server = localhost; port = 3306; user = root; password = Azerty123; database = poc";
        public List<string> GetServers()
        {
            List<string> servers = new List<string>();
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                mConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand("Select * from server", mConnection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        servers.Add(reader.GetString("ServerId"));
                    }
                }
                mConnection.Close();
            }
            return servers;
        }
        public void AddServer(bool FirstTime)
        {
            int amountRecords = 0;
            Server s = new Server();
            log.startTimer();
            string FKOFF = "SET FOREIGN_KEY_CHECKS = 0";
            string cmdStr = @"INSERT INTO server (ServerId, Enabled, Endpoint, LifeTimeCount, MaxKeepAlive, MaxNotifications, Name, 
                            Protocol, PublishingInterval, ReconnectOnSubscriptionDelete, SessionTimeOut, TimeZone, ServerCredentialsId)
                            VALUES (@ServerId, @Enabled, @Endpoint, @LifetimeCount, @MaxKeepAlive, @MaxNotifications, @Name, @Protocol, @PublishingInterval, @ReconnectOnSubscriptionDelete,
                            @SessionTimeOut, @TimeZone, @Credentials);";
            Console.WriteLine(cmdStr);
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                mConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(FKOFF, mConnection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (MySqlCommand myCmd = new MySqlCommand(cmdStr, mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    myCmd.Parameters.AddWithValue("@ServerId", Guid.NewGuid());
                    myCmd.Parameters.AddWithValue("@Enabled", Convert.ToInt32(s.Enabled));
                    myCmd.Parameters.AddWithValue("@Endpoint", s.Endpoint);
                    myCmd.Parameters.AddWithValue("@LifetimeCount", (int)s.LifetimeCount);
                    myCmd.Parameters.AddWithValue("@MaxKeepAlive", s.MaxKeepAlive);
                    myCmd.Parameters.AddWithValue("@MaxNotifications", s.MaxNotifications);
                    myCmd.Parameters.AddWithValue("@Name", s.Name);
                    myCmd.Parameters.AddWithValue("@Protocol", s.Protocol);
                    myCmd.Parameters.AddWithValue("@PublishingInterval", s.PublishingInterval);
                    myCmd.Parameters.AddWithValue("@ReconnectOnSubscriptionDelete", s.ReconnectOnSubscriptionDelete);
                    myCmd.Parameters.AddWithValue("@SessionTimeOut", s.SessionTimeOut);
                    myCmd.Parameters.AddWithValue("@TimeZone", s.TimeZone);
                    myCmd.Parameters.AddWithValue("@Credentials", Guid.NewGuid());
                    myCmd.ExecuteNonQuery();
                }
                mConnection.Close();
            }
            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.RAWMYSQL, FirstTime);
        }
    }
}