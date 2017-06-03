using System.Reflection;
using System.Web;
using System.Web.Routing;


namespace V6Soft.Web.Common.Routing
{
    /// <summary>
    ///     Routes static file requests to our handmade static file handler
    /// </summary>
    public class StaticEmbeddedFileRouteHandler : IRouteHandler
    {
        private const string FilePathRouteElementName = "filepath";

        private readonly Assembly m_Assembly;

        public StaticEmbeddedFileRouteHandler(Assembly assembly)
        {
            m_Assembly = assembly;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var filepath = requestContext.RouteData.Values[FilePathRouteElementName] as string;

            return new StaticEmbeddedFileHttpHandler(filepath, m_Assembly);
        }
    }
}
