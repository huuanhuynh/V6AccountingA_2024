using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.Filter.Sms;
using V6Controls;
using V6Controls.Forms;
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
        protected string _program, _text;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        protected DataTable _tblGridView2;
        //private V6TableStruct _tStruct;
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

        public XuLyBase0(string itemId, string program, string reportProcedure, string reportFile, string text, bool viewDetail = false)
        {
            m_itemId = itemId;
            _program = program;
            _reportProcedure = reportProcedure;
            _reportFile = reportFile;
            _text = text;
            ViewDetail = viewDetail;
            
            InitializeComponent();
            BaseInit();
        }

        private void BaseInit()
        {
            Text = _text;
            
            AddFilterControl(_program);
            FixFilterControlSize();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            LoadDefaultData(4, "", _program, m_itemId, "");
        }

        
        public FilterBase FilterControl { get; set; }
        protected void AddFilterControl(string program)
        {
            FilterControl = Filter.Filter.GetFilterControl(program);
            FilterControl.Height = panel1.Height - 5;
            panel1.Controls.Add(FilterControl);
            panel1.SizeChanged += panel1_SizeChanged;
            FilterControl.Focus();
        }

        private void FixFilterControlSize()
        {
            try
            {
                if (FilterControl is XASENDSMS)
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

        void panel1_SizeChanged(object sender, EventArgs e)
        {
            FilterControl.Height = panel1.Height - 5;
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

        protected bool _success;
        protected bool _executing;
        
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
                _success = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhToan", ex);
                _message = ex.Message;
                _executing = false;
            }
            _executing = false;
        }

        protected virtual void Nhan()
        {
            _message = string.Empty;
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
            if (_success)
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                ii = 0;
                try
                {
                    FilterControl.LoadDataFinish(_ds);
                    //Chỉ kích hoạt hàm FormatGridView
                    FilterControl.FormatGridView(null);
                    DoAfterExecuteSuccess();
                    _message = V6Text.Finish;
                    V6ControlFormHelper.SetStatusText(_message);
                    if (FilterControl.ExecuteMode != ExecuteMode.ExecuteProcedure)
                    {
                        ShowMainMessage(_message);
                    }
                    _success = false;
                }
                catch (Exception ex)
                {
                    timerViewReport.Stop();
                    _success = false;
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

        
        private void btnHuy_Click(object sender, EventArgs e)
        {
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
                        V6Tools.V6Export.ExportData.ToExcel(_tbl, save.FileName, _text, true);
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
            if(V6Setting.IsRunTime) FilterControl.SetStatus2Text();
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
            btnNhan.Focus();
        }

    }
}
