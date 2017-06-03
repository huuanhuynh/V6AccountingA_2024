
namespace V6Soft.Services.Common.Constants
{
    /// <summary>
    ///     Error code to be returned to client when a WCF operaion fails.
    ///     <para/>1xxx: Input errors (failed validation, null, empty...)
    /// </summary>
    public enum ErrorCode : uint
    {
        /// <summary>
        ///     General errors or the unknown reason.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Failed to validate model values.
        /// </summary>
        ConstraintViolation = 1001,

        /// <summary>
        ///     Parameters to service operation are invalid (null, empty...)
        /// </summary>
        InvalidParameter = 1002,

        /// <summary>
        ///     Failed to parse or convert data contracts to service models.
        /// </summary>
        InvalidDataContract = 1003,
    }
}
