namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD32
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
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 7)]
        public string dien_giaii { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal tien { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        [Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt { get; set; }

        [Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Key]
        [Column(Order = 16)]
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
        [Column(Order = 17)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM32 AM32 { get; set; }
    }

}
