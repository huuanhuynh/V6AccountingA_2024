using System.Collections.Generic;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Temp1.Dealers
{
    public interface ITemp1DataDealer
    {
        PagedSearchResult<Temp1> GetTemp1s(SearchCriteria criteria);
    }
}
