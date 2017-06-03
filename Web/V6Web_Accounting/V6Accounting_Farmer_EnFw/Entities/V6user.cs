using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    [Table("V6user")]
    public partial class V6User
    {
        [StringLength(1)]
        [Column("Module_id")]
        public string ModuleId { get; set; }
        [Key]
        [Column("user_id")]
        public decimal UserId { get; set; }
        
        [Column("user_name")]
        public string UserName { get; set; }

        [Required]
        [Column("user_pre")]
        public string UserPre { get; set; }

        [Required]
        [Column("password")]
        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [StringLength(48)]
        [Column("comment")]
        public string Comment { get; set; }

        [Column("is_admin")]
        public bool IsAdmin { get; set; }

        [Column("is_Vadmin")]
        public bool IsVAdmin { get; set; }

        [Required]
        [Column("rights")]
        public string Rights { get; set; }

        [Column("del_yn",TypeName = "numeric")]
        public decimal IsDelete { get; set; }

        [Column("r_del")]
        public string DeletionRights { get; set; }

        [Column("r_edit")]
        public string ModificationRights { get; set; }

        [Column("r_add")]
        public string AdditionRights { get; set; }

        [Column("user_id2", TypeName = "numeric")]
        public decimal? UserId2 { get; set; }

        [Column("user_acc")]
        public byte UserAcc { get; set; }

        [Column("user_inv")]
        public byte UserInv { get; set; }

        public byte Pagedefa { get; set; }

        [Column("r_kho")]
        [Required]
        public string WarehouseRights { get; set; }

        [Column("r_dvcs")]
        [Required]
        public string DvcsRights { get; set; }

        [Required]
        [Column("USER_OTHER")]
        public string UserOther { get; set; }

        [Column("Inherit_id", TypeName = "numeric")]
        public decimal InheritId { get; set; }

        [Required]
        [Column("inherit_ch")]
        public string InheritCh { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CheckSync { get; set; }

        [Required]
        [Column("LEVEL")]
        public string Level { get; set; }

        [StringLength(100)]
        [Column("web_password")]
        public string WebPassword { get; set; }

        public Guid? UID { get; set; }
    }
}
