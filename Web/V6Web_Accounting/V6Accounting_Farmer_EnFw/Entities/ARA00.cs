namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARA00
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [Key]
        [Column(Order = 7)]
        public string dien_giaih { get; set; }

        [Key]
        [Column(Order = 8)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(5)]
        public string nh_dk { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string tk { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string tk_du { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Key]
        [Column(Order = 19)]
        public byte tk_cn { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_td { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(2)]
        public string loai_ct { get; set; }

        [StringLength(16)]
        public string Ma_sp { get; set; }

        [StringLength(16)]
        public string So_lsx { get; set; }

        [StringLength(16)]
        public string Ma_hd { get; set; }

        [StringLength(16)]
        public string Ma_phi { get; set; }

        [StringLength(8)]
        public string Ma_nvien { get; set; }

        [StringLength(8)]
        public string Ma_bpht { get; set; }

        [Key]
        [Column(Order = 23)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 26)]
        public byte user_id2 { get; set; }

        [Key]
        [Column(Order = 27, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(1)]
        public string status { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        public byte? ct_nxt { get; set; }

        [StringLength(1)]
        public string ma_gd { get; set; }

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
        public string ma_bp { get; set; }

        [Key]
        [Column(Order = 30)]
        [StringLength(12)]
        public string LOAI_CTGS { get; set; }

        [Key]
        [Column(Order = 31)]
        [StringLength(1)]
        public string KIEU_CTGS { get; set; }

        [Key]
        [Column(Order = 32)]
        [StringLength(12)]
        public string SO_LO0 { get; set; }

        [Key]
        [Column(Order = 33)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 34)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 35)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        [Key]
        [Column(Order = 36)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        [Key]
        [Column(Order = 37)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        [Key]
        [Column(Order = 38)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }

}
