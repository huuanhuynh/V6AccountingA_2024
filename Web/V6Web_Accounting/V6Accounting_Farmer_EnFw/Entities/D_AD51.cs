namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

}
