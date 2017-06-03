using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using V6Soft.WebApi.Accounting.Configs;
using V6Soft.WebApi.Accounting.Providers;

[assembly: OwinStartup(typeof(V6Soft.WebApi.Accounting.Startup))]
namespace V6Soft.WebApi.Accounting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            //Rest of code is here;
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new V6AuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}