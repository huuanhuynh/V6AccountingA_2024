namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARS30
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ma_tt { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Key]
        [Column(Order = 7)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal t_cp { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal t_nk_nt { get; set; }

        [Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal t_nk { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal t_tt_nt0 { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal t_tt0 { get; set; }

        [Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal han_tt { get; set; }

        [Key]
        [Column(Order = 27, TypeName = "numeric")]
        public decimal t_tt_qd { get; set; }

        [Key]
        [Column(Order = 28, TypeName = "numeric")]
        public decimal tat_toan { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_tt { get; set; }

        [Key]
        [Column(Order = 29, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        [Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal tt { get; set; }

        [Key]
        [Column(Order = 31, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        [Key]
        [Column(Order = 32)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [Key]
        [Column(Order = 33, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 34)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 35)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 36)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 37, TypeName = "numeric")]
        public decimal t_tt1 { get; set; }

        [Key]
        [Column(Order = 38, TypeName = "numeric")]
        public decimal t_tt_nt1 { get; set; }

        [Key]
        [Column(Order = 39)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 40, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 41)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 42)]
        public byte user_id2 { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_cn { get; set; }

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

        [Key]
        [Column(Order = 43, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        [Key]
        [Column(Order = 44, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        [Key]
        [Column(Order = 45, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        [Key]
        [Column(Order = 46)]
        [StringLength(8)]
        public string MA_KHO { get; set; }

        [Key]
        [Column(Order = 47)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 48)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 49)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        [Key]
        [Column(Order = 50)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        [Key]
        [Column(Order = 51)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        [Key]
        [Column(Order = 52)]
        [StringLength(32)]
        public string ONG_BA { get; set; }

        [Key]
        [Column(Order = 53)]
        [StringLength(16)]
        public string TT_SOLSX { get; set; }

        [Key]
        [Column(Order = 54)]
        [StringLength(16)]
        public string MA_BP { get; set; }

        [Key]
        [Column(Order = 55)]
        [StringLength(8)]
        public string MA_NVIEN { get; set; }
    }
}
