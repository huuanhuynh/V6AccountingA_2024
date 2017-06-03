using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Web.Common.Models
{
    public class DynamicTableModel
    {
        /// <summary>
        ///     Gets or sets column names.
        /// </summary>
        public IEnumerable<string> Columns { get; set; }

        /// <summary>
        ///     Gets or sets the key-value collection of settings.
        /// </summary>
        public IDictionary<string, string> Settings { get; set; }
    }
}
