namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alck")]
    public partial class Alck
    {
        [Key]
        [StringLength(8)]
        public string ma_ck { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ck { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ck2 { get; set; }

        [StringLength(2)]
        public string loai_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? muc_do { get; set; }

        [StringLength(1)]
        public string tien_yn { get; set; }

        [StringLength(1)]
        public string tienh_yn { get; set; }

        [StringLength(1)]
        public string cong_yn { get; set; }

        [StringLength(1)]
        public string con_lai_yn { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct2 { get; set; }

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
    }
}
