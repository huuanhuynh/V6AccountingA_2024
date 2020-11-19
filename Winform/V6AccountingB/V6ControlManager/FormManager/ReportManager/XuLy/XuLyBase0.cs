using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.Filter.Sms;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class XuLyBase0 : V6FormControl
    {
        #region Biến toàn cục
        
        protected string _reportProcedure, _reportFile;
        protected string _program, _reportCaption, _reportCaption2;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        public DataTable MauInData;
        public DataRow MauInSelectedRow
        {
            get
            {
                return MauInData == null || MauInData.Rows.Count == 0 ? null : MauInData.Rows[0];
            }
        }
        protected DataTable _tblGridView2;
        //private V6TableStruct _tStruct;

        /// <summary>
        /// Danh sách event_method của Form_program.
        /// </summary>
        private Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        private Type Form_program;
        protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();

        /// <summary>
        /// Gọi hàm động trong Event_Methods theo tên Event trên form.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        protected object InvokeFormEvent(string eventName)
        {
            try // Dynamic invoke
            {
                if (Event_Methods.ContainsKey(eventName))
                {
                    V6ControlFormHelper.SetStatusText("InvokeFormEvent:" + eventName);
                    var method_name = Event_Methods[eventName];
                    return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
                }
                else
                {
                    V6ControlFormHelper.SetStatusText("InvokeFormEvent:" + eventName + "(No code)");
                }
            }
            catch (Exception ex1)
            {
                this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
            }
            return null;
        }

        private void CreateFormProgram()
        {
            try
            {
                IDictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("MA_FILE", _program);
                var AlreportData = V6BusinessHelper.Select(V6TableName.Albc, keys, "*").Data;
                if (AlreportData.Rows.Count == 0) return;

                var dataRow = AlreportData.Rows[0];
                var xml = dataRow["MMETHOD"].ToString().Trim();
                if (xml == "") return;
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                if (ds.Tables.Count <= 0) return;

                var data = ds.Tables[0];

                string using_text = "";
                string method_text = "";
                foreach (DataRow event_row in data.Rows)
                {
                    var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                    var method_name = event_row["method"].ToString().Trim();
                    Event_Methods[EVENT_NAME] = method_name;

                    using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                    method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                }
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "M" + _program, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 
        public XuLyBase0()
        {
            InitializeComponent();
            //MyInit();
        }

        public XuLyBase0(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2, bool viewDetail = false)
        {
            m_itemId = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _reportFile = reportFile;
            _reportCaption = reportCaption;
            _reportCaption2 = reportCaption2;
            ViewDetail = viewDetail;
            
            InitializeComponent();
            BaseInit();
        }

        private void BaseInit()
        {
            Text = _reportCaption;
            All_Objects["thisForm"] = this;
            CreateFormProgram();
            AddFilterControl(_program);
            LoadComboboxSource();
            InvokeFormEvent(FormDynamicEvent.INIT);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadDefaultData(4, "", _program, m_itemId, "");
            FixFilterControlSize();
        }

        
        public FilterBase FilterControl { get; set; }
        protected void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program, _reportProcedure);
            FilterControl.Height = panel1.Height - 5;
            panel1.Controls.Add(FilterControl);
            panel1.SizeChanged += panel1_SizeChanged;
            FilterControl.Focus();
        }

        private void FixFilterControlSize()
        {
            try
            {
                if (FilterControl is XASENDSMS || FilterControl is XASENDMAIL || FilterControl is XASENDSMSALL || FilterControl is XASENDEMAILALL)
                {
                    panel1.Width = grbDieuKienLoc.Width - 5;
                    panel1.Height = grbDieuKienLoc.Height - 5;
                    panel1.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
                    FilterControl.Width = panel1.Width - 5;
                    FilterControl.Height = panel1.Height - 5;
                    FilterControl.Anchor |= AnchorStyles.Right | AnchorStyles.Bottom;
                }
            }
            catch (Exception)
            {

            }
        }

        protected void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_reportFile, "", "", "");
        }

        void panel1_SizeChanged(object sender, EventArgs e)
        {
            FilterControl.Height = panel1.Height - 5;
            //FixFilterControlSize();
        }
        
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                return;
            }

            try
            {
                btnNhanImage = btnNhan.Image;
                Nhan();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExecuteError\n" + ex.Message);
            }
        }

        protected bool GenerateProcedureParameters()
        {
            try
            {
                _pList = new List<SqlParameter>();
                _pList.AddRange(FilterControl.GetFilterParameters());
                //_pList.Add(new SqlParameter("@cKey", "1=1" + (sKey.Length>0?" And " +sKey:"")));
                return true;
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("GenerateProcedureParameters: " + ex.Message);
                return false;
            }
        }

        #region ==== Execute ====

        protected virtual void TinhToan()
        {
            try
            {
                _executing = true;
                if (FilterControl.ExecuteMode == ExecuteMode.ExecuteProcedure)
                {
                    _ds = V6BusinessHelper.ExecuteProcedure(_reportProcedure, _pList.ToArray());
                }
                else
                {
                    //V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, _pList.ToArray());
                    SqlHelper.ExecuteNonQuery(DatabaseConfig.ConnectionString, CommandType.StoredProcedure,
                        _reportProcedure, 600, _pList.ToArray());
                }
                _executesuccess = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhToan", ex);
                _message = ex.Message;
                _executesuccess = false;
            }
            _executing = false;
        }

        protected virtual void Nhan()
        {
            _message = string.Empty;
            All_Objects["_plist"] = _pList;
            object beforeLoadData = InvokeFormEvent(FormDynamicEvent.BEFORELOADDATA);
            if (beforeLoadData != null && !(bool)beforeLoadData)
            {
                _message = V6Text.CheckInfor;
                SetStatusText(_message);
                _executing = false;
                return;
            }
            ExecuteProcedure();
        }

        /// <summary>
        /// GenerateProcedureParameters();//Add các key
        /// </summary>
        protected virtual void ExecuteProcedure()
        {
            if (GenerateProcedureParameters()) //Add các key khác
            {
                // Tinh toan
                var tTinhToan = new Thread(TinhToan);
                tTinhToan.Start();
                timerViewReport.Start();
            }
        }

        protected virtual void DoAfterExecuteSuccess()
        {
            
        }

        
        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executesuccess)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    All_Objects["_ds"] = _ds;
                    All_Objects["_tbl"] = _tbl;
                    All_Objects["_tbl2"] = _tbl2;
                    InvokeFormEvent(FormDynamicEvent.AFTERLOADDATA);
                    //Chỉ kích hoạt hàm FormatGridView
                    FilterControl.FormatGridView(null);
                    DoAfterExecuteSuccess();
                    _message = V6Text.Finish;
                    V6ControlFormHelper.SetStatusText(_message);
                    if (FilterControl.ExecuteMode != ExecuteMode.ExecuteProcedure)
                    {
                        ShowMainMessage(_message);
                    }
                    _executesuccess = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();
                    _executesuccess = false;
                    _message = ex.Message;
                    V6ControlFormHelper.SetStatusText(_message);
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
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                _message = V6Text.Busy;
            }
            V6ControlFormHelper.SetStatusText(_message);
        }
        
        #endregion ==== execute ====
        
        

        #region Linh tinh        

        public bool IsRunning
        {
            get { return _executing; }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                ShowMainMessage(V6Text.ProcessNotComplete);
                return;
            }
            Huy();
        }

        protected virtual void Huy()
        {
            if (_executing)
            {
                V6ControlFormHelper.ShowMainMessage(V6Text.Executing);
                return;
            }
            Dispose();
        }
        
        #endregion Linh tinh

        private void exportToExcel_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                V6ControlFormHelper.ShowMainMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog {Filter = @"Excel files (*.xls)|*.xls|Xlsx|*.xlsx"};
                if (save.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {   
                        V6Tools.V6Export.ExportData.ToExcel(_tbl, save.FileName, _reportCaption, true);
                    }
                    catch (Exception ex)
                    {
                        this.ShowErrorMessage(GetType() + ".ExportFail: " + ex.Message);
                        return;
                    }
                    if (V6Options.AutoOpenExcel)
                    {
                        V6ControlFormHelper.OpenFileProcess(save.FileName);
                    }
                    else
                    {
                        this.ShowInfoMessage(V6Text.ExportFinish);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ExportFail\n" + ex.Message);
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            var aControl = ActiveControl;
            if (aControl is V6VvarTextBox)
            {
                return base.DoHotKey0(keyData);
            }

            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            else if (keyData == Keys.F3 && FilterControl.F3)
            {
                XuLyHienThiFormSuaChungTuF3();
            }
            else if (keyData == Keys.F4 && FilterControl.F4)
            {
                XuLyBoSungThongTinChungTuF4();
            }
            else if (keyData == Keys.F5 && FilterControl.F5)
            {
                XuLyXemChiTietF5();
            }
            else if (keyData == Keys.F8 && FilterControl.F8)
            {
                XuLyF8();
            }
            else if (keyData == Keys.F9 && FilterControl.F9)
            {
                XuLyF9();
            }
            else if (keyData == Keys.F10 && FilterControl.F10)
            {
                XuLyF10();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        
        protected virtual void XuLyHienThiFormSuaChungTuF3()
        {
            throw new NotImplementedException();
        }
        
        protected virtual void XuLyBoSungThongTinChungTuF4()
        {
            throw new NotImplementedException();
        }
        protected virtual void XuLyXemChiTietF5()
        {
            throw new NotImplementedException();
        }
        protected virtual void XuLyF8()
        {
            throw new NotImplementedException();
        }

        protected virtual void XuLyF9()
        {
            throw new NotImplementedException();
        }
        
        protected virtual void XuLyF10()
        {
            throw new NotImplementedException();
        }

        protected virtual void ViewDetails(DataGridViewRow row)
        {
            throw new NotImplementedException();
        }

        public override void SetStatus2Text()
        {
            if(V6Setting.IsLoggedIn) FilterControl.SetStatus2Text();
        }

        private void XuLyBase_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //SetStatus2Text();
            }
        }
        
        protected int _oldIndex = -1;

        private void panel1_Leave(object sender, EventArgs e)
        {
            //btnNhan.Focus();
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInData == null || MauInData.Rows.Count == 0) return;
                var row0 = MauInData.Rows[0];
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", row0["MA_FILE"].ToString().Trim()},
                        {"MAU", row0["MAU"].ToString().Trim()},
                        {"LAN", row0["LAN"].ToString().Trim()},
                        {"REPORT", row0["REPORT"].ToString().Trim()}
                    };
                var f = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
                f.AfterInitControl += f_AfterInitControl;
                f.InitFormControl();
                f.SetFather(this);
                f.UpdateSuccessEvent += (data) =>
                {
                    //cap nhap thong tin
                    LoadComboboxSource();
                };
                f.ShowDialog(this);
                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnSuaTTMauBC_Click", ex);
            }
            SetStatus2Text();
        }

        public void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Albc");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInSelectedRow != null) return;

                ConfirmPasswordV6 f_v6 = new ConfirmPasswordV6();
                if (f_v6.ShowDialog(this) == DialogResult.OK)
                {
                    SortedDictionary<string, object> data0 = null;
                    //var viewt = new DataView(MauInData);
                    //viewt.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'";
                    var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _reportFile},
                        {"MAU", "VN"},
                        {"LAN", "V"},
                        {"REPORT", _reportFile}
                    };
                    if (MauInData == null || MauInData.Rows.Count == 0)
                    {
                        data0 = new SortedDictionary<string, object>();
                        data0.AddRange(keys);
                        data0["CAPTION"] = _reportCaption;
                        data0["CAPTION2"] = _reportCaption;
                        data0["TITLE"] = _reportCaption;
                        data0["FirstAdd"] = "1";
                    }

                    var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, keys, data0);
                    f2.AfterInitControl += f_AfterInitControl;
                    f2.InitFormControl();
                    f2.SetFather(this);
                    f2.ShowDialog(this);
                    SetStatus2Text();
                    if (f2.InsertSuccess)
                    {
                        //cap nhap thong tin
                        LoadComboboxSource();
                    };
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ThemMauBC_Click: ", ex);
            }
            SetStatus2Text();
        }

    }
}
