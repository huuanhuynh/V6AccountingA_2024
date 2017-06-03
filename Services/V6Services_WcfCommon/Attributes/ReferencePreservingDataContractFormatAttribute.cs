using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Services.Wcf.Common.Behaviors;


namespace V6Soft.Services.Wcf.Common.Attributes
{
    /// <summary>
    ///     Tells WCF service to keep circular references in data contracts.
    /// </summary>
    public class ReferencePreservingDataContractFormatAttribute
        : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription description, 
            BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, 
            ClientOperation proxy)
        {
            IOperationBehavior innerBehavior = 
                new ReferencePreservingDataContractSerializerOperationBehavior(
                    description);
            innerBehavior.ApplyClientBehavior(description, proxy);
        }

        public void ApplyDispatchBehavior(OperationDescription description, 
            DispatchOperation dispatch)
        {
            IOperationBehavior innerBehavior = 
                new ReferencePreservingDataContractSerializerOperationBehavior(
                    description);
            innerBehavior.ApplyDispatchBehavior(description, dispatch);
        }

        public void Validate(OperationDescription description)
        {
        }
    }
}
