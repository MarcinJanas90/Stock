using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Stock.Models;
using System.Data.Entity;
using Microsoft.AspNet.SignalR;
using Stock.Hubs;

namespace Stock.Services
{
    public class ConnectionServiceProvider : IConnectionServiceProvider
    {
        private ApplicationDbContext _applicationDbContext;
        private IHubContext _StockHubContext;

        public ConnectionServiceProvider()
        {
            _applicationDbContext = new ApplicationDbContext();
            _StockHubContext = GlobalHost.ConnectionManager.GetHubContext<StockHub>();
        }

        public async Task ConnectClient(string accountName, string connectionId)
        {
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account != null && _account.IsAuthenticated)
            {
                Connection _connection = await _applicationDbContext.Connecions.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

                if (_connection == null)
                {
                    _connection = new Connection();
                    _connection.ConnectionId = connectionId;
                    _connection.CorrespondingAccountName = accountName;
                    _account.AccountConnections.Add(_connection);
                    _applicationDbContext.Connecions.Add(_connection);
                    await _applicationDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DisconnectClient(string accountName, string connectionId)
        {
            Connection _connection = await _applicationDbContext.Connecions.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

            if (_connection != null)
            {
                _applicationDbContext.Connecions.Remove(_connection);
            }

            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x => x.AccountName == accountName);

            if (_account != null)
            {
                _account.AccountConnections.Remove(_connection);
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}