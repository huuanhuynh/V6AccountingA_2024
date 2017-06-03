using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.Http;

using MvcCodeRouting;

using V6Soft.Web.Common.Module;
using V6Soft.Web.Accounting.Controllers;


namespace V6Soft.Web.Accounting.Application
{
    /// <summary>
    ///     Provides functionalities to work with customer.
    /// </summary>
    public class CustomerModule : WebModule
    {
        /// <summary>
        ///     Initializes a new instance of MenuModule.
        /// </summary>
        public CustomerModule()
        {
            Name = BaseRoute = "Customer";
        }

        /// <summary>
        ///     See <see cref="WebModule.ExportAPIs"/>.
        /// </summary>
        public override Dictionary<string, string> ExportAPIs(System.Web.Mvc.UrlHelper urlHelper)
        {
            return new Dictionary<string, string>()
            {
                { "getgroups", urlHelper.Content("~/Customer/GroupApi/GetGroups") }
            };
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterApiRoutes"/>.
        /// </summary>
        public override void RegisterApiRoutes(System.Web.Http.HttpConfiguration httpConfig)
        {
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterWebRoutes"/>.
        /// </summary>
        public override void RegisterWebRoutes(RouteCollection routes)
        {
            base.RegisterWebRoutes(routes);

            /*
            routes.MapCodeRoutes(
                baseRoute: BaseRoute,
                rootController: typeof(HomeController),
                settings: new CodeRoutingSettings
                {
                    EnableEmbeddedViews = true
                }
             );

            routes.MapRoute(
                name: "CustomerDefault",
                url: "Customer/{controller}/{action}",
                defaults: new
                {
                    //controller = "Template"
                },
                namespaces: new string[] {
                    "V6Soft.Web.Accounting.Modules.Customer.Controllers"
                });
            //*/
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterBundles"/>.
        /// </summary>
        public override void RegisterBundles(System.Web.Optimization.BundleCollection bundles)
        {
        }

    }
}