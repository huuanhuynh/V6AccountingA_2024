using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

using V6Soft.Common.Utils.StringExtensions;
using V6Soft.Web.Common.JsonExtensions;
using V6Soft.Web.Accounting.Constants;
using V6Soft.Web.Common.Module;

using ActionNames = V6Soft.Web.Accounting.Constants.Names.Actions;


namespace V6Soft.Web.Accounting.HtmlExtensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        ///     Renders menu tree with specified <paramref name="level"/>.
        /// </summary>
        /// <param name="level">Number of descendant levels.</param>
        public static MvcHtmlString RenderMenu(this HtmlHelper helper, byte level)
        {
            return helper.Action(Names.Actions.Menu, Names.Controllers.Template);
        }

        /// <summary>
        ///     Converts C# constants to Json then embeds to Html.
        /// </summary>
        public static MvcHtmlString PrintJavascriptConstants(this HtmlHelper helper)
        {
            string scope = "V6Server";
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetRoutePatterns(scope));

            var tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("type", "text/javascript");
            tagBuilder.InnerHtml = stringBuilder.ToString();
            return new MvcHtmlString(tagBuilder.ToString());
        }

        private static string GetRoutePatterns(string scope)
        {
            return typeof(RoutePatterns).GetJsonFromConstants(scope, 
                p => "~/" + p); // Prepend ~/ to each of route pattern for easy URL resolving.
        }
        
    }
}