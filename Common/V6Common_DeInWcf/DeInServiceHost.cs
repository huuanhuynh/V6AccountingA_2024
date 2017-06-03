using System;
using System.ServiceModel;
using System.ServiceModel.Description;

using V6Soft.Common.Utils;
using V6Soft.Common.Utils.DependencyInjection;


namespace V6Soft.Common.DeInWcf
{
    /// <summary>
    /// This dependency-injected service host accepts a DI injector to resolve dependencies.
    /// </summary>
    public class DeInServiceHost : ServiceHost
    {
        public DeInServiceHost(IDependencyInjector injector, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            Guard.ArgumentNotNull(injector, "injector");
            
            ApplyServiceBehaviors(injector);        

            ApplyContractBehaviors(injector);

            foreach (var contractDescription in ImplementedContracts.Values)
            {
                var contractBehavior =
                    new DeInContractBehavior(new DeInInstanceProvider(injector, contractDescription.ContractType));

                contractDescription.Behaviors.Add(contractBehavior);
            }
        }

        private void ApplyContractBehaviors(IDependencyInjector injector)
        {
            var registeredContractBehaviors = injector.ResolveAll<IContractBehavior>();

            foreach (var contractBehavior in registeredContractBehaviors)
            {
                foreach (var contractDescription in ImplementedContracts.Values)
                {
                    contractDescription.Behaviors.Add(contractBehavior);
                }                
            }
        }

        private void ApplyServiceBehaviors(IDependencyInjector injector)
        {
            var registeredServiceBehaviors = injector.ResolveAll<IServiceBehavior>();

            foreach (var serviceBehavior in registeredServiceBehaviors)
            {
                Description.Behaviors.Add(serviceBehavior);
            }
        }
    }
}