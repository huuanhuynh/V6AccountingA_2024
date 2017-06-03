using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.ModelFactory.Constants
{
    /// <summary>
    /// Collection of object names
    /// </summary>
    public static class Names
    {
        /// <summary>
        /// Collection of stored procedure names.
        /// </summary>
        public static class Proc
        {
            public const string GetModelDefinitions = "dbo.GetModelDefinitions";
        }

        /// <summary>
        /// Collection of Xml object names.
        /// </summary>
        public static class Xml
        {
            #region Elements

            /// <summary>
            /// Namespace of XML Schema
            /// </summary>
            public const string XsdNamespace = "xsd";

            /// <summary>
            /// Name of &lt;xsd:attribute&gt; tag.
            /// </summary>
            public const string AttributeElem = "xsd:attribute";

            /// <summary>
            /// Name of &lt;xsd:restriction&gt; tag.
            /// </summary>
            public const string RestrictionElem = "xsd:restriction";

            /// <summary>
            /// Name of &lt;group&gt; tag.
            /// </summary>
            public const string GroupElem = "group";

            /// <summary>
            /// Name of &lt;map&gt; tag.
            /// </summary>
            public const string MapElem = "map";

            /// <summary>
            /// Name of &lt;mappings&gt; tag.
            /// </summary>
            public const string MappingsElem = "mappings";

            /// <summary>
            /// Name of &lt;xsd:maxLength&gt; tag.
            /// </summary>
            public const string MaxLengthElem = "xsd:maxLength";

            #endregion


            #region Attributes

            /// <summary>
            /// Name of attribute "base".
            /// </summary>
            public const string BaseAttr = "base";

            /// <summary>
            /// Name of attribute "code".
            /// </summary>
            public const string CodeAttr = "code";

            /// <summary>
            /// Name of attribute "group".
            /// </summary>
            public const string GroupAttr = "group";

            /// <summary>
            /// Name of attribute "name".
            /// </summary>
            public const string NameAttr = "name";

            /// <summary>
            /// Name of attribute "label".
            /// </summary>
            public const string LabelAttr = "label";

            /// <summary>
            /// Name of attribute "type".
            /// </summary>
            public const string TypeAttr = "type";

            /// <summary>
            /// Name of attribute "sortable".
            /// </summary>
            public const string SortableAttr = "sortable";

            /// <summary>
            /// Name of attribute "use".
            /// </summary>
            public const string UseAttr = "use";

            /// <summary>
            /// Name of attribute "value".
            /// </summary>
            public const string ValueAttr = "value";

            /// <summary>
            /// Name of attribute "visible".
            /// </summary>
            public const string VisibleAttr = "visible";

            #endregion

        }
    }
}
