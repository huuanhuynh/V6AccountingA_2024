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
        private readonly string FORM_NAME;
        private string _stt_rec, TABLE_NAME;
        private AldmConfig _aldmConfig;
        private V6FormControl TopControl;
        private V6FormControl BottomControl;

        public NoGridControl()
        {
            InitializeComponent();
        }

        public NoGridControl(string itemID, string formName)
        {
            m_itemId = itemID;
            FORM_NAME = formName;
            TABLE_NAME = FORM_NAME.Substring(1).ToUpper();
            _aldmConfig = ConfigManager.GetAldmConfigByTableName(TABLE_NAME);
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                AddFilterControl(FORM_NAME);
                BottomControl = NhanSuManager.GetControl(ItemID, FORM_NAME) as V6FormControl;
                if (BottomControl != null)
                {
                    BottomControl.Dock = DockStyle.Fill;
                    panelBottom.Controls.Add(BottomControl);
                }

                TopControl = NhanSuManager.GetControl(ItemID, "HINFOR_NS") as V6FormControl;
                if (TopControl != null)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(TopControl, true);
                    panelTop.Controls.Add(TopControl);
                }

                All_Objects["thisForm"] = this;
                CreateFormProgram();
                V6ControlFormHelper.ApplyDynamicFormControlEvents(this, Event_program, All_Objects);
                InvokeFormEvent(FormDynamicEvent.INIT);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME), ex);
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
                Event_program = V6ControlsHelper.CreateProgram("DynamicFormNameSpace", "DynamicFormClass", "D" + _aldmConfig.MA_DM, using_text, method_text);
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

        private void DoAdd()
        {
            try
            {
                //SaveSelectedCellLocation

                var CurrentTable = V6TableHelper.ToV6TableName(FORM_NAME.Substring(1));
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("TableError! " + FORM_NAME);
                }
                else
                {
                    //DataGridViewRow row = gridView1.GetFirstSelectedRow();

                    //if (row != null)
                    //{
                    //    var keys = new SortedDictionary<string, object>();
                    //    if (gridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                    //        keys.Add("UID", row.Cells["UID"].Value);
                    //    keys["STT_REC"] = _stt_rec;
                    //    //keys["STT_REC0"] = row.Cells["STT_REC0"].Value;

                    //    //if (KeyFields != null)
                    //    //    foreach (var keyField in KeyFields)
                    //    //    {
                    //    //        if (gridView1.Columns.Contains(keyField))
                    //    //        {
                    //    //            keys[keyField] = row.Cells[keyField].Value;
                    //    //        }
                    //    //    }

                    //    //var _data = row.ToDataDictionary();
                    //    var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, keys, null);
                    //    f.InsertSuccessEvent += f_InsertSuccess;
                    //    f.ShowDialog(this);
                    //}
                    //else
                    {
                        SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
                        _data["STT_REC"] = _stt_rec;
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, _data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        public void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, FORM_NAME.Substring(1));
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + FORM_NAME, ex);
            }
        }


        private void f_InsertSuccess(IDictionary<string, object> data)
        {
            try
            {
                ReLoad();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void ReLoad()
        {
            LoadData(FORM_NAME);
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
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME), ex);
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
                var ds = V6BusinessHelper.ExecuteProcedure(FORM_NAME, plist);
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
