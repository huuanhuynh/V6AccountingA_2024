namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alhd")]
    public partial class Alhd
    {
        [Key]
        [StringLength(16)]
        public string ma_hd { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_hd { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_hd2 { get; set; }

        [StringLength(16)]
        public string ma_kh { get; set; }

        [StringLength(8)]
        public string ma_nvbh { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(1)]
        public string loai_hd { get; set; }

        [StringLength(8)]
        public string nh_hd1 { get; set; }

        [StringLength(8)]
        public string nh_hd2 { get; set; }

        [StringLength(8)]
        public string nh_hd3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hd { get; set; }

        [StringLength(20)]
        public string so_hd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hd1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hd2 { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_gt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_gt { get; set; }

        public byte? tinh_trang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kl_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kl_th { get; set; }

        [Column(TypeName = "ntext")]
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

        [StringLength(1)]
        public string CT { get; set; }
    }

}
