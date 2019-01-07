using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class ZACOSXLT_TINHGIA_F3 : V6Form
    {
        #region Biến toàn cục

        protected IDictionary<string, object> _data;
        protected V6Mode _mode;
        protected string _text, _uID;
        //protected string _uid;
        protected string _tableName = "Acosxlt_cogtsp";

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


        #endregion properties
        public ZACOSXLT_TINHGIA_F3()
        {
            InitializeComponent();
        }

        public ZACOSXLT_TINHGIA_F3(V6Mode mode, string uID, IDictionary<string, object> data)
        {
            _mode = mode;
            _uID = uID;
            
            _data = data;
            InitializeComponent();
            MyInit();
            //Getmaxstt();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataDictionary(this, _data);

                if (chkdc_ck.Checked)
                {
                    txtGia.Enabled = true;
                }
                else
                {
                    txtGia.Enabled = false;
                    txtGia.Value = 0;
                }

                if (_mode == V6Mode.Add)
                {
                    Text = V6Text.Add;
                }
                else if(_mode == V6Mode.Edit)
                {
                    Text = V6Text.Edit;
                }
                else if (_mode == V6Mode.View)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(this, true);
                }
                Ready();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        public void Getmaxstt()
        {
            if (_mode == V6Mode.Add)
            {
                decimal maxvalue = V6BusinessHelper.GetMaxValueTable("V6HELP_QA", "STT", "1=1");
                txtThang.Value = maxvalue + 1;
            }
        }

        private void UpdateData()
        {
            try
            {
                var data = GetData();

                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _uID },
                };

                var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
                if (result == 1)
                {
                    Dispose();
                    ShowMainMessage(V6Text.UpdateSuccess);
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

                data["KHOA_HELP"] = _sttRec;

                var result = V6BusinessHelper.Insert(_tableName, data);

                if (result)
                {
                    Dispose();
                    ShowMainMessage(V6Text.AddSuccess);
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

        private void v6NumberTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                TinhToan();
            }
        }

        private void TinhToan()
        {
            txtTien.Value = txtSL.Value*txtGia.Value;
        }

        private void chkdc_ck_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdc_ck.Checked)
            {
                txtGia.Enabled = true;
            }
            else
            {
                txtGia.Enabled = false;
                txtGia.Value = 0;
            }
        }
    }
}
