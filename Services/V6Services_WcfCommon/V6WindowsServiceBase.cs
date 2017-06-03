using System;
using System.ServiceModel;

using V6Soft.Common.DeInWcf;
using V6Soft.Services.Common;


namespace V6Soft.Services.Wcf.Common
{
    public abstract class V6WindowsServiceBase : V6ServiceBase
    {
        public V6WindowsServiceBase(string serviceInstanceId, string serviceName)
            : base(serviceInstanceId, serviceName)
        {
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(null);
            OnServiceStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            OnServiceStop();
        }
        
        protected void SetupServiceHost<TServiceInterface, TServiceImpl>(string serviceHostName, string serviceHostUri)
        {
            Logger.LogTraceEntry("Setup Service Host: " + serviceHostName);
            DeInServiceHost serviceHost = ServiceHostBuilder.Build<TServiceInterface, TServiceImpl>(
                DependencyInjector, serviceHostName, serviceHostUri);

            RegisterServiceHostEventHandlers(serviceHost);
            try
            {
                serviceHost.Open();
            }
            catch (Exception exception)
            {
                string fault = string.Format("An exception occurred loading the {0} service", serviceHost);
                Logger.LogException(fault, exception);
                //TODO: Should raise alert
                throw;
            }
            Logger.LogTraceExit("Setup Service Host: " + serviceHostName);
        }
        
        protected abstract void OnServiceStart();
        protected abstract void OnServiceStop();


        private void RegisterServiceHostEventHandlers(DeInServiceHost serviceHost)
        {
            serviceHost.Faulted += new EventHandler(ServiceHost_Faulted);
            serviceHost.Opened += new EventHandler(ServiceHost_Opened);
            serviceHost.Closed += new EventHandler(ServiceHost_Closed);
        }

        private void ServiceHost_Opened(object sender, EventArgs e)
        {
            ServiceHost myservice = sender as ServiceHost;
            Logger.LogInfo(myservice.Description.Name + " Service Host Opened");
        }

        private void ServiceHost_Faulted(object sender, EventArgs e)
        {
            ServiceHost myservice = sender as ServiceHost;
            Logger.LogInfo(myservice.Description.Name + " Service Host Faulted");
            //TODO: Should raise alert
        }

        private void ServiceHost_Closed(object sender, EventArgs e)
        {
            ServiceHost myservice = sender as ServiceHost;
            Logger.LogInfo(myservice.Description.Name + " Service Host Closed");
        }

    }
}
