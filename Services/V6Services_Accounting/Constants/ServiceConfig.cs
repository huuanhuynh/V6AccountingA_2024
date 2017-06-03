
namespace V6Soft.Services.Accounting.Constants
{
    /// <summary>
    /// Constant for configuring service
    /// </summary>
    public class ServiceConfig
    {
        /// <summary> 
        /// Constant for the Service Host faulted
        /// <para/>Value: The {0} Service Host has faulted.
        /// </summary>
        public const string ServiceHostFaulted = "The {0} Service Host has faulted.";

        /// <summary>
        ///     Name for CustomerService
        ///     <para/>Value: "CustomerService"
        /// </summary>
        public const string CustomerServiceName = "CustomerService";

        /// <summary>
        ///     URL for client to connect to customer service host.
        ///     <para/>Value: "net.tcp://localhost:14228"
        /// </summary>
        public const string CustomerServiceHostUri = @"net.tcp://localhost:14228";

        /// <summary>
        ///     Name for DefinitionService
        ///     <para/>Value: "DefinitionService"
        /// </summary>
        public const string DefinitionServiceName = "DefinitionService";

        /// <summary>
        ///     URL for client to connect to definition service host.
        ///     <para/>Value: "net.tcp://localhost:14424"
        /// </summary>
        public const string DefinitionServiceHostUri = @"net.tcp://localhost:14424";

        /// <summary>
        ///     Name for MenuService
        ///     <para/>Value: "MenuService"
        /// </summary>
        public const string MenuServiceName = "MenuService";

        /// <summary>
        ///     URL for client to connect to definition service host.
        ///     <para/>Value: "net.tcp://localhost:14616"
        /// </summary>
        public const string MenuServiceHostUri = @"net.tcp://localhost:14616";

        /// <summary>
        ///     Name for PaymentMethodService
        ///     <para/>Value: "PaymentMethodService"
        /// </summary>
        public const string PaymentMethodServiceName = "PaymentMethodService";

        /// <summary>
        ///     URL for client to connect to definition service host.
        ///     <para/>Value: "net.tcp://localhost:14616"
        /// </summary>
        public const string PaymentMethodServiceHostUri = @"net.tcp://localhost:14617";
    }
}
