using System.Collections.Generic;


namespace V6Soft.Models.Core
{
    public class OperationResult
    {
        /// <summary>
        ///     Gets or sets the value indicating the operation was
        ///     successful or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Gets or sets list of error codes explaining
        ///     why the operation fails.
        /// </summary>
        public IList<ulong> ErrorCodes { get; set; }
    }
}
