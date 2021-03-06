﻿using System;
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
        private IShareMarketingService _shareMarketServiceProvider;

        public StockController(IShareMarketingService shareMarketServiceProvider)
        {
            _shareMarketServiceProvider = shareMarketServiceProvider;
        }

        // GET: Stock
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Stock/BuyShares/companyCode
        [Authorize]
        public async Task<ActionResult> BuyShares(string companyCode)
        {
            BoughtShareViewModel _boughtShareViewModel = await _shareMarketServiceProvider.GetCurrentBoughtShareInformation(companyCode);

            if (_boughtShareViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(_boughtShareViewModel);
        }

        // POST: Stock/BuyShares/
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> BuyShares(BoughtShareViewModel boughShareViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _shareMarketServiceProvider.BuyShare(User.Identity.Name, boughShareViewModel.CompanyCode, boughShareViewModel.SharesBoughtAmount);
                
                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }

                return View(boughShareViewModel);
            }

            return View(boughShareViewModel);
        }

        // GET: Stock/SellShares/companyCode
        [Authorize]
        public async Task<ActionResult> SellShares(string companyCode)
        {
            SoldShareViewModel _soldShareViewModel = await _shareMarketServiceProvider.GetCurrentSoldShareInformation(User.Identity.Name, companyCode);

            if (_soldShareViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(_soldShareViewModel);
        }

        // POST: Stock/SellShares
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> SellShares(SoldShareViewModel soldShareViewModel)
        {
            if (ModelState.IsValid)
            {
                var Status = await _shareMarketServiceProvider.SellShare(User.Identity.Name, soldShareViewModel.CompanyCode, soldShareViewModel.NumberOfSoldShares);

                if (Status)
                {
                    return RedirectToAction("Index", "Stock");
                }

                return View(soldShareViewModel);
            }

            return View(soldShareViewModel);
        }

    }
}