namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agltc3
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
        [StringLength(8)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_du { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_dau { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_cuoi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_dau_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_cuoi_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }

}
