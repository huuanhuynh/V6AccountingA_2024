namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARI70
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
        [Column(Order = 3)]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte pn_gia_tb { get; set; }

        [Key]
        [Column(Order = 5)]
        public byte px_gia_dd { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(12)]
        public string so_seri { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Key]
        [Column(Order = 11)]
        public byte tk_cn { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string ma_khon { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 15)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_nx { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal sl_nhap1 { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal sl_xuat1 { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal gia_nt1 { get; set; }

        [Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal gia1 { get; set; }

        [Key]
        [Column(Order = 27, TypeName = "numeric")]
        public decimal gia01 { get; set; }

        [Key]
        [Column(Order = 28, TypeName = "numeric")]
        public decimal gia_nt01 { get; set; }

        [Key]
        [Column(Order = 29, TypeName = "numeric")]
        public decimal gia21 { get; set; }

        [Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal gia_nt21 { get; set; }

        [Key]
        [Column(Order = 31)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Key]
        [Column(Order = 32)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        [Key]
        [Column(Order = 33)]
        [StringLength(16)]
        public string tk_dt { get; set; }

        [Key]
        [Column(Order = 34)]
        public byte nxt { get; set; }

        [Key]
        [Column(Order = 35)]
        public byte ct_dc { get; set; }

        [Key]
        [Column(Order = 36, TypeName = "numeric")]
        public decimal sl_nhap { get; set; }

        [Key]
        [Column(Order = 37, TypeName = "numeric")]
        public decimal sl_xuat { get; set; }

        [Key]
        [Column(Order = 38, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        [Key]
        [Column(Order = 39, TypeName = "numeric")]
        public decimal gia { get; set; }

        [Key]
        [Column(Order = 40, TypeName = "numeric")]
        public decimal tien_nt_n { get; set; }

        [Key]
        [Column(Order = 41, TypeName = "numeric")]
        public decimal tien_nt_x { get; set; }

        [Key]
        [Column(Order = 42, TypeName = "numeric")]
        public decimal tien_nhap { get; set; }

        [Key]
        [Column(Order = 43, TypeName = "numeric")]
        public decimal tien_xuat { get; set; }

        [Key]
        [Column(Order = 44, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        [Key]
        [Column(Order = 45, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        [Key]
        [Column(Order = 46, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        [Key]
        [Column(Order = 47, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        [Key]
        [Column(Order = 48, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        [Key]
        [Column(Order = 49, TypeName = "numeric")]
        public decimal cp { get; set; }

        [Key]
        [Column(Order = 50, TypeName = "numeric")]
        public decimal nk_nt { get; set; }

        [Key]
        [Column(Order = 51, TypeName = "numeric")]
        public decimal nk { get; set; }

        [Key]
        [Column(Order = 52)]
        [StringLength(16)]
        public string tk_thue_nk { get; set; }

        [Key]
        [Column(Order = 53, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        [Key]
        [Column(Order = 54, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        [Key]
        [Column(Order = 55, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        [Key]
        [Column(Order = 56, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        [Key]
        [Column(Order = 57)]
        public byte han_tt { get; set; }

        [Key]
        [Column(Order = 58)]
        public byte cp_thue_ck { get; set; }

        [Key]
        [Column(Order = 59, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Key]
        [Column(Order = 60, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Key]
        [Column(Order = 61, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Key]
        [Column(Order = 62)]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Key]
        [Column(Order = 63)]
        [StringLength(16)]
        public string tk_thue_co { get; set; }

        [Key]
        [Column(Order = 64, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        [Key]
        [Column(Order = 65, TypeName = "numeric")]
        public decimal ck { get; set; }

        [Key]
        [Column(Order = 66)]
        [StringLength(16)]
        public string tk_ck { get; set; }

        [Key]
        [Column(Order = 67)]
        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [Key]
        [Column(Order = 68)]
        [StringLength(13)]
        public string stt_rec_dc { get; set; }

        [Key]
        [Column(Order = 69)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 70)]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Key]
        [Column(Order = 71)]
        [StringLength(16)]
        public string ma_td { get; set; }

        [Key]
        [Column(Order = 72)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [Key]
        [Column(Order = 73)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [StringLength(2)]
        public string loai_ct { get; set; }

        [StringLength(8)]
        public string Ma_vitrin { get; set; }

        [StringLength(8)]
        public string Ma_vitri { get; set; }

        [StringLength(32)]
        public string Ong_ba { get; set; }

        [StringLength(16)]
        public string Ma_sp { get; set; }

        [StringLength(16)]
        public string So_lsx { get; set; }

        [StringLength(16)]
        public string Ma_hd { get; set; }

        [StringLength(16)]
        public string Ma_ku { get; set; }

        [StringLength(16)]
        public string Ma_phi { get; set; }

        [StringLength(8)]
        public string Ma_nvien { get; set; }

        [StringLength(16)]
        public string Ma_lo { get; set; }

        [StringLength(8)]
        public string Ma_httt { get; set; }

        [Key]
        [Column(Order = 74, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 75)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 76)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 77, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 78)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 79)]
        public byte user_id2 { get; set; }

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
        public string Ma_bpht { get; set; }

        [StringLength(1)]
        public string tang { get; set; }

        [Key]
        [Column(Order = 80, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        [Key]
        [Column(Order = 81, TypeName = "numeric")]
        public decimal TIEN1 { get; set; }

        [Key]
        [Column(Order = 82, TypeName = "numeric")]
        public decimal TIEN1_NT { get; set; }

        [Key]
        [Column(Order = 83)]
        [StringLength(13)]
        public string STT_RECDH { get; set; }

        [Key]
        [Column(Order = 84)]
        [StringLength(5)]
        public string STT_REC0DH { get; set; }

        [Key]
        [Column(Order = 85)]
        [StringLength(8)]
        public string MA_LNXN { get; set; }

        [Key]
        [Column(Order = 86)]
        [StringLength(8)]
        public string MA_LNX { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Key]
        [Column(Order = 87)]
        [StringLength(16)]
        public string Ma_kmm { get; set; }

        [Key]
        [Column(Order = 88)]
        [StringLength(16)]
        public string Ma_kmb { get; set; }

        [Key]
        [Column(Order = 89, TypeName = "numeric")]
        public decimal gg_nt { get; set; }

        [Key]
        [Column(Order = 90, TypeName = "numeric")]
        public decimal gg { get; set; }

        [Key]
        [Column(Order = 91)]
        [StringLength(16)]
        public string TK_GG { get; set; }

        [StringLength(8)]
        public string Ma_gia { get; set; }

        [Key]
        [Column(Order = 92)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [Column(Order = 93)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [Key]
        [Column(Order = 94)]
        [StringLength(1)]
        public string MA_LCT { get; set; }

        [Key]
        [Column(Order = 95)]
        [StringLength(1)]
        public string TT_LOAI { get; set; }

        [Key]
        [Column(Order = 96)]
        [StringLength(40)]
        public string TT_NGHE { get; set; }

        [Key]
        [Column(Order = 97)]
        [StringLength(16)]
        public string MA_SPPH { get; set; }

        [Key]
        [Column(Order = 98)]
        [StringLength(16)]
        public string MA_TD2PH { get; set; }

        [Key]
        [Column(Order = 99)]
        [StringLength(16)]
        public string MA_TD3PH { get; set; }

        [Key]
        [Column(Order = 100)]
        [StringLength(20)]
        public string TT_SOXE { get; set; }

        [Key]
        [Column(Order = 101)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 102)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 103)]
        [StringLength(4)]
        public string TT_NAMNU { get; set; }

        [Key]
        [Column(Order = 104, TypeName = "numeric")]
        public decimal TT_TUOI { get; set; }

        [Key]
        [Column(Order = 105, TypeName = "numeric")]
        public decimal TT_KMDI { get; set; }

        [Key]
        [Column(Order = 106, TypeName = "numeric")]
        public decimal TT_LANKT { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TT_NGAYMUA { get; set; }

        [StringLength(48)]
        public string TT_NOIMUA { get; set; }

        [Key]
        [Column(Order = 107)]
        [StringLength(20)]
        public string DIEN_THOAI { get; set; }

        [Key]
        [Column(Order = 108)]
        [StringLength(20)]
        public string DT_DD { get; set; }

        [Key]
        [Column(Order = 109)]
        public string SO_IMAGE { get; set; }

        [StringLength(128)]
        public string DIA_CHI { get; set; }

        [Key]
        [Column(Order = 110)]
        [StringLength(100)]
        public string TT_SONHA { get; set; }

        [Key]
        [Column(Order = 111)]
        [StringLength(16)]
        public string MA_PHUONG { get; set; }

        [Key]
        [Column(Order = 112)]
        [StringLength(16)]
        public string MA_TINH { get; set; }

        [Key]
        [Column(Order = 113)]
        [StringLength(16)]
        public string MA_QUAN { get; set; }

        [Key]
        [Column(Order = 114)]
        [StringLength(48)]
        public string TT_NVSC { get; set; }

        [Key]
        [Column(Order = 115)]
        [StringLength(1)]
        public string TT_LISTNV { get; set; }

        [Key]
        [Column(Order = 116)]
        [StringLength(8)]
        public string MA_BP1 { get; set; }

        [StringLength(48)]
        public string MA_THE { get; set; }

        [Key]
        [Column(Order = 117)]
        [StringLength(5)]
        public string LOAI_THE { get; set; }

        [Key]
        [Column(Order = 118)]
        [StringLength(5)]
        public string TT_GIOVAO { get; set; }

        [Key]
        [Column(Order = 119)]
        [StringLength(5)]
        public string TT_GIORA { get; set; }

        [Key]
        [Column(Order = 120)]
        [StringLength(16)]
        public string TT_SOLSX { get; set; }

        [Key]
        [Column(Order = 121, TypeName = "numeric")]
        public decimal Tien_tb { get; set; }
    }

}
