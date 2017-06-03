using System.Web.Mvc;
using System.Web.Routing;

using V6Soft.Web.Accounting.Constants;

using MvcCodeRouting;

using ActionNames = V6Soft.Web.Accounting.Constants.Names.Actions;
using ControllerNames = V6Soft.Web.Accounting.Constants.Names.Controllers;
using RouteNames = V6Soft.Web.Accounting.Constants.Names.Routes;


namespace V6Soft.Web.Accounting.Configs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapCodeRoutes(typeof(Controllers.HomeController));

            routes.MapRoute(
                name: RouteNames.Default,
                url: RoutePatterns.Default,
                defaults: new { controller = ControllerNames.Home, 
                    action = ActionNames.Index, id = UrlParameter.Optional 
                });
        }
    }
}