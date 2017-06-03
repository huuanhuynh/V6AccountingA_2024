
namespace V6Soft.Common.Logging
{
    /// <summary>
    /// LoggingConstants contain constant values used for logging purposes.
    /// </summary>
    public static class LoggingConstants
    {
        /// <summary>
        /// Event id for trace log entries.
        /// </summary>
        public const int EventIdTrace = 80;
        /// <summary>
        /// Event id for info log entries.
        /// </summary>
        public const int EventIdInfo = 90;
        /// <summary>
        /// Event id for warning log entries.
        /// </summary>
        public const int EventIdWarning = 95;
        /// <summary>
        /// Event id for exception log entries.
        /// </summary>
        public const int EventIdException = 100;

        /// <summary>
        /// Priority for trace log entries.
        /// </summary>
        public const int PriorityTrace = 80;
        /// <summary>
        /// Priority for info log entries.
        /// </summary>
        public const int PriorityInfo = 90;
        /// <summary>
        /// Priority for warning log entries.
        /// </summary>
        public const int PriorityWarning = 95;
        /// <summary>
        /// Priority for exception log entries.
        /// </summary>
        public const int PriorityException = 100;

        /// <summary>
        /// Category for info log entries.
        /// </summary>
        public const string CategoryInfo = "Info";
        /// <summary>
        /// Category for info log entries.
        /// </summary>
        public const string CategoryTrace = "Trace";
        /// <summary>
        /// Category for warning log entries.
        /// </summary>
        public const string CategoryWarning = "Warning";
        /// <summary>
        /// Category for exception log entries.
        /// </summary>
        public const string CategoryException = "Exception";
        /// <summary>   The automapper exception constant. </summary>
        public const string AutomapperException = "AutoMapper.AutoMapperMappingException";

    }
}
