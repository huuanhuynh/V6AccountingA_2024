using System.Collections.Generic;


namespace V6Soft.Models.Core.ViewModels
{
    /// <summary>
    ///     A list with pagination applied.
    /// </summary>
    public class PagedList<T>
    {
        /// <summary>
        ///     Gets or sets list of items after paging.
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        ///     Gets or sets total item before paging.
        /// </summary>
        public ulong Total { get; set; }

        /// <summary>
        ///     Gets or sets current page position.
        /// </summary>
        public ushort PageIndex { get; set; }

        /// <summary>
        ///     Gets or sets number of items in each page.
        /// </summary>
        public ushort PageSize { get; set; }
    }
}
