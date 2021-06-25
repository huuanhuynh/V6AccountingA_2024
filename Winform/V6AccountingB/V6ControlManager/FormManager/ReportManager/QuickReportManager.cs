using System; 
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager
{
    public static class QuickReportManager
    {
        public static ReportFilter44Base AddFilterControl44Base(string program, string reportProcedure, Panel panel1, ToolTip toolTip)
        {
            panel1.Controls.Clear();

            var FilterControl = Filter.Filter.GetFilterControl44(program, reportProcedure, toolTip);
            panel1.Controls.Add(FilterControl);
            FilterControl.LoadLanguage();
            FilterControl.Focus();
            return FilterControl;
        }

        /// <summary>
        /// Xây dựng FilterControl động theo program.
        /// </summary>
        /// <param name="filterControl"></param>
        /// <param name="program"></param>
        /// <param name="all_Objects">sẽ được gán thêm filterControl và các line.NAME, các eventArgs (sender và e).</param>
        /// <param name="toolTip">Đối tượng toolTip trên Form</param>
        public static void MadeFilterControls0(ReportFilter44Base filterControl, string program,
            Dictionary<string, object> all_Objects, ToolTip toolTip)
        {
            Type Event_program = null;
            //Dictionary<string, object> All_Objects = new Dictionary<string, object>();
            all_Objects["filterControl"] = filterControl;
            string all_using_text = "", all_method_text = "";

            SqlParameter[] plist =
            {
                new SqlParameter("@ma_bc", program), 
            };
            var dataALREPORT1 = V6BusinessHelper.Select("ALREPORT1", "*", "Ma_bc=@ma_bc", "", "Stt_filter", plist).Data;
            var viewSortSttKey = new DataView(dataALREPORT1);
            viewSortSttKey.Sort = "Stt_Key";
            var keyList = viewSortSttKey.ToTable().ToDataDictionary("key1", "key1");
            var keyList2 = viewSortSttKey.ToTable().ToDataDictionary("key2", "key2");
            filterControl.ParameterNameList = keyList;
            filterControl.ParameterNameList2 = keyList2;
            //FilterControl.
            string err = "";
            try
            {
                int i = 0, lineTop = 35;
                foreach (DataRow row in dataALREPORT1.Rows)
                {
                    var ten = row["ten"].ToString();
                    try
                    {
                        string define = row["filter"].ToString().Trim();
                        string define_M = row["filter_M"].ToString().Trim();
                        var defineInfo = new DefineInfo(define);
                        var defineInfo_M = new DefineInfo(define_M);

                        var lineControl0 = V6ControlFormHelper.MadeLineDynamicControl(define, toolTip);
                        all_Objects[lineControl0.Name] = lineControl0;

                        
                        if (lineControl0 is FilterLineDynamic)
                        {
                            FilterLineDynamic lineControl = lineControl0 as FilterLineDynamic;
                            var key1 = row["key1"].ToString().Trim();
                            var key2 = row["key2"].ToString().Trim();
                            var key3 = row["key3"].ToString().Trim();
                            var key4 = row["key4"].ToString().Trim();
                            var loai_key = row["loai_key"].ToString().Trim();
                            lineControl.DefineInfo.Key1 = key1;
                            lineControl.DefineInfo.Key2 = key2;
                            lineControl.DefineInfo.Key3 = key3;
                            lineControl.DefineInfo.Key4 = key4;
                            lineControl.DefineInfo.Loai_key = loai_key;


                            //Vị trí
                            lineControl.Location = new Point(3, lineTop);
                            if (lineControl.DefineInfo.Visible)
                            {
                                lineTop += lineControl.Height;
                            }
                            filterControl.AddLineControls(lineControl);
                            //panel1.Controls.Add(lineControl);
                            //Lookup Button
                            if (defineInfo_M.Visible)
                            {
                                LookupButton lButton = new LookupButton();
                                filterControl.groupBox1.Controls.Add(lButton);
                                lButton.ReferenceControl = lineControl0;

                                lButton.Name = "lbt" + defineInfo.Field;

                                lButton.R_DataType = defineInfo_M.R_DataType;
                                //lButton.R_Value = defineInfo_M.R_Value;
                                //lButton.R_Vvar = defineInfo_M.R_Vvar;
                                //lButton.R_Stt_rec = defineInfo_M.R_Stt_rec;
                                lButton.R_Ma_ct = defineInfo_M.R_Ma_ct;

                                lButton.M_DataType = defineInfo_M.M_DataType;
                                lButton.M_Value = defineInfo_M.M_Value;
                                lButton.M_Vvar = defineInfo_M.M_Vvar;
                                lButton.M_Stt_Rec = defineInfo_M.M_Stt_Rec;
                                lButton.M_Ma_ct = defineInfo_M.M_Ma_ct;

                                lButton.M_Type = defineInfo_M.M_Type;
                                //lButton.M_User_id = defineInfo_M.M_User_id;
                                //lButton.M_Lan = defineInfo_M.V6Login.SelectedLanguage;

                                lButton.Visible = defineInfo_M.Visible;
                            }

                            //Giữ lại control ngày.
                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT1")
                            {
                                filterControl.lineNgay_ct1 = lineControl;
                                lineControl.SetValue(V6Setting.M_ngay_ct1);
                            }

                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT2")
                            {
                                filterControl.lineNgay_ct2 = lineControl;
                                lineControl.SetValue(V6Setting.M_ngay_ct2);
                            }
                            //Giu lai tiente, ngonnguBC
                            if (lineControl.DefineInfo.DefaultValue == "M_MAU_BC")
                            {
                                filterControl.lineMauBC = lineControl;
                                //lineControl.SetValue(MAU == "VN" ? "0" : "1");
                            }

                            if (lineControl.DefineInfo.DefaultValue == "M_LAN")
                            {
                                filterControl.lineLAN = lineControl;
                                //lineLAN.SetValue(LAN);
                            }
                            //Giữ lại user_id
                            if (lineControl.DefineInfo.DefaultValue == "M_USER_ID")
                            {
                                filterControl.lineUserID = lineControl;
                                lineControl.SetValue(V6Login.UserId);
                            }

                            string xml = row["DMETHOD"].ToString().Trim();
                            if (!string.IsNullOrEmpty(xml))
                            {
                                DataSet ds = new DataSet();
                                ds.ReadXml(new StringReader(xml));
                                if (ds.Tables.Count <= 0) break;

                                var data = ds.Tables[0];

                                foreach (DataRow event_row in data.Rows)
                                {
                                    //nơi sử dụng: QuickReportManager, DynamicAddEditForm
                                    //Cần viết lại lineControl events.
                                    try
                                    {
                                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                                        var method_name = event_row["method"].ToString().Trim();

                                        all_using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                                        all_method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";

                                        //Make dynamic event and call
                                        switch (EVENT_NAME)
                                        {
                                            case ControlDynamicEvent.TEXTCHANGE:
                                                lineControl.TextChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.VALUECHANGE:
                                                //V6NumberTextBox numInput = lineControl._ as V6NumberTextBox;
                                                //if (numInput == null) break;

                                                lineControl.ValueChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.LOSTFOCUS:
                                                lineControl.Leave += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.GOTFOCUS:
                                                lineControl.GotFocus += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.V6LOSTFOCUS:
                                                lineControl.V6LostFocus += (s) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["eventargs"] = null;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.KEYDOWN:
                                                lineControl.KeyDown += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;

                                            case ControlDynamicEvent.CLICK:
                                                lineControl.Click += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    all_Objects["sender"] = s;
                                                    all_Objects["e"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, all_Objects);
                                                };
                                                break;
                                        }//end switch
                                    }
                                    catch (Exception exfor)
                                    {
                                        V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".EventFor", exfor);
                                    }
                                }//end for
                            }

                            // Add Description
                            if (toolTip != null)
                            {
                                string des = "" + defineInfo.DescriptionLang(V6Setting.IsVietnamese);
                                if (des.Trim() != "")
                                {
                                    toolTip.SetToolTip(lineControl.InputControl, des);
                                    toolTip.SetToolTip(lineControl, des);
                                }
                            }
                        }
                        else if (lineControl0 is FilterGroup)
                        {
                            //Copy code
                            FilterGroup lineControl = lineControl0 as FilterGroup;
                            var key1 = row["key1"].ToString().Trim();
                            var key2 = row["key2"].ToString().Trim();
                            var key3 = row["key3"].ToString().Trim();
                            var key4 = row["key4"].ToString().Trim();
                            var loai_key = row["loai_key"].ToString().Trim();
                            lineControl.DefineInfo.Key1 = key1;
                            lineControl.DefineInfo.Key2 = key2;
                            lineControl.DefineInfo.Key3 = key3;
                            lineControl.DefineInfo.Key4 = key4;
                            lineControl.DefineInfo.Loai_key = loai_key;


                            //Vị trí
                            lineControl.Location = new Point(3, lineTop);
                            lineTop += lineControl.Height;
                            filterControl.AddLineGroupControls(lineControl);
                        }
                        i++;
                    }
                    catch (Exception e1)
                    {
                        err += "\n" + i + " " + ten + ": " + e1.Message;
                    }
                }
                Event_program = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + program, all_using_text, all_method_text);
            }
            catch (Exception ex)
            {
                err += "\n" + ex.Message;
                filterControl.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".MadeFilterControls", ex);
            }
            if (err.Length > 0)
            {
                filterControl.ShowErrorMessage("MadeFilterControls error: " + err);
            }

            all_Objects = all_Objects;
        }

        public static void ShowQuickReport(Control owner, string itemId)
        {
            try
            {
                //return;
                var data = V6Menu.GetMenuQuickReport();
                if (data == null || data.Rows.Count == 0) return;
                
                var row = data.Rows[0];
                
                MenuButton mButton = new MenuButton()
                {
                    ItemID = row["itemid"].ToString().Trim().ToUpper(),
                    Text = V6Setting.IsVietnamese
                        ? row["vbar"].ToString().Trim()
                        : row["vbar2"].ToString().Trim(),
                    CodeForm = row["codeform"].ToString().Trim(),
                    Pro_old = row["pro_old"] == null ? null : row["pro_old"].ToString().Trim(),
                    Exe = row["program"].ToString().Trim(),
                    MaChungTu = row["ma_ct"].ToString().Trim(),
                    NhatKy = row["nhat_ky"].ToString().Trim(),

                    ReportFile = row["rep_file"].ToString().Trim(),
                    ReportTitle = row["title"].ToString().Trim(),
                    ReportTitle2 = row["title2"].ToString().Trim(),
                    ReportFileF5 = row["rep_fileF5"].ToString().Trim(),
                    ReportTitleF5 = row["titleF5"].ToString().Trim(),
                    ReportTitle2F5 = row["title2F5"].ToString().Trim(),

                    Key1 = row["Key1"].ToString().Trim(),
                    Key2 = row["Key2"].ToString().Trim(),
                    Key3 = row["Key3"].ToString().Trim(),
                    Key4 = row["Key4"].ToString().Trim(),
                };
                var c = MenuManager.MenuManager.GenControl(owner, mButton, null);
                //auto click
                if (c is ReportRViewBase)
                {
                    (c as ReportRViewBase).btnNhan_Click(null, null);
                }
                else if (c is ReportR44ViewBase)
                {
                    (c as ReportR44ViewBase).btnNhan_Click(null, null);
                }
                else if (c is ReportRView2Base)
                {
                    (c as ReportRView2Base).btnNhan_Click(null, null);
                }
                else if (c is ReportRWWView2Base)
                {
                    (c as ReportRWWView2Base).btnNhan_Click(null, null);
                }
                else if (c is ReportDViewBase)
                {
                    (c as ReportDViewBase).btnNhan_Click(null, null);
                }
                else if (c is ReportD99ViewBase)
                {
                    (c as ReportD99ViewBase).btnNhan_Click(null, null);
                }

                c.ShowToForm(owner, mButton.ReportTitle + (string.IsNullOrEmpty(mButton.ReportFile) ? "Code: " + mButton.CodeForm : ""),
                    true, false); // fullScreen, dialog
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void ShowReportR(IWin32Window owner, QuickReportParams quickParams)
        {
            V6FormControl view0 = null;
            if (quickParams.UseXtraReport != quickParams.Shift)
            {
                view0 = new ReportR_DX(quickParams.ItemID, quickParams.Program, quickParams.ReportProcedure,
                 quickParams.ReportFile, quickParams.ReportCaption, quickParams.ReportCaption2,
                 quickParams.ReportFileF5, quickParams.ReportTitleF5, quickParams.ReportTitle2F5);

                var view = (ReportR_DX)view0;
                view.CodeForm = quickParams.CodeForm;
                view.FilterControl.String1 = quickParams.FilterControlString1;
                view.FilterControl.String2 = quickParams.FilterControlString2;
                view.FilterControl.ParentFilterData = quickParams.FilterControlFilterData;
                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = quickParams.FilterControlInitFilters;
                view.FilterControl.SetParentRow(quickParams.ParentRowData);
                if (quickParams.FilterData != null)
                    view.FilterControl.SetData(quickParams.FilterData);

                if (quickParams.DataSet == null || quickParams.DataSet.Tables.Count == 0)
                {
                    if (quickParams.AutoRun) view.btnNhan_Click(null, null);
                }
                else
                {
                    view.DataSet = quickParams.DataSet;
                    if (quickParams.DisableBtnNhan) view.btnNhan.Enabled = false;
                }
            }
            else
            {
                view0 = new ReportRViewBase(quickParams.ItemID, quickParams.Program, quickParams.ReportProcedure,
                    quickParams.ReportFile, quickParams.ReportCaption, quickParams.ReportCaption2,
                    quickParams.ReportFileF5, quickParams.ReportTitleF5, quickParams.ReportTitle2F5);

                var view = (ReportRViewBase)view0;
                view.CodeForm = quickParams.CodeForm;
                view.FilterControl.String1 = quickParams.FilterControlString1;
                view.FilterControl.String2 = quickParams.FilterControlString2;
                view.FilterControl.ParentFilterData = quickParams.FilterControlFilterData;
                view.Dock = DockStyle.Fill;
                view.FilterControl.InitFilters = quickParams.FilterControlInitFilters;
                view.FilterControl.SetParentRow(quickParams.ParentRowData);
                if (quickParams.FilterData != null)
                    view.FilterControl.SetData(quickParams.FilterData);

                if (quickParams.DataSet == null || quickParams.DataSet.Tables.Count == 0)
                {
                    if (quickParams.AutoRun) view.btnNhan_Click(null, null);
                }
                else
                {
                    view.DataSet = quickParams.DataSet;
                    if (quickParams.DisableBtnNhan) view.btnNhan.Enabled = false;
                }
            }            

            view0.ShowToForm(owner, quickParams.FormTitle, true);
        }
        
        /// <summary>
        /// Xuất dữ liệu report thành pdf hoặc excel.
        /// </summary>
        /// <param name="owner">Form nền.</param>
        /// <param name="proc">Tên sql procedure.</param>
        /// <param name="parameters">Các tham số cho procedure.</param>
        /// <param name="Ma_File"></param>
        /// <param name="LAN">EVB</param>
        /// <param name="exportFile">Tên file xuất lưu.</param>
        /// <param name="MAU">VN/FC</param>
        public static void ExportReport(IWin32Window owner, string proc, SqlParameter[] parameters, string Ma_File, string MAU, string LAN, string exportFile)
        {
            try
            {
                if (string.IsNullOrEmpty(exportFile))
                {
                    V6ControlFormHelper.ShowWarningMessage("exportFile!");
                    return;
                }
                string ext = Path.GetExtension(exportFile).ToLower();
                if (ext == ".pdf" || ext.StartsWith(".doc") || ext.StartsWith(".xls"))
                {
                    //DoNothing;
                }
                else
                {
                    if (string.IsNullOrEmpty(ext))
                    {
                        ext = "(extension is null)";
                    }
                    V6ControlFormHelper.ShowWarningMessage(V6Text.Unsupported + " " + ext);
                    return;
                }



                var MauInData = Albc.GetRow(MAU, LAN, Ma_File);
                DataSet ds = LoadData_ER(proc, parameters);
                DataTable tbl = ds.Tables[0];
                DataTable tbl2 = ds.Tables[1];
                var ReportDocumentParameters = GetAllReportParams(LAN, MauInData, tbl2);
                
                

                if (ext == ".pdf" || ext.StartsWith(".doc"))
                {
                    string reportFile = string.Format(@"Reports\{0}\{1}\{2}.rpt", MAU, LAN, Ma_File);
                    ReportDocument rpt = LoadRpt_ER(ds, reportFile);
                    SetAllReportParams(rpt, ReportDocumentParameters);
                    SetCrossLineRpt(rpt, MauInData, tbl);
                    if (ext == ".pdf")
                    {
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, exportFile);
                    }
                    else if (ext.StartsWith(".doc"))
                    {
                        rpt.ExportToDisk(ExportFormatType.WordForWindows, exportFile);
                    }
                }
                else if (ext.StartsWith(".xls"))
                {
                    string ReportFile = MauInData["Report"].ToString().Trim();
                    //string ReportTitle = MauInData["Title"].ToString().Trim();
                    string ExcelTemplateFileFull = string.Format(@"Reports\{0}\{1}\{2}.xls", MAU, LAN, Ma_File);
                    if (File.Exists(ExcelTemplateFileFull))
                    {
                        V6ControlFormHelper.ExportExcelTemplate(owner, tbl, tbl2, ReportDocumentParameters,
                            MAU, LAN, ReportFile, ExcelTemplateFileFull, exportFile);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowWarningMessage("Không có file mẫu: " + ExcelTemplateFileFull, owner);
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ext))
                    {
                        ext = "(extension is null)";
                    }
                    V6ControlFormHelper.ShowWarningMessage(V6Text.Unsupported + " " + ext);
                    return;
                }

                if (V6Options.AutoOpenExcel)
                {
                    V6ControlFormHelper.OpenFileProcess(exportFile);
                }
                else
                {
                    V6ControlFormHelper.ShowMessage(V6Text.ExportFinish + "\n" + exportFile, owner);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException("Quick.ExportReport", ex);
            }
        }

        private static void SetCrossLineRpt(ReportDocument rpt, DataRow MauInData, DataTable _tbl)
        {
            int flag = 0;
            var checkField = "TEN_VT";
            try
            {
                bool IsInvoice = ObjectAndString.ObjectToInt(MauInData["ND51"]) == 1;
                if (!IsInvoice) return;

                var Khung = rpt.ReportDefinition.ReportObjects["Khung"];
                var DuongNgang = rpt.ReportDefinition.ReportObjects["DuongNgang"];
                var DuongCheo = rpt.ReportDefinition.ReportObjects["DuongCheo"];
                flag = 1;
                //var Section1 = rpt.ReportDefinition.Sections["ReportHeaderSection1"];
                //var Section2 = rpt.ReportDefinition.Sections["Section3"];
                //var h1 = Section1.Height;
                //var h2 = Section2.Height;



                int boxTop = Khung.Top;// 6500;
                int boxHeight = Khung.Height;// 3840;
                int lineHeight = DuongNgang.Height;

                int halfLineHeight = lineHeight / 2;// boxHeight/20;//192, 20 is maxLine

                int dropMax = 40;
                try
                {
                    //dropMax = ObjectAndString.ObjectToInt(Invoice.Alct["drop_Max"]);
                    if (dropMax < 1) dropMax = 40;
                    //Lấy lại thông tin dropMax theo albc (cboMauin)
                    if (MauInData != null && MauInData.Table.Columns.Contains("DROP_MAX"))
                    {
                        var dropMaxT = ObjectAndString.ObjectToInt(MauInData["DROP_MAX"]);
                        if (dropMaxT > 5) dropMax = dropMaxT;
                    }
                    //Lấy lại checkField (khác MA_VT)
                    if (MauInData != null && MauInData.Table.Columns.Contains("FIELD_MAX"))
                    {
                        var checkFieldT = MauInData["FIELD_MAX"].ToString().Trim();
                        if (checkFieldT.Length > 0) checkField = checkFieldT;
                    }
                }
                catch
                {
                    flag = 2;
                }


                if (!_tbl.Columns.Contains(checkField))
                {
                    checkField = _tbl.Columns.Contains("DIEN_GIAII") ? "DIEN_GIAII" : _tbl.Columns[0].ColumnName;
                }
                flag = 3;
                int crossLineNum = CalculateCrossLine(_tbl, checkField, dropMax, lineHeight);
                    //+ (int)numCrossAdd.Value; // điều chỉnh
                var top = boxTop + (halfLineHeight * crossLineNum);//3840/20=192
                var height = boxHeight - (top - boxTop);
                flag = 5;
                //if (height <= 0) height = 10;
                if (height < 150) // Hide lowCrossline.
                {
                    height = 10;
                    DuongNgang.Width = DuongNgang.Width + DuongCheo.Width;
                    DuongCheo.Width = 10;
                }

                DuongNgang.Height = 10;
                DuongNgang.Top = top + 30;

                DuongCheo.Height = height;
                DuongCheo.Top = top;

                flag = 9;
            }
            catch (Exception ex)
            {
                if (flag == 3)
                    V6ControlFormHelper.ShowMessage("Kiểm tra thông tin trường tính toán drop_line [" + checkField + "]");
                V6ControlFormHelper.WriteExLog("QuickReportManager" + ".SetCrossLineRpt", ex);
            }
        }

        private static int CalculateCrossLine(DataTable t, string field, int lengOfName, int twLineHeight)
        {
            var dropLineHeightBase = 300;
            var lineDroppedHeight = 600;
            var dropLineHeight1 = lineDroppedHeight - twLineHeight;
            //Mỗi dòng drop sẽ nhân với DropLineHeight
            //var dropCount = 0;
            var dropHeight = 0;
            foreach (DataRow r in t.Rows)
            {
                try
                {
                    var len = r[field].ToString().Trim().Length;
                    if (len > 1) len--;

                    var dropCount = len / lengOfName;
                    if (dropCount > 0)
                    {
                        dropHeight += dropLineHeight1;
                        if (dropCount > 1) dropHeight += dropLineHeightBase * (dropCount - 1);
                        //if (dropCount > 2) dropHeight += dropLineHeightBase;
                        //if (dropCount > 3) dropHeight += dropLineHeightBase;
                        //if (dropCount > 4) dropHeight += dropLineHeightBase;
                    }
                }
                catch
                {
                    // ignored
                }
            }


            //var dropHeight = dropLineHeight * dropCount;
            //số 2 ở đây là vì mỗi dòng có 2 cross line
            return t.Rows.Count * 2 + dropHeight / (twLineHeight / 2);
        }

        private static SortedDictionary<string, object> GetAllReportParams(string LAN, DataRow MauInData, DataTable _tbl2)
        {
            SortedDictionary<string, object>  ReportDocumentParameters = new SortedDictionary<string, object>();

            ReportDocumentParameters.Add("Decimals", 0);
            ReportDocumentParameters.Add("ThousandsSeparator", V6Options.M_NUM_SEPARATOR);
            ReportDocumentParameters.Add("DecimalSymbol", V6Options.M_NUM_POINT);
            ReportDocumentParameters.Add("DecimalsSL", V6Options.M_IP_R_SL);
            ReportDocumentParameters.Add("DecimalsDG", V6Options.M_IP_R_GIA);
            ReportDocumentParameters.Add("DecimalsDGNT", V6Options.M_IP_R_GIANT);
            ReportDocumentParameters.Add("DecimalsTT", V6Options.M_IP_R_TIEN);
            ReportDocumentParameters.Add("DecimalsTTNT", V6Options.M_IP_R_TIENNT);

            ReportDocumentParameters.Add("Mau?", 0);
            ReportDocumentParameters.Add("BanSao?", false);
            int MauTuIn = ObjectAndString.ObjectToInt(MauInData["MAU_TU_IN"]);
            ReportDocumentParameters.Add("ViewInfo", MauTuIn == 1);
            ReportDocumentParameters.Add(
                "Info",
                "In bởi  Phần mềm V6 Accounting2016.NET - Cty phần mềm V6 (www.v6corp.com) - MST: 0303180249 - ĐT: 08.62570563"
            );
            ReportDocumentParameters.Add("ViewCrossLine", true);


            //ReportDocumentParameters.Add("CrossLineNum", crossLineNum + numCrossAdd.Value);
            decimal TTT = ObjectAndString.ObjectToDecimal(_tbl2.Rows[0]["TTT"]);
            decimal TTT_NT = ObjectAndString.ObjectToDecimal(_tbl2.Rows[0]["TTT_NT"]);
            string MA_NT = _tbl2.Rows[0]["MA_NT"].ToString().Trim();
            ReportDocumentParameters.Add("SoTienVietBangChu", V6BusinessHelper.MoneyToWords(TTT, LAN, V6Options.M_MA_NT0));
            ReportDocumentParameters.Add("SoTienVietBangChuNT", V6BusinessHelper.MoneyToWords(TTT_NT, LAN, MA_NT));

            //ReportDocumentParameters.Add("ChuoiMaHoa", V6BusinessHelper.GetChuoiMaHoa(""));

            ReportDocumentParameters.Add("Title", MauInData["Title"].ToString().Trim());// "txtReportTitle.Text.Trim()");
            // V6Soft
            ReportDocumentParameters.Add("M_TEN_CTY", V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper());
            ReportDocumentParameters.Add("M_TEN_TCTY", V6Soft.V6SoftValue["M_TEN_TCTY"].ToUpper());
            ReportDocumentParameters.Add("M_DIA_CHI", V6Soft.V6SoftValue["M_DIA_CHI"]);


            ReportDocumentParameters.Add("M_TEN_CTY2", V6Soft.V6SoftValue["M_TEN_CTY2"].ToUpper());
            ReportDocumentParameters.Add("M_TEN_TCTY2", V6Soft.V6SoftValue["M_TEN_TCTY2"].ToUpper());
            ReportDocumentParameters.Add("M_DIA_CHI2", V6Soft.V6SoftValue["M_DIA_CHI2"]);
            // V6option
            ReportDocumentParameters.Add("M_MA_THUE", V6Options.GetValue("M_MA_THUE"));
            ReportDocumentParameters.Add("M_RTEN_VSOFT", V6Options.GetValue("M_RTEN_VSOFT"));

            ReportDocumentParameters.Add("M_TEN_NLB", "");// txtM_TEN_NLB.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_NLB2", "");// txtM_TEN_NLB2.Text.Trim());
            ReportDocumentParameters.Add("M_TEN_KHO_BD", V6Options.GetValue("M_TEN_KHO_BD"));
            ReportDocumentParameters.Add("M_TEN_KHO2_BD", V6Options.GetValue("M_TEN_KHO2_BD"));
            ReportDocumentParameters.Add("M_DIA_CHI_BD", V6Options.GetValue("M_DIA_CHI_BD"));
            ReportDocumentParameters.Add("M_DIA_CHI2_BD", V6Options.GetValue("M_DIA_CHI2_BD"));

            ReportDocumentParameters.Add("M_TEN_GD", V6Options.GetValue("M_TEN_GD"));
            ReportDocumentParameters.Add("M_TEN_GD2", V6Options.GetValue("M_TEN_GD2"));
            ReportDocumentParameters.Add("M_TEN_KTT", V6Options.GetValue("M_TEN_KTT"));
            ReportDocumentParameters.Add("M_TEN_KTT2", V6Options.GetValue("M_TEN_KTT2"));

            ReportDocumentParameters.Add("M_SO_QD_CDKT", V6Options.GetValue("M_SO_QD_CDKT"));
            ReportDocumentParameters.Add("M_SO_QD_CDKT2", V6Options.GetValue("M_SO_QD_CDKT2"));
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT", V6Options.GetValue("M_NGAY_QD_CDKT"));
            ReportDocumentParameters.Add("M_NGAY_QD_CDKT2", V6Options.GetValue("M_NGAY_QD_CDKT2"));

            ReportDocumentParameters.Add("M_RFONTNAME", V6Options.GetValue("M_RFONTNAME"));
            ReportDocumentParameters.Add("M_R_FONTSIZE", V6Options.GetValue("M_R_FONTSIZE"));


            V6Login.SetCompanyInfo(ReportDocumentParameters);

            //if (FilterControl.RptExtraParameters != null)
            //{
            //    ReportDocumentParameters.AddRange(FilterControl.RptExtraParameters, true);
            //}

            //var rptExtraParametersD = FilterControl.GetRptParametersD(Extra_para, LAN);

            //if (rptExtraParametersD != null)
            //{
            //    ReportDocumentParameters.AddRange(rptExtraParametersD, true);
            //}

            

            return ReportDocumentParameters;
        }

        private static void SetAllReportParams(ReportDocument rpt, SortedDictionary<string, object> ReportDocumentParameters)
        {
            string errors = "";
            foreach (KeyValuePair<string, object> item in ReportDocumentParameters)
            {
                try
                {
                    rpt.SetParameterValue(item.Key, item.Value);
                }
                catch (Exception ex)
                {
                    errors += "rpDoc " + item.Key + ": " + ex.Message + "\n";
                }
            }


            if (errors != "")
            {
                V6ControlFormHelper.WriteToLog("QuickReportManager" + ".SetAllReportParams:", errors);
            }
        }

        private static ReportDocument LoadRpt_ER(DataSet ds, string reportFile)
        {
            var rpDoc = new ReportDocument();
            if (File.Exists(reportFile)) rpDoc.Load(reportFile);
            else V6ControlFormHelper.ShowWarningMessage(V6Text.NotExist + ": " + reportFile);
            rpDoc.SetDataSource(ds);
            return rpDoc;
        }

        private static DataSet LoadData_ER(string proc, SqlParameter[] _pList)
        {
            DataSet ds = V6BusinessHelper.ExecuteProcedure(proc, _pList);
            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].TableName = "DataTable1";
            }
            if (ds.Tables.Count > 1)
            {
                ds.Tables[1].TableName = "DataTable2";
            }
            return ds;
        }

    }

    public struct QuickReportParams
    {
        public string ItemID { get; set; }
        public string Program { get; set; }
        public string ReportProcedure { get; set; }
        public string ReportFile { get; set; }
        public string ReportCaption { get; set; }
        public string ReportCaption2 { get; set; }
        public string ReportFileF5 { get; set; }
        public string ReportTitleF5 { get; set; }
        public string ReportTitle2F5 { get; set; }
        /// <summary>
        /// Các param của line từ lớp cha FilterControl.GetFilterParameters
        /// </summary>
        public List<SqlParameter> FilterControlInitFilters { get; set; }
        public string CodeForm { get; set; }
        public string FilterControlString1 { get; set; }
        public string FilterControlString2 { get; set; }
        public IDictionary<string, object> ParentRowData { get; set; }
        public string FormTitle { get; set; }
        /// <summary>
        /// Dữ liệu cho report. nếu có sẵn sẽ không chạy proc tự động.
        /// </summary>
        public DataSet DataSet { get; set; }

        public IDictionary<string, object> FilterControlFilterData { get; set; }
        public IDictionary<string, object> FilterData { get; set; }
        public bool AutoRun { get; set; }
        public bool DisableBtnNhan { get; set; }
        public bool UseXtraReport { get; set; }
        public bool Shift { get; set; }
        
    }
}
