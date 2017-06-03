using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace V6Soft.Models.Accounting.DTO
{
    public partial class Ad81
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
    }
}
