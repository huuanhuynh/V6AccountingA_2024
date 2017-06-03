using System.Collections.Generic;
using V6Soft.Accounting.Common;
using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Price.Dealers
{
    public class GiaDataDealer : IGiaDataDealer
    {
        private readonly IGiaDataFarmer m_DataFarmer;
        
        public GiaDataDealer(IGiaDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
        
        public PagedSearchResult<Gia> GetGias(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGias(criteria);
        }
    }
}
