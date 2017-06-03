using System.Linq;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core.Membership.Dto;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Membership.Extensions
{
    public static class ModelConverter
    {
        public static PagedSearchResult<UserListItem> ToUserViewModel(this PagedSearchResult<User> source)
        {
            var userItem = source.Data.Select(
                x => new UserListItem
                    {
                        Uid = x.UID,
                        UserId = x.UserId,
                        Username = x.UserName,
                        IsAdmin = x.IsAdmin
                    }
            );
            return new PagedSearchResult<UserListItem>(userItem.ToList(), source.Total);
        }
    }
}
