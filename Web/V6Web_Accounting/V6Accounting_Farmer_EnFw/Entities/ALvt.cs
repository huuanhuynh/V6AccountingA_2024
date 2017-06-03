namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALvt")]
    public partial class ALvt
    {
        [Key]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Required]
        [StringLength(16)]
        public string part_no { get; set; }

        [Required]
        [StringLength(64)]
        public string ten_vt { get; set; }

        [Required]
        [StringLength(64)]
        public string ten_vt2 { get; set; }

        [Required]
        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(10)]
        public string dvt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        public byte vt_ton_kho { get; set; }

        public byte gia_ton { get; set; }

        public byte? sua_tk_vt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_cl_vt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_vt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_gv { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_dt { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_tl { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_spdd { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vt1 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vt2 { get; set; }

        [Required]
        [StringLength(8)]
        public string nh_vt3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sl_min { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sl_max { get; set; }

        [Column(TypeName = "text")]
        public string ghi_chu { get; set; }

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

        [StringLength(30)]
        public string Short_name { get; set; }

        [StringLength(20)]
        public string Bar_code { get; set; }

        [StringLength(2)]
        public string Loai_vt { get; set; }

        [StringLength(8)]
        public string Tt_vt { get; set; }

        [StringLength(1)]
        public string Nhieu_dvt { get; set; }

        [StringLength(1)]
        public string Lo_yn { get; set; }

        [StringLength(1)]
        public string Kk_yn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight { get; set; }

        [StringLength(10)]
        public string DvtWeight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight0 { get; set; }

        [StringLength(10)]
        public string DvtWeight0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Length { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Width { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Height { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Diamet { get; set; }

        [StringLength(10)]
        public string DvtLength { get; set; }

        [StringLength(10)]
        public string DvtWidth { get; set; }

        [StringLength(10)]
        public string DvtHeight { get; set; }

        [StringLength(10)]
        public string DvtDiamet { get; set; }

        [StringLength(16)]
        public string Size { get; set; }

        [StringLength(16)]
        public string Color { get; set; }

        [StringLength(16)]
        public string Style { get; set; }

        [StringLength(8)]
        public string Ma_qg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Packs { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Packs1 { get; set; }

        [StringLength(1)]
        public string abc_code { get; set; }

        [StringLength(10)]
        public string Dvtpacks { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Cycle_kk { get; set; }

        [StringLength(8)]
        public string Ma_vitri { get; set; }

        [StringLength(8)]
        public string Ma_kho { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Han_sd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Han_bh { get; set; }

        [StringLength(1)]
        public string Kieu_lo { get; set; }

        [StringLength(1)]
        public string Cach_xuat { get; set; }

        [StringLength(8)]
        public string Lma_nvien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LdatePur { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LdateQc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Lso_qty { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Lso_qtymin { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Lso_qtymax { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LCycle { get; set; }

        [StringLength(1)]
        public string Lpolicy { get; set; }

        [StringLength(8)]
        public string Pma_nvien { get; set; }

        [StringLength(8)]
        public string Pma_khc { get; set; }

        [StringLength(8)]
        public string Pma_khp { get; set; }

        [StringLength(8)]
        public string Pma_khl { get; set; }

        [StringLength(128)]
        public string Prating { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Pquality { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Pquanlity { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Pdeliver { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PFlex { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Ptech { get; set; }

        [StringLength(8)]
        public string nh_vt9 { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [StringLength(8)]
        public string ma_thueNk { get; set; }

        [StringLength(16)]
        public string tk_ck { get; set; }

        [StringLength(1)]
        public string date_yn { get; set; }

        [Required]
        [StringLength(16)]
        public string TK_CP { get; set; }

        [Required]
        [StringLength(8)]
        public string MA_BPHT { get; set; }

        [StringLength(1)]
        public string VITRI_YN { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_VTTG { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_KHTG { get; set; }

        [Required]
        [StringLength(64)]
        public string TEN_KHTG { get; set; }

        [Required]
        [StringLength(48)]
        public string TEN_QG { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Thue_suat { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_VT4 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_VT5 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_VT6 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_VT7 { get; set; }

        [Required]
        [StringLength(8)]
        public string NH_VT8 { get; set; }

        [Required]
        [StringLength(100)]
        public string MODEL { get; set; }

        [Required]
        [StringLength(16)]
        public string MA_VV { get; set; }

        [Required]
        [StringLength(100)]
        public string CHECK_SYNC { get; set; }
    }

}
