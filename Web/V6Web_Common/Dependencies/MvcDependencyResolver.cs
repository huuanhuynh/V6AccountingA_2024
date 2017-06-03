using System.Web.Mvc;

using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Web.Common.Dependencies
{
    /// <summary>
    ///     Used by Asp.Net MVC engine to automatically resolves dependencies for 
    ///     web controllers.
    /// </summary>
    public class MvcDependencyResolver 
        : DependencyResolverBase, IDependencyResolver
    {
        /// <summary>
        ///     Initializes a new instance of MvcDependencyResolver with
        ///     specified dependency injector.
        /// </summary>
        public MvcDependencyResolver(IDependencyInjector injector)
            :base (injector)
        {
        }
    }
}
