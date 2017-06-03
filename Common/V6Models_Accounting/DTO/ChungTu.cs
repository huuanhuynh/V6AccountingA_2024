using System;
using V6Soft.Models.Core;

namespace V6Soft.Models.Accounting.DTO
{
    public class ChungTu : V6Model
    {
        /// <summary>
        /// Column: Module_id
        /// Description: 
        /// </summary>
        public string Module_ID { get; set; }
        /// <summary>
        /// Column: ma_phan_he
        /// Description: 
        /// </summary>
        public string MaPhanHe { get; set; }
        /// <summary>
        /// Column: ma_ct
        /// Description: 
        /// </summary>
        public string MaChungTu { get; set; }
        /// <summary>
        /// Column: ten_ct
        /// Description: 
        /// </summary>
        public string TenChungTu { get; set; }
        /// <summary>
        /// Column: ten_ct2
        /// Description: 
        /// </summary>
        public string TenChungTu2 { get; set; }
        /// <summary>
        /// Column: ma_ct_me
        /// Description: 
        /// </summary>
        public string MaChungTuMe { get; set; }
        /// <summary>
        /// Column: so_ct
        /// Description: 
        /// </summary>
        public decimal SoChungTu { get; set; }
        /// <summary>
        /// Column: m_ma_nk
        /// Description: 
        /// </summary>
        public string M_Ma_NK { get; set; }
        /// <summary>
        /// Column: m_ma_gd
        /// Description: 
        /// </summary>
        public string M_MaGiaoDich { get; set; }
        /// <summary>
        /// Column: m_ma_td
        /// Description: 
        /// </summary>
        public byte? M_Ma_TD { get; set; }
        /// <summary>
        /// Column: ma_nt
        /// Description: 
        /// </summary>
        public string MaNgoaiTe { get; set; }
        /// <summary>
        /// Column: tieu_de_ct
        /// Description: 
        /// </summary>
        public string TieuDeChungTu { get; set; }
        /// <summary>
        /// Column: tieu_de2
        /// Description: 
        /// </summary>
        public string TieuDe2 { get; set; }
        /// <summary>
        /// Column: so_lien
        /// Description: 
        /// </summary>
        public decimal SoLien { get; set; }
        /// <summary>
        /// Column: ma_ct_in
        /// Description: 
        /// </summary>
        public string MaChungTu_IN { get; set; }
        /// <summary>
        /// Column: form
        /// Description: 
        /// </summary>
        public string form { get; set; }
        /// <summary>
        /// Column: stt_ct_nkc
        /// Description: 
        /// </summary>
        public byte STT_ChungTu_NKC { get; set; }
        /// <summary>
        /// Column: stt_ctntxt
        /// Description: 
        /// </summary>
        public byte STT_ctntxt { get; set; }
        /// <summary>
        /// Column: ct_nxt
        /// Description: 
        /// </summary>
        public byte? ChungTuNhapXuatTruoc { get; set; }
        /// <summary>
        /// Column: m_phdbf
        /// Description: 
        /// </summary>
        public string M_PHDBF { get; set; }
        /// <summary>
        /// Column: m_ctdbf
        /// Description: 
        /// </summary>
        public string M_CTDBF { get; set; }
        /// <summary>
        /// Column: m_status
        /// Description: 
        /// </summary>
        public string M_TrangThai { get; set; }
        /// <summary>
        /// Column: post_type
        /// Description: 
        /// </summary>
        public byte KieuPost { get; set; }
        /// <summary>
        /// Column: m_sl_ct0
        /// Description: 
        /// </summary>
        public byte M_SoLuong_CT0 { get; set; }
        /// <summary>
        /// Column: m_trung_so
        /// Description: 
        /// </summary>
        public decimal M_TrungSo { get; set; }
        /// <summary>
        /// Column: m_loc_nsd
        /// Description: 
        /// </summary>
        public byte M_Loc_NSD { get; set; }
        /// <summary>
        /// Column: m_ong_ba
        /// Description: 
        /// </summary>
        public byte M_OngBa { get; set; }
        /// <summary>
        /// Column: m_ngay_ct
        /// Description: 
        /// </summary>
        public byte M_NgayChungTu { get; set; }
        /// <summary>
        /// Column: procedur
        /// Description: 
        /// </summary>
        public string proceDur { get; set; }
        /// <summary>
        /// Column: date2
        /// Description: 
        /// </summary>
        public DateTime? NgaySua { get; set; }
        /// <summary>
        /// Column: time2
        /// Description: 
        /// </summary>
        public string ThoiGianSua { get; set; }
        /// <summary>
        /// Column: user_id2
        /// Description: 
        /// </summary>
        public byte? NguoiSua { get; set; }
        /// <summary>
        /// Column: stt
        /// Description: 
        /// </summary>
        public byte? STT { get; set; }
        /// <summary>
        /// Column: m_ma_td2
        /// Description: 
        /// </summary>
        public string m_MaTuDo2 { get; set; }
        /// <summary>
        /// Column: m_ma_td3
        /// Description: 
        /// </summary>
        public string m_MaTuDo3 { get; set; }
        /// <summary>
        /// Column: m_ngay_td1
        /// Description: 
        /// </summary>
        public string M_NgayTuDo1 { get; set; }
        /// <summary>
        /// Column: m_sl_td1
        /// Description: 
        /// </summary>
        public string M_SoLuongTuDo1 { get; set; }
        /// <summary>
        /// Column: m_sl_td2
        /// Description: 
        /// </summary>
        public string M_SoLuongTuDo2 { get; set; }
        /// <summary>
        /// Column: m_sl_td3
        /// Description: 
        /// </summary>
        public string M_SoLuongTuDo3 { get; set; }
        /// <summary>
        /// Column: m_gc_td1
        /// Description: 
        /// </summary>
        public string m_GhiChuTuDo1 { get; set; }
        /// <summary>
        /// Column: m_gc_td2
        /// Description: 
        /// </summary>
        public string m_GhiChuTuDo2 { get; set; }
        /// <summary>
        /// Column: m_gc_td3
        /// Description: 
        /// </summary>
        public string m_GhiChuTuDo3 { get; set; }
        /// <summary>
        /// Column: post2
        /// Description: 
        /// </summary>
        public byte? Post2 { get; set; }
        /// <summary>
        /// Column: post3
        /// Description: 
        /// </summary>
        public byte? Post3 { get; set; }
        /// <summary>
        /// Column: m_ngay_td2
        /// Description: 
        /// </summary>
        public string M_NgayTuDo2 { get; set; }
        /// <summary>
        /// Column: m_ngay_td3
        /// Description: 
        /// </summary>
        public string M_NgayTuDo3 { get; set; }
        /// <summary>
        /// Column: dk_ctgs
        /// Description: 
        /// </summary>
        public string DK_CTGS { get; set; }
        /// <summary>
        /// Column: kh_yn
        /// Description: 
        /// </summary>
        public byte? LaKhachHang { get; set; }
        /// <summary>
        /// Column: cc_yn
        /// Description: 
        /// </summary>
        public byte? LaNhaCungCap { get; set; }
        /// <summary>
        /// Column: nv_yn
        /// Description: 
        /// </summary>
        public byte? LaNhanVien { get; set; }
        /// <summary>
        /// Column: ma_ct_old
        /// Description: 
        /// </summary>
        public string MaChungTuCu { get; set; }
        /// <summary>
        /// Column: m_ph_old
        /// Description: 
        /// </summary>
        public string M_PH_OLD { get; set; }
        /// <summary>
        /// Column: m_bp_bh
        /// Description: 
        /// </summary>
        public byte? M_BoPhanBaoHanh { get; set; }
        /// <summary>
        /// Column: m_ma_nvien
        /// Description: 
        /// </summary>
        public byte? M_MaNhanVien { get; set; }
        /// <summary>
        /// Column: m_ma_vv
        /// Description: 
        /// </summary>
        public byte? M_MaVuViec { get; set; }
        /// <summary>
        /// Column: m_ma_hd
        /// Description: 
        /// </summary>
        public string M_MaHopDong { get; set; }
        /// <summary>
        /// Column: m_ma_ku
        /// Description: 
        /// </summary>
        public string M_Ma_KheUoc { get; set; }
        /// <summary>
        /// Column: m_ma_phi
        /// Description: 
        /// </summary>
        public string M_Ma_Phi { get; set; }
        /// <summary>
        /// Column: m_ma_vitri
        /// Description: 
        /// </summary>
        public string M_MaViTri { get; set; }
        /// <summary>
        /// Column: m_ma_lo
        /// Description: 
        /// </summary>
        public string M_MaLo { get; set; }
        /// <summary>
        /// Column: m_ma_bpht
        /// Description: 
        /// </summary>
        public string m_MaBoPhanHachToan { get; set; }
        /// <summary>
        /// Column: m_ma_sp
        /// Description: 
        /// </summary>
        public string m_MaSanPham { get; set; }
        /// <summary>
        /// Column: m_k_post
        /// Description: 
        /// </summary>
        public string M_K_Post { get; set; }
        /// <summary>
        /// Column: Tk_no
        /// Description: 
        /// </summary>
        public string TaiKhoanNo { get; set; }
        /// <summary>
        /// Column: Tk_co
        /// Description: 
        /// </summary>
        public string TaiKhoanCo { get; set; }
        /// <summary>
        /// Column: M_MA_LNX
        /// Description: 
        /// </summary>
        public string M_Ma_LNX { get; set; }
        /// <summary>
        /// Column: M_HSD
        /// Description: 
        /// </summary>
        public string M_HanSuDung { get; set; }
        /// <summary>
        /// Column: m_ma_sonb
        /// Description: 
        /// </summary>
        public byte M_Ma_SONB { get; set; }
        /// <summary>
        /// Column: m_sxoa_nsd
        /// Description: 
        /// </summary>
        public byte M_SXOA_NSD { get; set; }
        /// <summary>
        /// Column: SIZE_CT
        /// Description: 
        /// </summary>
        public string Size_CT { get; set; }
        /// <summary>
        /// Column: THEM_IN
        /// Description: 
        /// </summary>
        public decimal THEM_IN { get; set; }
        /// <summary>
        /// Column: phandau
        /// Description: 
        /// </summary>
        public string PhanDau { get; set; }
        /// <summary>
        /// Column: phancuoi
        /// Description: 
        /// </summary>
        public string PhanCuoi { get; set; }
        /// <summary>
        /// Column: dinhdang
        /// Description: 
        /// </summary>
        public string DinhDang { get; set; }
        /// <summary>
        /// Column: M_Ma_KMB
        /// Description: 
        /// </summary>
        public string M_Ma_KMB { get; set; }
        /// <summary>
        /// Column: M_Ma_KMM
        /// Description: 
        /// </summary>
        public string M_Ma_KMM { get; set; }
        /// <summary>
        /// Column: M_SO_LSX
        /// Description: 
        /// </summary>
        public string M_So_LSX { get; set; }
        /// <summary>
        /// Column: M_MA_KHO2
        /// Description: 
        /// </summary>
        public string M_MaKho2 { get; set; }
        /// <summary>
        /// Column: F6BARCODE
        /// Description: 
        /// </summary>
        public string F6BARCODE { get; set; }
    }
}
