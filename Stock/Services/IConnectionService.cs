using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Services
{
    public interface IConnectionService
    {
        Task ConnectClient(string accountName, string connectionId);
        Task DisconnectClient(string accountName, string connectionId);
    }
}
