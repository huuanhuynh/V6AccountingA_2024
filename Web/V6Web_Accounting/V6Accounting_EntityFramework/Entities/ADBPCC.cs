namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADBPCC")]
    public partial class ADBPCC
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
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_CC { get; set; }

        [StringLength(16)]
        public string tk_PB { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

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
