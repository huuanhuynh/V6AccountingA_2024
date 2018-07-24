using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private readonly string _formname;
        private string _stt_rec, _table_name;
        private DataTable _gridViewData, _inforData;
        private V6FormControl ThongTinControl1;
        private V6FormControl ThongTinControl2;
        public OneGridControl()
        {
            InitializeComponent();
        }

        public OneGridControl(string itemID, string formname)
        {
            m_itemId = itemID;
            _formname = formname;
            _table_name = _formname.Substring(1);
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                AddFilterControl(_formname);
                ThongTinControl1 = NhanSuManager.GetControl(ItemID, _formname) as V6FormControl;
                if (ThongTinControl1 != null)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(ThongTinControl1, true);
                    ThongTinControl1.Dock = DockStyle.Fill;
                    panelControl.Controls.Add(ThongTinControl1);
                }

                ThongTinControl2 = NhanSuManager.GetControl(ItemID, "HINFOR_NS") as V6FormControl;
                if (ThongTinControl2 != null)
                {
                    V6ControlFormHelper.SetFormControlsReadOnly(ThongTinControl2, true);
                    panelControl2.Controls.Add(ThongTinControl2);
                }

                // SetData...
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _formname), ex);
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

                var CurrentTable = V6TableHelper.ToV6TableName(_formname.Substring(1));
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("TableError! " + _formname);
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
                    //    var f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, null);
                    //    f.InsertSuccessEvent += f_InsertSuccess;
                    //    f.ShowDialog(this);
                    //}
                    //else
                    {
                        SortedDictionary<string, object> _data = new SortedDictionary<string, object>();
                        _data["STT_REC"] = _stt_rec;
                        _data["STT_REC0"] = V6BusinessHelper.GetNewSttRec0(_gridViewData);
                        var f = new FormAddEdit(CurrentTable, V6Mode.Add, null, _data);
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

        private void DoEdit()
        {
            try
            {
                SaveSelectedCellLocation(gridView1);
                var CurrentTable = V6TableHelper.ToV6TableName(_formname.Substring(1));
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("TableError! " + CurrentTable);
                }
                else
                {
                    DataGridViewRow row = gridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (gridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);
                        keys["STT_REC"] = _stt_rec;

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        if (gridView1.Columns.Contains(keyField))
                        //        {
                        //            keys[keyField] = row.Cells[keyField].Value;
                        //        }
                        //    }

                        //var _data = row.ToDataDictionary();
                        var f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, null);
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage("Hãy chọn một dòng dữ liệu!");
                    }
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

                        //var id = V6Lookup.ValueByTableName[CurrentTable.ToString(), "vValue"].ToString().Trim();
                        //var listTable =
                        //    V6Lookup.ValueByTableName[CurrentTable.ToString(), "ListTable"].ToString().Trim();
                        var value = "";

                        if (gridView1.Columns.Contains("UID"))
                        {
                            var keys = new SortedDictionary<string, object> { { "UID", row.Cells["UID"].Value } };

                            if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + value, V6Text.DeleteConfirm)
                                == DialogResult.Yes)
                            {
                                confirm = true;
                                t = V6BusinessHelper.Delete(_table_name, keys);

                                if (t > 0)
                                {
                                    
                                }
                            }
                        }
                        else
                        {
                            //this.ShowWarningMessage("Không có khóa UID. Vẫn xóa!");

                            //_categories.Delete(CurrentTable, _data);
                        }
                    }

                    if (confirm)
                    {
                        if (t > 0)
                        {
                            ReLoad();
                            V6ControlFormHelper.ShowMainMessage("Đã xóa!");
                        }
                        else
                        {
                            V6ControlFormHelper.ShowMessage("Xóa chưa được!");
                        }
                    }

                    //var aev = AddEditManager.Init_Control(CurrentTable); //ảo
                    //if (!string.IsNullOrEmpty(aev.KeyField1))
                    //{
                    //    var oldKey1 = _data[aev.KeyField1].ToString().Trim();
                    //    var oldKey2 = "";
                    //    if (!string.IsNullOrEmpty(aev.KeyField2) && _data.ContainsKey(aev.KeyField2))
                    //        oldKey2 = _data[aev.KeyField2].ToString().Trim();
                    //    var oldKey3 = "";
                    //    if (!string.IsNullOrEmpty(aev.KeyField3) && _data.ContainsKey(aev.KeyField3))
                    //        oldKey3 = _data[aev.KeyField3].ToString().Trim();

                    //    var uid = _data.ContainsKey("UID") ? _data["UID"].ToString() : "";

                    //    V6ControlFormHelper.Copy_Here2Data(CurrentTable, V6Mode.Delete,
                    //        aev.KeyField1, aev.KeyField2, aev.KeyField3,
                    //        oldKey1, oldKey2, oldKey3,
                    //        oldKey1, oldKey2, oldKey3,
                    //        uid);
                    //}
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
        private void f_UpdateSuccess(SortedDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = gridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
                gridView1_SelectionChanged(null, null);
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

        private void f_InsertSuccess(SortedDictionary<string, object> data)
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
            LoadData(_formname);

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
                panelControl.Left = 1;
                panelControl.Width = Width - 2;
                panelControl2.Left = 1;
                panelControl2.Width = Width - 2;
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _formname), ex);
            }
        }

        public override void LoadData(string no_code)
        {
            try
            {
                FormManagerHelper.HideMainMenu();

                // Reset
                V6ControlFormHelper.SetFormDataDictionary(ThongTinControl1,null);
                V6ControlFormHelper.SetFormDataDictionary(ThongTinControl2, null);
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
                var ds = V6BusinessHelper.ExecuteProcedure(_formname, plist);
                
                if (ds.Tables.Count > 0)
                {
                    //{Tuanmh 15/09/2017
                    //1.Infor personal 

                    _inforData = ds.Tables[0];
                    if (_inforData.Rows.Count > 0)
                    {
                        V6ControlFormHelper.SetFormDataDictionary(ThongTinControl2, _inforData.Rows[0].ToDataDictionary());
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
                            V6ControlFormHelper.SetFormDataDictionary(ThongTinControl1, _gridViewData.Rows[0].ToDataDictionary());
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
                var config = V6ControlsHelper.GetAldmConfigByTableName(_table_name);
                V6ControlFormHelper.FormatGridViewAndHeader(gridView1, config.GRDS_V1, config.GRDF_V1,
                    V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _formname), ex);
            }
        }

        public override void SetParentData(IDictionary<string, object> nhanSuData)
        {
            FilterControl.SetParentRow(nhanSuData);
            //V6ControlFormHelper.SetSomeDataDictionary(ThongTinControl2, nhanSuData);
            ThongTinControl2.SetData(nhanSuData);
        }
       
        private void gridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.CurrentRow != null && _gridViewData != null)
                {
                    var selectedData = gridView1.CurrentRow.ToDataDictionary();
                    V6ControlFormHelper.SetFormDataDictionary(ThongTinControl1, selectedData);
                    //V6ControlFormHelper.SetFormDataDictionary(ThongTinControl2, selectedData);

                    //Làm lại. Lấy key dữ liệu. Tải đúng bảng dữ liệu rồi gán lên form.
                    //ThongTinControl.SetDataKeys(selectedData);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _formname), ex);
            }
        }

        private void gridView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    DoEdit();
                }
                else if (e.KeyCode == Keys.F4)
                {
                    DoAdd();
                }
                else if (e.KeyCode == Keys.F8)
                {
                    DoDelete();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0}.{1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _formname, ex.Message));
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
