using System;
using System.Collections.Generic;
using System.ServiceModel;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Response message contract to add dynamic model(s).
    /// </summary>
    [MessageContract]
    public class AddModelResponse
    {
        /// <summary>
        ///     Gets or sets UID generated when adding model.
        /// </summary>
        [MessageBodyMember]
        public Guid NewUID;
    }
}
