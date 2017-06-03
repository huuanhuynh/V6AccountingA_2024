using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;

using V6Soft.Web.Common.Module;
using V6Soft.Web.Accounting.Application;


namespace V6Soft.Web.Accounting.Application
{
    public static class ModuleRegistrator
    {
        /// <summary>
        ///     Registers module instances to the specified module managers.
        /// </summary>
        /// <param name="manager">The module manager to register with.</param>
        public static void RegisterModules(ModuleManager manager, RouteCollection routes, 
            HttpConfiguration httpConfig, BundleCollection bundles)
        {
            var modules = manager.Modules = new WebModule[] {
                new MenuModule(), new CustomerModule()
            };

            foreach(var module in modules)
            {
                module.RegisterWebRoutes(routes);
                module.RegisterApiRoutes(httpConfig);
                module.RegisterBundles(bundles);
            }
        }
    }
}