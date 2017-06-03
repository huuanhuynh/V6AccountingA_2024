using System.Collections.Generic;

using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Price.Dealers
{
    public class GiaVonDataDealer : IGiaVonDataDealer
    {
        private readonly IGiaVonDataFarmer m_DataFarmer;

        public GiaVonDataDealer(IGiaVonDataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
        
        public PagedSearchResult<Gia_Von> GetGiaVons(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGiaVons(criteria);
        }
    }
}
