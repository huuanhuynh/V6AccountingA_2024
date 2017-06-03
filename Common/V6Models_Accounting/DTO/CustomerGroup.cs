using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class CustomerGroup : V6Model
    {
        /// <summary>
        ///     Gets or sets Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        ///     Gets or sets Note
        /// </summary>
        public string Note { get; set; }

    }
}
