using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Request message contract to add dynamic model(s).
    /// </summary>
    [MessageContract]
    public class AddModelRequest
    {
        /// <summary>
        ///     Gets or sets dynamic model.
        /// </summary>
        [MessageBodyMember]
        public DynamicModelDC DynamicModel;
    }
}
