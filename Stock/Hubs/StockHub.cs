using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Stock.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Stock.Models;

namespace Stock.Hubs
{
    [HubName("StockHub")]
    public class StockHub : Hub
    {
        private IConnectionServiceProvider _connectionServiceProvider;
        private IUserNotificationServiceProvider _userNotificationServiceProvider;

        public StockHub(IConnectionServiceProvider connectionServiceProvider, IUserNotificationServiceProvider userNotificationServiceProvider)
        {
            _connectionServiceProvider = connectionServiceProvider;
            _userNotificationServiceProvider = userNotificationServiceProvider;
        }
        
        public void EstablishConnection()
        {
            Clients.Caller.EstablishConnection();
        }

        public async Task RenderStockPrices()
        {
            await _userNotificationServiceProvider.RenderStockPrices(Context.ConnectionId);
        }

        public async Task RenderWallet()
        {
            await _userNotificationServiceProvider.RenderWallet(Context.ConnectionId);
        }

        public async Task BuyShares(string companyName,int numberOfShates)
        {

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