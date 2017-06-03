namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alcc")]
    public partial class Alcc
    {
        [Key]
        [StringLength(8)]
        public string so_the_cc { get; set; }

        [StringLength(48)]
        public string ten_cc { get; set; }

        [StringLength(8)]
        public string so_hieu_cc { get; set; }

        [StringLength(48)]
        public string ten_cc2 { get; set; }

        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(16)]
        public string nuoc_sx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nam_sx { get; set; }

        [StringLength(8)]
        public string nh_cc1 { get; set; }

        [StringLength(8)]
        public string nh_cc2 { get; set; }

        [StringLength(8)]
        public string nh_cc3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tinh_pb { get; set; }

        [StringLength(8)]
        public string ma_tg_cc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_mua { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_pb0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_ky_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_pb1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kieu_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_le_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tong_sl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? loai_pb { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_cc { get; set; }

        [StringLength(16)]
        public string tk_pb { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [StringLength(24)]
        public string cong_suat { get; set; }

        [StringLength(8)]
        public string loai_cc { get; set; }

        [StringLength(48)]
        public string ts_kt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_dc { get; set; }

        [StringLength(48)]
        public string ly_do_dc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_giam { get; set; }

        [StringLength(2)]
        public string ma_giam_cc { get; set; }

        [StringLength(48)]
        public string ly_do_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [StringLength(128)]
        public string ghi_chu { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_dvsd { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

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

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

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

        [StringLength(8)]
        public string ma_qg { get; set; }

        [Required]
        [StringLength(2)]
        public string loai_cc0 { get; set; }

        [Required]
        [StringLength(2)]
        public string thuoc_nhom { get; set; }

        [Required]
        [StringLength(1)]
        public string trang_thai { get; set; }
    }

}
