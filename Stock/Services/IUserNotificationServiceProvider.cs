using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Services
{
    public interface IUserNotificationServiceProvider
    {
        Task RenderStockPrices(string connectionId);
        Task RenderWallet(string connectionId);
        Task ShowChart(string connectionId);
        Task UpdateChart();
        Task UpdateStockPrices();
        Task UpdateWalletValues();
        Task RenderConnectionProblem();
        Task RenderConnectionOK();
        Task RenderConnectionStatus(string connectionId);
    }
}
