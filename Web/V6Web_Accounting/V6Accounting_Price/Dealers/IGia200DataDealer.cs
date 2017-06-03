using System.Collections.Generic;

using V6Soft.Models.Accounting;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Price.Dealers
{
    public interface IGia200DataDealer
    {
        PagedSearchResult<Gia200> GetGia200s(SearchCriteria criteria);
    }
}
