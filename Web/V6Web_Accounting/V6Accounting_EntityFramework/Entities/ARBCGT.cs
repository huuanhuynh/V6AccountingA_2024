namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ARBCGT")]
    public partial class ARBCGT
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal thang { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal bold { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_so { get; set; }

        [StringLength(100)]
        public string chi_tieu { get; set; }

        [StringLength(100)]
        public string chi_tieu2 { get; set; }

        [StringLength(254)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string tk_no { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string tk_co { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal giam_tru { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ky_truoc { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ky_nay { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal luy_ke { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal ky_truocnt { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal ky_nay_nt { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal luy_ke_nt { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_ytcp { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal sl_tp_nk { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tl_ht { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(8)]
        public string ma_bpht { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal sl_dd_ck { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal dd_dk { get; set; }

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal dd_ck { get; set; }
    }
}
