using System.ServiceModel;

using V6Soft.Services.Common.Models;


namespace V6Soft.Services.Accounting.ServiceContracts
{
    /// <summary>
    /// IsExistCustomer response message contract.
    /// </summary>
    [MessageContract]
    public class IsExistCustomerResponse
    {
        /// <summary>
        /// Gets or sets value indicating customer exists or not.
        /// </summary>
        [MessageBodyMember]
        public bool IsExist { get; set; }
    }
}
