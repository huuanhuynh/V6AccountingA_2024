using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class V6Account : V6Model
    {
        public Guid Uid { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
