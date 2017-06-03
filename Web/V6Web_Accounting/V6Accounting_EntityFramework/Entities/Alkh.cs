namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alkh")]
    public partial class Alkh
    {
        [Key]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [Required]
        [StringLength(128)]
        public string ten_kh { get; set; }

        [StringLength(128)]
        public string ten_kh2 { get; set; }

        [Required]
        [StringLength(18)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(32)]
        public string dien_thoai { get; set; }

        [Required]
        [StringLength(16)]
        public string fax { get; set; }

        [Required]
        [StringLength(16)]
        public string e_mail { get; set; }

        [Required]
        [StringLength(32)]
        public string home_page { get; set; }

        [Required]
        [StringLength(32)]
        public string doi_tac { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(32)]
        public string ten_bp { get; set; }

        [Required]
        [StringLength(64)]
        public string ngan_hang { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string ghi_chu { get; set; }

        public byte han_tt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_kh1 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_kh2 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_kh3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_nt13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_cn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien_hd { get; set; }

        [Required]
        [StringLength(8)]
        public string Ma_httt { get; set; }

        [Required]
        [StringLength(8)]
        public string Nh_kh9 { get; set; }

        [Required]
        [StringLength(8)]
        public string Ma_snvien { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Ngay_gh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

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

        public byte? kh_yn { get; set; }

        public byte? cc_yn { get; set; }

        public byte? nv_yn { get; set; }

        [StringLength(24)]
        public string TK_NH { get; set; }

        [Required]
        [StringLength(20)]
        public string DT_DD { get; set; }

        [Required]
        [StringLength(100)]
        public string TT_SONHA { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_PHUONG { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_TINH { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_QUAN { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }
}
