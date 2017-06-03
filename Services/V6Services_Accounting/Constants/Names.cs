
namespace V6Soft.Services.Accounting.Constants
{
    /// <summary>
    ///     Stored procedure names.
    /// </summary>
    public static class StoreProcedures
    {
        /// <summary>
        ///     Gets all menu items.
        /// </summary>
        public const string GetMenuItems = "v6soft.GetMenuItems";

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Params:
        ///     <para/>`OID` [int]: OID of the menu item to get children.
        /// </summary>
        public const string GetMenuChildren = "v6soft.GetMenuChildren";
    }
}
