
namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents a field mapping entry in database.
    /// </summary>
    public class FieldMapping
    {
        /// <summary>
        ///     Gets or internally sets name in database.
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        ///     Gets or internally sets name used in application.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///     Gets or sets label key, which is used to look up a
        ///     localized label string.
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        ///     Gets or sets group name.
        /// </summary>
        public string Group { get; set; }


        public FieldMapping()
        {
        }

        public FieldMapping(string dbName, string appName, string label, string group)
        {
            DbName = dbName;
            AppName = appName;
            Label = label;
            Group = group;
        }
    }
}
