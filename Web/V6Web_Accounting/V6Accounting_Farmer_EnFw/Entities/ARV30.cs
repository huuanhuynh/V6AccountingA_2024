namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARV30
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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal mau_bc { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [StringLength(128)]
        public string ten_kh { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(18)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        public string ten_vt { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal gia { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string tk_du { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(32)]
        public string ghi_chu { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 18)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 27)]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        public string ma_kho { get; set; }

        [StringLength(16)]
        public string ma_vv { get; set; }

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
        [Column(Order = 28)]
        [StringLength(16)]
        public string Ma_kh2 { get; set; }

        [Key]
        [Column(Order = 29, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 30)]
        [StringLength(20)]
        public string ma_mauhd { get; set; }
    }

}
