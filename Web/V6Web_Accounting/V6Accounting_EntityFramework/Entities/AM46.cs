namespace V6Accounting_EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AM46
    {
        public AM46()
        {
            AD46 = new HashSet<AD46>();
        }

        [Key]
        [StringLength(13)]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        public string so_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_kh { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [StringLength(32)]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nk { get; set; }

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
        public decimal t_tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Required]
        [StringLength(128)]
        public string so_ct_tt { get; set; }

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
        [StringLength(2)]
        public string loai_ct { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        [StringLength(8)]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        [StringLength(13)]
        public string stt_rec_kt { get; set; }

        public virtual ICollection<AD46> AD46 { get; set; }
    }
}
