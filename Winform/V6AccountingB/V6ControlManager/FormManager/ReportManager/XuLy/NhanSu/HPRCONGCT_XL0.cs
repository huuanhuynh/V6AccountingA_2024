using System;
using System.Data;
using System.Data.SqlClient;
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
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class HPRCONGCT_XL0 : XuLyBase0
    {
        public HPRCONGCT_XL0()
        {
            InitializeComponent();
        }

        public HPRCONGCT_XL0(string itemId, string program, string reportProcedure, string reportFile, string text)
            : base(itemId, program, reportProcedure, reportFile, text, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dateNgay_ct1.SetValue(dateNgay_ct1.Date.AddMonths(-1));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("Kiểm tra số liệu. F3 sửa");
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

                tabControl1.TabPages.Clear();

                Control.CheckForIllegalCrossThreadCalls = false;
                Thread T = new Thread(ExecProc);
                T.IsBackground = true;
                T.Start();

                Timer timerRunAll = new Timer();
                timerRunAll.Interval = 100;
                timerRunAll.Tick += timerRunAll_Tick;
                _success = false;
                _executing = true;
                timerRunAll.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Nhan: " + ex.Message);
            }
        }

        private string _error = "";
        private void ExecProc()
        {
            try
            {
                _message = "";
                SqlParameter[] plist =
                {
                    new SqlParameter("Ngay_ct1", dateNgay_ct1.Date), 
                    new SqlParameter("Ngay_ct2", dateNgay_ct2.Date), 
                };
                //_ds = V6BusinessHelper.ExecuteProcedure("HPRCONGCT_XL0", plist);
                _ds = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.StoredProcedure,
                    "HPRCONGCT_XL0", 600, plist);

                _executing = false;
                _success = true;
            }
            catch (Exception ex)
            {
                _error = _message + " " + ex.Message;
                _success = false;
                _executing = false;
            }
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_success)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    ViewDataSet();
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage("HPRCONGCT_XL0: Tải dữ liệu xong!\r\n" + _message);

                    _success = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _success = false;
                    this.ShowErrorMessage(GetType() + ".TimerView" + ex.Message, ex.Source);
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
            grid.Dock = DockStyle.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;

            tab.Controls.Add(grid);
            grid.DataSource = table;
            tabControl1.TabPages.Add(tab);
            grid.KeyDown += grid_KeyDown;
        }

        void grid_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null && grid.CurrentRow != null)
            {
                if (e.KeyCode == Keys.F3)
                {
                    var selectedMaCt = grid.CurrentRow.Cells["Ma_ct"].Value.ToString();
                    var selectedSttRec = grid.CurrentRow.Cells["Stt_rec"].Value.ToString();
                    if (selectedMaCt == "INF")// phiếu nhập điều chuyển
                    {
                        selectedMaCt = "IXB"; // phiếu xuất điều chuyển
                        selectedSttRec = selectedSttRec.Left(10) + selectedMaCt;
                    }

                    var alctRow = V6BusinessHelper.Select("Alct", "ten_ct,ten_ct2,m_phdbf,m_ctdbf",
                                "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
                    var amName = alctRow["m_phdbf"].ToString().Trim();
                    var adName = alctRow["m_ctdbf"].ToString().Trim();
                    var fText = (alctRow[V6Setting.IsVietnamese ? "ten_ct" : "ten_ct2"] ?? "").ToString().Trim();
                    if (amName != "" && adName != "")
                    {
                        var f = new V6Form
                        {
                            WindowState = FormWindowState.Maximized,
                            Text = fText
                        };

                        var hoaDonForm = ChungTuF3.GetChungTuControl(selectedMaCt, Name, selectedSttRec);
                        hoaDonForm.Dock = DockStyle.Fill;
                        f.Controls.Add(hoaDonForm);

                        f.ShowDialog(this);
                    }
                }
            }
        }
    }
}
