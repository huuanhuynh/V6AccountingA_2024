namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_AD71
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

        [StringLength(8)]
        public string ma_kho_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_nx_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? gia_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_hg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_hg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cp_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

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
        public string pn_gia_tbi { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CK_NT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CK { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PT_CKI { get; set; }

        [StringLength(1)]
        public string Ck_vat_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg { get; set; }

        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [StringLength(20)]
        public string SO_MAY { get; set; }

        [StringLength(128)]
        public string so_image { get; set; }

        [Key]
        [StringLength(16)]
        public string SO_LSX { get; set; }
    }
}
