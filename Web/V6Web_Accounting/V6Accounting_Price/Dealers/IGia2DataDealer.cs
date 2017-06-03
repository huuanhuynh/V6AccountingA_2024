using System.Collections.Generic;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Price.Dealers
{
    public interface IGia2DataDealer
    {
        PagedSearchResult<Gia_2> GetGia2s(SearchCriteria criteria);
    }
}
