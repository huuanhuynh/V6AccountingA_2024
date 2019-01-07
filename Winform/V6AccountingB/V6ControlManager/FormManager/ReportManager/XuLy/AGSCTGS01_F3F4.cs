using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AGSCTGS01_F3F4 : V6FormControl
    {
        #region Biến toàn cục

        protected IDictionary<string, object> _data;
        protected V6Mode _mode;
        protected string _stt_rec, _text,_uid;
        protected string _tableName = "ARctgs01";

        public event HandleResultData InsertSuccessEvent;
        protected virtual void OnInsertSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = InsertSuccessEvent;
            if (handler != null) handler(datadic);
        }
        public event HandleResultData UpdateSuccessEvent;
        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }

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
            get { return txtSo_lo.Text; }
            set { txtSo_lo.Text = value; }
        }
        #endregion properties
        public AGSCTGS01_F3F4()
        {
            InitializeComponent();
        }

        public AGSCTGS01_F3F4(V6Mode mode, string stt_rec, IDictionary<string, object> data)
        {
            _mode = mode;
            _stt_rec = stt_rec;
            if (mode == V6Mode.Edit)
            {
                _uid = ((Guid) data["UID"]).ToString();
            }
            _data = data;
            InitializeComponent();
            MyInit();
            Getmaxstt();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataDictionary(this, _data);
                txtMaCt.Text = "S07";
                if (_mode == V6Mode.Add)
                {
                    TXTkieu_ctgs.Text = "1";
                    TXTNO_CO.Value = 1;
                }

                var length = V6BusinessHelper.VFV_iFsize("ARCTGS01", "SO_LO");
                if (length == 0) length = 12;
                txtSo_lo.MaxLength = length;
                length = V6BusinessHelper.VFV_iFsize("ARCTGS01", "dien_giai");
                if (length == 0) length = 128;
                txtdien_giai.MaxLength = length;
                v6IndexComboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public  void Getmaxstt()
        {
            if (_mode == V6Mode.Add)
            {
                decimal maxvalue = V6BusinessHelper.GetMaxValueTable("ARCTGS01", "STT", "NAM=" + txtNam.Value+" AND THANG=" + txtThang1.Value);
                txtStt.Value = maxvalue + 1;
            }
        }

        private void UpdateData()
        {
            try
            {
                var data = GetData();

                data["KHOA_CTGS"] = _stt_rec;
                
                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _uid },
                    { "KHOA_CTGS", _stt_rec}
                };

                var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
                if (result == 1)
                {
                    Dispose();
                    V6ControlFormHelper.ShowMainMessage("Sửa thành công");
                    OnUpdateSuccessEvent(data);
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

        private void InsertNew()
        {
            try
            {

                var data = GetData();

                data["KHOA_CTGS"] = _stt_rec;

                var result = V6BusinessHelper.Insert(_tableName, data);

                if (result)
                {
                    Dispose();
                    V6ControlFormHelper.ShowMainMessage("Thêm thành công");
                    OnInsertSuccessEvent(data);
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
       
        
        private void FormBaoCaoHangTonTheoKho_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {

            if (_mode == V6Mode.Edit)
            {
                UpdateData();
            }
            else if(_mode == V6Mode.Add)
            {
                InsertNew();
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

        private void txttk_du0_TextChanged(object sender, EventArgs e)
        {
            txtTk_du.Text = txttk_du0.Text.Trim();
        }
      

    }
}
