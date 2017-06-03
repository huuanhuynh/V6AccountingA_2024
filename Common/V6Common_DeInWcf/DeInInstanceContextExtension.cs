using System;
using System.ServiceModel;

using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Common.DeInWcf
{
    public class DeInInstanceContextExtension : IExtension<InstanceContext>
    {
        private IDependencyInjector m_ChildContainer;


        public IDependencyInjector GetChildContainer(IDependencyInjector container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("injector");
            }

            return m_ChildContainer ?? (m_ChildContainer = container.CreateChildInjector());
        }

        public void DisposeOfChildContainer()
        {
            if (m_ChildContainer != null)
            {
                m_ChildContainer.Dispose();
            }
        }

        public void Attach(InstanceContext owner)
        {
        }

        public void Detach(InstanceContext owner)
        {
        }
    }
}
