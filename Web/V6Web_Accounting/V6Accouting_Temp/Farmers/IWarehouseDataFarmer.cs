using System.Collections.Generic;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Temp1.Farmers
{
    public interface ITemp1DataFarmer : IGenericRepository<Alkho>
    {
        PagedSearchResult<Temp1> SearchTemp1s(SearchCriteria criteria);
    }
}
