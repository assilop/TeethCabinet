using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeethCabinet.Startup))]
namespace TeethCabinet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
