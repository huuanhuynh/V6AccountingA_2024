using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace V6Soft.Services.Accounting.Interfaces
{
    [MessageContract]
    public class AddCustomerResponse
    {
        /// <summary>
        /// Gets or sets value indicating customer added or not.
        /// </summary>
        [MessageBodyMember]
        public int AddCustomer { get; set; }
    }
}
