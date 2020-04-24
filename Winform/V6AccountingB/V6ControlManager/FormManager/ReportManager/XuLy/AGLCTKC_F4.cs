using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGLCTKC_F4 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _numlist, _text, _reportProcedure;
        protected int _year;

        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public delegate void HandleF4Success();

        public event HandleF4Success UpdateSuccessEvent;

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

       
        #endregion properties
        public AGLCTKC_F4()
        {
            InitializeComponent();
        }

        public AGLCTKC_F4(string numlist,int year,string reportProcedure)
        {
            _numlist = numlist;
            _year = year;
            _reportProcedure = reportProcedure;

            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                txtNam.Value = _year;
                txtKy1.Value = V6Setting.M_SV_DATE.Month;
                txtKy2.Value = V6Setting.M_SV_DATE.Month;
                txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
                if (V6Login.MadvcsCount <= 1){
                    txtMaDvcs.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (_executing)
            {
                return;
            }

            try
            {
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy2.Value, (int)txtNam.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                _executing = true;
                _executing_success = false;
                CheckForIllegalCrossThreadCalls = false;
                var tLoadData = new Thread(Executing);
                tLoadData.Start();
                timerViewReport.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnNhan_Click", ex);
            }
        }

        private void Executing()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@Type", "NEW"),
                    new SqlParameter("@Year", txtNam.Value),
                    new SqlParameter("@Period1", txtKy1.Value),
                    new SqlParameter("@Period2", txtKy2.Value),
                    new SqlParameter("@NumList", _numlist),
                    new SqlParameter("@User_id", V6Login.UserId),
                    new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValue)

                };

                int result = V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist);
                _executing_success = result > 0;
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                _executing_success = false;
                this.WriteExLog(GetType() + ".Executing!", ex);
            }
            _executing = false;
        }

        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                timerViewReport.Stop();
                btnNhan.Image = btnNhanImage;
                if (_executing_success)
                {
                    OnUpdateSuccessEvent();
                    Dispose();
                }
                else
                {
                    timerViewReport.Stop();
                    ShowMainMessage(_message);
                }
            }
        }
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else if (keyData == (Keys.Control | Keys.Enter))
            {
                btnNhan.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected int _oldIndex = -1;
        private bool _executing, _executing_success;

        protected virtual void OnUpdateSuccessEvent()
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler();
        }

        
    }
}
