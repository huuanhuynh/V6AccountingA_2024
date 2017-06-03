using V6Soft.Accounting.Receipt.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public class ChungTuCTDataDealer : IChungTuCTDataDealer
    {
        private readonly IChungTuCTDataFarmer m_DataFarmer;

        public ChungTuCTDataDealer(IChungTuCTDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }

        public PagedSearchResult<ChungTuChiTiet> GetChungTuCTs(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchChungTuCTs(criteria);
        }
    }
}
