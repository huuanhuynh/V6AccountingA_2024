using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using V6Soft.Web.Accounting.Constants;
using V6Soft.Web.Common.Module;

using ActionNames = V6Soft.Web.Accounting.Constants.Names.Actions;
using ControllerNames = V6Soft.Web.Accounting.Constants.Names.Controllers;
using RouteNames = V6Soft.Web.Accounting.Constants.Names.Routes;


namespace V6Soft.Web.Accounting.Application
{
    /// <summary>
    ///     Provides functionalities to work with menu.
    /// </summary>
    public class MenuModule : WebModule
    {
        /// <summary>
        ///     Initializes a new instance of MenuModule.
        /// </summary>
        public MenuModule()
        {
            Name = "Menu";
        }

        /// <summary>
        ///     See <see cref="WebModule.ExportAPIs"/>.
        /// </summary>
        public override Dictionary<string, string> ExportAPIs(UrlHelper urlHelper)
        {
            var urlMap = new Dictionary<string, string>();
            urlMap.Add("getmenutree", urlHelper.HttpRouteUrl(RouteNames.MenuApi, 
                new { action = ActionNames.GetMenuTree }));
            urlMap.Add("getmenuchildren", urlHelper.HttpRouteUrl(RouteNames.MenuApi,
                new { action = ActionNames.GetChildren }));

            return urlMap;
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterApiRoutes"/>
        /// </summary>
        public override void RegisterApiRoutes(System.Web.Http.HttpConfiguration httpConfig)
        {
            httpConfig.Routes.MapHttpRoute(RouteNames.MenuApi, ApiPatterns.Menu, 
                new { controller = ControllerNames.MenuApi }
            );
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterWebRoutes"/>
        /// </summary>
        public override void RegisterWebRoutes(System.Web.Routing.RouteCollection routes)
        {
            // TODO: Should register template action here
        }

        /// <summary>
        ///     See <see cref="WebModule.RegisterBundles"/>
        /// </summary>
        public override void RegisterBundles(System.Web.Optimization.BundleCollection bundles)
        {
            
        }
    }
}