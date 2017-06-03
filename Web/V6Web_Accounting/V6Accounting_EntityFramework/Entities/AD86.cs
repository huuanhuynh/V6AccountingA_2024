namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD86
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
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

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

        [Key]
        [Column(Order = 19)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal Pt_cki { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal Ck_nt { get; set; }

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal Ck { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal Gg_nt { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal Gg { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal Tien1_Nt { get; set; }

        [Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal Tien1 { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM86 AM86 { get; set; }
    }
}
