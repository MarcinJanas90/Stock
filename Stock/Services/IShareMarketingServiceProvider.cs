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
    public interface IShareMarketingServiceProvider
    {
        Task<Share> GetLatestShareInformationByCompanyCode(string companyCode);

        Task<BoughtShareViewModel> GetCurrentShareInformation(string companyCode);

        Task<SoldShareViewModel> GetCurrentSoldShareInformation(string accountName, string companyCode);

        Task<HttpStatusCode> BuyShare(string accountName, string shareCompanyCode, int numberOfShares);

        Task<HttpStatusCode> SellShare(string accountName, string shareCompanyCode, int numberOfShares);
    }
}
