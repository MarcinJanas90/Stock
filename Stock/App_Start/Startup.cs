using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Stock.Hubs;
using Stock.Services;

namespace Stock
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
                typeof(StockHub),
                () => new StockHub(new ConnectionServiceProvider(), new UserNotificationServiceProvider()));
            app.MapSignalR();
        }
    }
}