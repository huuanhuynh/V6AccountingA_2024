using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V6Soft.Web.Common.Constants
{
    public class Names
    {
        public static class ServerVariables
        {
            /// <summary>
            ///     Value: "REMOTE_ADDR"
            /// </summary>
            public const string RemoteIP = "REMOTE_ADDR";
        }

        /// <summary>
        ///     Constants for Html Element types,
        ///     used for auto-generated tags.
        /// </summary>
        public static class HtmlElements
        {
            /// <summary>
            ///     Constants for &lt;input type="checkbox"&gt;
            ///     <para/>Value: "longtxt"
            /// </summary>
            public const string Checkbox = "checkbox";

            /// <summary>
            ///     Constants for &lt;select&gt;
            ///     <para/>Value: "combobox"
            /// </summary>
            public const string Combobox = "combobox";

            /// <summary>
            ///     Constants for &lt;input type="hidden"&gt;
            ///     <para/>Value: "hidden"
            /// </summary>
            public const string Hidden = "hidden";

            /// <summary>
            ///     Constants for &lt;input type="text"&gt;
            ///     <para/>Value: "txt"
            /// </summary>
            public const string Text = "txt";

            /// <summary>
            ///     Constants for &lt;textarea&gt;
            ///     <para/>Value: "longtxt"
            /// </summary>
            public const string LongText = "longtxt";
        }
    }
}