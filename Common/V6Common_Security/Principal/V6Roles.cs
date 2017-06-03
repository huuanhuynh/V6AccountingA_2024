
namespace V6Soft.Common.Security.Principal
{
    /// <summary>
    /// The class contains constants for roles that a user can have in MTP.
    /// </summary>
    public static class V6Roles
    {
        /// <summary>
        /// Administrator role.
        /// </summary>
        public const string MtpRoleAdministrators = "Administrators";
        /// <summary>
        /// Service role (all services have this role).
        /// </summary>
        public const string V6RoleService = "V6Service";
        /// <summary>
        /// Service controller roles (all service controllers have this role).
        /// </summary>
        public const string V6RoleServiceController = "V6ServiceController";
        /// <summary>
        /// Manager role.
        /// </summary>
        public const string V6RoleManagers = "Managers";
        /// <summary>
        /// CustomerService role
        /// </summary>
        public const string V6RoleCustomerService = "CustomerService";
        /// <summary>
        /// BackOffice role
        /// </summary>
        public const string V6RoleBackOffice = "BackOffice";
    }
}
