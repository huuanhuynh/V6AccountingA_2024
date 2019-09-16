using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AINVITRI03_REPORT : XuLyBase0
    {
        public AINVITRI03_REPORT()
        {
            InitializeComponent();
        }

        public AINVITRI03_REPORT(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                //dateNgay_ct1.SetValue(dateNgay_ct1.Value.AddMonths(-1));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6COPY_RA Init: " + ex.Message);
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
                _executesuccess = false;
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
                    new SqlParameter("@Nam", dateYear.Date.Year),
                    new SqlParameter("@Thang", dateMonth.Date.Month),
                    new SqlParameter("@Ma_kh", txtMaKh.Text),
                    new SqlParameter("@Advance", advance),
                };
                _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, plist);

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


        private IDictionary<string, object> plistData;
        public override void SetData(IDictionary<string, object> d)
        {
            try
            {
                plistData = d;

                //{Tuanmh 07/06/2017
                if (d.ContainsKey("MA_VITRI"))
                {
                    var mavitri = d["MA_VITRI"].ToString().Trim();
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("MA_VITRI", mavitri);
                    var alvitri = V6BusinessHelper.Select(V6TableName.Alvitri, keys, "*").Data;
                    if (alvitri.Rows.Count == 1)
                    {
                        var makh = alvitri.Rows[0]["MA_KH"];
                        if (makh != null && makh.ToString().Trim() != "")
                        {
                            if (d.ContainsKey("MA_KH"))
                            {
                                txtMaKh.Text = makh.ToString().Trim();
                            }
                        }
                    }
                   
                }

                //if (d.ContainsKey("MA_KH")) txtMaKh.Text = d["MA_KH"].ToString().Trim();
                //}

                if (d.ContainsKey("CUOI_NGAY"))
                {
                    DateTime date = ObjectAndString.ObjectToFullDateTime(d["CUOI_NGAY"]);
                    dateYear.SetValue(date);
                    dateMonth.SetValue(date);
                }
                if (d.ContainsKey("MA_KHO"))
                {
                    lineMaKho.VvarTextBox.Text = (d["MA_KHO"]??"").ToString().Trim();

                   IDictionary<string, object> keys = new Dictionary<string, object>();
                   keys.Add("MA_KHO", lineMaKho.StringValue);
                    var alkho = V6BusinessHelper.Select(V6TableName.Alkho, keys, "*").Data;
                    if (alkho.Rows.Count == 1)
                    {
                        var madvcs = alkho.Rows[0]["MA_DVCS"];
                        if (madvcs != null && madvcs.ToString().Trim() != "")
                        {
                            txtma_dvcs.VvarTextBox.Text = madvcs.ToString().Trim();
                        }
                    }

                }
                if (d.ContainsKey("MA_VITRI")) lineMaVitri.VvarTextBox.Text = (d["MA_VITRI"]??"").ToString().Trim();
                if (d.ContainsKey("MA_VT")) lineMaVatTu.VvarTextBox.Text = (d["MA_VT"]??"").ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SetData " + ex.Message);
            }
        }

        private void timerRunAll_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                ((Timer)sender).Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    ViewData();
                    DoAfterExecuteSuccess();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Text("TAIDULIEUXONG") + "!\r\n" + _message);

                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    ((Timer)sender).Stop();
                    _executesuccess = false;
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
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                //if (row == null)
                //{
                //    richTextBox1.Clear();
                //    richTextBox2.Clear();
                //    richTextBox3.Clear();
                //    return;
                //}

                //richTextBox1.Text = row.Cells["YEU_CAU"].Value.ToString().Trim();
                //richTextBox2.Text = row.Cells["GIAI_PHAP"].Value.ToString().Trim();
                //richTextBox3.Text = row.Cells["GHI_CHU"].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".V6HELP_QA ViewDetails " + ex.Message, "V6ControlManager");
            }
        }

        //private V6TableName CurrentTable = V6TableName.V6help_qa;
        //private string[] KeyFields = new string[]{"KHOA_HELP"};
        private void DoView()
        {
            try
            {
                if (!V6Login.UserRight.AllowView("", "ABNGHI6"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var currentRowData = row.ToDataDictionary();
                    
                    var f = new FormAddEdit(V6TableName.Abnghi, V6Mode.View, null, currentRowData);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.ShowDialog(this);

                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void DoEdit()
        {
            try
            {
                if (!V6Login.UserRight.AllowEdit("", "ABNGHI6"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row != null)
                {
                    var currentRowData = row.ToDataDictionary();
                    
                    var f = new FormAddEdit(V6TableName.Abnghi, V6Mode.Edit, null, currentRowData);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.UpdateSuccessEvent += data =>
                    {
                        V6ControlFormHelper.UpdateGridViewRow(row, data);
                    };
                    f.ShowDialog(this);

                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void DoDelete()
        {
            try
            {
                if (!V6Login.UserRight.AllowDelete("", "ABNGHI6"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    //var selected_khoa = row.Cells["KHOA_HELP"].Value.ToString();
                    var currentRowData = row.ToDataDictionary();
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys.Add("UID", currentRowData["UID"]);
                    var delete = V6BusinessHelper.Delete("ABNGHI", keys);
                    if (delete > 0)
                    {
                        V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                    }
                    btnNhan.PerformClick();
                    SetStatus2Text();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }
        
        private void DoAdd()
        {
            try
            {
                if (!V6Login.UserRight.AllowAdd("", "ABNGHI6"))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                IDictionary<string, object> currentRowData = new SortedDictionary<string, object>();

                currentRowData["MA_KH"] = txtMaKh.Text;
                currentRowData["NAM"] = dateYear.Date.Year;
                currentRowData["THANG"] = dateMonth.Date.Month;
                currentRowData["MA_KHO"] = lineMaKho.StringValue;
                currentRowData["MA_VITRI"] = lineMaVitri.StringValue;
                currentRowData["MA_VT"] = lineMaVatTu.StringValue;
                currentRowData["MA_DVCS"] = txtma_dvcs.StringValue;
                
                var f = new FormAddEdit(V6TableName.Abnghi, V6Mode.Add, null, currentRowData);
                f.AfterInitControl += f_AfterInitControl;
                f.InitFormControl();
                f.InsertSuccessEvent += data =>
                {
                    ;
                };
                f.ShowDialog(this);

                btnNhan.PerformClick();

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoAdd:\n" + ex.Message);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Abnghi");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void DoPrint()
        {
            try
            {
                var f = new ReportRViewBase(m_itemId, _program + "BF7", _program + "BF7", _reportFile + "BF7", "caption", "2", "", "", "");
                f.FilterControl.SetData(plistData);
                f.btnNhan_Click(null, null);
                f.ShowToForm(this, GetType() + "_F7");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoPrint: " + ex.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                DoView();
                return true;
            }
            if (keyData == Keys.F3)
            {
                DoEdit();
                return true;
            }
            if (keyData == Keys.F4)
            {
                DoAdd();
                return true;
            }
            if (keyData == Keys.F7)
            {
                //this.ShowMessage("DoPrint");
                DoPrint();
                return true;
            }
            if (keyData == Keys.F8)
            {
                DoDelete();
                return true;
            }
            
            return base.DoHotKey0(keyData);
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

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            ViewDetails(dataGridView1.CurrentRow);
        }

    }
}
