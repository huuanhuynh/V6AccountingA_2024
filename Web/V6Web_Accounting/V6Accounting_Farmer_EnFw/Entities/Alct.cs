namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Alct")]
    public partial class Alct
    {
        [Required]
        [StringLength(1)]
        public string Module_id { get; set; }

        [Required]
        [StringLength(12)]
        public string ma_phan_he { get; set; }

        [Key]
        [StringLength(3)]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ct { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_ct2 { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct_me { get; set; }

        [Column(TypeName = "numeric")]
        public decimal so_ct { get; set; }

        [StringLength(3)]
        public string m_ma_nk { get; set; }

        [StringLength(1)]
        public string m_ma_gd { get; set; }

        public byte? m_ma_td { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_nt { get; set; }

        [Required]
        [StringLength(32)]
        public string tieu_de_ct { get; set; }

        [Required]
        [StringLength(32)]
        public string tieu_de2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal so_lien { get; set; }

        [Required]
        [StringLength(3)]
        public string ma_ct_in { get; set; }

        [Required]
        [StringLength(8)]
        public string form { get; set; }

        public byte stt_ct_nkc { get; set; }

        public byte stt_ctntxt { get; set; }

        public byte? ct_nxt { get; set; }

        [Required]
        [StringLength(6)]
        public string m_phdbf { get; set; }

        [Required]
        [StringLength(6)]
        public string m_ctdbf { get; set; }

        [Required]
        [StringLength(1)]
        public string m_status { get; set; }

        public byte post_type { get; set; }

        public byte m_sl_ct0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal m_trung_so { get; set; }

        public byte m_loc_nsd { get; set; }

        public byte m_ong_ba { get; set; }

        public byte m_ngay_ct { get; set; }

        [Required]
        [StringLength(100)]
        public string procedur { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        public byte? stt { get; set; }

        [StringLength(128)]
        public string m_ma_td2 { get; set; }

        [StringLength(128)]
        public string m_ma_td3 { get; set; }

        [StringLength(128)]
        public string m_ngay_td1 { get; set; }

        [StringLength(128)]
        public string m_sl_td1 { get; set; }

        [StringLength(128)]
        public string m_sl_td2 { get; set; }

        [StringLength(128)]
        public string m_sl_td3 { get; set; }

        [StringLength(128)]
        public string m_gc_td1 { get; set; }

        [StringLength(128)]
        public string m_gc_td2 { get; set; }

        [StringLength(128)]
        public string m_gc_td3 { get; set; }

        public byte? post2 { get; set; }

        public byte? post3 { get; set; }

        [StringLength(128)]
        public string m_ngay_td2 { get; set; }

        [StringLength(128)]
        public string m_ngay_td3 { get; set; }

        [StringLength(10)]
        public string dk_ctgs { get; set; }

        public byte? kh_yn { get; set; }

        public byte? cc_yn { get; set; }

        public byte? nv_yn { get; set; }

        [StringLength(3)]
        public string ma_ct_old { get; set; }

        [StringLength(10)]
        public string m_ph_old { get; set; }

        public byte? m_bp_bh { get; set; }

        public byte? m_ma_nvien { get; set; }

        public byte? m_ma_vv { get; set; }

        [StringLength(128)]
        public string m_ma_hd { get; set; }

        [StringLength(128)]
        public string m_ma_ku { get; set; }

        [StringLength(128)]
        public string m_ma_phi { get; set; }

        [StringLength(128)]
        public string m_ma_vitri { get; set; }

        [StringLength(128)]
        public string m_ma_lo { get; set; }

        [StringLength(128)]
        public string m_ma_bpht { get; set; }

        [StringLength(128)]
        public string m_ma_sp { get; set; }

        [StringLength(1)]
        public string m_k_post { get; set; }

        [Required]
        [StringLength(16)]
        public string Tk_no { get; set; }

        [Required]
        [StringLength(16)]
        public string Tk_co { get; set; }

        [StringLength(128)]
        public string M_MA_LNX { get; set; }

        [StringLength(128)]
        public string M_HSD { get; set; }

        public byte m_ma_sonb { get; set; }

        public byte m_sxoa_nsd { get; set; }

        [Required]
        [StringLength(12)]
        public string SIZE_CT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal THEM_IN { get; set; }

        [Required]
        [StringLength(12)]
        public string phandau { get; set; }

        [Required]
        [StringLength(12)]
        public string phancuoi { get; set; }

        [Required]
        [StringLength(12)]
        public string dinhdang { get; set; }

        [Required]
        [StringLength(128)]
        public string M_Ma_KMB { get; set; }

        [Required]
        [StringLength(128)]
        public string M_Ma_KMM { get; set; }

        [Required]
        [StringLength(128)]
        public string M_SO_LSX { get; set; }

        [Required]
        [StringLength(128)]
        public string M_MA_KHO2 { get; set; }

        [Required]
        [StringLength(1)]
        public string F6BARCODE { get; set; }
    }

}
