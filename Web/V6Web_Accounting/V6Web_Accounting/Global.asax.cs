using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using V6Soft.Web.Accounting.Application;
using V6Soft.Web.Accounting.Configs;
using V6Soft.Web.Common.Application;

using MvcCodeRouting;


namespace V6Soft.Web.Accounting
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class V6MvcApplication : V6HttpApplicationBase
    {
        protected override void OnApplicationStart()
        {
            Logger.LogTraceEntry("V6MvcApplication OnApplicationStart");

            base.OnApplicationStart();

            var webApp = (V6AccountingWebApplication)WebApplication;
            webApp.RegisterDependencies();
            webApp.RegisterModules(RouteTable.Routes, GlobalConfiguration.Configuration, BundleTable.Bundles);
                        
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.Register(BundleTable.Bundles);

            ViewEngines.Engines.EnableCodeRouting();

            Logger.LogTraceExit("V6MvcApplication OnApplicationStart");
        }

        protected override V6WebApplicationBase CreateApplication(string serviceInstanceId)
        {
            return new V6AccountingWebApplication(null);
        }
    }
}