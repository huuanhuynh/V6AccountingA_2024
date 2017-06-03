namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Altk0
    {
        [Key]
        [StringLength(16)]
        public string tk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tk { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_tk2 { get; set; }

        [Required]
        [StringLength(16)]
        public string ten_ngan { get; set; }

        [Required]
        [StringLength(16)]
        public string ten_ngan2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        public byte loai_tk { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_me { get; set; }

        public byte bac_tk { get; set; }

        public byte tk_sc { get; set; }

        public byte tk_cn { get; set; }

        [Required]
        [StringLength(4)]
        public string nh_tk0 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_tk2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal loai_cl_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal loai_cl_co { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }
    }
}
