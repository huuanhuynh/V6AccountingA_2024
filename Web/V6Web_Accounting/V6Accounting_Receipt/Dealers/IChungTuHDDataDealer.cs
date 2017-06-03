using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public interface IChungTuHDDataDealer
    {
        PagedSearchResult<ChiTietHopDong> GetChungTuHDs(SearchCriteria criteria);
    }
}
