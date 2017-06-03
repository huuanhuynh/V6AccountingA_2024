namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agltc8
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal he_so { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string dvt { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ty_le { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(48)]
        public string ten1 { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(48)]
        public string ten12 { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(3)]
        public string ma_so1 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(2)]
        public string ts_kd1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien1 { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(48)]
        public string ten2 { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(48)]
        public string ten22 { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(3)]
        public string ma_so2 { get; set; }

        [StringLength(2)]
        public string ts_kd2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? iscal { get; set; }
    }

}
