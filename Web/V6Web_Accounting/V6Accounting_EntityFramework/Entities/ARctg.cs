namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARctg
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Key]
        [Column(Order = 3)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal no_co { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal tien { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime ngay_ct1 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime ngay_ct2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string TK_DU { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(11)]
        public string KHOA_CTGS { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(12)]
        public string LOAI_CTGS { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(1)]
        public string KIEU_CTGS { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(1)]
        public string NHOM_CTGS { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(12)]
        public string SO_LO0 { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(8)]
        public string MA_DVCS { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal THANG { get; set; }
    }
}
