namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADCTCCBP")]
    public partial class ADCTCCBP
    {
        [Column(TypeName = "numeric")]
        public decimal? CC0 { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ky { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string so_the_CC { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_bp_i { get; set; }

        [StringLength(16)]
        public string tk_pb_i { get; set; }

        [StringLength(16)]
        public string tk_cp_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? He_so { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string time0 { get; set; }

        [Key]
        [Column(Order = 6)]
        public byte user_id0 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(1)]
        public string status { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 10)]
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

        [Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        [Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        [Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        [Key]
        [Column(Order = 19)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }
    }
}
