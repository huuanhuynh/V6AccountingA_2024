namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alkho")]
    public partial class Alkho
    {
        [Key]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_kho { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_kho2 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_dl { get; set; }

        public byte stt_ntxt { get; set; }

        [StringLength(30)]
        public string thu_kho { get; set; }

        [StringLength(64)]
        public string dia_chi { get; set; }

        [StringLength(32)]
        public string dien_thoai { get; set; }

        [StringLength(32)]
        public string fax { get; set; }

        [StringLength(32)]
        public string email { get; set; }

        [StringLength(8)]
        public string ma_lotrinh { get; set; }

        [StringLength(8)]
        public string ma_vc { get; set; }

        [StringLength(128)]
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

        [Required]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

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

        [Required]
        [StringLength(1)]
        public string date_yn { get; set; }

        [Required]
        [StringLength(1)]
        public string lo_yn { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_DVCS1 { get; set; }

        public Guid? UID { get; set; }
    }
}
