namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alvv")]
    public partial class Alvv
    {
        [Key]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_vv { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_vv2 { get; set; }

        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vv1 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vv2 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vv3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_vv1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_vv2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "money")]
        public decimal tien_nt { get; set; }

        [Column(TypeName = "money")]
        public decimal tien { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ghi_chu { get; set; }

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

        [StringLength(64)]
        public string tk_kc { get; set; }

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

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }
    }
}
