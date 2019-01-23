using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AFATANGNG_F4 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
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

        public string So_ctx
        {
            get { return txtSoCt.Text; }
            set { txtSoCt.Text = value; }
        }
        #endregion properties
        public AFATANGNG_F4()
        {
            InitializeComponent();
        }

        public AFATANGNG_F4(string stt_rec, DataRow am)
        {
            _sttRec = stt_rec;
            _am = am;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataRow(this, _am);
                txtMaCt.Text = "S02";
               txtTang_giam.Value = 1;
             txtTs0.Value = 0;
                txtThang1.Value = V6Setting.M_SV_DATE.Month;
                txtNam.Value = V6Setting.M_SV_DATE.Year;
                txtLyDoTang.SetInitFilter("loai_tg_ts ='T'");

                var length = V6BusinessHelper.VFV_iFsize("ADALTS", "So_ct");
                if (length == 0) length = 12;
                txtSoCt.MaxLength = length;
                length = V6BusinessHelper.VFV_iFsize("ADALTS", "dien_giai");
                if (length == 0) length = 128;
                txtdien_giai.MaxLength = length;
                

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
               
               var am = GetData(); 

               am["DIEN_GIAI"] = txtdien_giai.Text;
               am["STT_REC"] = _am["STT_REC"];



               var result = V6BusinessHelper.Insert("ADALTS", am);
                
                if (result)
                {
                    Dispose();
                    ShowMainMessage(V6Text.AddSuccess);
                    OnUpdateSuccessEvent(am);
                }
                else
                {
                    this.ShowWarningMessage("Insert Error!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Insert error:\n" + ex.Message);
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
        public void TinhGiaTriKhauHao()
        {
            try
            {
                var ppkh = ObjectAndString.ObjectToInt(V6Options.GetValue("M_PP_KH"));
                txtgt_cl.Value = txtnguyen_gia.Value - Txtgt_da_kh.Value;
                if (TxtSo_ky.Value > 0)
                {
                    Txtgt_kh_ky.Value = V6BusinessHelper.Vround(
                        ppkh == 1
                        ? txtnguyen_gia.Value / TxtSo_ky.Value
                        : txtgt_cl.Value / TxtSo_ky.Value,
                        V6Options.M_ROUND);

                }
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaTriKhauHao: " + ex.Message);
            }
        }

        private void txtnguyen_gia_V6LostFocus(object sender)
        {
            TinhGiaTriKhauHao();
        }
        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 0;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }
      

    }
}
