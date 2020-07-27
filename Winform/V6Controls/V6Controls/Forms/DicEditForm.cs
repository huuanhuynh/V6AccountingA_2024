using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.Albc;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Forms
{
    public partial class DicEditForm : V6Form
    {
        public DicEditForm()
        {
            InitializeComponent();
        }

        public DicEditForm(IDictionary<string, string> sourceDic)
        {
            InitializeComponent();
            AddList(sourceDic);
            Ready();
        }

        private void AddList(IDictionary<string, string> sourceDic)
        {
            try
            {
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Value";
                
                foreach (KeyValuePair<string, string> item in sourceDic)
                {
                    listBox1.Items.Add(new V6ListBoxItem(item.Key, item.Value));
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
            UpdateListViewItemName(txtName.Text);
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            UpdateListViewItemValue(txtValue.Text);
        }

        private void UpdateListViewItemName(string key)
        {
            try
            {
                if (_currentItem != null)
                {
                    _currentItem.Name = key;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void UpdateListViewItemValue(string value)
        {
            try
            {
                if (_currentItem != null) _currentItem.Value = value;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private V6ListBoxItem _currentItem;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _currentItem = listBox1.SelectedItem as V6ListBoxItem;
                if (_currentItem != null)
                {
                    txtName.Text = _currentItem.Name;
                    txtValue.Text = _currentItem.Value;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public string GetString(string itemSeparator, string valueSeparator)
        {
            string result = "";
            try
            {
                foreach (object item in listBox1.Items)
                {
                    var listItem = (V6ListBoxItem) item;
                    result += itemSeparator + listItem.Name + valueSeparator + listItem.Value;
                }

                if (result.Length > itemSeparator.Length) result = result.Substring(itemSeparator.Length);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }

            return result;
        }
    }

    internal class V6ListBoxItem
    {
        public V6ListBoxItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}