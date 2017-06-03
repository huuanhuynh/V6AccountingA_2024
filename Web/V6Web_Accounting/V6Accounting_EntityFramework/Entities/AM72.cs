namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AM72
    {
        public AM72()
        {
            AD72 = new HashSet<AD72>();
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

        [Column(TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_nk_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_nk { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_nk { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_no { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_thue_co { get; set; }

        public byte cp_thue_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_thue { get; set; }

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

        [StringLength(8)]
        public string ms_kh2 { get; set; }

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

        [StringLength(2)]
        public string ma_httt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        public virtual ICollection<AD72> AD72 { get; set; }
    }
}
