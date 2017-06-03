namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ABSPDD")]
    public partial class ABSPDD
    {
        [Column(TypeName = "numeric")]
        public decimal? nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Thang { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string so_lsx { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal sl_dd { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal tl_ht { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal sl_qd { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal sl_nk { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal sl_sx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_dd_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_dd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

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
