using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stock.Services;
using Stock.Models;
using System.Net;

namespace Stock.Controllers
{
    public class StockController : Controller
    {
        private IShareMarketingServiceProvider _shareMarketServiceProvider;

        public StockController(IShareMarketingServiceProvider shareMarketServiceProvider)
        {
            _shareMarketServiceProvider = shareMarketServiceProvider;
        }

        // GET: Stock
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stock/BuyShares/companyCode
        public async Task<ActionResult> BuyShares(string companyCode)
        {
            BoughtShareViewModel _boughtShareViewModel = await _shareMarketServiceProvider.GetCurrentShareInformation(companyCode);

            if (_boughtShareViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(_boughtShareViewModel);
        }

        // POST: Stock/BuyShares/
        [HttpPost]
        public async Task<ActionResult> BuyShares(BoughtShareViewModel boughShareViewModel)
        {
            await _shareMarketServiceProvider.BuyShare(User.Identity.Name,boughShareViewModel.CompanyCode,boughShareViewModel.SharesBoughtAmount);
            return RedirectToAction("Index", "Stock");
        }
    }
}