namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alvc")]
    public partial class Alvc
    {
        [Key]
        [StringLength(8)]
        public string ma_vc { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_vc { get; set; }

        [StringLength(48)]
        public string ten_vc2 { get; set; }

        [StringLength(8)]
        public string loai_vc { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? height { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? length { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? volume { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? width { get; set; }

        [StringLength(10)]
        public string dvtheight { get; set; }

        [StringLength(10)]
        public string dvtlength { get; set; }

        [StringLength(10)]
        public string dvtvolume { get; set; }

        [StringLength(10)]
        public string dvtweight { get; set; }

        [StringLength(10)]
        public string dvtwidth { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tg_xephang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tg_dohang { get; set; }

        [StringLength(10)]
        public string dvt_xep { get; set; }

        [StringLength(10)]
        public string dvt_do { get; set; }

        [StringLength(32)]
        public string bien_so { get; set; }

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

        [Required]
        [StringLength(1)]
        public string status { get; set; }

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
    }

}
