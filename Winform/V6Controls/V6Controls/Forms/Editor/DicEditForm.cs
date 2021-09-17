using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6Controls.Controls;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Editor
{
    public partial class DicEditForm : V6Form
    {
        public DicEditForm()
        {
            InitializeComponent();
        }

        private DataTable _dataSource;
        private DicEditButton _button;

        public DicEditForm(IDictionary<string, string> sourceDic, DicEditButton button)
        {
            InitializeComponent();
            CreateDataSource();
            AddList(sourceDic);
            _button = button;
            cboKeyWord.Items.Clear();
            if (button.KeyWordList != null) cboKeyWord.Items.AddRange(button.KeyWordList);
            Ready();
        }

        private void CreateDataSource()
        {
            try
            {
                _dataSource = new DataTable("Dic");
                _dataSource.Columns.Add("Name", typeof(string));
                _dataSource.Columns.Add("Value", typeof(string));

                dataGridView1.DataSource = _dataSource;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 300;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void AddList(IDictionary<string, string> sourceDic)
        {
            try
            {
                foreach (KeyValuePair<string, string> item in sourceDic)
                {
                    var newRow = _dataSource.NewRow();
                    newRow["Name"] = item.Key;
                    newRow["Value"] = item.Value;
                    _dataSource.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Them()
        {
            try
            {
                var newRow = _dataSource.NewRow();
                string newKey = "NAME";
                string value = "Value";
                if (txtNewKey.Text != "")
                {
                    if (txtNewKey.Text.Contains(_button.Separator_Value))
                    {
                        int i = txtNewKey.Text.IndexOf(_button.Separator_Value, StringComparison.Ordinal);
                        newKey = txtNewKey.Text.Substring(0, i);
                        value = txtNewKey.Text.Substring(i+1);
                    }
                    else
                    {
                        newKey = txtNewKey.Text;
                    }
                }
                newRow["Name"] = newKey;
                newRow["Value"] = value;
                _dataSource.Rows.Add(newRow);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void UpdateListViewItemName(string name)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.CurrentRow.Cells["Name"].Value = name;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void Xoa()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }


        private void SelectMultiIDForm_Load(object sender, EventArgs e)
        {

        }

        private void SelectMultiIDForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (_txtFields != null) _txtFields.Text = GetFieldsString();
            //if (_txtFormats != null) _txtFormats.Text = GetFormatsString();
            //if (_txtTextV != null) _txtTextV.Text = GetCaptionsStringV();
            //if (_txtTextE != null) _txtTextE.Text = GetCaptionsStringE();
            txtValue_Leave(txtValue, e);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTimTatCa_Click(object sender, EventArgs e)
        {
            
        }

        private void txtTim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTim_Click(null, null);
        }


        private void btnMove2Up_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMove2Down_Click(object sender, EventArgs e)
        {
            
        }

        private void DicEditForm_SizeChanged(object sender, EventArgs e)
        {
            //FixSize();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (flag_change) return;
            UpdateListViewItemName(txtName.Text);
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            UpdateListViewItemValue(txtValue.Text);
        }

        private void UpdateListViewItemValue(string value)
        {
            try
            {
                if (dataGridView1.CurrentRow != null) dataGridView1.CurrentRow.Cells["Value"].Value = value;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private bool flag_change = false;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag_change = true;
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                    txtValue.Text = dataGridView1.CurrentRow.Cells["Value"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }

            flag_change = false;
        }

        public string GetString(string itemSeparator, string valueSeparator)
        {
            string result = "";
            try
            {
                foreach (DataRow row in _dataSource.Rows)
                {
                    result += itemSeparator + row["Name"] + valueSeparator + row["Value"];
                }

                if (result.Length > itemSeparator.Length) result = result.Substring(itemSeparator.Length);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }

            return result;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Them();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Xoa();
        }

        private void cboKeyWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            var text = cboKeyWord.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                txtNewKey.Text = text;
                btnThem.PerformClick();
            }
        }
    }

    //internal class V6ListBoxItem
    //{
    //    public V6ListBoxItem(string name, string value)
    //    {
    //        Name = name;
    //        Value = value;
    //    }

    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //}
}