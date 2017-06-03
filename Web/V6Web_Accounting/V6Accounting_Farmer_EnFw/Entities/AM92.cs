namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AM92
    {
        public AM92()
        {
            AD92 = new HashSet<AD92>();
        }

        [Key]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(16)]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nk { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

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
        public decimal t_tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        public byte han_tt { get; set; }

        public byte so_hd_gtgt { get; set; }

        public byte loai_hd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt { get; set; }

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
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [StringLength(16)]
        public string ma_kh2 { get; set; }

        [StringLength(128)]
        public string so_ct_tt { get; set; }

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

        [Required]
        [StringLength(1)]
        public string K_V { get; set; }

        [StringLength(8)]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tso_luong1 { get; set; }

        [StringLength(2)]
        public string ma_httt { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_CT1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_CT2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NGAY_TRT { get; set; }

        public virtual ICollection<AD92> AD92 { get; set; }
    }

}
