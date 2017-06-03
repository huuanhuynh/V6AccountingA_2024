using System.Collections.Generic;

using V6Soft.Accounting.Common;
using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Price.Dealers
{
    public class Gia200DataDealer : IGia200DataDealer
    {
        private readonly IGia200DataFarmer m_DataFarmer;
        
        public Gia200DataDealer(IGia200DataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
        
        public PagedSearchResult<Gia200> GetGia200s(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGia200s(criteria);
        }
    }
}
