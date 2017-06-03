namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;
    using global::System.Data.Entity.Spatial;

    [Table("CorpLan")]
    public partial class CorpLan
    {
        [Required]
        [StringLength(50)]
        public string SFile { get; set; }

        [Required]
        [StringLength(20)]
        public string SField { get; set; }

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(1)]
        public string ctype { get; set; }

        [Required]
        [StringLength(200)]
        public string Sname { get; set; }

        [Required]
        [StringLength(254)]
        public string D { get; set; }

        [Required]
        [StringLength(254)]
        public string V { get; set; }

        [Required]
        [StringLength(254)]
        public string E { get; set; }

        [Required]
        [StringLength(254)]
        public string F { get; set; }

        [Required]
        [StringLength(254)]
        public string C { get; set; }

        [Required]
        [StringLength(254)]
        public string R { get; set; }

        [Required]
        [StringLength(254)]
        public string J { get; set; }

        [Required]
        [StringLength(254)]
        public string K { get; set; }

        [StringLength(3)]
        public string Ma_ct { get; set; }

        [StringLength(1)]
        public string M_or_D { get; set; }
    }


    public partial class D_AM81
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [StringLength(3)]
        public string ma_nk { get; set; }

        [StringLength(1)]
        public string ma_gd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        [StringLength(12)]
        public string so_seri { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(12)]
        public string so_lo { get; set; }

        [StringLength(12)]
        public string so_lo1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        public string ma_kh { get; set; }

        public byte? tk_cn { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [StringLength(18)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string ma_nx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_so_luong { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_thue { get; set; }

        public byte? sua_thue { get; set; }

        [StringLength(16)]
        public string tk_thue_co { get; set; }

        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_tkthue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        public byte? tinh_ck { get; set; }

        [StringLength(16)]
        public string tk_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_ck_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? han_tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(48)]
        public string ten_vtthue { get; set; }

        [StringLength(16)]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        public string ma_ud3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        public string gc_ud3 { get; set; }

        [StringLength(16)]
        public string ma_ud1 { get; set; }

        [StringLength(1)]
        public string K_V { get; set; }

        [StringLength(2)]
        public string ma_httt { get; set; }

        [StringLength(8)]
        public string ma_nvien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        public string loai_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        public byte? sua_ck { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Tien1_nt { get; set; }

        [StringLength(16)]
        public string Ma_SONB { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_gg { get; set; }

        [StringLength(16)]
        public string TK_GG { get; set; }

        [StringLength(1)]
        public string LOAI_HD { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_CT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_TIEN_NT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_TIEN4 { get; set; }

        [StringLength(128)]
        public string GHI_CHU01 { get; set; }

        [StringLength(1)]
        public string LOAI_CT0 { get; set; }

        [StringLength(1)]
        public string MA_LCT { get; set; }

        [StringLength(1)]
        public string TT_LOAI { get; set; }

        [StringLength(40)]
        public string TT_NGHE { get; set; }

        [StringLength(16)]
        public string MA_SPPH { get; set; }

        [StringLength(16)]
        public string MA_TD2PH { get; set; }

        [StringLength(16)]
        public string MA_TD3PH { get; set; }

        [StringLength(20)]
        public string TT_SOXE { get; set; }

        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [StringLength(4)]
        public string TT_NAMNU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TT_TUOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TT_KMDI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TT_LANKT { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TT_NGAYMUA { get; set; }

        [StringLength(48)]
        public string TT_NOIMUA { get; set; }

        [StringLength(20)]
        public string DIEN_THOAI { get; set; }

        [StringLength(20)]
        public string DT_DD { get; set; }

        [StringLength(128)]
        public string SO_IMAGE { get; set; }

        [StringLength(100)]
        public string TT_SONHA { get; set; }

        [StringLength(16)]
        public string MA_PHUONG { get; set; }

        [StringLength(16)]
        public string MA_TINH { get; set; }

        [StringLength(16)]
        public string MA_QUAN { get; set; }

        [StringLength(48)]
        public string TT_NVSC { get; set; }

        [StringLength(1)]
        public string TT_LISTNV { get; set; }

        [StringLength(8)]
        public string MA_BP1 { get; set; }

        [StringLength(48)]
        public string MA_THE { get; set; }

        [StringLength(5)]
        public string LOAI_THE { get; set; }

        [StringLength(5)]
        public string TT_GIOVAO { get; set; }

        [StringLength(5)]
        public string TT_GIORA { get; set; }

        [StringLength(20)]
        public string SO_CMND { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_CMND { get; set; }

        [StringLength(40)]
        public string NOI_CMND { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NAM_SINH { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_SINH { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_GIAO { get; set; }

        [StringLength(128)]
        public string GHI_CHU02 { get; set; }

        [StringLength(3)]
        public string MA_NT01 { get; set; }

        [StringLength(3)]
        public string MA_NT02 { get; set; }

        [StringLength(3)]
        public string MA_NT03 { get; set; }

        [StringLength(3)]
        public string MA_NT04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TY_GIA01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TY_GIA02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TY_GIA03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TY_GIA04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN_NT01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN_NT02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN_NT03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN_NT04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TTIEN04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_TIEN_NT5 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_TIEN5 { get; set; }

        [StringLength(16)]
        public string MA_KH3 { get; set; }

        [Key]
        [StringLength(16)]
        public string TT_SOLSX { get; set; }
    }


    public partial class D_AM76
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [StringLength(3)]
        public string ma_nk { get; set; }

        [StringLength(1)]
        public string ma_gd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(9)]
        public string so_lo { get; set; }

        [StringLength(9)]
        public string so_lo1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        public string ma_kh { get; set; }

        public byte? tk_cn { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [StringLength(18)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string ma_nx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_so_luong { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_thue { get; set; }

        public byte? sua_thue { get; set; }

        [StringLength(16)]
        public string tk_thue_co { get; set; }

        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_tkthue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? han_tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(32)]
        public string ghi_chu { get; set; }

        [StringLength(48)]
        public string ten_vtthue { get; set; }

        [StringLength(16)]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        public string ma_ud3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        public string gc_ud3 { get; set; }

        [StringLength(16)]
        public string ma_ud1 { get; set; }

        [StringLength(1)]
        public string K_V { get; set; }

        [StringLength(2)]
        public string ma_httt { get; set; }

        [StringLength(8)]
        public string ma_nvien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Ck_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Gg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Tien1_Nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_Tien1 { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string MA_NT01 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string MA_NT02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string MA_NT03 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string MA_NT04 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal TY_GIA01 { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal TY_GIA02 { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal TY_GIA03 { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal TY_GIA04 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal TTIEN_NT01 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal TTIEN_NT02 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal TTIEN_NT03 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal TTIEN_NT04 { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal TTIEN01 { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal TTIEN02 { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal TTIEN03 { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal TTIEN04 { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal t_tien_nt4 { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal t_tien4 { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "smalldatetime")]
        public DateTime ngay_ct4 { get; set; }

        [Key]
        [Column(Order = 19)]
        public string GHI_CHU01 { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }
    }


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


    public partial class D_AD85
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

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(8)]
        public string ma_vitrin { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [StringLength(8)]
        public string MA_LNXN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

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


    public partial class D_AD84
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

        [StringLength(1)]
        public string px_gia_ddi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

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


    public partial class D_AD81
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
        public string ma_td_i { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong1 { get; set; }

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
        public decimal? gia_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ck_nt { get; set; }

        [StringLength(16)]
        public string tk_vt { get; set; }

        [StringLength(16)]
        public string tk_gv { get; set; }

        [StringLength(16)]
        public string tk_dt { get; set; }

        [StringLength(13)]
        public string stt_rec_pn { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? PT_CKI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN1_NT { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gg { get; set; }

        [StringLength(8)]
        public string Ma_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA_NT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? GIA4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_NT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN4 { get; set; }

        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [StringLength(20)]
        public string SO_MAY { get; set; }

        [StringLength(128)]
        public string so_image { get; set; }

        [StringLength(1)]
        public string Status_DPI { get; set; }

        [Key]
        [StringLength(16)]
        public string SO_LSX { get; set; }
    }


    public partial class D_AD76
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
        public string ma_td_i { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong1 { get; set; }

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
        public decimal? gia_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [StringLength(16)]
        public string tk_vt { get; set; }

        [StringLength(16)]
        public string tk_gv { get; set; }

        [StringLength(16)]
        public string tk_tl { get; set; }

        [StringLength(13)]
        public string stt_rec_hd { get; set; }

        [StringLength(5)]
        public string stt_rec0hd { get; set; }

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
        public decimal? gia_nt21 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia21 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt0 { get; set; }

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


    public partial class D_AD74
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

        [StringLength(16)]
        public string tk_vt { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [StringLength(13)]
        public string stt_rec_px { get; set; }

        [StringLength(5)]
        public string stt_rec0px { get; set; }

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

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

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


    public partial class D_AD73
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
        public decimal? gia_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

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

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(13)]
        public string STT_REC_PN { get; set; }

        [StringLength(5)]
        public string STT_REC0PN { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [Key]
        [StringLength(16)]
        public string SO_LSX { get; set; }
    }


    public partial class D_AD72
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
        public decimal? nk_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nk { get; set; }

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

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [StringLength(16)]
        public string SO_LSX { get; set; }
    }


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


    public partial class D_AD56
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(8)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_qd { get; set; }

        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? ty_gia_ht2 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? Tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt { get; set; }

        [Key]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD51
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_qd { get; set; }

        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? ty_gia_ht2 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? Tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt { get; set; }

        [Key]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD46
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_qd { get; set; }

        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? ty_gia_ht2 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? Tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [Key]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD41
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_qd { get; set; }

        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? ty_gia_ht2 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TIEN_QD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TY_GIAQD { get; set; }

        [Key]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }

    public partial class D_AD39
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        public byte? tk_cn_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(5)]
        public string nh_dk { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

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
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD32
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
        public string tk_vt { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

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
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD31
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
        public string tk_vt { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

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
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    public partial class D_AD29
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        public byte? tk_cn_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(5)]
        public string nh_dk { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

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
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


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


    public partial class D_AD11
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(128)]
        public string dien_giaii { get; set; }

        [StringLength(16)]
        public string tk_i { get; set; }

        public byte? tk_cn_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [StringLength(5)]
        public string nh_dk { get; set; }

        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [StringLength(16)]
        public string ma_td_i { get; set; }

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

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [Key]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    [Table("Corpuser")]
    public partial class Corpuser
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string user_name { get; set; }

        [Required]
        [StringLength(8)]
        public string user_pre { get; set; }

        [Required]
        [StringLength(64)]
        public string password { get; set; }

        [Required]
        [StringLength(48)]
        public string comment { get; set; }

        public bool is_Vadmin { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string is_Madmin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal del_yn { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Rmodule { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Inherit_id { get; set; }

        [Required]
        [StringLength(1)]
        public string inherit_ch { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [Required]
        [StringLength(2)]
        public string LEVEL { get; set; }
    }


    [Table("CorpLang")]
    public partial class CorpLang
    {
        [Key]
        [StringLength(1)]
        public string Lan_Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Lan_name { get; set; }

        [Required]
        [StringLength(64)]
        public string Lan_name2 { get; set; }
    }


    public partial class CorpLan2
    {
        [Required]
        [StringLength(50)]
        public string SFile { get; set; }

        [Key]
        [StringLength(20)]
        public string SField { get; set; }

        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Sname { get; set; }

        [Required]
        [StringLength(254)]
        public string D { get; set; }

        [Required]
        [StringLength(254)]
        public string V { get; set; }

        [Required]
        [StringLength(254)]
        public string E { get; set; }

        [Required]
        [StringLength(254)]
        public string F { get; set; }

        [Required]
        [StringLength(254)]
        public string C { get; set; }

        [Required]
        [StringLength(254)]
        public string R { get; set; }

        [Required]
        [StringLength(254)]
        public string J { get; set; }

        [Required]
        [StringLength(254)]
        public string K { get; set; }
    }


    public partial class CorpLan1
    {
        [Required]
        [StringLength(50)]
        public string SFile { get; set; }

        [StringLength(50)]
        public string Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Nline { get; set; }

        [Required]
        [StringLength(254)]
        public string D { get; set; }

        [Required]
        [StringLength(254)]
        public string V { get; set; }

        [Required]
        [StringLength(254)]
        public string E { get; set; }

        [Required]
        [StringLength(254)]
        public string F { get; set; }

        [Required]
        [StringLength(254)]
        public string C { get; set; }

        [Required]
        [StringLength(254)]
        public string R { get; set; }

        [Required]
        [StringLength(254)]
        public string J { get; set; }

        [Required]
        [StringLength(254)]
        public string K { get; set; }
    }


    [Table("ALpost")]
    public partial class ALpost
    {
        [Key]
        [StringLength(3)]
        [Column("ma_ct", Order = 0)]
        public string MaCT { get; set; }

        [StringLength(1)]
        [Required]
        [Column("kieu_post")]
        public string KieuPost { get; set; }
        [Key]
        [StringLength(8)]
        [Column("ma_post", Order = 1)]
        public string MaPost { get; set; }

        
        [Column("default", TypeName = "numeric")]
        public decimal? Default { get; set; }

        [StringLength(48)]
        [Column("ten_post")]
        public string TenPost { get; set; }

        [StringLength(48)]
        [Column("ten_post2")]
        public string TenPost2 { get; set; }

        [StringLength(48)]
        [Column("ten_act")]
        public string TenAct { get; set; }

        [StringLength(48)]
        [Column("ten_act2")]
        public string TenAct2 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string STATUS { get; set; }

        
        [Column("ARI70", TypeName = "numeric")]
        public decimal? ARI70 { get; set; }

        
        [Column("ARA00", TypeName = "numeric")]
        public decimal? ARA00 { get; set; }

        
        [Column("date", TypeName = "smalldatetime")]
        public DateTime? Date { get; set; }

        [StringLength(8)]
        [Column("time")]
        public string Time { get; set; }

        
        [Column("user_id", TypeName = "numeric")]
        public decimal? user_id { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0", TypeName = "numeric")]
        public decimal? user_id0 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string MaTd1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTd2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTd3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTd1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTd2 { get; set; }

        
        [Column("ngay_td3",TypeName = "smalldatetime")]
        public DateTime? NgayTd3 { get; set; }

        
        [Column("sl_td1",TypeName = "numeric")]
        public decimal? SlTd1 { get; set; }

        
        [Column("sl_td2",TypeName = "numeric")]
        public decimal? SlTd2 { get; set; }

        
        [Column("sl_td3",TypeName = "numeric")]
        public decimal? SlTd3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GcTd1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GcTd2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GcTd3 { get; set; }

        
        [Column("dupdate",TypeName = "smalldatetime")]
        public DateTime? Dupdate { get; set; }

        [StringLength(8)]
        [Column("ma_phan_he")]
        public string MaPhanHe { get; set; }

        [StringLength(12)]
        [Column("Itemid")]
        public string Itemid { get; set; }

        
        [Column("stt_in",TypeName = "numeric")]
        public decimal? STTIn { get; set; }

        [StringLength(900)]
        [Column("search")]
        public string Search { get; set; }

        public Guid UID { get; set; }
    }

    [Table("ALqddvt")]
    public partial class ALqddvt
    {
        [Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 0)]
        public string MaVatTu { get; set; }

        [Key]
        [StringLength(10)]
        [Column("dvt", Order = 1)]
        public string DonViTinh { get; set; }

        [Key]
        [StringLength(10)]
        [Column("dvtqd", Order = 2)]
        public string DonViTinhQuyDoi { get; set; }

        [Column("he_so", TypeName = "numeric")]
        public decimal HeSo { get; set; }

        [StringLength(1)]
        [Column("xtype")]
        public string Xtype { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }


        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string MaTuDo1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTuDo2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2",TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALstt")]
    public partial class ALstt
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt_rec { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_dn { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ks { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam_bd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ky1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ks_ky { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_Dk { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_Ck { get; set; }
    }


    [Table("Alquan")]
    public partial class Alquan
    {
        [Key]
        [StringLength(16)]
        [Column("ma_quan")]
        public string MaQuan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_quan")]
        public string Ten_quan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_quan2")]
        public string Ten_quan2 { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string ghi_chu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }


        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }


        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }


        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }


        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alql")]
    public partial class Alql
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal thang { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ql { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("Alqg")]
    public partial class Alqg
    {
        [Key]
        [StringLength(8)]
        [Column("ma_qg")]
        public string MaQuocGia { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_qg")]
        public string TenQuocGia { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_qg2")]
        public string TenQuocGia2 { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }


        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALTHAUCT")]
    public partial class ALTHAUCT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string Ma_THAU { get; set; }

        [StringLength(16)]
        public string Ma_THAU0 { get; set; }

        [StringLength(16)]
        public string Ma_kh { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_km { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_km { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 10)]
        public byte user_id2 { get; set; }

        [Key]
        [Column(Order = 11)]
        public string Ghi_chukm { get; set; }

        [Key]
        [Column(Order = 12)]
        public string Ghi_chuck { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal T_SLKM { get; set; }
    }

    [Table("ALTHAU")]
    public partial class ALTHAU
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string Ma_THAU { get; set; }

        [Required]
        [StringLength(48)]
        public string Ten_THAU { get; set; }

        [Required]
        [StringLength(48)]
        public string Ten_THAU2 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [StringLength(16)]
        public string Ma_THAU0 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string Ma_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        public string ghi_chu { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

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
    }

    [Table("ALtgnt")]
    public partial class ALtgnt
    {
        [Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 0)]
        public string MaNgoaiTe  { get; set; }

        [Key]
        [Column("ngay_ct", Order = 1, TypeName = "smalldatetime")]
        public DateTime NgayChungTu { get; set; }

        [Key]
        [Column("ty_gia", Order = 2, TypeName = "numeric")]
        public decimal ty_Gia { get; set; }

        [Key]
        [Column("date0", Order = 3, TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [StringLength(8)]
        [Column("time0", Order = 4)]
        public string CreatedTime { get; set; }

        [Key]
        [Column("user_id0", Order = 5)]
        public byte CreatedUserId { get; set; }

        [Key]
        [StringLength(1)]
        [Column("status", Order = 6)]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }


        [Column("user_id2")]
        public byte? ModifiedUserId { get; set; }
        public Guid UID { get; set; }
    }


    [Table("ALtgcc")]
    public partial class ALtgcc
    {
        [Key]
        [StringLength(2)]
        [Column("ma_tg_cc")]
        public string Ma_TGCungCap { get; set; }

        [Required]
        [StringLength(32)]
        [Column("ten_tg_cc")]
        public string Ten_tg_cc { get; set; }

        [Required]
        [StringLength(32)]
        [Column("ten_tg_cc2")]
        public string Ten_tg_cc2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("loai_tg_cc")]
        public string Loai_tg_cc { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string MaTuDo1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTuDo2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTuDo3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        public Guid UID { get; set; }
    }

    public partial class Altd3
    {
        [Key]
        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td3 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td32 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(1)]
        public string LOAI { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [StringLength(24)]
        public string GC_TD1 { get; set; }

        [StringLength(24)]
        public string GC_TD2 { get; set; }

        [StringLength(24)]
        public string GC_TD3 { get; set; }
    }

    public partial class Altd2
    {
        [Key]
        [StringLength(16)]
        public string ma_td2 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td2 { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td22 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        [StringLength(24)]
        public string GC_TD1 { get; set; }

        [StringLength(24)]
        public string GC_TD2 { get; set; }

        [StringLength(24)]
        public string GC_TD3 { get; set; }
    }

    [Table("Altd")]
    public partial class Altd
    {
        [Key]
        [StringLength(16)]
        public string ma_td { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_td2 { get; set; }

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
    }

    public partial class v6bak
    {
        [Key]
        [Column(Order = 0, TypeName = "smalldatetime")]
        public DateTime ngay_bk { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal max_file { get; set; }

        [Key]
        [Column(Order = 2)]
        public string file_name { get; set; }

        [StringLength(128)]
        public string file_zip { get; set; }
    }

    public partial class V6Lookup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string vVar { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string vMa_file { get; set; }

        [Required]
        [StringLength(16)]
        public string vOrder { get; set; }

        [Required]
        [StringLength(16)]
        public string vValue { get; set; }

        [Required]
        [StringLength(254)]
        public string vLfScatter { get; set; }

        [Required]
        [StringLength(128)]
        public string vWidths { get; set; }

        [Required]
        [StringLength(254)]
        public string vFields { get; set; }

        [Required]
        [StringLength(254)]
        public string eFields { get; set; }

        [Required]
        [StringLength(254)]
        public string vHeaders { get; set; }

        [Required]
        [StringLength(254)]
        public string eHeaders { get; set; }

        [Required]
        [StringLength(1)]
        public string vUpdate { get; set; }

        [Required]
        [StringLength(254)]
        public string vTitle { get; set; }

        [Required]
        [StringLength(254)]
        public string eTitle { get; set; }

        [StringLength(254)]
        public string VTitlenew { get; set; }

        [StringLength(254)]
        public string ETitlenew { get; set; }

        public byte Large_yn { get; set; }

        [StringLength(100)]
        public string v1Title { get; set; }

        [StringLength(100)]
        public string e1Title { get; set; }

        [StringLength(128)]
        public string V_Search { get; set; }
    }


    public partial class V6Menu
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string module_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string v2id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string jobid { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string itemid { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stt_box { get; set; }

        [StringLength(2)]
        public string hotkey { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string vbar { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string vbar2 { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int hide_yn { get; set; }

        [StringLength(50)]
        public string page { get; set; }

        [StringLength(10)]
        public string ma_ct { get; set; }

        [StringLength(1)]
        public string loai_ct { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int basic_right { get; set; }
    }

    public partial class V6option
    {
        [Required]
        [StringLength(24)]
        [Column("ma_phan_he")]
        public string MaPhanHe { get; set; }

        [Key]
        [StringLength(3)]
        [Column("stt")]
        public string STT { get; set; }

        [Column("attribute")]
        public byte Attribute { get; set; }

        [Required]
        [StringLength(24)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(1)]
        [Column("type")]
        public string Loai { get; set; }

        [Required]
        [StringLength(64)]
        [Column("descript")]
        public string Descript { get; set; }

        [Required]
        [StringLength(64)]
        [Column("descript2")]
        public string Descript2 { get; set; }

        [Required]
        [StringLength(72)]
        [Column("val")]
        public string Val { get; set; }

        [Required]
        [StringLength(72)]
        [Column("defaul")]
        public string Defaul { get; set; }

        [Required]
        [StringLength(2)]
        [Column("formattype")]
        public string FormatLoai { get; set; }

        [Required]
        [StringLength(48)]
        [Column("inputmask")]
        public string Inputmask { get; set; }

        public Guid UID { get; set; }
    }


    [Table("VCOMMENT")]
    public partial class VCOMMENT
    {
        [StringLength(13)]
        public string stt_rec { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [StringLength(5)]
        public string KHOA { get; set; }

        [StringLength(2)]
        public string Level { get; set; }

        [StringLength(64)]
        public string Level_name { get; set; }

        [StringLength(64)]
        public string ten_user { get; set; }

        [StringLength(1)]
        public string Lock { get; set; }

        [StringLength(200)]
        public string comment1 { get; set; }

        [StringLength(200)]
        public string comment2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "smalldatetime")]
        public DateTime NGAY_CT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string SO_CT { get; set; }

        [Key]
        [Column(Order = 2)]
        public string DIEN_GIAI { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string MA_DVCS { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string MA_KHO { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string STATUS { get; set; }
    }

    public partial class V6rights
    {
        [Key]
        [Column(Order = 0)]
        public byte user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string Sfield { get; set; }

        [Required]
        [StringLength(50)]
        public string D { get; set; }

        [Required]
        [StringLength(50)]
        public string V { get; set; }

        [Required]
        [StringLength(50)]
        public string E { get; set; }

        [StringLength(1)]
        public string Rread { get; set; }

        [StringLength(1)]
        public string Rhide { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string MD { get; set; }

        [StringLength(10)]
        public string sfile { get; set; }

        [StringLength(1)]
        public string loai { get; set; }
    }

}
