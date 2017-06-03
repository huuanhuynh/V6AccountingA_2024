using System.Collections.Generic;

using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Price.Dealers
{
    public class GiaVVDataDealer : IGiaVVDataDealer
    {
        private readonly IGiaVVDataFarmer m_DataFarmer;

        public GiaVVDataDealer(IGiaVVDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }

        public PagedSearchResult<GiaVV> GetGiaVVs(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGiaVVs(criteria);
        }
    }
}
