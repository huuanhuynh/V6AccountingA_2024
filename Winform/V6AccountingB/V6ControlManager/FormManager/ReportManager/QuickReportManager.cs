using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.ReportD;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager
{
    public static class QuickReportManager
    {
        public static ReportFilter44Base AddFilterControl44Base(string program, Panel panel1)
        {
            var FilterControl = Filter.Filter.GetFilterControl44(program);
            panel1.Controls.Add(FilterControl);
            FilterControl.Focus();
            return FilterControl;
        }

        public static void MadeFilterControls(ReportFilter44Base filterControl, string program, out Dictionary<string, object> all_Objects)
        {
            Type Event_program = null;
            Dictionary<string, object> All_Objects = new Dictionary<string, object>(); 
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
                int i = 0;
                foreach (DataRow row in dataALREPORT1.Rows)
                {
                    var ten = row["ten"].ToString();
                    try
                    {
                        string define = row["filter"].ToString().Trim();
                        

                        var lineControl = V6ControlFormHelper.MadeLineDynamicControl(define);
                        All_Objects[lineControl.Name] = lineControl;

                        if (lineControl != null)
                        {
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
                            lineControl.Location = new Point(3, 35 + 25 * i);
                            filterControl.AddLineControls(lineControl);
                            //panel1.Controls.Add(lineControl);

                            //Giữ lại control ngày.
                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT1")
                                filterControl.lineNgay_ct1 = lineControl;
                            if (lineControl.DefineInfo.DefaultValue == "M_NGAY_CT2")
                                filterControl.lineNgay_ct2 = lineControl;
                            //Giu lai tiente, ngonnguBC
                            if (lineControl.DefineInfo.DefaultValue == "M_MAU_BC")
                                filterControl.lineMauBC = lineControl;
                            if (lineControl.DefineInfo.DefaultValue == "M_LAN")
                                filterControl.lineLAN = lineControl;

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

                                        if (data.Columns.Contains("using")) all_using_text += event_row["using"];
                                        all_method_text += event_row["content"];
                                        all_method_text += " ";

                                        //Make dynamic event and call
                                        switch (EVENT_NAME)
                                        {
                                            case "TEXTCHANGE":
                                                lineControl.TextChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "VALUECHANGE":
                                                //V6NumberTextBox numInput = lineControl._ as V6NumberTextBox;
                                                //if (numInput == null) break;

                                                lineControl.ValueChanged += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "LOSTFOCUS":
                                                lineControl.LostFocus += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "GOTFOCUS":
                                                lineControl.GotFocus += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "V6LOSTFOCUS":
                                                lineControl.V6LostFocus += (s) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = null;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "KEYDOWN":
                                                lineControl.KeyDown += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
                                                };
                                                break;

                                            case "CLICK":
                                                lineControl.Click += (s, e) =>
                                                {
                                                    if (Event_program == null) return;

                                                    All_Objects["sender"] = s;
                                                    All_Objects["eventargs"] = e;
                                                    V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
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

            all_Objects = All_Objects;
        }

        public static void ShowQuickReport(string itemId)
        {
            try
            {
                return;
                ReportRViewBase qr = new ReportRViewBase
                    (itemId, "ASOTH1", "ASOTH1", "ASOTH1", "rtitle", "t2", "rf5", "tf5", "t2f5");
                qr.ShowToForm("QuickReport", true);
                qr.btnNhan_Click(null, null);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public static void ShowReportR(IWin32Window owner, QuickReportParams quickParams)
        {
            var view = new ReportRViewBase(quickParams.ItemID, quickParams.Program, quickParams.ReportProcedure,
                quickParams.ReportFile, quickParams.ReportCaption, quickParams.ReportCaption2,
                quickParams.ReportFileF5, quickParams.ReportTitleF5, quickParams.ReportTitle2F5);
            view.CodeForm = quickParams.CodeForm;
            view.FilterControl.String1 = quickParams.FilterControlString1;
            view.FilterControl.String2 = quickParams.FilterControlString2;
            view.FilterControl.ParentFilterData = quickParams.FilterControlFilterData;
            view.Dock = DockStyle.Fill;
            view.FilterControl.InitFilters = quickParams.FilterControlInitFilters;
            view.FilterControl.SetParentRow(quickParams.ParentRowData);
            if(quickParams.FilterData != null)
            view.FilterControl.SetData(quickParams.FilterData);

            if (quickParams.DataSet == null || quickParams.DataSet.Tables.Count == 0)
            {
                if(quickParams.AutoRun) view.btnNhan_Click(null, null);
            }
            else view.DataSet = quickParams.DataSet;

            view.ShowToForm(quickParams.FormTitle, true);
        }

        /// <summary>
        /// Tập hợp các event động trên form report.
        /// </summary>
        public static class FormEvent
        {
            public static string INIT = "INIT";
            public static string BEFORELOADDATA = "BEFORELOADDATA";
            public static string AFTERADDFILTERCONTROL = "AFTERADDFILTERCONTROL";
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

        public SortedDictionary<string, object> FilterControlFilterData { get; set; }
        public SortedDictionary<string, object> FilterData { get; set; }
        public bool AutoRun { get; set; }
    }
}
