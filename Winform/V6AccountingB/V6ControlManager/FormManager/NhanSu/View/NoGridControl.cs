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
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.NhanSu.View
{
    public partial class NoGridControl : V6FormControl
    {
        private readonly string _formname;
        private string _stt_rec, _table_name;
        private V6FormControl ThongTinControl;
        private V6FormControl ThongTinControl2;
        public NoGridControl()
        {
            InitializeComponent();
        }

        public NoGridControl(string itemID, string formname)
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
                ThongTinControl = NhanSuManager.GetControl(ItemID, _formname) as V6FormControl;
                if (ThongTinControl != null)
                {
                    ThongTinControl.Dock = DockStyle.Fill;
                    panelControl.Controls.Add(ThongTinControl);
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
                //SaveSelectedCellLocation

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
                //SaveSelectedCellLocation
                var CurrentTable = V6TableHelper.ToV6TableName(_formname.Substring(1));
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("TableError! " + CurrentTable);
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEdit: " + ex.Message);
            }
        }

        private void DoDelete()
        {
            try
            {
               
               
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Xóa lỗi!\n" + ex.Message);
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
              }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".f_UpdateSuccess: " + ex.Message);
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
                    DataRow datarow = ds.Tables[0].Rows[0];
                    var dataDic = datarow.ToDataDictionary();
                    ThongTinControl2.SetData(dataDic);
                    ThongTinControl.SetData(dataDic);
                    //V6ControlFormHelper.SetFormDataRow(ThongTinControl2, datarow);
                    //V6ControlFormHelper.SetFormDataRow(ThongTinControl, datarow);
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
            ThongTinControl2.SetData(nhanSuData);
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
