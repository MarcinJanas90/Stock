using Microsoft.Owin;
using Owin;


namespace Stock
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}