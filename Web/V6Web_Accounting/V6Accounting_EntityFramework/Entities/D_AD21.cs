namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class D_AD21
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(16)]
        public string tk_dt { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien2 { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ck_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [StringLength(8)]
        public string ma_thue_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suati { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(16)]
        public string ma_kh2_i { get; set; }

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

        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal Tien { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string Ma_kh_i { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(13)]
        public string STT_REC_TT { get; set; }
    }
}
