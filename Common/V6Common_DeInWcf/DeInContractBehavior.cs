using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using V6Soft.Common.Utils;


namespace V6Soft.Common.DeInWcf
{
    public class DeInContractBehavior : IContractBehavior
    {
        private readonly IInstanceProvider m_InstanceProvider;

        public DeInContractBehavior(IInstanceProvider instanceProvider)
        {
            Guard.ArgumentNotNull(instanceProvider, "instanceProvider");

            m_InstanceProvider = instanceProvider;
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = m_InstanceProvider;
            dispatchRuntime.InstanceContextInitializers.Add(new DeInInstanceContextInitializer());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}
