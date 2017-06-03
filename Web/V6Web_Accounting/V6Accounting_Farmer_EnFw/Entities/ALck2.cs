namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ALck2
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_ck { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string nh_kh9 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string nh_vt9 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tl_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_ck { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime ngay_hl1 { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime ngay_hl2 { get; set; }

        [StringLength(8)]
        public string ma_gia { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal Tien { get; set; }
    }

}
