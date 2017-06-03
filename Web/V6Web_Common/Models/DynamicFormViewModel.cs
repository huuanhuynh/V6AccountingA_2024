using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Web.Common.Models
{
    public class DynamicFormViewModel
    {
        /// <summary>
        ///     Gets or set group names.
        /// </summary>
        public string[] Groups { get; set; }

        /// <summary>
        ///     Gets or sets field view models
        /// </summary>
        public IList<DynamicFieldViewModel> Fields { get; set; }

        /// <summary>
        ///     Gets or sets settings dictionary.
        /// </summary>
        public IDictionary<string, string> Settings { get; set; }


        public DynamicFieldViewModel this[string fieldName] 
        {
            get
            {
                return Fields.Where(f => f.Name == fieldName).FirstOrDefault();
            }
        }
    }
}
