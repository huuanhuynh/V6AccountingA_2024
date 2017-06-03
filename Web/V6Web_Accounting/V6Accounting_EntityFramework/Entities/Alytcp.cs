namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alytcp")]
    public partial class Alytcp
    {
        [Key]
        [StringLength(8)]
        public string ma_ytcp { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ytcp { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ytcp2 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_no { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_co { get; set; }

        [Required]
        [StringLength(2)]
        public string nhom { get; set; }

        public byte ddck_ck { get; set; }

        [Required]
        [StringLength(12)]
        public string ten_ngan { get; set; }

        [Required]
        [StringLength(12)]
        public string ten_ngan2 { get; set; }

        [Column(TypeName = "text")]
        public string ghi_chu { get; set; }

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
