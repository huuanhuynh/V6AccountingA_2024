using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class AAPPR_IND_F10 : V6FormControl
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
        /// <summary>
        /// Mã xử lý đã chọn sau khi bấm nhận.
        /// </summary>
        public string MA_XULY { get; private set; }
        #endregion properties
        public AAPPR_IND_F10()
        {
            InitializeComponent();
        }

        public AAPPR_IND_F10(string stt_rec, IDictionary<string, object> data)
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
                //V6ControlFormHelper.SetFormDataDictionary(this, _data);
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
                MA_XULY = txtMa_Xuly.Value.ToString();
                ((Form) Parent).DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Nhan", ex);
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
