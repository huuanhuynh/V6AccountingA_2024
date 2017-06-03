namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Aldmvtct")]
    public partial class Aldmvtct
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_bpht { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_hl1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal sl_SP { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal sl_dm_dh { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal sl_dm_kh { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal sl_tt { get; set; }

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
        [Column(Order = 11, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string time2 { get; set; }

        [Key]
        [Column(Order = 13)]
        public byte user_id2 { get; set; }

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
