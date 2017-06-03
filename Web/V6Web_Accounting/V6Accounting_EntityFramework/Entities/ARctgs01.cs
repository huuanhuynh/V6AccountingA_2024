namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARctgs01
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
        public decimal STT { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(12)]
        public string phandau { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(12)]
        public string phancuoi { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(12)]
        public string dinhdang { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string phanthang { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(1)]
        public string tag { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(1)]
        public string nhom_user { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(100)]
        public string TK_DU0 { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "smalldatetime")]
        public DateTime ngay_ct1 { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "smalldatetime")]
        public DateTime ngay_ct2 { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(100)]
        public string TK_DU { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(11)]
        public string KHOA_CTGS { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(12)]
        public string LOAI_CTGS { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(1)]
        public string KIEU_CTGS { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(1)]
        public string NHOM_CTGS { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(12)]
        public string SO_LO0 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(8)]
        public string MA_DVCS { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal THANG { get; set; }
    }
}
