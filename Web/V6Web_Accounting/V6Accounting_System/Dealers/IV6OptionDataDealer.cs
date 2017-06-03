using V6Soft.Accounting.Common.Dealers;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Accounting.ViewModels.V6Option;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.System.Dealers
{
    /// <summary>
    ///     Acts as a service client to get v6option data from V6OptionService.
    /// </summary>
    public interface IV6OptionDataDealer : IODataFriendly<V6Option>
    {
        /// <summary>
        ///     Gets list of v6options satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<V6OptionListItem> GetV6Options(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new v6option.
        /// </summary>
        bool AddV6Option(Models.Accounting.DTO.V6Option v6Option);
        /// <summary>
        ///     Delete a v6option.
        /// </summary>
        bool DeleteV6Option(string key);
        /// <summary>
        ///     Update data for a v6option.
        /// </summary>
        bool UpdateV6Option(Models.Accounting.DTO.V6Option v6Option);
    }
}
