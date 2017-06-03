namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADKHTS")]
    public partial class ADKHT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_kh { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ky { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_ts { get; set; }

        [StringLength(16)]
        public string tk_kh { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_kh_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }

}
