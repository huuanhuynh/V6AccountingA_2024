using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Structs;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_SOA_F4 : V6FormControl
    {
        #region Biến toàn cục

        private string _tableName_AM;
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
            get { return txtSoCtXuat.Text; }
            set { txtSoCtXuat.Text = value; }
        }
        #endregion properties
        public AAPPR_SOA_F4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stt_rec"></param>
        /// <param name="am"></param>
        /// <param name="tableName_AM">Tên bảng dữ liệu sẽ được cập nhập (update).</param>
        public AAPPR_SOA_F4(string stt_rec, DataRow am, string tableName_AM)
        {
            _sttRec = stt_rec;
            _am = am;
            _tableName_AM = tableName_AM;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                V6ControlFormHelper.SetFormDataRow(this, _am);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                am["GHI_CHU01"] = txtGhiChu01.Text;
                am["GHI_CHU02"] = txtGhiChu02.Text;
                am["SO_CTX"] = txtSoCtXuat.Text;

                SortedDictionary<string, object> keys
                    = new SortedDictionary<string, object> {{"Stt_rec", _sttRec}};

                var result = V6BusinessHelper.UpdateSimple(_tableName_AM, am, keys);
                if (result == 1)
                {
                    Dispose();
                    OnUpdateSuccessEvent(am);
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

        protected virtual void OnUpdateSuccessEvent(SortedDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }
    }
}
