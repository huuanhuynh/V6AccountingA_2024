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
    public partial class OneGridControl : V6FormControl
    {
        public readonly string FORM_NAME_0, FORM_NAME_NEW;
        public string _stt_rec, TABLE_NAME;
        public AldmConfig _aldmConfig;
        public DataTable _gridViewData, _inforData;
        public V6FormControl BottomControl;
        public V6FormControl TopControl;
        public OneGridControl()
        {
            InitializeComponent();
        }

        public OneGridControl(string itemID, string formName0, string formNameNew)
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
                    if (BottomControl is AddEditControlVirtual)
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

        private void OneGridControl_Load(object sender, EventArgs e)
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

        private void DoAdd()
        {
            try
            {
                SaveSelectedCellLocation(gridView1);

                SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
                _data["STT_REC"] = _stt_rec;
                _data["STT_REC0"] = V6BusinessHelper.GetNewSttRec0(_gridViewData);
                var f = new FormAddEdit(TABLE_NAME, V6Mode.Add, null, _data);
                f.AfterInitControl += f_AfterInitControl;
                f.InitFormControl(this);
                f.InsertSuccessEvent += f_InsertSuccess;
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
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

        private void DoEdit()
        {
            try
            {
                SaveSelectedCellLocation(gridView1);
                
                DataGridViewRow row = gridView1.GetFirstSelectedRow();
                if (row != null)
                {
                    var keys = new SortedDictionary<string, object>();
                    if (gridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                        keys.Add("UID", row.Cells["UID"].Value);
                    keys["STT_REC"] = _stt_rec;

                    var f = new FormAddEdit(TABLE_NAME, V6Mode.Edit, keys, null);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    f.UpdateSuccessEvent += f_UpdateSuccess;
                    f.CallReloadEvent += FCallReloadEvent;
                    f.ShowDialog(this);
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void DoDelete()
        {
            try
            {
                SaveSelectedCellLocation(gridView1);
                DataGridViewRow row = gridView1.GetFirstSelectedRow();
                var confirm = false;
                var t = 0;

                if (row != null)
                {
                    var _data = row.ToDataDictionary();
                    {

                        //var id = V6Lookup.GetValueByTableName(CurrentTable.ToString(), "vValue"].ToString().Trim();
                        //var listTable =
                        //    V6Lookup.GetValueByTableName(CurrentTable.ToString(), "ListTable"].ToString().Trim();
                        var value = "";

                        if (gridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, V6Text.DeleteConfirm)
                                == DialogResult.Yes)
                            {
                                confirm = true;
                                t = V6BusinessHelper.Delete(TABLE_NAME, keys);

                                if (t > 0)
                                {
                                    All_Objects["data"] = _data;
                                    InvokeFormEvent(FormDynamicEvent.AFTERDELETESUCCESS);
                                }
                            }
                        }
                        else
                        {
                            //this.ShowWarningMessage(V6Text.NoUID);

                            //_categories.Delete(CurrentTable, _data);
                        }
                    }

                    if (confirm)
                    {
                        if (t > 0)
                        {
                            ReLoad();
                            V6ControlFormHelper.ShowMainMessage(V6Text.Deleted);
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMessage(V6Text.DeleteFail);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoDelete", ex);
            }
        }

        
        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                ReLoad();

                //if (data == null) return;
                //DataGridViewRow row = gridView1.GetFirstSelectedRow();
                //V6ControlFormHelper.UpdateGridViewRow(row, data);
                //gridView1_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs eventArgs)
        {
            ReLoad();
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
            LoadData(FORM_NAME_NEW);
            LoadSelectedCellLocation(gridView1);
            gridView1_SelectionChanged(null, null);
        }

        public void HideFilterControl()
        {
            try
            {
                panelFilter.Visible = false;
                btnNhan.Visible = false;
                btnHuy.Visible = false;
                gridView1.Left = 1;
                gridView1.Width = Width - 2;
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
                FormManagerHelper.HideMainMenu();

                // Reset
                V6ControlFormHelper.SetFormDataDictionary(BottomControl,null);
                V6ControlFormHelper.SetFormDataDictionary(TopControl, null);
                _gridViewData = null;
                gridView1.DataSource = null;
                

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
                    //{Tuanmh 15/09/2017
                    //1.Infor personal 

                    _inforData = ds.Tables[0];
                    if (_inforData.Rows.Count > 0)
                    {
                        V6ControlFormHelper.SetFormDataDictionary(TopControl, _inforData.Rows[0].ToDataDictionary());
                    }


                    //2.Data gridview
                    if (ds.Tables.Count > 1)
                    {
                        _gridViewData = ds.Tables[1];
                        gridView1.DataSource = _gridViewData;
                        FormatGridView();
                        gridView1.Focus();
                        if (_gridViewData.Rows.Count > 0)
                        {
                            V6ControlFormHelper.SetFormDataDictionary(BottomControl, _gridViewData.Rows[0].ToDataDictionary());
                        }
                    }
                    gridView1.Focus();
                    //}
                }
                
            }
            catch (Exception ex)
            {
                this.ShowWarningMessage("LoadData error: " + ex.Message);
            }
        }

        private void FormatGridView()
        {
            try
            {
                V6ControlFormHelper.FormatGridViewAndHeader(gridView1, _aldmConfig.GRDS_V1, _aldmConfig.GRDF_V1,
                    V6Setting.IsVietnamese ? _aldmConfig.GRDHV_V1 : _aldmConfig.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME_NEW), ex);
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

        /// <summary>
        /// Kiểm tra có cấu hình Fpass
        /// </summary>
        /// <param name="index">0 cho F3, 1 cho F6, 2 cho F8</param>
        /// <returns></returns>
        bool NO_CONFIG_FPASS(int index)
        {
            if (_aldmConfig.NoInfo) return true;
            if (!_aldmConfig.EXTRA_INFOR.ContainsKey("F368_PASS")) return true;
            string f368 = _aldmConfig.EXTRA_INFOR["F368_PASS"].Trim();
            if (f368.Length > index && f368[index] == '1') return false;
            return true;
        }
       
        private void gridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.CurrentRow != null && _gridViewData != null)
                {
                    var selectedData = gridView1.CurrentRow.ToDataDictionary();
                    V6ControlFormHelper.SetFormDataDictionary(BottomControl, selectedData);
                    //V6ControlFormHelper.SetFormDataDictionary(ThongTinControl2, selectedData);

                    //Làm lại. Lấy key dữ liệu. Tải đúng bảng dữ liệu rồi gán lên form.
                    //ThongTinControl.SetDataKeys(selectedData);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME_NEW), ex);
            }
        }

        private void gridView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    if (V6Login.UserRight.AllowEdit("", TABLE_NAME.ToUpper() + "6"))
                    {
                        if (NO_CONFIG_FPASS(0) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
                        {
                            DoEdit();
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
                else if (e.KeyCode == Keys.F4)
                {
                    DoAdd();
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (V6Login.UserRight.AllowDelete("", TABLE_NAME.ToUpper() + "6"))
                    {
                        if (NO_CONFIG_FPASS(2) || new ConfirmPasswordF368().ShowDialog(this) == DialogResult.OK)
                        {
                            DoDelete();
                        }
                    }
                    else
                    {
                        V6ControlFormHelper.NoRightWarning();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0}.{1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, FORM_NAME_NEW, ex.Message));
            }
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
