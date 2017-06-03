using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Common.ModelFactory;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Request message contract to get runtime model(s).
    /// </summary>
    [MessageContract]
    public class GetModelsRequest
    {
        /// <summary>
        ///     Gets or sets output fields.
        /// </summary>
        [MessageBodyMember]
        public IList<string> OutputFields { get; set; }

        /// <summary>
        ///     Gets or sets search conditions.
        /// </summary>
        [MessageBodyMember]
        public IList<SearchCriterion> Criteria { get; set; }
        
        /// <summary>
        ///     Gets or sets page index.
        /// </summary>
        [MessageBodyMember]
        public ushort PageIndex { get; set; }

        /// <summary>
        ///     Gets or sets number of items per page.
        /// </summary>
        [MessageBodyMember]
        public ushort PageSize { get; set; }
        
        
        
        [MessageBodyMember]
        public ushort ModelIndex { get; set; }

        [MessageBodyMember]
        public System.Guid UID { get; set; }
        [MessageBodyMember]
        public string Code { get; set; }
    }
}
