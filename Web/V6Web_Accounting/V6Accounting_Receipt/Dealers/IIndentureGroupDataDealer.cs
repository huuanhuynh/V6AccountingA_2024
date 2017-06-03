using V6Soft.Models.Accounting.ViewModels.IndentureGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.IndentureGroup.Dealers
{
    /// <summary>
    ///     Acts as a service client to get indentureGroup data from IndentureGroupService.
    /// </summary>
    public interface IIndentureGroupDataDealer
    {
        /// <summary>
        ///     Gets list of indentureGroups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<IndentureGroupListItem> GetIndentureGroups(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new indentureGroup.
        /// </summary>
        bool AddIndentureGroup(AccModels.IndentureGroup indentureGroup);
        /// <summary>
        ///     Delete a indentureGroup.
        /// </summary>
        bool DeleteIndentureGroup(string key);
        /// <summary>
        ///     Update data for a indentureGroup.
        /// </summary>
        bool UpdateIndentureGroup(AccModels.IndentureGroup indentureGroup);
    }
}
