namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agltc1
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
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ts_nv { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal cong_no { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ngoai_bang { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string chi_tieu2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }
}
