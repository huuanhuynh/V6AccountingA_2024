using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AFASUAKH_F3 : V6FormControl
    {
        #region Biến toàn cục

        protected IDictionary<string, object> _data;
        protected string _text;
        //protected string _reportFileF5, _reportTitleF5, _reportTitle2F5;
        public event HandleResultData UpdateSuccessEvent;

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
        public AFASUAKH_F3()
        {
            InitializeComponent();
        }

        public AFASUAKH_F3(string stt_rec, IDictionary<string,object> data)
        {
            _sttRec = stt_rec;
            _data = data;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataDictionary(this, _data);
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
                int check = V6BusinessHelper.CheckDataLocked("2", V6Setting.M_SV_DATE, (int)txtKy.Value, (int)txtnam.Value);
                if (check == 1)
                {
                    this.ShowWarningMessage(V6Text.CheckLock);
                    return;
                }
                //@nam int,
                //@ky int,
                //@so_the_ts varchar(50),
                //@ma_nv varchar(16),
                //@gt_kh_ky numeric(16,2),
                //@user_id numeric(3,0),
                //@date2 smalldatetime,
                //@time2 varchar(8)

                var serverDateTime = V6BusinessHelper.GetServerDateTime();
                var dateString = serverDateTime.ToString("yyyyMMdd");
                var timeString = serverDateTime.ToString("HH:mm:ss");

                SqlParameter[] plist =
                    {
                        new SqlParameter("@nam",txtnam.Value ),
                        new SqlParameter("@ky", txtKy.Value),
                        new SqlParameter("@so_the_ts", txtSo_the_ts.Text),
                        new SqlParameter("@ma_nv", txtma_nv.Text),
                        new SqlParameter("@gt_kh_ky", txtgt_kh_ky.Value),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@date2",dateString),
                        new SqlParameter("@time2",timeString)
                    };
              
                var result = V6BusinessHelper.ExecuteProcedureNoneQuery("AFASUAKH_F3", plist);
                
                if (result > 0)
                {
                    OnUpdateSuccessEvent(GetData());
                    Dispose();
                }
                else
                {
                    this.ShowWarningMessage("Update: " + result);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Update error:\n" + ex.Message);
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

        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }
    }
}
