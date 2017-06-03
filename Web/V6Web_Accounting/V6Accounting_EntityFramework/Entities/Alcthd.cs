namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alcthd")]
    public partial class Alcthd
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string ma_hd { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }
    }
}
