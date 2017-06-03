namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AM31
    {
        public AM31()
        {
            AD31 = new HashSet<AD31>();
        }

        [Key]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_kh { get; set; }

        public byte tk_cn { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_nx { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal so_hd_gtgt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal han_tt { get; set; }

        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        public string ma_kh2 { get; set; }

        [StringLength(16)]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        public string ma_ud3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        public string ma_ud1 { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(8)]
        public string ma_nvien { get; set; }

        [StringLength(2)]
        public string ma_httt { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        public virtual ICollection<AD31> AD31 { get; set; }
    }
}
