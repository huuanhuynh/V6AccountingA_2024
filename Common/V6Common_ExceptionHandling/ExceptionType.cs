
namespace V6Soft.Common.ExceptionHandling
{
    /// <summary>
    /// Type of exception that occured.
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// Technical exception, e.g. database connection problems, file permissions.
        /// </summary>
        Technical = 0,
        /// <summary>
        /// Business exception that occured when business rules are violated, rule data is missing and so forth.
        /// </summary>
        Business = 1
    }
}
