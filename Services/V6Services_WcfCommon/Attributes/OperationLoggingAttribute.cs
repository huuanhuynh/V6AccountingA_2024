using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;


namespace V6Soft.Services.Wcf.Common.Attributes
{

    /// <summary>
    /// Applies logging aspect to a service operation.
    /// <example>
    /// public class HelloWCFImplementation : IHelloWCF
    /// {
    ///     [OperationLogging]
    ///     public SayHelloResponse SayHello(SayHelloRequest request)
    ///     {...}
    ///     
    ///     [OperationLogging(ParameterLogMode = ParameterLogMode.OnEntry, LoggableParameters = new[] { "Person", "Name" })]
    ///     public SayHelloResponse SayHello2(SayHelloRequest request)
    ///     {...}
    ///     
    ///     [OperationLogging(ParameterLogMode = ParameterLogMode.OnEntry | ParameterLogMode.OnError)]
    ///     public SayHelloResponse SayHello3(SayHelloRequest request)
    ///     {...}
    /// }
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class OperationLoggingAttribute : Attribute, IOperationBehavior
    {
        /// <summary>
        /// Gets/sets the level of object hierarchy to log. Default value: 0.
        /// </summary>
        public int RecursiveDepth { get; set; }

        /// <summary>
        /// Gets or sets the parameter log mode.
        /// </summary>
        /// <value>The parameter log mode.</value>
        public ParameterLogMode ParameterLogMode { get; set; }

        /// <summary>
        /// An array of parameter names whose values want to log. If this array is null 
        /// or empty, all parameters will be logged when needed. Default value: null.
        /// </summary>
        public string[] LoggableParameters { get; set; }

        /// <summary>
        /// See System.ServiceModel.Description.IOperationBehavior.ApplyDispatchBehavior()
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription,
            DispatchOperation dispatchOperation)
        {
            IOperationInvoker oldInvoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = CreateInvoker(oldInvoker, operationDescription,
                dispatchOperation);
        }

        /// <summary>
        /// See System.ServiceModel.Description.IOperationBehavior.AddBindingParameters()
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(OperationDescription operationDescription,
            BindingParameterCollection bindingParameters)
        { }

        /// <summary>
        /// See System.ServiceModel.Description.IOperationBehavior.ApplyClientBehavior()
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="clientOperation"></param>
        public void ApplyClientBehavior(OperationDescription operationDescription,
            ClientOperation clientOperation)
        { }

        /// <summary>
        /// See System.ServiceModel.Description.IOperationBehavior.Validate()
        /// </summary>
        /// <param name="operationDescription"></param>
        public void Validate(OperationDescription operationDescription)
        { }

        /// <summary>
        /// Returns a new LoggingInvoker instance.
        /// </summary>
        /// <param name="oldInvoker"></param>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        /// <returns></returns>
        protected LoggingInvoker CreateInvoker(IOperationInvoker oldInvoker,
            OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            return new LoggingInvoker(oldInvoker, operationDescription, dispatchOperation, this);
        }
    }
}
