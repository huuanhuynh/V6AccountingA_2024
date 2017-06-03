using V6Soft.Models.Core;
using V6Soft.Models.Core.Membership.Dto;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Membership.Dealers
{
    /// <summary>
    ///     Acts as a service client to get customer data from NhanVienService.
    /// </summary>
    public interface IUserDataDealer
    {
        /// <summary>
        ///     Gets list of users satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<UserListItem> GetUsers(SearchCriteria criteria);
        /// <summary>
        ///     Authenticate a user.
        /// </summary>
        /// <returns></returns>
        User Authenticate(string username, string password);
        /// <summary>
        ///     Persists data for a new user.
        /// </summary>
        bool AddUser(User user);
        /// <summary>
        ///     Delete a user.
        /// </summary>
        bool DeleteUser(string key);
        /// <summary>
        ///     Update data for a user.
        /// </summary>
        bool UpdateUser(User user);
    }
}
