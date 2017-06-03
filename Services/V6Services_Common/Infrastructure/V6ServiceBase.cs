using V6Soft.Common.Logging;
using V6Soft.Common.Utils.DependencyInjection;
using V6Soft.Services.Common.Configuration;
using V6Soft.Services.Common.Infrastructure;


namespace V6Soft.Services.Common.Infrastructure
{
    public abstract class V6ServiceBase : System.ServiceProcess.ServiceBase
    {
        /// <summary>
        /// Gets or sets the dependency resolver.
        /// </summary>
        protected IDependencyInjector DependencyInjector { get; set; }

        /// <summary>
        /// Gets or sets logger.
        /// </summary>
        protected ILogger Logger { get; set; }
        
        /// <summary>
        /// Gets or sets the Id of this deployed instance of this service.
        /// </summary>
        protected string InstanceId { get; set; }


        public V6ServiceBase(string serviceInstanceId, string serviceName)
        {
            InstanceId = serviceInstanceId;
            ServiceName = serviceName;
            
            DependencyInjector = CreateDependencyInjector();
            Logger = CreateLogger();
            DependencyInjector.RegisterInstance<ILogger>(Logger);
            AppContext.DependencyInjector = DependencyInjector;
            AppContext.Logger = Logger;
        }


        protected override void OnStart(string[] args)
        {
            Logger.LogTraceEntry("V6ServiceBase OnStart");
            base.OnStart(args);

            GetServiceInstanceConfiguration();
            StartHeartBeat();

            Logger.LogTraceExit("V6ServiceBase OnStart");

        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        /// <summary>
        /// Starts broadcasting signals to indicate that this service is still working.
        /// </summary>
        protected virtual void StartHeartBeat()
        {
        }

        protected virtual void ApplyConfiguration(ServiceConfiguration configuration)
        {
        }
           
        protected virtual IDependencyInjector CreateDependencyInjector()
        {
            return new UnityDependencyInjector();
        }

        protected virtual ILogger CreateLogger()
        {
            return new MicrosoftLogger();
        }
  

        /// <summary>
        /// Gets the configuration from a configuration service.
        /// </summary>
        private void GetServiceInstanceConfiguration()
        {
            var configuration = new ServiceConfiguration();
            ApplyConfiguration(configuration);
        }

    }
}
