using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlightRouteWebsite.Startup))]
namespace FlightRouteWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
