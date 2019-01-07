using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class STICK_NOTE_F5 : V6Form
    {
        #region Biến toàn cục

        protected DataTable _data;
        protected V6Mode _mode;

        public event HandleResultData InsertSuccessEvent;
        protected virtual void OnInsertSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = InsertSuccessEvent;
            if (handler != null) handler(datadic);
        }
        public event HandleResultData UpdateSuccessEvent;
        protected virtual void OnUpdateSuccessEvent(IDictionary<string, object> datadic)
        {
            var handler = UpdateSuccessEvent;
            if (handler != null) handler(datadic);
        }

        protected DataSet _ds;
        protected DataTable _tbl, _tbl2;
        //private V6TableStruct _tStruct;
        /// <summary>
        /// Dùng cho procedure chính (program?)
        /// </summary>
        protected SqlParameter[] _plist;

        public bool ViewDetail { get; set; }
        
        
        #endregion 

        #region ==== Properties ====


        #endregion properties
        public STICK_NOTE_F5()
        {
            InitializeComponent();
        }

        public STICK_NOTE_F5(SqlParameter[] plist)
        {
            _data = LoadData(plist);
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                dataGridView1.DataSource = _data;
                FormatGridView();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private DataTable LoadData(SqlParameter[] plist)
        {
            _plist = plist;
            var ds = V6BusinessHelper.ExecuteProcedure("V6STICK_NOTEF5", plist);
            //Hien thi du lieu lay duoc
            if (ds != null && ds.Tables.Count > 0)
            {
                _data = ds.Tables[0];
                return _data;
            }
            return null;
        }

        private void ReLoadData()
        {
            dataGridView1.DataSource = LoadData(_plist);
        }

        private void FormatGridView()
        {
            try
            {
                var config = ConfigManager.GetAldmConfigByTableName(_table_name);
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, config.GRDS_V1, config.GRDF_V1,
                    V6Setting.IsVietnamese ? config.GRDHV_V1 : config.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, Name), ex);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            SetStatus2Text();
        }

        private void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3-Sửa, F4-Thêm");
        }

        protected int _oldIndex = -1;

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                DoEdit();
            }
            else if (keyData == Keys.F4)
            {
                DoAdd();
            }
            else if (keyData == Keys.F8)
            {
                DoDelete();
            }
            else if (keyData == Keys.Escape)
            {
                Close();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }

        private string _table_name = "V6NOTESCT";
        public string Ma_nvien;
        public DateTime Ngay;

        private void DoEdit()
        {
            try
            {
                if (!V6Login.UserRight.AllowEdit("", _table_name))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                if (row == null)
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                    return;
                }

                var currentRowData = row.ToDataDictionary();
                var form = new STICK_NOTE_F3F4(V6Mode.Edit, currentRowData);
                
                form.UpdateSuccessEvent += data =>
                {
                    V6ControlFormHelper.UpdateGridViewRow(row, data);
                };
                form.ShowDialog(this);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void DoAdd()
        {
            try
            {
                if (!V6Login.UserRight.AllowAdd("", _table_name))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                IDictionary<string, object> currentRowData = new SortedDictionary<string, object>();
                if (row != null) currentRowData = row.ToDataDictionary();
                currentRowData["MA_NVIEN"] = Ma_nvien;
                currentRowData["NGAY"] = Ngay;

                var form = new STICK_NOTE_F3F4(V6Mode.Add, currentRowData);
                form.InsertSuccessEvent += data =>
                {
                    ReLoadData();
                    ShowMainMessage(V6Text.AddSuccess);
                };
                form.ShowDialog(this);

                SetStatus2Text();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoAdd", ex);
            }
        }

        private void DoDelete()
        {
            try
            {
                if (!V6Login.UserRight.AllowDelete("", _table_name))
                {
                    V6ControlFormHelper.NoRightWarning();
                    return;
                }

                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                if (row == null) return;

                if (this.ShowConfirmMessage(V6Text.DeleteConfirm + " " + row.Cells["TYPE"].Value, V6Text.Delete)
                    != DialogResult.Yes)
                {
                    return;
                }

                IDictionary<string, object> key = new SortedDictionary<string, object>();
                key.Add("UID", row.Cells["UID"].Value);
                if (V6BusinessHelper.Delete(_table_name, key) > 0)
                {
                    ReLoadData();
                    ShowMainMessage(V6Text.Deleted);
                }
                else
                {
                    ShowMainMessage(V6Text.DeleteFail);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoDelete", ex);
            }
        }
    }
}
