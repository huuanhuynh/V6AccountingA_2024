namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD56
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_kh_i { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tt { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

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

        [Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal ty_gia_ht2 { get; set; }

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
        [Column(Order = 23, TypeName = "numeric")]
        public decimal Tien { get; set; }

        [Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        [Key]
        [Column(Order = 30)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM56 AM56 { get; set; }
    }

}

