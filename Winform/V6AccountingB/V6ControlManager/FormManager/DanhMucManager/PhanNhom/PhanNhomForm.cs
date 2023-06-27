using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.DanhMucManager.PhanNhom
{
    public partial class PhanNhomForm : V6Form
    {
        public PhanNhomForm()
        {
            InitializeComponent();
            MyInit();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupTableName">Tên bảng nhóm (alnhkh)</param>
        /// <param name="dataTableName">Tên bảng dữ liệu (alkh)</param>
        /// <param name="idField"></param>
        /// <param name="field">field NHOM</param>
        public PhanNhomForm(string groupTableName, string dataTableName, string idField = "", string field = "")
        {
            _dataTableName = dataTableName;
            _groupTableNameName = groupTableName;
            _field0 = field;
            _idField0 = idField;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                btnNhan.Enabled = false;
                _lookupConfig = V6Lookup.GetV6lookupConfigByTableName(_dataTableName);
                M_OPTIONS = ObjectAndString.StringToDictionary(V6Options.GetValue("M_V6_ADV_GROUP_F3F4"));
                LoadDefaultData(2, "", _groupTableNameName, "itemid", "");
                AddMagiaA();
                LoadDataThread();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        private void AddMagiaA()
        {
            
            if (_groupTableNameName.ToUpper() == "ALMAGIA")
            {
                comboBox1.Items.Clear();
            }
            if (_dataTableName.ToUpper() == "ALKH")
            {
                comboBox1.Items.Add("Mã giá A");
            }
        }

        private string _groupTableNameName, _dataTableName;
        private V6lookupConfig _lookupConfig;
        private string _field = "", _field0 = "", _idField, _idField0;
        private DataTable _dataGroup = null, _dataGroup_Gia = null, _dataSource = null;
        private DataView _viewGroup, _viewGroup_Gia, _viewData;
        /// <summary>
        /// {TABLENAMEF3:1} {TABLENAMEF4:0}...
        /// </summary>
        private IDictionary<string, object> M_OPTIONS = new Dictionary<string, object>();

        private void PhanNhomForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void LoadDataThread()
        {
            try
            {
                Thread T = new Thread(LoadData);
                T.IsBackground = true;
                T.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        private void LoadData()
        {
            _dataGroup = V6BusinessHelper.SelectTable(_groupTableNameName);
            _dataGroup_Gia = V6BusinessHelper.SelectTable("almagia");
            _dataSource = V6BusinessHelper.SelectTable(_dataTableName);

            //CheckChuaPhanNhom();

            _executesuccess = true;
        }

        private void CheckChuaPhanNhom()
        {
            try
            {
                if (!_dataSource.Columns.Contains(_field))
                {
                    this.ShowWarningMessage("Loại nhóm không tồn tại!");
                    return;
                }
                //Them Loai_nh chưa phân nhóm.
                DataView tempView = new DataView(_dataSource.Copy());
                tempView.RowFilter = string.Format("Isnull({0},'') = ''", _field);
                if (tempView.Count > 0)
                {
                    //Thêm vào dòng group chưa phân nhóm - nếu chưa có.
                    if (_field == "MA_GIA")
                    {
                        DataView tempView2 = new DataView(_dataGroup_Gia.Copy());
                        tempView2.RowFilter = string.Format("Isnull({0},'') = ''", "MA_GIA");
                        if (tempView2.Count == 0)
                        {
                            //for (int i = 1; i <= 9; i++)
                            {
                                var newRow = _dataGroup_Gia.NewRow();
                                //newRow["MA_GIA"] = "";
                                newRow["MA_GIA"] = "";
                                newRow["Ten_gia"] = V6Setting.IsVietnamese ? "Chưa phân nhóm" : "No Group";
                                _dataGroup_Gia.Rows.Add(newRow);
                            }
                        }
                    }
                    else
                    {
                        DataView tempView2 = new DataView(_dataGroup.Copy());
                        tempView2.RowFilter = string.Format("Isnull({0},'') = ''", "Ma_nh");
                        if (tempView2.Count == 0)
                        {
                            for (int i = 1; i <= 9; i++)
                            {
                                var newRow = _dataGroup.NewRow();
                                newRow["Loai_nh"] = "" + i;
                                newRow["Ma_nh"] = "";
                                newRow["Ten_nh"] = V6Setting.IsVietnamese ? "Chưa phân nhóm" : "No Group";
                                _dataGroup.Rows.Add(newRow);
                            }
                        }
                    }
                }
                else
                {
                    //Xóa dòng group chưa phân nhóm
                    if (_field == "MA_GIA")
                    {
                        for (int i = _dataGroup_Gia.Rows.Count - 1; i >= 0; i--)
                        {
                            var row = _dataGroup_Gia.Rows[i];
                            if (row["MA_GIA"].ToString() == "")
                            {
                                _dataGroup_Gia.Rows.Remove(row);
                            }
                        }
                    }
                    else
                    {
                        for (int i = _dataGroup.Rows.Count - 1; i >= 0; i--)
                        {
                            var row = _dataGroup.Rows[i];
                            if (row["Ma_nh"].ToString() == "")
                            {
                                _dataGroup.Rows.Remove(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckChuaPhanNhom " + ex.Message);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_executesuccess)
                {
                    timer1.Stop();
                    progressBar1.Value = 100;
                    progressBar1.Visible = false;
                    btnNhan.Enabled = true;
                    if (comboBox1.SelectedIndex == -1)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.CallSelectedIndexChanged(new EventArgs());
                    }
                    var loai = comboBox1.Text.Right(1);
                    GetFieldNameInfo(loai);
                    CheckChuaPhanNhom();
                    Ready();
                    ViewDataAll();
                }
                else
                {
                    if (progressBar1.Value < 80)
                        progressBar1.Value += 5;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void GetFieldNameInfo(string loai)
        {
            V6TableName name = V6TableHelper.ToV6TableName(_dataTableName);
            if (name == V6TableName.Alkh)
            {
                if (loai == "A")
                {
                    _field = "MA_GIA";
                    _idField = "MA_KH";
                }
                else
                {
                    _field = "NH_KH" + loai;
                    _idField = "MA_KH";
                }                
            }
            else if (name == V6TableName.Alvt)
            {
                _field = "NH_VT" + loai;
                _idField = "MA_VT";
            }
            else if (name == V6TableName.Alvv)
            {
                _field = "NH_VV" + loai;
                _idField = "MA_VV";
            }
            else if (name == V6TableName.Alvitri)
            {
                _field = "NH_VITRI" + loai;
                _idField = "MA_VITRI";
            }
            else if (name == V6TableName.Alphi)
            {
                _field = "NH_PHI" + loai;
                _idField = "MA_PHI";
            }
            else if (name == V6TableName.Alts)
            {
                _field = "NH_TS" + loai;
                _idField = "SO_THE_TS";
            }
            else if (name == V6TableName.Alcc)
            {
                _field = "NH_CC" + loai;
                _idField = "SO_THE_CC";
            }
            else if (name == V6TableName.Alhd)
            {
                _field = "NH_HD" + loai;
                _idField = "MA_HD";
            }
            else if (name == V6TableName.Altk0)
            {
                _field = "NH_TK0" + loai;
                _idField = "TK";
            }
            else if (name == V6TableName.Alku)
            {
                _field = "NH_KU" + loai;
                _idField = "MA_KU";
            }
            else if (name == V6TableName.Almagia)
            {
                _field = "MA_GIA";
                _idField = "MA_GIA";
            }
            else
            {
                _field = _field0 + loai;
                _idField = _idField0;
            }
        }

        
        private void ViewDataAll()
        {
            try
            {
                ViewNhom();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewDataAll " + ex.Message);
            }
        }

        private void ViewNhom()
        {
            try
            {
                if (_viewGroup == null)
                    _viewGroup = new DataView(_dataGroup);
                if (_viewGroup_Gia == null)
                    _viewGroup_Gia = new DataView(_dataGroup_Gia);

                string filter_field = "Loai_nh";
                string sort_field = "Ma_nh";
                string display1 = "Ten_nh", display2 = "Ten_nh2";
                string value_field = "Ma_nh";
                if (comboBox1.Text.Right(1) == "A")
                {
                    filter_field = "MA_GIA";
                    sort_field = "MA_GIA";
                    display1 = "Ten_gia"; display2 = "Ten_gia2";
                    value_field = "Ma_gia";

                    _viewGroup_Gia.RowFilter = "";
                    _viewGroup_Gia.Sort = sort_field;

                    listBoxMaNh.DataSource = _viewGroup_Gia;
                    listBoxMaNh.DisplayMember = V6Setting.IsVietnamese ? display1 : display2;
                    listBoxMaNh.ValueMember = value_field;
                    
                    listBoxMaNh.DisplayMember = V6Setting.IsVietnamese ? display1 : display2;
                    listBoxMaNh.ValueMember = value_field;

                    var viewGroup_Gia2 = new DataView(_viewGroup_Gia.ToTable());
                    
                    cboToGroupList.DataSource = viewGroup_Gia2;
                    cboToGroupList.DisplayMember = V6Setting.IsVietnamese ? "ten_gia" : "ten_gia2";
                    cboToGroupList.ValueMember = "ma_gia";
                }
                else
                {
                    filter_field = "Loai_nh";
                    sort_field = "Ma_nh";
                    display1 = "Ten_nh"; display2 = "Ten_nh2";
                    value_field = "Ma_nh";

                    _viewGroup.RowFilter = filter_field + " = " + (comboBox1.SelectedIndex + 1);
                    _viewGroup.Sort = sort_field;

                    
                    listBoxMaNh.DataSource = _viewGroup;
                    listBoxMaNh.DisplayMember = V6Setting.IsVietnamese ? display1 : display2;
                    listBoxMaNh.ValueMember = value_field;

                    var viewGroup2 = new DataView(_viewGroup.ToTable());

                    
                    cboToGroupList.DataSource = viewGroup2;
                    cboToGroupList.DisplayMember = V6Setting.IsVietnamese ? display1 : display2;
                    cboToGroupList.ValueMember = value_field;
                }
                

                

                
                

                if (listBoxMaNh.Items.Count > 0)
                {
                    listBoxMaNh.SelectedIndex = 0;
                    ViewData(listBoxMaNh.SelectedValue.ToString().Trim());
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewNhom " + ex.Message);
            }
        }

        private void ViewData(string id)
        {
            try
            {
                if (_viewData == null)
                    _viewData = new DataView(_dataSource);

                if (string.IsNullOrEmpty(id))
                {
                    _viewData.RowFilter = string.Format("{0} = '' or {0} is null", _field, id);
                }
                else
                {
                    _viewData.RowFilter = string.Format("{0} = '{1}'", _field, id);
                }
                
                _viewData.Sort = _idField;
                dataGridView1.DataSource = _viewData;
                dataGridView1.Refresh();

                //string showFields = _lookupConfig.GRDS_V1;
                //string formatStrings = _lookupConfig.GRDF_V1;
                string headerString = V6Setting.IsVietnamese ? _lookupConfig.GRDHV_V1 : _lookupConfig.GRDHE_V1;
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, _lookupConfig.GRDS_V1, _lookupConfig.GRDF_V1, headerString);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ViewData", ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            GetFieldNameInfo(comboBox1.Text.Right(1));
            if (!_dataSource.Columns.Contains(_field))
            {
                this.ShowWarningMessage("Loại nhóm không tồn tại!");
                return;
            }
            CheckChuaPhanNhom();
            ViewNhom();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            ViewData(listBoxMaNh.SelectedValue.ToString().Trim());
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReady) return;
            
            if (dataGridView1.DataSource != null && dataGridView1.CurrentRow != null)
            {
                
            }
        }

        private void btnChuyenNhom_Click(object sender, EventArgs e)
        {
            if (IsReady)
            {
                progressBar1.Visible = true;
                ChangeSelectedToNewGroup();
                CheckChuaPhanNhom();
                NhanThread();
            }
        }

        private SortedDictionary<string, string> _changedList = new SortedDictionary<string, string>(); 
        private void ChangeSelectedToNewGroup()
        {
            try
            {
                if (listBoxMaNh.Items.Count == 0)
                {
                    this.ShowWarningMessage("Chưa có nhóm!");
                    return;
                }
                var oldGroup = listBoxMaNh.SelectedValue.ToString().Trim();
                var newGroup = cboToGroupList.SelectedValue.ToString().Trim();
                V6TableName tableName = V6TableHelper.ToV6TableName(_dataTableName);
                if (tableName == V6TableName.Alkh)
                {
                    
                }
                if (!_dataSource.Columns.Contains(_field))
                {
                    this.ShowWarningMessage("Loại nhóm không tồn tại!");
                    return;
                }
                if (newGroup == oldGroup)
                {
                    this.ShowWarningMessage("Nhóm chuyển vào giống nhóm đang chọn!");
                    return;
                }

                if (cboToGroupList.SelectedIndex >= 0)
                {
                    List<DataGridViewRow> rowList = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsSelect())
                        {
                            rowList.Add(row);
                        }
                    }
                    foreach (DataGridViewRow row in rowList)
                    {
                        var rowData = row.ToDataDictionary();
                        var idValue = rowData[_idField.ToUpper()].ToString().Trim();
                        _changedList[idValue] = newGroup;
                        row.Cells[_field].Value = newGroup;
                    }
                }

                dataGridView1.Refresh();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = _viewData;
                listBox1_SelectedIndexChanged(listBoxMaNh, new EventArgs());
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChangeSelectedToNewGroup " + ex.Message);
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            NhanThread();
        }

        private int _totalListCount;
        private int _updateCount;
        private bool _updateFinish;
        

        private void NhanThread()
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            _totalListCount = _changedList.Count;
            _updateCount = 0;
            _updateFinish = false;
            btnNhan.Enabled = false;
            btnChuyenNhom.Enabled = false;
            Thread T = new Thread(Nhan);
            T.IsBackground = true;
            T.Start();
            timer2.Start();
        }

        private void Nhan()
        {
            try
            {
                foreach (KeyValuePair<string, string> item in _changedList)
                {
                    SortedDictionary<string, object> data = new SortedDictionary<string, object>();
                    data.Add(_field.ToUpper(), item.Value);
                    SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                    keys.Add(_idField.ToUpper(), item.Key);

                    if (V6BusinessHelper.UpdateSimple(_dataTableName, data, keys) > 0)
                        _updateCount++;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Nhan", ex);
            }
            _updateFinish = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (_updateFinish)
            {
                timer2.Stop();
                progressBar1.Value = 100;
                progressBar1.Visible = false;
                string message = string.Format("Đã chuyển {0}.", _updateCount);
                if (_updateCount == 0) message += " Chọn dòng cần chuyển bằng SpaceBar hoặc Ctrl+A.";
                ShowMainMessage(message);
                _changedList = new SortedDictionary<string, string>();
                btnNhan.Enabled = true;
                btnChuyenNhom.Enabled = true;
            }
            else
            {
                progressBar1.Value = _updateCount*100/_totalListCount;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                string OPTION_KEY = _dataTableName.ToUpper() + "F3";
                if (!M_OPTIONS.ContainsKey(OPTION_KEY) || M_OPTIONS[OPTION_KEY].ToString() != "1")
                {
                    goto End;
                }
                if (V6Login.UserRight.AllowEdit("", _lookupConfig.vMa_file.ToUpper() + "6"))
                {
                    if (dataGridView1.SelectedCells.Count > 0)
                    {
                        var currentRow = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex];
                        var g = currentRow.Cells["UID"].Value.ToString();
                        Guid uid = new Guid(g);
                        var keys = new SortedDictionary<string, object>
                            {
                                {"UID", uid}
                            };
                        
                        var f = new FormAddEdit(_lookupConfig.vMa_file, V6Mode.Edit, keys, null);          // Load data vs DataOld[]
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(this);
                        //f.ParentData = _senderTextBox.ParentData;
                        f.UpdateSuccessEvent += f_UpdateSuccessEvent;
                        f.ShowDialog(this);
                    }
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
                return true;
            }
            else if (keyData == Keys.F4)
            {
                string OPTION_KEY = _dataTableName.ToUpper() + "F4";
                if (!M_OPTIONS.ContainsKey(OPTION_KEY) || M_OPTIONS[OPTION_KEY].ToString() != "1")
                {
                    goto End;
                }
                if (V6Login.UserRight.AllowAdd("", _lookupConfig.vMa_file.ToUpper() + "6"))
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                    var data = row != null ? row.ToDataDictionary() : null;
                    if (data == null) data = new Dictionary<string, object>();
                    data["AUTOID_LOAINH"] = comboBox1.SelectedIndex + 1;
                    data["AUTOID_NHVALUE"] = listBoxMaNh.SelectedValue.ToString().Trim().ToUpper();

                    var f = new FormAddEdit(_lookupConfig.vMa_file, V6Mode.Add, null, data);
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl(this);
                    //f.ParentData = _senderTextBox.ParentData;
                    if (data == null) f.SetParentData();                        // Code lạ??????
                    f.InsertSuccessEvent += f_InsertSuccessEvent;
                    f.ShowDialog(this);
                }
                else
                {
                    V6ControlFormHelper.NoRightWarning();
                }
                return true;
            }
            End:
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, _lookupConfig.vMa_file);
        }
        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls ", ex);
            }
        }

        void f_InsertSuccessEvent(IDictionary<string, object> dataDic)
        {
            try
            {
                _viewData.Table.AddRow(dataDic);
                ViewData(listBoxMaNh.SelectedValue.ToString().Trim());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".f_InsertSuccessEvent", ex);
            }
        }

        void f_UpdateSuccessEvent(IDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
                //_senderTextBox.Reset();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".a_UpdateSuccessEvent", ex);
            }
        }

    }
}
