using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Web.Common.Dependencies
{
    public class WebApiDependencyResolver
        : DependencyResolverBase, IDependencyResolver
    {
        /// <summary>
        ///     Initializes a new instance of WebApiDependencyResolver with
        ///     specified dependency injector.
        /// </summary>
        public WebApiDependencyResolver(IDependencyInjector injector)
            :base (injector)
        {
        }

        /// <summary>
        ///     See <see cref="IDependencyScope.BeginScope"/>
        /// </summary>
        public IDependencyScope BeginScope()
        {
            IDependencyInjector childInjector = Injector.CreateChildInjector();
            return new WebApiDependencyResolver(childInjector);
        }

        /// <summary>
        ///     See <see cref="IDependencyScope.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Injector.Dispose();
        }
    }
}
