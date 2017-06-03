using System.Security.Claims;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Microsoft.Owin.Security.OAuth;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.Models.Accounting.DTO;

namespace V6Soft.WebApi.Accounting.Providers
{
    public class V6AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //To handle custom parameters.
            var data = await context.Request.ReadFormAsync();
            var loginServices = AppContext.DependencyInjector.Resolve<ILoginServices>();
            var authenticatedUser = loginServices.CheckLogin(
                context.UserName,
                context.Password,
                data["dvcs"]);

            if (!authenticatedUser)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}