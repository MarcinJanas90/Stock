using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Models;
using System.Threading.Tasks;

namespace Stock.Services
{
    public interface IShareMarketingServiceProvider
    {
        Task<BoughtShareViewModel> GetCurrentShareInformation(string companyCode);

        Task BuyShare(string accountName, string shareCompanyCode, int numberOfShares);       
    }
}
