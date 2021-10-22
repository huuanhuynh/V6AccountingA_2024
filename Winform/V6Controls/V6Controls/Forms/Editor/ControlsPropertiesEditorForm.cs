using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Editor
{
    public partial class ControlsPropertiesEditorForm : V6Form
    {
        /// <summary>
        /// Form chứa.
        /// </summary>
        private readonly Control _mainControl;
        /// <summary>
        /// Đối tượng dưới con tro chuột.
        /// </summary>
        private readonly Control _mouseControl;
        public ControlsPropertiesEditorForm()
        {
            InitializeComponent();
        }

        public ControlsPropertiesEditorForm(Control mainControl, Control mouseControl)
        {
            _mainControl = mainControl;
            _mouseControl = mouseControl;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                AddTreeNode(_mainControl);
                SetSelectedTreeNode(_mouseControl);
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
            //    newNode = treeView1.Nodes.Add(string.Format("{0} ({1}) [{2}] [{3}]", mainControl.Text, mainControl.Name, mainControl.AccessibleName, mainControl.AccessibleDescription));
            //else
            //    newNode = parentNode.Nodes.Add(string.Format("{0} ({1}) [{2}] [{3}]", mainControl.Text, mainControl.Name, mainControl.AccessibleName, mainControl.AccessibleDescription));

            newNode.Tag = control;
            if (control is RichTextBox)
            {
                newNode.ImageIndex = 9;
            }
            else if (control is ComboBox)
            {
                newNode.ImageIndex = 12;
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
            else if (control is DataGridView)
            {
                newNode.ImageIndex = 13;
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

        private void SetSelectedTreeNode(Control control)
        {
            var node = FindNodeRecursive(treeView1.Nodes[0], control);
            if (node != null) treeView1.SelectedNode = node;
        }

        private TreeNode FindNodeRecursive(TreeNode node, Control control)
        {
            if (node.Tag == control) return node;
            foreach (TreeNode n in node.Nodes)
            {
                var r = FindNodeRecursive(n, control);
                if (r != null) return r;
            }
            return null;
        }

        /// <summary>
        /// Tải 1 file xml dạng dataset_table để lấy dữ liệu lên form.
        /// </summary>
        private void LoadMainControlDataXml()
        {
            try
            {
                var openFile = V6ControlFormHelper.ChooseOpenFile(this, "Xml|*.xml");
                if (string.IsNullOrEmpty(openFile)) return;
                FileStream fs = new FileStream(openFile, FileMode.Open);
                DataSet _ds = new DataSet();
                _ds.ReadXml(fs);
                fs.Close();
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    V6ControlFormHelper.SetFormDataRow(_mainControl, _ds.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".LoadMainControlDataXml", ex);
            }
        }

        /// <summary>
        /// Lưu form data xuống file xml.
        /// </summary>
        private void SaveMainControlDataXml()
        {
            try
            {
                var _ds = new DataSet("FormData");
                var formData = V6ControlFormHelper.GetFormDataDictionary(_mainControl);
                DataTable data = new DataTable("Data");
                data.AddRow(formData, true);
                _ds.Tables.Add(data);
                //var saveFile = V6ControlFormHelper.ChooseSaveFile("Xml|*.xml");
                var saveFile = new SaveFileDialog
                {
                    Filter = "XML files (*.Xml)|*.xml",
                    Title = "Xuất XML.",
                    FileName = _mainControl.Name + "_FORM_DATA"
                };
                
                if (saveFile.ShowDialog(this) == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(saveFile.FileName)) return;
                    FileStream fs = new FileStream(saveFile.FileName, FileMode.Create);
                    _ds.WriteXml(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".SaveMainControlDataXml", ex);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
            ShowControlInfo(e.Node.Tag as Control);
        }

        private Control _control;
        public string NewText { get { return textBox1.Text; } }
        private void ShowControlInfo(Control control_)
        {
            try
            {
                _control = control_;
                lblControlType.Text = "" + _control.GetType();
                lblControlName.Text = _control.Name;
                // Thông tin ngôn ngữ.
                if (string.IsNullOrEmpty(_control.AccessibleDescription))
                {
                    if (control_ is V6Control || control_ is V6Form)
                    {
                        panel1.Visible = true;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        checkBox1.Visible = false;
                        btnSave.Visible = false;

                        listView1.Items.Clear();
                        // Property
                        SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
                        foreach (PropertyInfo propertyInfo in control_.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            sortedDictionary["private(" + propertyInfo.Name + ")"] = "" + propertyInfo.GetValue(control_, null);
                        }
                        foreach (PropertyInfo propertyInfo in control_.GetType().GetProperties())
                        {
                            sortedDictionary["public(" + propertyInfo.Name + ")"] = "" + propertyInfo.GetValue(control_, null);
                        }
                        foreach (KeyValuePair<string, string> item in sortedDictionary)
                        {
                            listView1.Items.Add(new ListViewItem(new[] { item.Key, item.Value }));
                        }
                        
                        // Field
                        sortedDictionary.Clear();
                        foreach (FieldInfo fieldInfo in control_.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            sortedDictionary["private[" + fieldInfo.Name + "]"] = "" + fieldInfo.GetValue(control_);
                        }
                        foreach (FieldInfo fieldInfo in control_.GetType().GetFields())
                        {
                            sortedDictionary["public[" + fieldInfo.Name + "]"] = "" + fieldInfo.GetValue(control_);
                        }
                        foreach (KeyValuePair<string, string> item in sortedDictionary)
                        {
                            listView1.Items.Add(new ListViewItem(new[] { item.Key, item.Value }));
                        }

                        // Method
                        sortedDictionary.Clear();
                        foreach (MethodInfo info in control_.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
                        {
                            sortedDictionary[string.Format("private {0} {1}", info.ReturnType.Name, info.Name)] = "" + info;
                        }
                        foreach (MethodInfo info in control_.GetType().GetMethods())
                        {
                            sortedDictionary[string.Format("public {0} {1}", info.ReturnType, info.Name)] = "" + info;
                        }
                        foreach (KeyValuePair<string, string> item in sortedDictionary)
                        {
                            listView1.Items.Add(new ListViewItem(new[] { item.Key, item.Value }));
                        }
                        return;
                    }
                    else
                    {
                        panel1.Visible = false;
                        return;    
                    }
                }

                panel1.Visible = true;
                label1.Visible = true;
                textBox1.Visible = true;
                checkBox1.Visible = true;
                btnSave.Visible = true;
                //Thay đổi nội dung text bằng tiếng Anh
                if (V6Setting.Language != "V")
                {
                    Text = "Change text";
                    label1.Text = "Text";
                    btnSave.Text = "Ok";

                    columnHeader1.Text = "Name";
                    columnHeader2.Text = "Value";
                }
                textBox1.Text = _control.Text;
                
                if (V6Setting.IsVietnamese)
                {
                    checkBox1.Checked = true;
                    checkBox1.Visible = true;
                }

                listView1.Items.Clear();
                listView1.Items.Add(new ListViewItem(new[] { V6Setting.IsVietnamese ? "Tên đối tượng" : "Object Name", _control.Name }));
                listView1.Items.Add(new ListViewItem(new[] { V6Setting.IsVietnamese ? "Giá trị" : "Text", _control.Text }));
                listView1.Items.Add(new ListViewItem(new[] { "AccessibleDescription", _control.AccessibleDescription }));

                row = CorpLan.GetRow(_control.AccessibleDescription);
                if (row != null)
                {
                    listView1.Items.Add(new ListViewItem(new[] { "DefaultText", row["D"].ToString() }));
                    listView1.Items.Add(new ListViewItem(new[] { "VietText", string.Format("{0} ({1})", row["V"], ObjectAndString.ObjectToBool(row["CHANGE_V"]) ? "áp dụng" : "không áp dụng") }));
                    listView1.Items.Add(new ListViewItem(new[] { "EngText", row["E"].ToString() }));
                    if (row.Table.Columns.Contains(V6Setting.Language))
                        listView1.Items.Add(new ListViewItem(new[] { "SelectedLang", row[V6Setting.Language].ToString() }));
                }
                else if (_control.AccessibleDescription != ".")
                {
                    listView1.Items.Add(new ListViewItem(new[] { "Error", "No CorpLan info." }));

                    Control parent = V6ControlFormHelper.FindParent<V6Control>(_control) ?? V6ControlFormHelper.FindParent<V6Form>(_control);
                    this.WriteToLog((parent == null ? "" : parent.GetType() + ".") + _control.Name,
                        "No CorpLan info: " + _control.AccessibleDescription);
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
                    updateId = _control.AccessibleDescription;

                    if (V6Setting.IsVietnamese && !checkBox1.Checked)
                    {
                        _control.Text = row["V"].ToString();
                    }
                    else
                    {
                        _control.Text = updateText;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                treeView1.SelectedNode = treeView1.SelectedNode.Parent;
                //Control c = treeView1.SelectedNode.Tag as Control;
                //if (c.Parent != null) SetSelectedTreeNode(c.Parent);
            }
        }

        private void btnNhapXml_Click(object sender, EventArgs e)
        {
            LoadMainControlDataXml();
        }

        private void btnXuatXml_Click(object sender, EventArgs e)
        {
            SaveMainControlDataXml();
        }

        private void btnDefaultData_Click(object sender, EventArgs e)
        {
            try
            {
                var parent = V6ControlFormHelper.FindParent<V6FormControl>(_control) as V6FormControl;
                if (parent != null)
                {
                    parent.ShowAlinitAddEdit(_control);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".btnDefaultData_Click", ex);
            }
        }
    }
}
