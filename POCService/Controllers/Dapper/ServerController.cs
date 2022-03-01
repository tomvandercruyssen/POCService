//using POCService.DataContexts.MySQL;
using Dapper;
using MySqlConnector;
using POCService.Enums;
using POCService.Logging;
using SharedLib.Data;
using System;
using System.Collections.Generic;

namespace POCService.Controllers.Dapper
{

    public class ServerController
    {
        private readonly Logger log = new Logger();

        private readonly string conn = "server = localhost; port = 3306; user = root; password = Azerty123; database = poc";
        public List<string> GetServersIDs()
        {
            List<string> serverIDLijst = new List<string>();
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                mConnection.Open();
                IEnumerable<Server> serverLijst = mConnection.Query<Server>("Select ServerId from server");
                foreach (Server server in serverLijst)
                {
                    serverIDLijst.Add(server.ServerId);
                }
                mConnection.Close();
            }
            return serverIDLijst;
        }
        public void AddServer(bool FirstTime)
        {
            int amountRecords = 0;
            Server s = new Server();
            log.startTimer();
            Guid ServerId = Guid.NewGuid();
            int Enabled = Convert.ToInt32(s.Enabled);
            string Endpoint = s.Endpoint;
            int LifetimeCount = (int)s.LifetimeCount;
            uint MaxKeepAlive = s.MaxKeepAlive;
            uint MaxNotifications = s.MaxNotifications;
            string Name = s.Name;
            string Protocol = s.Protocol;
            uint PublishingInterval = s.PublishingInterval;
            bool ReconnectOnSubscriptionDelete = s.ReconnectOnSubscriptionDelete;
            uint SessionTimeOut = s.SessionTimeOut;
            string TimeZone = s.TimeZone;
            Guid ServerCredentialsId = Guid.Parse("06861fb8-48c8-470d-894e-c98c7e193cc4");
            using (MySqlConnection mConnection = new MySqlConnection(conn))
            {
                mConnection.Open();
                mConnection.Execute(@"INSERT INTO server (ServerId, Enabled, Endpoint, LifeTimeCount, MaxKeepAlive, MaxNotifications, Name, 
                            Protocol, PublishingInterval, ReconnectOnSubscriptionDelete, SessionTimeOut, TimeZone, ServerCredentialsId)
                            VALUES (@ServerId, @Enabled, @Endpoint, @LifetimeCount, @MaxKeepAlive, @MaxNotifications, @Name, @Protocol, @PublishingInterval, @ReconnectOnSubscriptionDelete,
                            @SessionTimeOut, @TimeZone, @ServerCredentialsId);", new {ServerId, Enabled, Endpoint, LifetimeCount, MaxKeepAlive, MaxNotifications, Name, Protocol, PublishingInterval, ReconnectOnSubscriptionDelete, SessionTimeOut,TimeZone, ServerCredentialsId });
                mConnection.Close();
            }
            log.stopTimer(amountRecords, QueriesEnum.ADDSERVER, TechnologiesEnum.DAPPER, FirstTime);
        }
    }
}