using System;
using System.Windows.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Controls.Forms.DanhMuc.Add_Edit.NhanSu;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public static class AddEditManager
    {
        #region ==== Init Control ====
        public static AddEditControlVirtual Init_Control(V6TableName tableName)
        {
            AddEditControlVirtual FormControl = null;
            //if (V6Check_Rights())
            switch (tableName)
            {
                case V6TableName.Albp:
                    FormControl = new BoPhanAddEditForm();
                    break;
                case V6TableName.Albpcc:
                    FormControl = new BoPhanSuDungCongCuAddEditForm();
                    break;
                case V6TableName.Albpht:
                    FormControl = new BoPhanHachToanAddEditForm();
                    break;
                case V6TableName.Albpts:
                    FormControl = new BoPhanSuDungTangSuatAddEditForm();
                    break;
                case V6TableName.Alcc:
                    break;
                case V6TableName.Alck:
                    FormControl = new ChietKhauAddEditForm();
                    break;
                case V6TableName.Alck2:
                    break;
                case V6TableName.Alckm:
                    break;
                case V6TableName.Alckmct:
                    break;
                case V6TableName.Alcltg:
                    break;
                case V6TableName.Alct:
                    FormControl = new AlctAddEditFrom();

                    break;
                case V6TableName.Alctct:
                    break;
                case V6TableName.Alcthd:
                    break;
                case V6TableName.Aldmpbct:
                    break;
                case V6TableName.Aldmpbph:
                    break;
                case V6TableName.Aldmvt:
                    break;
                case V6TableName.Acosxlt_aldmvt:
                    break;
                case V6TableName.Acosxlsx_aldmvt:
                    break;
                case V6TableName.Aldmvtct:
                    break;
                case V6TableName.Aldvcs:
                    FormControl = new DonViCoSoAddEditForm();
                    break;
                case V6TableName.Aldvt:
                    FormControl = new DonViTinhAddEditForm();
                    break;
                case V6TableName.Algia:
                    break;
                case V6TableName.Algia2:
                    FormControl = new Algia2AddEditForm();
                    break;
                case V6TableName.Algia200:
                    break;
                case V6TableName.Algiavon:
                    break;
                case V6TableName.Algiavon3:
                    break;
                case V6TableName.Algiavv:
                    break;
                case V6TableName.Alhd:
                    FormControl = new HopDongAddEditForm();
                    break;
                case V6TableName.Alhttt:
                    FormControl = new HinhThucThanhToanAddEditForm();
                    break;
                case V6TableName.Alhtvc:
                    FormControl = new HinhThucVanChuyenAddEditForm();
                    break;
                case V6TableName.Alkc:
                    FormControl = new AlkcAddEditForm();
                    break;
                case V6TableName.Alkh:
                    FormControl = new KhachHangAddEditFrom();
                    break;
                case V6TableName.Alkhct:
                    FormControl = new KhachHangChiTietFrom();
                    break;
                case V6TableName.Alkho:
                    FormControl = new KhoHangAddEditForm();
                    break;
                case V6TableName.Alkhtg:
                    break;
                case V6TableName.Alkmb:
                    break;
                case V6TableName.Alkmbct:
                    break;
                case V6TableName.Alkmm:
                    break;
                case V6TableName.Alkmmct:
                    break;
                case V6TableName.Alku:
                    FormControl = new KheUocAddEditForm();
                    break;
                case V6TableName.Allnx:
                    FormControl = new LoaiNhapXuatAddEditForm();
                    break;
                case V6TableName.Allo:
                    FormControl = new LoHangAddEditForm();
                    break;
                case V6TableName.Alloaicc:
                    break;
                case V6TableName.Alloaick:
                    break;
                case V6TableName.Alloaivc:
                    FormControl = new LoaiDichVuAddEditForm();
                    break;
                case V6TableName.Alloaivt:
                    break;
                case V6TableName.Almagd:
                    break;
                case V6TableName.Almagia:
                    FormControl = new MaGiaAddEditForm();
                    break;
                case V6TableName.Almauhd:
                    FormControl = new MauHoaDonAddEditForm();
                    break;
                case V6TableName.Alnhcc:
                    FormControl = new PhanNhomCongCuAddEditForm();
                    break;
                case V6TableName.Alnhdvcs:
                    break;
                case V6TableName.Alnhhd:
                    FormControl = new NhomHopDongAddEditForm();
                    break;
                case V6TableName.Alnhkh:
                    FormControl = new NhomKhachHangAddEditForm();
                    break;
                case V6TableName.Alnhkh2:
                    FormControl = new NhomGiaKhachHangAddEditForm();
                    break;
                case V6TableName.Alnhku:
                    FormControl = new NhomKheUocAddEditForm();
                    break;
                case V6TableName.Alnhphi:
                    FormControl = new NhomKhoanMucPhiAddEditForm();
                    break;
                case V6TableName.Alnhtk:
                    FormControl = new NhomTieuKhoanAddEditForm();
                    break;
                case V6TableName.Alnhtk0:
                    FormControl = new NhomTaiKhoanAddEditForm();
                    break;
                case V6TableName.Alnhts:
                    FormControl = new PhanNhomTaiSanAddEditForm();
                    break;
                case V6TableName.Alnhvt:
                    FormControl = new NhomVatTuAddEditForm();
                    break;
                case V6TableName.Alnhvt2:
                    FormControl = new NhomGiaVatTuAddEditForm();
                    break;
                case V6TableName.Alnhvv:
                    FormControl = new NhomVuViecAddEditForm();
                    break;
                case V6TableName.Alnhytcp:
                    FormControl = new NhomYeuToChiPhiAddEditForm();
                    break;
                case V6TableName.Acosxlt_alnhytcp:
                    FormControl = new NhomYeuToChiPhiSXLTAddEditForm();
                    break;
                case V6TableName.Acosxlsx_alnhytcp:
                    FormControl = new NhomYeuToChiPhiSXDHAddEditForm();
                    break;
                case V6TableName.Alnk:
                    break;
                case V6TableName.Alnt:
                    FormControl = new NgoaiTeAddEditForm();
                    break;
                case V6TableName.Alnv:
                    FormControl = new NguonVonAddEditForm();
                    break;
                case V6TableName.Alnvien:
                    FormControl = new NhanVienAddEditForm();
                    break;
                case V6TableName.Alpb:
                    break;
                case V6TableName.Alpb1:
                    break;
                case V6TableName.Alphi:
                    FormControl = new PhiAddEditForm();
                    break;
                case V6TableName.Alphuong:
                    FormControl = new PhuongXaAddEditForm();
                    break;
                case V6TableName.Alplcc:
                    FormControl = new PhanLoaiCongCuAddEditForm();
                    break;
                case V6TableName.Alplts:
                    FormControl = new PhanLoaiTaiSanAddEditForm();
                    break;
                case V6TableName.Alqddvt:
                    FormControl = new QuyDoiDonViTinhAddEditForm();
                    break;
                case V6TableName.Alqg:
                    FormControl = new QuocGiaAddEditForm();
                    break;
                case V6TableName.Alql:
                    break;
                case V6TableName.Alquan:
                    FormControl = new QuanHuyenAddEditForm();
                    break;
                case V6TableName.Alstt:
                    FormControl = new NamTaiChinhAddEditForm();
                    break;
                case V6TableName.Altd:
                    FormControl = new TuDienNguoiDungDinhNghiaAddEditForm();
                    break;
                case V6TableName.Altd2:
                    FormControl = new TuDienTuDinhNghia2AddEditForm();
                    break;
                case V6TableName.Altd3:
                    FormControl = new TuDienTuDinhNghia3AddEditForm();
                    break;
                case V6TableName.Altgcc:
                    FormControl = new LyDoTangGiamCCDCAddEditForm();
                    break;
                case V6TableName.Altgnt:
                    FormControl = new TyGiaNgoaiTeAddEditForm();
                    break;
                case V6TableName.Altgts:
                    FormControl = new LyDoTangGiamTSCDAddEditForm();
                    break;
                case V6TableName.Althau:
                    break;
                case V6TableName.Althauct:
                    break;
                case V6TableName.Althue:
                    FormControl = new ThueSuatAddEditForm();
                    break;
                case V6TableName.Altinh:
                    FormControl = new TinhThanhAddEditForm();
                    break;
                case V6TableName.Altk0:
                    FormControl = new TaiKhoanAddEditForm();
                    break;
                case V6TableName.Altk1:
                    break;
                case V6TableName.Altk2:
                    FormControl = new TieuKhoanAddEditForm();
                    break;
                case V6TableName.Altklkku:
                    break;
                case V6TableName.Altklkvv:
                    break;
                case V6TableName.Altknh:
                    FormControl = new TaiKhoanNganHangAddEditForm();
                    break;
                case V6TableName.Alts:
                    break;
                case V6TableName.Altt:
                    break;
                case V6TableName.Alttvt:
                    FormControl = new TinhTrangDichVuAddEditForm();
                    break;
                case V6TableName.Alvc:
                    FormControl = new VanChuyenAddEditForm();
                    break;
                case V6TableName.Alvitri:
                    FormControl = new ViTriAddEditForm();
                    break;
                case V6TableName.Alvt:
                    FormControl = new VatTuAddEditForm();
                    break;
                case V6TableName.Alvttg:
                    FormControl = new SanPhamTrungGianAddEditForm();
                    break;
                case V6TableName.Alvv:
                    FormControl = new VuViecAddEditForm();
                    break;
                case V6TableName.Alytcp:
                    FormControl = new YeuToChiPhiAddEditForm();
                    break;
                case V6TableName.Acosxlsx_alytcp:
                    FormControl = new YeuToChiPhiSPDHAddEditForm();
                    break;
                case V6TableName.Acosxlt_alytcp:
                    FormControl = new YeuToChiPhiSXLTAddEditForm();
                    break;
                case V6TableName.V6option:
                    FormControl = new V6OptionAddEditForm();
                    break;
                case V6TableName.V6soft:
                    FormControl = new V6SoftAddEditForm();
                    break;

                case V6TableName.V6user:
                    FormControl = new NguoiSuDungAddEditForm();
                    break;
                case V6TableName.Alct1:
                    FormControl = new V6Alct1AddEditForm();
                    break;
                case V6TableName.V6menu:
                    FormControl = new V6MenuAddEditForm();
                    break;
                case V6TableName.Althue30:
                    FormControl = new ThueSuat30AddEditForm();
                    break;
                case V6TableName.Alsonb:
                    FormControl = new AlsonbAddEditForm();
                    break;
                case V6TableName.Notable:
                    break;
                case V6TableName.Albc:
                    if (V6Login.IsAdmin) FormControl = new AlbcAddEditForm();
                    else
                    {
                        FormControl = new NoRightAddEdit(V6Text.NotAnAdmin);
                        FormControl.Dock = DockStyle.Fill;
                    }
                    break;
                case V6TableName.V_alts:
                    FormControl = new ValtsAddEditForm();
                    break;
                case V6TableName.V_alcc:
                    FormControl = new ValccAddEditForm();
                    break;
                case V6TableName.V_alts01:
                    FormControl = new Valts01AddEditForm();
                    break;
                case V6TableName.V_alcc01:
                    FormControl = new Valcc01AddEditForm();
                    break;
                case V6TableName.CorpLan:
                case V6TableName.CorpLan1:
                case V6TableName.CorpLan2:
                    FormControl = new CorpLanAddEditForm();
                    break;
                //case V6TableName.Hlns:
                case V6TableName.Hrpersonal:
                    FormControl = new NhanSuAddEditForm();
                    break;
                case V6TableName.Alloaiyt:
                    FormControl = new AlloaiytAddEditForm();
                    break;
                case V6TableName.Acosxlt_alloaiyt:
                    FormControl = new Acosxlt_alloaiytAddEditForm();
                    break;
                case V6TableName.Acosxlsx_alloaiyt:
                    FormControl = new Acosxlsx_alloaiytAddEditForm();
                    break;
                case V6TableName.Alct2:
                    FormControl = new V6Alct2AddEditForm();
                    break;
                case V6TableName.Almaubc:
                    FormControl = new Almaubc();
                    break;
                case V6TableName.Almaubcct:
                    FormControl = new AlmaubcCt();
                    break;
                case V6TableName.Alct3:
                    FormControl = new V6Alct3AddEditForm();
                    break;
                case V6TableName.Aldm:
                    FormControl = new Aldm();
                    break;
                case V6TableName.V6valid:
                    FormControl = new V6valid();
                    break;
                case V6TableName.V6lookup:
                    FormControl = new V6lookupForm();
                    break;
                case V6TableName.Abnghi:
                    FormControl = new AbnghiAddEditForm();
                    break;
                case V6TableName.Hrappfamily:
                    FormControl = new ThongTinLyLichForm();
                    break;
                case V6TableName.Hrlstrelation:
                    FormControl = new ThongTinQuanHeForm();
                    break;
                case V6TableName.Hrlstreligion:
                    FormControl = new ThongTinTonGiaoForm();
                    break;
                case V6TableName.Hrimages:
                    FormControl = new ThongTinHinhAnhChuKy();
                    break;

               case V6TableName.Hrlstschool:
                   FormControl = new DanhMucTruongHocForm();
                    break;

               case V6TableName.Prhlcong:
                    FormControl = new DanhMucKyHieuCongForm();
                    break;
               case V6TableName.Prhlnhcong:
                    FormControl = new DanhMucNhomCongForm();
                    break;
                case V6TableName.Hrxhlnhca:
                    FormControl = new DanhMucCaLamViecForm();
                    break;
                case V6TableName.Hrxhlca:
                    FormControl = new DanhMucChiTietCaLamViecForm();
                    break;
                case V6TableName.PRLICHLE:
                    FormControl = new KhaiBaoNgayNghiLe();
                    break;
                case V6TableName.Hrxky:
                    FormControl = new KhaiBaoKiTinhLuong();
                    break;
                case V6TableName.Prhlthuetn:
                    FormControl = new DanhMucThueTNCN();
                    break;
                case V6TableName.prhlphucap:
                    FormControl = new DanhMucCacKhoanPhuCap();
                    break;
                case V6TableName.Prhlttbh:
                    FormControl = new DanhMucCacKhoanTraThayBHXH();
                    break;
                case V6TableName.Prhltp:
                    FormControl = new DanhMucThuongPhat();
                    break;
                case V6TableName.Prloailuong:
                    FormControl = new KhaiBaoLoaiLuong();
                    break;
                case V6TableName.Prhlloaitn:
                    FormControl = new DanhMucLoaiThuNhapTinhThue();
                    break;
                case V6TableName.Hrxhltg:
                    FormControl = new DanhMucThoiGian();
                    break;
                case V6TableName.Hrlstnational:
                    FormControl = new ThongTinQuocGiaForm();
                    break;
                case V6TableName.Hrlstnationality:
                    FormControl = new ThongTinQuocTichForm();
                    break;
                case V6TableName.Hrlstpcs:
                    FormControl = new ThongTinTinhThanhForm();
                    break;
                case V6TableName.Hrlstdegree:
                    FormControl = new ThongTinBangCapForm();
                    break;
                case V6TableName.Hrlstcourse:
                    FormControl = new ThongTinChuyenNghanhForm();
                    break;
                case V6TableName.hrlstlanguage:
                    FormControl = new ThongTinNgoaiNguForm();
                    break;
                case V6TableName.Hrlstlang_level:
                    FormControl = new ThongTinCapDoNgoaiNguForm();
                    break;
                case V6TableName.HRLSTLIVINGARR:
                    FormControl = new ThongTinDieuKienSongForm();
                    break;
                case V6TableName.Hrlstethnic:
                    FormControl = new ThongTinDanTocForm();
                    break;
                case V6TableName.Alreport1:
                    FormControl = new ALREPORT1();
                    break;
                case V6TableName.Alreport:
                    FormControl = new ALREPORT();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("AddEditManager.InitControl tableName");
            }
            if (FormControl == null)
            {
                throw new Exception("Chưa hỗ trợ thêm - sửa!\n" + tableName);
            }
            FormControl.TableName = tableName;
            return FormControl;
        }
        #endregion init control
    }
}
