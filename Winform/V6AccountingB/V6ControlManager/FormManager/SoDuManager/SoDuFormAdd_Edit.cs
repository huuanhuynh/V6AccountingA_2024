using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager
{
    public partial class SoDuFormAddEdit : V6Form
    {
        public SoDuAddEditControlVirtual FormControl;
        private V6TableName _tableName;
        public delegate void SoDuSuccessHandleData(SoDuAddEditControlVirtual sender, SortedDictionary<string, object> dataDic);

        public event SoDuSuccessHandleData InsertSuccessEvent;
        public event SoDuSuccessHandleData UpdateSuccessEvent;
        
        
        public SoDuFormAddEdit()
        {
            InitializeComponent();
        }

        public SoDuFormAddEdit(V6TableName tableName, V6Mode mode = V6Mode.Add,
            SortedDictionary<string, object> keys = null,
            SortedDictionary<string, object> data = null)
        {
            InitializeComponent();
            
            FormControl = SoDuManager.GetAddEditControl(tableName);
            _tableName = tableName;
            FormControl.MyInit(tableName, mode, keys, data);
            
            panel1.Controls.Add(FormControl);
        }
        
        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            Text = FormControl.Mode + " - " + V6TableHelper.V6TableCaption(_tableName, V6Setting.Language);
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            if (FormControl.DoInsertOrUpdate())
            {
                if (FormControl.Mode == V6Mode.Add)
                {
                    V6ControlFormHelper.SetStatusText(V6Text.AddSuccess);
                    ShowMainMessage(V6Text.AddSuccess);
                    OnInsertSuccessEvent(FormControl, FormControl.DataDic);
                }
                if (FormControl.Mode == V6Mode.Edit)
                {
                    V6ControlFormHelper.SetStatusText(V6Text.EditSuccess);
                    ShowMainMessage(V6Text.EditSuccess);
                    OnUpdateSuccessEvent(FormControl, FormControl.DataDic);
                }
                Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        protected virtual void OnInsertSuccessEvent(SoDuAddEditControlVirtual sender, SortedDictionary<string, object> datadic)
        {
            var handler = InsertSuccessEvent;
            if (handler != null) handler(sender, datadic);
        }

        protected virtual void OnUpdateSuccessEvent(SoDuAddEditControlVirtual sender, SortedDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(sender, datadic);
        }

        private void btnInfos_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.ProcessUserDefineInfo(_tableName.ToString(), FormControl, this, _tableName.ToString());
        }
    }
}
