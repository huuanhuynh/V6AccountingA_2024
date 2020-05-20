using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager
{
    public partial class SoDuFormAddEdit : V6Form
    {
        public SoDuAddEditControlVirtual FormControl;
        //private V6TableName _tableName;
        private string _MA_DM;
        public delegate void SoDuSuccessHandleData(SoDuAddEditControlVirtual sender, IDictionary<string, object> datadic);

        public event SoDuSuccessHandleData InsertSuccessEvent;
        public event SoDuSuccessHandleData UpdateSuccessEvent;
        
        
        public SoDuFormAddEdit()
        {
            InitializeComponent();
        }

        public SoDuFormAddEdit(string ma_dm, V6Mode mode = V6Mode.Add,
            IDictionary<string, object> keys = null,
            IDictionary<string, object> data = null)
        {
            InitializeComponent();
            //_tableName = tableName;
            _MA_DM = ma_dm.ToUpper();
            FormControl = SoDuManager.GetAddEditControl(_MA_DM);
            
            FormControl.InitValues(_MA_DM, mode, keys, data);
            
            panel1.Controls.Add(FormControl);
        }
        
        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            Text = FormControl.Mode + " - " + FormControl.TitleLang;
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

        protected virtual void OnInsertSuccessEvent(SoDuAddEditControlVirtual sender, IDictionary<string, object> datadic)
        {
            try
            {
                var handler = InsertSuccessEvent;
                if (handler != null) handler(sender, datadic);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".OnInsertSuccessEvent", ex);
            }
        }

        protected virtual void OnUpdateSuccessEvent(SoDuAddEditControlVirtual sender, IDictionary<string, object> datadic)
        {
            try
            {
                var handler = UpdateSuccessEvent;
                if (handler != null) handler(sender, datadic);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".OnUpdateSuccessEvent", ex);
            }
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
