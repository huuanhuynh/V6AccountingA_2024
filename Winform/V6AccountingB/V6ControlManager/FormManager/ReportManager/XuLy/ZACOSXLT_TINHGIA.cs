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
    public class ZACOSXLT_TINHGIA : XuLyBase
    {

        public ZACOSXLT_TINHGIA(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            dataGridView1.KeyDown += dataGridView1_KeyDown;
        }

        void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                XuLyF3();
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Tính");
        }


        protected override void MakeReport2()
        {
            try
            {
                if (GenerateProcedureParameters())
                {
                    proc_list = ObjectAndString.SplitString(FilterControl.String2);
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
            if (_dataLoaded)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                FilterControl.Call1(c_index);
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
                        FormatGridViewF9();
                        //FormatGridViewBase();
                        FormatGridViewExtern();
                        dataGridView1.Focus();
                    }
                    else
                    {
                        //Thong bao tinh toan xong
                        V6ControlFormHelper.ShowMessage(V6Text.Finish);
                    }
                    _dataLoaded = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();

                    _dataLoaded = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
                }
            }
            else if (_dataLoading)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;

                FilterControl.Call1(c_index);
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
            }
        }

        private string[] proc_list;
        private int c_index;
        public void TinhGiaAll()
        {
            try
            {
                _dataLoading = true;
                _dataLoaded = false;

                // Chay nhieu proc
                string c_proc;
                for (int i = 0; i < proc_list.Length-1; i++)
                {
                    c_proc = proc_list[i];
                    c_index = i;
                    V6BusinessHelper.ExecuteProcedureNoneQuery(c_proc, _pList.ToArray());
                }

                c_proc = proc_list[proc_list.Length - 1];
                c_index = proc_list.Length - 1;
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

                _dataLoaded = true;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Query Error!\n" + ex.Message);
                _tbl = null;
                _tbl2 = null;
                _ds = null;
                _dataLoaded = false;
            }
            _dataLoading = false;
        }

        #region ==== F3 ====

        private void XuLyF3()
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

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            //Bo qua ham goc. Chay ham XuLyF3
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

                    var form = new ZACOSXLT_TINHGIA_F3(V6Mode.Edit, selected_uid, currentRowData);
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

        #region ==== F7 ====

        protected override void XuLyF7()
        {
            try
            {
                //FilterControl.UpdateValues();
                var oldKeys = FilterControl.GetFilterParameters();
                QuickReportParams quick_params = new QuickReportParams()
                {
                    AutoRun = true,
                    ItemID = m_itemId,
                    CodeForm = CodeForm,
                    DataSet = _ds.Copy(),

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
        }

        #endregion F7

        
        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                GenerateProcedureParameters();
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
                string c_proc = FilterControl.String1;
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
                FormatGridViewF9();
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


    }
}
