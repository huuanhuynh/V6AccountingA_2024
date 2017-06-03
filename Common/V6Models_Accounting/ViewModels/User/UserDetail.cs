using System;

namespace V6Soft.Models.Accounting.ViewModels.User
{
    public class UserDetail
    {
        public Guid Uid { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
