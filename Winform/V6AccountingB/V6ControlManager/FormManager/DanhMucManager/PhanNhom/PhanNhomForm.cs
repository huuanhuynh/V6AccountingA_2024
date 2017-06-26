using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.DanhMucManager.PhanNhom
{
    public partial class PhanNhomForm : V6Form
    {
        public PhanNhomForm()
        {
            InitializeComponent();
            MyInit();
        }
        
        public PhanNhomForm(string groupTableName, string dataTableName)
        {
            _dataTableName = dataTableName;
            _groupTableNameName = groupTableName;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            btnNhan.Enabled = false;
            LoadDataThread();
        }

        private string _groupTableNameName, _dataTableName;
        private string _idField;
        private bool _dataLoaded = false;
        private DataTable _dataGroup = null, _data = null;
        private DataView _viewGroup, _viewData;

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
                
            }
        }
        private void LoadData()
        {
            _dataGroup = V6BusinessHelper.SelectTable(_groupTableNameName);
            _data = V6BusinessHelper.SelectTable(_dataTableName);

            //CheckChuaPhanNhom();

            _dataLoaded = true;
        }

        private void CheckChuaPhanNhom()
        {
            try
            {
                if (!_data.Columns.Contains(_field))
                {
                    this.ShowWarningMessage("Loại nhóm không tồn tại!");
                    return;
                }
                //Them Loai_nh chưa phân nhóm.
                DataView tempView = new DataView(_data.Copy());
                tempView.RowFilter = string.Format("Isnull({0},'') = ''", _field);
                if (tempView.Count > 0)
                {
                    //Thêm vào dòng group chưa phân nhóm - nếu chưa có.
                    DataView tempView2 = new DataView(_dataGroup.Copy());
                    tempView2.RowFilter = string.Format("Isnull({0},'') = ''", "Ma_nh");
                    if (tempView2.Count == 0)
                    {
                        for (int i = 1; i <= 6; i++)
                        {
                            var newRow = _dataGroup.NewRow();
                            newRow["Loai_nh"] = "" + i;
                            newRow["Ma_nh"] = "";
                            newRow["Ten_nh"] = V6Setting.IsVietnamese ? "Chưa phân nhóm" : "No Group";
                            _dataGroup.Rows.Add(newRow);
                        }
                    }
                }
                else
                {
                    //Xóa dòng group chưa phân nhóm
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
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".CheckChuaPhanNhom " + ex.Message);
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_dataLoaded)
                {
                    timer1.Stop();
                    progressBar1.Value = 100;
                    btnNhan.Enabled = true;
                    comboBox1.SelectedIndex = 0;
                    var loai = comboBox1.SelectedIndex + 1;
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
                
            }
        }

        private void GetFieldNameInfo(int loai)
        {
            V6TableName name = V6TableHelper.ToV6TableName(_dataTableName);
            if (name == V6TableName.Alkh)
            {
                _field = "NH_KH" + loai;
                _idField = "MA_KH";
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
        }

        string _field = "";
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
                _viewGroup.RowFilter = "Loai_nh = " + (comboBox1.SelectedIndex + 1);
                _viewGroup.Sort = "Ma_nh";

                listBox1.DisplayMember = "Ten_nh";
                listBox1.ValueMember = "Ma_nh";
                listBox1.DataSource = _viewGroup;
                listBox1.DisplayMember = "Ten_nh";
                listBox1.ValueMember = "Ma_nh";

                var viewGroup2 = new DataView(_viewGroup.ToTable());

                cboToGroupList.DisplayMember = "Ten_nh";
                cboToGroupList.ValueMember = "Ma_nh";
                cboToGroupList.DataSource = viewGroup2;
                cboToGroupList.DisplayMember = "Ten_nh";
                cboToGroupList.ValueMember = "Ma_nh";
                

                if (listBox1.Items.Count > 0)
                {
                    listBox1.SelectedIndex = 0;
                    ViewData(listBox1.SelectedValue.ToString().Trim());
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
                    _viewData = new DataView(_data);
                _viewData.RowFilter = string.Format("{0} = '{1}'", _field, id);
                _viewData.Sort = _idField;
                dataGridView1.DataSource = _viewData;
                dataGridView1.Refresh();

                string showFields = V6Lookup.ValueByTableName[_dataTableName, "GRDS_V1"].ToString().Trim();
                string formatStrings = V6Lookup.ValueByTableName[_dataTableName, "GRDF_V1"].ToString().Trim();
                string headerString = V6Lookup.ValueByTableName[_dataTableName, V6Setting.IsVietnamese ? "GRDHV_V1" : "GRDHE_V1"].ToString().Trim();
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewData " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            GetFieldNameInfo(comboBox1.SelectedIndex + 1);
            if (!_data.Columns.Contains(_field))
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
            ViewData(listBox1.SelectedValue.ToString().Trim());
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReady) return;
            
            if (dataGridView1.DataSource != null && dataGridView1.CurrentRow != null)
            {
                if (e.KeyCode == Keys.Space && dataGridView1.CurrentRow != null)
                {
                    dataGridView1.CurrentRow.ChangeSelect();
                }
                else
                {
                    
                }
            }
        }

        private void btnChuyenNhom_Click(object sender, EventArgs e)
        {
            if (IsReady)
            {
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
                if (listBox1.Items.Count == 0)
                {
                    this.ShowWarningMessage("Chưa có nhóm!");
                    return;
                }
                var oldGroup = listBox1.SelectedValue.ToString().Trim();
                var newGroup = cboToGroupList.SelectedValue.ToString().Trim();
                V6TableName tableName = V6TableHelper.ToV6TableName(_dataTableName);
                if (tableName == V6TableName.Alkh)
                {
                    
                }
                if (!_data.Columns.Contains(_field))
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
                listBox1_SelectedIndexChanged(listBox1, new EventArgs());
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
                _changedList = new SortedDictionary<string, string>();
                btnNhan.Enabled = true;
                btnChuyenNhom.Enabled = true;
            }
            else
            {
                progressBar1.Value = _updateCount*100/_totalListCount;
            }
        }
    }
}
