using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bitsoccer.net.Startup))]
namespace bitsoccer.net
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
