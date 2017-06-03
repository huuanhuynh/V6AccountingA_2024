using System.Collections.Generic;
using System.Reflection;
using System.Web.Routing;
using V6Soft.Web.Common.Routing;


namespace V6Soft.Web.Common.Module
{
    /// <summary>
    ///     Represents a group of some particular functionalies that serve
    ///     common type of data.
    ///     <para/>Eg: CustomerModule, MaterialModule...
    /// </summary>
    public abstract class WebModule
    {
        /// <summary>
        ///     Gets or sets module name. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets base route. 
        ///     <para/>Any URL beginning with this string will be processed by this module.
        ///     <para/>Eg: /Customer/GetCustomerList
        /// </summary>
        public string BaseRoute { get; set; }

        
        /// <summary>
        ///     Returns a dictionary with API name as key, API URL as value.
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<string, string> ExportAPIs(System.Web.Mvc.UrlHelper urlHelper);

        /// <summary>
        ///     Registers web API routes.
        /// </summary>
        public abstract void RegisterApiRoutes(System.Web.Http.HttpConfiguration httpConfig);

        /// <summary>
        ///     Registers web MVC routes.
        /// </summary>
        public virtual void RegisterWebRoutes(System.Web.Routing.RouteCollection routes)
        {
            string routePattern = BaseRoute + "/R/{*filepath}";
            Assembly assembly = GetType().Assembly;
            routes.Add(BaseRoute + "EmbeddedStaticFileRoute",
                 new Route(routePattern,
                     new StaticEmbeddedFileRouteHandler(assembly)));
        }

        /// <summary>
        ///     Registers resource bundles.
        /// </summary>
        public abstract void RegisterBundles(System.Web.Optimization.BundleCollection bundles);
    }
}
