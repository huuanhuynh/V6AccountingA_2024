using System;
using System.Web.Http;
using System.Web.Mvc;
using DataAccessLayer.Implementations;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.WebApi.Accounting.Application;
using V6Soft.WebApi.Accounting.Configs;
using V6Soft.Web.Common.Application;

namespace V6Soft.WebApi.Accounting
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class V6ApiApplication : V6HttpApplicationBase
    {
        protected override void OnApplicationStart()
        {
            Logger.LogTraceEntry("V6ApiApplication OnApplicationStart");

            base.OnApplicationStart();

            //AutoMapperConfigurator.Configure();

            var webApp = (V6ApiAccountingEngine)WebApplication;
            webApp.RegisterDependencies();

            //V6SqlConnect.SqlConnect.StartSqlConnect("V6Soft", AppDomain.CurrentDomain.BaseDirectory);
            DataAccessLayer.Interfaces.ILoginServices loginServices = 
                new LoginServices();
            loginServices.StartSql();
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            Logger.LogTraceExit("V6ApiApplication OnApplicationStart");
        }

        protected override V6AppEngineBase CreateAppEngine(string serviceInstanceId)
        {
            return new V6ApiAccountingEngine(null);
        }
    }
}