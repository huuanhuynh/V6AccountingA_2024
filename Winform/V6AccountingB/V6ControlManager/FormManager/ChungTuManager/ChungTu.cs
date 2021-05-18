using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Controls;
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
using V6ControlManager.FormManager.ChungTuManager.TonKho.DeNghiNhapKhoINY;
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
using V6ControlManager.FormManager.ChungTuManager.TonKho.DeNghiXuatKhoIXY;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDiDuongINT;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuDuyetXuatBanIXP;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuNhapKho;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatDieuChuyen;
using V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Control = System.Windows.Forms.Control;
using Label = System.Windows.Forms.Label;

namespace V6ControlManager.FormManager.ChungTuManager
{
    /// <summary>
    /// Gọi hàm tạo một TabContainer nhiều chứng từ cùng loại dựa vào ma_ct.
    /// </summary>
    public static class ChungTu
    {
        public static void ViewMoney(Control view, decimal money, string ma_nt)
        {
            try
            {
                view.Text = V6BusinessHelper.MoneyToWords(money, V6Setting.Language, ma_nt);
            }
            catch (Exception)
            {
                //
            }
            
        }
        
        public static V6Control GetChungTuContainer(string maCt, string itemId) // ALCT.TYPE_VIEW 2 = showQuickView
        {
            if(maCt == "SOR")// Báo giá.
                return new BaoGiaContainer(maCt, itemId, true);
            if(maCt == "SOC")
                return new HoaDonCafeContainer(maCt, itemId, false);

            return new ChungTuChungContainer(maCt, itemId);
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
        /// Hiển thị dữ liệu dòng đang chọn của GridView lên Detail control, gán stt_rec0.
        /// </summary>
        /// <param name="dataGridView1">GridView lấy dữ liệu</param>
        /// <param name="detail1">Detail control</param>
        /// <param name="outGridViewRow">Lấy ra dòng đang chọn để xử lý sau này.</param>
        /// <param name="stt_rec0">Gán stt_rec dòng hiện tại.</param>
        public static void ViewSelectedDetailToDetailForm(DataGridView dataGridView1, HD_Detail detail1, out DataGridViewRow outGridViewRow, out string stt_rec0)
        {
            outGridViewRow = null;
            stt_rec0 = "";
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    outGridViewRow = dataGridView1.CurrentRow;
                    var values = outGridViewRow.ToDataDictionary();
                    var rec0 = values["STT_REC0"].ToString();
                    detail1.SetData(values);
                    stt_rec0 = rec0;
                }
            }
            catch (Exception ex)
            {
                detail1.ShowErrorMessage(ex.Message, "ViewSelectedDetailToDetailForm");
            }
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
                    case "AR19":
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
                    lbl.Text = string.Format("{0} ({1}) / ({2}) {3}", sumText, text_nt, text, numWords);
                    lbl.Visible = true;
                }
                else
                {
                    lbl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".ViewSum", ex);
            }
        }

        public static void SetTxtStatusProperties(V6InvoiceBase invoice, V6ColorTextBox txtTrangThai, V6Label lblStatusDescription)
        {
            try
            {
                var data0 = invoice.AlPost;
                var limitChars = "*";
                var description = "* " + V6Text.Text("ALL");
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alct1"></param>
        /// <param name="dataGridView1"></param>
        /// <param name="_orderList"></param>
        /// <param name="carryFields">Các trường giữ giá trị dùng lại khi thêm mới.</param>
        /// <param name="_alct1Dic"></param>
        public static void ApplyAlct1toGridView(DataTable alct1, V6ColorDataGridView dataGridView1,
            out List<string> _orderList, out List<string> carryFields, out SortedDictionary<string, DataRow> _alct1Dic)
        {
            var result = new Dictionary<string, AlctColumns>();

            //var alct1 = Invoice.Alct1;
            _orderList = new List<string>();
            carryFields = new List<string>();
            _alct1Dic = new SortedDictionary<string, DataRow>();

            //Control temp_control = new Control();
            foreach (DataRow row in alct1.Rows)
            {
                var read_only = 1 == ObjectAndString.ObjectToInt(row["visible"]);
                //if (!visible) continue;
                Config config = new Config(row.ToDataDictionary());
                var filter_m = config.GetString("FILTER_M");
                var FCOLUMN = config.GetString("fcolumn").ToUpper();
                _orderList.Add(FCOLUMN);
                _alct1Dic.Add(FCOLUMN, row);

                var fcaption = row[V6Setting.Language == "V" ? "caption" : "caption2"].ToString().Trim();
                var limits = row["limits"].ToString().Trim();
                var fvvar = row["fvvar"].ToString().Trim();
                var fstatus = Convert.ToBoolean(row["fstatus"]);

                var width = ObjectAndString.ObjectToInt(row["width"]);
                var ftype = row["ftype"].ToString().Trim();
                var fOrder = ObjectAndString.ObjectToInt(row["forder"]);
                var carry = ObjectAndString.ObjectToInt(row["carry"]) == 1;

                int decimals = 0;

                //Control c = temp_control;
                DataGridViewColumn c_column = dataGridView1.Columns[FCOLUMN];
                switch (ftype)
                {
                    #region Create controls
                    //case "A0":
                    //    if (FCOLUMN == "TANG")
                    //    {
                    //        c = CreateCheckTextBox(FCOLUMN, "a", fcaption, limits, width, fstatus, carry);
                    //    }
                    //    else if (FCOLUMN == "PX_GIA_DDI")
                    //    {
                    //        c = CreateCheckTextBox(FCOLUMN, "a", fcaption, limits, width, fstatus, carry);
                    //    }
                    //    else if (FCOLUMN == "PN_GIA_TBI")
                    //    {
                    //        c = CreateCheckTextBox(FCOLUMN, "a", fcaption, limits, width, fstatus, carry);
                    //    }
                    //    break;
                    //case "A1":
                    //    c = CreateCheckTextBox(FCOLUMN, "a", fcaption, limits, width, fstatus, carry);
                    //    break;
                    case "C0":
                        if (fvvar != "")
                        {
                            var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                            var notempty = Convert.ToBoolean(row["notempty"]);
                            //c = V6ControlFormHelper.CreateVvarTextBox(FCOLUMN, fvvar, fcaption, limits, width, fstatus, checkvvar, notempty, carry);

                            var column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6VvarDataGridViewColumn), "C" + fvvar) as V6VvarDataGridViewColumn;
                            if (column != null)
                            {
                                column.HeaderText = fcaption;
                                column.Vvar = fvvar;
                                column.CheckOnLeave = checkvvar;
                                column.CheckNotEmpty = notempty;
                                column.LimitCharter = limits;
                                column.Width = width;
                                column.Visible = fstatus;
                            }
                        }
                        else
                        {
                            //c = CreateColorTextBox(FCOLUMN, fcaption, limits, width, fstatus, carry);
                            var column = dataGridView1.Columns[FCOLUMN];
                            if (column != null)
                            {
                                column.HeaderText = fcaption;
                                //column.Carry = carry;
                                column.Width = width;
                                column.Visible = fstatus;
                            }
                        }
                        break;
                    //case "C1":  // LookupTextBox
                    //    if (fvvar != "")
                    //    {
                    //        var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                    //        var notempty = Convert.ToBoolean(row["notempty"]);
                    //        string ma_dm = row["MA_DM"].ToString().Trim();
                    //        string[] ss = ObjectAndString.SplitStringBy(fvvar, ':');
                    //        string value_field = ss[0];
                    //        string text_field = ss[1];
                    //        string bfields = ss[2];
                    //        string nfields = ss[3];
                    //        c = CreateLookupTextBox(FCOLUMN, ma_dm, value_field, text_field, bfields, nfields, fcaption, limits, width, fstatus, checkvvar, notempty, carry);
                    //    }
                    //    else
                    //    {
                    //        c = CreateColorTextBox(FCOLUMN, fcaption, limits, width, fstatus, carry);
                    //    }
                    //    break;
                    //case "C2":  // LookupProc
                    //    if (fvvar != "")
                    //    {
                    //        var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                    //        var notempty = Convert.ToBoolean(row["notempty"]);
                    //        string ma_dm = row["MA_DM"].ToString().Trim();
                    //        string[] ss = ObjectAndString.SplitStringBy(fvvar, ':');
                    //        string value_field = ss[0];
                    //        string text_field = ss[1];
                    //        string bfields = ss[2];
                    //        string nfields = ss[3];
                    //        c = CreateLookupProcTextBox(FCOLUMN, ma_dm, value_field, text_field, bfields, nfields, fcaption, limits, width, fstatus, checkvvar, notempty, carry);
                    //    }
                    //    else
                    //    {
                    //        c = CreateColorTextBox(FCOLUMN, fcaption, limits, width, fstatus, carry);
                    //    }
                    //    break;
                    //case "C3":  // LookupData
                    //    if (fvvar != "")
                    //    {
                    //        var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                    //        var notempty = Convert.ToBoolean(row["notempty"]);
                    //        c = CreateLookupDataTextBox(FCOLUMN, fvvar, fcaption, limits, width, fstatus, checkvvar, notempty, carry);
                    //    }
                    //    else
                    //    {
                    //        c = CreateColorTextBox(FCOLUMN, fcaption, limits, width, fstatus, carry);
                    //    }
                    //    break;
                    case "N9"://Kieu so bat ky
                    case "N0"://Tien
                    case "N1"://Ngoai te
                    case "N2"://so luong
                    case "N3"://GIA
                    case "N4"://Gia nt
                    case "N5"://Ty gia
                        
                        if (ftype == "N9")
                        {
                            decimals = row["fdecimal"] == null ? V6Setting.DecimalsNumber : ObjectAndString.ObjectToInt(row["fdecimal"]);
                        }
                        else if (ftype == "N0")
                        {
                            decimals = V6Options.M_IP_TIEN;
                        }
                        else if (ftype == "N1")
                        {
                            decimals = V6Options.M_IP_TIEN_NT;
                        }
                        else if (ftype == "N2")
                        {
                            decimals = V6Options.M_IP_SL;
                        }
                        else if (ftype == "N3")
                        {
                            decimals = V6Options.M_IP_GIA;
                        }
                        else if (ftype == "N4")
                        {
                            decimals = V6Options.M_IP_GIA_NT;
                        }
                        else if (ftype == "N5")
                        {
                            decimals = V6Options.M_IP_TY_GIA;
                        }
                        
                        //c = CreateNumberTextBox(FCOLUMN, fcaption, decimals, limits, width, fstatus, carry);
                        var num_column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6NumberDataGridViewColumn), "N" + decimals) as V6NumberDataGridViewColumn;
                        if (num_column != null)
                        {
                            num_column.HeaderText = fcaption;
                            num_column.DefaultCellStyle.Format = "N" + decimals;
                            //num_column.CheckOnLeave = checkvvar;
                            //num_column.CheckNotEmpty = notempty;
                            //num_column.LimitCharter = limits;
                            num_column.Width = width;
                            num_column.Visible = fstatus;
                        }
                        break;
                    
                    
                    
                    case "D0": // Allow null
                        //c = CreateDateTimeColor(FCOLUMN, fcaption, width, fstatus, carry);
                        var datecolor_column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6DateTimeColorGridViewColumn), null) as V6DateTimeColorGridViewColumn;
                        if (datecolor_column != null)
                        {
                            datecolor_column.HeaderText = fcaption;
                            datecolor_column.Width = width;
                            datecolor_column.Visible = fstatus;
                        }
                        break;
                    case "D1": // Not null
                        //c = CreateDateTimePicker(FCOLUMN, fcaption, width, fstatus, carry);
                        var date_column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6DateTimePickerGridViewColumn), null) as V6DateTimePickerGridViewColumn;
                        if (date_column != null)
                        {
                            date_column.HeaderText = fcaption;
                            date_column.Width = width;
                            date_column.Visible = fstatus;
                        }
                        break;
                    case "D2": // Not null + time
                        //c = CreateDateTimeFullPicker(FCOLUMN, fcaption, width, fstatus, carry);
                        var datetime_column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6DateTimePickerFullGridViewColumn), null) as V6DateTimePickerFullGridViewColumn;
                        if (datetime_column != null)
                        {
                            datetime_column.HeaderText = fcaption;
                            datetime_column.Width = width;
                            datetime_column.Visible = fstatus;
                        }
                        break;
                    case "D3": // null + time
                        //c = V6ControlFormHelper. CreateDateTimeFullPickerNull(FCOLUMN, fcaption, width, fstatus, carry);
                        var datetemp_column = dataGridView1.ChangeColumnType(FCOLUMN, typeof(V6DateTimeColorGridViewColumn), null) as V6DateTimeColorGridViewColumn;
                        if (datetemp_column != null)
                        {
                            datetemp_column.HeaderText = fcaption;
                            datetemp_column.Width = width;
                            datetemp_column.Visible = fstatus;
                        }
                        break;
                        break;
                    #endregion
                }

                if (read_only)
                {
                    c_column.ReadOnly = true;
                }

                if (c_column != null)
                {
                    //LookupButton lButton = null;
                    //if (!string.IsNullOrEmpty(filter_m))
                    //{
                    //    DefineInfo defineInfo_M = new DefineInfo(filter_m);
                    //    lButton = new LookupButton();
                    //    lButton.ReferenceControl = c;

                    //    lButton.Name = "lbt" + FCOLUMN;

                    //    lButton.R_DataType = defineInfo_M.R_DataType;
                    //    //lButton.R_Value = defineInfo_M.R_Value;
                    //    //lButton.R_Vvar = defineInfo_M.R_Vvar;
                    //    //lButton.R_Stt_rec = defineInfo_M.R_Stt_rec;
                    //    lButton.R_Ma_ct = defineInfo_M.R_Ma_ct;

                    //    lButton.M_DataType = defineInfo_M.M_DataType;
                    //    lButton.M_Value = defineInfo_M.M_Value;
                    //    lButton.M_Vvar = defineInfo_M.M_Vvar;
                    //    lButton.M_Stt_Rec = defineInfo_M.M_Stt_Rec;
                    //    lButton.M_Ma_ct = defineInfo_M.M_Ma_ct;

                    //    lButton.M_Type = defineInfo_M.M_Type;
                    //    //lButton.M_User_id = defineInfo_M.M_User_id;
                    //    //lButton.M_Lan = defineInfo_M.V6Login.SelectedLanguage;

                    //    lButton.Visible = defineInfo_M.Visible;
                    //}

                    result.Add(FCOLUMN, new AlctColumns() { DetailControl = c_column, LookupButton = null, LabelText = fcaption, IsCarry = carry, FOrder = fOrder, IsVisible = fstatus });
                    if (carry)
                    {
                        carryFields.Add(FCOLUMN);
                    }
                }
            }
            //orderList = _orderList;
            //carryFields = carryFields;
            //alct1Dic = _alct1Dic;
            string[] orderList = new string[5];
            V6ControlFormHelper.ReorderDataGridViewColumns(dataGridView1, orderList);
        }
    }

    /// <summary>
    /// Gọi một control chứng từ lẻ để hiện thông tin 1 chứng từ chuẩn bị cho việc sửa.
    /// </summary>
    public static class ChungTuF3
    {
        public static V6InvoiceControl GetChungTuControl(string maCt, string itemId, string sttRec)
        {
            AlctConfig alctConfig = ConfigManager.GetAlctConfig(maCt);
            string formCode = alctConfig.FORMCODE;
            if (formCode != null) formCode = formCode.ToUpper();

            switch (maCt)
            {
                #region ==== Phải thu ====
                case "SOA":
                    return new HoaDonControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOB":
                    return new HoaDonDichVuCoSLControl(maCt, itemId, sttRec) { Name = itemId };
                case "SOC":
                    return new HoaDonCafeControl(maCt, itemId, sttRec); 
                case "SOH":
                    if (formCode == "DONDATHANGBANCONTROL_A1")
                        return new DonDatHangBanControl_A1(maCt, itemId, sttRec) { Name = itemId };
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
                    return new PhieuXuatKhoControl(new V6Invoice84(), itemId, sttRec) { Name = itemId };
                case "IXB":
                    return new PhieuXuatDieuChuyenControl(maCt, itemId, sttRec) { Name = itemId };
                case "IND":
                    return new PhieuNhapKhoControl(maCt, itemId, sttRec) { Name = itemId };
                case "INY":
                    return new DeNghiNhapKhoINY_Control(maCt, itemId, sttRec) { Name = itemId };
                case "INT":
                    return new PhieuDiDuongINT_Control(maCt, itemId, sttRec) { Name = itemId };
                case "IXY":
                    return new DeNghiXuatKhoIXYControl(maCt, itemId, sttRec) { Name = itemId };
                case "IXP":
                    return new PhieuDuyetXuatBanIXPControl(maCt, itemId, sttRec) { Name = itemId };

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
