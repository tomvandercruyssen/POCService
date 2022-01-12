using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SharedLib.Data;
using Microsoft.EntityFrameworkCore;
using SharedLib.Enums;

namespace POCService
{
    //public class Connection
    //{

    //    public IUserIdentity UserIdentity { get; set; } = null;
    //    public string[] PreferredLocales { get; set; } = null;
    //    private ApplicationConfiguration configuration;

    //    // default values
    //    public static readonly int DefaultDiscoverTimeout = 15000;
    //    public static readonly uint DefaultSessionTimeout = 10000;
    //    public int DiscoverTimeout { get; set; } = DefaultDiscoverTimeout;

    //    public Session Session { get; set; }
    //    public Server Server { get; set; }
    //    public Subscription Subscription { get; set; }
    //    public List<MonitoredItem> MonitoredItems { get; set; }

    //    internal uint Write(string nodeid, uint value)
    //    {
    //        try
    //        {
    //            var node = new NodeId(nodeid);
    //            WriteValueCollection nodesToWrite = new WriteValueCollection
    //            {
    //                new WriteValue()
    //                {
    //                    AttributeId = Attributes.Value,
    //                    Value = new DataValue(new Variant(value)),
    //                    NodeId = node
    //                }
    //            };
    //            if (!IsConnected())
    //            {
    //                throw new Exception("Service was connected but server is down, try again in a minute.");
    //            }
    //            Session.Write(null, nodesToWrite, out _, out _);
    //            var readvalue = ReadValueFromNode(new ExpandedNodeId(node));
    //            if (readvalue is null)
    //            {
    //                throw new Exception("Node not found");
    //            }
    //            return (uint)readvalue.Value;
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //    }

    //    public Connection(ApplicationConfiguration configuration, Server server)
    //    {
    //        this.configuration = configuration;
    //        this.Server = server;
    //    }

    //    internal bool IsConnected()
    //    {
    //        return Session != null && Session.Connected && !Session.KeepAliveStopped;
    //    }

    //    public ReferenceDescriptionCollection Browse(NodeId rootNodeId)
    //    {
    //        BrowseDescription nodeToBrowse = new BrowseDescription
    //        {
    //            NodeId = rootNodeId,
    //            ReferenceTypeId = Opc.Ua.ReferenceTypeIds.HierarchicalReferences,
    //            IncludeSubtypes = true,
    //            ResultMask = (uint)(BrowseResultMask.All)
    //        };
    //        return ClientUtils.Browse(Session, nodeToBrowse, false);
    //    }

    //    public Task<Session> Connect()
    //    {
    //        if (String.IsNullOrEmpty(Server.Credentials.Password) || String.IsNullOrEmpty(Server.Credentials.Username))
    //        {
    //            return Connect(Server.Endpoint, false);
    //        }
    //        else
    //        {
    //            return Connect(Server.Endpoint, true);
    //        }
    //    }

    //    private async Task<Session> Connect
    //        (
    //            string serverUrl,
    //            bool useSecurity,
    //            uint sessionTimeout = 0
    //        )
    //    {
    //        // disconnect from existing session.
    //        Disconnect();
    //        if (!Server.Enabled)
    //        {
    //            Session = null;
    //            return null;
    //        }
    //        // select the best endpoint.
    //        EndpointDescription endpointDescription = default;
    //        EndpointConfiguration endpointConfiguration = default;
    //        try
    //        {
    //            endpointDescription = CoreClientUtils.SelectEndpoint(serverUrl, useSecurity, DiscoverTimeout);
    //            endpointConfiguration = EndpointConfiguration.Create(configuration);
    //        }
    //        catch (ServiceResultException e)
    //        {
    //            Debug.WriteLine(String.Format("{0}: {1}", e.StatusCode, e.Message));
    //        }
    //        if (endpointDescription == default || endpointConfiguration == default)
    //        {
    //            Session = null;
    //            return null;
    //        }
    //        var endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);

    //        if (useSecurity)
    //        {
    //            UserIdentity = new UserIdentity(Server.Credentials.Username, Server.Credentials.Password);
    //        }

    //        Session = await Session.Create(
    //            configuration,
    //            endpoint,
    //            false,
    //            true,
    //            configuration.ApplicationName,
    //            sessionTimeout == 0 ? DefaultSessionTimeout : Server.SessionTimeOut,
    //            UserIdentity,
    //            PreferredLocales);
    //        if (!IsConnected())
    //        {
    //            new LoggingService(null).Log(SharedLib.Enums.ServiceProcesses.DATAGATHERING, SharedLib.Enums.LogCategories.OPCCONNECTION, String.Format("Failed connecting {0}", Server.Name));
    //        }
    //        try
    //        {
    //            Debug.WriteLine(DateTime.Now, "Connected, loading complex type system.");
    //            var typeSystemLoader = new ComplexTypeSystem(Session);
    //            await typeSystemLoader.Load();
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.WriteLine(DateTime.Now, "Connected, failed to load complex type system.");
    //            Opc.Ua.Utils.Trace(e, "Failed to load complex type system.");
    //        }

    //        // return the new session.
    //        return Session;
    //    }

    //    public void UpdateSubscriptions(MonitoredItemNotificationEventHandler mine)
    //    {
    //        var server = new EdgeDataContext().Server.Include(s => s.Tags).FirstOrDefault(s => s.ServerId.Equals(Server.ServerId));
    //        MonitoredItems = new List<MonitoredItem>();
    //        if (Subscription != null)
    //        {
    //            Subscription.RemoveItems(Subscription.MonitoredItems);
    //        }
    //        foreach (var item in server.Tags)
    //        {
    //            try
    //            {
    //                SubscribeToSingleNode(item, mine);
    //            }
    //            catch (Exception e)
    //            {
    //                new LoggingService(null).Log(ServiceProcesses.DATAGATHERING, LogCategories.OPCCONNECTION, String.Format("Failed to update subscription for tag {0} with id {1} and nodeid {2} - {3}", item.Name, item.TagId, item.NodeId, e.Message));
    //            }
    //        }
    //    }

    //    public void Disconnect()
    //    {
    //        if (Session != null)
    //        {
    //            ClearSubscription();
    //            Session.Close();
    //            Session = null;
    //        }
    //    }

    //    public Node FindNode(NodeId nodeId)
    //    {
    //        if (Session != null)
    //        {
    //            return Session.NodeCache.Find(nodeId) as Node;
    //        }
    //        return null;
    //    }

    //    public Variant ReadOnlyValueFromNode(ExpandedNodeId nodeId)
    //    {
    //        if (!(Session.NodeCache.Find(nodeId) is ILocalNode node))
    //        {
    //            return new Variant();
    //        }
    //        if (node.NodeClass == NodeClass.Variable)
    //        {
    //            var result = ReadValueFromNode(nodeId);
    //            if (result is null)
    //            {
    //                return default;
    //            }
    //            return result.WrappedValue;
    //        }
    //        return default;
    //    }

    //    public void RemoveMonitoredItem(String nodeId)
    //    {
    //        var mi = MonitoredItems.Find(i => i.StartNodeId.Equals(new NodeId(nodeId)));
    //        Subscription.RemoveItem(mi);
    //        MonitoredItems.Remove(mi);
    //        Subscription.ApplyChanges();
    //    }

    //    public DataValue ReadValueFromNode(ExpandedNodeId nodeId)
    //    {
    //        try
    //        {
    //            return Session.ReadValue((NodeId)nodeId);
    //        }
    //        catch (ServiceResultException e)
    //        {
    //            Debug.WriteLine("{0}: StatusCode {1}",
    //            e.GetType().FullName, StatusCode.LookupSymbolicId(e.StatusCode));
    //        }
    //        return null;
    //    }

    //    public void SubscribeToSingleNode(Tag tag, MonitoredItemNotificationEventHandler monitoredItemNotificationEventHandler)
    //    {
    //        if (!IsConnected())
    //        {
    //            return;
    //        }
    //        var node = FindNode(tag.NodeId);
    //        if (node is null)
    //        {
    //            throw new Exception("Node with given nodeid does not exist on this server");
    //        }
    //        if (Subscription == null || !Subscription.Created)
    //        {
    //            Subscription = new Subscription(Session.DefaultSubscription)
    //            {
    //                PublishingEnabled = true,
    //                PublishingInterval = (int)Server.PublishingInterval,
    //                MaxNotificationsPerPublish = Server.MaxNotifications,
    //                KeepAliveCount = Server.MaxKeepAlive,
    //                LifetimeCount = Server.LifetimeCount
    //            };
    //            Session.AddSubscription(Subscription);
    //            Subscription.Create();
    //        }
    //        if (!Subscription.Created)
    //        {
    //            Subscription.Create();
    //        }
    //        var mi = new MonitoredItem(Subscription.DefaultItem);

    //        if (MonitoredItems == null || !Subscription.MonitoredItems.Any())
    //        {
    //            MonitoredItems = new List<MonitoredItem>();
    //        }

    //        mi.StartNodeId = (NodeId)node.NodeId;
    //        mi.AttributeId = Attributes.Value;
    //        mi.MonitoringMode = MonitoringMode.Reporting;
    //        mi.SamplingInterval = int.Parse(tag.SamplingInterval.ToString());
    //        mi.QueueSize = tag.QueueSize;
    //        mi.DiscardOldest = tag.DiscardOldest;
    //        mi.Notification += monitoredItemNotificationEventHandler;

    //        if (!MonitoredItems.Any(m => m.StartNodeId == mi.StartNodeId && m.SamplingInterval == mi.SamplingInterval))
    //        {
    //            MonitoredItems.Add(mi);
    //            Subscription.AddItem(mi);
    //        }

    //        /*
    //        MonitoredItems.Add(mi);
    //        Subscription.AddItem(mi);
    //        */

    //        Subscription.ApplyChanges();
    //        if (!Subscription.MonitoredItems.Any())
    //        {
    //            Subscription.Delete(false);
    //        }
    //        Subscription.StateChanged += Subscription_StateChanged;
    //    }

    //    private void Subscription_StateChanged(Subscription subscription, SubscriptionStateChangedEventArgs e)
    //    {
    //        if (e.Status == SubscriptionChangeMask.Deleted)
    //        {
    //            if (Server.ReconnectOnSubscriptionDelete)
    //            {
    //                subscription.Create();
    //            }
    //        }
    //    }

    //    public void ClearSubscription()
    //    {
    //        try
    //        {
    //            if (Subscription != null)
    //            {
    //                if (Subscription.Created)
    //                {
    //                    Subscription.Delete(false);
    //                }
    //                Subscription = null;
    //                MonitoredItems = null;
    //            }
    //        }
    //        catch (ServiceResultException e)
    //        {

    //            Debug.WriteLine("ServiceResultException: {0}", StatusCode.LookupSymbolicId(e.StatusCode));
    //        }
    //    }
    //}
}
