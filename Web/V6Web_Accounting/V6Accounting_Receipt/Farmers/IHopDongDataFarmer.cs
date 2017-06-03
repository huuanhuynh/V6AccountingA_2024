using V6Soft.Accounting.Common;
using V6Soft.Accounting.Common.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Receipt.Farmers
{
    public interface IHopDongDataFarmer : IGenericDataFarmer<Albp>
    {
        PagedSearchResult<HopDong> SearchHopDongs(SearchCriteria criteria);
    }
}
