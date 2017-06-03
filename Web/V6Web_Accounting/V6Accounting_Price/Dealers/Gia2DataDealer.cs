using System.Collections.Generic;

using V6Soft.Accounting.Price.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Price.Dealers
{
    public class Gia2DataDealer : IGia2DataDealer
    {
        private readonly IGia2DataFarmer m_DataFarmer;


        public Gia2DataDealer(IGia2DataFarmer dataFarmer)
        {
            m_DataFarmer = dataFarmer;
        }
        

        public PagedSearchResult<Gia_2> GetGia2s(SearchCriteria criteria)
        {
            return m_DataFarmer.SearchGia2s(criteria);
        }
    }
}
