using System;
using System.Windows.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Controls.Forms.DanhMuc.Add_Edit.Alreport;
using V6Controls.Forms.DanhMuc.Add_Edit.NhanSu;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public static class AddEditManager 
    {
        
        #region ==== Init Control ====
        /// <summary>
        /// Tạo control trong form.
        /// </summary>
        /// <param name="ma_dm">Tên bảng gốc.</param>
        /// <returns></returns>
        public static AddEditControlVirtual Init_Control(string ma_dm)
        {
            AldmConfig aldm_config = ConfigManager.GetAldmConfig(ma_dm);
            string formCode = aldm_config.FormCode;
            if (formCode != null) formCode = formCode.ToUpper();
            if (aldm_config.HaveInfo && aldm_config.EXTRA_INFOR.ContainsKey("MA_DM0"))
            {
                ma_dm = aldm_config.EXTRA_INFOR["MA_DM0"];
                //aldm_config = Config
            }

            AddEditControlVirtual FormControl = null;
            //if (V6Check_Rights())
            switch (ma_dm.ToUpper())
            {
                case "ALBP":
                    FormControl = new BoPhanAddEditForm();
                    break;
                case "ALBPCC":
                    FormControl = new BoPhanSuDungCongCuAddEditForm();
                    break;
                case "ALBPHT":
                    FormControl = new BoPhanHachToanAddEditForm();
                    break;
                case "ALBPTS":
                    FormControl = new BoPhanSuDungTangSuatAddEditForm();
                    break;
                case "ALCK":
                    FormControl = new ChietKhauAddEditForm();
                    break;
                case "ALCT":
                    FormControl = new AlctAddEditFrom();
                    break;
                case "ALDVCS":
                    FormControl = new DonViCoSoAddEditForm();
                    break;
                case "ALDVT":
                    FormControl = new DonViTinhAddEditForm();
                    break;
                case "ALGIA2":
                    if (formCode == "ALGIA2_A1") FormControl = new Algia2AddEditForm_A1();
                    else FormControl = new Algia2AddEditForm();
                    break;
                case "ALGIA0":
                    FormControl = new Algia0AddEditForm();
                    break;
                case "ALHD":
                    FormControl = new HopDongAddEditForm();
                    break;
                case "ALHTTT":
                    FormControl = new HinhThucThanhToanAddEditForm();
                    break;
                case "ALHTVC":
                    FormControl = new HinhThucVanChuyenAddEditForm();
                    break;
                case "ALKC":
                    FormControl = new AlkcAddEditForm();
                    break;
                case "ALKH":
                    if (formCode == "ALKH_A1") FormControl = new KhachHangAddEditFrom_A1();
                    else FormControl = new KhachHangAddEditFrom();
                    break;
                case "ALKHCT":
                    FormControl = new KhachHangChiTietFrom();
                    break;
                case "ALVITRICT":
                    FormControl = new ViTriChiTietAddEditForm();
                    break;
                case "ALKHO":
                    FormControl = new KhoHangAddEditForm();
                    break;
                case "ALKU":
                    FormControl = new KheUocAddEditForm();
                    break;
                case "ALKUCT":
                    FormControl = new KheUocChiTietForm();
                    break;
                case "ALLNX":
                    FormControl = new LoaiNhapXuatAddEditForm();
                    break;
                case "ALLO":
                    FormControl = new LoHangAddEditForm();
                    break;
                case "ALLOAIVC":
                    FormControl = new LoaiDichVuAddEditForm();
                    break;
                case "ALMAGIA":
                    FormControl = new MaGiaAddEditForm();
                    break;
                case "ALMAUHD":
                    FormControl = new MauHoaDonAddEditForm();
                    break;
                case "ALNHCC":
                    FormControl = new PhanNhomCongCuAddEditForm();
                    break;
                case "ALNHHD":
                    FormControl = new NhomHopDongAddEditForm();
                    break;
                case "ALNHKH":
                    FormControl = new NhomKhachHangAddEditForm();
                    break;
                case "ALNHKH2":
                    FormControl = new NhomGiaKhachHangAddEditForm();
                    break;
                case "ALNHKU":
                    FormControl = new NhomKheUocAddEditForm();
                    break;
                case "ALNHPHI":
                    FormControl = new NhomKhoanMucPhiAddEditForm();
                    break;
                case "ALNHTK0":
                    FormControl = new PhanLoaiCacTaiKhoanAddEditForm();
                    break;
                case "ALNHTK":
                    FormControl = new NhomTaiKhoanAddEditForm();
                    break;
                case "ALNHTS":
                    FormControl = new PhanNhomTaiSanAddEditForm();
                    break;
                case "ALNHVT":
                    FormControl = new NhomVatTuAddEditForm();
                    break;
                case "ALNHVV":
                    FormControl = new NhomVuViecAddEditForm();
                    break;
                case "ALNHYTCP":
                    FormControl = new NhomYeuToChiPhiAddEditForm();
                    break;
                case "ACOSXLT_ALNHYTCP":
                    FormControl = new NhomYeuToChiPhiSXLTAddEditForm();
                    break;
                case "ACOSXLSX_ALNHYTCP":
                    FormControl = new NhomYeuToChiPhiSXDHAddEditForm();
                    break;
                case "ALNT":
                    FormControl = new NgoaiTeAddEditForm();
                    break;
                case "ALNV":
                    FormControl = new NguonVonAddEditForm();
                    break;
                case "ALNVIEN":
                    FormControl = new NhanVienAddEditForm();
                    break;
                case "ALPHI":
                    FormControl = new PhiAddEditForm();
                    break;
                case "ALPHUONG":
                    FormControl = new PhuongXaAddEditForm();
                    break;
                case "ALPLCC":
                    FormControl = new PhanLoaiCongCuAddEditForm();
                    break;
                case "ALPLTS":
                    FormControl = new PhanLoaiTaiSanAddEditForm();
                    break;
                case "ALQDDVT":
                    FormControl = new QuyDoiDonViTinhAddEditForm();
                    break;
                case "ALQG":
                    FormControl = new QuocGiaAddEditForm();
                    break;
                case "ALQUAN":
                    FormControl = new QuanHuyenAddEditForm();
                    break;
                case "ALSTT":
                    FormControl = new NamTaiChinhAddEditForm();
                    break;
                case "ALTD":
                    FormControl = new TuDienNguoiDungDinhNghiaAddEditForm();
                    break;
                case "ALTD2":
                    FormControl = new TuDienTuDinhNghia2AddEditForm();
                    break;
                case "ALTD3":
                    FormControl = new TuDienTuDinhNghia3AddEditForm();
                    break;
                case "ALTGCC":
                    FormControl = new LyDoTangGiamCCDCAddEditForm();
                    break;
                case "ALTGNT":
                    FormControl = new TyGiaNgoaiTeAddEditForm();
                    break;
                case "ALTGTS":
                    FormControl = new LyDoTangGiamTSCDAddEditForm();
                    break;
                case "ALTHUE":
                    FormControl = new ThueSuatAddEditForm();
                    break;
                case "ALTINH":
                    FormControl = new TinhThanhAddEditForm();
                    break;
                case "ALTK0":
                    FormControl = new TaiKhoanAddEditForm();
                    break;
                case "ALTK2":
                    FormControl = new TieuKhoanAddEditForm();
                    break;
                case "ALTKNH":
                    FormControl = new TaiKhoanNganHangAddEditForm();
                    break;
                case "ALTTVT":
                    FormControl = new TinhTrangDichVuAddEditForm();
                    break;
                case "ALVC":
                    FormControl = new VanChuyenAddEditForm();
                    break;
                case "ALVITRI":
                    FormControl = new ViTriAddEditForm();
                    break;
                case "ALVT":
                    if (formCode == "ALVT_A1") FormControl = new VatTuAddEditForm_A1();
                    else if (formCode == "ALVT_A2") FormControl = new VatTuAddEditForm_A2();
                    else if (formCode == "ALVT_A3") FormControl = new VatTuAddEditForm_A3();
                    else if (formCode == "ALVT_A4") FormControl = new VatTuAddEditForm_A4();
                    else FormControl = new VatTuAddEditForm();
                    break;
                case "ALVTTG":
                    FormControl = new SanPhamTrungGianAddEditForm();
                    break;
                case "ALVV":
                    if (formCode.EndsWith("_A1")) FormControl = new VuViecAddEditForm_A1();
                    else FormControl = new VuViecAddEditForm();
                    break;
                case "ALYTCP":
                    FormControl = new YeuToChiPhiAddEditForm();
                    break;
                case "ACOSXLSX_ALYTCP":
                    FormControl = new YeuToChiPhiSPDHAddEditForm();
                    break;
                case "ACOSXLT_ALYTCP":
                    FormControl = new YeuToChiPhiSXLTAddEditForm();
                    break;
                case "V6OPTION":
                    FormControl = new V6OptionAddEditForm();
                    break;
                case "V6SOFT":
                    FormControl = new V6SoftAddEditForm();
                    break;
                case "V6USER":
                    FormControl = new NguoiSuDungAddEditForm();
                    break;
                case "ALCT1":
                    FormControl = new V6Alct1AddEditForm();
                    break;
                case "V6MENU":
                    FormControl = new V6MenuAddEditForm();
                    break;
                case "ALTHUE30":
                    FormControl = new ThueSuat30AddEditForm();
                    break;
                case "ALSONB":
                    FormControl = new AlsonbAddEditForm();
                    break;
                case "ALBC":
                    if (V6Login.IsAdmin)
                    {
                        FormControl = new AlbcAddEditForm();
                    }
                    else if ((Control.ModifierKeys & Keys.Control) == Keys.Control
                        && (new ConfirmPasswordV6 { TopMost = true }.ShowDialog() == DialogResult.OK))
                    {
                        FormControl = new AlbcAddEditForm();
                    }
                    else
                    {
                        FormControl = new NoRightAddEdit(V6Text.NotAnAdmin);
                        FormControl.Dock = DockStyle.Fill;
                    }
                    break;
                case "V_ALTS":
                    FormControl = new ValtsAddEditForm();
                    break;
                case "V_ALCC":
                    FormControl = new ValccAddEditForm();
                    break;
                case "V_ALTS01":
                    FormControl = new Valts01AddEditForm();
                    break;
                case "V_ALCC01":
                    FormControl = new Valcc01AddEditForm();
                    break;
                case "CORPLAN":
                    FormControl = new CorpLanAddEditForm();
                    break;
                case "CORPLAN1":
                    FormControl = new CorpLanAddEditForm();
                    break;
                case "CORPLAN2":
                    FormControl = new CorpLanAddEditForm();
                    break;
                //case "HLNS":
                case "HRPERSONAL":
                    FormControl = new NhanSuAddEditForm();
                    break;
                case "ALLOAIYT":
                    FormControl = new AlloaiytAddEditForm();
                    break;
                case "ACOSXLT_ALLOAIYT":
                    FormControl = new Acosxlt_alloaiytAddEditForm();
                    break;
                case "ACOSXLSX_ALLOAIYT":
                    FormControl = new Acosxlsx_alloaiytAddEditForm();
                    break;
                case "ALCT2":
                    FormControl = new V6Alct2AddEditForm();
                    break;
                case "ALMAUBC":
                    FormControl = new Almaubc();
                    break;
                case "ALMAUBCCT":
                    FormControl = new AlmaubcCt();
                    break;
                case "ALCT3":
                    FormControl = new V6Alct3AddEditForm();
                    break;
                case "ALDM":
                    FormControl = new Aldm();
                    break;
                case "V6VALID":
                    FormControl = new V6valid();
                    break;
                case "V6LOOKUP":
                    FormControl = new V6lookupForm();
                    break;
                case "ABNGHI":
                    FormControl = new AbnghiAddEditForm();
                    break;
                case "HRAPPFAMILY":
                    FormControl = new ThongTinLyLichForm();
                    break;

                case "HRLSTRELATION":
                    FormControl = new ThongTinQuanHeForm();
                    break;
                case "HRLSTRELIGION":
                    FormControl = new ThongTinTonGiaoForm();
                    break;
                case "HRIMAGES":
                    FormControl = new ThongTinHinhAnhChuKy();
                    break;
                case "HRLSTSCHOOL":
                    FormControl = new DanhMucTruongHocForm();
                    break;
                case "PRHLCONG":
                    FormControl = new DanhMucKyHieuCongForm();
                    break;
                case "PRHLNHCONG":
                    FormControl = new DanhMucNhomCongForm();
                    break;
                case "HRXHLNHCA":
                    FormControl = new DanhMucCaLamViecForm();
                    break;
                case "HRXHLCA":
                    FormControl = new DanhMucChiTietCaLamViecForm();
                    break;
                case "PRLICHLE":
                    FormControl = new KhaiBaoLichLe();
                    break;
                case "HRLSTCONTRACTTYPE":
                    FormControl = new ThongTinHopDong();
                    break;
                case "HRXKY":
                    FormControl = new KhaiBaoKyTinhLuong();
                    break;
                case "PRHLTHUETN":
                    FormControl = new DanhMucThueTNCN();
                    break;
                case "PRHLPHUCAP":
                    FormControl = new DanhMucCacKhoanPhuCap();
                    break;
                case "PRHLTTBH":
                    FormControl = new DanhMucCacKhoanTraThayBHXH();
                    break;
                case "PRHLTP":
                    FormControl = new DanhMucThuongPhat();
                    break;
                case "PRLOAILUONG":
                    FormControl = new KhaiBaoLoaiLuong();
                    break;
                case "PRHLLOAITN":
                    FormControl = new DanhMucLoaiThuNhapTinhThue();
                    break;
                case "HRXHLTG":
                    FormControl = new DanhMucThoiGian();
                    break;
                case "HRLSTNATIONAL":
                    FormControl = new ThongTinQuocGiaForm();
                    break;
                case "HRLSTNATIONALITY":
                    FormControl = new ThongTinQuocTichForm();
                    break;
                case "HRLSTPCS":
                    FormControl = new ThongTinTinhThanhForm();
                    break;
                case "HRLSTDEGREE":
                    FormControl = new ThongTinBangCapForm();
                    break;
                case "HRLSTCOURSE":
                    FormControl = new ThongTinChuyenNghanhForm();
                    break;
                case "HRLSTLANGUAGE":
                    FormControl = new ThongTinNgoaiNguForm();
                    break;
                case "HRLSTLANG_LEVEL":
                    FormControl = new ThongTinCapDoNgoaiNguForm();
                    break;
                case "HRLSTLIVINGARR":
                    FormControl = new ThongTinDieuKienSongForm();
                    break;
                case "HRLSTETHNIC":
                    FormControl = new ThongTinDanTocForm();
                    break;
                case "ALREPORT1":
                    FormControl = new ALREPORT1_AddEdit();
                    break;
                case "ALREPORT":
                    FormControl = new ALREPORT_AddEdit();
                    break;
                case "ALNHVITRI":
                    FormControl = new NhomViTriAddEditForm();
                    break;
                case "HRJOBEXPERIENCE":
                    FormControl = new KinhNghiemLamViec();
                    break;
                case "HRJOBEXPERIENCE2":
                    FormControl = new KinhNghiemLamViec2();
                    break;

                default:
                    if (aldm_config.HaveInfo)
                    {
                        if (aldm_config.IS_ALDM)
                        {
                            FormControl = new DynamicAddEditForm(ma_dm, aldm_config);
                            break;
                        }
                    }
                    FormControl = new NoRightAddEdit(V6Text.NoDefine + " AddEditManager.InitControl ma_dm:" + ma_dm);
                    //throw new ArgumentOutOfRangeException("AddEditManager.InitControl ma_dm:" + ma_dm);
                    break;
            }
            if (FormControl == null)
            {
                throw new Exception("Chưa hỗ trợ thêm - sửa!\n" + ma_dm);
            }
            FormControl._aldmConfig = aldm_config;
            FormControl._MA_DM = ma_dm.ToUpper();
            return FormControl;
        }
        #endregion init control
    }
}
