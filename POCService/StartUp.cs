using Microsoft.EntityFrameworkCore;
using POCService.Controllers;
using SharedLib.Data;
using SharedLib.DTO;
using SharedLib.DTO.Requests;
using System;
using System.Linq;

namespace POCService
{
    public class StartUp
    {

        ServerController sc = new ServerController();
        TagController tc = new TagController();
        public StartUp()
        {
            addServer();
            var context = new EdgeDataContext();
            var servers = context.Server.Include(s => s.Credentials).Include(s => s.Tags).Select(s => new ServerDTO(s)).ToList();
            addTag(servers[0].ServerId);
            addTag(servers[0].ServerId);
            var watch = System.Diagnostics.Stopwatch.StartNew();

            string test = servers[0].TagIds[0];
            for (int i = 0; i <1000; i++)
            {
                addReadings(i, test);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
        private void addServer()
        {
            var req = new CreateServerResponse();
            ServerDTO selectedServer = new ServerDTO();
            selectedServer.Name = "server12";
            selectedServer.Endpoint = "endpoint1";
            selectedServer.Protocol = "protocol1";
            selectedServer.TimeZone = "timezone1";
            selectedServer.PublishingInterval = 1000;
            selectedServer.MaxNotifications = 1000;
            selectedServer.Enabled = true;
            selectedServer.SessionTimeOut = 1000;
            selectedServer.MaxKeepAlive = 1000;
            selectedServer.LifetimeCount = 1000;
            selectedServer.ReconnectOnSubscriptionDelete = true;
            req.Server = selectedServer;
            sc.UpdateServer(req);
        }

        //private void addTag(Guid id)
        //{
        //    var tag = new TagDTO()
        //    {
        //        Name = "tag1",
        //        NodeId = "ns=2;s=len009",
        //        Type = "uint",
        //        BufferHours = (uint)1,
        //        SamplingInterval = (uint)1000,
        //        QueueSize = (uint)1000,
        //        DiscardOldest = true
        //    };
        //    tc.UpdateTag(id, tag);
        //}

        private void addTag(string id)
        {
            var tag = new TagDTO()
            {
                Name = "tag1",
                NodeId = "ns=2;s=len009",
                Type = "uint",
                BufferHours = (uint)1,
                SamplingInterval = (uint)1000,
                QueueSize = (uint)1000,
                DiscardOldest = true
            };
            tc.UpdateTag(id, tag);
        }

        private void addReadings(int i, string test)
        {
            Reading reading = new Reading();
            reading.Created = DateTime.UtcNow;
            reading.Quality = "quality";
            reading.StringValue = "stringvalue";
            reading.IntegerValue = i;
            reading.UnsignedIntegerValue = 6874833;
            reading.FloatValue = 633.5423;

            tc.addReading(test, reading);
        }
    }
}


