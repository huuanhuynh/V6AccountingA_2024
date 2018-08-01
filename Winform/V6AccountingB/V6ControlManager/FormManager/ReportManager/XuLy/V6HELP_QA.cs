using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6HELP_QA : XuLyBase0
    {
        public V6HELP_QA()
        {
            InitializeComponent();
        }

        public V6HELP_QA(string itemId, string program, string reportProcedure, string reportFile, string text)
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
                this.ShowErrorMessage(GetType() + ".Init: " + ex.Message);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F2 xem, F3 sửa, F4 thêm, F8 xóa.");
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

                dataGridView1.DataSource = null;

                CheckForIllegalCrossThreadCalls = false;
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
                this.ShowErrorMessage(GetType() + ".Nhan " + ex.Message);
            }
        }

        private string _error = "";
        private void ExecProc()
        {
            try
            {
                _message = "";

                var advance = "";
                var and_or = radAnd.Checked ? "and " : "or  ";
                foreach (Control control in groupBox1.Controls)
                {
                    var lineControl = control as FilterLineBase;
                    if (lineControl != null && lineControl.IsSelected)
                    {
                        advance += and_or + lineControl.Query;
                    }
                }
                if (advance.Length > 4) advance = advance.Substring(4);
                SqlParameter[] plist =
                {
                    new SqlParameter("Ngay_ct1", dateNgay_ct1.YYYYMMDD),
                    new SqlParameter("Ngay_ct2", dateNgay_ct2.YYYYMMDD),
                    new SqlParameter("@Advance", advance),
                };
                _ds = V6BusinessHelper.ExecuteProcedure("V6HELP_QA1", plist);
                //_ds = SqlHelper.ExecuteDataset(DatabaseConfig.ConnectionString, CommandType.StoredProcedure,
                //    "V6HELP_QA", 600, plist);

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
                    ViewData();
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage("Tải dữ liệu xong!\r\n" + _message);

                    _success = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _success = false;
                    this.ShowErrorMessage(GetType() + ".TimerView " + ex.Message, ex.Source);
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

        private void ViewData()
        {
            try
            {
                if (_ds == null || _ds.Tables.Count == 0) return;
                dataGridView1.DataSource = _ds.Tables[0];
                ViewDetails(dataGridView1.CurrentRow);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                if (row == null)
                {
                    richTextBox1.Clear();
                    richTextBox2.Clear();
                    richTextBox3.Clear();
                    return;
                }

                richTextBox1.Text = row.Cells["YEU_CAU"].Value.ToString().Trim();
                richTextBox2.Text = row.Cells["GIAI_PHAP"].Value.ToString().Trim();
                richTextBox3.Text = row.Cells["GHI_CHU"].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewDetails " + ex.Message, "V6ControlManager");
            }
        }

        //private V6TableName CurrentTable = V6TableName.V6help_qa;
        //private string[] KeyFields = new string[]{"KHOA_HELP"};
        private void DoView()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var selected_khoa = row.Cells["KHOA_HELP"].Value.ToString();
                    var currentRowData = row.ToDataDictionary();

                    var form = new V6HELP_QA_F3F4(V6Mode.View, selected_khoa, currentRowData);
                    form.UpdateSuccessEvent += data =>
                    {
                        V6ControlFormHelper.UpdateGridViewRow(row, data);
                    };

                    form.ShowDialog(this);
                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoView " + ex.Message);
            }
        }

        private void DoEdit()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var selected_khoa = row.Cells["KHOA_HELP"].Value.ToString();
                    var currentRowData = row.ToDataDictionary();

                    var form = new V6HELP_QA_F3F4(V6Mode.Edit, selected_khoa, currentRowData);
                    form.UpdateSuccessEvent += data =>
                    {
                        V6ControlFormHelper.UpdateGridViewRow(row, data);
                    };

                    form.ShowDialog(this);
                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEdit " + ex.Message);
            }
        }

        private void DoDelete()
        {
            try
            {
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    var selected_khoa = row.Cells["KHOA_HELP"].Value.ToString();
                    var currentRowData = row.ToDataDictionary();
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys.Add("KHOA_HELP", selected_khoa);
                    var delete = V6BusinessHelper.Delete("V6HELP_QA", keys);
                    if (delete > 0)
                    {
                        V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                    }
                    btnNhan.PerformClick();
                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoDelete " + ex.Message);
            }
        }
        
        private void DoAdd()
        {
            try
            {
                if (!V6Login.UserRight.AllowAdd("", "S09"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                var currentRowData = new SortedDictionary<string, object>();


                var new_like_stt_rec = V6BusinessHelper.GetNewLikeSttRec("S09", "KHOA_HELP", "M");
                currentRowData["KHOA_HELP"] = new_like_stt_rec;
                
                var form = new V6HELP_QA_F3F4(V6Mode.Add, new_like_stt_rec, currentRowData);
                form.InsertSuccessEvent += data =>
                {
                    
                };

                form.ShowDialog(this);
                SetStatus2Text();
                btnNhan.PerformClick();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoAdd " + ex.Message);
            }
        }

        public override void DoHotKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                DoView();
                return;
            }
            if (keyData == Keys.F3)
            {
                DoEdit();
                return;
            }
            if (keyData == Keys.F4)
            {
                DoAdd();
                return;
            }
            if (keyData == Keys.F8)
            {
                DoDelete();
                return;
            }
            base.DoHotKey(keyData);
        }


        void grid_KeyDown(object sender, KeyEventArgs e)
        {
            //DataGridView grid = sender as DataGridView;
            ////if (grid != null && grid.CurrentRow != null)
            //{
            //    if (e.KeyCode == Keys.F3)
            //    {
            //        DoEdit();
            //    }
            //    else if (e.KeyCode == Keys.F4)
            //    {
            //        DoAdd();
            //    }
            //}
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ViewDetails(dataGridView1.CurrentRow);
        }

    }
}
