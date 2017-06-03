using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using V6Soft.Web.Common.Models;
using V6Soft.Web.Common.Module;


namespace V6Soft.Web.Common.HtmlExtensions
{
    public static class HtmlExtensions
    {
        const string UrlContainerNamePrefix = "url-container-";

        public static MvcHtmlString RenderModuleUrls(this HtmlHelper html, 
            UrlHelper urlHelper)
        {
            IEnumerable<WebModule> modules = ModuleManager.Instance.Modules;
            var htmlBuilder = new StringBuilder();
            string tagString;

            foreach(var module in modules)
            {
                tagString = html.RenderUrlContainer(
                    BuildUrlContainerId(module.Name),
                    module.ExportAPIs(urlHelper)
                );
                htmlBuilder.Append(tagString);
            }

            return new MvcHtmlString(htmlBuilder.ToString());
        }

        /// <summary>
        ///     Renders HTML elements to hold URL needed by a page
        ///     to make requests.
        /// </summary>
        public static MvcHtmlString RenderDependencyUrls(this HtmlHelper html, 
            UrlHelper urlHelper, 
            IEnumerable<DependencyUrlCollection> urlCollections)
        {
            var htmlBuilder = new StringBuilder();
            string tagString;

            foreach(var collection in urlCollections)
            {
                tagString = html.RenderUrlContainer(
                    BuildUrlContainerId(collection.Module), collection.UrlMap                    
                );
                htmlBuilder.Append(tagString);
            }

            return new MvcHtmlString(htmlBuilder.ToString());
        }

        /// <summary>
        ///     Renders an element holding a URL for JavaScript to use to make requests.
        /// </summary>
        public static string RenderUrlContainer(this HtmlHelper html, string id,
            IDictionary<string, string> urlMap)
        {
            if (urlMap == null || !urlMap.Any()) { return null; }

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "hidden");
            tagBuilder.MergeAttribute("id", id);

            foreach (KeyValuePair<string, string> pair in urlMap)
            {
                tagBuilder.MergeAttribute("data-" + pair.Key, pair.Value);
            }

            /* Expected result:
             * <input type="hidden" id="{id}" data-apiname1="..." data-apiname2="..." />
             */
            return tagBuilder.ToString();
        }
        /*
        public static string BuildDynamicFieldElement(this HtmlHelper html, 
            DynamicFieldViewModel fieldModel)
        {
            var divTagBuilder = new TagBuilder("div");
            divTagBuilder.MergeAttribute("class", "form-group");
            
            var labelTagBuilder = new TagBuilder("label");
            labelTagBuilder.InnerHtml = fieldModel.Label;

            var inputTagBuilder = new TagBuilder(fieldModel.HtmlElement);
            if (fieldModel.HtmlElement == "textarea")
            {
                inputTagBuilder = new TagBuilder("textarea");
            }
            else if (fieldModel.HtmlElement != "input")
            {
                inputTagBuilder = new TagBuilder("input");
                // TODO: Should have different types based on field data.
                inputTagBuilder.MergeAttribute("type", fieldModel.HtmlElement);
            }
            //tagBuilder.MergeAttribute("id", id);

            string cssClass = "form-control";

            if (fieldModel.IsRequired)
            {
                cssClass += " required";
            }

            if(fieldModel.MaxLength > 0)
            {
                inputTagBuilder.MergeAttribute("maxlength", fieldModel.MaxLength + "");
            }

            inputTagBuilder.MergeAttribute("class", cssClass);

            divTagBuilder.InnerHtml = labelTagBuilder.ToString() + inputTagBuilder.ToString();

            return divTagBuilder.ToString();
        }
        //*/
        private static string BuildUrlContainerId(string moduleName)
        {
            // Expected result: url-container-modulename
            return UrlContainerNamePrefix + moduleName.ToLower();
        }

    }
}
