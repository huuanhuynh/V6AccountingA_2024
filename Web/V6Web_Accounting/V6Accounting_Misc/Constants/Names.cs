
namespace V6Soft.Accounting.Misc.Constants
{
    /// <summary>
    ///     Stored procedure names.
    /// </summary>
    public static class StoreProcedures
    {
        /// <summary>
        ///     Gets all menu items.
        /// </summary>
        public const string GetMenuItems = "dbo.GetMenuItems";

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Params:
        ///     <para/>`OID` [int]: OID of the menu item to get children.
        /// </summary>
        public const string GetMenuChildren = "dbo.GetMenuChildren";
    }
}
