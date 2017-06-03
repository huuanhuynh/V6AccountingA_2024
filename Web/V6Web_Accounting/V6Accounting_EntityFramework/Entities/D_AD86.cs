namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_AD86
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong1 { get; set; }

        [StringLength(16)]
        public string tk_vt { get; set; }

        [StringLength(8)]
        public string ma_kho_i { get; set; }

        [StringLength(16)]
        public string ma_nx_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

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

        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

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

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Pt_cki { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Ck_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien1_Nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien1 { get; set; }

        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [StringLength(16)]
        public string SO_LSX { get; set; }
    }
}
