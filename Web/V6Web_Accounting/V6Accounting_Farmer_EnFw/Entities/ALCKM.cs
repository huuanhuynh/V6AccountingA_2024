namespace V6Soft.Accounting.Farmers.EnFw.Entities
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.ComponentModel.DataAnnotations;
    using global::System.ComponentModel.DataAnnotations.Schema;

    [Table("ALCKM")]
    public partial class ALCKM
    {
        ////[Key]
        [StringLength(16)]
        [Column("Ma_CK", Order = 0)]
        public string Ma_CK { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_CK")]
        public string Ten_CK { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_CK2")]
        public string Ten_CK2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 1)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [StringLength(16)]
        [Column("Ma_CK0")]
        public string Ma_CK0 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_kh", Order = 3)]
        public string Ma_kh { get; set; }

        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        [Column("ghi_chu")]
        public string ghi_chu { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Status")]
        public string Status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }
    }


    [Table("ALCKMCt")]
    public partial class ALCKMCt
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 0)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_CK,Order = 1")]
        public string Ma_CK { get; set; }

        [StringLength(16)]
        [Column("Ma_CK0")]
        public string Ma_CK0 { get; set; }

        [StringLength(16)]
        [Column("Ma_kh")]
        public string Ma_kh { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 3)]
        public string ma_vt { get; set; }

        [Column("pt_ck", TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        [Column("tien_ck", TypeName = "numeric")]
        public decimal? tien_ck { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("Status", Order = 4)]
        public string Status { get; set; }

        //////[Key]
        [Column("date0", Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 6)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 7)]
        public byte user_id0 { get; set; }

        //////[Key]
        [Column("date2", Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 9)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 10)]
        public byte user_id2 { get; set; }
    }


    [Table("Alcltg")]
    public partial class Alcltg
    {
        ////[Key]
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("tag", Order = 1)]
        public string tag { get; set; }

        //////[Key]
        
        [Column("stt", Order = 2, TypeName = "numeric")]
        public decimal stt { get; set; }

        //////[Key]
        
        [StringLength(32)]
        [Column("ten_bt", Order = 3)]
        public string ten_bt { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk", Order = 4)]
        public string tk { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_ct", Order = 5)]
        public string so_ct { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec01", Order = 6)]
        public string stt_rec01 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec02", Order = 7)]
        public string stt_rec02 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec03", Order = 8)]
        public string stt_rec03 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec04", Order = 9)]
        public string stt_rec04 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec05", Order = 10)]
        public string stt_rec05 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec06", Order = 11)]
        public string stt_rec06 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec07", Order = 12)]
        public string stt_rec07 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec08", Order = 13)]
        public string stt_rec08 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec09", Order = 14)]
        public string stt_rec09 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("Order = 15")]
        public string stt_rec10 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec11", Order = 16)]
        public string stt_rec11 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec12", Order = 17)]
        public string stt_rec12 { get; set; }

        //////[Key]
        [Column("date0", Order = 18, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 19)]
        public byte user_id0 { get; set; }


        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(12)]
        [Column("so_ct01")]
        public string so_ct01 { get; set; }


        [StringLength(12)]
        [Column("so_ct02")]
        public string so_ct02 { get; set; }

        [StringLength(12)]
        [Column("so_ct03")]
        public string so_ct03 { get; set; }

        [StringLength(12)]
        [Column("so_ct04")]
        public string so_ct04 { get; set; }

        [StringLength(12)]
        [Column("so_ct05")]
        public string so_ct05 { get; set; }

        [StringLength(12)]
        [Column("so_ct06")]
        public string so_ct06 { get; set; }

        [StringLength(12)]
        [Column("so_ct07")]
        public string so_ct07 { get; set; }

        [StringLength(12)]
        [Column("so_ct08")]
        public string so_ct08 { get; set; }

        [StringLength(12)]
        [Column("so_ct09")]
        public string so_ct09 { get; set; }

        [StringLength(12)]
        [Column("so_ct10")]
        public string so_ct10 { get; set; }

        [StringLength(12)]
        [Column("so_ct11")]
        public string so_ct11 { get; set; }

        [StringLength(12)]
        [Column("so_ct12")]
        public string so_ct12 { get; set; }
    }


    [Table("Alct")]
    public partial class Alct
    {
        [Required]
        [StringLength(1)]
        [Column("Module_id")]
        public string ModuleID { get; set; }

        [Required]
        [StringLength(12)]
        [Column("ma_phan_he")]
        public string MaPhanHe { get; set; }

        [Key]
        [StringLength(3)]
        [Column("ma_ct")]
        public string MaCT { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ct")]
        public string TenCT { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ct2")]
        public string TenCT2 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct_me")]
        public string MaCTMe { get; set; }

        [Column("so_ct",TypeName = "numeric")]
        public decimal so_ct { get; set; }

        [StringLength(3)]
        [Column("m_ma_nk")]
        public string MMaNk { get; set; }

        [StringLength(1)]
        [Column("m_ma_gd")]
        public string MMaGd { get; set; }

        [Column("m_ma_td")]
        public decimal MMaTd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string MaNt { get; set; }

        [Required]
        [StringLength(32)]
        [Column("tieu_de_ct")]
        public string TieuDeCT { get; set; }

        [Required]
        [StringLength(32)]
        [Column("tieu_de2")]
        public string TieuDe2 { get; set; }

        [Column("so_lien",TypeName = "numeric")]
        public decimal SoLien { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct_in")]
        public string MaCTIn { get; set; }

        [Required]
        [StringLength(8)]
        [Column("form")]
        public string Form { get; set; }

        [Column("stt_ct_nkc")]
        public decimal STTCTNkc { get; set; }

        [Column("stt_ctntxt")]
        public decimal STTCtntxt { get; set; }

        [Column("ct_nxt")]
        public decimal? CTNxt { get; set; }

        [Required]
        [StringLength(6)]
        [Column("m_phdbf")]
        public string MPhdbf { get; set; }

        [Required]
        [StringLength(6)]
        [Column("m_ctdbf")]
        public string MCtdbf { get; set; }

        [Required]
        [StringLength(1)]
        [Column("m_status")]
        public string MSTATUS { get; set; }

        [Column("post_type")]
        public decimal PostType { get; set; }

        [Column("m_sl_ct0")]
        public decimal MSlCT0 { get; set; }


        [Column("m_trung_so",TypeName = "numeric")]
        public decimal MTrungSo { get; set; }

        [Column("m_loc_nsd")]
        public decimal MLocNsd { get; set; }

        [Column("m_ong_ba")]
        public decimal MOngBa { get; set; }

        [Column("m_ngay_ct")]
        public decimal MNgayCT { get; set; }

        [Required]
        [StringLength(100)]
        [Column("procedur")]
        public string Procedur { get; set; }

        [Column("date2",TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public decimal? ModifiedUserId { get; set; }

        [Column("stt")]
        public decimal? STT { get; set; }

        [StringLength(128)]
        [Column("m_ma_td2")]
        public string MMaTd2 { get; set; }

        [StringLength(128)]
        [Column("m_ma_td3")]
        public string MMaTd3 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td1")]
        public string MNgayTd1 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td1")]
        public string MSlTd1 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td2")]
        public string MSlTd2 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td3")]
        public string MSlTd3 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td1")]
        public string MGcTd1 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td2")]
        public string MGcTd2 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td3")]
        public string MGcTd3 { get; set; }

        [Column("post2")]
        public decimal? Post2 { get; set; }

        [Column("post3")]
        public decimal? Post3 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td2")]
        public string MNgayTd2 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td3")]
        public string MNgayTd3 { get; set; }

        [StringLength(10)]
        [Column("dk_ctgs")]
        public string DkCtgs { get; set; }

        [Column("kh_yn")]
        public decimal? KhYn { get; set; }

        [Column("cc_yn")]
        public decimal? CCYn { get; set; }

        [Column("nv_yn")]
        public decimal? NvYn { get; set; }

        [StringLength(3)]
        [Column("ma_ct_old")]
        public string MaCTOld { get; set; }

        [StringLength(10)]
        [Column("m_ph_old")]
        public string MPhOld { get; set; }

        [Column("m_bp_bh")]
        public decimal? MBpBh { get; set; }

        [Column("m_ma_nvien")]
        public decimal? MMaNvien { get; set; }

        [Column("m_ma_vv")]
        public decimal? MMaVv { get; set; }

        [StringLength(128)]
        [Column("m_ma_hd")]
        public string MMaHd { get; set; }

        [StringLength(128)]
        [Column("m_ma_ku")]
        public string MMaKu { get; set; }

        [StringLength(128)]
        [Column("m_ma_phi")]

        public string MMaPhi { get; set; }

        [StringLength(128)]
        [Column("m_ma_vitri")]
        public string MMaVitri { get; set; }

        [StringLength(128)]
        [Column("m_ma_lo")]
        public string MMaLo { get; set; }

        [StringLength(128)]
        [Column("m_ma_bpht")]
        public string MMaBpht { get; set; }

        [StringLength(128)]
        [Column("m_ma_sp")]
        public string MMaSp { get; set; }

        [StringLength(1)]
        [Column("m_k_post")]
        public string MKPost { get; set; }

        [Required]
        [StringLength(16)]
        [Column("Tk_no")]
        public string TkNo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("Tk_co")]
        public string TkCo { get; set; }

        [StringLength(128)]
        [Column("M_MA_LNX")]
        public string MMaLnx { get; set; }

        [StringLength(128)]
        [Column("M_HSD")]
        public string MHSD { get; set; }

        [Column("m_ma_sonb")]
        public byte MMaSonb { get; set; }

        [Column("m_sxoa_nsd")]
        public byte MSxoaNsd { get; set; }

        [Required]
        [StringLength(12)]
        [Column("SIZE_CT")]
        public string SizeCT { get; set; }

        [Column("THEM_IN", TypeName = "numeric")]
        public decimal ThemIn { get; set; }

        [Required]
        [StringLength(12)]
        [Column("phandau")]
        public string Phandau { get; set; }

        [Required]
        [StringLength(12)]
        [Column("phancuoi")]
        public string Phancuoi { get; set; }

        [Required]
        [StringLength(12)]
        [Column("dinhdang")]
        public string Dinhdang { get; set; }

        //[Required]
        //[StringLength(128)]
        //[Column("M_Ma_KMB")]
        //public string MMaKmb { get; set; }

        //[Required]
        //[StringLength(128)]
        //[Column("M_Ma_KMM")]
        //public string MMaKmm { get; set; }

        //[Required]
        //[StringLength(128)]
        //[Column("M_SO_LSX")]
        //public string MSoLsx { get; set; }

        //[Required]
        //[StringLength(128)]
        //[Column("M_MA_KHO2")]
        //public string MMaKho2 { get; set; }

        //[Required]
        //[StringLength(1)]
        //[Column("F6BARCODE")]
        //public string F6BARCODE { get; set; }

        [StringLength(128)]
        [Column("ADV_AM")]
        public string AdvAm { get; set; }

        [StringLength(128)]
        [Column("ADV_AD")]
        public string AdvAd { get; set; }
    }

    [Table("alct1")]
    public partial class Alct1
    {
        [Column("ma_ct", Order = 0)]
        public string MaCT { get; set; }
        [StringLength(16)]
        [Column("ma_dm")]
        public string MaDm { get; set; }
        [StringLength(64)]
        [Column("caption")]
        public string Caption { get; set; }
        [StringLength(64)]
        [Column("caption2")]
        public string Caption2 { get; set; }
        [Column("loai")]
        public int LOAI { get; set; }
        [StringLength(2)]
        [Column("ftype")]
        public string Ftype { get; set; }
        [Column("forder")]
        public int Forder { get; set; }
        [Column("width")]
        public int Width { get; set; }
        [Column("carry")]
        public short Carry { get; set; }
        [Column("fstatus")]
        public bool Fstatus { get; set; }
        [Column("checkvvar")]
        public bool Checkvvar { get; set; }
        [Column("notempty")]
        public bool Notempty { get; set; }
        [Column("fdecimal")]
        public short Fdecimal { get; set; }
        [Column("visible")]
        public bool Visible { get; set; }
         [StringLength(20)]
        [Column("fvvar")]
        public string Fvvar { get; set; }
         [StringLength(20)]
        [Column("fcolumn")]
        public string Fcolumn { get; set; }
        [StringLength(50)]
        [Column("lstatus")]
        public string Lstatus { get; set; }
        [Column("fstatus2")]
         public bool Fstatus2 { get; set; }
        [Column("UID")]
        public Guid UID { get; set; }
    }

    [Table("Alctct")]
    public partial class Alctct
    {
        ////[Key]
        [StringLength(1)]
        [Column("Module_id", Order = 0)]
        public string Module_id { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("ma_phan_he", Order = 1)]
        public string ma_phan_he { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_ct", Order = 2)]
        public string ma_ct { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_ct_me", Order = 3)]
        public string ma_ct_me { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 4)]
        public string ma_dvcs { get; set; }

        [Column("user_id_ct")]
        public byte? user_id_ct { get; set; }

        //////[Key]
        [Column("so_ct", Order = 5, TypeName = "numeric")]
        public decimal so_ct { get; set; }

        //////[Key]
        [Column("m_trung_so", Order = 6, TypeName = "numeric")]
        public decimal m_trung_so { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("chuan", Order = 7)]
        public string chuan { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("loai", Order = 8)]
        public string loai { get; set; }
    }


    [Table("Alcthd")]
    public partial class Alcthd
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_hd", Order = 0)]
        public string ma_hd { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 1)]
        public string ma_vt { get; set; }

        [Column("so_luong", TypeName = "numeric")]
        public decimal? so_luong { get; set; }

        [Column("gia_nt", TypeName = "numeric")]
        public decimal? gia_nt { get; set; }

        [Column("gia", TypeName = "numeric")]
        public decimal? gia { get; set; }

        [Column("tien_nt", TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column("tien", TypeName = "numeric")]
        public decimal? tien { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        [StringLength(10)]
        [Column("dvt1")]
        public string dvt1 { get; set; }

        [StringLength(10)]
        [Column("dvt")]
        public string dvt { get; set; }
    }


    [Table("Aldmpbct")]
    public partial class Aldmpbct
    {
        [Column("Stt", TypeName = "numeric")]
        public decimal Stt { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("ma_bpht",Order = 0)]
        public string ma_bpht { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("TK_CO",Order = 1)]
        public string TK_CO { get; set; }

        [Required]
        [StringLength(16)]
        [Column("TK_NO")]
        public string TK_NO { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_sp", Order = 2)]
        public string ma_sp { get; set; }

        [Column("he_so", TypeName = "numeric")]
        public decimal? he_so { get; set; }

        //////[Key]
        [Column("ngay_hl1", Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_hl1 { get; set; }

        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Aldmpbph")]
    public partial class Aldmpbph
    {
        [Required]
        [StringLength(1)]
        [Column("tag")]
        public string tag { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("ma_bpht", Order = 0)]
        public string ma_bpht { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_co", Order = 1)]
        public string tk_co { get; set; }

        [Required]
        [StringLength(48)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Column("ngay_hl", TypeName = "smalldatetime")]
        public DateTime? ngay_hl { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Aldmvt")]
    public partial class Aldmvt
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_bpht", Order = 0)]
        public string ma_bpht { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_sp", Order = 1)]
        public string ma_sp { get; set; }

        [Column("sl_sp", TypeName = "numeric")]
        public decimal sl_sp { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column("ngay_td2",TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Aldmvtct")]
    public partial class Aldmvtct
    {
        ////[Key]
        
        [StringLength(8)]
        [Column("ma_bpht", Order = 0)]
        public string ma_bpht { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_sp", Order = 1)]
        public string ma_sp { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 2)]
        public string ma_vt { get; set; }

        //////[Key]
        [Column("ngay_hl1", Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_hl1 { get; set; }

        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        //////[Key]
        [Column("sl_SP", Order = 4, TypeName = "numeric")]
        public decimal sl_SP { get; set; }
        //////[Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal sl_dm_dh { get; set; }

        //////[Key]
        [Column("sl_dm_kh", Order = 6, TypeName = "numeric")]
        public decimal sl_dm_kh { get; set; }

        //////[Key]
        [Column("sl_tt", Order = 7, TypeName = "numeric")]
        public decimal sl_tt { get; set; }

        //////[Key]
        [Column("date0", Order = 8, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 9)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 10)]
        public byte user_id0 { get; set; }

        //////[Key]
        [Column("date2", Order = 11, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 12)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 13)]
        public byte user_id2 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }

    [Table("Aldvcs")]
    public partial class Aldvcs
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string MaDonVi { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_dvcs")]
        public string TenDonVi { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_dvcs2")]
        public string TenDonVi2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [StringLength(128)]
        [Column("DIA_CHI")]
        public string DiaChi { get; set; }

        [StringLength(128)]
        [Column("DIA_CHI2")]
        public string DiaChi2 { get; set; }

        [StringLength(128)]
        [Column("DIEN_THOAI")]
        public string DienThoai { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_DVCS1")]
        public string Nhom_DVCS1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_DVCS2")]
        public string Nhom_DVCS2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_DVCS3")]
        public string Nhom_DVCS3 { get; set; }

        public Guid UID { get; set; }
    }

    [Table("Aldvt")]
    public partial class Aldvt
    {
        ////[Key]
        [StringLength(10)]
        [Column("dvt")]
        public string DonViTinh { get; set; }

        [Required]
        [StringLength(24)]
        [Column("ten_dvt")]
        public string TenDonViTinh { get; set; }

        [Required]
        [StringLength(24)]
        [Column("ten_dvt2")]
        public string TenDonViTinh2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALgia")]
    public partial class ALgia
    {
        ////[Key]
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_kho", Order = 1)]
        public string ma_kho { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vt",Order = 2)]
        public string ma_vt { get; set; }

        
        [Column("gia01",TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        
        [Column("gia_nt01",TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        
        [Column("gia02",TypeName = "numeric")]
        public decimal? gia02 { get; set; }

        
        [Column("gia_nt02",TypeName = "numeric")]
        public decimal? gia_nt02 { get; set; }

        
        [Column("gia03",TypeName = "numeric")]
        public decimal? gia03 { get; set; }

        
        [Column("gia_nt03",TypeName = "numeric")]
        public decimal? gia_nt03 { get; set; }

        
        [Column("gia04",TypeName = "numeric")]
        public decimal? gia04 { get; set; }

        
        [Column("gia_nt04",TypeName = "numeric")]
        public decimal? gia_nt04 { get; set; }

        
        [Column("gia05",TypeName = "numeric")]
        public decimal? gia05 { get; set; }

        
        [Column("gia_nt05",TypeName = "numeric")]
        public decimal? gia_nt05 { get; set; }

        
        [Column("gia06",TypeName = "numeric")]
        public decimal? gia06 { get; set; }

        
        [Column("gia_nt06",TypeName = "numeric")]
        public decimal? gia_nt06 { get; set; }

        
        [Column("gia07",TypeName = "numeric")]
        public decimal? gia07 { get; set; }

        
        [Column("gia_nt07",TypeName = "numeric")]
        public decimal? gia_nt07 { get; set; }

        
        [Column("gia08",TypeName = "numeric")]
        public decimal? gia08 { get; set; }

        
        [Column("gia_nt08",TypeName = "numeric")]
        public decimal? gia_nt08 { get; set; }

        
        [Column("gia09",TypeName = "numeric")]
        public decimal? gia09 { get; set; }

        
        [Column("gia_nt09",TypeName = "numeric")]
        public decimal? gia_nt09 { get; set; }

        
        [Column("gia10",TypeName = "numeric")]
        public decimal? gia10 { get; set; }

        
        [Column("gia_nt10",TypeName = "numeric")]
        public decimal? gia_nt10 { get; set; }

        
        [Column("gia11",TypeName = "numeric")]
        public decimal? gia11 { get; set; }

        
        [Column("gia_nt11",TypeName = "numeric")]
        public decimal? gia_nt11 { get; set; }

        
        [Column("gia12",TypeName = "numeric")]
        public decimal? gia12 { get; set; }

        
        [Column("gia_nt12",TypeName = "numeric")]
        public decimal? gia_nt12 { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0",TypeName = "numeric")]
        public decimal? user_id0 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    public partial class ALgia2
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 0)]
        public string ma_vt { get; set; }

        //////[Key]
        [Column("ngay_ban", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_ban { get; set; }

        //////[Key]
        [Column("gia_nt2", Order = 2, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //////[Key]
        [Column("gia2", Order = 3, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 4)]
        public string ma_nt { get; set; }

        //////[Key]
        [Column("date0", Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 6)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 7)]
        public byte user_id0 { get; set; }

        //////[Key]
        [Column("date2", Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 9)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 10)]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_gia")]
        public string ma_gia { get; set; }

        [StringLength(10)]
        [Column("Dvt")]
        public string Dvt { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_kh", Order = 11)]
        public string Ma_kh { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("STATUS", Order = 12)]
        public string STATUS { get; set; }

        
        [Column("NGAY_HHL", TypeName = "smalldatetime")]
        public DateTime? NGAY_HHL { get; set; }

        //////[Key]
        [StringLength(100)]
        [Column("CHECK_SYNC", Order = 13)]
        public string CHECK_SYNC { get; set; }
    }


    public partial class ALGIA200
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 0)]
        public string ma_vt { get; set; }

        //////[Key]
        [Column("ngay_ban", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_ban { get; set; }

        //////[Key]
        [Column("gia_nt2", Order = 2, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //////[Key]
        [Column("gia2", Order = 3, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 4)]
        public string ma_nt { get; set; }

        //////[Key]
        [Column("date0", Order = 5, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 6)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 7)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [Column("date2", Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 9)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 10)]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_gia")]
        public string ma_gia { get; set; }

        [StringLength(10)]
        [Column("Dvt")]
        public string Dvt { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_kh", Order = 11)]
        public string Ma_kh { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("STATUS", Order = 12)]
        public string STATUS { get; set; }

        
        [Column("NGAY_HHL", TypeName = "smalldatetime")]
        public DateTime? NGAY_HHL { get; set; }

        //////[Key]
        [StringLength(100)]
        [Column("CHECK_SYNC", Order = 13)]
        public string CHECK_SYNC { get; set; }
    }


    [Table("ALgiavon")]
    public partial class ALgiavon
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 0)]
        public string ma_vt { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("loai", Order = 1)]
        public string loai { get; set; }

        
        [Column("giavon", TypeName = "numeric")]
        public decimal? giavon { get; set; }

        
        [Column("giavonnt", TypeName = "numeric")]
        public decimal? giavonnt { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }
    }


    public partial class ALgiavon3
    {
        ////[Key]
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_kho", Order = 1)]
        public string ma_kho { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vt", Order = 2)]
        public string ma_vt { get; set; }

        
        [Column("gia01",TypeName = "numeric")]
        public decimal? gia01 { get; set; }

        
        [Column("gia_nt01", TypeName = "numeric")]
        public decimal? gia_nt01 { get; set; }

        
        [Column("gia02", TypeName = "numeric")]
        public decimal? gia02 { get; set; }

        
        [Column("gia_nt02", TypeName = "numeric")]
        public decimal? gia_nt02 { get; set; }

        
        [Column("gia03", TypeName = "numeric")]
        public decimal? gia03 { get; set; }

        
        [Column("gia_nt03", TypeName = "numeric")]
        public decimal? gia_nt03 { get; set; }

        
        [Column("gia04", TypeName = "numeric")]
        public decimal? gia04 { get; set; }

        
        [Column("gia_nt04", TypeName = "numeric")]
        public decimal? gia_nt04 { get; set; }

        
        [Column("gia05", TypeName = "numeric")]
        public decimal? gia05 { get; set; }

        
        [Column("gia_nt05", TypeName = "numeric")]
        public decimal? gia_nt05 { get; set; }

        
        [Column("gia06", TypeName = "numeric")]
        public decimal? gia06 { get; set; }

        
        [Column("gia_nt06", TypeName = "numeric")]
        public decimal? gia_nt06 { get; set; }

        
        [Column("gia07", TypeName = "numeric")]
        public decimal? gia07 { get; set; }

        
        [Column("gia_nt07", TypeName = "numeric")]
        public decimal? gia_nt07 { get; set; }

        
        [Column("gia08", TypeName = "numeric")]
        public decimal? gia08 { get; set; }

        
        [Column("gia_nt08", TypeName = "numeric")]
        public decimal? gia_nt08 { get; set; }

        
        [Column("gia09", TypeName = "numeric")]
        public decimal? gia09 { get; set; }

        
        [Column("gia_nt09", TypeName = "numeric")]
        public decimal? gia_nt09 { get; set; }

        
        [Column("gia10", TypeName = "numeric")]
        public decimal? gia10 { get; set; }

        
        [Column("gia_nt10", TypeName = "numeric")]
        public decimal? gia_nt10 { get; set; }

        
        [Column("gia11", TypeName = "numeric")]
        public decimal? gia11 { get; set; }

        
        [Column("gia_nt11", TypeName = "numeric")]
        public decimal? gia_nt11 { get; set; }

        
        [Column("gia12", TypeName = "numeric")]
        public decimal? gia12 { get; set; }

        
        [Column("gia_nt12", TypeName = "numeric")]
        public decimal? gia_nt12 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0", TypeName = "numeric")]
        public decimal? user_id0 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2", TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }

    [Table("ALgiavv")]
    public partial class ALgiavv
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vv", Order = 0)]
        public string ma_vv { get; set; }

        //////[Key]
        [Column("ngay_ban", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_ban { get; set; }

        //////[Key]
        [Column("gia_nt2", Order = 2, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //////[Key]
        [Column("gia2", Order = 3, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 6)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [Column("date2", Order = 7, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 8)]
        public string time2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("nh_vt2", Order = 9)]
        public string nh_vt2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 10)]
        public byte user_id2 { get; set; }
    }


    [Table("Alhd")]
    public partial class Alhd
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_hd")]
        public string ma_hd { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_hd")]
        public string ten_hd { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_hd2")]
        public string ten_hd2 { get; set; }

        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(8)]
        [Column("ma_nvbh")]
        public string ma_nvbh { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(1)]
        [Column("loai_hd")]
        public string loai_hd { get; set; }

        [StringLength(8)]
        [Column("nh_hd1")]
        public string nh_hd1 { get; set; }

        [StringLength(8)]
        [Column("nh_hd2")]
        public string nh_hd2 { get; set; }

        [StringLength(8)]
        [Column("nh_hd3")]
        public string nh_hd3 { get; set; }

        
        [Column("ngay_hd", TypeName = "smalldatetime")]
        public DateTime? ngay_hd { get; set; }

        [StringLength(20)]
        [Column("so_hd")]
        public string so_hd { get; set; }

        
        [Column("ngay_hd1", TypeName = "smalldatetime")]
        public DateTime? ngay_hd1 { get; set; }

        
        [Column("ngay_hd2", TypeName = "smalldatetime")]
        public DateTime? ngay_hd2 { get; set; }

        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("tien_nt", TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        
        [Column("tien", TypeName = "numeric")]
        public decimal? tien { get; set; }

        
        [Column("tien_gt_nt", TypeName = "numeric")]
        public decimal? tien_gt_nt { get; set; }

        
        [Column("tien_gt", TypeName = "numeric")]
        public decimal? tien_gt { get; set; }


        [Column("tinh_trang")]
        public byte? tinh_trang { get; set; }

        
        [Column("kl_kh", TypeName = "numeric")]
        public decimal? kl_kh { get; set; }

        
        [Column("kl_th", TypeName = "numeric")]
        public decimal? kl_th { get; set; }

        
        [Column("ghi_chu", TypeName = "ntext")]
        public string ghi_chu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        [StringLength(1)]
        [Column("CT")]
        public string CT { get; set; }
    }


    [Table("Alhttt")]
    public partial class Alhttt
    {
        ////[Key]
        [StringLength(2)]
        [Column("ma_httt")]
        public string MaHinhThucThanhToan { get; set; }

        [StringLength(48)]
        [Column("ten_httt")]
        public string TenHinhThucThanhToan { get; set; }

        [StringLength(48)]
        [Column("ten_httt2")]
        public string TenHinhThucThanhToan2 { get; set; }

        [Column("han_tt", TypeName = "numeric")]
        public decimal? HanThanhToan { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

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


    [Table("Alhtvc")]
    public partial class Alhtvc
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_htvc")]
        public string MaHinhThucVanChuyen { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_htvc")]
        public string TenHinhThucVanChuyen { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_htvc2")]
        public string TenHinhThucVanChuyen2 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

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


    [Table("ALkc")]
    public partial class ALkc
    {
        ////[Key]
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("tag", Order = 1)]
        public string tag { get; set; }

        //////[Key]
        [Column("stt", Order = 2, TypeName = "numeric")]
        public decimal stt { get; set; }

        //////[Key]
        [StringLength(64)]
        [Column("ten_bt", Order = 3)]
        public string ten_bt { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_no", Order = 4)]
        public string tk_no { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_co", Order = 5)]
        public string tk_co { get; set; }

        //////[Key]
        [Column("loai_kc", Order = 6, TypeName = "numeric")]
        public decimal loai_kc { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_ct", Order = 7)]
        public string so_ct { get; set; }

        //////[Key]
        [Column("kc_vv_yn", Order = 8)]
        public byte kc_vv_yn { get; set; }

        //////[Key]
        [Column("kc_td_yn", Order = 9)]
        public byte kc_td_yn { get; set; }

        //////[Key]
        [Column("kc_bpht_yn", Order = 10)]
        public byte kc_bpht_yn { get; set; }

        //////[Key]
        [Column("kc_sp_yn", Order = 11)]
        public byte kc_sp_yn { get; set; }

        //////[Key]
        [Column("kc_hd_yn", Order = 12)]
        public byte kc_hd_yn { get; set; }

        //////[Key]
        [Column("kc_ku_yn", Order = 13)]
        public byte kc_ku_yn { get; set; }

        //////[Key]
        [Column("kc_phi_yn", Order = 14)]
        public byte kc_phi_yn { get; set; }

        //////[Key]
        [Column("kc_td2_yn", Order = 15)]
        public byte kc_td2_yn { get; set; }

        //////[Key]
        [Column("kc_td3_yn", Order = 16)]
        public byte kc_td3_yn { get; set; }

        //////[Key]
        [Column("group_kc", Order = 17)]
        public string group_kc { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec01", Order = 18)]
        public string stt_rec01 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec02", Order = 19)]
        public string stt_rec02 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec03", Order = 20)]
        public string stt_rec03 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec04", Order = 21)]
        public string stt_rec04 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec05", Order = 22)]
        public string stt_rec05 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec06", Order = 23)]
        public string stt_rec06 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec07", Order = 24)]
        public string stt_rec07 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec08", Order = 25)]
        public string stt_rec08 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec09", Order = 26)]
        public string stt_rec09 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec10", Order = 27)]
        public string stt_rec10 { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec11", Order = 28)]
        public string stt_rec11 { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("stt_rec12", Order = 29)]
        public string stt_rec12 { get; set; }

        //////[Key]
        [Column("date0", Order = 30, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 31)]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }


        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(12)]
        [Column("so_ct01")]
        public string so_ct01 { get; set; }

        [StringLength(12)]
        [Column("so_ct02")]
        public string so_ct02 { get; set; }

        [StringLength(12)]
        [Column("so_ct03")]
        public string so_ct03 { get; set; }

        [StringLength(12)]
        [Column("so_ct04")]
        public string so_ct04 { get; set; }

        [StringLength(12)]
        [Column("so_ct05")]
        public string so_ct05 { get; set; }

        [StringLength(12)]
        [Column("so_ct06")]
        public string so_ct06 { get; set; }

        [StringLength(12)]
        [Column("so_ct07")]
        public string so_ct07 { get; set; }

        [StringLength(12)]
        [Column("so_ct08")]
        public string so_ct08 { get; set; }

        [StringLength(12)]
        [Column("so_ct09")]
        public string so_ct09 { get; set; }

        [StringLength(12)]
        [Column("so_ct10")]
        public string so_ct10 { get; set; }

        [StringLength(12)]
        [Column("so_ct11")]
        public string so_ct11 { get; set; }

        [StringLength(12)]
        [Column("so_ct12")]
        public string so_ct12 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("AUTO", Order = 32)]
        public string AUTO { get; set; }
    }

    public class Alkh
    {
        [Key]
        [MaxLength(16)]
        [Column("ma_kh")]
        public string MaKhachHang { get; set; }

        [MaxLength(128)]
        [Column("ten_kh")]
        public string TenKhachHang { get; set; }

        [MaxLength(128)]
        [Column("ten_kh2")]
        public string TenTiengAnh { get; set; }

        [MaxLength(18)]
        [Column("ma_so_thue")]
        public string MaSoThue { get; set; }

        [MaxLength(128)]
        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [MaxLength(32)]
        [Column("dien_thoai")]
        public string DienThoai { get; set; }

        [MaxLength(16)]
        [Column("fax")]
        public string Fax { get; set; }

        [MaxLength(16)]
        [Column("e_mail")]
        public string Email { get; set; }

        [MaxLength(32)]
        [Column("home_page")]
        public string TrangChu { get; set; }

        [MaxLength(32)]
        [Column("doi_tac")]
        public string DoiTac { get; set; }

        [MaxLength(32)]
        [Column("ong_ba")]
        public string OngBa { get; set; }

        [MaxLength(32)]
        [Column("ten_bp")]
        public string TenBoPhan { get; set; }

        [MaxLength(64)]
        [Column("ngan_hang")]
        public string NganHang { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("han_tt")]
        public decimal? HanThanhToan { get; set; }

        [MaxLength(16)]
        [Column("tk")]
        public string TaiKhoan { get; set; }

        [MaxLength(8)]
        [Column("nh_kh1")]
        public string NhomKhachHang1 { get; set; }

        [MaxLength(8)]
        [Column("nh_kh2")]
        public string NhomKhachHang2 { get; set; }

        [MaxLength(8)]
        [Column("nh_kh3")]
        public string NhomKhachHang3 { get; set; }

        [Column("du_nt13", TypeName = "numeric")]
        public decimal? DuNgoaiTe13 { get; set; }

        [Column("du13", TypeName = "numeric")]
        public decimal? Du13 { get; set; }

        [Column("t_tien_cn", TypeName = "numeric")]
        public decimal? TongTienCongNo { get; set; }

        [Column("t_tien_hd", TypeName = "numeric")]
        public decimal? TongTienHoaDon { get; set; }

        [MaxLength(8)]
        [Column("Ma_httt")]
        public string MaHinhThucThanhToan { get; set; }

        [MaxLength(8)]
        [Column("Nh_kh9")]
        public string NhomKhachHang9 { get; set; }

        [MaxLength(8)]
        [Column("Ma_snvien")]
        public string MaSoNhanVien { get; set; }

        [Column("Ngay_gh", TypeName = "smalldatetime")]
        public DateTime? NgayGioiHan { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public decimal CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [MaxLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public decimal ModifiedUserId { get; set; }

        [MaxLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [MaxLength(16)]
        [Column("ma_td1")]
        public string MaTuDo1 { get; set; }

        [MaxLength(16)]
        [Column("ma_td2")]
        public string MaTuDo2 { get; set; }

        [MaxLength(16)]
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

        [MaxLength(24)]
        [Column("gc_td1")]
        public string GhiChuTuDo1 { get; set; }

        [MaxLength(24)]
        [Column("gc_td2")]
        public string GhiChuTuDo2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string GhiChuTuDo3 { get; set; }

        [Column("kh_yn")]
        public decimal? LaKhachHang { get; set; }

        [Column("cc_yn")]
        public decimal? LaNhaCungCap { get; set; }

        [Column("nv_yn")]
        public decimal? LaNhanVien { get; set; }

        [Column("TK_NH")]
        [MaxLength(24)]
        public string TaiKhoanNganHang { get; set; }

        //[Column("DT_DD")]
        //[MaxLength(20)]
        //public string DienThoaiDiDong { get; set; }

        //[Column("TT_SONHA")]
        //[MaxLength(100)]
        //public string ThongTinSoNha { get; set; }

        //[Column("MA_PHUONG")]
        //[MaxLength(16)]
        //public string MaPhuong { get; set; }

        //[Column("MA_TINH")]
        //[MaxLength(16)]
        //public string MaTinh { get; set; }

        //[Column("MA_QUAN")]
        //[MaxLength(16)]
        //public string MaQuan { get; set; }

        //[Column("CHECK_SYNC")]
        //[MaxLength(100)]
        //public string CheckSync { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alkho")]
    public partial class Alkho
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_kho")]
        public string MaKho { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_kho")]
        public string TenKho { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_kho2")]
        public string ten_kho2 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_dl")]
        public string TaiKhoanDaiLy { get; set; }

        [Column("stt_ntxt")]
        public byte STT_NhapTruocXuatTruoc { get; set; }

        [StringLength(30)]
        [Column("thu_kho")]
        public string ThuKho { get; set; }

        [StringLength(64)]
        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [StringLength(32)]
        [Column("dien_thoai")]
        public string DienThoai { get; set; }

        [StringLength(32)]
        [Column("fax")]
        public string Fax { get; set; }

        [StringLength(32)]
        [Column("email")]
        public string Email { get; set; }

        [StringLength(8)]
        [Column("ma_lotrinh")]
        public string MaLoTrinh { get; set; }

        [StringLength(8)]
        [Column("ma_vc")]
        public string MaVanChuyen { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string MaDonVi { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

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

        [Required]
        [StringLength(1)]
        [Column("date_yn")]
        public string TheoDoiDate { get; set; }

        [Required]
        [StringLength(1)]
        [Column("lo_yn")]
        public string IsParcel { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_DVCS1")]
        public string Nhom_DVCS1 { get; set; }

        public Guid UID { get; set; }
    }


    [Table("AlkhTG")]
    public partial class AlkhTG
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_KHTG")]
        public string ma_KHTG { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ten_khtg")]
        public string ten_khtg { get; set; }

        [StringLength(64)]
        [Column("ten_khtg2")]
        public string ten_khtg2 { get; set; }

        [Required]
        [StringLength(18)]
        [Column("ma_so_thue")]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(32)]
        [Column("dien_thoai")]
        public string dien_thoai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("fax")]
        public string fax { get; set; }

        [Required]
        [StringLength(16)]
        [Column("e_mail")]
        public string e_mail { get; set; }

        [Required]
        [StringLength(32)]
        [Column("home_page")]
        public string home_page { get; set; }

        [Required]
        [StringLength(32)]
        [Column("doi_tac")]
        public string doi_tac { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(32)]
        [Column("ten_bp")]
        public string ten_bp { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ngan_hang")]
        public string ngan_hang { get; set; }

        
        [Required]
        [Column("ghi_chu", TypeName = "text")]
        public string ghi_chu { get; set; }

        [Column("han_tt")]
        public byte han_tt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_kh1")]
        public string nh_kh1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_kh2")]
        public string nh_kh2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_kh3")]
        public string nh_kh3 { get; set; }

        
        [Column("du_nt13", TypeName = "numeric")]
        public decimal? du_nt13 { get; set; }

        
        [Column("du13", TypeName = "numeric")]
        public decimal? du13 { get; set; }

        
        [Column("t_tien_cn", TypeName = "numeric")]
        public decimal? t_tien_cn { get; set; }

        
        [Column("t_tien_hd", TypeName = "numeric")]
        public decimal? t_tien_hd { get; set; }

        [Required]
        [StringLength(8)]
        [Column("Ma_httt")]
        public string Ma_httt { get; set; }

        [Required]
        [StringLength(8)]
        [Column("Nh_kh9")]
        public string Nh_kh9 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("Ma_snvien")]
        public string Ma_snvien { get; set; }

        
        [Column("Ngay_gh", TypeName = "smalldatetime")]
        public DateTime? Ngay_gh { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }


        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        [Column("kh_yn")]
        public byte? kh_yn { get; set; }

        [Column("cc_yn")]
        public byte? cc_yn { get; set; }

        [Column("nv_yn")]
        public byte? nv_yn { get; set; }

        [StringLength(13)]
        [Column("Bar_code")]
        public string Bar_code { get; set; }

        [StringLength(24)]
        [Column("TK_NH")]
        public string TK_NH { get; set; }
    }


    [Table("ALKMB")]
    public partial class ALKMB
    {
        ////[Key]
        
        [StringLength(16)]
        [Column("Ma_km", Order = 0)]
        public string Ma_km { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_km")]
        public string Ten_km { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_km2")]
        public string Ten_km2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("ma_dvcs", Order = 1)]
        public string ma_dvcs { get; set; }

        //////[Key]
        
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        
        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [StringLength(16)]
        [Column("Ma_km0")]
        public string Ma_km0 { get; set; }

        [StringLength(16)]
        [Column("Ma_kmm")]
        public string Ma_kmm { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("Ma_kh", Order = 3)]
        public string Ma_kh { get; set; }

        
        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        
        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        
        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        
        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        [Column("ghi_chu")]
        public string ghi_chu { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Status")]
        public string Status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }
    }


    [Table("ALKMBCt")]
    public partial class ALKMBCt
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 0)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_km", Order = 1)]
        public string Ma_km { get; set; }

        [StringLength(16)]
        [Column("Ma_km0")]
        public string Ma_km0 { get; set; }

        [StringLength(16)]
        [Column("Ma_kmm")]
        public string Ma_kmm { get; set; }

        [StringLength(16)]
        [Column("Ma_kh")]
        public string Ma_kh { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        
        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        
        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        
        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        
        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        
        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 3)]
        public string ma_vt { get; set; }

        
        [Column("pt_ck", TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_sp", Order = 4)]
        public string ma_sp { get; set; }

        
        [Column("sl_km", TypeName = "numeric")]
        public decimal? sl_km { get; set; }

        
        [Column("tien_km", TypeName = "numeric")]
        public decimal? tien_km { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("Status", Order = 5)]
        public string Status { get; set; }

        //////[Key]
        [Column("date0", Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 7)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 8)]
        public byte user_id0 { get; set; }

        //////[Key]
        [Column("date2", Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 10)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 11)]
        public byte user_id2 { get; set; }

        //////[Key]
        [Column("Ghi_chukm", Order = 12)]
        public string Ghi_chukm { get; set; }

        //////[Key]
        [Column("Ghi_chuck", Order = 13)]
        public string Ghi_chuck { get; set; }

        //////[Key]
        [Column("T_SLKM", Order = 14, TypeName = "numeric")]
        public decimal T_SLKM { get; set; }
    }


    [Table("ALKMM")]
    public partial class ALKMM
    {
        ////[Key]
        [StringLength(16)]
        [Column("Ma_km", Order = 0)]
        public string Ma_km { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_km")]
        public string Ten_km { get; set; }

        [Required]
        [StringLength(48)]
        [Column("Ten_km2")]
        public string Ten_km2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 1)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        
        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        [StringLength(16)]
        [Column("Ma_km0")]
        public string Ma_km0 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_kh", Order = 3)]
        public string Ma_kh { get; set; }

        
        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        
        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        
        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        
        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(128)]
        [Column("ghi_chu")]
        public string ghi_chu { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Status")]
        public string Status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }
    }


    [Table("ALKMMCt")]
    public partial class ALKMMCt
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 0)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("Ma_km", Order = 1)]
        public string Ma_km { get; set; }

        [StringLength(16)]
        [Column("Ma_km0")]
        public string Ma_km0 { get; set; }

        [StringLength(16)]
        [Column("Ma_kh")]
        public string Ma_kh { get; set; }

        //////[Key]
        [Column("ngay_hl", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_hl { get; set; }

        
        [Column("ngay_hl2", TypeName = "smalldatetime")]
        public DateTime? ngay_hl2 { get; set; }

        
        [Column("t_sl1", TypeName = "numeric")]
        public decimal? t_sl1 { get; set; }

        
        [Column("t_sl2", TypeName = "numeric")]
        public decimal? t_sl2 { get; set; }

        
        [Column("t_tien1", TypeName = "numeric")]
        public decimal? t_tien1 { get; set; }

        
        [Column("t_tien2", TypeName = "numeric")]
        public decimal? t_tien2 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vt", Order = 3)]
        public string ma_vt { get; set; }

        
        [Column("pt_ck", TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_sp", Order = 4)]
        public string ma_sp { get; set; }

        
        [Column("sl_km", TypeName = "numeric")]
        public decimal? sl_km { get; set; }

        
        [Column("tien_km", TypeName = "numeric")]
        public decimal? tien_km { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("Status", Order = 5)]
        public string Status { get; set; }

        //////[Key]
        
        [Column("date0", Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 7)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 8)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [Column("date2", Order = 9, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 10)]
        public string time2 { get; set; }

        //////[Key]
        
        [Column("user_id2", Order = 11)]
        public byte user_id2 { get; set; }
    }


    [Table("Alku")]
    public partial class Alku
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_ku")]
        public string MaKheUoc { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ku")]
        public string TenKheUoc { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ku2")]
        public string TenKheUoc2 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string MaNgoaiTe { get; set; }

        [Column("tien0", TypeName = "numeric")]
        public decimal Tien0 { get; set; }

        [Column("tien_nt0", TypeName = "numeric")]
        public decimal TienNgoaiTe0 { get; set; }

        [Column("ngay_ku1", TypeName = "smalldatetime")]
        public DateTime? NgayKheUoc1 { get; set; }

        [Column("ngay_ku2", TypeName = "smalldatetime")]
        public DateTime? ngay_ku2 { get; set; }

        [Column("lai_suat1", TypeName = "numeric")]
        public decimal LaiSuat1 { get; set; }

        [Column("lai_suat2", TypeName = "numeric")]
        public decimal LaiSuat2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_ku1")]
        public string NhomKheUoc1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_ku2")]
        public string NhomKheUoc2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_ku3")]
        public string NhomKheUoc3 { get; set; }

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

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [StringLength(20)]
        [Column("so_ku")]
        public string SoKheUoc { get; set; }

        [StringLength(16)]
        [Column("ma_kh")]
        public string MaKhachHang { get; set; }

        [StringLength(64)]
        [Column("Tk_kc")]
        public string TaiKhoan_KC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Allnx")]
    public partial class Allnx
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_lnx", Order = 0)]
        public string MaLoaiNhapXuat { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_loai")]
        public string TenLoai { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_loai2")]
        public string TenLoai2 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("loai", Order = 1)]
        public string Loai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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


    [Table("Allo")]
    public partial class Allo
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vt", Order = 0)]
        public string MaVatTu { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_lo", Order = 1)]
        public string MaLo { get; set; }

        [StringLength(48)]
        [Column("ten_lo")]
        public string TenLo { get; set; }

        [StringLength(48)]
        [Column("ten_lo2")]
        public string TenLo2 { get; set; }

        [Column("ngay_nhap", TypeName = "smalldatetime")]
        public DateTime? NgayNhap { get; set; }

        [Column("ngay_sx", TypeName = "smalldatetime")]
        public DateTime? NgaySanXuat { get; set; }

        [Column("ngay_bdsd", TypeName = "smalldatetime")]
        public DateTime? Ngay_BD_SuDung { get; set; }

        [Column("ngay_kt", TypeName = "smalldatetime")]
        public DateTime? NgayKiemTra { get; set; }

        [Column("ngay_hhsd", TypeName = "smalldatetime")]
        public DateTime? NgayHetHanSuDung { get; set; }

        [Column("ngay_hhbh", TypeName = "smalldatetime")]
        public DateTime? NgayHetHanBaoHanh { get; set; }

        [StringLength(16)]
        [Column("ma_vt2")]
        public string MaVatTu2 { get; set; }

        [Column("sl_nhap", TypeName = "numeric")]
        public decimal SoLuongNhap { get; set; }

        [Column("sl_xuat", TypeName = "numeric")]
        public decimal SoLuongXuat { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

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

        [StringLength(20)]
        [Column("SO_LOSX")]
        public string SoLoSanXuat { get; set; }

        [StringLength(20)]
        [Column("SO_LODK")]
        public string SoLoDangKy { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string MaKhachHang { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALloaicc")]
    public partial class ALloaicc
    {
        ////[Key]
        [StringLength(2)]
        [Column("loai_cc0")]
        public string LoaiCongCu0 { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten")]
        public string Ten { get; set; }

        [StringLength(48)]
        [Column("ten2")]
        public string Ten2 { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

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


    [Table("Alloaick")]
    public partial class Alloaick
    {
        ////[Key]
        [StringLength(2)]
        [Column("ma_loai")]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_loai")]
        public string TenLoai { get; set; }

        [StringLength(48)]
        [Column("ten_loai2")]
        public string TenLoai2 { get; set; }

        [StringLength(2)]
        [Column("xtype")]
        public string Xtype { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

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



    [Table("Alloaivc")]
    public partial class Alloaivc
    {
        ////[Key]
        [StringLength(2)]
        [Column("ma_loai")]
        public string MaLoai { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_loai")]
        public string TenLoai { get; set; }

        [StringLength(48)]
        [Column("ten_loai2")]
        public string TenLoai2 { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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


    [Table("ALloaivt")]
    public partial class ALloaivt
    {
        ////[Key]
        [StringLength(2)]
        [Column("loai_vt")]
        public string Loai_VatTu { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_loai")]
        public string TenLoai { get; set; }

        [StringLength(48)]
        [Column("ten_loai2")]
        public string TenLoai2 { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

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


    [Table("Almagd")]
    public partial class Almagd
    {
        ////[Key]
        [StringLength(3)]
        [Column("ma_ct_me", Order = 0)]
        public string MaCtMe { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_ct", Order = 1)]
        public string MaCt { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("ma_gd", Order = 2)]
        public string MaGd { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_gd", Order = 3)]
        public string TenGd { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_gd2", Order = 4)]
        public string TenGd2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("form", Order = 5)]
        public string Form { get; set; }

        //////[Key]
        [Column("tk_cn", Order = 6)]
        public byte TkCn { get; set; }

        //////[Key]
        [StringLength(2)]
        [Column("loai_ct", Order = 7)]
        public string LoaiCt { get; set; }
        public Guid UID { get; set; }
    }


    [Table("Almagia")]
    public partial class Almagia
    {
        [Key]
        [StringLength(8)]
        [Column("ma_gia")]
        public string MaGia { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_gia")]
        public string TenGia { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_gia2")]
        public string TenGia2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Loai")]
        public string Loai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public decimal CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public decimal ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALMAUHD")]
    public partial class ALMAUHD
    {
        ////[Key]
        [StringLength(20)]
        [Column("ma_mauhd")]
        public string MaMauHoaDon { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ten_mauhd")]
        public string TenMauHoaDon { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ten_mauhd2")]
        public string TenMauHoaDon2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("loai_mau")]
        public string LoaiMau { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

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


    [Table("ALnhCC")]
    public partial class ALnhCC
    {
        ////[Key]
        [Column("loai_nh", Order = 0, TypeName = "numeric")]
        public decimal LoaiNhom { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string MaNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string TenNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string TenNhom2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0", TypeName = "numeric")]
        public decimal NguoiNhap { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2", TypeName = "numeric")]
        public decimal? NguoiSua { get; set; }

        public Guid UID { get; set; }
    }


    public partial class ALnhdvc
    {
        //////[Key]
        [Column("loai_nh", Order = 0)]
        public byte loai_nh { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("ten_nh", Order = 2)]
        public string ten_nh { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("ten_nh2", Order = 3)]
        public string ten_nh2 { get; set; }

        ////[Key]
        [Column("dia_chi", Order = 4)]
        public string dia_chi { get; set; }

        ////[Key]
        [Column("dia_chi2", Order = 5)]
        public string dia_chi2 { get; set; }

        ////[Key]
        [Column("date0", Order = 6, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time0", Order = 7)]
        public string time0 { get; set; }

        ////[Key]
        [Column("user_id0", Order = 8)]
        public byte user_id0 { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("status", Order = 9)]
        public string status { get; set; }

        ////[Key]
        [Column("date2", Order = 10, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time2", Order = 11)]
        public string time2 { get; set; }

        ////[Key]
        [Column("user_id2", Order = 12)]
        public byte user_id2 { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("CHECK_SYNC", Order = 13)]
        public string CHECK_SYNC { get; set; }
    }


    [Table("ALnhhd")]
    public partial class ALnhhd
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte loai_nh { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string ten_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string ten_nh2 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }

    [Table("ALnhkh")]
    public partial class ALnhkh
    {
        ////[Key]
        
        [Column("loai_nh", Order = 0)]
        public byte loai_nh { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        ////[Key]
        [StringLength(48)]
        [Column("ten_nh", Order = 2)]
        public string ten_nh { get; set; }

        ////[Key]
        [StringLength(48)]
        [Column("ten_nh2", Order = 3)]
        public string ten_nh2 { get; set; }

        ////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string time0 { get; set; }

        ////[Key]
        [Column("user_id0", Order = 6)]
        public byte user_id0 { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("status", Order = 7)]
        public string status { get; set; }

        ////[Key]
        [Column("date2", Order = 8, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time2", Order = 9)]
        public string time2 { get; set; }

        ////[Key]
        [Column("user_id2", Order = 10)]
        public byte user_id2 { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("CHECK_SYNC", Order = 11)]
        public string CHECK_SYNC { get; set; }

        //////[Key]
        //[Column(Order = 12)]
        public Guid UID { get; set; }
    }


    public partial class Alnhkh2
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_nh")]
        public string MaNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string TenNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string TenNhom2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

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


    [Table("Alnhku")]
    public partial class Alnhku
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte LoaiNhom { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string MaNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh", Order = 2)]
        public string TenNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh2", Order = 3)]
        public string TenNhom2 { get; set; }

        //////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string CreatedTime { get; set; }

        //////[Key]
        [Column("user_id0", Order = 6)]
        public byte CreatedUserId { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 7)]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alnhphi")]
    public partial class Alnhphi
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte loai_nh { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string ten_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string ten_nh2 { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    public partial class ALnht
    {
        ////[Key]
        [Column("loai_nh", Order = 0, TypeName = "numeric")]
        public decimal loai_nh { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string ten_nh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string ten_nh2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0", TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2", TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ALnhtk")]
    public partial class ALnhtk
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte LoaiNhom { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string MaNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh", Order = 2)]
        public string TenNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh2", Order = 3)]
        public string TenNhom2 { get; set; }

        //////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string CreatedTime { get; set; }

        //////[Key]
        [Column("user_id0", Order = 6)]
        public byte CreatedUserId { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 7)]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

        public Guid UID { get; set; }
    }


    public partial class ALnhtk0
    {
        ////[Key]
        [StringLength(4)]
        [Column("ma_nh")]
        public string MaNhom { get; set; }

        [Required]
        [StringLength(96)]
        [Column("ten_nh")]
        public string TenNhom { get; set; }

        [Required]
        [StringLength(96)]
        [Column("ten_nh2")]
        public string TenNhom2 { get; set; }
        public Guid UID { get; set; }
    }



    [Table("ALnhvt")]
    public partial class ALnhvt
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte LoaiNhom { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string MaNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh", Order = 2)]
        public string TenNhom { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh2", Order = 3)]
        public string TenNhom2 { get; set; }

        //////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string CreatedTime { get; set; }

        //////[Key]
        [Column("user_id0", Order = 6)]
        public byte CreatedUserId { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 7)]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

        //////[Key]
        [StringLength(100)]
        [Column("CHECK_SYNC", Order = 8)]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    public partial class Alnhvt2
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_nh")]
        public string MaNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh")]
        public string TenNhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nh2")]
        public string TenNhom2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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


    [Table("Alnhvv")]
    public partial class Alnhvv
    {
        ////[Key]
        [Column("loai_nh", Order = 0)]
        public byte loai_nh { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_nh", Order = 1)]
        public string ma_nh { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh", Order = 2)]
        public string ten_nh { get; set; }

        //////[Key]
        [StringLength(48)]
        [Column("ten_nh2", Order = 3)]
        public string ten_nh2 { get; set; }

        //////[Key]
        [Column("date0", Order = 4, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 5)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 6)]
        public byte user_id0 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 7)]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }
    }


    [Table("ALnhytcp")]
    public partial class ALnhytcp
    {
        ////[Key]
        [StringLength(2)]
        [Column("nhom")]
        public string nhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nhom")]
        public string ten_nhom { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nhom2")]
        public string ten_nhom2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0", TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2", TypeName = "numeric")]
        public decimal? user_id2 { get; set; }
    }


    [Table("ALnk")]
    public partial class ALnk
    {
        [Required]
        [StringLength(3)]
        [Column("Ma_ct")]
        public string Ma_ct { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nk")]
        public string ten_nk { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nk2")]
        public string ten_nk2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }
    }


    [Table("Alnt")]
    public partial class Alnt
    {
        [Key]
        [StringLength(3)]
        [Column("ma_nt")]
        public string MaNt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ten_nt")]
        public string TenNt { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        
        [Column("user_id0", TypeName = "numeric")]
        public decimal CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string STATUS { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ten_nt2")]
        public string TenNt2 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        
        [Column("user_id2", TypeName = "numeric")]
        public decimal? ModifiedUserId { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_pscl_no")]
        public string TkPsclNo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_pscl_co")]
        public string TkPsclCo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_dgcl_no")]
        public string TkDgclNo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_dgcl_co")]
        public string TkDgclCo { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alnv")]
    public partial class Alnv
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_nv")]
        public string MaNguonVon { get; set; }

        [Required]
        [StringLength(24)]
        [Column("ten_nv")]
        public string TenNguonVon { get; set; }

        [Required]
        [StringLength(24)]
        [Column("ten_nv2")]
        public string TenNguonVon2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0", TypeName = "numeric")]
        public decimal NguoiNhap { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2", TypeName = "numeric")]
        public decimal? NguoiSua { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alnvien")]
    public partial class Alnvien
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nvien")]
        public string ten_nvien { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_nvien2")]
        public string ten_nvien2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Loai")]
        public string Loai { get; set; }

        
        [Column("han_tt", TypeName = "numeric")]
        public decimal? han_tt { get; set; }

        
        [Column("ghi_chu", TypeName = "text")]
        public string ghi_chu { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
        public Guid UID { get; set; }
    }

    [Table("ALpb")]
    public partial class ALpb
    {
        
        [Column("Nam", TypeName = "numeric")]
        public decimal Nam { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Tag")]
        public string Tag { get; set; }

        
        [Column("SttRec", TypeName = "numeric")]
        public decimal Stt { get; set; }

        //////[Key]
        [StringLength(13)]
        [Column("SttRec")]
        public string SttRec { get; set; }

        [Required]
        [StringLength(32)]
        [Column("TenBt")]
        public string TenBt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("Tk")]
        public string Tk { get; set; }

        [Column("LoaiPb")]
        public byte LoaiPb { get; set; }

        [Column("PsNoCo")]
        public byte PsNoCo { get; set; }

        [Required]
        [StringLength(12)]
        [Column("SoCt")]
        public string SoCt { get; set; }

        
        [Column("TienNt", TypeName = "numeric")]
        public decimal TienNt { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MaNt")]
        public string MaNt { get; set; }

        
        [Column("TyGia", TypeName = "numeric")]
        public decimal TyGia { get; set; }

        
        [Column("Tien", TypeName = "numeric")]
        public decimal Tien { get; set; }

        [Required]
        [StringLength(8)]
        [Column("MaDvcs")]
        public string MaDvcs { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec01")]
        public string SttRec01 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec02")]
        public string SttRec02 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec03")]
        public string SttRec03 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec04")]
        public string SttRec04 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec05")]
        public string SttRec05 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec06")]
        public string SttRec06 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec07")]
        public string SttRec07 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec08")]
        public string SttRec08 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec09")]
        public string SttRec09 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec10")]
        public string SttRec10 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec11")]
        public string SttRec11 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("SttRec12")]
        public string SttRec12 { get; set; }

        [Column("Date0", TypeName = "smalldatetime")]
        public DateTime Date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("Time0")]
        public string Time0 { get; set; }

        [Column("UserId0")]
        public byte UserId0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Status")]
        public string Status { get; set; }

        [Column("Date2", TypeName = "smalldatetime")]
        public DateTime? Date2 { get; set; }

        [StringLength(8)]
        [Column("Time2")]
        public string Time2 { get; set; }

        [Column("UserId2")]
        public byte? UserId2 { get; set; }

        [StringLength(12)]
        [Column("SoCt01")]
        public string SoCt01 { get; set; }

        [StringLength(12)]
        [Column("SoCt02")]
        public string SoCt02 { get; set; }

        [StringLength(12)]
        [Column("SoCt03")]
        public string SoCt03 { get; set; }

        [StringLength(12)]
        [Column("SoCt04")]
        public string SoCt04 { get; set; }

        [StringLength(12)]
        [Column("SoCt05")]
        public string SoCt05 { get; set; }

        [StringLength(12)]
        [Column("SoCt06")]
        public string SoCt06 { get; set; }

        [StringLength(12)]
        [Column("SoCt07")]
        public string SoCt07 { get; set; }

        [StringLength(12)]
        [Column("SoCt08")]
        public string SoCt08 { get; set; }

        [StringLength(12)]
        [Column("SoCt09")]
        public string SoCt09 { get; set; }

        [StringLength(12)]
        [Column("SoCt10")]
        public string SoCt10 { get; set; }

        [StringLength(12)]
        [Column("SoCt11")]
        public string SoCt11 { get; set; }

        [StringLength(12)]
        [Column("SoCt12")]
        public string SoCt12 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Loai")]
        public string Loai { get; set; }

        [Required]
        [StringLength(40)]
        [Column("TenLoai")]
        public string TenLoai { get; set; }

        [Required]
        [StringLength(8)]
        [Column("MA_BPHTPH")]
        public string MA_BPHTPH { get; set; }

        [Required]
        [StringLength(1)]
        [Column("AUTO")]
        public string AUTO { get; set; }

        [Required]
        [StringLength(2)]
        [Column("LOAI_PBCP")]
        public string LOAI_PBCP { get; set; }
    }


    public partial class ALpb1
    {
        ////[Key]
        [StringLength(13)]
        [Column("stt_rec", Order = 0)]
        public string stt_rec { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk", Order = 1)]
        public string tk { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_he_so")]
        public string tk_he_so { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_vv", Order = 3)]
        public string ma_vv { get; set; }

        //////[Key]
        [Column("heso01", Order = 4, TypeName = "numeric")]
        public decimal heso01 { get; set; }

        //////[Key]
        [Column("heso02", Order = 5, TypeName = "numeric")]
        public decimal heso02 { get; set; }

        //////[Key]
        [Column("heso03", Order = 6, TypeName = "numeric")]
        public decimal heso03 { get; set; }

        //////[Key]
        [Column("heso04")]
        public decimal heso04 { get; set; }

        //////[Key]
        
        [Column("heso05", Order = 8, TypeName = "numeric")]
        public decimal heso05 { get; set; }

        //////[Key]
        
        [Column("heso06", Order = 9, TypeName = "numeric")]
        public decimal heso06 { get; set; }

        //////[Key]
        
        [Column("heso07")]
        public decimal heso07 { get; set; }

        //////[Key]
        
        [Column("heso08", Order = 11, TypeName = "numeric")]
        public decimal heso08 { get; set; }

        //////[Key]
        
        [Column("heso09", Order = 12, TypeName = "numeric")]
        public decimal heso09 { get; set; }

        //////[Key]
        
        [Column("heso10", Order = 13, TypeName = "numeric")]
        public decimal heso10 { get; set; }

        //////[Key]
        
        [Column("heso11", Order = 14, TypeName = "numeric")]
        public decimal heso11 { get; set; }

        //////[Key]
        
        [Column("heso12", Order = 15, TypeName = "numeric")]
        public decimal heso12 { get; set; }

        //////[Key]
        
        [Column("date0", Order = 16, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 17)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 18)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("status", Order = 19)]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td")]
        public string ma_td { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_bpht",Order = 21)]
        public string ma_bpht { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_sp",Order = 22)]
        public string ma_sp { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_hd", Order = 23)]
        public string ma_hd { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_ku", Order = 24)]
        public string ma_ku { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_phi",Order = 25)]
        public string ma_phi { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td2",Order = 26)]
        public string ma_td2 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td3",Order = 27)]
        public string ma_td3 { get; set; }
    }

    [Table("Alphi")]
    public partial class Alphi
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_phi")]
        public string ma_phi { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_phi")]
        public string ten_phi { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_phi2")]
        public string ten_phi2 { get; set; }

        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(8)]
        [Column("ma_nvbh")]
        public string ma_nvbh { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [Column("loai_phi")]
        public byte? loai_phi { get; set; }

        [StringLength(8)]
        [Column("nh_phi1")]
        public string nh_phi1 { get; set; }

        [StringLength(8)]
        [Column("nh_phi2")]
        public string nh_phi2 { get; set; }

        [StringLength(8)]
        [Column("nh_phi3")]
        public string nh_phi3 { get; set; }
        [Column("ngay_phi", TypeName = "smalldatetime")]
        
        public DateTime? ngay_phi { get; set; }

        [StringLength(16)]
        [Column("so_phi")]
        public string so_phi { get; set; }

        
        [Column("ngay_phi1", TypeName = "smalldatetime")]
        public DateTime? ngay_phi1 { get; set; }

        
        [Column("ngay_phi2", TypeName = "smalldatetime")]
        public DateTime? ngay_phi2 { get; set; }

        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("tien_nt", TypeName = "numeric")]
        public decimal? tien_nt { get; set; }

        [Column("tien", TypeName = "numeric")]
        public decimal? tien { get; set; }

        
        [Column("tien_gt_nt", TypeName = "numeric")]
        public decimal? tien_gt_nt { get; set; }

        
        [Column("tien_gt", TypeName = "numeric")]
        public decimal? tien_gt { get; set; }

        [Column("tinh_trang")]
        public byte? tinh_trang { get; set; }

        
        [Column("kl_kh", TypeName = "numeric")]
        public decimal? kl_kh { get; set; }

        
        [Column("kl_th", TypeName = "numeric")]
        public decimal? kl_th { get; set; }

        
        [Column("ghi_chu", TypeName = "ntext")]
        public string ghi_chu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }


        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Alphuong")]
    public partial class Alphuong
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_phuong")]
        public string MaPhuong { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ph")]
        public string TenPhuong { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ph2")]
        public string TenPhuong2 { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

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

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALplcc")]
    public partial class ALplcc
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_loai")]
        public string MaLoai { get; set; }

        [StringLength(48)]
        [Column("ten_loai")]
        public string Ten_Loai { get; set; }

        [StringLength(48)]
        [Column("ten_loai2")]
        public string Ten_Loai2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

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


    public partial class ALplt
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_loai")]
        public string ma_loai { get; set; }

        [StringLength(48)]
        [Column("ten_loai")]
        public string ten_loai { get; set; }

        [StringLength(48)]
        [Column("ten_loai2")]
        public string ten_loai2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Alytcp")]
    public partial class Alytcp
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_ytcp")]
        public string ma_ytcp { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ytcp")]
        public string ten_ytcp { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_ytcp2")]
        public string ten_ytcp2 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_no")]
        public string tk_no { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_co")]
        public string tk_co { get; set; }

        [Required]
        [StringLength(2)]
        [Column("nhom")]
        public string nhom { get; set; }
        [Column("ddck_ck")]
        public byte ddck_ck { get; set; }

        [Required]
        [StringLength(12)]
        [Column("ten_ngan")]
        public string ten_ngan { get; set; }

        [Required]
        [StringLength(12)]
        [Column("ten_ngan2")]
        public string ten_ngan2 { get; set; }

        
        [Column("ghi_chu", TypeName = "text")]
        public string ghi_chu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

         [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }


        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }


        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }
    }


    [Table("Alvv")]
    public partial class Alvv
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vv")]
        public string ten_vv { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vv2")]
        public string ten_vv2 { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vv1")]
        public string nh_vv1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vv2")]
        public string nh_vv2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vv3")]
        public string nh_vv3 { get; set; }

        
        [Column("ngay_vv1", TypeName = "smalldatetime")]
        public DateTime? ngay_vv1 { get; set; }

        
        [Column("ngay_vv2", TypeName = "smalldatetime")]
        public DateTime? ngay_vv2 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("tien_nt", TypeName = "money")]
        public decimal tien_nt { get; set; }

        
        [Column("tien", TypeName = "money")]
        public decimal tien { get; set; }

        
        [Required]
        [Column("ghi_chu", TypeName = "text")]
        public string ghi_chu { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [StringLength(64)]
        [Column("tk_kc")]
        public string tk_kc { get; set; }

        [StringLength(16)]
        [Column("ma_td1")]
        public string ma_td1 { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3,", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }
    }


    [Table("ALvttg")]
    public partial class ALvttg
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_vttg")]
        public string MaVatTuTheGioi { get; set; }

        [Required]
        [StringLength(16)]
        [Column("part_no")]
        public string PartNo { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vttg")]
        public string ten_vttg { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vttg2")]
        public string ten_vttg2 { get; set; }

        [Required]
        [StringLength(10)]
        [Column("dvt")]
        public string DonViTinh { get; set; }

        [StringLength(10)]
        [Column("dvt1")]
        public string DonViTinh1 { get; set; }

        [Column("he_so1", TypeName = "numeric")]
        public decimal? HeSo1 { get; set; }

        [Column("vt_ton_kho")]

        public byte VatTuTonKho { get; set; }

        [Column("gia_ton")]
        public byte GiaTon { get; set; }

        [Column("sua_tk_vt")]

        public byte? ChoPhepSuaTaiKhoanVatTu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_cl_vt")]
        public string TaiKhoan_CL_VT { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_vt")]
        public string TaiKhoanVatTu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_gv")]
        public string TaiKhoanGiaVon { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_dt")]
        public string TaiKhoanDoanhThu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_tl")]
        public string TaiKhoanTraLai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_spdd")]
        public string TaiKhoanSanPhamDoDang { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt1")]
        public string NhomVatTu1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt2")]
        public string NhomVatTu2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt3")]
        public string NhomVatTu3 { get; set; }

        [Column("sl_min", TypeName = "numeric")]
        public decimal SoLuongToiThieu { get; set; }

        [Column("sl_max", TypeName = "numeric")]
        public decimal SoLuongToiDa { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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

        [StringLength(30)]
        [Column("Short_name")]
        public string ShortName { get; set; }

        [StringLength(13)]
        [Column("Bar_code")]
        public string BarCode { get; set; }

        [StringLength(2)]
        [Column("Loai_vt")]
        public string Loai_VatTu { get; set; }

        [StringLength(8)]
        [Column("Tt_vt")]
        public string TinhTrangVatTu { get; set; }

        [StringLength(1)]
        [Column("Nhieu_dvt")]
        public string NhieuDonViTinh { get; set; }

        [StringLength(1)]
        [Column("Lo_yn")]
        public string Lo_yn { get; set; }

        [StringLength(1)]
        [Column("Kk_yn")]
        public string KK_YN { get; set; }

        [Column("Weight", TypeName = "numeric")]
        public decimal? TrongLuong { get; set; }

        [StringLength(10)]
        [Column("DvtWeight")]
        public string DonViTrongLuong { get; set; }

        [Column("Weight0", TypeName = "numeric")]
        public decimal? TrongLuong0 { get; set; }

        [StringLength(10)]
        [Column("DvtWeight0")]
        public string DonViTrongLuong0 { get; set; }

        [Column("Length", TypeName = "numeric")]
        public decimal? ChieuDai { get; set; }


        [Column("Width", TypeName = "numeric")]
        public decimal? ChieuNgang { get; set; }


        [Column("Height", TypeName = "numeric")]
        public decimal? ChieuCao { get; set; }


        [Column("Diamet", TypeName = "numeric")]
        public decimal? Diamet { get; set; }

        [StringLength(10)]
        [Column("DvtLength")]
        public string DonViChieuDai { get; set; }

        [StringLength(10)]
        [Column("DvtWidth")]
        public string DonViChieuRong { get; set; }

        [StringLength(10)]
        [Column("DvtHeight")]
        public string DonViChieuCao { get; set; }

        [StringLength(10)]
        [Column("DvtDiamet")]
        public string DonViDiamet { get; set; }

        [StringLength(16)]
        [Column("Size")]
        public string Size { get; set; }

        [StringLength(16)]
        [Column("Color")]
        public string MauSac { get; set; }

        [StringLength(16)]
        [Column("Style")]
        public string Kieu { get; set; }

        [StringLength(8)]
        [Column("Ma_qg")]
        public string MaQuocGia { get; set; }


        [Column("Packs", TypeName = "numeric")]
        public decimal? Packs { get; set; }


        [Column("Packs1", TypeName = "numeric")]
        public decimal? Packs1 { get; set; }

        [StringLength(1)]
        [Column("abc_code")]
        public string ABCCode { get; set; }

        [StringLength(10)]
        [Column("Dvtpacks")]
        public string DonViGoi { get; set; }


        [Column("Cycle_kk", TypeName = "numeric")]
        public decimal? Cycle_KK { get; set; }

        [StringLength(8)]
        [Column("Ma_vitri")]
        public string MaViTri { get; set; }

        [StringLength(8)]
        [Column("Ma_kho")]
        public string MaKho { get; set; }

        [Column("Han_sd", TypeName = "numeric")]
        public decimal? HanSuDung { get; set; }

        [Column("Han_bh", TypeName = "numeric")]
        public decimal? HanBaoHanh { get; set; }

        [StringLength(1)]
        [Column("Kieu_lo")]
        public string KieuLo { get; set; }

        [StringLength(1)]
        [Column("Cach_xuat")]
        public string CachXuat { get; set; }

        [StringLength(8)]
        [Column("Lma_nvien")]
        public string NhanVienCodeL { get; set; }


        [Column("LdatePur", TypeName = "numeric")]
        public decimal? NgayMuaL { get; set; }


        [Column("LdateQc", TypeName = "numeric")]
        public decimal? LdateQc { get; set; }


        [Column("Lso_qty", TypeName = "numeric")]
        public decimal? Lsoqty { get; set; }


        [Column("Lso_qtymin", TypeName = "numeric")]
        public decimal? Lso_qtymin { get; set; }


        [Column("Lso_qtymax", TypeName = "numeric")]
        public decimal? Lso_qtymax { get; set; }


        [Column("LCycle", TypeName = "numeric")]
        public decimal? LCycle { get; set; }

        [StringLength(1)]
        [Column("Lpolicy")]
        public string Lpolicy { get; set; }

        [StringLength(8)]
        [Column("Pma_nvien")]
        public string NhanVienCodeP { get; set; }

        [StringLength(8)]
        [Column("Pma_khc")]
        public string KhachHangCodePC { get; set; }

        [StringLength(8)]
        [Column("Pma_khp")]
        public string KhachHangCodePP { get; set; }

        [StringLength(8)]
        [Column("Pma_khl")]
        public string KhachHangCodePL { get; set; }

        [StringLength(128)]
        [Column("Prating")]
        public string Prating { get; set; }


        [Column("Pquality", TypeName = "numeric")]
        public decimal? ChatLuongP { get; set; }


        [Column("Pquanlity", TypeName = "numeric")]
        public decimal? SoLuongP { get; set; }


        [Column("Pdeliver", TypeName = "numeric")]
        public decimal? Pdeliver { get; set; }


        [Column("PFlex", TypeName = "numeric")]
        public decimal? PFlex { get; set; }

        [Column("Ptech", TypeName = "numeric")]
        public decimal? Ptech { get; set; }

        [StringLength(8)]
        [Column("nh_vt9")]
        public string NhomVatTu9 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string MaThue { get; set; }

        [StringLength(8)]
        [Column("ma_thueNk")]
        public string MaThueNhapKhau { get; set; }

        [StringLength(16)]
        [Column("tk_ck")]
        public string TaiKhoanChietKhau { get; set; }

        [StringLength(1)]
        [Column("date_yn")]
        public string TheoDoiDate { get; set; }

        [Required]
        [StringLength(16)]
        [Column("TK_CP")]
        public string TaiKhoan_CP { get; set; }

        [Required]
        [StringLength(8)]
        [Column("MA_BPHT")]
        public string MaBoPhanHachToan { get; set; }

        [StringLength(1)]
        [Column("VITRI_YN")]
        public string TheoDoiViTri { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_KHTG")]
        public string MaKhachHangTheGioi { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALvt")]
    public partial class ALvt
    {
        [Key]
        [StringLength(16)]
        [Column("ma_vt")]
        public string MaVatTu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("part_no")]
        public string PartNo { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ten_vt")]
        public string TenVatTu { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ten_vt2")]
        public string TenVatTu2 { get; set; }

        [Required]
        [StringLength(10)]
        [Column("dvt")]
        public string DonViTinh { get; set; }

        [StringLength(10)]
        [Column("dvt1")]
        public string DonViTinh1 { get; set; }

        [Column("he_so1", TypeName = "numeric")]
        public decimal? HeSo1 { get; set; }

        [Column("vt_ton_kho")]
        public byte VatTuTonKho { get; set; }

        [Column("gia_ton")]
        public byte GiaTon { get; set; }

        [Column("sua_tk_vt")]
        public byte? ChoPhepSuaTaiKhoanVatTu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_cl_vt")]
        public string TaiKhoan_CL_VT { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_vt")]
        public string TaiKhoanVatTu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_gv")]
        public string TaiKhoanGiaVon { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_dt")]
        public string TaiKhoanDoanhThu { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_tl")]
        public string TaiKhoanTraLai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_spdd")]
        public string TaiKhoanSanPhamDoDang { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt1")]
        public string NhomVatTu1 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt2")]
        public string NhomVatTu2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_vt3")]
        public string NhomVatTu3 { get; set; }

        [Column("sl_min", TypeName = "numeric")]
        public decimal SoLuongToiThieu { get; set; }

        [Column("sl_max", TypeName = "numeric")]
        public decimal SoLuongToiDa { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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

        [StringLength(30)]
        [Column("Short_name")]
        public string ShortName { get; set; }

        [StringLength(20)]
        [Column("Bar_code")]
        public string BarCode { get; set; }

        [StringLength(2)]
        [Column("Loai_vt")]
        public string Loai_VatTu { get; set; }

        [StringLength(8)]
        [Column("Tt_vt")]
        public string TinhTrangVatTu { get; set; }

        [StringLength(1)]
        [Column("Nhieu_dvt")]
        public string NhieuDonViTinh { get; set; }

        [StringLength(1)]
        [Column("Lo_yn")]
        public string IsParcel { get; set; }

        [StringLength(1)]
        [Column("Kk_yn")]
        public string KK_YN { get; set; }

        [Column("Weight", TypeName = "numeric")]
        public decimal? TrongLuong { get; set; }

        [StringLength(10)]
        [Column("DvtWeight")]
        public string DonViTinhTrongLuong { get; set; }

        [Column("Weight0", TypeName = "numeric")]
        public decimal? TrongLuong0 { get; set; }

        [StringLength(10)]
        [Column("DvtWeight0")]
        public string DonViTinhTrongLuong0 { get; set; }

        [Column("Length", TypeName = "numeric")]
        public decimal? ChieuDai { get; set; }

        [Column("Width", TypeName = "numeric")]
        public decimal? ChieuNgang { get; set; }

        [Column("Height", TypeName = "numeric")]
        public decimal? ChieuCao { get; set; }

        [Column("Diamet", TypeName = "numeric")]
        public decimal? Diamet { get; set; }

        [StringLength(10)]
        [Column("DvtLength")]
        public string DonViTinhChieuDai { get; set; }

        [StringLength(10)]
        [Column("DvtWidth")]
        public string DonViTinhChieuNgang { get; set; }

        [StringLength(10)]
        [Column("DvtHeight")]
        public string DonViTinhChieuCao { get; set; }

        [StringLength(10)]
        [Column("DvtDiamet")]
        public string DonViTinhDiamet { get; set; }

        [StringLength(16)]
        [Column("Size")]
        public string Size { get; set; }

        [StringLength(16)]
        [Column("Color")]
        public string MauSac { get; set; }

        [StringLength(16)]
        [Column("Style")]
        public string Kieu { get; set; }

        [StringLength(8)]
        [Column("Ma_qg")]
        public string MaQuocGia { get; set; }

        [Column("Packs", TypeName = "numeric")]
        public decimal? Packs { get; set; }

        [Column("Packs1", TypeName = "numeric")]
        public decimal? Packs1 { get; set; }

        [StringLength(1)]
        [Column("abc_code")]
        public string ABCCode { get; set; }

        [StringLength(10)]
        [Column("Dvtpacks")]
        public string DonViTinhPacks { get; set; }

        [Column("Cycle_kk", TypeName = "numeric")]
        public decimal? Cycle_KK { get; set; }

        [StringLength(8)]
        [Column("Ma_vitri")]
        public string MaViTri { get; set; }

        [StringLength(8)]
        [Column("Ma_kho")]
        public string MaKho { get; set; }

        [Column("Han_sd", TypeName = "numeric")]
        public decimal? HanSuDung { get; set; }

        [Column("Han_bh", TypeName = "numeric")]
        public decimal? HanBaoHanh { get; set; }

        [StringLength(1)]
        [Column("Kieu_lo")]
        public string KieuLo { get; set; }

        [StringLength(1)]
        [Column("Cach_xuat")]
        public string CachXuat { get; set; }

        [StringLength(8)]
        [Column("Lma_nvien")]
        public string NhanVienCodeL { get; set; }

        [Column("LdatePur", TypeName = "numeric")]
        public decimal? NgayMuaL { get; set; }

        [Column("LdateQc", TypeName = "numeric")]
        public decimal? Ldateqc { get; set; }

        [Column("Lso_qty", TypeName = "numeric")]
        public decimal? Lsoqty { get; set; }

        [Column("Lso_qtymin", TypeName = "numeric")]
        public decimal? Lsoqtymin { get; set; }

        [Column("Lso_qtymax", TypeName = "numeric")]
        public decimal? Lsoqtymax { get; set; }

        [Column("LCycle", TypeName = "numeric")]
        public decimal? Lcycle { get; set; }

        [StringLength(1)]
        [Column("Lpolicy")]
        public string Lpolicy { get; set; }

        [StringLength(8)]
        [Column("Pma_nvien")]
        public string NhanVienCodeP { get; set; }

        [StringLength(8)]
        [Column("Pma_khc")]
        public string KhachHangCodePC { get; set; }

        [StringLength(8)]
        [Column("Pma_khp")]
        public string KhachHangCodePP { get; set; }

        [StringLength(8)]
        [Column("Pma_khl")]
        public string KhachHangCodePL { get; set; }

        [StringLength(128)]
        [Column("Prating")]
        public string Prating { get; set; }

        [Column("Pquality", TypeName = "numeric")]
        public decimal? ChatLuongP { get; set; }

        [Column("Pquanlity", TypeName = "numeric")]
        public decimal? SoLuongP { get; set; }

        [Column("Pdeliver", TypeName = "numeric")]
        public decimal? Pdeliver { get; set; }

        [Column("PFlex", TypeName = "numeric")]
        public decimal? PFlex { get; set; }

        [Column("Ptech", TypeName = "numeric")]
        public decimal? Ptech { get; set; }

        [StringLength(8)]
        [Column("nh_vt9")]
        public string NhomVatTu9 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string MaThue { get; set; }

        [StringLength(8)]
        [Column("ma_thueNk")]
        public string MaThueNhapKhau { get; set; }

        [StringLength(16)]
        [Column("tk_ck")]
        public string TaiKhoanChietKhau { get; set; }

        [StringLength(1)]
        [Column("date_yn")]
        public string TheoDoiDate { get; set; }

        [Required]
        [StringLength(16)]
        [Column("TK_CP")]
        public string TaiKhoan_CP { get; set; }

        [Required]
        [StringLength(8)]
        [Column("MA_BPHT")]
        public string MaBoPhanHachToan { get; set; }

        [StringLength(1)]
        [Column("VITRI_YN")]
        public string TheoDoiViTri { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_VTTG")]
        public string MaVatTuTheGioi { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_KHTG")]
        public string MaKhachHangTheGioi { get; set; }

        [Required]
        [StringLength(64)]
        [Column("TEN_KHTG")]
        public string TenKhachHangTheGioi { get; set; }

        [Required]
        [StringLength(48)]
        [Column("TEN_QG")]
        public string TenQuocGia { get; set; }

        [Column("Thue_suat", TypeName = "numeric")]
        public decimal? ThueSuat { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_VT4")]
        public string NhomVatTu4 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_VT5")]
        public string NhomVatTu5 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_VT6")]
        public string NhomVatTu6 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_VT7")]
        public string NhomVatTu7 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("NH_VT8")]
        public string NhomVatTu8 { get; set; }

        [Required]
        [StringLength(100)]
        [Column("MODEL")]
        public string MODEL { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_VV")]
        public string MaVuViec { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALvitri")]
    public partial class ALvitri
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_kho", Order = 0)]
        public string MaKho { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_vitri", Order = 1)]
        public string MaViTri { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vitri")]
        public string TenViTri { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vitri2")]
        public string TenViTri2 { get; set; }

        [Column("stt_ntxt")]
        public byte? STT_NhapTruocXuatTruoc { get; set; }

        [StringLength(8)]
        [Column("ma_loai")]
        public string MaLoai { get; set; }

        [Column("kieu_nhap", TypeName = "numeric")]
        public decimal? KieuNhap { get; set; }

        [Column("kieu_xuat", TypeName = "numeric")]
        public decimal? KieuXuat { get; set; }

        [Column("kieu_ban", TypeName = "numeric")]
        public decimal? KieuBan { get; set; }

        [StringLength(16)]
        [Column("ma_vt")]
        public string MaVatTu { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

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

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Alvc")]
    public partial class Alvc
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_vc")]
        public string MaVanChuyen { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_vc")]
        public string TenVanChuyen { get; set; }

        [StringLength(48)]
        [Column("ten_vc2")]
        public string TenVanChuyen2 { get; set; }

        [StringLength(8)]
        [Column("loai_vc")]
        public string LoaiVanChuyen { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string OngBa { get; set; }

        [Column("height", TypeName = "numeric")]
        public decimal? ChieuCao { get; set; }


        [Column("length", TypeName = "numeric")]
        public decimal? ChieuDai { get; set; }

        [Column("volume", TypeName = "numeric")]
        public decimal? TheTich { get; set; }

        [Column("weight", TypeName = "numeric")]
        public decimal? TrongLuong { get; set; }

        [Column("width", TypeName = "numeric")]
        public decimal? ChieuNgang { get; set; }

        [StringLength(10)]
        [Column("dvtheight")]
        public string DonViChieuCao { get; set; }

        [StringLength(10)]
        [Column("dvtlength")]
        public string DonViChieuDai { get; set; }

        [StringLength(10)]
        [Column("dvtvolume")]
        public string DonViTinhTheTich { get; set; }

        [StringLength(10)]
        [Column("dvtweight")]
        public string DonViTrongLuong { get; set; }

        [StringLength(10)]
        [Column("dvtwidth")]
        public string DonViChieuRong { get; set; }

        [Column("tg_xephang", TypeName = "numeric")]
        public decimal? ThoiGianXepHang { get; set; }

        [Column("tg_dohang", TypeName = "numeric")]
        public decimal? ThoiGianDoHang { get; set; }

        [StringLength(10)]
        [Column("dvt_xep")]
        public string DonViTinh_Xep { get; set; }

        [StringLength(10)]
        [Column("dvt_do")]
        public string DonViTinh_Do { get; set; }

        [StringLength(32)]
        [Column("bien_so")]
        public string BienSo { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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


    [Table("ALttvt")]
    public partial class ALttvt
    {
        ////[Key]
        [StringLength(8)]
        [Column("Tt_vt")]
        public string TinhTrangVatTu { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tt")]
        public string TenTinhTrang { get; set; }

        [StringLength(48)]
        [Column("ten_tt2")]
        public string TenTinhTrang2 { get; set; }

        [StringLength(128)]
        [Column("ghi_chu")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

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


    [Table("Altt")]
    public partial class Altt
    {
        ////[Key]
        [StringLength(10)]
        [Column("ma_dm")]
        public string ma_dm { get; set; }

        [StringLength(128)]
        [Column("m_ma_td1")]
        public string m_ma_td1 { get; set; }

        [StringLength(128)]
        [Column("m_ma_td2")]
        public string m_ma_td2 { get; set; }

        [StringLength(128)]
        [Column("m_ma_td3")]
        public string m_ma_td3 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td1")]
        public string m_ngay_td1 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td2")]
        public string m_ngay_td2 { get; set; }

        [StringLength(128)]
        [Column("m_ngay_td3")]
        public string m_ngay_td3 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td1")]
        public string m_sl_td1 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td2")]
        public string m_sl_td2 { get; set; }

        [StringLength(128)]
        [Column("m_sl_td3")]
        public string m_sl_td3 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td1")]
        public string m_gc_td1 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td2")]
        public string m_gc_td2 { get; set; }

        [StringLength(128)]
        [Column("m_gc_td3")]
        public string m_gc_td3 { get; set; }
    }


    [Table("ALtknh")]
    public partial class ALtknh
    {
        ////[Key]
        [StringLength(16)]
        [Column("tk")]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tknh")]
        public string TaiKhoanNganHang { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tknh")]
        public string TenTaiKhoanNganHang { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tknh2")]
        public string TenTaiKhoanNganHang2 { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

        public Guid UID { get; set; }
    }


    [Table("ALtklkvv")]
    public partial class ALtklkvv
    {
        ////[Key]
        [StringLength(16)]
        [Column("tk_lkvv")]
        public string tk_lkvv { get; set; }

        [Column("no_co")]
        public byte no_co { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }
    }


    [Table("ALtklkKU")]
    public partial class ALtklkKU
    {
        ////[Key]
        [StringLength(16)]
        [Column("tk_lkKU")]
        public string tk_lkKU { get; set; }

        [Column("no_co")]
        public byte no_co { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }
    }


    public partial class Altk2
    {
        [StringLength(1)]
        [Column("type")]
        public string type { get; set; }

        [StringLength(16)]
        [Column("tk2")]
        public string tk2 { get; set; }

        [StringLength(8)]
        [Column("nh_tk2")]
        public string nh_tk2 { get; set; }

        [StringLength(48)]
        [Column("ten_tk2")]
        public string ten_tk2 { get; set; }

        [StringLength(48)]
        [Column("ten_tk22")]
        public string ten_tk22 { get; set; }

        [StringLength(1)]
        [Column("dau")]
        public string dau { get; set; }

        [Column("loai")]
        public byte loai { get; set; }

        [Column("bac")]
        public byte bac { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime? date0 { get; set; }

        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte? user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }

        //////[Key]
        public Guid UID { get; set; }
    }


    public partial class Altk1
    {
        ////[Key]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tk")]
        public string ten_tk { get; set; }

        [Required]
        [StringLength(64)]
        [Column("ds_tk")]
        public string ds_tk { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tk2")]
        public string ten_tk2 { get; set; }

        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime? date2 { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte? user_id2 { get; set; }
    }


    public partial class Altk0
    {
        ////[Key]
        [StringLength(16)]
        [Column("tk")]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tk")]
        public string TenTaiKhoan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tk2")]
        public string TenTaiKhoan2 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ten_ngan")]
        public string TenNgan { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ten_ngan2")]
        public string TenNgan2 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string MaNgoaiTe { get; set; }

        [Column("loai_tk")]
        public byte LoaiTaiKhoan { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_me")]
        public string TaiKhoanMe { get; set; }


        [Column("bac_tk")]
        public byte BacTaiKhoan { get; set; }

        [Column("tk_sc")]
        public byte TaiKhoan_SC { get; set; }

        [Column("tk_cn")]
        public byte TaiKhoanCongNo { get; set; }

        [Required]
        [StringLength(4)]
        [Column("nh_tk0")]
        public string NhomTaiKhoan0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("nh_tk2")]
        public string NhomTaiKhoan2 { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime? NgayKhoiTao { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }


        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? NguoiSua { get; set; }

        [Column("loai_cl_no", TypeName = "numeric")]
        public decimal Loai_CL_No { get; set; }

        [Column("loai_cl_co", TypeName = "numeric")]
        public decimal Loai_CL_Co { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CheckSync { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Altinh")]
    public partial class Altinh
    {
        ////[Key]
        [StringLength(16)]
        [Column("ma_tinh")]
        public string MaTinh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tinh")]
        public string TenTinh { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_tinh2")]
        public string TenTinh2 { get; set; }

        [Column("ghi_chu", TypeName = "text")]
        public string GhiChu { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte ModifiedUserId { get; set; }

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

        [Required]
        [StringLength(1)]
        [Column("LOAI")]
        public string Loai { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CHECK_SYNC")]
        public string CHECK_SYNC { get; set; }

        public Guid UID { get; set; }
    }


    [Table("Althue")]
    public partial class Althue
    {
        ////[Key]
        [StringLength(8)]
        [Column("ma_thue")]
        public string MaThue { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_thue")]
        public string TenThue { get; set; }

        [Required]
        [StringLength(48)]
        [Column("ten_thue2")]
        public string TenThue2 { get; set; }

        [Column("thue_suat", TypeName = "numeric")]
        public decimal ThueSuat { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string TaiKhoanThueCo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string TaiKhoanThueNo { get; set; }

        [Column("date0", TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string CreatedTime { get; set; }

        [Column("user_id0")]
        public byte CreatedUserId { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string TrangThai { get; set; }

        [Column("date2", TypeName = "smalldatetime")]
        public DateTime ModifiedDate { get; set; }

        [StringLength(8)]
        [Column("time2")]
        public string ModifiedTime { get; set; }

        [Column("user_id2")]
        public byte? ModifiedUserId { get; set; }

        public Guid UID { get; set; }
    }



    public partial class ARV30
    {
        ////[Key]
        [StringLength(13)]
        [Column("stt_rec", Order = 0)]
        public string stt_rec { get; set; }

        //////[Key]
        [StringLength(5)]
        [Column("stt_rec0", Order = 1)]
        public string stt_rec0 { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_ct", Order = 2)]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct", TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_ct", Order = 3)]
        public string so_ct { get; set; }

        
        [Column("ngay_ct0", TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_ct0", Order = 4)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        //////[Key]
        
        [Column("mau_bc", Order = 5, TypeName = "numeric")]
        public decimal mau_bc { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_kh", Order = 6)]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("ten_kh")]
        public string ten_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        //////[Key]
        [StringLength(18)]
        [Column("ma_so_thue", Order = 7)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        [Column("ten_vt")]
        public string ten_vt { get; set; }

        //////[Key]
        [Column("so_luong", Order = 8, TypeName = "numeric")]
        public decimal so_luong { get; set; }

        //////[Key]
        [Column("gia", Order = 9, TypeName = "numeric")]
        public decimal gia { get; set; }

        //////[Key]
        [Column("t_tien", Order = 10, TypeName = "numeric")]
        public decimal t_tien { get; set; }

        //////[Key]
        [Column("thue_suat", Order = 11, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //////[Key]
        [Column("t_thue", Order = 12, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_thue_no", Order = 13)]
        public string tk_thue_no { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_du", Order = 14)]
        public string tk_du { get; set; }

        //////[Key]
        [StringLength(32)]
        [Column("ghi_chu", Order = 15)]
        public string ghi_chu { get; set; }

        //////[Key]
        [Column("date0", Order = 16, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 17)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 18)]
        public byte user_id0 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 19)]
        public string status { get; set; }

        //////[Key]
        [Column("t_tien_nt", Order = 20, TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        //////[Key]
        [Column("t_thue_nt", Order = 21, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 22)]
        public string ma_nt { get; set; }

        //////[Key]
        [Column("gia_nt", Order = 23, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 24)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [Column("date2", Order = 25, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 26)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 27)]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_kho")]
        public string ma_kho { get; set; }

        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("Ma_kh2", Order = 28)]
        public string Ma_kh2 { get; set; }

        //////[Key]
        
        [Column("ty_gia", Order = 29, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        
        [StringLength(20)]
        [Column("ma_mauhd", Order = 30)]
        public string ma_mauhd { get; set; }
    }

    public partial class ARV20
    {
        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec", Order = 0)]
        public string stt_rec { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_ct", Order = 1)]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        
        [Column("ngay_lct", TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_ct", Order = 2)]
        public string so_ct { get; set; }

        //////[Key]
        [StringLength(12)]
        [Column("so_seri", Order = 3)]
        public string so_seri { get; set; }

        [StringLength(128)]
        [Column("ten_kh")]
        public string ten_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        //////[Key]
        [StringLength(18)]
        [Column("ma_so_thue", Order = 4)]
        public string ma_so_thue { get; set; }

        [StringLength(128)]
        [Column("ten_vt")]
        public string ten_vt { get; set; }

        //////[Key]
        [Column("t_tien2", Order = 5, TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_thue", Order = 6)]
        public string ma_thue { get; set; }

        //////[Key]
        [Column("thue_suat", Order = 7, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //////[Key]
        [Column("t_thue", Order = 8, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_thue_co", Order = 9)]
        public string tk_thue_co { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("tk_du", Order = 10)]
        public string tk_du { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        //////[Key]
        [StringLength(32)]
        [Column("ghi_chu", Order = 11)]
        public string ghi_chu { get; set; }

        //////[Key]
        [Column("date0", Order = 12, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 13)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 14)]
        public byte user_id0 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 15)]
        public string status { get; set; }

        //////[Key]
        [Column("t_tien_nt2", Order = 16, TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        //////[Key]
        [Column("t_thue_nt", Order = 17, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 18)]
        public string ma_nt { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 19)]
        public string ma_dvcs { get; set; }

        //////[Key]
        [Column("date2", Order = 20, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 21)]
        public string time2 { get; set; }

        //////[Key]
        
        [Column("user_id2", Order = 22)]
        public byte user_id2 { get; set; }

        [StringLength(5)]
        [Column("stt_rec0")]
        public string stt_rec0 { get; set; }

        [StringLength(8)]
        [Column("ma_kho")]
        public string ma_kho { get; set; }

        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        
        [Column("ty_gia", TypeName = "numeric")]
        public decimal? ty_gia { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_hd")]
        public string ma_hd { get; set; }

        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [StringLength(16)]
        [Column("ma_phi")]
        public string ma_phi { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("Ma_kh2", Order = 23)]
        public string Ma_kh2 { get; set; }

        //////[Key]
        
        [StringLength(20)]
        [Column("ma_mauhd", Order = 24)]
        public string ma_mauhd { get; set; }
    }


    public partial class ARS90
    {
        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec", Order = 0)]
        public string stt_rec { get; set; }

        //////[Key]
        
        [StringLength(5)]
        [Column("stt_rec0", Order = 1)]
        public string stt_rec0 { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_ct", Order = 2)]
        public string ma_ct { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("ma_gd", Order = 3)]
        public string ma_gd { get; set; }

        //////[Key]
        
        [Column("pn_gia_tb", Order = 4)]
        public byte pn_gia_tb { get; set; }

        //////[Key]
        
        [Column("px_gia_dd", Order = 5)]
        public byte px_gia_dd { get; set; }

        //////[Key]
        
        [Column("ngay_lct", Order = 6, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        //////[Key]
        
        [Column("ngay_ct", Order = 7, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_ct", Order = 8)]
        public string so_ct { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_seri", Order = 9)]
        public string so_seri { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_lo", Order = 10)]
        public string so_lo { get; set; }

        
        [Column("ngay_lo", TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        //////[Key]
        
        [Column("tk_cn", Order = 11)]
        public byte tk_cn { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_kh", Order = 12)]
        public string ma_kh { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("ma_khon", Order = 13)]
        public string ma_khon { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("ma_kho", Order = 14)]
        public string ma_kho { get; set; }

        //////[Key]
        
        [Column("dien_giai", Order = 15)]
        public string dien_giai { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vv", Order = 16)]
        public string ma_vv { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_nx", Order = 17)]
        public string ma_nx { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_nt", Order = 18)]
        public string ma_nt { get; set; }

        //////[Key]
        
        [Column("ty_gia", Order = 19, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vt", Order = 20)]
        public string ma_vt { get; set; }

        //////[Key]
        
        [StringLength(5)]
        [Column("dvt1", Order = 21)]
        public string dvt1 { get; set; }

        //////[Key]
        
        [Column("sl_nhap1", Order = 22, TypeName = "numeric")]
        public decimal sl_nhap1 { get; set; }

        //////[Key]
        
        [Column("sl_xuat1", Order = 23, TypeName = "numeric")]
        public decimal sl_xuat1 { get; set; }

        //////[Key]
        
        [Column("he_so1", Order = 24, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        //////[Key]
        
        [Column("gia_nt1", Order = 25, TypeName = "numeric")]
        public decimal gia_nt1 { get; set; }

        //////[Key]
        
        [Column("gia1", Order = 26, TypeName = "numeric")]
        public decimal gia1 { get; set; }

        //////[Key]
        
        [Column("gia01", Order = 27, TypeName = "numeric")]
        public decimal gia01 { get; set; }

        //////[Key]
        
        [Column("gia_nt01", Order = 28, TypeName = "numeric")]
        public decimal gia_nt01 { get; set; }

        //////[Key]
        
        [Column("gia21", Order = 29, TypeName = "numeric")]
        public decimal gia21 { get; set; }

        //////[Key]
        
        [Column("gia_nt21", Order = 30, TypeName = "numeric")]
        public decimal gia_nt21 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_vt", Order = 31)]
        public string tk_vt { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_gv", Order = 32)]
        public string tk_gv { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_dt", Order = 33)]
        public string tk_dt { get; set; }

        //////[Key]
        
        [Column("nxt", Order = 34)]
        public byte nxt { get; set; }

        //////[Key]
        
        [Column("ct_dc", Order = 35)]
        public byte ct_dc { get; set; }

        //////[Key]
        
        [Column("sl_nhap", Order = 36, TypeName = "numeric")]
        public decimal sl_nhap { get; set; }

        //////[Key]
        
        [Column("sl_xuat", Order = 37, TypeName = "numeric")]
        public decimal sl_xuat { get; set; }

        //////[Key]
        
        [Column("gia_nt", Order = 38, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        //////[Key]
        
        [Column("gia", Order = 39, TypeName = "numeric")]
        public decimal gia { get; set; }

        //////[Key]
        
        [Column("tien_nt_n", Order = 40, TypeName = "numeric")]
        public decimal tien_nt_n { get; set; }

        //////[Key]
        
        [Column("tien_nt_x", Order = 41, TypeName = "numeric")]
        public decimal tien_nt_x { get; set; }

        //////[Key]
        
        [Column("tien_nhap", Order = 42, TypeName = "numeric")]
        public decimal tien_nhap { get; set; }

        //////[Key]
        
        [Column("tien_xuat", Order = 43, TypeName = "numeric")]
        public decimal tien_xuat { get; set; }

        //////[Key]
        
        [Column("gia_nt0", Order = 44, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        //////[Key]
        
        [Column("gia0", Order = 45, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        //////[Key]
        
        [Column("tien_nt0", Order = 46, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        //////[Key]
        
        [Column("tien0", Order = 47, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        //////[Key]
        
        [Column("cp_nt", Order = 48, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        //////[Key]
        
        [Column("cp", Order = 49, TypeName = "numeric")]
        public decimal cp { get; set; }

        //////[Key]
        
        [Column("nk_nt", Order = 50, TypeName = "numeric")]
        public decimal nk_nt { get; set; }

        //////[Key]
        
        [Column("nk", Order = 51, TypeName = "numeric")]
        public decimal nk { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_nk", Order = 52)]
        public string tk_thue_nk { get; set; }

        //////[Key]
        
        [Column("gia_nt2", Order = 53, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        //////[Key]
        
        [Column("gia2", Order = 54, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        //////[Key]
        
        [Column("tien_nt2", Order = 55, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        //////[Key]
        
        [Column("tien2", Order = 56, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        //////[Key]
        
        [Column("han_tt", Order = 57)]
        public byte han_tt { get; set; }

        //////[Key]
        
        [Column("cp_thue_ck", Order = 58)]
        public byte cp_thue_ck { get; set; }

        //////[Key]
        
        [Column("thue_suat", Order = 59, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        //////[Key]
        
        [Column("thue_nt", Order = 60, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        //////[Key]
        
        [Column("thue", Order = 61, TypeName = "numeric")]
        public decimal thue { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_no", Order = 62)]
        public string tk_thue_no { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_co", Order = 63)]
        public string tk_thue_co { get; set; }

        //////[Key]
        
        [Column("ck_nt", Order = 64, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        //////[Key]
        
        [Column("ck", Order = 65, TypeName = "numeric")]
        public decimal ck { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk_ck", Order = 66)]
        public string tk_ck { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_pn", Order = 67)]
        public string stt_rec_pn { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_dc", Order = 68)]
        public string stt_rec_dc { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("status", Order = 69)]
        public string status { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_nk", Order = 70)]
        public string ma_nk { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td", Order = 71)]
        public string ma_td { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("ma_dvcs", Order = 72)]
        public string ma_dvcs { get; set; }

        
        [Column("ngay_ct0", TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_ct0", Order = 73)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        [StringLength(8)]
        [Column("Ma_vitrin")]
        public string Ma_vitrin { get; set; }

        [StringLength(8)]
        [Column("Ma_vitri")]
        public string Ma_vitri { get; set; }

        [StringLength(32)]
        [Column("Ong_ba")]
        public string Ong_ba { get; set; }

        [StringLength(16)]
        [Column("Ma_sp")]
        public string Ma_sp { get; set; }

        [StringLength(16)]
        [Column("So_lsx")]
        public string So_lsx { get; set; }

        [StringLength(16)]
        [Column("Ma_hd")]
        public string Ma_hd { get; set; }

        [StringLength(16)]
        [Column("Ma_ku")]
        public string Ma_ku { get; set; }

        [StringLength(16)]
        [Column("Ma_phi")]
        public string Ma_phi { get; set; }

        [StringLength(8)]
        [Column("Ma_nvien")]
        public string Ma_nvien { get; set; }

        [StringLength(16)]
        [Column("Ma_lo")]
        public string Ma_lo { get; set; }

        [StringLength(8)]
        [Column("Ma_httt")]
        public string Ma_httt { get; set; }

        [StringLength(8)]
        [Column("Ma_bpht")]
        public string Ma_bpht { get; set; }

        [StringLength(1)]
        [Column("Tang")]
        public string Tang { get; set; }

        //////[Key]
        
        [Column("date0", Order = 74, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 75)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 76)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [Column("date2", Order = 77, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 78)]
        public string time2 { get; set; }

        //////[Key]
        
        [Column("user_id2", Order = 79)]
        public byte user_id2 { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        //////[Key]
        
        [Column("PT_CKI", Order = 80, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        //////[Key]
        
        [Column("TIEN1", Order = 81, TypeName = "numeric")]
        public decimal TIEN1 { get; set; }

        //////[Key]
        
        [Column("TIEN1_NT", Order = 82, TypeName = "numeric")]
        public decimal TIEN1_NT { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("STT_RECDH", Order = 83)]
        public string STT_RECDH { get; set; }

        //////[Key]
        
        [StringLength(5)]
        [Column("STT_REC0DH", Order = 84)]
        public string STT_REC0DH { get; set; }

        
        [Column("NGAY_CT1", TypeName = "smalldatetime")]
        public DateTime? NGAY_CT1 { get; set; }

        
        [Column("NGAY_CT2", TypeName = "smalldatetime")]
        public DateTime? NGAY_CT2 { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("Loai", Order = 85)]
        public string Loai { get; set; }
    }


    public partial class ARS31
    {
        //////[Key]
        [StringLength(13)]
        [Column("stt_rec_hd", Order = 0)]
        public string stt_rec_hd { get; set; }

        //////[Key]
        [Column("ngay_lct", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        //////[Key]
        [Column("ngay_ct", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //////[Key]
        [Column("ky", Order = 3, TypeName = "numeric")]
        public decimal ky { get; set; }

        //////[Key]
        [Column("nam", Order = 4, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        [Column("tien_nt", Order = 5, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //////[Key]
        [StringLength(3)]
        [Column("ma_nt", Order = 6)]
        public string ma_nt { get; set; }

        //////[Key]
        [Column("ty_gia", Order = 7, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        [Column("tien", Order = 8, TypeName = "numeric")]
        public decimal tien { get; set; }

        //////[Key]
        [Column("ty_gia_dg", Order = 9, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        //////[Key]
        [Column("tien_cl_no", Order = 10, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        //////[Key]
        [Column("tien_cl_co", Order = 11, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        //////[Key]
        [Column("date0", Order = 12, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time0", Order = 13)]
        public string time0 { get; set; }

        //////[Key]
        [Column("user_id0", Order = 14)]
        public byte user_id0 { get; set; }

        //////[Key]
        [StringLength(1)]
        [Column("status", Order = 15)]
        public string status { get; set; }

        //////[Key]
        [Column("date2", Order = 16, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        [StringLength(8)]
        [Column("time2", Order = 17)]
        public string time2 { get; set; }

        //////[Key]
        [Column("user_id2", Order = 18)]
        public byte user_id2 { get; set; }

        //////[Key]
        [Column("sl_td1", Order = 19, TypeName = "numeric")]
        public decimal sl_td1 { get; set; }

        //////[Key]
        [Column("sl_td2", Order = 20, TypeName = "numeric")]
        public decimal sl_td2 { get; set; }

        //////[Key]
        [Column("sl_td3", Order = 21, TypeName = "numeric")]
        public decimal sl_td3 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td1", Order = 22)]
        public string ma_td1 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td2",Order = 23)]
        public string ma_td2 { get; set; }

        //////[Key]
        [StringLength(16)]
        [Column("ma_td3", Order = 24)]
        public string ma_td3 { get; set; }

        //////[Key]
        [Column("ngay_td1", Order = 25, TypeName = "smalldatetime")]
        public DateTime ngay_td1 { get; set; }

        //////[Key]
        [Column("ngay_td2", Order = 26, TypeName = "smalldatetime")]
        public DateTime ngay_td2 { get; set; }

        //////[Key]
        [Column("ngay_td3", Order = 27, TypeName = "smalldatetime")]
        public DateTime ngay_td3 { get; set; }

        //////[Key]
        [StringLength(24)]
        [Column("gc_td1", Order = 28)]
        public string gc_td1 { get; set; }

        //////[Key]
        [StringLength(24)]
        [Column("gc_td2", Order = 29)]
        public string gc_td2 { get; set; }

        //////[Key]
        [StringLength(24)]
        [Column("gc_td3", Order = 30)]
        public string gc_td3 { get; set; }
    }


    public partial class ARS30
    {
        //////[Key]
        
        [Column("ma_tt", Order = 0, TypeName = "numeric")]
        public decimal ma_tt { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("ma_gd", Order = 1)]
        public string ma_gd { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec", Order = 2)]
        public string stt_rec { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_ct", Order = 3)]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime? ngay_lct { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_ct", Order = 4)]
        public string so_ct { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_kh", Order = 5)]
        public string ma_kh { get; set; }

        
        [Column("ngay_ct0", TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_ct0", Order = 6)]
        public string so_ct0 { get; set; }

        //////[Key]
        
        [Column("dien_giai", Order = 7)]
        public string dien_giai { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vv", Order = 8)]
        public string ma_vv { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk", Order = 9)]
        public string tk { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_nt", Order = 10)]
        public string ma_nt { get; set; }

        //////[Key]
        
        [Column("ty_gia", Order = 11, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        
        [Column("t_tien_nt", Order = 12, TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        //////[Key]
        
        [Column("t_tien", Order = 13, TypeName = "numeric")]
        public decimal t_tien { get; set; }

        //////[Key]
        
        [Column("t_tien_nt0", Order = 14, TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        //////[Key]
        
        [Column("t_tien0", Order = 15, TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        //////[Key]
        
        [Column("t_cp_nt", Order = 16, TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        //////[Key]
        
        [Column("t_cp", Order = 17, TypeName = "numeric")]
        public decimal t_cp { get; set; }

        //////[Key]
        
        [Column("t_nk_nt", Order = 18, TypeName = "numeric")]
        public decimal t_nk_nt { get; set; }

        //////[Key]
        
        [Column("t_nk", Order = 19, TypeName = "numeric")]
        public decimal t_nk { get; set; }

        //////[Key]
        
        [Column("t_thue_nt", Order = 20, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        //////[Key]
        
        [Column("t_thue", Order = 21, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        //////[Key]
        
        [Column("t_tt_nt", Order = 22, TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        //////[Key]
        
        [Column("t_tt", Order = 23, TypeName = "numeric")]
        public decimal t_tt { get; set; }

        //////[Key]
        
        [Column("t_tt_nt0", Order = 24, TypeName = "numeric")]
        public decimal t_tt_nt0 { get; set; }

        //////[Key]
        
        [Column("t_tt0", Order = 25, TypeName = "numeric")]
        public decimal t_tt0 { get; set; }

        //////[Key]
        
        [Column("han_tt", Order = 26, TypeName = "numeric")]
        public decimal han_tt { get; set; }

        //////[Key]
        
        [Column("t_tt_qd", Order = 27, TypeName = "numeric")]
        public decimal t_tt_qd { get; set; }

        //////[Key]
        
        [Column("tat_toan", Order = 28, TypeName = "numeric")]
        public decimal tat_toan { get; set; }

        
        [Column("ngay_tt", TypeName = "smalldatetime")]
        public DateTime? ngay_tt { get; set; }

        //////[Key]
        
        [Column("tt_nt", Order = 29, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        //////[Key]
        
        [Column("tt", Order = 30, TypeName = "numeric")]
        public decimal tt { get; set; }

        //////[Key]
        
        [Column("tt_qd", Order = 31, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_tt", Order = 32)]
        public string stt_rec_tt { get; set; }

        //////[Key]
        
        [Column("date0", Order = 33, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 34)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 35)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("status", Order = 36)]
        public string status { get; set; }

        //////[Key]
        
        [Column("t_tt1", Order = 37, TypeName = "numeric")]
        public decimal t_tt1 { get; set; }

        //////[Key]
        
        [Column("t_tt_nt1", Order = 38, TypeName = "numeric")]
        public decimal t_tt_nt1 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("ma_dvcs", Order = 39)]
        public string ma_dvcs { get; set; }

        //////[Key]
        
        [Column("date2", Order = 40, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 41)]
        public string time2 { get; set; }

        //////[Key]
        
        [Column("user_id2", Order = 42)]
        public byte user_id2 { get; set; }

        [StringLength(5)]
        [Column("stt_rec0")]
        public string stt_rec0 { get; set; }

        
        [Column("tt_cn", TypeName = "numeric")]
        public decimal? tt_cn { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        //////[Key]
        
        [Column("ty_gia_dg", Order = 43, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        //////[Key]
        
        [Column("tien_cl_nos", Order = 44, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        //////[Key]
        
        [Column("tien_cl_co", Order = 45, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("MA_KHO", Order = 46)]
        public string MA_KHO { get; set; }

        //////[Key]
        
        [StringLength(20)]
        [Column("TT_SOKHUNG", Order = 47)]
        public string TT_SOKHUNG { get; set; }

        //////[Key]
        
        [StringLength(20)]
        [Column("TT_SOMAY", Order = 48)]
        public string TT_SOMAY { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_spph", Order = 49)]
        public string ma_spph { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td2ph", Order = 50)]
        public string ma_td2ph { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td3ph", Order = 51)]
        public string ma_td3ph { get; set; }

        //////[Key]
        
        [StringLength(32)]
        [Column("ONG_BA", Order = 52)]
        public string ONG_BA { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("TT_SOLSX", Order = 53)]
        public string TT_SOLSX { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("MA_BP", Order = 54)]
        public string MA_BP { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("MA_NVIEN", Order = 55)]
        public string MA_NVIEN { get; set; }
    }


    public partial class ARS21
    {
        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_hd", Order = 0)]
        public string stt_rec_hd { get; set; }

        //////[Key]
        
        [Column("ngay_lct", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        //////[Key]
        
        [Column("ngay_ct", Order = 2, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        //////[Key]
        
        [Column("ky", Order = 3, TypeName = "numeric")]
        public decimal ky { get; set; }

        //////[Key]
        
        [Column("nam", Order = 4, TypeName = "numeric")]
        public decimal nam { get; set; }

        //////[Key]
        
        [Column("tien_nt", Order = 5, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_nt", Order = 6)]
        public string ma_nt { get; set; }

        //////[Key]
        
        [Column("ty_gia", Order = 7, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        
        [Column("tien", Order = 8, TypeName = "numeric")]
        public decimal tien { get; set; }

        //////[Key]
        
        [Column("ty_gia_dg", Order = 9, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        //////[Key]
        
        [Column("tien_cl_no", Order = 10, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        //////[Key]
        
        [Column("tien_cl_co", Order = 11, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        //////[Key]
        
        [Column("date0", Order = 12, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 13)]
        public string time0 { get; set; }

        //////[Key]
        
        [Column("user_id0", Order = 14)]
        public byte user_id0 { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("status", Order = 15)]
        public string status { get; set; }

        //////[Key]
        
        [Column("date2", Order = 16, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        //////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 17)]
        public string time2 { get; set; }

        //////[Key]
        
        [Column("user_id2", Order = 18)]
        public byte user_id2 { get; set; }

        //////[Key]
        
        [Column("sl_td1", Order = 19, TypeName = "numeric")]
        public decimal sl_td1 { get; set; }

        //////[Key]
        
        [Column("sl_td2", Order = 20, TypeName = "numeric")]
        public decimal sl_td2 { get; set; }

        //////[Key]
        
        [Column("sl_td3", Order = 21, TypeName = "numeric")]
        public decimal sl_td3 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td1", Order = 22)]
        public string ma_td1 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td2", Order = 23)]
        public string ma_td2 { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_td3", Order = 24)]
        public string ma_td3 { get; set; }

        //////[Key]
        
        [Column("ngay_td1", Order = 25, TypeName = "smalldatetime")]
        public DateTime ngay_td1 { get; set; }

        //////[Key]
        
        [Column("ngay_td2", Order = 26, TypeName = "smalldatetime")]
        public DateTime ngay_td2 { get; set; }

        //////[Key]
        
        [Column("ngay_td3", Order = 27, TypeName = "smalldatetime")]
        public DateTime ngay_td3 { get; set; }

        //////[Key]
        
        [StringLength(24)]
        [Column("gc_td1", Order = 28)]
        public string gc_td1 { get; set; }

        //////[Key]
        
        [StringLength(24)]
        [Column("gc_td2", Order = 29)]
        public string gc_td2 { get; set; }

        //////[Key]
        
        [StringLength(24)]
        [Column("gc_td3", Order = 30)]
        public string gc_td3 { get; set; }
    }


    public partial class ARS20
    {
        //////[Key]
        
        [Column("ma_tt", Order = 0)]
        public byte ma_tt { get; set; }

        //////[Key]
        
        [StringLength(1)]
        [Column("ma_gd", Order = 1)]
        public string ma_gd { get; set; }

        //////[Key]
        
        [StringLength(13)]
        [Column("stt_rec", Order = 2)]
        public string stt_rec { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_ct", Order = 3)]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime? ngay_ct { get; set; }

        //////[Key]
        
        [StringLength(12)]
        [Column("so_ct", Order = 4)]
        public string so_ct { get; set; }

        [StringLength(12)]
        [Column("so_seri")]
        public string so_seri { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_kh", Order = 5)]
        public string ma_kh { get; set; }

        //////[Key]
        
        [Column("dien_giai", Order = 6)]
        public string dien_giai { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("ma_vv", Order = 7)]
        public string ma_vv { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        //////[Key]
        
        [StringLength(16)]
        [Column("tk", Order = 8)]
        public string tk { get; set; }

        //////[Key]
        
        [StringLength(3)]
        [Column("ma_nt", Order = 9)]
        public string ma_nt { get; set; }

        //////[Key]
        
        [Column("ty_gia", Order = 10, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        //////[Key]
        
        [Column("t_tien_nt2", Order = 11, TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        //////[Key]
        
        [Column("t_tien2", Order = 12, TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        //////[Key]
        
        [Column("t_thue_nt", Order = 13, TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        //////[Key]
        
        [Column("t_thue", Order = 14, TypeName = "numeric")]
        public decimal t_thue { get; set; }

        //////[Key]
        
        [Column("t_ck_nt", Order = 15, TypeName = "numeric")]
        public decimal t_ck_nt { get; set; }

        //////[Key]
        
        [Column("t_ck", Order = 16, TypeName = "numeric")]
        public decimal t_ck { get; set; }

        //////[Key]
        
        [Column("t_tt_nt", Order = 17, TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        //////[Key]
        
        [Column("t_tt", Order = 18, TypeName = "numeric")]
        public decimal t_tt { get; set; }

        //////[Key]
        
        [Column("t_tt_nt0", Order = 19, TypeName = "numeric")]
        public decimal t_tt_nt0 { get; set; }

        //////[Key]
        
        [Column("t_tt0", Order = 20, TypeName = "numeric")]
        public decimal t_tt0 { get; set; }

        //////[Key]
        
        [Column("han_tt", Order = 21, TypeName = "numeric")]
        public decimal han_tt { get; set; }

        ////[Key]
        
        [Column("t_tt_qd", Order = 22, TypeName = "numeric")]
        public decimal t_tt_qd { get; set; }

        ////[Key]
        
        [Column("tat_toan", Order = 23, TypeName = "numeric")]
        public decimal tat_toan { get; set; }

        
        [Column("ngay_tt", TypeName = "smalldatetime")]
        public DateTime? ngay_tt { get; set; }

        ////[Key]
        
        [Column("tt_nt", Order = 24, TypeName = "numeric")]
        public decimal tt_nt { get; set; }

        ////[Key]
        
        [Column("tt", Order = 25, TypeName = "numeric")]
        public decimal tt { get; set; }

        ////[Key]
        
        [Column("tt_qd", Order = 26, TypeName = "numeric")]
        public decimal tt_qd { get; set; }

        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_tt", Order = 27)]
        public string stt_rec_tt { get; set; }

        ////[Key]
        [Column("date0", Order = 28, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time0", Order = 29)]
        public string time0 { get; set; }

        ////[Key]
        [Column("user_id0", Order = 30)]
        public byte user_id0 { get; set; }

        ////[Key]
        [Column("t_tt1", Order = 31, TypeName = "numeric")]
        public decimal t_tt1 { get; set; }

        ////[Key]
        [Column("t_tt_nt1", Order = 32, TypeName = "numeric")]
        public decimal t_tt_nt1 { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("status", Order = 33)]
        public string status { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("ma_dvcs", Order = 34)]
        public string ma_dvcs { get; set; }

        ////[Key]
        [Column("date2", Order = 35, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("time2", Order = 36)]
        public string time2 { get; set; }

        ////[Key]
        [Column("user_id2", Order = 37)]
        public byte user_id2 { get; set; }

        [StringLength(5)]
        [Column("stt_rec0")]
        public string stt_rec0 { get; set; }

        
        [Column("tt_cn", TypeName = "numeric")]
        public decimal? tt_cn { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        ////[Key]
        [Column("ty_gia_dg", Order = 38, TypeName = "numeric")]
        public decimal ty_gia_dg { get; set; }

        ////[Key]
        [Column("tien_cl_no", Order = 39, TypeName = "numeric")]
        public decimal tien_cl_no { get; set; }

        ////[Key]
        [Column("tien_cl_co", Order = 40, TypeName = "numeric")]
        public decimal tien_cl_co { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(16)]
        [Column("ma_hd")]
        public string ma_hd { get; set; }

        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [StringLength(16)]
        [Column("ma_phi")]
        public string ma_phi { get; set; }

        ////[Key]
        [StringLength(20)]
        [Column("TT_SOKHUNG", Order = 41)]
        public string TT_SOKHUNG { get; set; }

        ////[Key]
        [StringLength(20)]
        [Column("TT_SOMAY", Order = 42)]
        public string TT_SOMAY { get; set; }

        ////[Key]
        [StringLength(32)]
        [Column("ong_ba", Order = 43)]
        public string ong_ba { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("MA_KHO", Order = 44)]
        public string MA_KHO { get; set; }

        ////[Key]
        [Column("T_TIEN_NT5", Order = 45, TypeName = "numeric")]
        public decimal T_TIEN_NT5 { get; set; }

        ////[Key]
        [Column("T_TIEN5", Order = 46, TypeName = "numeric")]
        public decimal T_TIEN5 { get; set; }

        ////[Key]
        [StringLength(16)]
        [Column("MA_KH3", Order = 47)]
        public string MA_KH3 { get; set; }

        ////[Key]
        [StringLength(16)]
        [Column("ma_spph", Order = 48)]
        public string ma_spph { get; set; }

        ////[Key]
        [StringLength(16)]
        [Column("ma_td2ph", Order = 49)]
        public string ma_td2ph { get; set; }

        ////[Key]
        [StringLength(16)]
        [Column("ma_td3ph", Order = 50)]
        public string ma_td3ph { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("ma_lct", Order = 51)]
        public string ma_lct { get; set; }

        ////[Key]
        [StringLength(16)]
        [Column("TT_SOLSX", Order = 52)]
        public string TT_SOLSX { get; set; }
    }


    public partial class ARI70
    {
        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec",Order = 0)]
        public string stt_rec { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("stt_rec0",Order = 1)]
        public string stt_rec0 { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_ct",Order = 2)]
        public string ma_ct { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("ma_gd",Order = 3)]
        public string ma_gd { get; set; }

        ////[Key]
        
        [Column("pn_gia_tb",Order = 4)]
        public byte pn_gia_tb { get; set; }

        ////[Key]
        
        [Column("px_gia_dd",Order = 5)]
        public byte px_gia_dd { get; set; }

        ////[Key]
        
        [Column("ngay_lct",Order = 6, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        ////[Key]
        
        [Column("ngay_ct",Order = 7, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_ct",Order = 8)]
        public string so_ct { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_seri")]
        public string so_seri { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_lo",Order = 10)]
        public string so_lo { get; set; }


        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        ////[Key]
        
        [Column("tk_cn", Order = 11)]
        public byte tk_cn { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_kh", Order = 12)]
        public string ma_kh { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_khon", Order = 13)]
        public string ma_khon { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_kho", Order = 14)]
        public string ma_kho { get; set; }

        ////[Key]
        
        [Column("dien_giai", Order = 15)]
        public string dien_giai { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_vv", Order = 16)]
        public string ma_vv { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_nx", Order = 17)]
        public string ma_nx { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_nt", Order = 18)]
        public string ma_nt { get; set; }

        ////[Key]
        
        [Column("ty_gia", Order = 19, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_vt", Order = 20)]
        public string ma_vt { get; set; }

        ////[Key]
        
        [StringLength(10)]
        [Column("dvt1", Order = 21)]
        public string dvt1 { get; set; }

        ////[Key]
        
        [Column("sl_nhap1", Order = 22, TypeName = "numeric")]
        public decimal sl_nhap1 { get; set; }

        ////[Key]
        
        [Column("sl_xuat1", Order = 23, TypeName = "numeric")]
        public decimal sl_xuat1 { get; set; }

        ////[Key]
        
        [Column("he_so1", Order = 24, TypeName = "numeric")]
        public decimal he_so1 { get; set; }

        ////[Key]
        
        [Column("gia_nt1", Order = 25, TypeName = "numeric")]
        public decimal gia_nt1 { get; set; }

        ////[Key]
        
        [Column("gia1", Order = 26, TypeName = "numeric")]
        public decimal gia1 { get; set; }

        ////[Key]
        
        [Column("gia01", Order = 27, TypeName = "numeric")]
        public decimal gia01 { get; set; }

        ////[Key]
        
        [Column("gia_nt01", Order = 28, TypeName = "numeric")]
        public decimal gia_nt01 { get; set; }

        ////[Key]
        
        [Column("gia21", Order = 29, TypeName = "numeric")]
        public decimal gia21 { get; set; }

        ////[Key]
        
        [Column("gia_nt21", Order = 30, TypeName = "numeric")]
        public decimal gia_nt21 { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_vt", Order = 31)]
        public string tk_vt { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_gv", Order = 32)]
        public string tk_gv { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_dt", Order = 33)]
        public string tk_dt { get; set; }

        ////[Key]
        
        [Column("nxt", Order = 34)]
        public byte nxt { get; set; }

        ////[Key]
        
        [Column("ct_dc", Order = 35)]
        public byte ct_dc { get; set; }

        ////[Key]
        
        [Column("sl_nhap", Order = 36, TypeName = "numeric")]
        public decimal sl_nhap { get; set; }

        ////[Key]
        
        [Column("sl_xuat", Order = 37, TypeName = "numeric")]
        public decimal sl_xuat { get; set; }

        ////[Key]
        
        [Column("gia_nt", Order = 38, TypeName = "numeric")]
        public decimal gia_nt { get; set; }

        ////[Key]
        
        [Column("gia", Order = 39, TypeName = "numeric")]
        public decimal gia { get; set; }

        ////[Key]
        
        [Column("tien_nt_n", Order = 40, TypeName = "numeric")]
        public decimal tien_nt_n { get; set; }

        ////[Key]
        
        [Column("tien_nt_x", Order = 41, TypeName = "numeric")]
        public decimal tien_nt_x { get; set; }

        ////[Key]
        
        [Column("tien_nhap", Order = 42, TypeName = "numeric")]
        public decimal tien_nhap { get; set; }

        ////[Key]
        
        [Column("tien_xuat", Order = 43, TypeName = "numeric")]
        public decimal tien_xuat { get; set; }

        ////[Key]
        
        [Column("gia_nt0", Order = 44, TypeName = "numeric")]
        public decimal gia_nt0 { get; set; }

        ////[Key]
        
        [Column("gia0", Order = 45, TypeName = "numeric")]
        public decimal gia0 { get; set; }

        ////[Key]
        
        [Column("tien_nt0", Order = 46, TypeName = "numeric")]
        public decimal tien_nt0 { get; set; }

        ////[Key]
        
        [Column("tien0", Order = 47, TypeName = "numeric")]
        public decimal tien0 { get; set; }

        ////[Key]
        
        [Column("cp_nt", Order = 48, TypeName = "numeric")]
        public decimal cp_nt { get; set; }

        ////[Key]
        
        [Column("cp", Order = 49, TypeName = "numeric")]
        public decimal cp { get; set; }

        ////[Key]
        
        [Column("nk_nt", Order = 50, TypeName = "numeric")]
        public decimal nk_nt { get; set; }

        ////[Key]
        
        [Column("nk", Order = 51, TypeName = "numeric")]
        public decimal nk { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_nk", Order = 52)]
        public string tk_thue_nk { get; set; }

        ////[Key]
        
        [Column("gia_nt2", Order = 53, TypeName = "numeric")]
        public decimal gia_nt2 { get; set; }

        ////[Key]
        
        [Column("gia2", Order = 54, TypeName = "numeric")]
        public decimal gia2 { get; set; }

        ////[Key]
        
        [Column("tien_nt2", Order = 55, TypeName = "numeric")]
        public decimal tien_nt2 { get; set; }

        ////[Key]
        
        [Column("tien2", Order = 56, TypeName = "numeric")]
        public decimal tien2 { get; set; }

        ////[Key]
        
        [Column("han_tt", Order = 57)]
        public byte han_tt { get; set; }

        ////[Key]
        
        [Column("cp_thue_ck", Order = 58)]
        public byte cp_thue_ck { get; set; }

        ////[Key]
        
        [Column("thue_suat", Order = 59, TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        ////[Key]
        
        [Column("thue_nt", Order = 60, TypeName = "numeric")]
        public decimal thue_nt { get; set; }

        ////[Key]
        
        [Column("thue", Order = 61, TypeName = "numeric")]
        public decimal thue { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_no", Order = 62)]
        public string tk_thue_no { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_thue_co", Order = 63)]
        public string tk_thue_co { get; set; }

        ////[Key]
        
        [Column("ck_nt", Order = 64, TypeName = "numeric")]
        public decimal ck_nt { get; set; }

        ////[Key]
        
        [Column("ck", Order = 65, TypeName = "numeric")]
        public decimal ck { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_ck", Order = 66)]
        public string tk_ck { get; set; }

        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_pn", Order = 67)]
        public string stt_rec_pn { get; set; }

        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec_dc", Order = 68)]
        public string stt_rec_dc { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("status", Order = 69)]
        public string status { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_nk", Order = 70)]
        public string ma_nk { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_td", Order = 71)]
        public string ma_td { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_dvcs", Order = 72)]
        public string ma_dvcs { get; set; }

        
        [Column("ngay_ct0", TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_ct0", Order = 73)]
        public string so_ct0 { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        [StringLength(8)]
        [Column("Ma_vitrin")]
        public string Ma_vitrin { get; set; }

        [StringLength(8)]
        [Column("Ma_vitri")]
        public string Ma_vitri { get; set; }

        [StringLength(32)]
        [Column("Ong_ba")]
        public string Ong_ba { get; set; }

        [StringLength(16)]
        [Column("Ma_sp")]
        public string Ma_sp { get; set; }

        [StringLength(16)]
        [Column("So_lsx")]
        public string So_lsx { get; set; }

        [StringLength(16)]
        [Column("Ma_hd")]
        public string Ma_hd { get; set; }

        [StringLength(16)]
        [Column("Ma_ku")]
        public string Ma_ku { get; set; }

        [StringLength(16)]
        [Column("Ma_phi")]
        public string Ma_phi { get; set; }

        [StringLength(8)]
        [Column("Ma_nvien")]
        public string Ma_nvien { get; set; }

        [StringLength(16)]
        [Column("Ma_lo")]
        public string Ma_lo { get; set; }

        [StringLength(8)]
        [Column("Ma_httt")]
        public string Ma_httt { get; set; }

        ////[Key]
        
        [Column("date0", Order = 74, TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("time0", Order = 75)]
        public string time0 { get; set; }

        ////[Key]
        
        [Column("user_id0", Order = 76)]
        public byte user_id0 { get; set; }

        ////[Key]
        
        [Column("date2", Order = 77, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("time2", Order = 78)]
        public string time2 { get; set; }

        ////[Key]
        
        [Column("user_id2", Order = 79)]
        public byte user_id2 { get; set; }

        
        [Column("ln", TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1", TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1", TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2", TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3", TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
        [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2", TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        [Column("ngay_td3", TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(8)]
        [Column("Ma_bpht")]
        public string Ma_bpht { get; set; }

        [StringLength(1)]
        [Column("tang")]
        public string tang { get; set; }

        ////[Key]
        
        [Column("PT_CKI", Order = 80, TypeName = "numeric")]
        public decimal PT_CKI { get; set; }

        ////[Key]
        [Column("TIEN1",Order = 81, TypeName = "numeric")]
        public decimal TIEN1 { get; set; }

        ////[Key]
        
        [Column("TIEN1_NT")]
        public decimal TIEN1_NT { get; set; }

        ////[Key]
        
        [StringLength(13)]
        [Column("STT_RECDH", Order = 83)]
        public string STT_RECDH { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("STT_REC0DH", Order = 84)]
        public string STT_REC0DH { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("MA_LNXN", Order = 85)]
        public string MA_LNXN { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("MA_LNX", Order = 86)]
        public string MA_LNX { get; set; }

        
        [Column("HSD", TypeName = "smalldatetime")]
        public DateTime? HSD { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("Ma_kmm", Order = 87)]
        public string Ma_kmm { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("Ma_kmb", Order = 88)]
        public string Ma_kmb { get; set; }

        ////[Key]
        
        [Column("gg_nt", Order = 89, TypeName = "numeric")]
        public decimal gg_nt { get; set; }

        ////[Key]
        
        [Column("gg")]
        public decimal gg { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("TK_GG",Order = 91)]
        public string TK_GG { get; set; }

        [StringLength(8)]
        [Column("Ma_gia")]
        public string Ma_gia { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("SO_MAY",Order = 90, TypeName = "numeric")]
        public string SO_MAY { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("SO_KHUNG", Order = 93)]
        public string SO_KHUNG { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("MA_LCT",Order = 94)]
        public string MA_LCT { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("TT_LOAI", Order = 95)]
        public string TT_LOAI { get; set; }

        ////[Key]
        
        [StringLength(40)]
        [Column("TT_NGHE", Order = 96)]
        public string TT_NGHE { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_SPPH", Order = 97)]
        public string MA_SPPH { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_TD2PH", Order = 98)]
        public string MA_TD2PH { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_TD3PH", Order = 99)]
        public string MA_TD3PH { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("TT_SOXE")]
        public string TT_SOXE { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("TT_SOKHUNG", Order = 101)]
        public string TT_SOKHUNG { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("TT_SOMAY", Order = 102)]
        public string TT_SOMAY { get; set; }

        ////[Key]
        
        [StringLength(4)]
        [Column("TT_NAMNU", Order = 103)]
        public string TT_NAMNU { get; set; }

        ////[Key]
        
        [Column("TT_TUOI", Order = 104, TypeName = "numeric")]
        public decimal TT_TUOI { get; set; }

        ////[Key]
        
        [Column("TT_KMDI", Order = 105, TypeName = "numeric")]
        public decimal TT_KMDI { get; set; }

        ////[Key]
        
        [Column("TT_LANKT", Order = 106, TypeName = "numeric")]
        public decimal TT_LANKT { get; set; }

        
        [Column("TT_NGAYMUA", TypeName = "smalldatetime")]
        public DateTime? TT_NGAYMUA { get; set; }

        [StringLength(48)]
        [Column("TT_NOIMUA")]
        public string TT_NOIMUA { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("DIEN_THOAI", Order = 107)]
        public string DIEN_THOAI { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("DT_DD", Order = 108)]
        public string DT_DD { get; set; }

        ////[Key]
        
        [Column("SO_IMAGE", Order = 109)]
        public string SO_IMAGE { get; set; }

        [StringLength(128)]
        [Column("DIA_CHI")]
        public string DIA_CHI { get; set; }

        ////[Key]
        
        [StringLength(100)]
        [Column("TT_SONHA", Order = 110)]
        public string TT_SONHA { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_PHUONG", Order = 111)]
        public string MA_PHUONG { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_TINH", Order = 112)]
        public string MA_TINH { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("MA_QUAN", Order = 113)]
        public string MA_QUAN { get; set; }

        ////[Key]
        
        [StringLength(48)]
        [Column("TT_NVSC", Order = 114)]
        public string TT_NVSC { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("TT_LISTNV", Order = 115)]
        public string TT_LISTNV { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("MA_BP1", Order = 116)]
        public string MA_BP1 { get; set; }

        [StringLength(48)]
        [Column("MA_THE")]
        public string MA_THE { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("LOAI_THE", Order = 117)]
        public string LOAI_THE { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("TT_GIOVAO", Order = 118)]
        public string TT_GIOVAO { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("TT_GIORA", Order = 119)]
        public string TT_GIORA { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("TT_SOLSX", Order = 120)]
        public string TT_SOLSX { get; set; }

        ////[Key]
        
        [Column("Tien_tb", Order = 121, TypeName = "numeric")]
        public decimal Tien_tb { get; set; }
    }


    public partial class ARctgs01
    {
        ////[Key]
        
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        ////[Key]
        
        [Column("ngay_lo", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lo { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_lo", Order = 2)]
        public string so_lo { get; set; }

        ////[Key]
        
        [Column("dien_giai", Order = 3)]
        public string dien_giai { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk", Order = 4)]
        public string tk { get; set; }

        ////[Key]
        
        [Column("no_co",Order = 5, TypeName = "numeric")]
        public decimal no_co { get; set; }

        ////[Key]
        
        [Column("STT", Order = 6, TypeName = "numeric")]
        public decimal STT { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("phandau", Order = 7)]
        public string phandau { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("phancuoi", Order = 8)]
        public string phancuoi { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("dinhdang", Order = 9)]
        public string dinhdang { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("phanthang", Order = 10)]
        public string phanthang { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("tag",Order = 11)]
        public string tag { get; set; }

        ////[Key]
        [StringLength(1)]
        [Column("nhom_user", Order = 12)]
        public string nhom_user { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("TK_DU0", Order = 13)]
        public string TK_DU0 { get; set; }

        ////[Key]
        
        [Column("ngay_ct1", Order = 14, TypeName = "smalldatetime")]
        public DateTime ngay_ct1 { get; set; }

        ////[Key]
        
        [Column("ngay_ct2", Order = 15, TypeName = "smalldatetime")]
        public DateTime ngay_ct2 { get; set; }

        ////[Key]
        [StringLength(100)]
        [Column("TK_DU")]
        public string TK_DU { get; set; }

        ////[Key]
        
        [StringLength(11)]
        [Column("Order = 17")]
        public string KHOA_CTGS { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("LOAI_CTGS", Order = 18)]
        public string LOAI_CTGS { get; set; }

        ////[Key]
        
        [Column("KIEU_CTGS", Order = 19)]
        [StringLength(1)]
        public string KIEU_CTGS { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("NHOM_CTGS", Order = 20)]
        public string NHOM_CTGS { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("SO_LO0",Order = 21)]
        public string SO_LO0 { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("MA_DVCS", Order = 22)]
        public string MA_DVCS { get; set; }

        ////[Key]
        
        [Column("THANG")]
        public decimal THANG { get; set; }
    }


    public partial class ARctg
    {
        ////[Key]
        
        [Column("nam", Order = 0, TypeName = "numeric")]
        public decimal nam { get; set; }

        ////[Key]
        
        [Column("ngay_lo", Order = 1, TypeName = "smalldatetime")]
        public DateTime ngay_lo { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_lo", Order = 2)]
        public string so_lo { get; set; }

        ////[Key]
        
        [Column("dien_giai", Order = 3)]
        public string dien_giai { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk", Order = 4)]
        public string tk { get; set; }

        ////[Key]
        
        [Column("no_co", Order = 5, TypeName = "numeric")]
        public decimal no_co { get; set; }

        ////[Key]
        
        [Column("tien", Order = 6, TypeName = "numeric")]
        public decimal tien { get; set; }

        ////[Key]
        
        [Column("tien_nt", Order = 7, TypeName = "numeric")]
        public decimal tien_nt { get; set; }

        ////[Key]
        
        [Column("ngay_ct1", Order = 8, TypeName = "smalldatetime")]
        public DateTime ngay_ct1 { get; set; }

        ////[Key]
        
        [Column("ngay_ct2", Order = 9, TypeName = "smalldatetime")]
        public DateTime ngay_ct2 { get; set; }

        ////[Key]
        
        [StringLength(100)]
        [Column("TK_DU", Order = 10)]
        public string TK_DU { get; set; }

        ////[Key]
        
        [StringLength(11)]
        [Column("KHOA_CTGS", Order = 11)]
        public string KHOA_CTGS { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("LOAI_CTGS", Order = 12)]
        public string LOAI_CTGS { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("KIEU_CTGS", Order = 13)]
        public string KIEU_CTGS { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("NHOM_CTGS", Order = 14)]
        public string NHOM_CTGS { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("SO_LO0", Order = 15)]
        public string SO_LO0 { get; set; }

        ////[Key]
        [StringLength(8)]
        [Column("MA_DVCS", Order = 16)]
        public string MA_DVCS { get; set; }

        ////[Key]
        [Column("THANG", Order = 17, TypeName = "numeric")]
        public decimal THANG { get; set; }
    }


    [Table("ARBCGT")]
    public partial class ARBCGT
    {
        ////[Key]
        [Column("thang", Order = 0, TypeName = "numeric")]
        public decimal thang { get; set; }

        ////[Key]
        [Column("nam", Order = 1, TypeName = "numeric")]
        public decimal nam { get; set; }

        ////[Key]
        [Column("stt", Order = 2, TypeName = "numeric")]
        public decimal stt { get; set; }

        ////[Key]
        [Column("bold", Order = 3, TypeName = "numeric")]
        public decimal bold { get; set; }

        ////[Key]
        [Column("in_ck", Order = 4, TypeName = "numeric")]
        public decimal in_ck { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_so", Order = 5)]
        public string ma_so { get; set; }

        [StringLength(100)]
        [Column("chi_tieu")]
        public string chi_tieu { get; set; }

        [StringLength(100)]
        [Column("chi_tieu2")]
        public string chi_tieu2 { get; set; }

        [StringLength(254)]
        [Column("cach_tinh")]
        public string cach_tinh { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_no", Order = 6)]
        public string tk_no { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_co", Order = 7)]
        public string tk_co { get; set; }

        ////[Key]
        
        [Column("giam_tru", Order = 8, TypeName = "numeric")]
        public decimal giam_tru { get; set; }

        ////[Key]
        
        [Column("ky_truoc",Order = 9, TypeName = "numeric")]
        public decimal ky_truoc { get; set; }

        ////[Key]
       
        [Column("ky_nay",Order = 10, TypeName = "numeric")]
        public decimal ky_nay { get; set; }

        ////[Key]
        
        [Column("luy_ke",Order = 11, TypeName = "numeric")]
        public decimal luy_ke { get; set; }

        ////[Key]
        
        [Column("ky_truocnt")]
        public decimal ky_truocnt { get; set; }

        ////[Key]
        
        [Column("ky_nay_nt",Order = 13, TypeName = "numeric")]
        public decimal ky_nay_nt { get; set; }

        ////[Key]
        
        [Column("luy_ke_nt")]
        public decimal luy_ke_nt { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_ytcp",Order = 15)]
        public string ma_ytcp { get; set; }

        ////[Key]
        
        [Column("sl_tp_nk",Order = 16, TypeName = "numeric")]
        public decimal sl_tp_nk { get; set; }

        ////[Key]
        
        [Column("tl_ht",Order = 17, TypeName = "numeric")]
        public decimal tl_ht { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_sp",Order = 18)]
        public string ma_sp { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_bpht",Order = 19)]
        public string ma_bpht { get; set; }

        ////[Key]
        
        [Column("sl_dd_ck")]
        public decimal sl_dd_ck { get; set; }

        ////[Key]
        
        [Column("dd_dk",Order = 21, TypeName = "numeric")]
        public decimal dd_dk { get; set; }

        ////[Key]
        
        [Column("ps_no",Order = 22, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        ////[Key]
        [Column("dd_ck",Order = 23, TypeName = "numeric")]
        public decimal dd_ck { get; set; }
    }


    public partial class ARA00
    {
        ////[Key]
        
        [StringLength(13)]
        [Column("stt_rec",Order = 0)]
        public string stt_rec { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("ma_dvcs",Order = 1)]
        public string ma_dvcs { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_ct",Order = 2)]
        public string ma_TypeNamect { get; set; }

        ////[Key]
        
        [Column("ngay_ct",Order = 3, TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        ////[Key]
        
        [Column("ngay_lct",Order = 4, TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_ct",Order = 5)]
        public string so_ct { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("so_lo",Order = 6)]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        ////[Key]
        
        [Column("dien_giaih",Order = 7)]
        public string dien_giaih { get; set; }

        ////[Key]
        
        [Column("dien_giai",Order = 8)]
        public string dien_giai { get; set; }

        ////[Key]
        
        [StringLength(5)]
        [Column("nh_dk",Order = 9)]
        public string nh_dk { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk",Order = 10)]
        public string tk { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("tk_du",Order = 11)]
        public string tk_du { get; set; }

        ////[Key]

        [Column("ps_no_nt",Order = 12, TypeName = "numeric")]
        
        public decimal ps_no_nt { get; set; }

        ////[Key]
        
        [Column("ps_co_nt",Order = 13, TypeName = "numeric")]
        public decimal ps_co_nt { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_nt",Order = 14)]
        public string ma_nt { get; set; }

        ////[Key]
        
        [Column("ty_gia",Order = 15, TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        ////[Key]
        
        [Column("ps_no",Order = 16, TypeName = "numeric")]
        public decimal ps_no { get; set; }

        ////[Key]
        
        [Column("ps_co",Order = 17, TypeName = "numeric")]
        public decimal ps_co { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_kh",Order = 18)]
        public string ma_kh { get; set; }

        ////[Key]
        
        [Column("tk_cn",Order = 19)]
        public byte tk_cn { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_vv",Order = 20)]
        public string ma_vv { get; set; }

        ////[Key]
        
        [StringLength(3)]
        [Column("ma_nk",Order = 21)]
        public string ma_nk { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_td",Order = 22)]
        public string ma_td { get; set; }

        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        [StringLength(16)]
        [Column("Ma_sp")]
        public string Ma_sp { get; set; }

        [StringLength(16)]
        [Column("So_lsx")]
        public string So_lsx { get; set; }

        [StringLength(16)]
        [Column("Ma_hd")]
        public string Ma_hd { get; set; }

        [StringLength(16)]
        [Column("Ma_phi")]
        public string Ma_phi { get; set; }

        [StringLength(8)]
        [Column("Ma_nvien")]
        public string Ma_nvien { get; set; }

        [StringLength(8)]
        [Column("Ma_bpht")]
        public string Ma_bpht { get; set; }

        ////[Key]
        
        [Column("user_id0",Order = 23)]
        public byte user_id0 { get; set; }

        ////[Key]
        [Column("date0")]
        public DateTime date0 { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("time0",Order = 25)]
        public string time0 { get; set; }

        ////[Key]
        
        [Column("user_id2",Order = 26)]
        public byte user_id2 { get; set; }

        ////[Key]
        
        [Column("date2",Order = 27, TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("time2",Order = 28)]
        public string time2 { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("status",Order = 29)]
        public string status { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Column("ct_nxt")]
        public byte? ct_nxt { get; set; }

        [StringLength(1)]

        [Column("ma_gd")]
        public string ma_gd { get; set; }

        

        [Column("ln",TypeName = "numeric")]
        public decimal? ln { get; set; }

        [StringLength(16)]
        [Column("ma_td2")]
        public string ma_td2 { get; set; }

        [StringLength(16)]
        [Column("ma_td3")]
        public string ma_td3 { get; set; }

        
        [Column("ngay_td1",TypeName = "smalldatetime")]
        public DateTime? ngay_td1 { get; set; }

        
        [Column("sl_td1",TypeName = "numeric")]
        public decimal? sl_td1 { get; set; }

        
        [Column("sl_td2",TypeName = "numeric")]
        public decimal? sl_td2 { get; set; }

        
        [Column("sl_td3",TypeName = "numeric")]
        public decimal? sl_td3 { get; set; }

        [StringLength(24)]
         [Column("gc_td1")]
        public string gc_td1 { get; set; }

        [StringLength(24)]
        [Column("gc_td2")]
        public string gc_td2 { get; set; }

        [StringLength(24)]
        [Column("gc_td3")]
        public string gc_td3 { get; set; }

        
        [Column("ngay_td2",TypeName = "smalldatetime")]
        public DateTime? ngay_td2 { get; set; }

        
        [Column("ngay_td3",TypeName = "smalldatetime")]
        public DateTime? ngay_td3 { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("LOAI_CTGS",Order = 30)]
        public string LOAI_CTGS { get; set; }

        ////[Key]
        
        [StringLength(1)]
        [Column("KIEU_CTGS",Order = 31)]
        public string KIEU_CTGS { get; set; }

        ////[Key]
        
        [StringLength(12)]
        [Column("SO_LO0",Order = 32)]
        public string SO_LO0 { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("TT_SOKHUNG",Order = 33)]
        public string TT_SOKHUNG { get; set; }

        ////[Key]
        
        [StringLength(20)]
        [Column("TT_SOMAY",Order = 34)]
        public string TT_SOMAY { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_spph",Order = 35)]
        public string ma_spph { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_td2ph",Order = 36)]
        public string ma_td2ph { get; set; }

        ////[Key]
        
        [StringLength(16)]
        [Column("ma_td3ph",Order = 37)]
        public string ma_td3ph { get; set; }

        ////[Key]
        
        [StringLength(8)]
        [Column("MA_KHO2",Order = 38)]
        public string MA_KHO2 { get; set; }
    }


    public partial class AM92
    {
        public AM92()
        {
            AD92 = new HashSet<AD92>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tien_nt0",TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        
        [Column("t_tien0",TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        [Column("han_tt")]
        public byte han_tt { get; set; }

        [Column("so_hd_gtgt")]
        public byte so_hd_gtgt { get; set; }

        [Column("loai_hd")]
        public byte loai_hd { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        
        [Column("tso_luong1",TypeName = "numeric")]
        public decimal? tso_luong1 { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        
        [Column("NGAY_CT1",TypeName = "smalldatetime")]
        public DateTime? NGAY_CT1 { get; set; }

        
        [Column("NGAY_CT2",TypeName = "smalldatetime")]
        public DateTime? NGAY_CT2 { get; set; }

        
        [Column("NGAY_TRT")]
        public DateTime? NGAY_TRT { get; set; }

        public virtual ICollection<AD92> AD92 { get; set; }
    }


    public partial class AM91
    {
        public AM91()
        {
            AD91 = new HashSet<AD91>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri")]
        public string so_seri { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(18)]
        [Column("ma_so_thue")]
        public string ma_so_thue { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Column("px_gia_dd")]
        public byte px_gia_dd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Column("sua_thue")]
        public byte sua_thue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("sua_tkthue",TypeName = "numeric")]
        public decimal sua_tkthue { get; set; }

        
        [Column("t_tien_nt2",TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        
        [Column("t_tien2",TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        [Column("tinh_ck")]
        public byte tinh_ck { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_ck")]
        public string tk_ck { get; set; }

        
        [Column("t_ck_nt",TypeName = "numeric")]
        public decimal t_ck_nt { get; set; }

        
        [Column("t_ck",TypeName = "numeric")]
        public decimal t_ck { get; set; }

        
        [Column("han_tt",TypeName = "numeric")]
        public decimal han_tt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(48)]
        [Column("ten_vtthue")]
        public string ten_vtthue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        
        [Column("T_Tien1",TypeName = "numeric")]
        public decimal T_Tien1 { get; set; }

        
        [Column("T_Tien1_nt")]
        public decimal T_Tien1_nt { get; set; }

        
        [Column("CHIA_NHOM",TypeName = "numeric")]
        public decimal CHIA_NHOM { get; set; }

        [Required]
        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        
        [Column("Tso_luong1",TypeName = "numeric")]
        public decimal Tso_luong1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("loai_ck")]
        public string loai_ck { get; set; }

        
        [Column("pt_ck",TypeName = "numeric")]
        public decimal pt_ck { get; set; }

        [Required]
        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [Required]
        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [Column("sua_ck")]
        public byte sua_ck { get; set; }

        
        [Column("NGAY_CT1",TypeName = "smalldatetime")]
        public DateTime? NGAY_CT1 { get; set; }

        
        [Column("NGAY_CT2",TypeName = "smalldatetime")]
        public DateTime? NGAY_CT2 { get; set; }

        
        [Column("NGAY_TRT",TypeName = "smalldatetime")]
        public DateTime? NGAY_TRT { get; set; }

        public virtual ICollection<AD91> AD91 { get; set; }
    }

    public partial class AM86
    {
        public AM86()
        {
            AD86 = new HashSet<AD86>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(12)]
        [Column("so_seri")]
        public string so_seri { get; set; }

        [StringLength(12)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal? thue_suat { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal? t_thue { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal? t_thue_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal? t_tt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal? t_tt_nt { get; set; }

        [StringLength(32)]
        [Column("ghi_chu")]
        public string ghi_chu { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [Column("sua_thue")]
        public byte? sua_thue { get; set; }

        [Column("mau_bc")]
        public byte? mau_bc { get; set; }

        [Column("han_tt")]
        public byte? han_tt { get; set; }

        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        
        [Column("TSo_luong1",TypeName = "numeric")]
        public decimal? TSo_luong1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        
        [Column("T_Ck_nt",TypeName = "numeric")]
        public decimal T_Ck_nt { get; set; }

        
        [Column("T_Ck",TypeName = "numeric")]
        public decimal T_Ck { get; set; }

        
        [Column("T_Gg_nt",TypeName = "numeric")]
        public decimal T_Gg_nt { get; set; }

        
        [Column("T_Gg",TypeName = "numeric")]
        public decimal T_Gg { get; set; }

        
        [Column("T_Tien1_Nt",TypeName = "numeric")]
        public decimal T_Tien1_Nt { get; set; }

        
        [Column("T_Tien1",TypeName = "numeric")]
        public decimal T_Tien1 { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_mauhd")]
        public string ma_mauhd { get; set; }

        public virtual ICollection<AD86> AD86 { get; set; }
    }


    public partial class AM85
    {
        public AM85()
        {
            AD85 = new HashSet<AD85>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_khon")]
        public string ma_khon { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_kho")]
        public string ma_kho { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(13)]
        [Column("stt_rec_dc")]
        public string stt_rec_dc { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        
        [Column("tso_luong1",TypeName = "numeric")]
        public decimal? tso_luong1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD85> AD85 { get; set; }
    }


    public partial class AM84
    {
        public AM84()
        {
            AD84 = new HashSet<AD84>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        
        [Column("tso_luong1",TypeName = "numeric")]
        public decimal? tso_luong1 { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_SP")]
        public string MA_SP { get; set; }

        
        [Column("SL_SP",TypeName = "numeric")]
        public decimal SL_SP { get; set; }

        [Required]
        [StringLength(13)]
        [Column("STT_RECPN")]
        public string STT_RECPN { get; set; }

        public virtual ICollection<AD84> AD84 { get; set; }
    }


    public partial class AM81
    {
        public AM81()
        {
            AD81 = new HashSet<AD81>();
        }

        [Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string SttRec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string MaCT { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string MaNk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string MaGd { get; set; }

        
        [Column("ngay_ct", TypeName = "smalldatetime")]
        public DateTime NgayCt { get; set; }

        
        [Column("ngay_lct", TypeName = "smalldatetime")]
        public DateTime NgayLct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_seri")]
        public string SoSeri { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string SoCt { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string SoLo { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo1")]
        public string SoLo1 { get; set; }

        
        [Column("ngay_lo", TypeName = "smalldatetime")]
        public DateTime? NgayLo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string MaKh { get; set; }

         [Column("tk_cn")]
        public byte TkCn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string OngBa { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(18)]
        [Column("ma_so_thue")]
        public string MaSoThue { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string DienGiai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_bp")]
        public string MaBp { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string MaNx { get; set; }

        
        [Column("t_so_luong", TypeName = "numeric")]
        public decimal SoLuong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string MaNt { get; set; }

        
        [Column("ty_gia", TypeName = "numeric")]
        public decimal TyGia { get; set; }

        
        [Column("t_tien_nt", TypeName = "numeric")]
        public decimal TienNt { get; set; }

        
        [Column("t_tien", TypeName = "numeric")]
        public decimal Tien { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_thue")]
        public string MaThue { get; set; }

        
        [Column("thue_suat", TypeName = "numeric")]
        public decimal ThueSuat { get; set; }

        
        [Column("t_thue_nt", TypeName = "numeric")]
        public decimal ThueNt { get; set; }

        
        [Column("t_thue", TypeName = "numeric")]
        public decimal Thue { get; set; }

         [Column("sua_thue")]
        public byte SuaThue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string TkThueCo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string TkThueNo { get; set; }

        
        [Column("sua_tkthue")]
        public decimal SuaTkthue { get; set; }

        
        [Column("t_tien_nt2", TypeName = "numeric")]
        public decimal TienNt2 { get; set; }

        
        [Column("t_tien2")]
        public decimal Tien2 { get; set; }

         [Column("tinh_ck")]
        public byte TinhCk { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_ck")]
        public string TkCk { get; set; }

        
        [Column("t_ck_nt", TypeName = "numeric")]
        public decimal CkNt { get; set; }

        
        [Column("t_ck")]
        public decimal Ck { get; set; }

        
        [Column("han_tt", TypeName = "numeric")]
        public decimal HanTt { get; set; }

        
        [Column("t_tt_nt", TypeName = "numeric")]
        public decimal TtNt { get; set; }

        
        [Column("t_tt", TypeName = "numeric")]
        public decimal Tt { get; set; }
       
        
        [Column("date0", TypeName = "smalldatetime")]
        public DateTime Date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string Time0 { get; set; }

        [Column("user_id0")]
        public byte UserId0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string Status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string MaDvcs { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string SoDh { get; set; }

        
        [Column("date2", TypeName = "smalldatetime")]
        public DateTime Date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string Time2 { get; set; }

        [Column("user_id2")]
        public byte UserId2 { get; set; }

        [StringLength(48)]
        [Column("ten_vtthue")]
        public string TenVtthue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string MaUd2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string MaUd3 { get; set; }

        
        [Column("ngay_ud1", TypeName = "smalldatetime")]
        public DateTime? NgayUd1 { get; set; }

        
        [Column("ngay_ud2", TypeName = "smalldatetime")]
        public DateTime? NgayUd2 { get; set; }

        
        [Column("ngay_ud3", TypeName = "smalldatetime")]
        public DateTime? NgayUd3 { get; set; }

        
        [Column("sl_ud1", TypeName = "numeric")]
        public decimal? SlUd1 { get; set; }

        
        [Column("sl_ud2", TypeName = "numeric")]
        public decimal? SlUd2 { get; set; }

        
        [Column("sl_ud3", TypeName = "numeric")]
        public decimal? SlUd3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string GcUd1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string GcUd2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string GcUd3 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string MaUd1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string KV { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string MaHttt { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string MaNvien { get; set; }

        
        [Column("Tso_luong1")]
        public decimal? TsoLuong1 { get; set; }

        [StringLength(1)]
        [Column("loai_ck")]
        public string LoaiCk { get; set; }

        
        [Column("pt_ck", TypeName = "numeric")]
        public decimal? PtCk { get; set; }

        [Column("sua_ck")]
        public byte? SuaCk { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string KieuPost { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string Xtag { get; set; }

        
        [Column("T_Tien1", TypeName = "numeric")]
        public decimal Tien1 { get; set; }

        
        [Column("T_Tien1_nt", TypeName = "numeric")]
        public decimal Tien1Nt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("Ma_SONB")]
        public string MaSonb { get; set; }

        
        [Column("T_gg_nt", TypeName = "numeric")]
        public decimal GgNt { get; set; }

        
        [Column("T_gg", TypeName = "numeric")]
        public decimal Gg { get; set; }

        [Required]
        [StringLength(16)]
        [Column("TK_GG")]
        public string TkGg { get; set; }

        [Required]
        [StringLength(1)]
        [Column("LOAI_HD")]
        public string LoaiHd { get; set; }

        
        [Column("NGAY_CT4", TypeName = "smalldatetime")]
        public DateTime NgayCt4 { get; set; }

        
        [Column("T_TIEN_NT4", TypeName = "numeric")]
        public decimal TienNt4 { get; set; }

        
        [Column("T_TIEN4", TypeName = "numeric")]
        public decimal Tien4 { get; set; }

        [Required]
        [StringLength(128)]
        [Column("GHI_CHU01")]
        public string GhiChu01 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("LOAI_CT0")]
        public string LoaiCt0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("MA_LCT")]
        public string MaLct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("TT_LOAI")]
        public string Loai { get; set; }

        [Required]
        [StringLength(40)]
        [Column("TT_NGHE")]
        public string Nghe { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_SPPH")]
        public string MaSpph { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_TD2PH")]
        public string MaTd2Ph { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_TD3PH")]
        public string MaTd3Ph { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TT_SOXE")]
        public string SoXe { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TT_SOKHUNG")]
        public string SoKhung { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TT_SOMAY")]
        public string SoMay { get; set; }

        [Required]
        [StringLength(4)]
        [Column("TT_NAMNU")]
        public string NamNu { get; set; }

        
        [Column("TT_TUOI", TypeName = "numeric")]
        public decimal Tuoi { get; set; }

        
        [Column("TT_KMDI", TypeName = "numeric")]
        public decimal Kmdi { get; set; }

        
        [Column("TT_LANKT", TypeName = "numeric")]
        public decimal Lankt { get; set; }

        
        [Column("TT_NGAYMUA", TypeName = "smalldatetime")]
        public DateTime? NgayMua { get; set; }

        [StringLength(48)]
        [Column("TT_NOIMUA")]
        public string NoiMua { get; set; }

        [Required]
        [StringLength(20)]
        [Column("DIEN_THOAI")]
        public string DienThoai { get; set; }

        [Required]
        [StringLength(20)]
        [Column("DT_DD")]
        public string DtDd { get; set; }

        [Required]
        [StringLength(128)]
        [Column("SO_IMAGE")]
        public string SoImage { get; set; }

        [Required]
        [StringLength(100)]
        [Column("TT_SONHA")]
        public string SoNha { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_PHUONG")]
        public string MaPhuong { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_TINH")]
        public string MaTinh { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_QUAN")]
        public string MaQuan { get; set; }

        [Required]
        [StringLength(48)]
        [Column("TT_NVSC")]
        public string Nvsc { get; set; }

        [Required]
        [StringLength(1)]
        [Column("TT_LISTNV")]
        public string ListNv { get; set; }

        [Required]
        [StringLength(8)]
        [Column("MA_BP1")]
        public string MaBp1 { get; set; }

        [StringLength(48)]
        [Column("MA_THE")]
        public string MaThe { get; set; }

        [Required]
        [StringLength(5)]
        [Column("LOAI_THE")]
        public string LoaiThe { get; set; }

        [Required]
        [StringLength(5)]
        [Column("TT_GIOVAO")]
        public string GioVao { get; set; }

        [Required]
        [StringLength(5)]
        [Column("TT_GIORA")]
        public string GioRa { get; set; }

        [Required]
        [StringLength(20)]
        [Column("SO_CMND")]
        public string SoCmnd { get; set; }

        
        [Column("NGAY_CMND", TypeName = "smalldatetime")]
        public DateTime? NgayCmnd { get; set; }

        [Required]
        [StringLength(40)]
        [Column("NOI_CMND")]
        public string NoiCmnd { get; set; }

        
        [Column("NAM_SINH", TypeName = "numeric")]
        public decimal NamSinh { get; set; }

        
        [Column("NGAY_SINH",TypeName = "smalldatetime")]
        public DateTime? NgaySinh { get; set; }

        
        [Column("NGAY_GIAO", TypeName = "smalldatetime")]
        public DateTime? NgayGiao { get; set; }

        [Required]
        [StringLength(128)]
        [Column("GHI_CHU02")]
        public string GhiChu02 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT01")]
        public string MaNt01 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT02")]
        public string MaNt02 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT03")]
        public string MaNt03 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT04")]
        public string MaNt04 { get; set; }

        
        [Column("TY_GIA01", TypeName = "numeric")]
        public decimal TyGia01 { get; set; }

        
        [Column("TY_GIA02", TypeName = "numeric")]
        public decimal TyGia02 { get; set; }

        
        [Column("TY_GIA03", TypeName = "numeric")]
        public decimal TyGia03 { get; set; }

        
        [Column("TY_GIA04", TypeName = "numeric")]
        public decimal TyGia04 { get; set; }

        
        [Column("TTIEN_NT01", TypeName = "numeric")]
        public decimal TienNt01 { get; set; }

        
        [Column("TTIEN_NT02", TypeName = "numeric")]
        public decimal TienNt02 { get; set; }

        
        [Column("TTIEN_NT03", TypeName = "numeric")]
        public decimal TienNt03 { get; set; }

        
        [Column("TTIEN_NT04", TypeName = "numeric")]
        public decimal TienNt04 { get; set; }

        
        [Column("TTIEN01", TypeName = "numeric")]
        public decimal Tien01 { get; set; }

        
        [Column("TTIEN02", TypeName = "numeric")]
        public decimal Tien02 { get; set; }

        
        [Column("TTIEN03", TypeName = "numeric")]
        public decimal Tien03 { get; set; }

        
        [Column("TTIEN04",TypeName = "numeric")]
        public decimal Tien04 { get; set; }

        
        [Column("T_TIEN_NT5", TypeName = "numeric")]
        public decimal TienNt5 { get; set; }

        
        [Column("T_TIEN5", TypeName = "numeric")]
        public decimal Tien5 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("MA_KH3")]
        public string MaKh3 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("TT_SOLSX")]
        public string SoLsx { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_mauhd")]
        public string MaMauhd { get; set; }

        [Column("AD81")]
        public virtual ICollection<AD81> AD81 { get; set; }
    }


    public partial class AM76
    {
        public AM76()
        {
            AD76 = new HashSet<AD76>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(9)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        [Required]
        [StringLength(9)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(18)]
        [Column("ma_so_thue")]
        public string ma_so_thue { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Column("sua_thue")]
        public byte sua_thue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("sua_tkthue",TypeName = "numeric")]
        public decimal sua_tkthue { get; set; }

        
        [Column("t_tien_nt2",TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        
        [Column("t_tien2",TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        
        [Column("han_tt",TypeName = "numeric")]
        public decimal han_tt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(32)]
        [Column("ghi_chu")]
        public string ghi_chu { get; set; }

        [StringLength(48)]
        [Column("ten_vtthue")]
        public string ten_vtthue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        
        [Column("Tso_luong1",TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        [Column("Tso_luong1")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        
        [Column("T_Ck_nt",TypeName = "numeric")]
        public decimal T_Ck_nt { get; set; }

        
        [Column("T_Ck",TypeName = "numeric")]
        public decimal T_Ck { get; set; }

        
        [Column("T_Gg_nt",TypeName = "numeric")]
        public decimal? T_Gg_nt { get; set; }

        
        [Column("T_Gg",TypeName = "numeric")]
        public decimal? T_Gg { get; set; }

        
        [Column("T_Tien1_Nt",TypeName = "numeric")]
        public decimal? T_Tien1_Nt { get; set; }

        
        [Column("T_Tien1",TypeName = "numeric")]
        public decimal? T_Tien1 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT01")]
        public string MA_NT01 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT02")]
        public string MA_NT02 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT03")]
        public string MA_NT03 { get; set; }

        [Required]
        [StringLength(3)]
        [Column("MA_NT04")]
        public string MA_NT04 { get; set; }

        
        [Column("TY_GIA01",TypeName = "numeric")]
        public decimal TY_GIA01 { get; set; }

        
        [Column("TY_GIA02",TypeName = "numeric")]
        public decimal TY_GIA02 { get; set; }

        
        [Column("TY_GIA03",TypeName = "numeric")]
        public decimal TY_GIA03 { get; set; }

        
        [Column("TY_GIA04",TypeName = "numeric")]
        public decimal TY_GIA04 { get; set; }

        
        [Column("TTIEN_NT01",TypeName = "numeric")]
        public decimal TTIEN_NT01 { get; set; }

        
        [Column("TTIEN_NT02",TypeName = "numeric")]
        public decimal TTIEN_NT02 { get; set; }

        
        [Column("TTIEN_NT03",TypeName = "numeric")]
        public decimal TTIEN_NT03 { get; set; }

        
        [Column("TTIEN_NT04",TypeName = "numeric")]
        public decimal TTIEN_NT04 { get; set; }

        
        [Column("TTIEN01",TypeName = "numeric")]
        public decimal TTIEN01 { get; set; }

        
        [Column("TTIEN02",TypeName = "numeric")]
        public decimal TTIEN02 { get; set; }

        
        [Column("TTIEN03",TypeName = "numeric")]
        public decimal TTIEN03 { get; set; }

        
        [Column("TTIEN04",TypeName = "numeric")]
        public decimal TTIEN04 { get; set; }

        
        [Column("t_tien_nt4",TypeName = "numeric")]
        public decimal t_tien_nt4 { get; set; }

        
        [Column("t_tien4",TypeName = "numeric")]
        public decimal t_tien4 { get; set; }

        
        [Column("ngay_ct4",TypeName = "smalldatetime")]
        public DateTime ngay_ct4 { get; set; }

        [Required]
        [StringLength(128)]
        [Column("GHI_CHU01")]
        public string GHI_CHU01 { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TT_SOKHUNG")]
        public string TT_SOKHUNG { get; set; }

        [Required]
        [StringLength(20)]
        [Column("TT_SOMAY")]
        public string TT_SOMAY { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_spph")]
        public string ma_spph { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_td2ph")]
        public string ma_td2ph { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_td3ph")]
        public string ma_td3ph { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_mauhd")]
        public string ma_mauhd { get; set; }

        public virtual ICollection<AD76> AD76 { get; set; }
    }


    public partial class AM74
    {
        public AM74()
        {
            AD74 = new HashSet<AD74>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        
        [Column("Tso_luong1",TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD74> AD74 { get; set; }
    }


    public partial class AM73
    {
        public AM73()
        {
            AD73 = new HashSet<AD73>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(13)]
        [Column("stt_rec_pn")]
        public string stt_rec_pn { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_pn")]
        public string so_pn { get; set; }

        
        [Column("ngay_pn",TypeName = "smalldatetime")]
        public DateTime? ngay_pn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tien_nt0",TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        
        [Column("t_tien0",TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        
        [Column("t_cp_nt",TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        
        [Column("t_cp",TypeName = "numeric")]
        public decimal t_cp { get; set; }

        [Column("cp_thue_ck")]
        public byte cp_thue_ck { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Column("han_tt")]
        public byte han_tt { get; set; }

        [Column("so_hd_gtgt")]
        public byte so_hd_gtgt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Column("loai_hd")]
        public byte loai_hd { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        
        [Column("Tso_luong1",TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD73> AD73 { get; set; }
    }


    public partial class AM72
    {
        public AM72()
        {
            AD72 = new HashSet<AD72>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tien_nt0",TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        
        [Column("t_tien0",TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        
        [Column("t_cp_nt",TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        [Column("t_cp", TypeName = "numeric")]
        public decimal t_cp { get; set; }

        [Column("t_nk_nt", TypeName = "numeric")]
        public decimal t_nk_nt { get; set; }

        
        [Column("t_nk",TypeName = "numeric")]
        public decimal t_nk { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_nk")]
        public string tk_thue_nk { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Column("cp_thue_ck")]
        public byte cp_thue_ck { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Column("han_tt")]
        public byte han_tt { get; set; }

        [Column("so_hd_gtgt")]
        public byte so_hd_gtgt { get; set; }

        [Column("loai_hd")]
        public byte loai_hd { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        [StringLength(8)]
        [Column("ms_kh2")]
        public string ms_kh2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        
        [Column("Tso_luong1",TypeName = "numeric")]
        public decimal? Tso_luong1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD72> AD72 { get; set; }
    }


    public partial class AM71
    {
        public AM71()
        {
            AD71 = new HashSet<AD71>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        
        [Column("t_so_luong",TypeName = "numeric")]
        public decimal t_so_luong { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tien_nt0",TypeName = "numeric")]
        public decimal t_tien_nt0 { get; set; }

        
        [Column("t_tien0",TypeName = "numeric")]
        public decimal t_tien0 { get; set; }

        
        [Column("t_cp_nt",TypeName = "numeric")]
        public decimal t_cp_nt { get; set; }

        
        [Column("t_cp",TypeName = "numeric")]
        public decimal t_cp { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Column("cp_thue_ck")]
        public byte cp_thue_ck { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Column("han_tt")]
        public byte han_tt { get; set; }

        [Column("so_hd_gtgt")]
        public byte so_hd_gtgt { get; set; }

        [Column("loai_hd")]
        public byte loai_hd { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("K_V")]
        public string K_V { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        
        [Column("tso_luong1",TypeName = "numeric")]
        public decimal? tso_luong1 { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        
        [Column("T_CK_NT",TypeName = "numeric")]
        public decimal T_CK_NT { get; set; }

        
        [Column("T_CK",TypeName = "numeric")]
        public decimal T_CK { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Ck_vat")]
        public string Ck_vat { get; set; }

        
        [Column("T_Gg_nt",TypeName = "numeric")]
        public decimal T_Gg_nt { get; set; }

        
        [Column("T_Gg",TypeName = "numeric")]
        public decimal T_Gg { get; set; }

        [StringLength(16)]
        [Column("Tk_gg")]
        public string Tk_gg { get; set; }

        [Required]
        [StringLength(1)]
        [Column("LOAI_CT0")]
        public string LOAI_CT0 { get; set; }

        public virtual ICollection<AD71> AD71 { get; set; }
    }


    public partial class AM57
    {
        public AM57()
        {
            AD57 = new HashSet<AD57>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_td")]
        public string ma_td { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_du")]
        public string tk_du { get; set; }

        
        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        
        [Column("t_tien_ht",TypeName = "numeric")]
        public decimal t_tien_ht { get; set; }

        
        [Column("t_cltg",TypeName = "numeric")]
        public decimal t_cltg { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_cltg")]
        public string tk_cltg { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0",TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD57> AD57 { get; set; }
    }


    public partial class AM56
    {
        public AM56()
        {
            AD56 = new HashSet<AD56>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Required]
        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        
        [Column("so_hd_gtgt",TypeName = "numeric")]
        public decimal so_hd_gtgt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("date0")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        
        [Column("ty_gia_ht")]
        public decimal ty_gia_ht { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [StringLength(13)]
        [Column("stt_rec_kt")]
        public string stt_rec_kt { get; set; }

        public virtual ICollection<AD56> AD56 { get; set; }
    }


    public partial class AM52
    {
        public AM52()
        {
            AD52 = new HashSet<AD52>();
        }

        ////[Key]
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

        [Required]
        [StringLength(8)]
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
        public string ma_ku { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(16)]
        public string ma_td { get; set; }

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

        [Required]
        [StringLength(16)]
        public string tk_du { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_tien_ht { get; set; }

        [Column(TypeName = "numeric")]
        public decimal t_cltg { get; set; }

        [Required]
        [StringLength(16)]
        public string tk_cltg { get; set; }

        [Column(TypeName = "smalldatetime")]
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

        [StringLength(1)]
        public string kieu_post { get; set; }

        [StringLength(1)]
        public string xtag { get; set; }

        public virtual ICollection<AD52> AD52 { get; set; }
    }


    public partial class AM51
    {
        public AM51()
        {
            AD51 = new HashSet<AD51>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        [Column("t_tt",TypeName = "numeric")]
        
        public decimal t_tt { get; set; }

        [Required]
        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        
        [Column("so_hd_gtgt",TypeName = "numeric")]
        public decimal so_hd_gtgt { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        [Column("date0",TypeName = "smalldatetime")]
        
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        
        [Column("t_tt_qd",TypeName = "numeric")]
        public decimal? t_tt_qd { get; set; }

        [Column("tat_toan")]
        public byte? tat_toan { get; set; }

        [Column("sua_thue")]
        public byte? sua_thue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

         [Column("ngay_ud1",TypeName = "smalldatetime")]
        
        public DateTime? ngay_ud1 { get; set; }

        
         [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        
        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [StringLength(13)]
        [Column("stt_rec_kt")]
        public string stt_rec_kt { get; set; }

        public virtual ICollection<AD51> AD51 { get; set; }
    }


    public partial class AM47
    {
        public AM47()
        {
            AD47 = new HashSet<AD47>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_td")]
        public string ma_td { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_du")]
        public string tk_du { get; set; }

        
        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        
        [Column("t_tien_ht",TypeName = "numeric")]
        public decimal t_tien_ht { get; set; }

        
        [Column("t_cltg",TypeName = "numeric")]
        public decimal t_cltg { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_cltg")]
        public string tk_cltg { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0",TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD47> AD47 { get; set; }
    }


    public partial class AM46
    {
        public AM46()
        {
            AD46 = new HashSet<AD46>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Required]
        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        
        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [StringLength(13)]
        [Column("stt_rec_kt")]
        public string stt_rec_kt { get; set; }

        public virtual ICollection<AD46> AD46 { get; set; }
    }


    public partial class AM42
    {
        public AM42()
        {
            AD42 = new HashSet<AD42>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_ku")]
        public string ma_ku { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_vv")]
        public string ma_vv { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_td")]
        public string ma_td { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_du")]
        public string tk_du { get; set; }

        
        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        
        [Column("t_tien_ht",TypeName = "numeric")]
        public decimal t_tien_ht { get; set; }

        
        [Column("t_cltg",TypeName = "numeric")]
        public decimal t_cltg { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_cltg")]
        public string tk_cltg { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0",TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD42> AD42 { get; set; }
    }


    public partial class AM41
    {
        public AM41()
        {
            AD41 = new HashSet<AD41>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk")]
        public string tk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        [Required]
        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [Required]
        [StringLength(2)]
        [Column("loai_ct")]
        public string loai_ct { get; set; }

        [Column("ty_gia_ht",TypeName = "numeric")]
        public decimal ty_gia_ht { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [StringLength(13)]
        [Column("stt_rec_kt")]
        public string stt_rec_kt { get; set; }

        public virtual ICollection<AD41> AD41 { get; set; }
    }


    public partial class AM39
    {
        public AM39()
        {
            AD39 = new HashSet<AD39>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("hd_gtgt")]
        public byte? hd_gtgt { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD39> AD39 { get; set; }
    }


    public partial class AM32
    {
        public AM32()
        {
            AD32 = new HashSet<AD32>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("so_hd_gtgt",TypeName = "numeric")]
        public decimal so_hd_gtgt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("han_tt",TypeName = "numeric")]
        public decimal han_tt { get; set; }

        [Column("date0")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0",TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [StringLength(128)]
        [Column("so_ct_tt")]
        public string so_ct_tt { get; set; }

        [Column("sua_thue")]
        public byte? sua_thue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD32> AD32 { get; set; }
    }


    public partial class AM31
    {
        public AM31()
        {
            AD31 = new HashSet<AD31>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [StringLength(12)]
        [Column("so_seri0")]
        public string so_seri0 { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct0")]
        public string so_ct0 { get; set; }

        
        [Column("ngay_ct0",TypeName = "smalldatetime")]
        public DateTime? ngay_ct0 { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("so_hd_gtgt",TypeName = "numeric")]
        public decimal so_hd_gtgt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt")]
        public decimal t_tt { get; set; }

        
        [Column("han_tt",TypeName = "numeric")]
        public decimal han_tt { get; set; }

        [Column("date0")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        
        [Column("user_id0",TypeName = "numeric")]
        public decimal user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        
        [Column("user_id2",TypeName = "numeric")]
        public decimal user_id2 { get; set; }

        [StringLength(16)]
        [Column("ma_kh2")]
        public string ma_kh2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(2)]
        [Column("ma_httt")]
        public string ma_httt { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD31> AD31 { get; set; }
    }


    public partial class AM29
    {
        public AM29()
        {
            AD29 = new HashSet<AD29>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("hd_gtgt")]
        public byte? hd_gtgt { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD29> AD29 { get; set; }
    }


    public partial class AM21
    {
        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_seri")]
        public string so_seri { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [StringLength(16)]
        [Column("so_dh")]
        public string so_dh { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_kh")]
        public string ma_kh { get; set; }

        [Column("tk_cn")]
        public byte tk_cn { get; set; }

        [StringLength(32)]
        [Column("ong_ba")]
        public string ong_ba { get; set; }

        [StringLength(128)]
        [Column("dia_chi")]
        public string dia_chi { get; set; }

        [Required]
        [StringLength(128)]
        [Column("dien_giai")]
        public string dien_giai { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [Required]
        [StringLength(16)]
        [Column("ma_nx")]
        public string ma_nx { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt2",TypeName = "numeric")]
        public decimal t_tien_nt2 { get; set; }

        
        [Column("t_tien2",TypeName = "numeric")]
        public decimal t_tien2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_thue")]
        public string ma_thue { get; set; }

        
        [Column("thue_suat",TypeName = "numeric")]
        public decimal thue_suat { get; set; }

        
        [Column("t_thue_nt",TypeName = "numeric")]
        public decimal t_thue_nt { get; set; }

        
        [Column("t_thue",TypeName = "numeric")]
        public decimal t_thue { get; set; }

        
        [Column("sua_thue",TypeName = "numeric")]
        public decimal sua_thue { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_co")]
        public string tk_thue_co { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_thue_no")]
        public string tk_thue_no { get; set; }

        
        [Column("sua_tkthue",TypeName = "numeric")]
        public decimal sua_tkthue { get; set; }

        
        [Column("tinh_ck",TypeName = "numeric")]
        public decimal tinh_ck { get; set; }

        [Required]
        [StringLength(16)]
        [Column("tk_ck")]
        public string tk_ck { get; set; }

        
        [Column("t_ck_nt",TypeName = "numeric")]
        public decimal t_ck_nt { get; set; }

        
        [Column("t_ck",TypeName = "numeric")]
        public decimal t_ck { get; set; }

        
        [Column("han_tt",TypeName = "numeric")]
        public decimal han_tt { get; set; }

        
        [Column("t_tt_nt",TypeName = "numeric")]
        public decimal t_tt_nt { get; set; }

        
        [Column("t_tt",TypeName = "numeric")]
        public decimal t_tt { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [StringLength(48)]
        [Column("ten_vtthue")]
        public string ten_vtthue { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(2)]
        [Column("ma_httts")]
        public string ma_httt { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        
        [Column("pt_ck",TypeName = "numeric")]
        public decimal? pt_ck { get; set; }

        [Column("sua_ck")]
        public byte? sua_ck { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        [Required]
        [StringLength(20)]
        [Column("ma_mauhd")]
        public string ma_mauhd { get; set; }
    }


    public partial class AM11
    {
        public AM11()
        {
            AD11 = new HashSet<AD11>();
        }

        ////[Key]
        [StringLength(13)]
        [Column("stt_rec")]
        public string stt_rec { get; set; }

        [Required]
        [StringLength(8)]
        [Column("ma_dvcs")]
        public string ma_dvcs { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_ct")]
        public string ma_ct { get; set; }

        
        [Column("ngay_ct",TypeName = "smalldatetime")]
        public DateTime ngay_ct { get; set; }

        
        [Column("ngay_lct",TypeName = "smalldatetime")]
        public DateTime ngay_lct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_ct")]
        public string so_ct { get; set; }

        [Required]
        [StringLength(12)]
        [Column("so_lo")]
        public string so_lo { get; set; }

        
        [Column("ngay_lo",TypeName = "smalldatetime")]
        public DateTime? ngay_lo { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nk")]
        public string ma_nk { get; set; }

        [Required]
        [StringLength(1)]
        [Column("ma_gd")]
        public string ma_gd { get; set; }

        [Required]
        [StringLength(3)]
        [Column("ma_nt")]
        public string ma_nt { get; set; }

        
        [Column("ty_gia",TypeName = "numeric")]
        public decimal ty_gia { get; set; }

        
        [Column("t_tien_nt",TypeName = "numeric")]
        public decimal t_tien_nt { get; set; }

        
        [Column("t_tien",TypeName = "numeric")]
        public decimal t_tien { get; set; }

        
        [Column("date0",TypeName = "smalldatetime")]
        public DateTime date0 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time0")]
        public string time0 { get; set; }

        [Column("user_id0")]
        public byte user_id0 { get; set; }

        [Required]
        [StringLength(1)]
        [Column("status")]
        public string status { get; set; }

        
        [Column("date2",TypeName = "smalldatetime")]
        public DateTime date2 { get; set; }

        [Required]
        [StringLength(8)]
        [Column("time2")]
        public string time2 { get; set; }

        [Column("user_id2")]
        public byte user_id2 { get; set; }

        [Column("hd_gtgt")]
        public byte? hd_gtgt { get; set; }

        [StringLength(16)]
        [Column("ma_ud2")]
        public string ma_ud2 { get; set; }

        [StringLength(16)]
        [Column("ma_ud3")]
        public string ma_ud3 { get; set; }

        
        [Column("ngay_ud1",TypeName = "smalldatetime")]
        public DateTime? ngay_ud1 { get; set; }

        
        [Column("ngay_ud2",TypeName = "smalldatetime")]
        public DateTime? ngay_ud2 { get; set; }

        
        [Column("ngay_ud3",TypeName = "smalldatetime")]
        public DateTime? ngay_ud3 { get; set; }

        
        [Column("sl_ud1",TypeName = "numeric")]
        public decimal? sl_ud1 { get; set; }

        
        [Column("sl_ud2",TypeName = "numeric")]
        public decimal? sl_ud2 { get; set; }

        
        [Column("sl_ud3",TypeName = "numeric")]
        public decimal? sl_ud3 { get; set; }

        [StringLength(24)]
        [Column("gc_ud1")]
        public string gc_ud1 { get; set; }

        [StringLength(24)]
        [Column("gc_ud2")]
        public string gc_ud2 { get; set; }

        [StringLength(24)]
        [Column("gc_ud3")]
        public string gc_ud3 { get; set; }

        [StringLength(12)]
        [Column("so_lo1")]
        public string so_lo1 { get; set; }

        [StringLength(16)]
        [Column("ma_ud1")]
        public string ma_ud1 { get; set; }

        [StringLength(128)]
        [Column("Dien_giai")]
        public string Dien_giai { get; set; }

        [StringLength(16)]
        [Column("ma_bp")]
        public string ma_bp { get; set; }

        [StringLength(8)]
        [Column("ma_nvien")]
        public string ma_nvien { get; set; }

        [StringLength(1)]
        [Column("kieu_post")]
        public string kieu_post { get; set; }

        [StringLength(1)]
        [Column("xtag")]
        public string xtag { get; set; }

        public virtual ICollection<AD11> AD11 { get; set; }
    }
}

