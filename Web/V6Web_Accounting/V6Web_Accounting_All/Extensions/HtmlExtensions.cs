using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using V6Soft.Web.Accounting.Constants;
using V6Soft.Web.Common.JsonExtensions;
using V6Soft.Web.Common.Models;

using HtmlElemNames = V6Soft.Web.Common.Constants.Names.HtmlElements;


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
        /*
        /// <summary>
        ///     Renders dynamic list.
        /// </summary>
        public static MvcHtmlString RenderDynamicList(this HtmlHelper helper, 
            string modelName, dynamic parameters)
        {
            return helper.Action(Names.Actions.List, Names.Controllers.DynamicModel,
                new { modelName = modelName, parameters = parameters });
        }

        /// <summary>
        ///     Renders dynamic list.
        /// </summary>
        public static MvcHtmlString RenderDynamicEdit(this HtmlHelper helper,
            DynamicModel model)
        {
            return helper.Action(Names.Actions.Edit, Names.Controllers.DynamicModel,
                new { model = model });
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
        public static MvcHtmlString BuildHtmlElement(this HtmlHelper helper,
            string tagName, DynamicFieldViewModel fieldModel, object attributes = null,
            string prepend = "", string append = "")
        {
            var stringBuilder = new StringBuilder(prepend);
            var tagBuilder = new TagBuilder(tagName);
            string fieldName;

            // Attaches initial attributes
            if (attributes != null)
            {
                foreach(PropertyInfo prop in attributes.GetType().GetProperties())
                {
                    tagBuilder.MergeAttribute(prop.Name, 
                        prop.GetValue(attributes) + "");
                }
            }

            fieldName = fieldModel.Name.ToLower();
            tagBuilder.MergeAttribute("name", fieldName);

            fieldName = "dynModel." + fieldName;
            tagBuilder.MergeAttribute("ng-model", fieldName);

            AssignTagValue(tagBuilder, fieldModel, fieldName);

            if (fieldModel.HtmlElement != HtmlElemNames.Hidden)
            {
                AddConstraintAttributes(fieldModel, tagBuilder);
            }

            stringBuilder.Append(tagBuilder.ToString()).Append(append);
            return new MvcHtmlString(stringBuilder.ToString());
        }
        //*/

        /// <summary>
        ///     Builds and HTML element that holds settings for view.
        /// </summary>
        public static MvcHtmlString BuildSettingsElement(this HtmlHelper helper,
            IDictionary<string, string> settings)
        {
            var tagBuilder = new TagBuilder("aside");

            tagBuilder.MergeAttribute("id", "page-settings");
            foreach(var setting in settings)
            {
                tagBuilder.MergeAttribute("data-" + setting.Key, setting.Value);
            }

            return new MvcHtmlString(tagBuilder.ToString());
        }
        /*
        public static string RequiredClass(this HtmlHelper helper, DynamicFieldViewModel field)
        {
            return field.IsRequired ? "control-label--required" : string.Empty;
        }
        
        public static string FieldId(this HtmlHelper helper, string formId, DynamicFieldViewModel field)
        {
            return string.Format("{0}:{1}", formId, field.Name);
        }

        public static string ErrorClass(this HtmlHelper helper, DynamicFieldViewModel field)
        {
            return string.Format("{{ 'has-error' : frmEdit.{0}.$invalid && !frmEdit.{0}.$pristine }}", 
                field.Name);
        }

        private static string GetRoutePatterns(string scope)
        {
            return typeof(RoutePatterns).GetJsonFromConstants(scope, 
                p => "~/" + p); // Prepend ~/ to each of route pattern for easy URL resolving.
        }
        
        private static void AddConstraintAttributes(DynamicFieldViewModel fieldModel, TagBuilder tagBuilder)
        {
            if (fieldModel.IsRequired)
            {
                tagBuilder.MergeAttribute("required", "required");
                tagBuilder.MergeAttribute("ng-required", "true");
            }

            if (fieldModel.MaxLength > 0)
            {
                string maxLength = fieldModel.MaxLength + "";
                tagBuilder.MergeAttribute("maxlength", maxLength);
                tagBuilder.MergeAttribute("ng-maxlength", maxLength);
            }
        }

        private static void AssignTagValue(TagBuilder tagBuilder, DynamicFieldViewModel fieldModel,
            string fieldName)
        {
            object fieldValue = fieldModel.Value;
            if (!string.IsNullOrEmpty(fieldValue + ""))
            {
                switch (fieldModel.HtmlElement)
                {
                    case HtmlElemNames.Checkbox:
                        if (null != (fieldValue as bool?))
                        {
                            tagBuilder.MergeAttribute("checked", "checked");
                            tagBuilder.MergeAttribute("ng-init", fieldName + "=true");
                        }
                        break;
                    case HtmlElemNames.LongText:
                        tagBuilder.InnerHtml = fieldValue + "";
                        tagBuilder.MergeAttribute("ng-init", 
                            string.Format("{0}='{1}'", fieldName, fieldValue)
                        );
                        break;
                    default:
                        tagBuilder.MergeAttribute("value", fieldValue + "");
                        tagBuilder.MergeAttribute("ng-init",
                            string.Format("{0}='{1}'", fieldName, fieldValue)
                        );
                        break;
                }
            }
        }
        //*/
    }
}