using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class ATODCCTBP_F4 : V6FormControl
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
        public ATODCCTBP_F4()
        {
            InitializeComponent();
        }

        public ATODCCTBP_F4(string stt_rec, DataRow am)
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
                txtMaCt.Text = "S03";
                txtCC0.Value = 0;
               
                txtTk_pb_I.FilterStart = true;
                TxtTk_cp_I.FilterStart = true;
                txtTk_pb_I.SetInitFilter("Loai_tk=1");
                TxtTk_cp_I.SetInitFilter("Loai_tk=1");

                txtThang1.Value = V6Setting.M_SV_DATE.Month;
                txtNam.Value = V6Setting.M_SV_DATE.Year;

               
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

            SqlTransaction transaction = SqlConnect.CreateSqlTransaction("ATODCCTBP_F4");
            try
            {
                var am = GetData();
                am["STT_REC"] = _am["STT_REC"];

              
                var result = V6BusinessHelper.Insert(transaction,"ADCTCCBP", am);
                if (result)
                {

                    var keys = new SortedDictionary<string, object>
                    {
                        { "STT_REC",  am["STT_REC"]  },
                        { "SO_THE_CC",  am["SO_THE_CC"]  },
                        { "NAM",  am["NAM"]  },
                        { "KY",  am["KY"]  }

                    };

                   var where = V6SqlConnect.SqlGenerator.GenSqlWhere(keys);
                   var tongheso = SqlConnect.ExecuteScalar(transaction, CommandType.Text,
                      "SElECT SUM(HE_SO) AS T_HE_SO FROM ADCTCCBP WHERE " + where);
                   var am1 = new SortedDictionary<string, object>
                    {
                        { "T_HE_SO", tongheso }

                    };
                   V6BusinessHelper.Update(transaction, "ADCTCCBP", am1, keys);
                   transaction.Commit();

                    Dispose();
                }
                else
                {
                    transaction.Rollback();
                    this.ShowWarningMessage("Insert: !! " );
                }
                
               
            }
            catch (Exception ex)
            {
                transaction.Rollback();
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
      
        private void txtThang12_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var txt = (V6NumberTextBox)sender;
                if (txt.Value < 1) txt.Value = 1;
                if (txt.Value > 12) txt.Value = 12;
            }
            catch (Exception)
            {

            }
        }



        private void txtTk_kh_Leave(object sender, EventArgs e)
        {
            if (txtTk_pb_I.Data != null)
                txtten_tk_kh.Text = txtTk_pb_I.Data["TEN_TK"].ToString().Trim();
        }

        private void TxtTk_cp_Leave(object sender, EventArgs e)
        {
            if (TxtTk_cp_I.Data != null)
                txtten_tk_cp.Text = TxtTk_cp_I.Data["TEN_TK"].ToString().Trim();
        }


        private void txtThang1_Leave(object sender, EventArgs e)
        {
            if (txtThang1.Value < 1 || txtThang1.Value > 12)
                txtThang1.Value = 1;
        }


    }
}
