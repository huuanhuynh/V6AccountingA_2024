using System;
using System.ComponentModel.DataAnnotations;


namespace V6Soft.Models.Accounting.DTO
{
    public class ReceiptDetail
    {
        [Key]
        public string SttRec { get; set; }

        public string SttRect0 { get; set; }

        public string MaCt { get; set; }

        public DateTime? NgayCt { get; set; }

        public string SoCt { get; set; }

        public string MaKhoI { get; set; }

        public string MaVvI { get; set; }

        public string MaTdI { get; set; }

        public string MaVt{ get; set; }

        public string Dvt1 { get; set; }

        public decimal? HeSo1 { get; set; }

        public decimal? SoLuong1 { get; set; }

        public decimal? SoLuong { get; set; }

        public decimal? GiaNt { get; set; }

        public decimal? Gia { get; set; }

        public decimal? TienNt { get; set; }

        public decimal? Tien { get; set; }

        public decimal? GiaNt2 { get; set; }

        public decimal? Gia2 { get; set; }

        public decimal? TienNt2 { get; set; }

        public decimal? Tien2 { get; set; }

        public decimal? Thue { get; set; }

        public decimal? ThueNt { get; set; }

        public decimal? Ck { get; set; }

        public decimal? CkNt { get; set; }

        public string TkVt { get; set; }

        public string TkGv { get; set; }

        public string TkDt { get; set; }

        public string SttRecPn { get; set; }

        public string SttRec0pn { get; set; }

        public decimal? ln { get; set; }

        public string MaTd2 { get; set; }

        public string MaTd3 { get; set; }

        public DateTime? NgayTd1 { get; set; }

        public decimal? SlTd1 { get; set; }

        public decimal? SlTd2 { get; set; }

        public decimal? SlTd3 { get; set; }

        public string GcTd1 { get; set; }

        public string GcTd2 { get; set; }

        public string GcTd3 { get; set; }

        public DateTime? NgayTd2 { get; set; }

        public DateTime? NgayTd3 { get; set; }

        public string TkThueI { get; set; }

        public string PxGiaDdi { get; set; }

        public string SttRecdh { get; set; }

        public string SttRec0dh { get; set; }

        public string MaBpht { get; set; }

        public string MaSp { get; set; }

        public string Tang { get; set; }

        public string MaHd { get; set; }

        public string MaKu { get; set; }

        public string MaPhi { get; set; }

        public string MaLo { get; set; }

        public string MaViTri { get; set; }

        public string Dvt { get; set; }

        public decimal? GiaNt1 { get; set; }

        public decimal? Gia1 { get; set; }

        public decimal? GiaNt21 { get; set; }

        public decimal? Gia21 { get; set; }

        public decimal? GiaBanNt { get; set; }

        public decimal? GiaBan { get; set; }

        public string TkCki { get; set; }

        public decimal? PtCki { get; set; }

        public decimal? Tien1 { get; set; }

        public decimal? Tien1Nt { get; set; }

        public string MaLnxI { get; set; }

        public DateTime? Hsd { get; set; }

        public decimal? GgNt { get; set; }

        public decimal? Gg { get; set; }

        public string MaGia { get; set; }

        public decimal? GiaNt4 { get; set; }

        public decimal? Gia4 { get; set; }

        public decimal? TienNt4 { get; set; }

        public decimal? Tien4 { get; set; }

        public string SoKhung { get; set; }

        public string SoMay { get; set; }

        public string SoImage { get; set; }

        public string StatusDpi { get; set; }

        public string SoLsx { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
