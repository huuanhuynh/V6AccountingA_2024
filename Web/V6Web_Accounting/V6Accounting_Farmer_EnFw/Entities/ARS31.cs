namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ARS31
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec_hd { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ky { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal tien { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 14)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(1)]
        public string status { get; set; }

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
        [Column(Order = 19, TypeName = "numeric")]
        public decimal sl_td1 { get; set; }

        [Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal sl_td2 { get; set; }

        [Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal sl_td3 { get; set; }

        [Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_td1 { get; set; }

        [Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string ma_td2 { get; set; }

        [Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Key]
        [Column(Order = 25, TypeName = "smalldatetime")]
        public DateTime ngay_td1 { get; set; }

        [Key]
        [Column(Order = 26, TypeName = "smalldatetime")]
        public DateTime ngay_td2 { get; set; }

        [Key]
        [Column(Order = 27, TypeName = "smalldatetime")]
        public DateTime ngay_td3 { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(24)]
        public string gc_td1 { get; set; }

        [Key]
        [Column(Order = 29)]
        [StringLength(24)]
        public string gc_td2 { get; set; }

        [Key]
        [Column(Order = 30)]
        [StringLength(24)]
        public string gc_td3 { get; set; }
    }

}
