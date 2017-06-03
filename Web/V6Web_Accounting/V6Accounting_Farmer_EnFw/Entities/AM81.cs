namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AM81
    {
        public AM81()
        {
            AD81 = new HashSet<AD81>();
        }

        [Key]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_seri { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Required]
        [StringLength(12)]
        public string so_lo1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_kh { get; set; }

        public byte tk_cn { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(18)]
        public string ma_so_thue { get; set; }

        [Required]
        [StringLength(128)]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_nx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue { get; set; }

        public byte sua_thue { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_co { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sua_tkthue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        public byte tinh_ck { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_ck_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal han_tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

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

        [Required]
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
        public decimal T_Tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_Tien1_nt { get; set; }

        [Required]
        [StringLength(16)]
        public string Ma_SONB { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_gg { get; set; }

        [Required]
        [StringLength(16)]
        public string TK_GG { get; set; }

        [Required]
        [StringLength(1)]
        public string LOAI_HD { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime NGAY_CT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_TIEN_NT4 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_TIEN4 { get; set; }

        [Required]
        [StringLength(128)]
        public string GHI_CHU01 { get; set; }

        [Required]
        [StringLength(1)]
        public string LOAI_CT0 { get; set; }

        [Required]
        [StringLength(1)]
        public string MA_LCT { get; set; }

        [Required]
        [StringLength(1)]
        public string TT_LOAI { get; set; }

        [Required]
        [StringLength(40)]
        public string TT_NGHE { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_SPPH { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_TD2PH { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_TD3PH { get; set; }

        [Required]
        [StringLength(20)]
        public string TT_SOXE { get; set; }

        [Required]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Required]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Required]
        [StringLength(4)]
        public string TT_NAMNU { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TT_TUOI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TT_KMDI { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TT_LANKT { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TT_NGAYMUA { get; set; }

        [StringLength(48)]
        public string TT_NOIMUA { get; set; }

        [Required]
        [StringLength(20)]
        public string DIEN_THOAI { get; set; }

        [Required]
        [StringLength(20)]
        public string DT_DD { get; set; }

        [Required]
        [StringLength(128)]
        public string SO_IMAGE { get; set; }

        [Required]
        [StringLength(100)]
        public string TT_SONHA { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_PHUONG { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_TINH { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_QUAN { get; set; }

        [Required]
        [StringLength(48)]
        public string TT_NVSC { get; set; }

        [Required]
        [StringLength(1)]
        public string TT_LISTNV { get; set; }

        [Required]
        [StringLength(8)]
        public string MA_BP1 { get; set; }

        [StringLength(48)]
        public string MA_THE { get; set; }

        [Required]
        [StringLength(5)]
        public string LOAI_THE { get; set; }

        [Required]
        [StringLength(5)]
        public string TT_GIOVAO { get; set; }

        [Required]
        [StringLength(5)]
        public string TT_GIORA { get; set; }

        [Required]
        [StringLength(20)]
        public string SO_CMND { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_CMND { get; set; }

        [Required]
        [StringLength(40)]
        public string NOI_CMND { get; set; }

        [Column(TypeName = "numeric")]
        public decimal NAM_SINH { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_SINH { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_GIAO { get; set; }

        [Required]
        [StringLength(128)]
        public string GHI_CHU02 { get; set; }

        [Required]
        [StringLength(3)]
        public string MA_NT01 { get; set; }

        [Required]
        [StringLength(3)]
        public string MA_NT02 { get; set; }

        [Required]
        [StringLength(3)]
        public string MA_NT03 { get; set; }

        [Required]
        [StringLength(3)]
        public string MA_NT04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TY_GIA01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TY_GIA02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TY_GIA03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TY_GIA04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN_NT01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN_NT02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN_NT03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN_NT04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal TTIEN04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_TIEN_NT5 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal T_TIEN5 { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_KH3 { get; set; }

        [Required]
        [StringLength(16)]
        public string TT_SOLSX { get; set; }

        [Required]
        [StringLength(20)]
        public string ma_mauhd { get; set; }

        public virtual ICollection<AD81> AD81 { get; set; }
    }

}
