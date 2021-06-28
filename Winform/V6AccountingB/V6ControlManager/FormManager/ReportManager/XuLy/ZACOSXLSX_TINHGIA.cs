using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class ZACOSXLSX_TINHGIA : XuLy48Base
    {

        public ZACOSXLSX_TINHGIA(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.enter_to_tab = false;
        }

        void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Tính chức năng đang chọn, F10 Chạy tiếp sau điều chỉnh");
        }


        protected override void MakeReport2()
        {
            try
            {
                if (GenerateProcedureParameters())
                {
                    int check = V6BusinessHelper.CheckDataLocked("1", FilterControl.Date1, (int)FilterControl.Number1, (int)FilterControl.Number2);
                    if (check == 1)
                    {
                        this.ShowWarningMessage(V6Text.CheckLock);
                        return;
                    }

                    list_proc = FilterControl.ObjectDictionary["LIST_PROC"] as List<string>;
                    list_vitri = FilterControl.ObjectDictionary["LIST_VITRI"] as List<string>;
                    Load_Data = true;
                    var tLoadData = new Thread(TinhGiaAll);
                    tLoadData.Start();
                    timerViewReport.Start();
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(ex.Message);
            }
        }

        protected override void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                FilterControl.Call1(c_index);
                //FilterControl.Call2(c_message);
                ii = 0;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    if (Load_Data)
                    {
                        dataGridView1.SetFrozen(0);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = _tbl;

                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView2.AutoGenerateColumns = true;
                        //FormatGridViewF9();
                        //FormatGridViewBase();
                        var aldmConfig7 = ConfigManager.GetAldmConfig(c_proc);
                        FormatGridView1_byConfig(aldmConfig7);
                        //FormatGridViewExtern();
                        dataGridView1.Focus();
                    }
                    else
                    {
                        //Thong bao tinh toan xong
                        V6ControlFormHelper.ShowMessage(V6Text.Finish);
                    }
                    FilterControl.Call2(V6Text.Finish);
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();

                    _executesuccess = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;

                FilterControl.Call1(c_index);
                //FilterControl.Call2(c_message);

                if (c_index == list_proc.Count - 2 && FilterControl.Check1 && _editting6 && _editting60)
                {
                    _editting60 = false;
                    _aldmConfig6 = ConfigManager.GetAldmConfig(c_proc);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _tbl;
                    FormatGridView1_byConfig(_aldmConfig6);
                    //btnEditGrid.PerformClick();// AllowEdit(); // => xử lý sự kiện endedit=> update simple.
                    btnEditGrid.Enabled = false;
                    ApplyConfigExtra_GRD_ALLOW(_aldmConfig6);
                }
                else
                {

                }
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
            }
        }

        private void FormatGridView1_byConfig(AldmConfig config)
        {
            try
            {
                if (config.HaveInfo)
                {
                    dataGridView1.AutoGenerateColumns = true;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, config.GRDS_V1, config.GRDF_V1, config.GRDH_LANG_V1);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView6", ex);
            }
        }

        private List<string> list_proc, list_vitri;
        private int c_index;
        private AldmConfig _aldmConfig6 = null;
        private bool _editting6 = false;
        private bool _editting60 = false;
        string c_proc = "", c_vitri = "";
        public void TinhGiaAll()
        {
            try
            {
                _executing = true;
                _executesuccess = false;

                // Chay nhieu proc 1-5
                for (int i = 0; i < list_proc.Count-2; i++)
                {
                    c_index = i;
                    c_proc = list_proc[i];
                    c_vitri = list_vitri[i];

                    Call_TinhGia_ALL(c_vitri);
                    V6BusinessHelper.ExecuteProcedureNoneQuery(c_proc, _pList.ToArray());
                }

                // PROC 6
                c_proc = list_proc[list_proc.Count - 2];
                c_vitri = list_vitri[list_proc.Count - 2];
                c_index = list_proc.Count - 2;
                Call_TinhGia_ALL(c_vitri);
                _ds = V6BusinessHelper.ExecuteProcedure(c_proc, _pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
                }
                if (_ds.Tables.Count > 1)
                {
                    _tbl2 = _ds.Tables[1];
                    _tbl2.TableName = "DataTable2";
                }
                else
                {
                    _tbl2 = null;
                }
                
                _editting6 = true;
                _editting60 = true;
                while (FilterControl.Check1 && _editting6)
                {
                    DoNothing();
                }


                c_proc = list_proc[list_proc.Count - 1];
                c_vitri = list_vitri[list_proc.Count - 1];
                c_index = list_proc.Count - 1;
                Call_TinhGia_ALL(c_vitri);
                _ds = V6BusinessHelper.ExecuteProcedure(c_proc, _pList.ToArray());
                if (_ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    _tbl.TableName = "DataTable1";
                }
                if (_ds.Tables.Count > 1)
                {
                    _tbl2 = _ds.Tables[1];
                    _tbl2.TableName = "DataTable2";
                }
                else
                {
                    _tbl2 = null;
                }

                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _message = V6Text.Text("QUERY_FAILED") + "\n";
                if (ex.Message.StartsWith("Could not find stored procedure")) _message += V6Text.NotExist + ex.Message.Substring(31);
                else _message += ex.Message;

                this.ShowErrorMessage(GetType() + ".TinhGiaAll " + _message);
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _executesuccess = false;
            }
            _executing = false;
        }

        public void Call_TinhGia_ALL(string cvitri_description)
        {
            if (cvitri_description.StartsWith("AINGIA_TB:"))
            {
                string description = cvitri_description.Substring(cvitri_description.IndexOf(':') + 1);
                var temp_dic = ObjectAndString.StringToStringDictionary(description);
                string ma_kho = "";
                if (temp_dic.ContainsKey("MA_KHO"))
                {
                    ma_kho = temp_dic["MA_KHO"];
                }
                string ma_vt = "";
                if (temp_dic.ContainsKey("MA_VT"))
                {
                    ma_vt = temp_dic["MA_VT"];
                }
                int dk_cl = 0;
                if (temp_dic.ContainsKey("DK_CL"))
                {
                    dk_cl = ObjectAndString.ObjectToInt(temp_dic["DK_CL"]);
                }
                int tinh_giatb = 0;
                if (temp_dic.ContainsKey("TINH_GIATB"))
                {
                    tinh_giatb = ObjectAndString.ObjectToInt(temp_dic["TINH_GIATB"]);
                }
                string advance = "";
                if (temp_dic.ContainsKey("ADVANCE"))
                {
                    advance = temp_dic["ADVANCE"];
                }
                V6BusinessHelper.TinhGia_TB(FilterControl.Date1.Month, FilterControl.Date1.Year, FilterControl.Date2.Month, FilterControl.Date2.Year,
                    ma_kho, ma_vt, dk_cl, tinh_giatb, advance);
            }
            else if (cvitri_description.StartsWith("AINGIA_TBDD:"))
            {
                string description = cvitri_description.Substring(cvitri_description.IndexOf(':') + 1);
                var temp_dic = ObjectAndString.StringToStringDictionary(description);
                string ma_kho = "";
                if (temp_dic.ContainsKey("MA_KHO"))
                {
                    ma_kho = temp_dic["MA_KHO"];
                }
                string ma_vt = "";
                if (temp_dic.ContainsKey("MA_VT"))
                {
                    ma_vt = temp_dic["MA_VT"];
                }
                int dk_cl = 0;
                if (temp_dic.ContainsKey("DK_CL"))
                {
                    dk_cl = ObjectAndString.ObjectToInt(temp_dic["DK_CL"]);
                }
                int tinh_giatb = 0;
                if (temp_dic.ContainsKey("TINH_GIATB"))
                {
                    tinh_giatb = ObjectAndString.ObjectToInt(temp_dic["TINH_GIATB"]);
                }
                string advance = "";
                if (temp_dic.ContainsKey("ADVANCE"))
                {
                    advance = temp_dic["ADVANCE"];
                }
                V6BusinessHelper.TinhGia_TBDD(FilterControl.Date1.Month, FilterControl.Date1.Year, FilterControl.Date2.Month, FilterControl.Date2.Year,
                    ma_kho, ma_vt, dk_cl, tinh_giatb, advance);
            }
            else if (cvitri_description.StartsWith("AINGIA_NTXT:"))
            {
                string description = cvitri_description.Substring(cvitri_description.IndexOf(':') + 1);
                var temp_dic = ObjectAndString.StringToStringDictionary(description);
               
                string ma_vt = "";
                if (temp_dic.ContainsKey("MA_VT"))
                {
                    ma_vt = temp_dic["MA_VT"];
                }
                int warning = 0;
                if (temp_dic.ContainsKey("WARNING"))
                {
                    warning = ObjectAndString.ObjectToInt(temp_dic["WARNING"]);
                }
                int tinh_giatb = 0;
                if (temp_dic.ContainsKey("TINH_GIATB"))
                {
                    tinh_giatb = ObjectAndString.ObjectToInt(temp_dic["TINH_GIATB"]);
                }
                string advance = "";
                if (temp_dic.ContainsKey("ADVANCE"))
                {
                    advance = temp_dic["ADVANCE"];
                }
                V6BusinessHelper.TinhGia_NTXT(FilterControl.Date1.Month, FilterControl.Date1.Year, FilterControl.Date2.Month, ma_vt, warning, tinh_giatb, advance);
            }
        }
        

        #region ==== F3 ====

        private void XuLyF3()
        {
            
        }

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            if (FilterControl.String1 == "ACOSXLT_CODIEUCHINH0")
            {
                DoEdit_ACOSXLT_CODIEUCHINH0();
            }
            else
            {
                if (FilterControl.F3) base.XuLyHienThiFormSuaChungTuF3();
            }
        }

        private void XuLySuaF3()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLySuaF3", ex);
            }
        }

        private void DoEdit_ACOSXLT_CODIEUCHINH0()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var selected_uid = row.Cells["UID"].Value.ToString();
                    var currentRowData = row.ToDataDictionary();

                    var form = new ZACOSXLSX_TINHGIA_F3(V6Mode.Edit, selected_uid, currentRowData);
                    form.UpdateSuccessEvent += data =>
                    {
                        V6ControlFormHelper.UpdateGridViewRow(row, data);
                    };

                    form.ShowDialog(this);
                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEdit " + ex.Message);
            }
        }

        #endregion F3

        public override void dataGridView1_CellEndEdit_Virtual(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGridView1.Rows[e.RowIndex];
            var field = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            updateFieldList = new List<string>();
            GridView1CellEndEdit_Extern(row, field.ToUpper(), row.Cells[field].Value);
            // Mới thêm
            var EDIT_FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
            //Xu ly cong thuc tinh toan
            
            XuLyCongThucTinhToanAll(EDIT_FIELD, EDIT_FIELD, _aldmConfig6);

            if (_updateDatabase) UpdateData(e.RowIndex, e.ColumnIndex, _aldmConfig6);
        }

        public override void GridView1CellEndEdit_Extern(DataGridViewRow row, string FIELD, object fieldData)
        {
            return; // tạm thời bỏ qua. sử dụng công thức.
            IDictionary<string, object> data = new Dictionary<string, object>();
            data["DC_CK"] = "1";
            V6ControlFormHelper.UpdateGridViewRow(row, data);
            if (!updateFieldList.Contains("DC_CK")) updateFieldList.Add("DC_CK");
        }

        #region ==== F7 ====

        protected override void XuLyF7()
        {
            try
            {
                bool shift = (ModifierKeys & Keys.Shift) == Keys.Shift;
                //FilterControl.UpdateValues();
                var oldKeys = FilterControl.GetFilterParameters();
                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = true,
                    DisableBtnNhan = true,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = _ds.Copy(),
                    UseXtraReport = MenuButton.UseXtraReport,
                    Shift = shift,

                    Program = FilterControl.String1,
                    ReportProcedure = FilterControl.String1,
                    ReportFile = FilterControl.String1,
                    ReportCaption = "",
                    ReportCaption2 = "",
                    ReportFileF5 = "",
                    FilterControlInitFilters = oldKeys,
                    FilterControlString1 = FilterControl.String1,
                    FilterControlString2 = FilterControl.String2,
                    ParentRowData = dataGridView1.CurrentRow.ToDataDictionary(),
                    FormTitle = "XuLyF7",
                };
                QuickReportManager.ShowReportR(this, quick_params);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
            SetStatus2Text();
        }

        #endregion F7

        
        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                GenerateProcedureParameters();
                int check = V6BusinessHelper.CheckDataLocked("1", FilterControl.Date1, (int)FilterControl.Number1, (int)FilterControl.Number2);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                LockButtons();
                FilterControl.UpdateValues();
                Timer timerF9 = new Timer {Interval = 1000};
                timerF9.Tick += tF9_Tick;
                remove_list_g = new List<DataGridViewRow>();
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                t.IsBackground = true;
                t.Start();
                timerF9.Start();
                V6ControlFormHelper.SetStatusText("F9 running");
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        private string f9Message = "";
        private string f9MessageAll = "";
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";

                // Chay 1 proc
                c_proc = FilterControl.String1;
                if (FilterControl.ObjectDictionary != null && FilterControl.ObjectDictionary.ContainsKey("C_VITRI"))
                {
                    c_vitri = FilterControl.ObjectDictionary["C_VITRI"].ToString();
                }
                else
                {
                    c_vitri = ":";
                }
                Call_TinhGia_ALL(c_vitri);
                _ds = V6BusinessHelper.ExecuteProcedure(c_proc, _pList.ToArray());
                _tbl = null;
                _tbl2 = null;
                
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    _tbl = _ds.Tables[0];
                    if (_ds.Tables.Count > 1)
                    {
                        _tbl2 = _ds.Tables[1];
                    }
                }

            }
            catch (Exception ex)
            {
                f9Message = "F9Thread: " + ex.Message;
            }
            
            f9Running = false;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                //RemoveGridViewRow();

                var cError = f9Message;
                if (cError.Length > 0)
                {
                    f9Message = f9Message.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText(cError);
                    V6ControlFormHelper.ShowMainMessage("F9 running: " + cError);
                }
            }
            else
            {
                ((Timer)sender).Stop();
                UnlockButtons();
                dataGridView1.SetFrozen(0);
                //RemoveGridViewRow();
                dataGridView1.DataSource = _tbl;
                //FormatGridViewF9();
                var aldmConfig9 = ConfigManager.GetAldmConfig(c_proc);
                FormatGridView1_byConfig(aldmConfig9);
                //btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish " + f9Message);
                V6ControlFormHelper.ShowMainMessage("F9 finish! " + f9Message);
                V6ControlFormHelper.ShowInfoMessage("F9 finish: " + f9MessageAll, 500, this);
                if (f9MessageAll.Length > 0)
                {
                    this.WriteToLog(GetType() + ".F9Message ", f9MessageAll);
                }
                f9Message = "";
                f9MessageAll = "";
            }
        }

        protected void FormatGridViewF9()
        {
            try
            {
                //Header
                if (_tbl != null)
                {
                    var fieldList = (from DataColumn column in _tbl.Columns select column.ColumnName).ToList();

                    var fieldDic = CorpLan2.GetFieldsHeader(fieldList);
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        if (fieldDic.ContainsKey(dataGridView1.Columns[i].DataPropertyName.ToUpper()))
                        {
                            dataGridView1.Columns[i].HeaderText =
                                fieldDic[dataGridView1.Columns[i].DataPropertyName.ToUpper()];
                        }
                    }
                    //Format
                    var f = dataGridView1.Columns["so_luong"];
                    if (f != null)
                    {
                        f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_SL");
                    }
                    f = dataGridView1.Columns["TIEN2"];
                    if (f != null)
                    {
                        f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_TIEN");
                    }
                    f = dataGridView1.Columns["GIA2"];
                    if (f != null)
                    {
                        f.DefaultCellStyle.Format = V6Options.GetValue("M_IP_R_GIA");
                    }
                }

                //Format mới
                var mauin = Albc.GetMauInData(FilterControl.String1, "", "", "");
                if (mauin != null && mauin.Rows.Count > 0)
                {
                    var mauin_row = mauin.Rows[0];
                    dataGridView1.AutoGenerateColumns = true;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1,
                        mauin_row["GRDS_V1"].ToString().Trim(),
                        mauin_row["GRDF_V1"].ToString().Trim(),
                        V6Setting.IsVietnamese
                            ? mauin_row["GRDHV_V1"].ToString().Trim()
                            : mauin_row["GRDHE_V1"].ToString().Trim());
                    if (ViewDetail)
                    {
                        dataGridView2.AutoGenerateColumns = true;
                        V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2,
                            mauin_row["GRDS_V2"].ToString().Trim(),
                            mauin_row["GRDF_V2"].ToString().Trim(),
                            V6Setting.IsVietnamese
                                ? mauin_row["GRDHV_V2"].ToString().Trim()
                                : mauin_row["GRDHE_V2"].ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".FormatGridViewF9", ex);
            }
        }
        #endregion xử lý F9


        protected override void XuLyF10()
        {
            _editting6 = false; // chạy tiếp.
        }
    }
}
