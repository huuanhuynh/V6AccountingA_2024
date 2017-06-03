
namespace V6Soft.Services.Assistant.Constants 
{
    /// <summary>
    ///     Constant for configuring service
    /// </summary>
    public class ServiceConfig
    {
        /// <summary> 
        ///     Constant for the Service Host faulted
        ///     <para/>Value: The {0} Service Host has faulted.
        /// </summary>
        public const string ServiceHostFaulted = "The {0} Service Host has faulted.";

        /// <summary>
        ///     Name for CustomerService
        ///     <para/>Value: "CustomerService"
        /// </summary>
        public const string AssistantServiceName = "AssistantService";

        /// <summary>
        ///     URL for client to connect to customer service host.
        ///     <para/>Value: "net.tcp://localhost:14228"
        /// </summary>
        public const string AssistantServiceHostUri = @"net.tcp://localhost:14229"; 
    }
}
