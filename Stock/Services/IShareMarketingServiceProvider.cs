using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stock.Services
{
    public interface IShareMarketingServiceProvider
    {
        Task<Share> GetLatestShareInformationByCompanyCode(string companyCode);

        Task<BoughtShareViewModel> GetCurrentShareInformation(string companyCode);

        Task<SoldShareViewModel> GetCurrentSoldShareInformation(string accountName, string companyCode);

        Task BuyShare(string accountName, string shareCompanyCode, int numberOfShares);

        Task<HttpStatusCodeResult> SellShare(string accountName, string shareCompanyCode, int numberOfShares);
    }
}
