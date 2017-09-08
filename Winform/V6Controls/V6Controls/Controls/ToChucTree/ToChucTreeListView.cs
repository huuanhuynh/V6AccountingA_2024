using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Controls.ToChucTree
{
    public class ToChucTreeListView : TreeListView
    {
        #region ==== Contructor ====
        public ToChucTreeListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ToChucTreeListView
            // 
            this.SelectedIndexChanged += new System.EventHandler(this.ToChucTreeListView_SelectedIndexChanged);
            this.ResumeLayout(false);

        }

        private void ToChucTreeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedItems == null || SelectedItems.Count == 0) return;
            var selectedItem = SelectedItems[0];
            OnChangeSelected(selectedItem, GetItemData(selectedItem));
        }
        #endregion contructor.


        #region ==== Properties ====

        private DataTable _data;
        private bool _unknowChange = true;
        private void Apply_dataEvents(DataTable data)
        {
            // Có thể không kiểm soát được hết sự kiện nên không làm.
            //try
            //{
            //    data.RowChanged += data_RowChanged;
            //    data.RowDeleted += data_RowChanged;
            //    data.TableNewRow += data_TableNewRow;
            //}
            //catch (Exception)
            //{

            //}
        }

        void data_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if (_unknowChange)
            {
                ResetViewData();
            }
        }

        void data_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (_unknowChange)
            {
                ResetViewData();
            }
        }

        [DefaultValue(true)]
        [Description("Tự động tạo cột theo bảng dữ liệu đưa vào. SetData()")]
        public bool AutoGenerateColumns { get { return _autoGenColumns; } set { _autoGenColumns = value; } }
        private bool _autoGenColumns = true;

        /// <summary>
        /// Set GroupList and NameList fields
        /// </summary>
        public void SetGroupAndNameFieldList(string idField, string textField)
        {
            ID_Field = idField;
            Text_Field = textField;
            ResetViewData();
            OnGroupListChanged();
        }

        public string GetIDField()
        {
            return ID_Field;
        }
        public string GetTextField()
        {
            return Text_Field;
        }
        
        public string ID_Field { get; set; }
        public string Text_Field { get; set; }
        public string ImageIndex_Field { get; set; }
        [DefaultValue("parent")]
        public string ParentIdField { get { return _parent_field.ToUpper(); } set { _parent_field = value; } }
        private string _parent_field = "parent";
        public string Sort_Field { get { return _sortfield; } set { _sortfield = value; } }
        public string _sortfield = "fsort";

        [DefaultValue(true)]
        public bool ViewName { get { return _view_name; }
            set
            {
                _view_name = value;
                //ResetView();
                Columns[0].Text = _view_name ? "Tên" : "Mã";
                ResetViewData();
            }
        }

        private bool _view_name = true;

        ///// <summary>
        ///// Kiểm tra dòng đang chọn có phải là cấp cuối (chi tiết) hay không.
        ///// </summary>
        //[Browsable(false)]
        //public bool IsDetailSelected
        //{
        //    get
        //    {
        //        if (SelectedItems.Count == 0) return false;
        //        return SelectedItems[0].Level == MaxLevel;
        //    }
        //}

        /// <summary>
        /// Dữ liệu của dòng đang chọn.
        /// </summary>
        [Browsable(false)]
        public SortedDictionary<string, object> SelectedItemData
        {
            get
            {
                if (SelectedItems == null || SelectedItems.Count == 0) return null;
                var item = SelectedItems[0];

                var result = GetItemData(item);

                return result;
            }
        }

        //public int MaxLevel
        //{
        //    get
        //    {
        //        if (ID_Field != null)
        //        {
        //            return ID_Field.Length - 1;
        //        }
        //        return -1;
        //    }
        //}

        

        #endregion properties


        #region ==== Events ====

        public event EventHandler GroupListChanged;
        protected virtual void OnGroupListChanged()
        {
            var handler = GroupListChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }


        public event Action<IDictionary<string, object>> TreeRowAdded;
        protected virtual void OnTreeRowAdded(IDictionary<string, object> obj)
        {
            var handler = TreeRowAdded;
            if (handler != null) handler(obj);
        }

        /// <summary>
        /// (TreeListViewItem item, IDictionary[string, object] data)
        /// </summary>
        public event Action<TreeListViewItem, IDictionary<string, object>> ChangeSelected;
        protected virtual void OnChangeSelected(TreeListViewItem item, IDictionary<string, object> data)
        {
            var handler = ChangeSelected;
            if (handler != null) handler(item, data);
        }


        #endregion events


        ///// <summary>
        ///// Kiểm tra mã có nằm trong danh sách nhóm hay không.
        ///// </summary>
        ///// <param name="NAME">UPPER</param>
        ///// <returns></returns>
        //private bool IsInGroup(string NAME)
        //{
        //    foreach (string group in ID_Field)
        //    {
        //        if (NAME == group.ToUpper()) return true;
        //    }
        //    return false;
        //}
        private SortedDictionary<string, object> GetItemData(TreeListViewItem item)
        {
            try
            {
                return ((DataRow)item.Tag).ToDataDictionary();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("ToChucTreeListView " + ex.Message, Application.ProductName);
            }
            return null;
        }
        
        private void ResetView()
        {
            //try
            //{
                if (AutoGenerateColumns || Columns.Count == 0)
                {
                    Columns.Clear();
                    if (ViewName)
                    {
                        Columns.Add("V6ID", V6Setting.Language == "V" ? "Tên" : "Name", 250);
                    }
                    else
                    {
                        Columns.Add("V6ID", V6Setting.Language == "V" ? "Mã": "ID", 250);
                    }
                    
                    if (_data == null) return;

                    // Nếu có config columns riêng thì khác, sẽ làm sau. !!!!!
                    if (_viewColumns == null || _viewColumns.Length == 0)
                    {
                        foreach (DataColumn column in _data.Columns)
                        {
                            var columnNAME = column.ColumnName;
                            var isNumber = ObjectAndString.IsNumberType(column.DataType);
                            if (columnNAME == "dt_thue")
                            {
                                var a = 0;
                            }
                            //if (IsInGroup(columnNAME.ToUpper())) continue;
                            if (!Columns.ContainsKey(column.ColumnName))
                            {
                                Columns.Add(columnNAME, columnNAME, 100,
                                    isNumber ? HorizontalAlignment.Right : HorizontalAlignment.Left, -1);
                            }
                        }
                    }
                    else
                    {
                        if(_viewNames.Length<_viewColumns.Length) throw new Exception("Thiếu tiêu đề cột");

                        for (int i = 0; i < _viewColumns.Length; i++)
                        {
                            var column = _data.Columns[_viewColumns[i]];
                            if (column == null) continue;

                            var columnNAME = column.ColumnName;
                            var columnTEXT = _viewNames[i];
                            var columnWIDTH = _viewWidths.Length > i ? _viewWidths[i] : 100;

                            var isNumber = ObjectAndString.IsNumberType(column.DataType);
                            //if (IsInGroup(columnNAME.ToUpper())) continue;
                            if (!Columns.ContainsKey(column.ColumnName))
                            {
                                Columns.Add(columnNAME, columnTEXT, columnWIDTH,
                                    isNumber ? HorizontalAlignment.Right : HorizontalAlignment.Left, -1);
                            }
                        }
                    }
                }

                ResetViewData();
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteToLog("ToChucTreeListView " + ex.Message, Application.ProductName);
            //}
        }

        private void ResetViewData()
        {
            Items.Clear();
            if (_data == null) return;
            //Add node theo parent
            //Add root node (without parent)
            DataView parents = new DataView(_data);
            parents.RowFilter = string.Format("IsNull([{0}],'') = ''", _parent_field);//, _sortfield, DataViewRowState.None);
            parents.Sort = _sortfield;

            DataTable parents_table = parents.ToTable();
            foreach (DataRow parent_row in parents_table.Rows)
            {
                AddParentNode(parent_row);
            }
                
            //Code cũ thêm từng dòng.
            //foreach (DataRow row in _data.Rows)
            //{
            //    AddTreeRow(row);
            //}
            ExpandAll();
        }

        /// <summary>
        /// Thêm node cha và tất cả con cháu của nó.
        /// </summary>
        /// <param name="parentRow"></param>
        private void AddParentNode(DataRow parentRow)
        {
            try
            {
                //Thêm node cha
                string id = parentRow[ID_Field].ToString().Trim();
                string text = parentRow[Text_Field].ToString().Trim();
                int image_index = ObjectAndString.ObjectToInt(parentRow[ImageIndex_Field]);
                TreeListViewItem parentNode = new TreeListViewItem(id, image_index);
                parentNode.Name = id;
                if (ViewName) parentNode.Text = text;
                parentNode.Tag = parentRow;
                //Add more (subitems)
                AddSubItem(parentNode, parentRow);
                Items.Add(parentNode);
                //Thêm con cháu. (đệ quy)
                AddChild(parentNode);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddParentNode", ex);
            }
        }

        /// <summary>
        /// Đệ quy. Thêm các con cho node từ DataSource
        /// </summary>
        /// <param name="parentNode"></param>
        private void AddChild(TreeListViewItem parentNode)
        {
            // Nếu không có con cháu thì ngưng
            string parent_id = parentNode.Name;
            DataView childs = new DataView(_data);
            childs.RowFilter = string.Format("[{0}] = '"+parent_id+"'", ParentIdField);
            childs.Sort = _sortfield;
            //, DataViewRowState.None);
            DataTable childs_table = childs.ToTable();
            if (childs_table.Rows.Count == 0) return;
            foreach (DataRow row in childs_table.Rows)
            {
                string id = row[ID_Field].ToString().Trim();
                string text = row[Text_Field].ToString().Trim();
                int image_index = ObjectAndString.ObjectToInt(row[ImageIndex_Field]);
                TreeListViewItem node = new TreeListViewItem(id, image_index);
                node.Name = id;
                if (ViewName) node.Text = text;
                node.Tag = row;
                //Add more (subitems)
                AddSubItem(node, row);
                parentNode.Items.Add(node);
                AddChild(node);
            }
        }

        private void AddSubItem(TreeListViewItem node, DataRow row)
        {
            for (int i = 1; i < Columns.Count; i++)
            {
                var columnNAME = Columns[i].Name.ToUpper();
                //if (IsInGroup(columnNAME)) continue;
                if (row.Table.Columns.Contains(columnNAME))
                {
                    var value = row[columnNAME];
                    if (ObjectAndString.IsNumberType(value.GetType()))
                    {
                        var numberString = ObjectAndString.NumberToString(value, _viewDecimals[i - 1],
                            V6Options.M_NUM_POINT, ".");
                        node.SubItems.Add(numberString);
                    }
                    else
                    {
                        node.SubItems.Add(ObjectAndString.ObjectToString(value));
                    }
                }
                else
                {
                    node.SubItems.Add("");
                }
            }
        }

        public void AddData(IDictionary<string, object> data)
        {
            var newRow = _data.AddRow(data);
            AddNewNode(newRow);
        }

        public void AddData(ICollection<SortedDictionary<string, object>> data)
        {
            if (_data == null)
            {
                _data = new DataTable();
                Apply_dataEvents(_data);
            }
            foreach (IDictionary<string, object> line in data)
            {
                var newRow = _data.AddRow(line);
                AddNewNode(newRow);
            }
        }

        public void AddData(DataTable data)
        {
            AddData(data.ToListDataDictionary());
        }

        /// <summary>
        /// Thêm một dòng vào tree
        /// </summary>
        /// <param name="data">key in UPPER</param>
        private void AddNewNode(DataRow data)//IDictionary<string, object>
        {
            if (ID_Field == null || ID_Field.Length == 0)
            {
                throw new Exception("No GroupFieldList");
            }
            if (Text_Field == null || Text_Field.Length == 0)
            {
                throw new Exception("No GroupNameFieldList");
            }
            
            var NODE_ID = data[ID_Field].ToString().Trim();
            var NODE_TEXT = data[Text_Field].ToString().Trim();
            var PARENT_ID = data[_parent_field].ToString().Trim();
            // Find parrent node
            TreeListViewItem node = FindNode(PARENT_ID);
            // If not found, create new node
            if (node == null)
            {
                AddNewParentNode(data);
                //node = new TreeListViewItem(NODE_ID, 0);
                //node.Name = NODE_ID;
                //if (ViewName) node.Text = NODE_TEXT;
                //Items.Add(node);
            }
            else // if found add child
            {
                AddNewChild(node, data);
            }
            
            OnTreeRowAdded(data.ToDataDictionary());
        }

        private void AddNewChild(TreeListViewItem node, DataRow data)
        {
            throw new NotImplementedException();
        }

        private void AddNewParentNode(DataRow data)
        {
            throw new NotImplementedException();
        }

        public TreeListViewItem FindNode(string id)
        {
            if (Items == null || ItemsCount == 0) return null;
            foreach (TreeListViewItem item in Items)
            {
                return FindNodeRecursive(item, id);
            }
            return null;
        }

        private TreeListViewItem FindNodeRecursive(TreeListViewItem item, string id)
        {
            if (item.Name == id) return item;
            foreach (TreeListViewItem i in item.Items)
            {
                return FindNodeRecursive(i, id);
            }
            return null;
        }

        // Hàm cũ không còn phù hợp
        //private void AddNode(int level, TreeListViewItem parent, DataRow data)//IDictionary<string, object>
        //{
        //    if (level == ID_Field.Length)
        //    {
        //        parent.Tag = data;
        //        if (_viewColumns == null || _viewColumns.Length == 0)
        //        {
        //            for (int i = 1; i < Columns.Count; i++)
        //            {
        //                var columnNAME = Columns[i].Name.ToUpper();
        //                //if (IsInGroup(columnNAME)) continue;
        //                if (data.Table.Columns.Contains(columnNAME))
        //                {
        //                    var value = data[columnNAME];
        //                    parent.SubItems.Add(ObjectAndString.ObjectToString(value));
        //                }
        //                else
        //                {
        //                    parent.SubItems.Add("");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int i = 1; i < Columns.Count; i++)
        //            {
        //                var columnNAME = Columns[i].Name.ToUpper();
        //                //if (IsInGroup(columnNAME)) continue;
        //                if (data.Table.Columns.Contains(columnNAME))
        //                {
        //                    var value = data[columnNAME];
        //                    if (ObjectAndString.IsNumberType(value.GetType()))
        //                    {
        //                        var numberString = ObjectAndString.NumberToString(value, _viewDecimals[i-1],
        //                            V6Options.M_NUM_POINT, ".");
        //                        parent.SubItems.Add(numberString);
        //                    }
        //                    else
        //                    {
        //                        parent.SubItems.Add(ObjectAndString.ObjectToString(value));
        //                    }
        //                }
        //                else
        //                {
        //                    parent.SubItems.Add("");
        //                }
        //            }
        //        }
        //    }
        //    else // Đoạn code này giống với code của hàm ở trên.
        //    {
        //        var GROUP_FIELD = ID_Field[level].ToUpper();
        //        var GROUP_CODE = data[GROUP_FIELD].ToString().Trim();
        //        var NAME_FIELD = Text_Field[level];
        //        var GROUP_NAME = data[NAME_FIELD].ToString().Trim();

        //        // Find parrent node
        //        TreeListViewItem node = null;
        //        foreach (TreeListViewItem item in parent.Items)
        //        {
        //            if (item.Name == GROUP_CODE)
        //            {
        //                node = item;
        //                break;
        //            }
        //        }
        //        // If not found, create new node
        //        if (node == null)
        //        {
        //            node = new TreeListViewItem(GROUP_CODE, level);
        //            node.Name = GROUP_CODE;
        //            if(ViewName) node.Text = GROUP_NAME;
        //            parent.Items.Add(node);
        //        }

        //        //Add tiếp các cấp khác
        //        AddNode(level+1, node, data);
        //    }
        //}

        private string[] _viewColumns;
        private string[] _viewNames;
        private int[] _viewWidths;
        private int[] _viewDecimals;

        public void SetData(DataTable data, string viewColumns, string viewNames, string viewFormat)
        {
            _data = data;
            Apply_dataEvents(_data);
            ParseFormat(viewColumns, viewNames, viewFormat);
            ResetView();
        }

        private void ParseFormat(string viewColumns, string viewNames, string viewFormat)
        {
            try
            {
                _viewColumns = viewColumns.Split(new[] {viewColumns.Contains(";") ? ';' : ','},
                    StringSplitOptions.RemoveEmptyEntries);
                _viewNames = viewNames.Split(new[] {viewNames.Contains(";") ? ';' : ','},
                    StringSplitOptions.RemoveEmptyEntries);
                var viewFormats = viewFormat.Split(viewFormat.Contains(";") ? ';' : ',');
                _viewWidths = new int[viewFormats.Length];
                _viewDecimals = new int[viewFormats.Length];
                for (int i = 0; i < viewFormats.Length; i++)
                {
                    var widthText = "100";
                    var decimalsText = "0";
                    var format = viewFormats[i];
                    if (format.Length > 0)
                    {
                        widthText = format.Substring(1);
                        if (format.StartsWith("N"))
                        {
                            if (format.Contains(":"))
                            {
                                var a2 = format.Split(':');
                                decimalsText = a2[0].Substring(1);
                                widthText = a2[1];
                            }
                            else
                            {
                                decimalsText = format.Substring(1);
                            }
                        }
                    }
                    _viewWidths[i] = ObjectAndString.ObjectToInt(widthText);
                    if (_viewWidths[i] == 0) _viewWidths[i] = 100;
                    _viewDecimals[i] = ObjectAndString.ObjectToInt(decimalsText);
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }


        public void UpdateSelectedItemData(IDictionary<string, object> data)//!!!! Update _data???
        {
            if (SelectedItems == null || SelectedItems.Count == 0) return;
            
            var selected_item = SelectedItems[0];
            var rowData = (DataRow)selected_item.Tag;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (rowData.Table.Columns.Contains(item.Key))
                    rowData[item.Key] = ObjectAndString.ObjectTo(rowData.Table.Columns[item.Key].DataType, item.Value);
            }
            
            for (int i = 0; i < Columns.Count; i++)
            {
                var FIELD = Columns[i].Name.ToUpper();
                if (data.ContainsKey(FIELD))
                {
                    selected_item.SubItems[i].Text = ObjectAndString.ObjectToString(data[FIELD]);
                }
                else
                {
                    //item.SubItems[i - 1].Text = "";
                }
            }
        }

        public void ViewDataToNewForm()
        {
            try
            {
                var data = _data.Copy();

                {
                    var f = new V6Form
                    {
                        WindowState = FormWindowState.Normal,
                        MaximizeBox = true,
                        MinimizeBox = false,
                        ShowInTaskbar = false,
                        //FormBorderStyle = FormBorderStyle.None
                        Text = Name,
                        Size = new Size(800, 600)
                    };

                    var clip = f.CreateGraphics().VisibleClipBounds;
                    //child.Location = new Point(0, 0);
                    //child.Dock = DockStyle.Fill;
                    DataGridView newGridView = new DataGridView
                    {
                        AllowUserToAddRows = false,
                        AllowUserToDeleteRows = false,
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                        ReadOnly = true,
                        Size = new Size((int)clip.Width, (int)(clip.Height - 25))
                    };

                    f.Controls.Add(newGridView);
                    newGridView.DataSource = data;

                    GridViewSummary gSum = new GridViewSummary();
                    gSum.DataGridView = newGridView;

                    f.ShowDialog(this);
                    gSum.Refresh();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewDataToNewForm: " + ex.Message);
            }
        }
    }
}