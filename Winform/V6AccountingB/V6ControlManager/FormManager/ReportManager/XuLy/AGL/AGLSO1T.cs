﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGLSO1T : XuLyBase
    {
        public AGLSO1T(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = MadeStatus2();
            }
            V6ControlFormHelper.SetStatusText2(text, id);
        }

        private string MadeStatus2()
        {
            string status = "";
            if (FilterControl == null) return string.Format("F6: {0}, F7: {1}, F9: {2}, F10: {3}",
                V6Text.Text("XUATEXCEL_NHIEUFILE"), V6Text.Text("XUATEXCEL_NHIEUSHEET"), V6Text.Text("INTUNGTRANG"), V6Text.Text("INLIENTUC"));
            if (FilterControl.F6) status += ", F6: " + V6Text.Text("XUATEXCEL_NHIEUFILE");
            if (FilterControl.F6) status += ", F7: " + V6Text.Text("XUATEXCEL_NHIEUSHEET");
            if (FilterControl.F6) status += ", F9: " + V6Text.Text("INTUNGTRANG");
            if (FilterControl.F6) status += ", F10: " + V6Text.Text("INLIENTUC");
            if (status.Length > 2) status = status.Substring(2);
            return status;
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        protected override void XuLyXemChiTietF5()
        {
            //var _reportFileF5 = "AGLSO1TF5";
            var _reportTitleF5 = "SỔ CÁI TÀI KHOẢN";
            var _reportTitle2F5 = "Account detail";
            bool shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
            var oldKeys = FilterControl.GetFilterParameters();
            if (MenuButton.UseXtraReport != shift_is_down)
            {
                var view = new ReportR_DX(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                    _reportTitleF5, _reportTitle2F5, "", "", "");
                view.CodeForm = CodeForm;
                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = oldKeys;
                view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
                view.PrintMode = V6PrintMode.AutoLoadData;
                view.ShowToForm(this, _reportCaption, true);
            }
            else
            {
                var view = new ReportRViewBase(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                    _reportTitleF5, _reportTitle2F5, "", "", "");
                view.CodeForm = CodeForm;
                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = oldKeys;
                view.FilterControl.SetParentRow(dataGridView1.CurrentRow.ToDataDictionary());
                view.PrintMode = V6PrintMode.AutoLoadData;
                view.ShowToForm(this, _reportCaption, true);
            }

            SetStatus2Text();
        }

        #region ==== Xử lý F6 ====
        protected override void XuLyF6()
        {
            try
            {
                shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (this.ShowConfirmMessage(V6Text.Text("ASKEXPORTEXCELTUNGTRANG1"), V6Text.Confirm, 0, 0, "ASKEXPORTEXCELTUNGTRANG1") != DialogResult.Yes)
                {
                    return;
                }

                var saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Excel|*.xls;*.xlsx", _reportTitleF5);
                var saveExt = Path.GetExtension(saveFile);
                if (string.IsNullOrEmpty(saveFile))
                {
                    printting = false;
                }
                else
                {
                    saveFile = Path.Combine(Path.GetDirectoryName(saveFile) ?? V6Login.StartupPath, Path.GetFileNameWithoutExtension(saveFile));
                    All_Objects["saveFile"] = saveFile;
                    All_Objects["saveExt"] = saveExt;
                    printting = true;
                }                

                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF6_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                V6ControlFormHelper.NoOpen = true;
                F6Thread();
                //Thread t = new Thread(F6Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF6", ex);
            }
        }

        private void F6Thread()
        {
            f9Running = true;
            f9ErrorAll = "";

            int i = 0;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {

                        var TK = (row.Cells[KEYFIELD].Value ?? "").ToString().Trim();

                        var oldKeys = FilterControl.GetFilterParameters();
                        var _reportFileF5 = "AGLSO1TF5";
                        var _reportTitleF5 = "SỔ CÁI TÀI KHOẢN";
                        var _reportTitle2F5 = "Account detail";

                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                                _reportTitleF5, _reportTitle2F5, "", "", "");

                            view.CodeForm = CodeForm;
                            
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add(KEYFIELD, TK);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;
                            view.FilterControl.SetParentRow(row.ToDataDictionary());

                            view.AutoExportExcelFileName = All_Objects["saveFile"] + "_" + TK + All_Objects["saveExt"];

                            view.PrinterName = _PrinterName;
                            view.PrintCopies = _PrintCopies;
                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.ShowToForm(this, "", true);
                        }
                        else
                        {
                            var view = new ReportRViewBase(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                                _reportTitleF5, _reportTitle2F5, "", "", "");

                            view.CodeForm = CodeForm;
                            //view.FilterControl.Call1(ma_kh);
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add(KEYFIELD, TK);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;

                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;

                            view.FilterControl.SetParentRow(row.ToDataDictionary());

                            //view.AutoPrint = FilterControl.Check1;
                            view.AutoExportExcel = All_Objects["saveFile"] + "_" + TK + ".xls";

                            view.PrinterName = _PrinterName;
                            view.PrintCopies = _PrintCopies;

                            view.PrintMode = V6PrintMode.AutoLoadData;
                            view.ShowToForm(this, "", true);
                        }

                        SetStatus2Text();
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }
            f9Running = false;
        }

        void tF6_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                //  btnNhan.PerformClick();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.NoOpen = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 end.");
            }
        }

        #endregion ==== Xử lý F6 ====

        #region ==== Xử lý F7 ====
        protected override void XuLyF7()
        {
            try
            {
                shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (this.ShowConfirmMessage(V6Text.Text("ASKEXPORTEXCEL_NHIEUSHEET"), V6Text.Confirm, 0, 0, "ASKEXPORTEXCEL_NHIEUSHEET") != DialogResult.Yes)
                {
                    return;
                }
                
                var saveFile = V6ControlFormHelper.ChooseSaveFile(this, "Excel|*.xls;*.xlsx", _reportTitleF5);
                if (string.IsNullOrEmpty(saveFile))
                {
                    printting = false;
                }
                else
                {
                    All_Objects["saveFile"] = saveFile;
                    printting = true;
                }

                Timer tF7 = new Timer();
                tF7.Interval = 500;
                tF7.Tick += tF7_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                V6ControlFormHelper.NoOpen = true;
                F7Thread();

                tF7.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
        }

        private void F7Thread()
        {
            f9Running = true;
            f9ErrorAll = "";
            int i = 0;

            List<ExportExcelSetting> setting_list = new List<ExportExcelSetting>();
            string excelTemplateFile = null;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        var setting = new ExportExcelSetting();
                        setting_list.Add(setting);

                        var tk = (row.Cells[KEYFIELD].Value ?? "").ToString().Trim();

                        var oldKeys = FilterControl.GetFilterParameters();
                        var _reportFileF5 = "AGLSO1TF5";
                        var _reportTitleF5 = "SỔ CÁI TÀI KHOẢN";
                        var _reportTitle2F5 = "Account detail";

                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                                _reportTitleF5, _reportTitle2F5, "", "", "");

                            view.CodeForm = CodeForm;
                            //view.FilterControl.Call1(ma_kh);
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add(KEYFIELD, tk);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.SetLoaiTien("" + FilterControl.GetOD("VN_FC")); // hàm mới: gán check Tiền Việt / Ngoại tệ
                            view.SetLanguage("" + FilterControl.GetOD("VEBC"));  // hàm mới: gán check loại report Tiếng Việt, Eng, Cả 2 hoặc đã chọn lúc login.

                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;

                            view.FilterControl.SetParentRow(row.ToDataDictionary());


                            view.AutoExportExcelFileName = All_Objects["saveFile"] + "_" + tk + ".xls";
                            //view.AutoPrint = FilterControl.Check1;
                            //view.PrinterName = _PrinterName;
                            //view.PrintCopies = _PrintCopies;

                            view.GenerateProcedureParameters();
                            view.LoadData();
                            //view.Form_Load(view, new EventArgs());
                            view.ViewReport();// giả;
                            //view.ShowToForm(this, "", true);
                            // Gom data cho Xuất Excel nhiều Sheets.
                            setting.data = view._tbl1;
                            setting.data2 = view._tbl2;
                            //setting._pList = view._pList;
                            setting.sheet_name = row.Cells[KEYFIELD].Value.ToString();
                            setting.albcConfigData = view._albcConfig.DATA;
                            if (excelTemplateFile == null) excelTemplateFile = view.ExcelTemplateFileFull;
                            setting.reportParameters = view.ReportDocumentParameters;
                            V6ControlFormHelper.GEN_PARAMETERS_TO_SETTING_TEST(view._albcConfig, setting);
                        }// else... bỏ qua CR_report.

                        SetStatus2Text();
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }

            ExportData.ToExcelTemplate_ManySheet(excelTemplateFile, All_Objects["saveFile"].ToString(), setting_list, V6Setting.V6_number_format_info);
            if (V6Options.AutoOpenExcel)
            {
                V6ControlFormHelper.OpenFileProcess(All_Objects["saveFile"].ToString());
            }
            else
            {
                this.ShowInfoMessage(V6Text.ExportFinish);
            }

            f9Running = false;
        }

        void tF7_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F7 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                //  btnNhan.PerformClick();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.NoOpen = false;
                V6ControlFormHelper.SetStatusText("F7 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F7 end." + f9ErrorAll);
            }
        }

        #endregion ==== Xử lý F7 ====

        #region ==== Xử lý F9 ====


        private bool InLienTuc = false;
        private bool f9Running;
        private string f9Error = "";    // có bao nhiêu hiện bao nhiêu ở Status.
        private string f9ErrorAll = ""; // cộng dồn hiển thị cuối cùng.
        private string _oldDefaultPrinter, _PrinterName;
        private int _PrintCopies;
        private bool printting;
        private bool shift_is_down = false;
        protected override void XuLyF9()
        {
            try
            {
                shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (this.ShowConfirmMessage(V6Text.Text("ASKINTUNGTRANG1"), V6Text.Confirm, 0, 0, "ASKINTUNGTRANG1") != DialogResult.Yes)
                {
                    return;
                }
                InLienTuc = true;

                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
                _PrinterName = _oldDefaultPrinter;

                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                F9Thread();
                //Thread t = new Thread(F9Thread);
                //t.SetApartmentState(ApartmentState.STA);
                //t.IsBackground = true;
                //t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";

            int i = 0;
            int j = 0;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        j++;
                        var TK = (row.Cells[KEYFIELD].Value ?? "").ToString().Trim();
                        var oldKeys = FilterControl.GetFilterParameters();
                        //var _reportFileF5 = "AGLSO1TF5";
                        var _reportTitleF5 = "SỔ CÁI TÀI KHOẢN";
                        var _reportTitle2F5 = "Account detail";
                        
                        if (MenuButton.UseXtraReport != shift_is_down)
                        {
                            var view = new ReportR_DX(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                                _reportTitleF5, _reportTitle2F5, "", "", "");
                            
                            view.SetAO("INLIENTUC", "1");
                            view.CodeForm = CodeForm;
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add(KEYFIELD, TK);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.SetLoaiTien("" + FilterControl.GetOD("VN_FC"));
                            view.SetLanguage("" + FilterControl.GetOD("VEBC"));
                            
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;
                            view.FilterControl.SetParentRow(row.ToDataDictionary());
                            view.PrintMode = InLienTuc ? V6PrintMode.AutoPrint : V6PrintMode.DoNoThing;
                            if (j == 1) view.PrintMode = V6PrintMode.AutoClickPrint;
                            view.PrinterName = _PrinterName;
                            view.PrintCopies = _PrintCopies;
                            view.ShowToForm(this, _reportCaption, true);
                            if (j == 1) _PrinterName = view.PrinterName;
                        }
                        else
                        {
                            var view = new ReportRViewBase(m_itemId, _program + "F5", _reportProcedure + "F5", _reportFile + "F5",
                                _reportTitleF5, _reportTitle2F5, "", "", "");
                            view.SetAO("INLIENTUC", "1");
                            view.CodeForm = CodeForm;
                            
                            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                            data.Add(KEYFIELD, TK);
                            V6ControlFormHelper.SetFormDataDictionary(view.FilterControl, data);
                            view.CodeForm = CodeForm;
                            view.Advance = FilterControl.Advance;
                            view.FilterControl.String1 = FilterControl.String1;
                            view.FilterControl.String2 = FilterControl.String2;
                            view.SetLoaiTien("" + FilterControl.GetOD("VN_FC"));
                            view.SetLanguage("" + FilterControl.GetOD("VEBC"));
                            view.Dock = DockStyle.Fill;
                            view.FilterControl.InitFilters = oldKeys;
                            view.FilterControl.SetParentRow(row.ToDataDictionary());

                            view.PrintMode = InLienTuc ? V6PrintMode.AutoPrint : V6PrintMode.DoNoThing;
                            if (j == 1)
                            {
                                view.PrintMode = V6PrintMode.AutoClickPrint;
                            }
                            view.PrinterName = _PrinterName;
                            view.PrintCopies = _PrintCopies;
                            view.ShowToForm(this, "", true);
                            if (j == 1)
                            {
                                _PrinterName = view.PrinterName;
                            }
                        }

                        SetStatus2Text();
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }
            f9Running = false;
        }

        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                //  btnNhan.PerformClick();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                SetStatusText("F9 end.");
            }
        }
        #endregion xulyF9


        
        private bool f10Running;
        private string f10Message = "";
        private string f10ErrorAll = "";
        private string f10Error = "";

        protected override void XuLyF10()
        {
            try
            {
                shift_is_down = (ModifierKeys & Keys.Shift) == Keys.Shift;
                if (this.ShowConfirmMessage(V6Text.Text("ASKINLIENTUC"), V6Text.Confirm, 0, 0, "ASKINLIENTUC") != DialogResult.Yes)
                {
                    return;
                }

                InLienTuc = false;

                _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();

                if (InLienTuc) // thì chọn máy in trước
                {
                    var printerst = V6ControlFormHelper.ChoosePrinter(this, _oldDefaultPrinter);
                    if (printerst != null)
                    {
                        _PrinterName = printerst.PrinterName;
                        _PrintCopies = printerst.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                    }
                    else
                    {
                        printting = false;
                    }
                }

                remove_list_g = new List<DataGridViewRow>();
                Timer tF10 = new Timer();
                tF10.Interval = 500;
                tF10.Tick += tF10_Tick;
                CheckForIllegalCrossThreadCalls = false;
                Thread t = new Thread(F10Thread);
                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();
                tF10.Start();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF10: " + ex.Message);
            }
        }

        private void F10Thread()
        {
            f10Running = true;
            f10ErrorAll = "";

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsSelect())
                    {
                        remove_list_g.Add(row);
                    }
                }
                var oldKeys = FilterControl.GetFilterParameters();
                //var _reportFileF5 = "AGLSO1TF10";
                var _reportTitleF5 = "SỔ CÁI TÀI KHOẢN";
                var _reportTitle2F5 = "Account detail";

                if (MenuButton.UseXtraReport != shift_is_down)
                {
                    var view = new ReportRView2_DX(m_itemId, _program + "F10", _reportProcedure + "F10", _reportFile + "F10",
                        _reportTitleF5, _reportTitle2F5, "", "", "");

                    view.CodeForm = CodeForm;
                    view.Advance = FilterControl.Advance;
                    view.FilterControl.String1 = FilterControl.String1;
                    view.FilterControl.String2 = FilterControl.String2;

                    view.Dock = DockStyle.Fill;
                    view.FilterControl.InitFilters = oldKeys;

                    view.FilterControl.SetParentAllRow(dataGridView1);

                    //view.AutoPrint = InLienTuc;
                    //view.PrinterName = _PrinterName;
                    //view.PrintCopies = _PrintCopies;
                    view.PrintMode = V6PrintMode.AutoLoadData;
                    view.ShowToForm(this, _reportTitleF5, true);
                }
                else
                {
                    var view = new ReportRView2Base(m_itemId, _program + "F10", _reportProcedure + "F10", _reportFile + "F10",
                        _reportTitleF5, _reportTitle2F5, "", "", "");

                    view.CodeForm = CodeForm;
                    view.Advance = FilterControl.Advance;
                    view.FilterControl.String1 = FilterControl.String1;
                    view.FilterControl.String2 = FilterControl.String2;

                    view.Dock = DockStyle.Fill;
                    view.FilterControl.InitFilters = oldKeys;

                    view.FilterControl.SetParentAllRow(dataGridView1);

                    //view.AutoPrint = InLienTuc;
                    //view.PrinterName = _PrinterName;
                    //view.PrintCopies = _PrintCopies;
                    view.PrintMode = V6PrintMode.AutoLoadData;
                    view.ShowToForm(this, _reportTitleF5, true);
                }
                SetStatus2Text();
            }
            catch (Exception ex)
            {

                f10Error += ex.Message;
                f10ErrorAll += ex.Message;
            }


            f10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (f10Running)
            {
                var cError = f10Error;
                f10Error = f10Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F10 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                try
                {
                    PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                InLienTuc = false;
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (f10ErrorAll.Length > 0 ? "Error: " : "")
                    + f10ErrorAll);

                SetStatusText("F10 " + V6Text.Finish);
            }
        }

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                //var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                //var data = invoice.LoadAd81(sttRec);
                //dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        private string so_ctx_temp;
        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                

                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBoSungThongTinChungTuF4", ex);
            }
        }
    }
}
