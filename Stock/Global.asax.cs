﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Stock.Services;
using Newtonsoft.Json.Linq;
using Stock.App_Start;

namespace Stock
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer _timer;
        private ShareValueService _ShareValuesServiceProvider;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _ShareValuesServiceProvider = new ShareValueService(new UserNotificationService());

            ApplicationGlobals.IsRemoteServerAvalaible = true;

            _ShareValuesServiceProvider.InitializeShareValues();
            _timer = new Timer(10000);
            _timer.Elapsed += ShareValueService.GetActualShareValues;
            _timer.Start();
        }
    }
}
