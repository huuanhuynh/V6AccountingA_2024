using System.Collections.Generic;
using System.Runtime.Serialization;

using V6Soft.Services.Common.Constants;


namespace V6Soft.Services.Wcf.Common.Models
{
    /// <summary>
    ///     Contains exception details when a WCF operation fails.
    /// </summary>
    [DataContract]
    public class OperationFault
    {
        /// <summary>
        ///     Gets or sets error codes that cause the operation to fail.
        /// </summary>
        [DataMember]
        public IList<ErrorCode> ErrorCodes { get; set; }

        public OperationFault()
        {
            ErrorCodes = new List<ErrorCode>();
        }

        public OperationFault(ErrorCode errorCode)
        {
            ErrorCodes = new List<ErrorCode>() { errorCode };
        }

    }
}
