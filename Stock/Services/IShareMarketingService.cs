using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;

namespace Stock.Services
{
    public interface IShareMarketingService
    {
        Task<Share> GetLatestShareInformationByCompanyCode(string companyCode);

        Task<BoughtShareViewModel> GetCurrentBoughtShareInformation(string companyCode);

        Task<SoldShareViewModel> GetCurrentSoldShareInformation(string accountName, string companyCode);

        Task<bool> BuyShare(string accountName, string shareCompanyCode, int numberOfShares);

        Task<bool> SellShare(string accountName, string shareCompanyCode, int numberOfShares);
    }
}
