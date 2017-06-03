namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADHSCC")]
    public partial class ADHSCC
    {
        [Column(TypeName = "numeric")]
        public decimal? cc0 { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_cc { get; set; }

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

        [StringLength(16)]
        public string tk_pb { get; set; }

        [StringLength(16)]
        public string tk_cc { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_pb_ky { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? t_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_gt_pb_ky { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        [Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }
    }
}
