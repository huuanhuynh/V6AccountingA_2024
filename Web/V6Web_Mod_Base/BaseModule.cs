using System.Collections.Generic;

using MvcCodeRouting;

using V6Soft.Web.Accounting.Modules.Base.Constants;
using V6Soft.Web.Accounting.Modules.Base.Controllers;
using V6Soft.Web.Common.Module;


namespace V6Soft.Web.Accounting.Modules.Base
{
    /// <summary>
    ///     Provides common functionalities for other modules.
    /// </summary>
    public class BaseModule : WebModule
    {
        /// <summary>
        ///     Initializes a new instance of MenuModule.
        /// </summary>
        public BaseModule()
        {
            Name = BaseRoute = "Base";
        }

        public override Dictionary<string, string> ExportAPIs(System.Web.Mvc.UrlHelper urlHelper)
        {
            return null;
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterWebRoutes"/>.
        /// </summary>
        public override void RegisterWebRoutes(System.Web.Routing.RouteCollection routes)
        {
            base.RegisterWebRoutes(routes);

            routes.MapCodeRoutes(
                baseRoute: BaseRoute,
                rootController: typeof(RootController),
                settings: new CodeRoutingSettings
                {
                    EnableEmbeddedViews = true
                }
             );
        }

        public override void RegisterApiRoutes(System.Web.Http.HttpConfiguration httpConfig)
        {
            
        }

        public override void RegisterBundles(System.Web.Optimization.BundleCollection bundles)
        {
            
        }
    }
}