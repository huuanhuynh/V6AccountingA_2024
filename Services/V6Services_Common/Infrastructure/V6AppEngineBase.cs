using System.Threading.Tasks;


namespace V6Soft.Web.Common.Application
{
    /// <summary>
    /// Provides basic behaviors for V6 App Engines.<br/>
    /// App Engine is the core responsible for all business processings, 
    ///     App Engine can be hosted on a Http Application (ASP MVC Web, ASP Webform Web), 
    ///     a WinForm Application, or even a Windows Service. Moreover, it never directly writes
    ///     logs, produces output or accepts user events/requests. Instead, the hosting layer
    ///     must provide these functionalities.<br/>
    /// An V6 App Engine includes Data Dealers, Farmers and lower layers.
    /// </summary>
    public abstract class V6AppEngineBase : V6Soft.Services.Common.Infrastructure.V6ServiceBase
    {
        public V6AppEngineBase(string serviceInstanceId, string serviceName)
            : base(serviceInstanceId, serviceName)
        {
        }

        public virtual Task Start()
        {
            Logger.LogTraceEntry("V6AppEngineBase Start");

            var task = Task.Factory.StartNew(() =>
            {
                OnStart(null);
            });

            Logger.LogTraceExit("V6AppEngineBase Start");

            return task;            
        }

        public virtual new void Stop()
        {
            Logger.LogTraceEntry("V6AppEngineBase Stop");

            OnStop();

            Logger.LogTraceExit("V6AppEngineBase Stop");

        }
        
    }
}
