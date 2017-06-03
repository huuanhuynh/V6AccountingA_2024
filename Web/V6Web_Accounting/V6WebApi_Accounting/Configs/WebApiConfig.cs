using System.Web.Http;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.Web.Common.Dependencies;



namespace V6Soft.WebApi.Accounting.Configs
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
            // Do NOT change the Json.NET property naming convention. All name aliasing must be done by Breeze on the client.
            /*
            JsonSerializerSettings jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            */

            config.DependencyResolver =
                new WebApiDependencyResolver(AppContext.DependencyInjector);

            ConfigRoutes(config);
        }

        private static void ConfigRoutes(HttpConfiguration config)
        {
            // Must be this exact order
            // 1.
            config.MapHttpAttributeRoutes();

            // 2.
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}
