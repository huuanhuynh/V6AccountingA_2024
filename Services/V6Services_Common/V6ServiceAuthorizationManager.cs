using System.ServiceModel;



namespace V6Soft.Services.Common
{
    /// <summary>
    /// Service Authorization Manager
    /// </summary>
    internal class V6ServiceAuthorizationManager : ServiceAuthorizationManager
    {
        private const string MexRequest = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get";

        /// <summary>
        /// Checks authorization for the given operation context based on default policy evaluation.
        /// </summary>
        /// <param name="operationContext">The System.ServiceModel.OperationContext for the current authorization request.</param>
        /// <returns>true if access is granted; otherwise, false. The default is true.</returns>
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            // Check if the message request is a metadata request.
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;
            if (action != null && action.Equals(MexRequest))
            {
                return true;
            }
            return base.CheckAccessCore(operationContext);
        }
    }
}
