namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Allo")]
    public partial class Allo
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(48)]
        public string ten_lo { get; set; }

        [StringLength(48)]
        public string ten_lo2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_nhap { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_sx { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_bdsd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_kt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hhsd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hhbh { get; set; }

        [StringLength(16)]
        public string ma_vt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sl_nhap { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sl_xuat { get; set; }

        [StringLength(128)]
        public string ghi_chu { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [StringLength(20)]
        public string SO_LOSX { get; set; }

        [StringLength(20)]
        public string SO_LODK { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_kh { get; set; }
    }

}
