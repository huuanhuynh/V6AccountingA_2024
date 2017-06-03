using System.Collections.Generic;

using V6Soft.Common.Utils.Linq;


namespace V6Soft.Models.Core
{
    public class SearchCriteria
    {
        public string SearchText { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public FilterDescriptor Filter { get; set; }
        public string FilterLogic { get; set; }
        public IList<SortDescriptor> Sort { get; set; }

        public SearchCriteria()
        {
            Page = 1;
        }

        public SearchCriteria(int pageSize)
        {
            Page = 1;
            PageSize = pageSize;
        }
    }
}
