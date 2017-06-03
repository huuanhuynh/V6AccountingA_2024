using System.Collections.Generic;


namespace V6Soft.Web.Common.Models
{
    /// <summary>
    ///     Represents a collection of URLs needed for a page to work.
    /// </summary>
    public class DependencyUrlCollection
    {
        /// <summary>
        ///     Gets or sets module name.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        ///     Gets or sets url map with key as API name,
        ///     value as URL.
        /// </summary>
        public IDictionary<string, string> UrlMap { get; set; }


        /// <summary>
        ///     Initializes a new instance of DependencyUrlCollection
        /// </summary>
        /// <param name="moduleName">Module name</param>
        public DependencyUrlCollection(string moduleName)
        {
            Module = moduleName;
        }
    }
}
