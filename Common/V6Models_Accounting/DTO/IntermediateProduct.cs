using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class IntermediateProduct : V6Model
    {
        /// <summary>
        /// Column: ma_vttg
        /// Description: 
        /// </summary>
        public string MaVatTutg { get; set; }
        /// <summary>
        /// Column: part_no
        /// Description: 
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// Column: ten_vttg
        /// Description: 
        /// </summary>
        public string TenVatTutg { get; set; }
        /// <summary>
        /// Column: ten_vttg2
        /// Description: 
        /// </summary>
        public string TenVatTutg2 { get; set; }
        /// <summary>
        /// Column: dvt
        /// Description: 
        /// </summary>
        public string DonViTinh { get; set; }
        /// <summary>
        /// Column: dvt1
        /// Description: 
        /// </summary>
        public string DonViTinh1 { get; set; }
        /// <summary>
        /// Column: he_so1
        /// Description: 
        /// </summary>
        public decimal? HeSo1 { get; set; }
        /// <summary>
        /// Column: vt_ton_kho
        /// Description: 
        /// </summary>
        public byte VatTuTonKho { get; set; }
        /// <summary>
        /// Column: gia_ton
        /// Description: 
        /// </summary>
        public byte GiaTon { get; set; }
        /// <summary>
        /// Column: sua_tk_vt
        /// Description: 
        /// </summary>
        public byte? ChoPhepSuaTaiKhoanVatTu { get; set; }
        /// <summary>
        /// Column: tk_cl_vt
        /// Description: 
        /// </summary>
        public string TaiKhoan_cl_vt { get; set; }
        /// <summary>
        /// Column: tk_vt
        /// Description: 
        /// </summary>
        public string TaiKhoanVatTu { get; set; }
        /// <summary>
        /// Column: tk_gv
        /// Description: 
        /// </summary>
        public string TaiKhoanGiaVon { get; set; }
        /// <summary>
        /// Column: tk_dt
        /// Description: 
        /// </summary>
        public string TaiKhoanDoanhThu { get; set; }
        /// <summary>
        /// Column: tk_tl
        /// Description: 
        /// </summary>
        public string TaiKhoanTraLai { get; set; }
        /// <summary>
        /// Column: tk_spdd
        /// Description: 
        /// </summary>
        public string TaiKhoan_spdd { get; set; }
        /// <summary>
        /// Column: nh_vt1
        /// Description: 
        /// </summary>
        public string NhomVatTu1 { get; set; }
        /// <summary>
        /// Column: nh_vt2
        /// Description: 
        /// </summary>
        public string NhomVatTu2 { get; set; }
        /// <summary>
        /// Column: nh_vt3
        /// Description: 
        /// </summary>
        public string NhomVatTu3 { get; set; }
        /// <summary>
        /// Column: sl_min
        /// Description: 
        /// </summary>
        public decimal SoLuongToiThieu { get; set; }
        /// <summary>
        /// Column: sl_max
        /// Description: 
        /// </summary>
        public decimal SoLuongToiDa { get; set; }
        /// <summary>
        /// Column: ghi_chu
        /// Description: 
        /// </summary>
        public string GhiChu { get; set; }
        /// <summary>
        /// Column: status
        /// Description: 
        /// </summary>
        public string TrangThai { get; set; }
        /// <summary>
        /// Column: ma_td1
        /// Description: 
        /// </summary>
        public string MaTuDo1 { get; set; }
        /// <summary>
        /// Column: ma_td2
        /// Description: 
        /// </summary>
        public string MaTuDo2 { get; set; }
        /// <summary>
        /// Column: ma_td3
        /// Description: 
        /// </summary>
        public string MaTuDo3 { get; set; }
        /// <summary>
        /// Column: ngay_td1
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: ngay_td2
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: ngay_td3
        /// Description: 
        /// </summary>
        public DateTime? NgayTuDo3 { get; set; }
        /// <summary>
        /// Column: sl_td1
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo1 { get; set; }
        /// <summary>
        /// Column: sl_td2
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo2 { get; set; }
        /// <summary>
        /// Column: sl_td3
        /// Description: 
        /// </summary>
        public decimal? SoLuongTuDo3 { get; set; }
        /// <summary>
        /// Column: gc_td1
        /// Description: 
        /// </summary>
        public string GhiChuTuDo1 { get; set; }
        /// <summary>
        /// Column: gc_td2
        /// Description: 
        /// </summary>
        public string GhiChuTuDo2 { get; set; }
        /// <summary>
        /// Column: gc_td3
        /// Description: 
        /// </summary>
        public string GhiChuTuDo3 { get; set; }
        /// <summary>
        /// Column: Short_name
        /// Description: 
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        /// Column: Bar_code
        /// Description: 
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// Column: Loai_vt
        /// Description: 
        /// </summary>
        public string Loai_VatTu { get; set; }
        /// <summary>
        /// Column: Tt_vt
        /// Description: 
        /// </summary>
        public string ThongTinVatTu { get; set; }
        /// <summary>
        /// Column: Nhieu_dvt
        /// Description: 
        /// </summary>
        public string nhieu_DonViTinh { get; set; }
        /// <summary>
        /// Column: Lo_yn
        /// Description: 
        /// </summary>
        public string IsParcel { get; set; }
        /// <summary>
        /// Column: Kk_yn
        /// Description: 
        /// </summary>
        public string KK_YN { get; set; }
        /// <summary>
        /// Column: Weight
        /// Description: 
        /// </summary>
        public decimal? TrongLuong { get; set; }
        /// <summary>
        /// Column: DvtWeight
        /// Description: 
        /// </summary>
        public string DonViTinhTrongLuong { get; set; }
        /// <summary>
        /// Column: Weight0
        /// Description: 
        /// </summary>
        public decimal? TrongLuong0 { get; set; }
        /// <summary>
        /// Column: DvtWeight0
        /// Description: 
        /// </summary>
        public string DonViTinhTrongLuong0 { get; set; }
        /// <summary>
        /// Column: Length
        /// Description: 
        /// </summary>
        public decimal? Length { get; set; }
        /// <summary>
        /// Column: Width
        /// Description: 
        /// </summary>
        public decimal? ChieuNgang { get; set; }
        /// <summary>
        /// Column: Height
        /// Description: 
        /// </summary>
        public decimal? ChieuCao { get; set; }
        /// <summary>
        /// Column: Diamet
        /// Description: 
        /// </summary>
        public decimal? Diamet { get; set; }
        /// <summary>
        /// Column: DvtLength
        /// Description: 
        /// </summary>
        public string DonViTinhLength { get; set; }
        /// <summary>
        /// Column: DvtWidth
        /// Description: 
        /// </summary>
        public string DonViTinhChieuNgang { get; set; }
        /// <summary>
        /// Column: DvtHeight
        /// Description: 
        /// </summary>
        public string DonViTinhChieuCao { get; set; }
        /// <summary>
        /// Column: DvtDiamet
        /// Description: 
        /// </summary>
        public string DonViTinhDiamet { get; set; }
        /// <summary>
        /// Column: Size
        /// Description: 
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Column: Color
        /// Description: 
        /// </summary>
        public string MauSac { get; set; }
        /// <summary>
        /// Column: Style
        /// Description: 
        /// </summary>
        public string Kieu { get; set; }
        /// <summary>
        /// Column: Ma_qg
        /// Description: 
        /// </summary>
        public string MaQuocGia { get; set; }
        /// <summary>
        /// Column: Packs
        /// Description: 
        /// </summary>
        public decimal? Packs { get; set; }
        /// <summary>
        /// Column: Packs1
        /// Description: 
        /// </summary>
        public decimal? Packs1 { get; set; }
        /// <summary>
        /// Column: abc_code
        /// Description: 
        /// </summary>
        public string ABCCode { get; set; }
        /// <summary>
        /// Column: Dvtpacks
        /// Description: 
        /// </summary>
        public string DonViTinhPacks { get; set; }
        /// <summary>
        /// Column: Cycle_kk
        /// Description: 
        /// </summary>
        public decimal? cycle_kk { get; set; }
        /// <summary>
        /// Column: Ma_vitri
        /// Description: 
        /// </summary>
        public string MaViTri { get; set; }
        /// <summary>
        /// Column: Ma_kho
        /// Description: 
        /// </summary>
        public string MaKho { get; set; }
        /// <summary>
        /// Column: Han_sd
        /// Description: 
        /// </summary>
        public decimal? HanSuDung { get; set; }
        /// <summary>
        /// Column: Han_bh
        /// Description: 
        /// </summary>
        public decimal? HanBaoHanh { get; set; }
        /// <summary>
        /// Column: Kieu_lo
        /// Description: 
        /// </summary>
        public string KieuLo { get; set; }
        /// <summary>
        /// Column: Cach_xuat
        /// Description: 
        /// </summary>
        public string CachXuat { get; set; }
        /// <summary>
        /// Column: Lma_nvien
        /// Description: 
        /// </summary>
        public string EmployeeCodeL { get; set; }
        /// <summary>
        /// Column: LdatePur
        /// Description: 
        /// </summary>
        public decimal? PurchaseDateL { get; set; }
        /// <summary>
        /// Column: LdateQc
        /// Description: 
        /// </summary>
        public decimal? Ldateqc { get; set; }
        /// <summary>
        /// Column: Lso_qty
        /// Description: 
        /// </summary>
        public decimal? Lsoqty { get; set; }
        /// <summary>
        /// Column: Lso_qtymin
        /// Description: 
        /// </summary>
        public decimal? Lsoqtymin { get; set; }
        /// <summary>
        /// Column: Lso_qtymax
        /// Description: 
        /// </summary>
        public decimal? Lsoqtymax { get; set; }
        /// <summary>
        /// Column: LCycle
        /// Description: 
        /// </summary>
        public decimal? Lcycle { get; set; }
        /// <summary>
        /// Column: Lpolicy
        /// Description: 
        /// </summary>
        public string Lpolicy { get; set; }
        /// <summary>
        /// Column: Pma_nvien
        /// Description: 
        /// </summary>
        public string pMaNhanVien { get; set; }
        /// <summary>
        /// Column: Pma_khc
        /// Description: 
        /// </summary>
        public string pMaKhachHangc { get; set; }
        /// <summary>
        /// Column: Pma_khp
        /// Description: 
        /// </summary>
        public string pMaKhachHangp { get; set; }
        /// <summary>
        /// Column: Pma_khl
        /// Description: 
        /// </summary>
        public string pMaKhachHangl { get; set; }
        /// <summary>
        /// Column: Prating
        /// Description: 
        /// </summary>
        public string Prating { get; set; }
        /// <summary>
        /// Column: Pquality
        /// Description: 
        /// </summary>
        public decimal? Pquality { get; set; }
        /// <summary>
        /// Column: Pquanlity
        /// Description: 
        /// </summary>
        public decimal? pquanlity { get; set; }
        /// <summary>
        /// Column: Pdeliver
        /// Description: 
        /// </summary>
        public decimal? Pdeliver { get; set; }
        /// <summary>
        /// Column: PFlex
        /// Description: 
        /// </summary>
        public decimal? Pflex { get; set; }
        /// <summary>
        /// Column: Ptech
        /// Description: 
        /// </summary>
        public decimal? Ptech { get; set; }
        /// <summary>
        /// Column: nh_vt9
        /// Description: 
        /// </summary>
        public string NhomVatTu9 { get; set; }
        /// <summary>
        /// Column: ma_thue
        /// Description: 
        /// </summary>
        public string MaThue { get; set; }
        /// <summary>
        /// Column: ma_thueNk
        /// Description: 
        /// </summary>
        public string MaThuenk { get; set; }
        /// <summary>
        /// Column: tk_ck
        /// Description: 
        /// </summary>
        public string TaiKhoan_ck { get; set; }
        /// <summary>
        /// Column: date_yn
        /// Description: 
        /// </summary>
        public string TheoDoiDate { get; set; }
        /// <summary>
        /// Column: TK_CP
        /// Description: 
        /// </summary>
        public string TaiKhoan_cp { get; set; }
        /// <summary>
        /// Column: MA_BPHT
        /// Description: 
        /// </summary>
        public string MaBoPhanHachToan { get; set; }
        /// <summary>
        /// Column: VITRI_YN
        /// Description: 
        /// </summary>
        public string TheoDoiViTri { get; set; }
        /// <summary>
        /// Column: MA_KHTG
        /// Description: 
        /// </summary>
        public string MaKhachHangtg { get; set; }
    }
}
