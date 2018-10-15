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
    public partial class AFAGIAMNG_F4 : V6FormControl
    {
        #region Biến toàn cục

        protected DataRow _am;
        protected string _stt_rec, _text,_uid;
        

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
        public AFAGIAMNG_F4()
        {
            InitializeComponent();
        }

        public AFAGIAMNG_F4(string stt_rec, DataRow am)
        {
            _stt_rec = stt_rec;
            _uid= ((System.Guid)am["UID"]).ToString();
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
                txtTang_giam.Value = 2;
                txtTs0.Value = 0;
                txtLyDoTang.SetInitFilter("loai_tg_ts ='G'");

                var length = V6BusinessHelper.VFV_iFsize("ADALTS", "So_ct");
                if (length == 0) length = 12;
                txtSoCt.MaxLength = length;
                length = V6BusinessHelper.VFV_iFsize("ADALTS", "dien_giai");
                if (length == 0) length = 128;
                txtdien_giai.MaxLength = length;
                //TxtTen_ts.Text = _am["TEN_TS"].ToString();
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
                var am = new SortedDictionary<string, object>();
                am = GetData();
                am["STT_REC"] = _am["STT_REC"];
                am["DIEN_GIAI"] = txtdien_giai.Text;

               
                var result = V6BusinessHelper.Insert("ADALTS", am);
                if (result)
                {

                    Dispose();
                }
                else
                {
                    this.ShowWarningMessage("Insert: ");
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

        protected virtual void OnUpdateSuccessEvent(SortedDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }
        private void TinhGiaTriKhauHao()
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
