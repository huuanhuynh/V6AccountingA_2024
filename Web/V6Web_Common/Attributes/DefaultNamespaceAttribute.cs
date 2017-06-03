using System;


namespace V6Soft.Web.Common.Attributes
{
    /// <summary>
    ///     Stores root namespace to get embedded resource.
    /// </summary>
    public class DefaultNamespaceAttribute : Attribute
    {
        /// <summary>
        ///     Gets default namespace.
        /// </summary>
        public string DefaultNamespace { get; private set; }

        public DefaultNamespaceAttribute(string defaultNamespace)
        {
            DefaultNamespace = defaultNamespace;
        }

    }
}
