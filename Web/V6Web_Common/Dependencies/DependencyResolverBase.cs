using System;
using System.Collections.Generic;

using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Web.Common.Dependencies
{
    /// <summary>
    ///     Provides methods to register and resolve dependencies.
    /// </summary>
    public abstract class DependencyResolverBase
    {
        protected IDependencyInjector Injector { get; set; }


        /// <summary>
        ///     Initializes a new instance of DependencyResolverBase with
        ///     specified dependency injector.
        /// </summary>
        public DependencyResolverBase(IDependencyInjector injector)
        {
            Injector = injector;
        }

        /// <summary>
        ///     Resolves an instance of the specified service type.
        /// </summary>
        public object GetService(Type serviceType)
        {
            try
            {
                return Injector.Resolve(serviceType);
            }
            catch
            {
                    return null;
            }
        }

        /// <summary>
        ///     Resolves many instanaces of this service type.
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Injector.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}
