namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ADThue43
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string form { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal stt { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string stt1 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_so0 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_so { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string loai_ct { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(254)]
        public string chi_tieu { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string chi_tieu2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string type { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(10)]
        public string dbf { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(48)]
        public string tk_no { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(48)]
        public string tk_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ds { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ds_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal bold { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal auto { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(1)]
        public string tag { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(3)]
        public string ma_so01 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(3)]
        public string ma_so02 { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }
    }

}
