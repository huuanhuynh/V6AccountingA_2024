namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;
    using global::System.Data.Entity.Spatial;

    [Table("Abbpkh")]
    public partial class Abbpkh
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("Abhd")]
    public partial class Abhd
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_hd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("Abhdkh")]
    public partial class Abhdkh
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_hd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ABkh")]
    public partial class ABkh
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 9)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 14)]
        public byte user_id2 { get; set; }
    }


    [Table("Abkhovvkh")]
    public partial class Abkhovvkh
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    [Table("Abku")]
    public partial class Abku
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [StringLength(1)]
        public string status { get; set; }
    }


    [Table("ABlkct")]
    public partial class ABlkct
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal z_lk { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal z_lk_nt { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal dt_lk { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal dt_lk_nt { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal user_id2 { get; set; }
    }


    [Table("ABlo")]
    public partial class ABlo
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string ma_vitri { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_lo { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ton00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }
    }


    [Table("ABntxt")]
    public partial class ABntxt
    {
        [Column(TypeName = "numeric")]
        public decimal? nam { get; set; }

        [StringLength(13)]
        public string stt_rec_nt { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        public byte? pn_co_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ngay { get; set; }

        [StringLength(3)]
        public string ma_ct { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? stt_ctntxt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [StringLength(8)]
        public string ma_kho { get; set; }

        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cp_nt { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ton_kho01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt01 { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ton_kho02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du02 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt02 { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ton_kho03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du03 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt03 { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ton_kho04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du04 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt04 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ton_kho05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt05 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ton_kho06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt06 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ton_kho07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt07 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ton_kho08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt08 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ton_kho09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt09 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ton_kho10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du10 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt10 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ton_kho11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du11 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt11 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal ton_kho12 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du12 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt12 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal ton_kho13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du13 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_du_nt13 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        public byte? user_id2 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }
    }


    [Table("Abphi")]
    public partial class Abphi
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ABSPDD")]
    public partial class ABSPDD
    {
        [Column(TypeName = "numeric")]
        public decimal? nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Thang { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string so_lsx { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal sl_dd { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal tl_ht { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal sl_qd { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal sl_nk { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal sl_sx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_dd_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_dd { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        public string time0 { get; set; }

        public byte? user_id0 { get; set; }

        [StringLength(1)]
        public string status { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("AbTD")]
    public partial class AbTD
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_TD { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    public partial class AbTD2
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_TD2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    public partial class AbTD3
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_TD3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ABtk")]
    public partial class ABtk
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal du_no1 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal du_co1 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal du_no_nt1 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal du_co_nt1 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal user_id2 { get; set; }
    }


    [Table("ABtknt")]
    public partial class ABtknt
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal du_no1 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal du_co1 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal du_no_nt1 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal du_co_nt1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal user_id2 { get; set; }
    }


    [Table("ABvitri")]
    public partial class ABvitri
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string ma_vitri { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ton00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [Required]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }
    }


    [Table("ABvt")]
    public partial class ABvt
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ton00 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du00 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_nt00 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string Ma_Dvcs { get; set; }
    }


    public partial class ABvt13
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ton13 { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du13 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_nt13 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }
    }


    [Table("ABvv")]
    public partial class ABvv
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("Abvvkh")]
    public partial class Abvvkh
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_no_nt00 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal du_co_nt00 { get; set; }

        [StringLength(128)]
        public string dien_giai { get; set; }

        [StringLength(3)]
        public string ma_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }

        [Required]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }
    }


    [Table("ACKU")]
    public partial class ACKU
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_KU { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal lk_no { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal lk_co { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal lk_no_nt { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal lk_co_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ACvv")]
    public partial class ACvv
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_vv { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal lk_no { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal lk_co { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal lk_no_nt { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal lk_co_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        public string time2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    public partial class AD11
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte tk_cn_i { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(5)]
        public string nh_dk { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM11 AM11 { get; set; }
    }


    public partial class AD21
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk_dt { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        //[Key]
        [Column(Order = 9)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal ck { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal tt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        [StringLength(8)]
        public string ma_thue_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suati { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(16)]
        public string ma_kh2_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        //[Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal Tien { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string Ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(13)]
        public string STT_REC_TT { get; set; }
    }


    public partial class AD29
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte tk_cn_i { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(5)]
        public string nh_dk { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM29 AM29 { get; set; }
    }


    public partial class AD31
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM31 AM31 { get; set; }
    }


    public partial class AD32
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM32 AM32 { get; set; }
    }


    public partial class AD39
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte tk_cn_i { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(5)]
        public string nh_dk { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM39 AM39 { get; set; }
    }

    
    public partial class AD41
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal ty_gia_ht2 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal Tien { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal TIEN_QD { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal TY_GIAQD { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM41 AM41 { get; set; }
    }


    public partial class AD42
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        public virtual AM42 AM42 { get; set; }
    }


    public partial class AD46
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        [StringLength(8)]
        public string ma_thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tt_nt { get; set; }

        [StringLength(12)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal ty_gia_ht2 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal Tien { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM46 AM46 { get; set; }
    }

    public partial class AD47
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        public virtual AM47 AM47 { get; set; }
    }


    public partial class AD51
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tt { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal ty_gia_ht2 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal Tien { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        //[Key]
        [Column(Order = 30)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM51 AM51 { get; set; }
    }


    public partial class AD52
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        public virtual AM52 AM52 { get; set; }
    }


    public partial class AD56
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_kh_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(13)]
        public string stt_rec_tt { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(12)]
        public string so_seri0 { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(12)]
        public string so_ct0 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tt { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(5)]
        public string stt_rec0 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal ty_gia_ht2 { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal Tien { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal Tien_nt { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string TT_SOKHUNG { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string TT_SOMAY { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string ma_spph { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string ma_td2ph { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(16)]
        public string ma_td3ph { get; set; }

        //[Key]
        [Column(Order = 30)]
        [StringLength(8)]
        public string MA_KHO2 { get; set; }

        public virtual AM56 AM56 { get; set; }
    }


    public partial class AD57
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        public string dien_giaii { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string tk_i { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal ps_no_nt { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        public virtual AM57 AM57 { get; set; }
    }


    public partial class AD71
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal tien_hg_nt { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal tien_hg { get; set; }

        //[Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        //[Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal cp { get; set; }

        //[Key]
        [Column(Order = 27, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 28, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal CK_NT { get; set; }

        //[Key]
        [Column(Order = 31, TypeName = "numeric")]
        public decimal CK { get; set; }

        //[Key]
        [Column(Order = 32, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        //[Key]
        [Column(Order = 33)]
        [StringLength(1)]
        public string Ck_vat_i { get; set; }

        //[Key]
        [Column(Order = 34, TypeName = "numeric")]
        public decimal Thue_suat { get; set; }

        //[Key]
        [Column(Order = 35, TypeName = "numeric")]
        public decimal Gg_nt { get; set; }

        //[Key]
        [Column(Order = 36, TypeName = "numeric")]
        public decimal Gg { get; set; }

        //[Key]
        [Column(Order = 37)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 38)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 39)]
        public string so_image { get; set; }

        //[Key]
        [Column(Order = 40)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM71 AM71 { get; set; }
    }


    public partial class AD72
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal tien_hg_nt { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal tien_hg { get; set; }

        //[Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        //[Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal cp { get; set; }

        //[Key]
        [Column(Order = 27, TypeName = "numeric")]
        public decimal nk_nt { get; set; }

        //[Key]
        [Column(Order = 28, TypeName = "numeric")]
        public decimal nk { get; set; }

        //[Key]
        [Column(Order = 29, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        //[Key]
        [Column(Order = 31)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 32)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 33)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 34)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM72 AM72 { get; set; }
    }


    public partial class AD73
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal tien_hg_nt { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal tien_hg { get; set; }

        //[Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        //[Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal cp { get; set; }

        //[Key]
        [Column(Order = 27, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 28, TypeName = "numeric")]
        public decimal thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(13)]
        public string STT_REC_PN { get; set; }

        [StringLength(5)]
        public string STT_REC0PN { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 30)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM73 AM73 { get; set; }
    }


    public partial class AD74
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal tien { get; set; }

        [StringLength(13)]
        public string stt_rec_px { get; set; }

        [StringLength(5)]
        public string stt_rec0px { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 27)]
        public string so_image { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM74 AM74 { get; set; }
    }


    public partial class AD76
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string tk_tl { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(13)]
        public string stt_rec_hd { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(5)]
        public string stt_rec0hd { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt21 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia21 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 29, TypeName = "numeric")]
        public decimal Pt_cki { get; set; }

        //[Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal Ck_nt { get; set; }

        //[Key]
        [Column(Order = 31, TypeName = "numeric")]
        public decimal Ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Gg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien1_Nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Tien1 { get; set; }

        //[Key]
        [Column(Order = 32)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 33)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 34)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM76 AM76 { get; set; }
    }


    public partial class AD81
    {
        [Key]        
        [StringLength(13)]
        [Column("stt_rec", Order = 0)]
        public string SttRec { get; set; }
     
        [StringLength(5)]
        [Column("stt_rec0", Order = 1)]
        public string SttRec0 { get; set; }

        [StringLength(3)]
        [Column("ma_ct", Order = 2)]
        public string MaCt { get; set; }

        [Column("ngay_ct", Order = 3, TypeName = "smalldatetime")]
        public DateTime NgayCt { get; set; }

        [StringLength(12)]
        [Column("so_ct", Order = 4)]
        public string SoCt { get; set; }

        [StringLength(8)]
        [Column("ma_kho_i", Order = 5)]
        public string MaKhoI { get; set; }

        [StringLength(16)]
        [Column("ma_vv_i", Order = 6)]
        public string MaVvI { get; set; }

        [StringLength(16)]
        [Column("ma_td_i", Order = 7)]
        public string MaTdI { get; set; }

        [StringLength(16)]
        [Column("ma_vt", Order = 8)]
        public string MaVt { get; set; }

        [StringLength(10)]
        [Column("dvt1", Order = 9)]
        public string Dvt1 { get; set; }

        [Column("he_so1", Order = 10, TypeName = "numeric")]
        public decimal HeSo1 { get; set; }

        [Column("so_luong1", Order = 11, TypeName = "numeric")]
        public decimal SoLuong1 { get; set; }

        [Column("so_luong", Order = 12, TypeName = "numeric")]
        public decimal SoLuong { get; set; }
                
        [Column("gia_nt", Order = 13, TypeName = "numeric")]
        public decimal GiaNt { get; set; }
                
        [Column("gia", Order = 14, TypeName = "numeric")]
        public decimal Gia { get; set; }
                
        [Column("tien_nt", Order = 15, TypeName = "numeric")]
        public decimal TienNt { get; set; }
                
        [Column("tien", Order = 16, TypeName = "numeric")]
        public decimal Tien { get; set; }
                
        [Column("gia_nt2", Order = 17, TypeName = "numeric")]
        public decimal GiaNt2 { get; set; }
                
        [Column("gia2", Order = 18, TypeName = "numeric")]
        public decimal Gia2 { get; set; }
                
        [Column("tien_nt2", Order = 19, TypeName = "numeric")]
        public decimal TienNt2 { get; set; }
                
        [Column("tien2", Order = 20, TypeName = "numeric")]
        public decimal Tien2 { get; set; }
                        
        [Column("thue", Order = 21, TypeName = "numeric")]
        public decimal Thue { get; set; }

        [Column("thue_nt", Order = 22, TypeName = "numeric")]
        public decimal ThueNt { get; set; }

        [Column("ck", Order = 23, TypeName = "numeric")]
        public decimal Ck { get; set; }
                
        [Column("ck_nt", Order = 24, TypeName = "numeric")]
        public decimal CkNt { get; set; }

        [StringLength(16)]
        [Column("tk_vt", Order = 25)]
        public string TkVt { get; set; }

        [StringLength(16)]
        [Column("tk_gv", Order = 26)]
        public string TkGv { get; set; }
                
        [StringLength(16)]
        [Column("tk_dt", Order = 27)]
        public string TkDt { get; set; }
                
        [StringLength(13)]
        [Column("stt_rec_pn", Order = 28)]
        public string SttRecPn { get; set; }

        [StringLength(5)]
        [Column("stt_rec0pn", Order = 29)]
        public string SttRec0Pn { get; set; }
                
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTd2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTd3 { get; set; }
                
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTd1 { get; set; }
                
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SlTd1 { get; set; }
                
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SlTd2 { get; set; }
                
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SlTd3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GcTd1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GcTd2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GcTd3 { get; set; }

        
        [Column("ngay_td2")]
        public DateTime? NgayTd2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTd3 { get; set; }

        [StringLength(16)]
        [Column("tk_thue_i")]
        public string TkThueI { get; set; }

        [StringLength(1)]
        [Column("px_gia_ddi")]
        public string PxGiaDdi { get; set; }

        [StringLength(13)]
        [Column("stt_recdh")]
        public string SttRecdh { get; set; }

        [StringLength(5)]
        [Column("stt_rec0dh")]
        public string SttRec0Dh { get; set; }

        [StringLength(8)]
        [Column("ma_bpht")]
        public string MaBpht { get; set; }

        [StringLength(16)]
        [Column("ma_sp")]
        public string MaSp { get; set; }

        [StringLength(1)]
        [Column("tang")]
        public string Tang { get; set; }

        [StringLength(16)]
        [Column("ma_hd")]
        public string MaHd { get; set; }

        [StringLength(16)]
        [Column("ma_ku")]
        public string MaKu { get; set; }

        [StringLength(16)]
        [Column("ma_phi")]
        public string MaPhi { get; set; }

        [StringLength(16)]
        [Column("ma_lo")]
        public string MaLo { get; set; }

        [StringLength(8)]
        [Column("ma_vitri")]
        public string MaViTri { get; set; }

        [StringLength(10)]
        [Column("dvt")]
        public string Dvt { get; set; }

        
        [Column("gia_nt1", TypeName = "numeric")]
        public decimal? GiaNt1 { get; set; }

        
        [Column("gia1", TypeName = "numeric")]
        public decimal? Gia1 { get; set; }

        
        [Column("gia_nt21", TypeName = "numeric")]
        public decimal? GiaNt21 { get; set; }

        
        [Column("gia21", TypeName = "numeric")]
        public decimal? Gia21 { get; set; }

        
        [Column("gia_ban_nt", TypeName = "numeric")]
        public decimal? GiaBanNt { get; set; }

        
        [Column("gia_ban", TypeName = "numeric")]
        public decimal? GiaBan { get; set; }

        [StringLength(16)]
        [Column("tk_cki")]
        public string TkCki { get; set; }

        [Column("PT_CKI", Order = 30, TypeName = "numeric")]
        public decimal PtCki { get; set; }

        [Column("TIEN1", Order = 31, TypeName = "numeric")]
        public decimal Tien1 { get; set; }

        [Column("TIEN1_NT", Order = 32, TypeName = "numeric")]
        public decimal Tien1Nt { get; set; }

        [StringLength(8)]
        [Column("MA_LNX_I", Order = 33)]
        public string MaLnxI { get; set; }

        
        [Column("HSD", TypeName = "smalldatetime")]
        public DateTime? Hsd { get; set; }

        [Column("gg_nt", Order = 34, TypeName = "numeric")]
        public decimal GgNt { get; set; }

        [Column("gg", Order = 35, TypeName = "numeric")]
        public decimal Gg { get; set; }

        [StringLength(8)]
        [Column("Ma_gia")]
        public string MaGia { get; set; }

        [Column("GIA_NT4", Order = 36, TypeName = "numeric")]
        public decimal GiaNt4 { get; set; }

        [Column("GIA4", Order = 37, TypeName = "numeric")]
        public decimal Gia4 { get; set; }

        [Column("TIEN_NT4", Order = 38, TypeName = "numeric")]
        public decimal TienNt4 { get; set; }

        [Column("TIEN4", Order = 39, TypeName = "numeric")]
        public decimal Tien4 { get; set; }

        [StringLength(20)]
        [Column("SO_KHUNG", Order = 40)]
        public string SoKhung { get; set; }

        [StringLength(20)]
        [Column("SO_MAY", Order = 41)]
        public string SoMay { get; set; }

        [Column("so_image", Order = 42)]
        public string SoImage { get; set; }

        [StringLength(1)]
        [Column("Status_DPI", Order = 43)]
        public string StatusDpi { get; set; }

        [StringLength(16)]
        [Column("SO_LSX", Order = 44)]
        public string SoLsx { get; set; }

        [Column("AM81")]
        public virtual AM81 AM81 { get; set; }
    }


    public partial class AD81CT
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string ma_loai { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string tt_vt { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(100)]
        public string dg_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_01 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_02 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_03 { get; set; }

        [StringLength(20)]
        public string char_01 { get; set; }

        [StringLength(20)]
        public string char_02 { get; set; }

        [StringLength(20)]
        public string char_03 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal num_01 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal num_02 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal num_03 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 13)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 18)]
        public byte user_id2 { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(60)]
        public string NGUOI_SD1 { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(40)]
        public string DIENTHOAI1 { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(40)]
        public string DT_DD1 { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(20)]
        public string TT_SOXE1 { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(4)]
        public string TT_NAMNU1 { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string MA_TD2 { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(16)]
        public string MA_TD3 { get; set; }
    }


    public partial class AD81CT0
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [StringLength(100)]
        public string dg_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_stat { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_mua { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong1 { get; set; }

        [StringLength(128)]
        public string dia_chi { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_01 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_02 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_03 { get; set; }

        [StringLength(20)]
        public string char_01 { get; set; }

        [StringLength(20)]
        public string char_02 { get; set; }

        [StringLength(20)]
        public string char_03 { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal num_01 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal num_02 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal num_03 { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 16)]
        public byte user_id2 { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(60)]
        public string NGUOI_SD1 { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(40)]
        public string DIENTHOAI1 { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(40)]
        public string DT_DD1 { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(20)]
        public string TT_SOXE1 { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(4)]
        public string TT_NAMNU1 { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 24)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string MA_TD2 { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(16)]
        public string MA_TD3 { get; set; }
    }


    public partial class AD84
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(1)]
        public string px_gia_ddi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 22)]
        public string so_image { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM84 AM84 { get; set; }
    }


    public partial class AD85
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien { get; set; }

        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string px_gia_ddi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(8)]
        public string ma_vitrin { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(8)]
        public string MA_LNXN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 22)]
        public string so_image { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM85 AM85 { get; set; }
    }


    public partial class AD86
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string px_gia_ddi { get; set; }

        [StringLength(13)]
        public string stt_recdh { get; set; }

        [StringLength(5)]
        public string stt_rec0dh { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(8)]
        public string MA_LNX_I { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal Pt_cki { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal Ck_nt { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal Ck { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal Gg_nt { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal Gg { get; set; }

        //[Key]
        [Column(Order = 25, TypeName = "numeric")]
        public decimal Tien1_Nt { get; set; }

        //[Key]
        [Column(Order = 26, TypeName = "numeric")]
        public decimal Tien1 { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(20)]
        public string SO_KHUNG { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(20)]
        public string SO_MAY { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(16)]
        public string SO_LSX { get; set; }

        public virtual AM86 AM86 { get; set; }
    }


    public partial class AD91
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(5)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal thue { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal ck { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        //[Key]
        [Column(Order = 25)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 26)]
        [StringLength(16)]
        public string tk_gv { get; set; }

        //[Key]
        [Column(Order = 27)]
        [StringLength(16)]
        public string tk_dt { get; set; }

        //[Key]
        [Column(Order = 28)]
        [StringLength(13)]
        public string stt_rec_pn { get; set; }

        //[Key]
        [Column(Order = 29)]
        [StringLength(5)]
        public string stt_rec0pn { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        //[Key]
        [Column(Order = 30, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        //[Key]
        [Column(Order = 31, TypeName = "numeric")]
        public decimal TIEN1 { get; set; }

        //[Key]
        [Column(Order = 32, TypeName = "numeric")]
        public decimal TIEN1_NT { get; set; }

        //[Key]
        [Column(Order = 33)]
        [StringLength(10)]
        public string dvt { get; set; }

        //[Key]
        [Column(Order = 34, TypeName = "numeric")]
        public decimal gia_nt1 { get; set; }

        //[Key]
        [Column(Order = 35, TypeName = "numeric")]
        public decimal gia1 { get; set; }

        //[Key]
        [Column(Order = 36, TypeName = "numeric")]
        public decimal gia_nt21 { get; set; }

        //[Key]
        [Column(Order = 37, TypeName = "numeric")]
        public decimal gia21 { get; set; }

        //[Key]
        [Column(Order = 38, TypeName = "numeric")]
        public decimal gia_ban_nt { get; set; }

        //[Key]
        [Column(Order = 39, TypeName = "numeric")]
        public decimal gia_ban { get; set; }

        //[Key]
        [Column(Order = 40)]
        [StringLength(16)]
        public string tk_cki { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        public virtual AM91 AM91 { get; set; }
    }


    public partial class AD92
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string stt_rec { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string stt_rec0 { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string ma_ct { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string so_ct { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_kho_i { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(16)]
        public string ma_nx_i { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(10)]
        public string dvt1 { get; set; }

        //[Key]
        [Column(Order = 11, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal so_luong1 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string tk_vt { get; set; }

        //[Key]
        [Column(Order = 14, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //[Key]
        [Column(Order = 15, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal gia { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal tien { get; set; }

        //[Key]
        [Column(Order = 19, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //[Key]
        [Column(Order = 21, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //[Key]
        [Column(Order = 22, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //[Key]
        [Column(Order = 23, TypeName = "numeric")]
        public decimal tien_hg_nt { get; set; }

        //[Key]
        [Column(Order = 24, TypeName = "numeric")]
        public decimal tien_hg { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

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

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        public string tk_thue_i { get; set; }

        [StringLength(1)]
        public string pn_gia_tbi { get; set; }

        [StringLength(8)]
        public string ma_bpht { get; set; }

        [StringLength(16)]
        public string ma_sp { get; set; }

        [StringLength(16)]
        public string ma_hd { get; set; }

        [StringLength(16)]
        public string ma_ku { get; set; }

        [StringLength(16)]
        public string ma_phi { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        [StringLength(8)]
        public string ma_vitri { get; set; }

        [StringLength(16)]
        public string ma_lo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        public virtual AM92 AM92 { get; set; }
    }


    [Table("ADALCC")]
    public partial class ADALCC
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal Cc0 { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string so_the_Cc { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_gs { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal Tang_giam { get; set; }

        [StringLength(8)]
        public string ma_tg_Cc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_pb_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal so_ky { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 14)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADALTS")]
    public partial class ADALT
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal Ts0 { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_gs { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal Tang_giam { get; set; }

        [StringLength(8)]
        public string ma_tg_ts { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_kh_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal so_ky { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 10)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 12, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 14)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADBPCC")]
    public partial class ADBPCC
    {
        [Column(TypeName = "numeric")]
        public decimal? CC0 { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string so_the_CC { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_CC { get; set; }

        [StringLength(16)]
        public string tk_PB { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADBPTS")]
    public partial class ADBPT
    {
        [Column(TypeName = "numeric")]
        public decimal? Ts0 { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_ts { get; set; }

        [StringLength(16)]
        public string tk_kh { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADCTCC")]
    public partial class ADCTCC
    {
        [Column(TypeName = "numeric")]
        public decimal? CC0 { get; set; }

        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_CC { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(48)]
        public string ten_ptkt { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_tri { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADCTCCBP")]
    public partial class ADCTCCBP
    {
        [Column(TypeName = "numeric")]
        public decimal? CC0 { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string so_the_CC { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_bp_i { get; set; }

        [StringLength(16)]
        public string tk_pb_i { get; set; }

        [StringLength(16)]
        public string tk_cp_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? He_so { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }
    }


    [Table("ADCTTS")]
    public partial class ADCTT
    {
        [Column(TypeName = "numeric")]
        public decimal? Ts0 { get; set; }

        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(48)]
        public string ten_ptkt { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gia_tri { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADCTTSBP")]
    public partial class ADCTTSBP
    {
        [Column(TypeName = "numeric")]
        public decimal? Ts0 { get; set; }

        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? line_nbr { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_bp_i { get; set; }

        [StringLength(16)]
        public string tk_kh_i { get; set; }

        [StringLength(16)]
        public string tk_cp_i { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? He_so { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_phi { get; set; }
    }


    [Table("ADHSCC")]
    public partial class ADHSCC
    {
        [Column(TypeName = "numeric")]
        public decimal? cc0 { get; set; }

        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_cc { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam { get; set; }

        [StringLength(16)]
        public string tk_pb { get; set; }

        [StringLength(16)]
        public string tk_cc { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_pb_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_gt_pb_ky { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(16)]
        public string ma_phi { get; set; }

        //[Key]
        [Column(Order = 20)]
        [StringLength(16)]
        public string ma_kh_i { get; set; }
    }


    [Table("ADHSTS")]
    public partial class ADHST
    {
        [Column(TypeName = "numeric")]
        public decimal? Ts0 { get; set; }

        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam { get; set; }

        [StringLength(16)]
        public string tk_kh { get; set; }

        [StringLength(16)]
        public string tk_ts { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_kh_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_he_so { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? T_gt_kh_ky { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string ma_vv_i { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string ma_sp { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(8)]
        public string ma_bpht_i { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(16)]
        public string ma_td_i { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(16)]
        public string ma_td2_i { get; set; }

        //[Key]
        [Column(Order = 17)]
        [StringLength(16)]
        public string ma_td3_i { get; set; }

        //[Key]
        [Column(Order = 18)]
        [StringLength(16)]
        public string ma_ku { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(16)]
        public string ma_phi { get; set; }
    }


    [Table("ADKHTS")]
    public partial class ADKHT
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_kh { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_ts { get; set; }

        [StringLength(16)]
        public string tk_kh { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_kh { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_kh_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADPBCC")]
    public partial class ADPBCC
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_cc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sua_pb { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_nv { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal nam { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_cc { get; set; }

        [StringLength(16)]
        public string tk_pb { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nguyen_gia { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_da_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_tang { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_cl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gt_pb_ky { get; set; }

        [StringLength(64)]
        public string dien_giai { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string status { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 11)]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADSLCC")]
    public partial class ADSLCC
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_cc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_pb { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("ADSLTS")]
    public partial class ADSLT
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string so_the_ts { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal ky { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_kh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    public partial class ADThue43
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(16)]
        public string form { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string stt1 { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_so0 { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(2)]
        public string loai_ct { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(254)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string type { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(10)]
        public string dbf { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(8)]
        public string ma_thue { get; set; }

        //[Key]
        [Column(Order = 13, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(48)]
        public string tk_no { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(48)]
        public string tk_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ds { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ds_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? thue_nt { get; set; }

        //[Key]
        [Column(Order = 16, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 17, TypeName = "numeric")]
        public decimal bold { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }

        //[Key]
        [Column(Order = 18, TypeName = "numeric")]
        public decimal auto { get; set; }

        //[Key]
        [Column(Order = 19)]
        [StringLength(1)]
        public string tag { get; set; }

        //[Key]
        [Column(Order = 20, TypeName = "numeric")]
        public decimal nam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky { get; set; }

        //[Key]
        [Column(Order = 21)]
        [StringLength(3)]
        public string ma_so01 { get; set; }

        //[Key]
        [Column(Order = 22)]
        [StringLength(3)]
        public string ma_so02 { get; set; }

        //[Key]
        [Column(Order = 23)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }
    }


    public partial class Agltc1
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal ts_nv { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal cong_no { get; set; }

        //[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal ngoai_bang { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(100)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(100)]
        public string chi_tieu2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc2
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(48)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(48)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string tk_no { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(48)]
        public string tk_co { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal giam_tru { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truoc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truocnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc3
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(16)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(16)]
        public string tk_du { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_dau { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_cuoi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_dau_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_no_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ps_co_nt0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? du_cuoi_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc4
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string loai_ct { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(254)]
        public string cach_tinh0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        [StringLength(48)]
        public string tk_no { get; set; }

        [StringLength(48)]
        public string tk_co { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? luy_ke_nt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc5
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string tk_no { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(48)]
        public string tk_co { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal dau { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truoc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truocnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_naynt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc6
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal bold { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 7)]
        [StringLength(48)]
        public string tk { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(4)]
        public string loai_ct { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truoc { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_nay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_truocnt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ky_naynt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        public byte? iscal { get; set; }
    }


    public partial class Agltc8
    {
        //[Key]
        [Column(Order = 0, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string ma_so { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(48)]
        public string cach_tinh { get; set; }

        //[Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal stt { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal he_so { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string dvt { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ty_le { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string chi_tieu { get; set; }

        //[Key]
        [Column(Order = 9)]
        [StringLength(64)]
        public string chi_tieu2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(48)]
        public string ten1 { get; set; }

        //[Key]
        [Column(Order = 11)]
        [StringLength(48)]
        public string ten12 { get; set; }

        //[Key]
        [Column(Order = 12)]
        [StringLength(3)]
        public string ma_so1 { get; set; }

        //[Key]
        [Column(Order = 13)]
        [StringLength(2)]
        public string ts_kd1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien1 { get; set; }

        //[Key]
        [Column(Order = 14)]
        [StringLength(48)]
        public string ten2 { get; set; }

        //[Key]
        [Column(Order = 15)]
        [StringLength(48)]
        public string ten22 { get; set; }

        //[Key]
        [Column(Order = 16)]
        [StringLength(3)]
        public string ma_so2 { get; set; }

        [StringLength(2)]
        public string ts_kd2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tb2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ids { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? iscal { get; set; }
    }


    [Table("AKhungCK")]
    public partial class AKhungCK
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string khung_id { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        public string ghi_chu { get; set; }

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
        [StringLength(16)]
        public string Ma_kh { get; set; }
    }


    [Table("AKhungCkCt")]
    public partial class AKhungCkCt
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_dvcs { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string Khung_ID { get; set; }

        //[Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        //[Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(8)]
        public string time0 { get; set; }

        //[Key]
        [Column(Order = 6)]
        public byte user_id0 { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(8)]
        public string time2 { get; set; }

        //[Key]
        [Column(Order = 9)]
        public byte user_id2 { get; set; }

        //[Key]
        [Column(Order = 10)]
        [StringLength(16)]
        public string Ma_kh { get; set; }
    }


    [Table("Albp")]
    public partial class Albp
    {
        [Key]
        [StringLength(16)]
        [Column("ma_bp")]
        public string MaBoPhan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_bp")]
        public string TenBoPhan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_bp2")]
        public string TenBoPhan2 { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }
       
        [Required]
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }
          [Required]
          [Column("user_id0")]
        public decimal CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
          public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [MaxLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }
        
          [Required]
          [Column("user_id2")]
        public decimal ModifiedUserId { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
          public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Albpcc")]
    public partial class Albpcc
    {
        //[Key]
        [StringLength(16)]
        [Column("ma_bp")]
        public string MaBoPhan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_bp")]
        public string TenBoPhan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_bp2")]
        public string TenBoPhan2 { get; set; }
        [Required]
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }
        [Required]
        [Column("user_id0")]
        public byte CreatedUserId { get; set; }
        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

         [Required]
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
         public string ModifiedTime { get; set; }

         [Required]
         [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
         public string MaTuDo1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTuDo2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTuDo3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }
        public Guid UID { get; set; }
    }


    [Table("Albpht")]
    public partial class Albpht
    {
        //[Key]
        [StringLength(8)]
        public string ma_bpht { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_bpht { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_bpht2 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk621 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk622 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk623 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk627 { get; set; }

        [Required]
        [StringLength(16)]
        public string tk154 { get; set; }

        [Column(TypeName = "text")]
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

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal STT_TINH { get; set; }
    }


    public partial class Albpt
    {
        //[Key]
        [StringLength(16)]
        public string ma_bp { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_bp { get; set; }

        [Required]
        [StringLength(48)]
        public string ten_bp2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }
    }


    [Table("Alcc")]
    public partial class Alcc
    {
        //[Key]
        [StringLength(8)]
        public string so_the_cc { get; set; }

        [StringLength(48)]
        public string ten_cc { get; set; }

        [StringLength(8)]
        public string so_hieu_cc { get; set; }

        [StringLength(48)]
        public string ten_cc2 { get; set; }

        [StringLength(8)]
        public string ma_dvcs { get; set; }

        [StringLength(10)]
        public string dvt { get; set; }

        [StringLength(16)]
        public string nuoc_sx { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? nam_sx { get; set; }

        [StringLength(8)]
        public string nh_cc1 { get; set; }

        [StringLength(8)]
        public string nh_cc2 { get; set; }

        [StringLength(8)]
        public string nh_cc3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tinh_pb { get; set; }

        [StringLength(8)]
        public string ma_tg_cc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_mua { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_pb0 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_ky_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_cl { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_pb1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? kieu_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ty_le_pb { get; set; }

        [Column(TypeName = "numeric")]
        public decimal tong_sl { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? loai_pb { get; set; }

        [StringLength(16)]
        public string ma_bp { get; set; }

        [StringLength(16)]
        public string tk_cc { get; set; }

        [StringLength(16)]
        public string tk_pb { get; set; }

        [StringLength(16)]
        public string tk_cp { get; set; }

        [StringLength(24)]
        public string cong_suat { get; set; }

        [StringLength(8)]
        public string loai_cc { get; set; }

        [StringLength(48)]
        public string ts_kt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_dc { get; set; }

        [StringLength(48)]
        public string ly_do_dc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_giam { get; set; }

        [StringLength(2)]
        public string ma_giam_cc { get; set; }

        [StringLength(48)]
        public string ly_do_giam { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [StringLength(128)]
        public string ghi_chu { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_dvsd { get; set; }

        [StringLength(12)]
        public string so_ct { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        public string time0 { get; set; }

        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        public string status { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        public string time2 { get; set; }

        public byte user_id2 { get; set; }

        [StringLength(16)]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        public string ma_td3 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        public string gc_td3 { get; set; }

        [StringLength(8)]
        public string ma_qg { get; set; }

        [Required]
        [StringLength(2)]
        public string loai_cc0 { get; set; }

        [Required]
        [StringLength(2)]
        public string thuoc_nhom { get; set; }

        [Required]
        [StringLength(1)]
        public string trang_thai { get; set; }
    }


    [Table("Alck")]
    public partial class Alck
    {
        //[Key]
        [StringLength(8)]
        [Column("ma_ck")]
        public string MaChietKhau { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ck")]
        public string TenChietKhau { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ck2")]
        public string TenChietKhau2 { get; set; }

        [StringLength(2)]
        [Column("loai_ck")]
        public string LoaiChietKhau { get; set; }

        [Column("muc_do", TypeName = "numeric")]
        public decimal? Muc_DO { get; set; }

        [StringLength(1)]
        [Column("tien_yn")]
        public string ChoPhepSuaTien { get; set; }

        [StringLength(1)]
        [Column("tienh_yn")]
        public string ChoPhepSuaTienH { get; set; }

        [StringLength(1)]
        [Column("cong_yn")]
        public string Cong_YN { get; set; }

        [StringLength(1)]
        [Column("con_lai_yn")]
        public string ConLai_YN { get; set; }

        [Column("ngay_ct1", TypeName = "smalldatetime")]
        public DateTime? NgayChungTu1 { get; set; }

        [Column("ngay_ct2", TypeName = "smalldatetime")]
        public DateTime? NgayChungTu2 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }
        
        [Required]
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Required]
        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? ModifiedUserId { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string MaTuDo1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string MaTuDo2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string MaTuDo3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? NgayTuDo3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? SoLuongTuDo1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? SoLuongTuDo2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? SoLuongTuDo3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        public Guid UID { get; set; }
    }


    public partial class ALck2
    {
        //[Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ma_ck { get; set; }

        //[Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string ma_kho { get; set; }

        //[Key]
        [Column(Order = 2)]
        [StringLength(16)]
        public string ma_kh { get; set; }

        //[Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string nh_kh9 { get; set; }

        //[Key]
        [Column(Order = 4)]
        [StringLength(16)]
        public string ma_vt { get; set; }

        //[Key]
        [Column(Order = 5)]
        [StringLength(16)]
        public string nh_vt9 { get; set; }

        //[Key]
        [Column(Order = 6)]
        [StringLength(10)]
        public string dvt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? he_so { get; set; }

        //[Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tl_ck { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? tien_ck { get; set; }

        //[Key]
        [Column(Order = 8)]
        [StringLength(3)]
        public string ma_nt { get; set; }

        //[Key]
        [Column(Order = 9, TypeName = "smalldatetime")]
        public DateTime ngay_hl1 { get; set; }

        //[Key]
        [Column(Order = 10, TypeName = "smalldatetime")]
        public DateTime ngay_hl2 { get; set; }

        [StringLength(8)]
        public string ma_gia { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal Tien { get; set; }
    }
}
