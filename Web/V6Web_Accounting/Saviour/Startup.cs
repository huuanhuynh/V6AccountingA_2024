using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Saviour.Startup))]
namespace Saviour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
