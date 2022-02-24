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
using MySqlConnector;
using System.Text;
using System.Data;

namespace POCService.Controllers.RawMySQL
{

    public class ServerController
    {
        Logger log = new Logger();

        private string conn = "server = localhost; port = 3306; user = root; password = Azerty123; database = poc";

        public void addServer(bool FirstTime)
        {
            int amountRecords = 0;
            var s = new Server();
            log.startTimer();
            string FKOFF = "SET FOREIGN_KEY_CHECKS = 0";
            string cmdStr = @"INSERT INTO server (ServerId, Enabled, Endpoint, LifeTimeCount, MaxKeepAlive, MaxNotifications, Name, 
                            Protocol, PublishingInterval, ReconnectOnSubscriptionDelete, SessionTimeOut, TimeZone, ServerCredentialsId)
                            VALUES (@ServerId, @Enabled, @Endpoint, @LifetimeCount, @MaxKeepAlive, @MaxNotifications, @Name, @Protocol, @PublishingInterval, @ReconnectOnSubscriptionDelete,
                            @SessionTimeOut, @TimeZone, @Credentials);";
            Console.WriteLine(cmdStr);
            Console.WriteLine(s.LifetimeCount);
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
                    myCmd.Parameters.AddWithValue("@ServerId", s.ServerId);
                    myCmd.Parameters.AddWithValue("@Enabled", (int) Convert.ToInt32(s.Enabled));
                    myCmd.Parameters.AddWithValue("@Endpoint", s.Endpoint);
                    myCmd.Parameters.AddWithValue("@LifetimeCount", (int) s.LifetimeCount);
                    myCmd.Parameters.AddWithValue("@MaxKeepAlive", s.MaxKeepAlive);
                    myCmd.Parameters.AddWithValue("@MaxNotifications", s.MaxNotifications);
                    myCmd.Parameters.AddWithValue("@Name", s.Name);
                    myCmd.Parameters.AddWithValue("@Protocol", s.Protocol);
                    myCmd.Parameters.AddWithValue("@PublishingInterval", s.PublishingInterval);
                    myCmd.Parameters.AddWithValue("@ReconnectOnSubscriptionDelete", s.ReconnectOnSubscriptionDelete);
                    myCmd.Parameters.AddWithValue("@SessionTimeOut", s.SessionTimeOut);
                    myCmd.Parameters.AddWithValue("@TimeZone", s.TimeZone);
                    myCmd.Parameters.AddWithValue("@Credentials", s.Credentials.ServerCredentialsId);
                    myCmd.ExecuteNonQuery();
                }
            }
            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.MySQL, FirstTime);
        }
    }
}