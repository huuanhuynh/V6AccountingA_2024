using V6Soft.Models.Accounting.ViewModels.DonViCoSo;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.BranchUnit.Dealers
{
    /// <summary>
    ///     Acts as a service client to get branchUnit data from BranchUnitService.
    /// </summary>
    public interface IBranchUnitDataDealer
    {
        /// <summary>
        ///     Gets list of branchUnits satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<BranchUnitListItem> GetBranchUnits(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new branchUnit.
        /// </summary>
        bool AddBranchUnit(AccModels.BranchUnit branchUnit);
        /// <summary>
        ///     Delete a branchUnit.
        /// </summary>
        bool DeleteBranchUnit(string key);
        /// <summary>
        ///     Update data for a branchUnit.
        /// </summary>
        bool UpdateBranchUnit(AccModels.BranchUnit branchUnit);
    }
}
