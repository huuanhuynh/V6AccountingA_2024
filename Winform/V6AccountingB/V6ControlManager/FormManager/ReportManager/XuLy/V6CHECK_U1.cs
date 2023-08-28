using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6CHECK_U1 : XuLyBase0
    {
        public V6CHECK_U1()
        {
            InitializeComponent();
        }

        public V6CHECK_U1(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgay_ct1.SetValue(dateNgay_ct1.Date.AddMonths(-1));
                LoadCombobox();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Init", ex);
            }
        }

        public override void SetStatus2Text()
        {
            string id = "ST2" + _reportProcedure;
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = "Kiểm tra số liệu. F3 sửa.";
            }

            V6ControlFormHelper.SetStatusText2(text, id);
        }

        private void LoadCombobox()
        {
            try
            {
                cboProcList.ValueMember = "proc";
                cboProcList.DisplayMember = V6Setting.IsVietnamese ? "ten" : "ten2";
                cboProcList.DataSource = V6BusinessHelper.Select("V6Check_Data", "*", "", "", "STT").Data;
                cboProcList.ValueMember = "proc";
                cboProcList.DisplayMember = V6Setting.IsVietnamese ? "ten" : "ten2";
                cboProcList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        
        protected override void Nhan()
        {
            try
            {
                if (_executing)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                    return;
                }

                Lock();
                tabControl1.TabPages.Clear();

                CheckForIllegalCrossThreadCalls = false;
                Thread T = new Thread(ExecProc);
                T.IsBackground = true;
                T.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 100;
                timerRunAll.Tick += timerRunAll_Tick;
                _executesuccess = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Nhan", ex);
            }
        }

        private string _error = "";
        private string _selectedProc = "";
        private void ExecProc()
        {
            try
            {
                _message = "";
                if (string.IsNullOrEmpty(_selectedProc))
                {
                    throw new Exception("Empty Proc.");
                }
                SqlParameter[] plist =
                {
                    new SqlParameter("Ngay_ct1", dateNgay_ct1.Date), 
                    new SqlParameter("Ngay_ct2", dateNgay_ct2.Date), 
                };
                //_ds = V6BusinessHelper.ExecuteProcedure("V6CHECK_U1", plist);
                _ds = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.StoredProcedure,
                    _selectedProc, 600, plist);

                _executing = false;
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _executesuccess = false;
                _executing = false;
            }
        }

        private void Lock()
        {
            dateNgay_ct1.ReadOnly = true;
            dateNgay_ct2.ReadOnly = true;
            cboProcList.Enabled = false;
        }

        private void UnLock()
        {
            dateNgay_ct1.ReadOnly = false;
            dateNgay_ct2.ReadOnly = false;
            cboProcList.Enabled = true;
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                UnLock();
                try
                {
                    ViewDataSet();
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage(lblTen.Text + " " + V6Text.Finish + " " + _message);

                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _executesuccess = false;
                    this.ShowErrorException(GetType() + ".Timer_Success", ex);
                }
            }
            else if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii];
                ii++;
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                this.ShowErrorMessage(_error);
                UnLock();
            }
        }

        private void ViewDataSet()
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0) return;
                for (int i = 0; i < _ds.Tables.Count; i++)
                {
                    var table = _ds.Tables[i];
                    //if (string.IsNullOrEmpty(table.TableName))
                    table.TableName = "Data" + (1+i);
                    AddTab(table);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void AddTab(DataTable table)
        {
            TabPage tab = new TabPage(table.TableName);
            var grid = new V6ColorDataGridView();
            grid.Name = "dataGridView1";
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            tab.Controls.Add(grid);
            grid.DataSource = table;
            tabControl1.TabPages.Add(tab);
            
            grid.HideColumns("STT_REC");
            V6ControlFormHelper.SetGridViewCaption(grid);
            
            grid.KeyDown += grid_KeyDown;
        }

        void grid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null && grid.CurrentRow != null)
            {
                if (e.KeyCode == Keys.F3 && grid.Columns.Contains("Ma_ct") && grid.Columns.Contains("Stt_rec"))
                {
                    var selectedMaCt = grid.CurrentRow.Cells["Ma_ct"].Value.ToString();
                    var selectedSttRec = grid.CurrentRow.Cells["Stt_rec"].Value.ToString();
                    if (selectedMaCt == "INF")// phiếu nhập điều chuyển
                    {
                        selectedMaCt = "IXB"; // phiếu xuất điều chuyển
                        selectedSttRec = selectedSttRec.Left(10) + selectedMaCt;
                    }

                    AlctConfig alctConfig = ConfigManager.GetAlctConfig(selectedMaCt);
                    if (alctConfig.TableNameAM != "" && alctConfig.TableNameAD != "")
                    {
                        var hoaDonForm = ChungTuF3.GetChungTuControl(selectedMaCt, Name, selectedSttRec);
                        if (V6Options.M_SUA_BC == "1")
                        {
                            hoaDonForm.ClickSuaOnLoad = true;
                        }
                        hoaDonForm.ShowToForm(this, V6Setting.IsVietnamese ? alctConfig.TEN_CT : alctConfig.TEN_CT2, true);
                        SetStatus2Text();
                    }
                }
            }
        }

        private void cboProcList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _selectedProc = cboProcList.SelectedValue.ToString().Trim();
                lblTen.Text = cboProcList.Text;
            }
            catch (Exception ex)
            {
                _selectedProc = null;
                this.WriteExLog(GetType() + ".cboProcList_Select", ex);
            }
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    var selectedGrid = V6ControlFormHelper.GetControlByName(tabControl1.SelectedTab, "dataGridView1")
                        as V6ColorDataGridView;
                    if (selectedGrid != null) selectedGrid.ViewDataToNewForm();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".viewDataToolStripMenuItem_Click", ex);
            }
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedGrid = V6ControlFormHelper.GetControlByName(tabControl1.SelectedTab, "dataGridView1")
                       as V6ColorDataGridView;
                if (selectedGrid != null)
                {
                    SaveFileDialog o = new SaveFileDialog();
                    o.Filter = "All|*.*|Excel|*.xls;*.xlsx|Xml|*.xml";
                    if (o.ShowDialog(this) == DialogResult.OK)
                    {
                        var setting = new ExportExcelSetting();
                        setting.data = selectedGrid.DataSource as DataTable;
                        setting.saveFile = o.FileName;
                        ExportData.ToExcel(setting);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".exportExcelToolStripMenuItem_Click", ex);
            }
        }
    }
}
