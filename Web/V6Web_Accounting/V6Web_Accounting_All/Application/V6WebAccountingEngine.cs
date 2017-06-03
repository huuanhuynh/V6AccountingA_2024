using System.Web.Mvc;

using V6Soft.Web.Common.Application;
using V6Soft.Web.Common.Dependencies;


namespace V6Soft.Web.Accounting.Application
{
    public class V6WebAccountingEngine : V6AppEngineBase
    {
        public V6WebAccountingEngine(string serviceInstanceId)
            : base(serviceInstanceId, "V6 Web-based Accounting Management System")
        {
            InstanceId = serviceInstanceId;
        }
        
        protected override void OnStart(string[] args)
        {
            Logger.LogTraceEntry("V6WebAccountingEngine OnStart");
            base.OnStart(args);
            DependencyResolver.SetResolver(new MvcDependencyResolver(DependencyInjector));

            Logger.LogTraceExit("V6WebAccountingEngine OnStart");
        }
    }
}