using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Response message contract to get runtime model(s).
    /// </summary>
    [MessageContract]
    public class GetModelsResponse
    {
        /// <summary>
        ///     Gets or sets runtime models.
        /// </summary>
        [MessageBodyMember]
        public IList<DynamicModelDC> DynamicModels;

        /// <summary>
        ///     Gets or sets number of results without paging.
        /// </summary>
        [MessageBodyMember]
        public ulong Total;
    }
}
