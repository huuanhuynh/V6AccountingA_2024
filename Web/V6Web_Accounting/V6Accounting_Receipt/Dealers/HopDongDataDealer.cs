using V6Soft.Accounting.Receipt.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public class HopDongDataDealer : IHopDongDataDealer
    {
        private readonly IHopDongDataFarmer m_DataFarmer;
        
        public HopDongDataDealer(IHopDongDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
                
        public PagedSearchResult<HopDong> GetHopDongs(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchHopDongs(criteria);
        }
    }
}
