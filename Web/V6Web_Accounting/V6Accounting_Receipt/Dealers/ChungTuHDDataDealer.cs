using V6Soft.Accounting.Receipt.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public class ChungTuHDDataDealer : IChungTuHDDataDealer
    {
        private readonly IChungTuHDDataFarmer m_DataFarmer;
        
        public ChungTuHDDataDealer(IChungTuHDDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }

        public PagedSearchResult<ChiTietHopDong> GetChungTuHDs(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchChungTuHDs(criteria);
        }
    }
}
