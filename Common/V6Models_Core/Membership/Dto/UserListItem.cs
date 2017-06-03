using System;

namespace V6Soft.Models.Core.Membership.Dto
{
    public class UserListItem : V6Model
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public Guid Uid { get; set; }
    }
}
