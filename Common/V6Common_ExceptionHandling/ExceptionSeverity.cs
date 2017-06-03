
namespace V6Soft.Common.ExceptionHandling
{
    /// <summary>
    /// Types of exception severities.
    /// </summary>
    public enum ExceptionSeverity
    {
        /// <summary>
        /// Normal level of exception, expected and handled by the code in a graceful manner; either by
        /// resolving or throwing specific exceptions.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Critical level of exception, not expected, further processing stopped or hindered.
        /// </summary>
        Critical = 1
    }
}
