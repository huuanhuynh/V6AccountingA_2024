using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Common.ModelFactory;

namespace V6Soft.Web.Common.Models
{
    /// <summary>
    ///     Represents dynamic model for view.
    /// </summary>
    public class DynamicViewModel
    {
        /// <summary>
        ///     Gets or sets fields.
        /// </summary>
        public IDictionary<string, object> Fields { get; set; }


        public DynamicViewModel(DynamicModel rawModel)
        {
            Fields = rawModel.FieldMap;
        }
    }
}
