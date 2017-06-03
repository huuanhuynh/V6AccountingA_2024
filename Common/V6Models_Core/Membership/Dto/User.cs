namespace V6Soft.Models.Core.Membership.Dto
{
    public class User : V6Model
    {
        public string ModuleId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPre { get; set; }
        public string PlainPassword { get; set; }
        public string Comment { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsVAdmin { get; set; }
        public string Rights { get; set; }
        public decimal IsDelete { get; set; }
        public string DeletionRights { get; set; }
        public string ModificationRights { get; set; }
        public string AdditionRights { get; set; }
        public decimal? UserId2 { get; set; }
        public byte UserInv { get; set; }
        public byte Pagedefa { get; set; }
        public string WarehouseRights { get; set; }
        public string DvcsRights { get; set; }
        public string UserOther { get; set; }
        public decimal InheritId { get; set; }
        public string InheritCh { get; set; }
        public string CheckSync { get; set; }
        public string Level { get; set; }
        public int UserAcc { get; set; }
    }
}
