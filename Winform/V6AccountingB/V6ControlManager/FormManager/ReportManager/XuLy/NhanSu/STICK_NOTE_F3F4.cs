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
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class STICK_NOTE_F3F4 : V6Form
    {
        #region Biến toàn cục

        protected IDictionary<string, object> _dataOld;
        protected V6Mode _mode;
        //protected string _text;
        //protected string _uid;
        protected string _tableName = "V6NOTESCT";

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
        public STICK_NOTE_F3F4()
        {
            InitializeComponent();
        }

        public STICK_NOTE_F3F4(V6Mode mode, IDictionary<string, object> data)
        {
            _mode = mode;
            
            _dataOld = data;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadAdvanceControls(_tableName);
                V6ControlFormHelper.LoadAndSetFormInfoDefine(_tableName, tabTuDinhNghia, this);
                LoadStruct(_tableName);
                V6ControlFormHelper.SetFormDataDictionary(this, _dataOld);
                dateNgay.DisableTag();
                txtMaNVien.ExistRowInTable();

                if (_mode == V6Mode.Add)
                {
                    Text = "Thêm";
                    chkStatus.Checked = true;
                }
                else if(_mode == V6Mode.Edit)
                {
                    Text = "Sửa";
                }
                else if (_mode == V6Mode.View)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(this, true);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void LoadAdvanceControls(string tableName)
        {
            try
            {
                Dictionary<string, object> All_Objects = new Dictionary<string, object>();
                All_Objects["thisForm"] = this;
                FormManagerHelper.CreateAdvanceFormControls(this, tableName, All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls", ex);
            }
        }

        private void UpdateData()
        {
            try
            {
                var data = GetData();

                //data["KHOA_HELP"] = _stt_rec;
                
                var keys = new SortedDictionary<string, object>
                {
                    { "UID", _dataOld["UID"]}
                };

                var result = V6BusinessHelper.UpdateSimple(_tableName, data, keys);
                if (result == 1)
                {
                    Dispose();
                    ShowTopLeftMessage(V6Text.UpdateSuccess);
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
                data["USER_ID"] = V6Login.UserId;
                
                var result = V6BusinessHelper.Insert(_tableName, data);

                if (result)
                {
                    Dispose();
                    ShowTopLeftMessage(V6Text.AddSuccess);
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
       
        
        private void Form_Load(object sender, EventArgs e)
        {
            //SetStatus2Text();
        }

        
        public void btnNhan_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (_mode == V6Mode.Edit)
                {
                    UpdateData();
                }
                else if (_mode == V6Mode.Add)
                {
                    InsertNew();
                }
            }
            else
            {
                ShowMainMessage(V6Text.DataExist);
            }
        }

        private bool ValidateData()
        {
            try
            {
                byte status = 0;
                var dataDic = GetData();
                dataDic["USER_ID"] = V6Login.UserId;
                var dataOld = new SortedDictionary<string, object>();
                if (_mode == V6Mode.Edit)
                {
                    dataOld.AddRange(_dataOld);
                }
                else
                {
                    dataOld = GetData();
                }
                
                var _aldmConfig = ConfigManager.GetAldmConfig(_tableName);
                var KEY_LIST = ObjectAndString.SplitString(_aldmConfig.KEY.ToUpper());
                string error = CheckValid(dataOld, dataDic, _tableName, KEY_LIST);
                return string.IsNullOrEmpty(error);
            }
            catch (Exception ex)
            {
                this.ShowErrorException("ValidateData", ex);
            }
            return false;
        }

        protected string CheckValid(IDictionary<string, object> dataOld, IDictionary<string, object> dataDic,
            string tableName, IList<string> KEY_LIST)
        {
            var keys_new = new SortedDictionary<string, object>();
            foreach (string KEY in KEY_LIST)
            {
                keys_new.Add(KEY, dataDic[KEY]);
            }

            string where_new = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_new);

            AldmConfig config = ConfigManager.GetAldmConfig(tableName);
            bool exist_new = V6BusinessHelper.CheckDataExistStruct(tableName, keys_new, config.CHECK_LONG);

            if (_mode == V6Mode.Edit)
            {
                SortedDictionary<string, object> keys_old = new SortedDictionary<string, object>();
                foreach (string KEY in KEY_LIST)
                {
                    keys_old.Add(KEY, dataOld[KEY]);
                }
                string where_old = SqlGenerator.GenWhere(V6BusinessHelper.GetTableStruct(tableName), keys_old);
                //bool exist_old = V6BusinessHelper.CheckDataExistStruct(TableName, keys_old);

                if (where_new != where_old && exist_new)
                    return V6Text.EditDenied + " " + where_new;
            }
            else if (_mode == V6Mode.Add)
            {
                if (exist_new)
                    return V6Text.AddDenied + " " + where_new;
            }

            return "";
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

        private void btnInfos_Click(object sender, EventArgs e)
        {
            if (_mode == V6Mode.Add || _mode == V6Mode.Edit)
            {
                V6ControlFormHelper.ProcessUserDefineInfo(_tableName, v6TabControl1, this, _tableName);
            }
        }

    }
}
