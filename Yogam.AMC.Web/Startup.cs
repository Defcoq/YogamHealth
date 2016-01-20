using Microsoft.Owin;
using Yogam.AMC.Web;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Yogam.AMC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
