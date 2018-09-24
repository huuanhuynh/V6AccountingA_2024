using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.DonDatHangBan;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HangTraLai;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVu;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonDichVuCoSL;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.DonDatHangMua;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.HoaDonMuaHangDichVu;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapChiPhiMuaHang;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapKhau;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuNhapMua;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuThanhToanTamUng;
using V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuXuatTraLaiNCC;
using V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuChi;
using V6ControlManager.FormManager.ChungTuManager.TienMat.PhieuThu;
using V6ControlManager.FormManager.ChungTuManager.TongHop.PhieuKeToan;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuNhapKho;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    /// <summary>
    /// Gọi hàm tạo một TabContainer nhiều chứng từ cùng loại dựa vào ma_ct.
    /// </summary>
    public static class ChungTu
    {
        public static void ViewMoney(Control view, decimal money, string ma_nt)
        {
            view.Text = V6BusinessHelper.MoneyToWords(money, V6Setting.Language, ma_nt);
        }
        public static V6Control GetChungTuContainer(string maCt, string itemId)
        {
            if(maCt == "SOR")// Báo giá.
                return new BaoGiaContainer(maCt, itemId, true);
            if(maCt == "SOC")
                return new HoaDonCafeContainer(maCt, itemId, false);

            return new ChungTuChungContainer(maCt, itemId, false);
            //switch (maCt)
            //{
            //    #region ==== Phải thu ====
            //    case "SOA":// Hóa đơn bán hàng kiêm phiếu xuất
            //    case "SOH":// Đơn đặt hàng bán
            //    case "SOR":// Báo giá.
            //    case "AR1":// Hóa đơn dịch vụ
            //    case "SOF":// Phiếu nhập hàng bán bị trả lại
            //    #endregion phải thu
            //    #region ==== Tồn kho ====
            //    case "IXA":// Phiếu xuất kho
            //    case "IXB":// Phiếu xuất điều chuyển
            //    case "IND":// Phiếu NHẬP kho
            //    #endregion tồn kho
            //    #region ==== Tiền mặt ====
            //    case "BC1":// Bao co
            //    case "TA1":// Phieu thu
            //    case "BN1":// Bao no
            //    case "CA1":// Phieu chi
            //    #endregion tiền mặt
            //    #region ==== Phải trả ====
            //    case "POA":// PhieuNhapMua
            //    case "POH":// DonDatHangMua
            //    case "POB":// PhieuNhapKhau
            //    case "POC":// PhieuNhapChiPhiMuaHang
            //    case "AP1":// HoaDonMuaHangDichVu
            //    case "AP2":// PhieuThanhToanTamUng
            //    case "IXC":// PhieuXuatTraLaiNCC
            //    #endregion phải trả
            //    #region ==== Tổng hợp ====
            //    // Phiếu kế toán dùng chung
            //    case "GL1":
            //    case "AP9":
            //    case "AR9":
            //    #endregion tổng hợp
            //    default:
            //        return new ChungTuChungContainer(maCt, itemId, false);
            //}
        }

        /// <summary>
        /// Hiển thị dữ liệu dòng đang chọn của GridView lên Detail control.
        /// </summary>
        /// <param name="dataGridView1">GridView lấy dữ liệu</param>
        /// <param name="detail1">Detail control</param>
        /// <param name="outGridViewRow">Lấy ra dòng đang chọn để xử lý sau này.</param>
        /// <returns>Stt_Rec0</returns>
        public static string ViewSelectedDetailToDetailForm(DataGridView dataGridView1, HD_Detail detail1, out DataGridViewRow outGridViewRow)
        {
            outGridViewRow = null;
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    outGridViewRow = dataGridView1.CurrentRow;
                    var values = outGridViewRow.ToDataDictionary();
                    var rec0 = values["STT_REC0"].ToString();
                    detail1.SetData(values);
                    return rec0;
                }
            }
            catch (Exception ex)
            {
                detail1.ShowErrorMessage(ex.Message, "ViewSelectedDetailToDetailForm");
            }
            return "";
        }

        public static void ViewSearchSumary(V6Form searchForm, DataTable am, Label lbl, string mact, string mant)
        {
            try
            {
                string ttt_field;
                string ttt_nt_field;
                switch (mact)
                {
                    //soa,sof,ar1,poa,pob,c,ixc,ca1,bn1,ap1,2, soh,poh t_tt+nt
                    //ind,ixa,ixb,ta1,bc1,gl1,ap9,ar9 t_tien + nt
                    case "SOA":
                        ttt_nt_field = "T_TT_NT";
                        ttt_field = "T_TT";
                        break;
                    case "IND":
                    case "IXA":
                    case "IXB":
                    case "GL1":
                    case "AP9":
                    case "AR9":
                        ttt_nt_field = "T_TIEN_NT";
                        ttt_field = "T_TIEN";
                        break;
                    default:
                        ttt_nt_field = "T_TT_NT";
                        ttt_field = "T_TT";
                        break;
                }

                decimal ttt_nt = 0, ttt = 0;
                if (am.Columns.Contains(ttt_nt_field))
                    ttt_nt = V6BusinessHelper.TinhTong(am, ttt_nt_field);
                if (am.Columns.Contains(ttt_field))
                    ttt = V6BusinessHelper.TinhTong(am, ttt_field);

                if (ttt != 0)
                {
                    //var text_nt = ttt_nt.ToString("### ### ### ###.00");
                    //text_nt = text_nt.Replace(".", "#");
                    //text_nt = text_nt.Replace(" ", ".");
                    //text_nt = text_nt.Replace("#", V6Options.M_NUM_POINT);
                    var text_nt = ObjectAndString.NumberToString(ttt_nt, 2, V6Options.M_NUM_POINT, ".");

                    //var text = ttt.ToString("### ### ### ###.00");
                    //text = text.Replace(".", "#");
                    //text = text.Replace(" ", ".");
                    //text = text.Replace("#", V6Options.M_NUM_POINT);
                    var text = ObjectAndString.NumberToString(ttt, 2, V6Options.M_NUM_POINT, ".");
                    var numWords = V6BusinessHelper.MoneyToWords(ttt, V6Setting.Language, mant);
                    var sumText = V6Setting.IsVietnamese ? "Tổng cộng" : "Total";
                    lbl.Text = String.Format("{0} ({1}) / ({2}) {3}",
                        sumText, text_nt, text, numWords);
                    lbl.Visible = true;
                }
                else
                {
                    lbl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                searchForm.ShowErrorMessage(MethodBase.GetCurrentMethod().DeclaringType + " ViewSum: " + ex.Message);
            }
        }

        public static void SetTxtStatusProperties(V6InvoiceBase invoice, V6ColorTextBox txtTrangThai, V6Label lblStatusDescription)
        {
            try
            {
                var data0 = invoice.AlPost;
                var limitChars = "*";
                var description = V6Setting.IsVietnamese ? "* Tất cả" : "* All";
                var ten_post_field = V6Setting.IsVietnamese ? "ten_post" : "ten_post2";
                foreach (DataRow row in data0.Rows)
                {
                    var kieu_post = row["Kieu_post"].ToString().Trim();
                    limitChars += kieu_post;
                    description += string.Format(", {0} {1}", kieu_post, row[ten_post_field].ToString().Trim());
                }
                txtTrangThai.LimitCharacters = limitChars;
                lblStatusDescription.Text = description;
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog("ChungTu.SetTxtStatusProperties", ex);
            }
        }
    }

    /// <summary>
    /// Gọi một control chứng từ lẻ để hiện thông tin 1 chứng từ chuẩn bị cho việc sửa.
    /// </summary>
    public static class ChungTuF3
    {
        public static V6InvoiceControl GetChungTuControl(string maCt, string itemId, string sttRec)
        {
            switch (maCt)
            {
                #region ==== Phải thu ====
                case "SOA":
                    return new HoaDonControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOB":
                    return new HoaDonDichVuCoSLControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOH":
                    return new DonDatHangBanControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOR":
                    return new BaoGiaControl(maCt, itemId, sttRec) { Name = itemId };
                case "AR1":
                    return new HoaDonDichVuControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOF":
                    return new HangTraLaiControl(maCt, itemId, sttRec) { Name = itemId };
                #endregion phải thu

                #region ==== Tồn kho ====
                case "IXA":
                    return new PhieuXuatKhoControl(maCt, itemId, sttRec) { Name = itemId };
                case "IXB":
                    return new PhieuXuatDieuChuyenControl(maCt, itemId, sttRec) { Name = itemId };
                case "IND":
                    return new PhieuNhapKhoControl(maCt, itemId, sttRec) { Name = itemId };
                #endregion tồn kho

                #region ==== Tiền mặt ====
                case "BC1":
                    //return new BaoCoControl((maCt, itemId, sttRec) { Name = itemId };
                case "TA1":
                    return new PhieuThuControl(maCt, itemId, sttRec) { Name = itemId };
                case "BN1":
                    //return new BaoNoControl((maCt, itemId, sttRec) { Name = itemId };
                case "CA1":
                    return new PhieuChiControl(maCt, itemId, sttRec) { Name = itemId };
                
                
                #endregion

                #region ==== Phải trả ====
                case "POA":
                    return new PhieuNhapMuaControl(maCt, itemId, sttRec) { Name = itemId };
                case "POH":
                    return new DonDatHangMuaControl(maCt, itemId, sttRec) { Name = itemId };
                case "POB":
                    return new PhieuNhapKhauControl(maCt, itemId, sttRec) { Name = itemId };
                case "POC":
                    return new PhieuNhapChiPhiMuaHangControl(maCt, itemId, sttRec) { Name = itemId };
                case "IXC":
                    return new PhieuXuatTraLaiNCCControl(maCt, itemId, sttRec) { Name = itemId };
                case "AP1":
                    return new HoaDonMuaHangDichVuControl(maCt, itemId, sttRec) { Name = itemId };
                case "AP2":// Phiếu thanh toán tạm ứng
                    return new PhieuThanhToanTamUngControl(maCt, itemId, sttRec) { Name = itemId };
                #endregion phải trả

                #region ==== Tổng hợp ====
                // Phiếu kế toán dùng chung
                case "GL1":
                case "AP9":
                case "AR9":
                    return new PhieuKeToanControl(maCt, itemId, sttRec) { Name = itemId };
                #endregion phiếu kế toán

                default:
                    return new V6InvoiceControl(maCt);
            }
            
        }
    }
}
