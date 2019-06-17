﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6Structs;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class FormAddEdit : V6Form
    {
        public AddEditControlVirtual FormControl;
        private readonly V6TableName _tableName = V6TableName.Notable;
        private V6Mode _mode;
        private IDictionary<string, object> _keys;
        private IDictionary<string, object> _data;
        private readonly string _tableNameString;
        //private string _tableView;//use _aldmConfig;
        
        public event HandleResultData InsertSuccessEvent;
        public event HandleResultData UpdateSuccessEvent;
        public event EventHandler CallReloadEvent;
        public event EventHandler AfterInitControl;
        protected virtual void OnAfterInitControl()
        {
            var handler = AfterInitControl;
            if (handler != null) handler(FormControl, EventArgs.Empty);
        }

        /// <summary>
        /// No_use
        /// </summary>
        //protected Dictionary<string, string> Event_Methods = new Dictionary<string, string>();
        //protected Type Event_program;
        //protected Dictionary<string, object> All_Objects = new Dictionary<string, object>();
        

        /// <summary>
        /// Khởi tạo form / không sử dụng.
        /// </summary>
        public FormAddEdit()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Khởi tạo form thêm/sửa
        /// </summary>
        /// <param name="tableName">Tên bảng dữ liệu đang thêm/sửa</param>
        /// <param name="mode">V6Mode.Add hoặc V6Mode.Edit</param>
        /// <param name="keys">Khóa để sửa, dùng để load oldData nếu tham số (data) null.</param>
        /// <param name="data">Dữ liệu cũ sẽ gán lên form. Nếu null load bằng keys nếu có.</param>
        public FormAddEdit(V6TableName tableName, V6Mode mode = V6Mode.Add,
            IDictionary<string, object> keys = null,
            IDictionary<string, object> data = null)
        {
            _tableName = tableName;
            _tableNameString = tableName.ToString();
            _mode = mode;
            _keys = keys;
            _data = data;

            InitializeComponent();
            
            //InitFormControl();
        }

        /// <summary>
        /// Sử dụng tableName string khi chưa khai báo V6TableName và table phải có khai báo is_dm trong aldm.
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="mode"></param>
        /// <param name="keys">Khóa lấy dữ liệu</param>
        /// <param name="data">Hoặc dữ liệu có sẵn</param>
        public FormAddEdit(string tableName, V6Mode mode = V6Mode.Add,
            IDictionary<string, object> keys = null,
            IDictionary<string, object> data = null)
        {
            _tableNameString = tableName;
            _tableName = V6TableHelper.ToV6TableName(_tableNameString);
            _mode = mode;
            _keys = keys;
            _data = data;

            InitializeComponent();

            //InitFormControl();
        }

        public IDictionary<string, object> ParentData
        {
            get
            {
                return FormControl.ParentData;
            }
            set
            {
                FormControl.ParentData = value;
            }
        }

        public void SetParentData()
        {
            FormControl.SetParentData();
        }

        private AldmConfig _aldmConfig;
        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            MyInit2();
        }

        /// <summary>
        /// Hàm này cần được gọi sau khi khởi tạo new()
        /// </summary>
        public void InitFormControl()
        {
            try
            {
                LoadAldmConfig();

                FormControl = AddEditManager.Init_Control(_tableName, _tableNameString, _aldmConfig.FormCode);
                if (FormControl is NoRightAddEdit)
                {
                    string keys_string = "";
                    if (_keys != null)
                    {
                        foreach (KeyValuePair<string, object> item in _keys)
                        {
                            keys_string += " " + item.Value;
                        }
                    }
                    ((NoRightAddEdit)FormControl).NoRightInfo = keys_string;
                }

                OnAfterInitControl();
                FormControl.InitValues(_tableName, _mode, _keys, _data);

                panel1.Controls.Add(FormControl);

                if (FormControl == null || FormControl is NoRightAddEdit)
                {
                    btnNhan.Enabled = false;
                    btnInfos.Visible = false;
                }

                MyInit();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".InitFormControl", ex);
            }
        }

        private void LoadAldmConfig()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfig(_tableNameString);
                if (_aldmConfig.HaveInfo)
                {
                    if (_aldmConfig.IS_ALDM)
                    {
                        Text = FormControl.Mode + " - " + (V6Setting.IsVietnamese ? _aldmConfig.TITLE : _aldmConfig.TITLE2);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAldmConfig", ex);
            }
        }

        private void MyInit()
        {
            try
            {
                Text = FormControl.Mode + " - " + V6TableHelper.V6TableCaption(_tableName, V6Setting.Language);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private void MyInit2()
        {
            try
            {
                FormControl.InvokeFormEvent("INIT2");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init2", ex);
            }
        }

        //protected void CreateFormProgram()
        //{
        //    try
        //    {
        //        //DMETHOD
        //        if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
        //        {
        //            //this.ShowWarningMessage("No column name [DMETHOD]");
        //            return;
        //        }

        //        string using_text = "";
        //        string method_text = "";
        //        //foreach (DataRow dataRow in Invoice.Alct1.Rows)
        //        {
        //            var xml = _aldmConfig.DMETHOD;
        //            if (xml == "") return;
        //            DataSet ds = new DataSet();
        //            ds.ReadXml(new StringReader(xml));
        //            if (ds.Tables.Count <= 0) return;
        //            var data = ds.Tables[0];
        //            foreach (DataRow event_row in data.Rows)
        //            {
        //                var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
        //                var method_name = event_row["method"].ToString().Trim();
        //                Event_Methods[EVENT_NAME] = method_name;

        //                using_text += data.Columns.Contains("using") ? event_row["using"] : "";
        //                method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
        //            }
        //        }

        //    Build:
        //        Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".CreateProgram0", ex);
        //    }
        //}

        //private object InvokeFormEvent(string eventName)
        //{
        //    try // Dynamic invoke
        //    {
        //        if (Event_Methods.ContainsKey(eventName))
        //        {
        //            var method_name = Event_Methods[eventName];
        //            return V6ControlsHelper.InvokeMethodDynamic(Event_program, method_name, All_Objects);
        //        }
        //    }
        //    catch (Exception ex1)
        //    {
        //        this.WriteExLog(GetType() + ".Dynamic invoke " + eventName, ex1);
        //    }
        //    return null;
        //}


        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (FormControl.DoInsertOrUpdate())
            {
                DoInsertOrUpdateSuccess(FormControl.DataDic);
                if (FormControl.Mode == V6Mode.Add)
                {
                    V6ControlFormHelper.SetStatusText(V6Text.AddSuccess);
                    ShowMainMessage(V6Text.AddSuccess);
                    
                    DoInsertSuccess(FormControl.DataDic);
                }
                else if (FormControl.Mode == V6Mode.Edit)
                {
                    V6ControlFormHelper.SetStatusText(V6Text.EditSuccess);
                    ShowMainMessage(V6Text.EditSuccess);
                    
                    DoUpdateSuccess(FormControl.DataDic);
                }

                if(FormControl.ReloadFlag) DoReload();
                Close();
            }
        }

        private void DoInsertOrUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            FormControl.All_Objects["dataDic"] = dataDic;
            FormControl.InvokeFormEvent("AFTERSAVE");
            FormControl.InvokeFormEvent("AFTERINSERTORUPDATE");
            //InvokeFormEvent();
        }

        private void DoUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            FormControl.All_Objects["dataDic"] = dataDic;
            FormControl.InvokeFormEvent("AFTERUPDATE");
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(dataDic);
        }

        private void DoInsertSuccess(SortedDictionary<string, object> dataDic)
        {
            FormControl.All_Objects["dataDic"] = dataDic;
            FormControl.InvokeFormEvent("AFTERINSERT");
            var handler = InsertSuccessEvent;
            if (handler != null) handler(dataDic);
        }

        private void DoReload()
        {
            var handler = CallReloadEvent;
            if (handler != null) handler(this, new EventArgs());
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        private void btnInfos_Click(object sender, EventArgs e)
        {
            if (FormControl.Mode == V6Mode.Add || FormControl.Mode == V6Mode.Edit)
            {
                V6ControlFormHelper.ProcessUserDefineInfo(
                    string.IsNullOrEmpty(_aldmConfig.TABLE_VIEW) ? _tableNameString : _aldmConfig.TABLE_VIEW,
                    FormControl, this, _tableName.ToString());
            }
        }
        
    }
}
