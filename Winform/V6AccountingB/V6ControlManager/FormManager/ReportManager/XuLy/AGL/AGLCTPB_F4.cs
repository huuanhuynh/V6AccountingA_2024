using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGLCTPB_F4 : V6Form
    {
        #region Biến toàn cục

        public DataRow _am;
        public List<string> _sttreclist;
        public string _text, _reportProcedure;
        public int _year;

        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public delegate void HandleF4Success();

        //public event HandleF4Success UpdateSuccessEvent;

        public DataSet _ds;
        public DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        public List<SqlParameter> _pList;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====

       
        #endregion properties
        public AGLCTPB_F4()
        {
            InitializeComponent();
        }

        public AGLCTPB_F4(List<string> sttreclist, int year, string reportProcedure)
        {
            _sttreclist = sttreclist;
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
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy1.Value),
                    new SqlParameter("@Day01", one),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay1.Value = (DateTime)result;
                }
                SqlParameter[] plist2 =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy2.Value),
                    new SqlParameter("@Day01", zero),
                };
                object result2 = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist2);
                if (result2 is DateTime)
                {
                    ngay2.Value = (DateTime)result2;
                }

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

        private void Form_Load(object sender, EventArgs e)
        {
            
        }
        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                _executing = true;
                _executesuccess = false;
                                
                var tLoadData = new Thread(Execute);
                tLoadData.Start();
                timerViewReport.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Update error:\n" + ex.Message);
            }
        }

        private void Execute()
        {
            try
            {
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy2.Value, (int)txtNam.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }

                string paramss = "";

                foreach (string stt_rec in _sttreclist)
                {
                    paramss += "," + stt_rec;
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Type", "NEW"),
                        new SqlParameter("@Year", (int)txtNam.Value),
                        new SqlParameter("@Period1", (int)txtKy1.Value),
                        new SqlParameter("@Period2", (int)txtKy2.Value),
                        new SqlParameter("@Stt_recs", stt_rec),
                        new SqlParameter("@User_id", V6Login.UserId),
                        new SqlParameter("@Ma_dvcs", txtMaDvcs.StringValue),
                        new SqlParameter("@ngay1", ngay1.Value.Date),
                        new SqlParameter("@ngay2", ngay2.Value.Date),
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist);
                }

                if (paramss.Length > 0)
                {
                    V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
                        string.Format("reportProcedure:{0} {1}", _reportProcedure, paramss.Substring(1)));
                }

                _executesuccess = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadData", ex);
                _message = "Load data error: " + ex.Message;
                _executesuccess = false;
            }

            _executing = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        //protected virtual void OnUpdateSuccessEvent()
        //{
        //    var handler = UpdateSuccessEvent;
        //    if (handler != null) handler();
        //}

        private void txtKy1_V6LostFocus(object sender)
        {
            try
            {
                // tạo ngay1 gọi proc function VFV_YearMonthToDate @Year @Month @Day01 => ngay1.Value = result.
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy1.Value),
                    new SqlParameter("@Day01", one),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay1.Value = (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".txtKy1_V6LostFocus", ex);
            }
        }

        private void txtKy2_V6LostFocus(object sender)
        {
            try
            {
                // tạo ngay1 gọi proc function VFV_YearMonthToDate @Year @Month @Day01 => ngay1.Value = result.
                int one = 1, zero = 0;
                SqlParameter[] plist =
                {
                    new SqlParameter("@Year", (int)txtNam.Value),
                    new SqlParameter("@Month", (int)txtKy2.Value),
                    new SqlParameter("@Day01", zero),
                };
                object result = V6BusinessHelper.ExecuteFunctionScalar("VFV_YearMonthToDate", plist);
                if (result is DateTime)
                {
                    ngay2.Value = (DateTime)result;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".txtKy2_V6LostFocus", ex);
            }
        }

        public bool _executing, _executesuccess;
        private void timerViewReport_Tick(object sender, EventArgs e)
        {
            if (_executing)
            {
                btnNhan.Image = waitingImages.Images[ii++];
                if (ii >= waitingImages.Images.Count) ii = 0;
            }
            else
            {
                if (_executesuccess)
                {
                    timerViewReport.Stop();
                    btnNhan.Image = btnNhanImage;
                    ii = 0;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    timerViewReport.Stop();
                    btnNhan.Image = btnNhanImage;
                    DialogResult = DialogResult.Abort;
                }
            }
        }
    }
}
