using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConnectionWithSSRS.Startup))]
namespace ConnectionWithSSRS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
