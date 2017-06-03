using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Request message contract to remove runtime model(s).
    /// </summary>
    [MessageContract]
    public class RemoveModelRequest
    {
        /// <summary>
        ///     Gets or sets UID of the removed runtime model.
        /// </summary>
        [MessageBodyMember]
        public Guid UID;
        [MessageBodyMember]
        public ushort ModelIndex;
    }
}
