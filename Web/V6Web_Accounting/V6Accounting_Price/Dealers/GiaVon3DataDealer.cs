using System.Collections.Generic;

using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Price.Dealers
{
    public class GiaVon3DataDealer : IGiaVon3DataDealer
    {
        private readonly IGiaVon3DataFarmer m_DataFarmer;

        public GiaVon3DataDealer(IGiaVon3DataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
        
        public PagedSearchResult<GiaVon3> GetGiaVon3s(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGiaVon3s(criteria);
        }
    }
}
