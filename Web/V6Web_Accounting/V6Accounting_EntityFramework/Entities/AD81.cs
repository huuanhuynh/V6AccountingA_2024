namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD81
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal gia { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tien { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        [Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal ck { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string tk_dt { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string px_gia_ddi { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(1)]
        public string tang { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt21 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia21 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_ban_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_ban { get; set; }

        [StringLength(16)]
        public string tk_cki { get; set; }

        [Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        [Key]
        [Column(Order = 31, TypeName = "numeric")]
        public decimal TIEN1 { get; set; }

        [Key]
        [Column(Order = 32, TypeName = "numeric")]
        public decimal TIEN1_NT { get; set; }

        [Key]
        [Column(Order = 33)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Key]
        [Column(Order = 34, TypeName = "numeric")]
        public decimal gg_nt { get; set; }

        [Key]
        [Column(Order = 35, TypeName = "numeric")]
        public decimal gg { get; set; }

        [StringLength(8)]
        public string Ma_gia { get; set; }

        [Key]
        [Column(Order = 36, TypeName = "numeric")]
        public decimal GIA_NT4 { get; set; }

        [Key]
        [Column(Order = 37, TypeName = "numeric")]
        public decimal GIA4 { get; set; }

        [Key]
        [Column(Order = 38, TypeName = "numeric")]
        public decimal TIEN_NT4 { get; set; }

        [Key]
        [Column(Order = 39, TypeName = "numeric")]
        public decimal TIEN4 { get; set; }

        [Key]
        [Column(Order = 40)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [Key]
        [Column(Order = 41)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [Column(Order = 42)]
        public string so_image { get; set; }

        [Key]
        [Column(Order = 43)]
        [StringLength(1)]
        public string Status_DPI { get; set; }

        [Key]
        [Column(Order = 44)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM81 AM81 { get; set; }
    }
}
