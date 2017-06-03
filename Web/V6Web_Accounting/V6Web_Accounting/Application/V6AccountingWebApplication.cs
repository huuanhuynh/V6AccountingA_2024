using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;

using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Interfaces.Accounting.Assistant.DataDealers;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.ServiceClients.Accounting.Assistant.DataDealers;
using V6Soft.ServiceClients.Accounting.Customer.DataDealers;
using V6Soft.Web.Common;
using V6Soft.Web.Common.Application;
using V6Soft.Web.Common.Module;


namespace V6Soft.Web.Accounting.Application
{
    public class V6AccountingWebApplication : V6WebApplicationBase
    {
        public V6AccountingWebApplication(string serviceInstanceId)
            : base(serviceInstanceId, "V6 Accounting Web Application")
        {
            InstanceId = serviceInstanceId;
        }

        public void RegisterDependencies()
        {
            Logger.LogTraceEntry("V6AccountingWebApplication RegisterDependencies");

            RuntimeModelFactory.DefinitionLoader
                = new ServiceModelDefinitionLoader(new ModelDefinitionDataDealer());

            DependencyInjector.RegisterType<ICustomerDataDealer, CustomerDataDealer>();
            DependencyInjector.RegisterType<IAssistantDataDealer, AssistantDataDealer>();
            DependencyInjector.RegisterType<IMenuDataDealer, MenuDataDealer>();

            Logger.LogTraceExit("V6AccountingWebApplication RegisterDependencies");
        }
        
        public void RegisterModules(RouteCollection routeCollection, HttpConfiguration httpConfig, 
            BundleCollection bundleCollection)
        {
            ModuleManager manager = ModuleManager.Instance;
            ModuleRegistrator.RegisterModules(manager, routeCollection, httpConfig, bundleCollection);
        }
    }
}