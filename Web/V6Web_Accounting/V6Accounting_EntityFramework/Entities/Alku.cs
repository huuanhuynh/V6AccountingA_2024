namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alku")]
    public partial class Alku
    {
        [Key]
        [StringLength(16)]
        public string ma_ku { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ku { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ku2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ku1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ku2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal lai_suat1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal lai_suat2 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_ku1 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_ku2 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_ku3 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(20)]
        public string so_ku { get; set; }

        [StringLength(16)]
        public string ma_kh { get; set; }

        [StringLength(64)]
        public string Tk_kc { get; set; }
    }
}
