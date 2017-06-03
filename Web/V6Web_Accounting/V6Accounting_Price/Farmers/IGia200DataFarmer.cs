using System.Collections.Generic;

using V6Soft.Accounting.Common;
using V6Soft.Accounting.Common.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Price.Farmers
{
    public interface IGia200DataFarmer : IGenericDataFarmer<ALGIA200>
    {
        PagedSearchResult<Gia200> SearchGia200s(SearchCriteria criteria);
    }
}
