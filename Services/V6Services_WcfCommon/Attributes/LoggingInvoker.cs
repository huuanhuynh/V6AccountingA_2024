using System;
using System.Data.SqlClient;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

using V6Soft.Services.Common.Infrastructure;


namespace V6Soft.Services.Wcf.Common.Attributes
{
    /// <summary>
    /// Flag indicating when to write logs.
    /// </summary>
    [Flags]
    public enum ParameterLogMode
    {
        /// <summary>
        /// No logging
        /// </summary>
        None = 0,
        /// <summary>
        /// Log on error
        /// </summary>
        OnError = 1,
        /// <summary>
        /// Log on entry
        /// </summary>
        OnEntry = 2
    }

    /// <summary>
    /// Implements System.ServiceModel.Dispatcher.IOperationInvoker
    /// to serve logging purposes.
    /// </summary>
    public class LoggingInvoker : IOperationInvoker
    {
        private static string GetArgumentString(object[] args, 
            Predicate<string> isStringifiable, int recursiveDepth = 2)
        {
            var builder = new StringBuilder();
            builder.Append("\tArguments: ");
            switch (args.Length)
            {
                case 0:
                    builder.Append("()");
                    break;
                default:
                    builder.Append("(").Append(args[0].Stringify(recursiveDepth, 
                        isLoggable: isStringifiable));
                    for (int index = 1; index < args.Length; index++)
                    {
                        builder.Append(", " + args[index].Stringify(recursiveDepth, 
                            isLoggable: isStringifiable));
                    }
                    builder.Append(")");
                    break;
            }

            return builder.ToString();
        }

        private static bool IsLoggable(string name, string[] referencedNames)
        {
            if (referencedNames == null || referencedNames.Length == 0) { return true; }

            foreach (string referenceName in referencedNames)
            {
                if (name.Equals(referenceName)) { return true; }
            }
            return false;
        }

        private readonly IOperationInvoker m_OldInvoker;

        private ParameterLogMode m_ParameterLogMode;
        private string[] m_LoggableParameters;
        private int m_RecursiveDepth;

        /// <summary>
        /// Returns a new LoggingInvoker instance.
        /// </summary>
        /// <param name="oldInvoker"></param>
        /// <param name="operationDescription"></param>
        /// <param name="dispatchOperation"></param>
        /// <param name="referencedAttribute"></param>
        public LoggingInvoker(IOperationInvoker oldInvoker, 
            OperationDescription operationDescription = null, 
            DispatchOperation dispatchOperation = null, 
            OperationLoggingAttribute referencedAttribute = null)
        {
            m_OldInvoker = oldInvoker;
            this.OperationDescription = operationDescription;
            this.DispatchOperation = dispatchOperation;
            this.ReferencedAttribute = referencedAttribute;
            if (referencedAttribute == null)
            {
                m_RecursiveDepth = 2;
                m_ParameterLogMode = ParameterLogMode.OnError;
                m_LoggableParameters = null;
            }
            else
            {
                m_RecursiveDepth = referencedAttribute.RecursiveDepth;
                m_ParameterLogMode = referencedAttribute.ParameterLogMode;
                m_LoggableParameters = referencedAttribute.LoggableParameters;
            }
        }

        private string InvocationId { get; set; }

        private string MethodArguments { get; set; }

        private OperationDescription OperationDescription { get; set; }

        private DispatchOperation DispatchOperation { get; set; }

        private OperationLoggingAttribute ReferencedAttribute { get; set; }

        /// <summary>
        /// Returns an array of argument objects
        /// </summary>
        /// <returns></returns>
        public virtual object[] AllocateInputs()
        {
            return m_OldInvoker.AllocateInputs();
        }

        /// <summary>
        /// Invokes the service method against the specified instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inputs"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            PreInvoke(instance, inputs);

            object returnedValue = null;
            object[] outputParams = new object[] { };
            Exception exception = null;
            try
            {
                returnedValue = m_OldInvoker.Invoke(instance, inputs, out outputParams);
                outputs = outputParams;
                return returnedValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw ProcessException(ex);
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, exception);
            }
        }

        /// <summary>
        /// Begins to invoke the service method against the specified instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inputs"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult InvokeBegin(object instance, object[] inputs, 
            AsyncCallback callback, object state)
        {
            PreInvoke(instance, inputs);
            return m_OldInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        /// <summary>
        /// Returns the result from the previous asynchronous invocation.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="outputs"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            object returnedValue = null;
            object[] outputParams = { };
            Exception exception = null;

            try
            {
                returnedValue = m_OldInvoker.InvokeEnd(instance, out outputs, result);
                outputs = outputParams;
                return returnedValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw ProcessException(ex);
            }
            finally
            {
                PostInvoke(instance, returnedValue, outputParams, exception);
            }
        }

        /// <summary>
        /// If true, the dispatcher will call Invoke(). Otherwise, the dispatcher will 
        /// call InvokeBegin() and InvokeEnd().
        /// </summary>
        public bool IsSynchronous
        {
            get { return m_OldInvoker.IsSynchronous; }
        }

        private void PreInvoke(object instance, object[] inputs)
        {
            InvocationId = OperationDescription.DeclaringContract.Name + "." 
                + OperationDescription.Name + "(" + Guid.NewGuid().ToString() + ")";
            MethodArguments = GetArgumentString(
                inputs, name => IsLoggable(name, m_LoggableParameters));
            
            if ((m_ParameterLogMode & ParameterLogMode.OnEntry) == ParameterLogMode.OnEntry)
            {
                AppContext.Logger.LogTraceEntry(InvocationId + Environment.NewLine 
                    + MethodArguments);
            }
            else
            {
                AppContext.Logger.LogTraceEntry(InvocationId);
            }
        }

        private Exception ProcessException(Exception ex)
        {
            if ((m_ParameterLogMode & ParameterLogMode.OnError) == ParameterLogMode.OnError)
            {
                AppContext.Logger.LogException(InvocationId + Environment.NewLine 
                    + MethodArguments, ex);
            }
            else
            {
                AppContext.Logger.LogException(InvocationId, ex);
            }

            if (ex is SqlException)
            {
                return new Exception("Error connecting to or accessing database.");
            }
            return ex;
        }

        /// <summary>
        /// Always called, even if operation had an exception
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="returnedValue"></param>
        /// <param name="outputs"></param>
        /// <param name="exception"></param>
        private void PostInvoke(object instance, object returnedValue,
                                           object[] outputs, Exception exception)
        {
            AppContext.Logger.LogTraceExit(InvocationId);
        }
    }

}
