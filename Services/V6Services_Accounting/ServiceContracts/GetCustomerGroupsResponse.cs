using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Models.Accounting;
using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Accounting.ServiceContracts
{
    /// <summary>
    /// GetCustomerGroups response message contracts.
    /// </summary>
    [MessageContract]
    public class GetCustomerGroupsResponse : GetModelsResponse
    {
        /// <summary>
        ///     Gets or sets customer groups.
        /// </summary>
        [MessageBodyMember]
        public IList<RuntimeModelDC> CustomerGroups;

    }
}
