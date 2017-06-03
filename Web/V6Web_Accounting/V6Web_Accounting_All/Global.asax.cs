using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.Register(BundleTable.Bundles);

            ViewEngines.Engines.EnableCodeRouting();

            Logger.LogTraceExit("V6MvcApplication OnApplicationStart");
        }

        protected override V6AppEngineBase CreateAppEngine(string serviceInstanceId)
        {
            return new V6WebAccountingEngine(null);
        }
    }
}