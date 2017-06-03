using System;

using V6Soft.Models.Core;


namespace V6Soft.Models.Core
{
    public class AddResult : OperationResult
    {
        /// <summary>
        ///     Gets or sets generated UID after adding a model.
        /// </summary>
        public Guid UID { get; set; }
    }
}
