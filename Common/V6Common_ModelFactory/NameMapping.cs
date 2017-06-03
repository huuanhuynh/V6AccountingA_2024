
namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents a field name or model name mapping.
    /// </summary>
    public class NameMapping
    {
        /// <summary>
        ///     Gets or internally sets name in database.
        /// </summary>
        public string DbName { get; internal set; }

        /// <summary>
        ///     Gets or internally sets name used in application.
        /// </summary>
        public string AppName { get; internal set; }


        public NameMapping()
        {
        }
        
        public NameMapping(string dbName, string appName)
        {
            DbName = dbName;
            AppName = appName;
        }
    }
}
