using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(V6Web_Mod_Controls.Startup))]
namespace V6Web_Mod_Controls
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
