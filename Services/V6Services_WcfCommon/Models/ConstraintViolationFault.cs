using System.Collections.Generic;
using System.Runtime.Serialization;

using V6Soft.Services.Common.Constants;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Services.Wcf.Common.Models
{
    /// <summary>
    ///     Contains exception details when a WCF operation fails
    ///     because the models don't satisfy constraints.
    /// </summary>
    [DataContract]
    public class ConstraintViolationFault : OperationFault
    {
        /// <summary>
        ///     Gets or sets the model indeses that violates constraints.
        /// </summary>
        [DataMember]
        public IList<ModelIndex> ModelIndeses { get; set; }


        public ConstraintViolationFault()
            : base(ErrorCode.ConstraintViolation)
        {
        }
    }
}
