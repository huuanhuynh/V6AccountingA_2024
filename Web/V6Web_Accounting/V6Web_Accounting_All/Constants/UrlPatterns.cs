using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V6Soft.Web.Accounting.Constants
{
    /// <summary>
    ///     Is used by RouteConfig to set up routes.
    /// </summary>
    public static class RoutePatterns
    {
        /// <summary>
        ///     Value: "{controller}/{action}/{id}"
        /// </summary>
        public const string Default = "{controller}/{action}/{id}";

        /// <summary>
        ///     Value: "Menu/{id}/{friendlyUrl}"
        /// </summary>
        public const string SubMenuPage = "Menu/{id}/{friendlyUrl}";

        /// <summary>
        ///     Value: "Template/{viewName}"
        /// </summary>
        public const string View = "Template/{viewName}";
    }

    /// <summary>
    ///     Is used by WebApiConfig to set up routes.
    /// </summary>
    public static class ApiPatterns
    {
        /// <summary>
        ///     Value: "api/Menu/{action}"
        /// </summary>
        public const string Menu = "api/Menu/{action}";

        /// <summary>
        ///     Value: "api/{controller}/{action}/{id}"
        /// </summary>
        public const string DefaultApi = "api/{controller}/{action}/{id}";
    }
}