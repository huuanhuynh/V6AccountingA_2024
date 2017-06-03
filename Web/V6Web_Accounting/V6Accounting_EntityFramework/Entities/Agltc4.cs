namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agltc4
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string loai_ct { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        [StringLength(48)]
        public string tk_no { get; set; }

        [StringLength(48)]
        public string tk_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }
}
