using System.Collections.Generic;
using System.ServiceModel;
using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Request message contract to modify runtime model(s).
    /// </summary>
    [MessageContract]
    public class ModifyModelRequest
    {
        /// <summary>
        ///     Gets or sets runtime model.
        /// </summary>
        [MessageBodyMember]
        public RuntimeModelDC RuntimeModel;
        [MessageBodyMember]
        public ushort ModelIndex;
    }
}
