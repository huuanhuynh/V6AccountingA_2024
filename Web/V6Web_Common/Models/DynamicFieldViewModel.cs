using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory;
using HtmlElemNames = V6Soft.Web.Common.Constants.Names.HtmlElements;


namespace V6Soft.Web.Common.Models
{
    /// <summary>
    ///     Represents a field of a form view.
    /// </summary>
    public class DynamicFieldViewModel
    {
        /// <summary>
        ///     Gets or sets field name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets localized label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     Gets or sets group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///     Gets or sets HTML tag.
        /// </summary>
        public string HtmlElement { get; set; }

        /// <summary>
        ///     Gets or sets value indicating whether this field is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     Gets or sets max number of character allowed to input.
        /// </summary>
        public int MaxLength { get; set; }
        
        /// <summary>
        ///     Gets or sets field value.
        /// </summary>
        public object Value { get; set; }


        public DynamicFieldViewModel(ModelFieldDefinition fieldDef, object value)
        {
            Name = fieldDef.Name;
            Label = fieldDef.Label;
            Group = fieldDef.Group;
            IsRequired = false;
            MaxLength = 0;
            Value = value;

            ProcessConstraints(fieldDef.Constraints);
            DetermineHtmlElement(fieldDef);
        }


        private void DetermineHtmlElement(ModelFieldDefinition fieldDef)
        {
            switch (fieldDef.Type.Name)
            {
                case "Boolean":
                    HtmlElement = HtmlElemNames.Checkbox;
                    break;
                default:
                    if (MaxLength > 100)
                    {
                        HtmlElement = HtmlElemNames.LongText;
                    }
                    else if(string.IsNullOrEmpty(Label))
                    {
                        HtmlElement = HtmlElemNames.Hidden;
                    }
                    else
                    {
                        HtmlElement = HtmlElemNames.Text;
                    }
                    break;
            }
        }

        private void ProcessConstraints(IList<IFieldConstraint> constraints)
        {            
            foreach (var constraint in constraints)
            {
                switch (constraint.GetType().Name)
                {
                    case "LengthConstraint":
                        MaxLength = ((LengthConstraint)constraint).MaxLength;
                        break;
                    case "NotNullFieldConstraint":                        
                        IsRequired = true;
                        break;
                }
            }
        }
    }
}
