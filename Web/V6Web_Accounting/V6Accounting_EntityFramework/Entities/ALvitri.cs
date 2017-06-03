namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALvitri")]
    public partial class ALvitri
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_vitri { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_vitri { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_vitri2 { get; set; }

        public byte? stt_ntxt { get; set; }

        [StringLength(8)]
        public string ma_loai { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kieu_nhap { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kieu_xuat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kieu_ban { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(128)]
        public string ghi_chu { get; set; }

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

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }
    }
}
