using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class ATODCBP_F3 : V6FormControl
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
        
        #endregion properties
        public ATODCBP_F3()
        {
            InitializeComponent();
        }

        public ATODCBP_F3(string stt_rec, DataRow am)
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
                txtCC0.Value = 0;
                TxtTk_cc.FilterStart=true;
                txtTk_pb.FilterStart = true;
                TxtTk_cp.FilterStart = true;
                TxtTk_cc.SetInitFilter("Loai_tk=1");
                txtTk_pb.SetInitFilter("Loai_tk=1");
                TxtTk_cp.SetInitFilter("Loai_tk=1");

                txtSo_the_cc.ExistRowInTable();
                TxtMa_BP.ExistRowInTable();

                if (TxtTk_cc.Data != null)
                    txtten_tk_cc.Text = TxtTk_cc.Data["TEN_TK"].ToString().Trim();
                if (txtTk_pb.Data != null)
                    txtten_tk_pb.Text = txtTk_pb.Data["TEN_TK"].ToString().Trim();
                if (TxtTk_cp.Data != null)
                    txtten_tk_cp.Text = TxtTk_cp.Data["TEN_TK"].ToString().Trim();

             
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

       
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            
            try
            {
                var am = new SortedDictionary<string, object>();
                am = GetData();
                am["STT_REC"] = _am["STT_REC"];
                
                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _uid }
                };

                var result = V6BusinessHelper.UpdateSimple("ADBPCC", am, keys);
                if (result == 1)
                {

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

        private void txtThang1_Leave(object sender, EventArgs e)
        {
            if (txtThang1.Value < 1 || txtThang1.Value > 12)
                txtThang1.Value = 1;
        }

        private void TxtTk_ts_Leave(object sender, EventArgs e)
        {
            if (TxtTk_cc.Data != null)
                txtten_tk_cc.Text = TxtTk_cc.Data["TEN_TK"].ToString().Trim();
        }

        private void txtTk_kh_Leave(object sender, EventArgs e)
        {
            if (txtTk_pb.Data != null)
                txtten_tk_pb.Text = txtTk_pb.Data["TEN_TK"].ToString().Trim();
        }

        private void TxtTk_cp_Leave(object sender, EventArgs e)
        {
            if (TxtTk_cp.Data != null)
                txtten_tk_cp.Text = TxtTk_cp.Data["TEN_TK"].ToString().Trim();
        }
      

    }
}
