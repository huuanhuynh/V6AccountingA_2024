using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using V6Soft.Common.Utils;
using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Common.DeInWcf
{
    public class DeInInstanceProvider : IInstanceProvider
    {
        private readonly IDependencyInjector m_Container;
        private readonly Type m_ContractType;
        
        public DeInInstanceProvider(IDependencyInjector container, Type contractType)
        {
            Guard.ArgumentNotNull(container, "injector");
            Guard.ArgumentNotNull(contractType, "contractType");
            
            m_Container = container;
            m_ContractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var childContainer =
                instanceContext.Extensions.Find<DeInInstanceContextExtension>().GetChildContainer(m_Container);

            return childContainer.Resolve(m_ContractType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            instanceContext.Extensions.Find<DeInInstanceContextExtension>().DisposeOfChildContainer();            
        }        
    }
}
