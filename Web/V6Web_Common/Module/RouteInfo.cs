
namespace V6Soft.Web.Common.Module
{
    /// <summary>
    ///     Stores routing information.
    /// </summary>
    public struct RouteInfo
    {
        /// <summary>
        ///     Gets or sets name.
        /// </summary>
        public string Name;

        /// <summary>
        ///     Gets or sets url pattern.
        /// </summary>
        public string UrlPattern;

        /// <summary>
        ///     Gets or sets default route values.
        /// </summary>
        public object Defaults;

        /// <summary>
        ///     Gets or sets route constraints.
        /// </summary>
        public object Constraints;

        /// <summary>
        ///     Initializes a new instance of RouteInfo
        /// </summary>
        /// <param name="name">Mandatory</param>
        /// <param name="urlPattern">Mandatory</param>
        /// <param name="defaults">Optional</param>
        /// <param name="constraints">Optional</param>
        public RouteInfo(string name, string urlPattern, object defaults = null,
            object constraints = null)
        {
            Name = name;
            UrlPattern = urlPattern;
            Defaults = defaults;
            Constraints = constraints;
        }
    }
}
