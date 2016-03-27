using System;
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

namespace Stock
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer _timer;
        private ShareValueServiceProvider _ShareValuesServiceProvider;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            _ShareValuesServiceProvider = new ShareValueServiceProvider(new UserNotificationServiceProvider());

            _timer = new Timer(10000);
            _timer.Elapsed += ShareValueServiceProvider.GetActualShareValues;
            _timer.Start();
        }
    }
}
