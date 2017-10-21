using System;
using System.Collections.Generic;
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
        private readonly string _tableNameString;
        private string _tableView;
        
        public event HandleResultData InsertSuccessEvent;
        public event HandleResultData UpdateSuccessEvent;
        public event EventHandler CallReloadEvent;

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
            SortedDictionary<string, object> keys = null,
            SortedDictionary<string, object> data = null)
        {
            _tableName = tableName;
            _tableNameString = tableName.ToString();
            InitializeComponent();
            
            FormControl = AddEditManager.Init_Control(tableName, tableName.ToString());
            
            _tableName = tableName;
            FormControl.MyInit(tableName, mode, keys, data);

            panel1.Controls.Add(FormControl);
            //panel1.SendToBack();

            if (FormControl == null || FormControl is NoRightAddEdit)
            {
                btnNhan.Enabled = false;
                btnInfos.Visible = false;
            }
        }

        /// <summary>
        /// Sử dụng tableName string khi chưa khai báo V6TableName và table phải có khai báo is_dm trong aldm.
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="mode"></param>
        /// <param name="keys">Khóa lấy dữ liệu</param>
        /// <param name="data">Hoặc dữ liệu có sẵn</param>
        public FormAddEdit(string tableName, V6Mode mode = V6Mode.Add,
            SortedDictionary<string, object> keys = null,
            SortedDictionary<string, object> data = null)
        {
            _tableNameString = tableName;
            _tableName = V6TableHelper.ToV6TableName(_tableNameString);
            InitializeComponent();

            FormControl = AddEditManager.Init_Control(_tableName, _tableNameString);
            //_tableName = tableName;

            FormControl.MyInit(_tableName, mode, keys, data);
            
            panel1.Controls.Add(FormControl);
            //panel1.SendToBack();

            if (FormControl == null || FormControl is NoRightAddEdit)
            {
                btnNhan.Enabled = false;
                btnInfos.Visible = false;
            }
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

        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            Text = FormControl.Mode + " - " + V6TableHelper.V6TableCaption(_tableName, V6Setting.Language);
            bool is_aldm = false;
            IDictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("MA_DM", _tableNameString);
            var aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
            if (aldm.Rows.Count == 1)
            {
                is_aldm = aldm.Rows[0]["IS_ALDM"].ToString() == "1";
                _tableView = aldm.Rows[0]["TABLE_VIEW"].ToString();

                if (is_aldm)
                {
                    Text = FormControl.Mode + " - " + (V6Setting.IsVietnamese ? aldm.Rows[0]["TITLE"] : aldm.Rows[0]["TITLE2"]);
                }
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {

            if (FormControl.DoInsertOrUpdate())
            {
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

        private void DoUpdateSuccess(SortedDictionary<string, object> dataDic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(dataDic);
        }

        private void DoInsertSuccess(SortedDictionary<string, object> dataDic)
        {
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
            V6ControlFormHelper.ProcessUserDefineInfo(
                string.IsNullOrEmpty(_tableView) ?_tableNameString : _tableView,
                FormControl, this, _tableName.ToString());
        }

    }
}
