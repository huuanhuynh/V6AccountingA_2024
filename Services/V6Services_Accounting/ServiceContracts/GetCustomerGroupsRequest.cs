using System.ServiceModel;

using V6Soft.Models.Core;
using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Accounting.ServiceContracts
{
    /// <summary>
    ///     GetCustomerGroups contracts message contracts.
    /// </summary>
    [MessageContract]
    public class GetCustomerGroupsRequest : GetModelsRequest
    {
    }
}
