using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Stock.Services;
using Microsoft.AspNet.SignalR.Hubs;

namespace Stock.Hubs
{
    [HubName("StockHub")]
    public class StockHub : Hub
    {
        private IConnectionServiceProvider _connectionServiceProvider;

        public void EstablishConnection()
        {
            Clients.Caller.EstablishConnection();
        }
        public StockHub(IConnectionServiceProvider connectionServiceProvider)
        {
            _connectionServiceProvider = connectionServiceProvider;
        }
        
        public override async Task OnConnected()
        {
            await _connectionServiceProvider.ConnectClient(Context.User.Identity.Name, Context.ConnectionId);
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await _connectionServiceProvider.DisconnectClient(Context.User.Identity.Name, Context.ConnectionId);
            await base.OnDisconnected(stopCalled);
        }
    }
}