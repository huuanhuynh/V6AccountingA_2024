namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V6user
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string Module_id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal user_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string user_name { get; set; }

        [Required]
        [StringLength(8)]
        public string user_pre { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(48)]
        public string comment { get; set; }

        public bool is_admin { get; set; }

        public bool is_Vadmin { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string rights { get; set; }

        [Column(TypeName = "numeric")]
        public decimal del_yn { get; set; }

        [Column(TypeName = "text")]
        public string r_del { get; set; }

        [Column(TypeName = "text")]
        public string r_edit { get; set; }

        [Column(TypeName = "text")]
        public string r_add { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        public byte user_acc { get; set; }

        public byte user_inv { get; set; }

        public byte Pagedefa { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string r_kho { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string r_dvcs { get; set; }

        [Required]
        [StringLength(1)]
        public string USER_OTHER { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Inherit_id { get; set; }

        [Required]
        [StringLength(1)]
        public string inherit_ch { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [Required]
        [StringLength(2)]
        public string LEVEL { get; set; }

        [StringLength(100)]
        public string web_password { get; set; }

        public Guid? UID { get; set; }
    }
}
