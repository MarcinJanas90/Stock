using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Stock.Models;
using Stock.Hubs;
using System.Data.Entity;

namespace Stock.Services
{
    public class UserNotificationServiceProvider : IUserNotificationServiceProvider
    {
        private IHubContext _StockHubContext;
        private ApplicationDbContext _applicationDbContext;

        public UserNotificationServiceProvider()
        {
            _StockHubContext = GlobalHost.ConnectionManager.GetHubContext<StockHub>();
            _applicationDbContext = new ApplicationDbContext();
        }

        public async Task RenderStockPrices(string connectionId)
        {
            DateTime MaxDateTime = await _applicationDbContext.Shares.Select(x => x.PublicationDate).MaxAsync();
            List<Share> Tmp = await _applicationDbContext.Shares.Where(x => x.PublicationDate == MaxDateTime).ToListAsync();
            List<Share> LatestShares = await _applicationDbContext.Shares.OrderByDescending(x => x.PublicationDate).Take(100).ToListAsync();
            LatestShares = LatestShares.Where(x => x.PublicationDate == LatestShares.Max(y => y.PublicationDate)).ToList();

            List<string> CompanyCodes = LatestShares.Select(x => x.CompanyCode).ToList();
            List<double> ShareValues = LatestShares.Select(x => x.UnitPrice).ToList();

            _StockHubContext.Clients.Client(connectionId).RenderStockPrices(CompanyCodes, ShareValues);
        }

        public async Task RenderWallet(string connectionId)
        {
            Connection _connection = await _applicationDbContext.Connecions.FirstOrDefaultAsync(x => x.ConnectionId == connectionId);
            Account _account = await _applicationDbContext.Accounts.FirstOrDefaultAsync(x=>x.AccountName ==_connection.CorrespondingAccountName);
            List<Share> LatestShares = await _applicationDbContext.Shares.Where(x => x.PublicationDate == _applicationDbContext.Shares.Max(y => y.PublicationDate)).ToListAsync(); ;

            if (_account != null)
            {
                List<string> CompanyCodes = new List<string>();
                List<double> UnitPrices = new List<double>();
                List<int> Amounts = new List<int>();
                List<double> Values = new List<double>();

                foreach (Share share in LatestShares)
                {
                    if (_account.AccountOwnedShares.Select(x => x.CompanyCode).ToList().Contains(share.CompanyCode))
                    {
                        CompanyCodes.Add(share.CompanyCode);
                        UnitPrices.Add(share.UnitPrice);
                        Amounts.Add(_account.AccountOwnedShares.FirstOrDefault(x => x.CompanyCode == share.CompanyCode).NumberOfOwnedShares);
                        Values.Add(share.UnitPrice* _account.AccountOwnedShares.FirstOrDefault(x => x.CompanyCode == share.CompanyCode).NumberOfOwnedShares);
                    }
                }

                _StockHubContext.Clients.Client(connectionId).renderWallet(_account.AccountWallet, CompanyCodes, UnitPrices, Amounts, Values);
            }
        }

        public async Task UpdateStockPrices()
        {
            List<Connection> Connections = await _applicationDbContext.Connecions.ToListAsync();

            List<Share> LatestShares = await _applicationDbContext.Shares.OrderByDescending(x => x.PublicationDate).Take(100).ToListAsync();
            LatestShares = LatestShares.Where(x => x.PublicationDate == LatestShares.Max(y => y.PublicationDate)).ToList();

            List<string> CompanyCodes = LatestShares.Select(x => x.CompanyCode).ToList();
            List<double> ShareValues = LatestShares.Select(x => x.UnitPrice).ToList();

            foreach (Connection connection in Connections)
            {
                _StockHubContext.Clients.Client(connection.ConnectionId).UpdateStockPrices(CompanyCodes, ShareValues);
            }
        }

        public async Task UpdateWalletValues()
        {
            await _StockHubContext.Clients.All.updateWalletValues();
        }
    }
}