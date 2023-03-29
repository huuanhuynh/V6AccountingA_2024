using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6ControlManager.FormManager.ChungTuManager;
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
        public bool IS_COPY;

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
            try
            {
                //_tableName = tableName;
                _MA_DM = ma_dm.ToUpper();
                FormControl = SoDuManager.GetAddEditControl(_MA_DM);
                FormControl.IS_COPY = IS_COPY;
                FormControl.InitValues(_MA_DM, mode, keys, data);
                panel1.Controls.Add(FormControl);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ctor", ex);
            }
        }
        
        private void FormAdd_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                Text = FormControl.Mode + " - " + FormControl.TitleLang;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "_Load", ex);
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

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                var detail1 = FormControl.GetControlByName("detail1") as HD_Detail;
                if (detail1 != null && (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit))
                {
                    if (detail1.MODE == V6Mode.Add)
                    {
                        //if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                        detail1.OnMoiClick();//.btnMoi.PerformClick();
                    }
                    else if (detail1.MODE == V6Mode.Edit)
                    {
                        //if (tabControl1.SelectedTab != tabChiTiet) tabControl1.SelectedTab = tabChiTiet;
                        detail1.btnSua.PerformClick();
                    }
                    return true;
                }
                else
                {
                    return base.DoHotKey0(keyData);
                }
            }
            return base.DoHotKey0(keyData);
        }
    }
}
