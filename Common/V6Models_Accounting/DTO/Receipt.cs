using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using V6Soft.Models.Core;


namespace V6Soft.Models.Accounting.DTO
{
    public class Receipt : V6Model
    {
        public Receipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
        }

        [Key]
        public string SttRec { get; set; }

        public string MaCt { get; set; }

        public string MaNk { get; set; }

        public string MaGd { get; set; }

        public DateTime? NgayCt { get; set; }

        public DateTime? NgayLct { get; set; }

        public string SoSeri { get; set; }

        public string SoCt { get; set; }

        public string SoLo { get; set; }

        public string SoLo1 { get; set; }

        public DateTime? NgayLo { get; set; }

        public string MaKh { get; set; }

        public byte? TkCn { get; set; }

        public string OngBa { get; set; }

        public string DiaChi { get; set; }

        public string MaSoThue { get; set; }

        public string DienGiai { get; set; }

        public string MaBp { get; set; }

        public string MaNx { get; set; }
                
        public decimal? SoLuong { get; set; }

        public string MaNt { get; set; }

        public decimal? TyGia { get; set; }

        public decimal? TienNt { get; set; }

        public decimal? Tien { get; set; }

        public string MaThue { get; set; }

        public decimal? ThueSuat { get; set; }

        public decimal? ThueNt { get; set; }

        public decimal? Thue { get; set; }

        public byte? SuaThue { get; set; }

        public string TkThueCo { get; set; }

        public string TkThueNo { get; set; }

        public decimal? SuaTkThue { get; set; }

        public decimal? TienNt2 { get; set; }

        public decimal? Tien2 { get; set; }

        public byte? TinhCk { get; set; }

        public string TkCk { get; set; }

        public decimal? CkNt { get; set; }

        public decimal? Ck { get; set; }

        public decimal? HanTt { get; set; }

        public decimal? TtNt { get; set; }

        public decimal? Tt { get; set; }

        public DateTime? Date0 { get; set; }

        public string Time0 { get; set; }

        public byte? UserId0 { get; set; }

        public string Status { get; set; }

        public string MaDvcs { get; set; }

        public string SoDh { get; set; }

        public DateTime? Date2 { get; set; }

        public string Time2 { get; set; }

        public byte? UserId2 { get; set; }

        public string TenVtthue { get; set; }

        public string MaUd2 { get; set; }

        public string MaUd3 { get; set; }

        public DateTime? NgayUd1 { get; set; }

        public DateTime? NgayUd2 { get; set; }

        public DateTime? NgayUd3 { get; set; }

        public decimal? SlUd1 { get; set; }

        public decimal? SlUd2 { get; set; }

        public decimal? SlUd3 { get; set; }

        public string GcUd1 { get; set; }

        public string GcUd2 { get; set; }

        public string GcUd3 { get; set; }

        public string MaUd1 { get; set; }

        public string KV { get; set; }

        public string MaHttt { get; set; }

        public string MaNvien { get; set; }

        public decimal? TsoLuong1 { get; set; }

        public string LoaiCk { get; set; }

        public decimal? PtCk { get; set; }

        public byte? SuaCk { get; set; }

        public string KieuPost { get; set; }

        public string Xtag { get; set; }

        public decimal? Tien1 { get; set; }

        public decimal? Tien1Nt { get; set; }

        public string MaSonb { get; set; }

        public decimal? GgNt { get; set; }

        public decimal? Gg { get; set; }

        public string TkGg { get; set; }

        public string LoaiHd { get; set; }

        public DateTime? NgayCt4 { get; set; }

        public decimal? TienNt4 { get; set; }

        public decimal? Tien4 { get; set; }

        public string GhiChu01 { get; set; }

        public string LoaiCt0 { get; set; }

        public string MaLct { get; set; }

        public string Loai { get; set; }

        public string Nghe { get; set; }

        public string MaSpph { get; set; }

        public string MaTd2ph { get; set; }

        public string MaTd3Ph { get; set; }

        public string SoXe { get; set; }

        public string SoKhung { get; set; }

        public string SoMay { get; set; }

        public string NamNu { get; set; }

        public decimal? Tuoi { get; set; }

        public decimal? Kmdi { get; set; }

        public decimal? Lankt { get; set; }

        public DateTime? NgayMua { get; set; }

        public string NoiMua { get; set; }

        public string DienThoai { get; set; }

        public string Dtdd { get; set; }

        public string SoImage { get; set; }

        public string SoNha { get; set; }

        public string MaPhuong { get; set; }

        public string MaTinh { get; set; }

        public string MaQuan { get; set; }

        public string Nvsc { get; set; }

        public string ListNv { get; set; }

        public string MaBp1 { get; set; }

        public string MaThe { get; set; }

        public string LoaiThe { get; set; }

        public string GioVao { get; set; }

        public string GioRa { get; set; }

        public string SoCmnd { get; set; }

        public DateTime? NgayCmnd { get; set; }

        public string NoiCmnd { get; set; }

        public decimal? NamSinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public DateTime? NgayGiao { get; set; }

        public string GhiChu02 { get; set; }

        public string MaNt01 { get; set; }

        public string MaNt02 { get; set; }

        public string MaNt03 { get; set; }

        public string MaNt04 { get; set; }

        public decimal? TyGia01 { get; set; }

        public decimal? TyGia02 { get; set; }

        public decimal? TyGia03 { get; set; }

        public decimal? TyGia04 { get; set; }

        public decimal? TienNt01 { get; set; }

        public decimal? TienNt02 { get; set; }

        public decimal? TienNt03 { get; set; }

        public decimal? TienNt04 { get; set; }

        public decimal? Tien01 { get; set; }

        public decimal? Tien02 { get; set; }

        public decimal? Tien03 { get; set; }

        public decimal? Tien04 { get; set; }

        public decimal? TienNt5 { get; set; }

        public decimal? Tien5 { get; set; }

        public string MaKh3 { get; set; }

        public string SoLsx { get; set; }

        public string MaMauHd { get; set; }

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
