﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Stock.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.Data.Entity;
using Stock.App_Start;

namespace Stock.Services
{
    public class ShareValueServiceProvider
    {
        private static ApplicationDbContext _applicationDbContext;
        private static IUserNotificationServiceProvider _userNotificationServiceProvider;

        public ShareValueServiceProvider(IUserNotificationServiceProvider userInterfaceServiceProvider)
        {
            _applicationDbContext = new ApplicationDbContext();
            _userNotificationServiceProvider = userInterfaceServiceProvider;
        }

        public static async void GetActualShareValues(object source, ElapsedEventArgs e)
        {
            string uri = "http://webtask.future-processing.com:8068/stocks";
            var Request = (HttpWebRequest)WebRequest.Create(uri);
            Request.Accept = "application/json";
            Request.Method = "GET";

            HttpWebResponse Response;
            try
            {
                Response = (HttpWebResponse)await Request.GetResponseAsync();
            }
            catch
            {
                Response = null;
            }

            if (Response != null && Response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (ApplicationGlobals.IsRemoteServerAvalaible != true)
                {
                    ApplicationGlobals.IsRemoteServerAvalaible = true;
                    await _userNotificationServiceProvider.EnableButtons();
                }

                string ResponseJson = await new StreamReader(Response.GetResponseStream(), true).ReadToEndAsync();

                JObject _JObject = JObject.Parse(ResponseJson);
                DateTime PublicationDate = (DateTime)_JObject["publicationDate"];

                // Check if there are newer sahers values

                Share LatestShare = await _applicationDbContext.Shares.OrderByDescending(x => x.PublicationDate).FirstOrDefaultAsync();

                if (LatestShare == null || DateTime.Compare(PublicationDate, LatestShare.PublicationDate) > 0)
                {
                    JArray Items = (JArray)_JObject["items"];


                    foreach (var item in Items)
                    {
                        Share _share = new Share();
                        _share.CompanyName = (string)item["name"];
                        _share.CompanyCode = (string)item["code"];
                        _share.UnitNumber = (int)item["unit"];
                        _share.UnitNumber = (int)item["unit"];
                        _share.UnitPrice = (double)item["price"];
                        _share.PublicationDate = PublicationDate;

                        _applicationDbContext.Shares.Add(_share);
                    }
                    await _applicationDbContext.SaveChangesAsync();
                    await _userNotificationServiceProvider.UpdateStockPrices();
                    await _userNotificationServiceProvider.UpdateWalletValues();
                    await _userNotificationServiceProvider.UpdateChart();
                }
            }
            else
            {
                if (ApplicationGlobals.IsRemoteServerAvalaible != false)
                {
                    ApplicationGlobals.IsRemoteServerAvalaible = false;
                    await _userNotificationServiceProvider.DisableButtons();
                }
            }
        }
    }
}