using System.ServiceModel;


namespace V6Soft.Services.Accounting.ServiceContracts
{
    /// <summary>
    /// IsExistCustomer contracts message contracts.
    /// </summary>
    [MessageContract]
    public class IsExistCustomerRequest
    {
        /// <summary>
        /// Gets or sets customer code.
        /// </summary>
        [MessageBodyMember]
        public string CustomerCode { get; set; }
    }
}
