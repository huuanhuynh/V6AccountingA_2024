using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.NhanSu.Filter;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.NhanSu.View
{
    public partial class NoGridControl : V6FormControl
    {
        public readonly string FORM_NAME_0, FORM_NAME_NEW;
        public string _stt_rec, TABLE_NAME;
        public AldmConfig _aldmConfig;
        public V6FormControl TopControl;
        public V6FormControl BottomControl;

        public NoGridControl()
        {
            InitializeComponent();
        }

        public NoGridControl(string itemID, string formName0, string formNameNew)
        {
            m_itemId = itemID;
            FORM_NAME_0 = formName0;
            FORM_NAME_NEW = formNameNew;
            TABLE_NAME = formNameNew.Substring(1).ToUpper();
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                _aldmConfig = ConfigManager.GetAldmConfigByTableName(TABLE_NAME);
                AddFilterControl(FORM_NAME_NEW);
                BottomControl = NhanSuManager.GetControl(ItemID, FORM_NAME_NEW) as V6FormControl;
                if (BottomControl != null)
                {
                    BottomControl.Dock = DockStyle.Fill;
                    panelBottom.Controls.Add(BottomControl);
                    //if (BottomControl is AddEditControlVirtual)
                    {
                        V6ControlFormHelper.LoadAndSetFormInfoDefine(TABLE_NAME, BottomControl, this);
                        LoadAdvanceControls(BottomControl, TABLE_NAME);
                    }
                    V6ControlFormHelper.SetFormControlsReadOnly(BottomControl, true);
                }

                TopControl = NhanSuManager.GetControl(ItemID, "HINFOR_NS") as V6FormControl;
                if (TopControl != null)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(TopControl, true);
                    panelTop.Controls.Add(TopControl);
                }

                All_Objects["thisForm"] = this;
                CreateFormProgram();
                V6ControlFormHelper.ApplyDynamicFormControlEvents(this, null, Form_program, All_Objects);
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME_NEW), ex);
            }
        }

        private void NoGridControl_Load(object sender, EventArgs e)
        {
            InvokeFormEvent(FormDynamicEvent.INIT2);
        }

        protected void CreateFormProgram()
        {
            try
            {
                All_Objects["thisForm"] = this;
                //DMETHOD
                if (_aldmConfig.NoInfo || string.IsNullOrEmpty(_aldmConfig.DMETHOD))
                {
                    return;
                }

                string using_text = "";
                string method_text = "";
                //foreach (DataRow dataRow in Invoice.Alct1.Rows)
                {
                    var xml = _aldmConfig.DMETHOD;
                    if (xml == "") return;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) return;
                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        var method_name = event_row["method"].ToString().Trim();
                        Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }

                Build:
                Form_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateProgram0", ex);
            }
        }

        public FilterBase FilterControl { get; set; }
        protected void AddFilterControl(string program)
        {
            FilterControl = NhanSuFilterManager.GetFilterControl(program);
            panelFilter.Controls.Add(FilterControl);
            FilterControl.Focus();
        }
        
        public void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, FORM_NAME_NEW.Substring(1));
        }

        protected void LoadAdvanceControls(Control form, string ma_bc)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_bc, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + ma_bc, ex);
            }
        }

        
        public void HideFilterControl()
        {
            try
            {
                panelFilter.Visible = false;
                btnNhan.Visible = false;
                btnHuy.Visible = false;
                panelBottom.Left = 1;
                panelBottom.Width = Width - 2;
                panelTop.Left = 1;
                panelTop.Width = Width - 2;
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME_NEW), ex);
            }
        }

        public override void LoadData(string no_code)
        {
            try
            {
                SqlParameter[] plist = FilterControl.GetFilterParameters().ToArray();
                //Find stt_rec
                foreach (SqlParameter parameter in plist)
                {
                    if (parameter.ParameterName.ToUpper().EndsWith("STT_REC"))
                    {
                        _stt_rec = parameter.Value.ToString();
                        break;
                    }
                }
                var ds = V6BusinessHelper.ExecuteProcedure(FORM_NAME_NEW, plist);
                
                if (ds.Tables.Count > 0)
                {
                    DataRow datarow = ds.Tables[0].Rows[0];
                    var dataDic = datarow.ToDataDictionary();
                    TopControl.SetData(dataDic);
                    BottomControl.SetData(dataDic);
                }
                if (ds.Tables.Count > 1)
                {
                    DataRow datarow = ds.Tables[1].Rows[0];
                    var dataDic = datarow.ToDataDictionary();
                    
                    BottomControl.SetSomeData(dataDic);
                }

            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("LoadData error: " + ex.Message);
            }
        }

     
        public override void SetParentData(IDictionary<string, object> nhanSuData)
        {
            FilterControl.SetParentRow(nhanSuData);
            //V6ControlFormHelper.SetSomeDataDictionary(ThongTinControl2, nhanSuData);
            TopControl.SetData(nhanSuData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy.PerformClick();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
    }
}
