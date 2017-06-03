using System.Collections.Generic;

namespace V6Soft.Models.Core.ViewModels
{
    public class PagedSearchResult<T>
    {
        public IList<T> Data { get; set; }
        public int Total { get; set; }

        public PagedSearchResult(IList<T> data, int total)
        {
            Data = data;
            Total = total;
        }
    }
}
