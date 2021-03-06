﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Stock.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Stock.Models;
using Stock.App_Start;

namespace Stock.Hubs
{
    [HubName("StockHub")]
    public class StockHub : Hub
    {
        private IConnectionService _connectionServiceProvider;
        private IUserNotificationService _userNotificationServiceProvider;

        public StockHub(IConnectionService connectionServiceProvider, IUserNotificationService userNotificationServiceProvider)
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

        public async Task ShowChart()
        {
            await _userNotificationServiceProvider.ShowChart(Context.ConnectionId);
        }

        public override async Task OnConnected()
        {
            await _connectionServiceProvider.ConnectClient(Context.User.Identity.Name, Context.ConnectionId);
            await _userNotificationServiceProvider.RenderConnectionStatus(Context.ConnectionId);
            await base.OnConnected();
        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            await _connectionServiceProvider.DisconnectClient(Context.User.Identity.Name, Context.ConnectionId);
            await base.OnDisconnected(stopCalled);
        }
    }
}