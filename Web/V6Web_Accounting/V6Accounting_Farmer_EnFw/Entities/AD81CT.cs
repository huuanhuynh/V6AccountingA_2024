namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AD81CT
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
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string ma_loai { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string tt_vt { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(100)]
        public string dg_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_01 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_02 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_03 { get; set; }

        [StringLength(20)]
        public string char_01 { get; set; }

        [StringLength(20)]
        public string char_02 { get; set; }

        [StringLength(20)]
        public string char_03 { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal num_01 { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal num_02 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal num_03 { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 13)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Key]
        [Column(Order = 16, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 18)]
        public byte user_id2 { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(60)]
        public string NGUOI_SD1 { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(40)]
        public string DIENTHOAI1 { get; set; }

        [Key]
        [Column(Order = 21)]
        [StringLength(40)]
        public string DT_DD1 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(20)]
        public string TT_SOXE1 { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(4)]
        public string TT_NAMNU1 { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        [Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string MA_TD2 { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string MA_TD3 { get; set; }
    }

}
