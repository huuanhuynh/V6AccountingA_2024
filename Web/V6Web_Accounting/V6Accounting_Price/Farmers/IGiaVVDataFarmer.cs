using System.Collections.Generic;

using V6Soft.Accounting.Common;
using V6Soft.Accounting.Common.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Price.Farmers
{
    public interface IGiaVVDataFarmer : IGenericDataFarmer<ALgiavv>
    {
        PagedSearchResult<GiaVV> SearchGiaVVs(SearchCriteria criteria);
    }
}
