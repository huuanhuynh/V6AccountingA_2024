namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD11
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
        [Column(Order = 6)]
        public byte tk_cn_i { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(5)]
        public string nh_dk { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 14)]
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
        [Column(Order = 15)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM11 AM11 { get; set; }
    }

}
