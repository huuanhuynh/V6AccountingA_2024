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

            routes.MapCodeRoutes(typeof(Controllers.HomeController));

            routes.MapRoute(
                name: RouteNames.SubMenuPage,
                url: RoutePatterns.SubMenuPage,
                defaults: new {
                    controller = ControllerNames.Menu,
                    action = ActionNames.ViewSubItems, 
                    //action = ActionNames.Index, 
                    //viewName = Views.SubMenuPage,
                    friendlyUrl = UrlParameter.Optional,
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                name: "Template",
                url: "List/{viewName}",
                defaults: new {
                    controller = ControllerNames.Template, 
                    action = ActionNames.Index
                },
                namespaces: new string[] {
                    "V6Soft.Web.Accounting.Controllers"
                });

            routes.MapRoute(
                name: RouteNames.Default,
                url: RoutePatterns.Default,
                defaults: new { controller = ControllerNames.Home, 
                    action = ActionNames.Index, id = UrlParameter.Optional 
                });
        }
    }
}