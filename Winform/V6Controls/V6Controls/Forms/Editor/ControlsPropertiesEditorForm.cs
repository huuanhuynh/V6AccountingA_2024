using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Editor
{
    public partial class ControlsPropertiesEditorForm : V6Form
    {
        private Control _control = null;
        public ControlsPropertiesEditorForm()
        {
            InitializeComponent();
        }

        public ControlsPropertiesEditorForm(Control control)
        {
            _control = control;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                AddTreeNode(_control);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        private void AddTreeNode(Control control, TreeNode parentNode = null)
        {
            if (control == null) return;
            var newNode = (parentNode == null ? treeView1.Nodes : parentNode.Nodes)
                .Add(string.Format("{0} ({1}) [{2}] [{3}]", control.Text, control.Name, control.AccessibleName, control.AccessibleDescription));

            //if (parentNode == null)
            //    newNode = treeView1.Nodes.Add(string.Format("{0} ({1}) [{2}] [{3}]", control.Text, control.Name, control.AccessibleName, control.AccessibleDescription));
            //else
            //    newNode = parentNode.Nodes.Add(string.Format("{0} ({1}) [{2}] [{3}]", control.Text, control.Name, control.AccessibleName, control.AccessibleDescription));

            newNode.Tag = control;
            if (control is RichTextBox)
            {
                newNode.ImageIndex = 9;
            }
            else if (control is TextBox || control is TextBoxBase)
            {
                newNode.ImageIndex = 11;
            }
            else if (control is TabControl)
            {
                newNode.ImageIndex = 10;
            }
            else if (control is RadioButton)
            {
                newNode.ImageIndex = 8;
            }
            else if (control is ListBox)
            {
                newNode.ImageIndex = 7;
            }
            else if (control is Label)
            {
                newNode.ImageIndex = 6;
            }
            else if (control is NumericUpDown)
            {
                newNode.ImageIndex = 5;
            }
            else if (control is GroupBox)
            {
                newNode.ImageIndex = 4;
            }
            else if (control is DateTimePicker)
            {
                newNode.ImageIndex = 3;
            }
            else if (control is CheckBox)
            {
                newNode.ImageIndex = 2;
            }
            else if (control is Button)
            {
                newNode.ImageIndex = 1;
            }
            else
            {
                newNode.ImageIndex = 0;
            }
            newNode.SelectedImageIndex = newNode.ImageIndex;

            foreach (Control c in control.Controls)
            {
                AddTreeNode(c, newNode);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
            ShowControlInfo(e.Node.Tag as Control);
        }

        private Control _label;
        public string NewText { get { return textBox1.Text; } }
        private void ShowControlInfo(Control label)
        {
            try
            {
                _label = label;
                if (string.IsNullOrEmpty(_label.AccessibleDescription))
                {
                    panel1.Visible = false;
                    return;
                }

                panel1.Visible = true;
                //Thay đổi nội dung text bằng tiếng Anh
                if (V6Setting.Language != "V")
                {
                    Text = "Change text";
                    label1.Text = "Text";
                    button1.Text = "Ok";

                    columnHeader1.Text = "Name";
                    columnHeader2.Text = "Value";
                }
                textBox1.Text = _label.Text;
                
                if (V6Setting.IsVietnamese)
                {
                    checkBox1.Checked = true;
                    checkBox1.Visible = true;
                }

                listView1.Items.Clear();
                listView1.Items.Add(new ListViewItem(new[] { V6Setting.IsVietnamese ? "Tên đối tượng" : "Object Name", _label.Name }));
                listView1.Items.Add(new ListViewItem(new[] { V6Setting.IsVietnamese ? "Giá trị" : "Text", _label.Text }));
                listView1.Items.Add(new ListViewItem(new[] { "AccessibleDescription", _label.AccessibleDescription }));

                row = CorpLan.GetRow(_label.AccessibleDescription);
                if (row != null)
                {
                    listView1.Items.Add(new ListViewItem(new[] { "DefaultText", row["D"].ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "VietText", string.Format("{0} ({1})", row["V"], ObjectAndString.ObjectToBool(row["CHANGE_V"]) ? "áp dụng" : "không áp dụng") }));
                    listView1.Items.Add(new ListViewItem(new[] { "EngText", row["E"].ToString() }));
                    if (row.Table.Columns.Contains(V6Setting.Language))
                        listView1.Items.Add(new ListViewItem(new[] { "SelectedLang", row[V6Setting.Language].ToString() }));
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new[] { "Error", "No CorpLan info." }));

                    var parent = V6ControlFormHelper.FindParent<V6Control>(_label) ?? V6ControlFormHelper.FindParent<V6Form>(_label);
                    this.WriteToLog((parent == null ? "" : parent.GetType() + ".") + _label.Name,
                        "No CorpLan info: " + _label.AccessibleDescription);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowControlInfo", ex);
            }
        }

        private void Accept()
        {
            try
            {
                if (row != null)
                {
                    change_v = checkBox1.Checked ? "1" : "0";
                    updateText = NewText;
                    updateId = _label.AccessibleDescription;

                    if (V6Setting.IsVietnamese && !checkBox1.Checked)
                    {
                        _label.Text = row["V"].ToString();
                    }
                    else
                    {
                        _label.Text = updateText;
                    }

                    var T = new Thread(UpdateDatabase);
                    T.IsBackground = true;
                    T.Start();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Accept", ex);
            }
        }

        private DataRow row;
        private string updateText, updateId, change_v;


        private void UpdateDatabase()
        {
            try
            {
                if (CorpLan.Update(updateId, V6Setting.Language, updateText, change_v))
                {
                    V6ControlFormHelper.SetStatusText(V6Text.UpdateSuccess);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".UpdateDatabase", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Accept();
        }
    }
}
