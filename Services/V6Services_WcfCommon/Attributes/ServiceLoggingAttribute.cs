using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;


namespace V6Soft.Services.Wcf.Common.Attributes
{
    /// <summary>
    /// Applies logging aspect to all service operations that have not been associated with an OperationAttribute declaration.
    /// In this example, SayHello will inherit the logging settings from ServiceLogging while SayHello2 will use its specific logging settings.
    /// <example>
    /// [ServiceLogging(ParameterLogMode = ParameterLogMode.OnEntry)]
    /// public class HelloWCFImplementation : IHelloWCF
    /// {
    ///     public SayHelloResponse SayHello(SayHelloRequest request)
    ///     {...}
    ///     
    ///     [OperationLogging(ParameterLogMode = ParameterLogMode.OnEntry, LoggableParameters = new[] { "Person", "Name" })]
    ///     public SayHelloResponse SayHello2(SayHelloRequest request)
    ///     {...}    
    /// }
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ServiceLoggingAttribute : Attribute, IServiceBehavior
    {
        private const int DefaultRecursiveDepth = 2;
        private const ParameterLogMode DefaultParameterLogMode =
            Attributes.ParameterLogMode.OnError;

        /// <summary>
        /// Gets/sets the depth of object hierarchy to log recursively. Default value: 2.
        /// </summary>
        public int RecursiveDepth { get; set; }

        /// <summary>
        /// Gets/sets when the parameters should be logged.
        /// </summary>
        public ParameterLogMode? ParameterLogMode { get; set; }

        /// <summary>
        /// An array of parameter names whose values want to log. If this array is null 
        /// or empty, all parameters will be logged when needed. Default value: null.
        /// </summary>
        public string[] LoggableParameters { get; set; }

        /// <summary>
        /// See System.ServiceModel.Description.IServiceBehavior.ApplyDispatchBehavior()
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ServiceEndpoint endpoint in serviceDescription.Endpoints)
            {
                foreach (OperationDescription operation in endpoint.Contract.Operations)
                {
                    if (operation.Behaviors.Find<OperationLoggingAttribute>() != null)
                    {
                        continue;
                    }
                    operation.Behaviors.Add(CreateOperationInterceptor());
                }
            }
        }

        /// <summary>
        /// See System.ServiceModel.Description.IServiceBehavior.AddBindingParameters()
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        { }

        /// <summary>
        /// See System.ServiceModel.Description.IServiceBehavior.Validate()
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        { }

        /// <summary>
        /// Returns a new OperationLoggingAttribute instance.
        /// </summary>
        /// <returns></returns>
        protected OperationLoggingAttribute CreateOperationInterceptor()
        {
            ParameterLogMode logMode = ParameterLogMode.HasValue
                ? ParameterLogMode.Value
                : DefaultParameterLogMode;

            return new OperationLoggingAttribute()
            {
                RecursiveDepth = Math.Max(RecursiveDepth, DefaultRecursiveDepth),
                ParameterLogMode = logMode,
                LoggableParameters = LoggableParameters
            };
        }
    }
}
