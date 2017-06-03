using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using V6Soft.Services.Common.Infrastructure;
using V6Soft.Web.Accounting.Constants;
using V6Soft.Web.Common.Dependencies;

using ControllerNames = V6Soft.Web.Accounting.Constants.Names.Controllers;
using RouteNames = V6Soft.Web.Accounting.Constants.Names.Routes;


namespace V6Soft.Web.Accounting.Configs
{
    public static class WebApiConfig
    {
        #region Static

        private static void MapRoute(this HttpConfiguration config, string name,
            string template, object defaults)
        {
            config.Routes.MapHttpRoute(
                name: name,
                routeTemplate: template,
                defaults: defaults
            );
        }

        #endregion


        public static void Register(HttpConfiguration config)
        {
            JsonSerializerSettings jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.All;
            config.DependencyResolver =
                new WebApiDependencyResolver(AppContext.DependencyInjector);

            ConfigRoutes(config);
        }
        

        private static void ConfigRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.MapRoute(RouteNames.DefaultApi, ApiPatterns.DefaultApi,
                new { id = RouteParameter.Optional });
        }
    }
}
