using V6Soft.Accounting.Membership.Extensions;
using V6Soft.Accounting.Membership.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Core;
using V6Soft.Models.Core.Membership.Dto;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Membership.Dealers
{
    /// <summary>
    ///     Provides NhanVien-related operations (nhanvien CRUD, nhanvien group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectUserDataDealer : IUserDataDealer
    {
        private ILogger m_Logger;
        private readonly IUserDataFarmer m_UserFarmer;

        public DirectUserDataDealer(ILogger logger, IUserDataFarmer userFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(userFarmer, "userFarmer");

            m_Logger = logger;
            m_UserFarmer = userFarmer;
        }

        /// <summary>
        ///     See <see cref="IUserDataDealer.GetUsers()"/>
        /// </summary>
        public PagedSearchResult<UserListItem> GetUsers(SearchCriteria criteria)
        {
            PagedSearchResult<UserListItem> allUsers =
                m_UserFarmer.GetUsers(criteria).ToUserViewModel();
            return allUsers;
        }
        
        public User Authenticate(string username, string password)
        {
            return m_UserFarmer.Authenticate(username, password);
        }

        public bool AddUser(User user)
        {
            return m_UserFarmer.AddUser(user);
        }

        public bool DeleteUser(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
