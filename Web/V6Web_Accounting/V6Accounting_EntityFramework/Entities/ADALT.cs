namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADALTS")]
    public partial class ADALT
    {
        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal Ts0 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_gs { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ky { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal Tang_giam { get; set; }

        [StringLength(8)]
        public string ma_tg_ts { get; set; }

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
        [Column(Order = 7, TypeName = "numeric")]
        public decimal so_ky { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 10)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 14)]
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
