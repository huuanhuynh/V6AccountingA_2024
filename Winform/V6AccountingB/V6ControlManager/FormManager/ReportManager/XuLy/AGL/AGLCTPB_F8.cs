using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGLCTPB_F8 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _sttreclist, _text, _reportProcedure;
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
        public AGLCTPB_F8()
        {
            InitializeComponent();
        }

        public AGLCTPB_F8(string sttreclist, int year, string reportProcedure)
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
            
            try
            {
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy2.Value, (int)txtNam.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                //@Type AS VARCHAR(8),
                //@Year AS INT,
                //@Period1 AS INT = 0,
                //@Period2 AS INT = 0,
                //@Stt_recs VARCHAR(MAX) = '', 
                //@User_id INT = 1, 
                //@Ma_dvcs VARCHAR(50) = ''

                if (this.ShowConfirmMessage("Có chắc chắn xóa không ?") == DialogResult.Yes)
                {

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Type", "DEL"),
                        new SqlParameter("@Year", txtNam.Value),
                        new SqlParameter("@Period1", txtKy1.Value),
                        new SqlParameter("@Period2", txtKy2.Value),
                        new SqlParameter("@Stt_recs", _sttreclist),
                        new SqlParameter("@User_id", V6Login.UserId),
                        new SqlParameter("@Ma_dvcs", ""),
                        new SqlParameter("@ngay1", ngay1.Value.Date),
                        new SqlParameter("@ngay2", ngay2.Value.Date),

                    };
                    string paramss = V6ControlFormHelper.PlistToString(plist);
                    V6BusinessHelper.WriteV6UserLog(ItemID, GetType() + "." + MethodBase.GetCurrentMethod().Name,
                    string.Format("reportProcedure:{0} {1}", _reportProcedure, paramss));
                    V6BusinessHelper.ExecuteProcedureNoneQuery(_reportProcedure, plist);
                    
                    OnUpdateSuccessEvent();
                    Dispose();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                }
                else
                {
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Delete error:\n" + ex.Message);
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

        protected virtual void OnUpdateSuccessEvent()
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler();
        }

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
    }
}
