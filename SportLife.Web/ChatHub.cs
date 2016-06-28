using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using SportLife.Bll.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionMapping _connections =
            new ConnectionMapping();

        public void Send(string name, string message, int userId)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void GetAllUsers()
        {
            string name = Context.User.Identity.Name;
            var names = _connections.GetConnections(name);
            var others = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(names);

            Clients.Caller.showConnected(names);
        }

        public void Left()
        {

        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                _connections.Add(name, Context.ConnectionId);
                Clients.Others.joined(name);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string name = Context.ConnectionId;
            if (!string.IsNullOrEmpty(name))
            {
                var logoutMan = _connections.Remove(name);
                if (!string.IsNullOrEmpty(logoutMan))
                {
                    Clients.All.disconnect(logoutMan);
                }
            }

            return base.OnDisconnected();
        }

    }

    public class ConnectionMapping
    {
        private readonly Dictionary<string, string> _connections =
            new Dictionary<string, string>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.ContainsKey(key))
                {
                    _connections.Add(key, connectionId);
                }
                else
                {
                    _connections[key] = connectionId;
                }
            }
        }

        public IEnumerable<string> GetConnections(string key)
        {

            if (_connections.Any())
            {
                return _connections.Keys.Where(c => c != key).ToList();
            }

            return Enumerable.Empty<string>();
        }

        public string Remove(string value)
        {
            lock (_connections)
            {
                var key = _connections.FirstOrDefault(c => c.Value == value);

                try
                {
                    var ok = _connections.Remove(key.Key);

                    if (ok) return key.Key;

                }
                catch (Exception e)
                {

                }

                return string.Empty;
            }
        }
    }


}