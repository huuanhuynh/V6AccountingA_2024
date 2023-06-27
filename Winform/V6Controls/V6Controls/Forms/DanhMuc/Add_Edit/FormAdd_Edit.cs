using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class FormAddEdit : V6Form
    {
        public Control _fatherControl;
        public AddEditControlVirtual FormControl;
        public readonly V6Mode _mode;
        public readonly IDictionary<string, object> _keys;
        public readonly IDictionary<string, object> _data;
        public readonly string _MA_DM;
        /// <summary>
        /// Cờ thể hiện người dùng bấm nút copy hay nút add.
        /// </summary>
        public bool IS_COPY;
        
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
        public FormAddEdit(V6TableName tableName, V6Mode mode, IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            _MA_DM = tableName.ToString();
            _mode = mode;
            _keys = new Dictionary<string, object>();
            _keys.AddRange(keys);
            _data = data;
            
            InitializeComponent();

            //InitFormControl();
        }

        /// <summary>
        /// Sử dụng tableName string khi chưa khai báo V6TableName và table phải có khai báo is_dm trong aldm.
        /// </summary>
        /// <param name="ma_dm">Tên bảng</param>
        /// <param name="mode">V6Mode Add Edit View</param>
        /// <param name="keys">Khóa lấy dữ liệu để sẵn lên form hoặc null.</param>
        /// <param name="data">Hoặc dữ liệu có sẵn hoặc null.</param>
        public FormAddEdit(string ma_dm, V6Mode mode, IDictionary<string, object> keys, IDictionary<string, object> data)
        {
            _MA_DM = ma_dm;
            _mode = mode;
            _keys = new Dictionary<string, object>();
            _keys.AddRange(keys);
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

        /// <summary>
        /// Cờ báo hiệu thêm thành công sau khi đóng form.
        /// </summary>
        public bool InsertSuccess { get; set; }
        /// <summary>
        /// Cờ báo hiệu sửa thành công sau khi đóng form.
        /// </summary>
        public bool UpdateSuccess { get; set; }
        /// <summary>
        /// Dữ liệu đã dùng để thêm hoặc sửa (insert/update).
        /// </summary>
        public IDictionary<string, object> Data { get { return FormControl.DataDic; } }
        
        ///// <summary>
        ///// Gán control nơi sinh ra. Phục vụ cho các hàm sâu xa.
        ///// </summary>
        ///// <param name="fatherControl"></param>
        //public void SetFather(Control fatherControl)
        //{
        //    _fatherControl = fatherControl;
        //    FormControl.SetGrandFather(fatherControl);
        //}

        public void SetParentData()
        {
            FormControl.SetParentData();
        }

        private AldmConfig _aldmConfig;
        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            MyInit2();
        }

        public void InitFormControl()
        {
            InitFormControl(null);
        }

        /// <summary>
        /// Hàm này cần được gọi sau khi khởi tạo new()
        /// </summary>
        public void InitFormControl(Control grandFather)
        {
            try
            {
                //LoadAldmConfig();

                FormControl = AddEditManager.Init_Control(_MA_DM);
                FormControl.IS_COPY = IS_COPY;
                FormControl.SetGrandFather(grandFather);
                _aldmConfig = FormControl._aldmConfig;
                Text = _mode + " - " + FormControl.TitleLang;
                
                if (FormControl is NoRightAddEdit)
                {
                    FormControl.Dock = DockStyle.Fill;
                    string keys_string = "";
                    if (_keys != null)
                    {
                        foreach (KeyValuePair<string, object> item in _keys)
                        {
                            keys_string += " " + item.Value;
                        }
                    }
                    if (!string.IsNullOrEmpty(keys_string)) ((NoRightAddEdit)FormControl).NoRightInfo = keys_string;
                }

                OnAfterInitControl();
                FormControl.InitValues(_MA_DM, _mode, _keys, _data);

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
        
        private void MyInit()
        {
            try
            {
                if (_aldmConfig.HaveInfo)
                {
                    if (_aldmConfig.IS_ALDM)
                    {
                        Text = _mode + " - " + (V6Setting.IsVietnamese ? _aldmConfig.TITLE : _aldmConfig.TITLE2);
                    }
                }
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
        //            return V6ControlsHelper.InvokeMethodDynamic(Form_program, method_name, All_Objects);
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
            //else
            //{
            //    DoReload();
            //    Close();
            //}
        }

        private void DoInsertOrUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            FormControl.All_Objects["dataDic"] = dataDic;
            FormControl.InvokeFormEvent(FormDynamicEvent.AFTERSAVE2);
            FormControl.InvokeFormEvent("AFTERINSERTORUPDATE");
            //InvokeFormEvent();
        }

        private void DoUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            UpdateSuccess = true;
            FormControl.All_Objects["dataDic"] = dataDic;
            FormControl.InvokeFormEvent("AFTERUPDATE");
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(dataDic);
        }

        private void DoInsertSuccess(SortedDictionary<string, object> dataDic)
        {
            InsertSuccess = true;
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
                V6ControlFormHelper.ProcessUserDefineInfo(_MA_DM, FormControl, this, _MA_DM);
            }
        }
    }
}
