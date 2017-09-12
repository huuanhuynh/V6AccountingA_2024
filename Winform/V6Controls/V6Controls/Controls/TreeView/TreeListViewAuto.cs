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

namespace V6Controls.Controls.TreeView
{
    public class TreeListViewAuto : TreeListView
    {
        #region ==== Contructor ====
        public TreeListViewAuto()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeListViewAuto
            // 
            this.SelectedIndexChanged += new System.EventHandler(this.TreeListViewAuto_SelectedIndexChanged);
            this.ResumeLayout(false);

        }

        private void TreeListViewAuto_SelectedIndexChanged(object sender, EventArgs e)
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
        public void SetGroupAndNameFieldList(string[] groupFieldList, string[] groupNameFieldList)
        {
            _groupFieldList = groupFieldList;
            _groupNameFieldList = groupNameFieldList;
            ResetViewData();
            OnGroupListChanged();
        }

        public string[] GetGroupFieldList()
        {
            return _groupFieldList;
        }
        public string[] GetGroupNameFieldList()
        {
            return _groupNameFieldList;
        }
        
        private string[] _groupFieldList;
        private string[] _groupNameFieldList;

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

        /// <summary>
        /// Kiểm tra dòng đang chọn có phải là cấp cuối (chi tiết) hay không.
        /// </summary>
        [Browsable(false)]
        public bool IsDetailSelected
        {
            get
            {
                if (SelectedItems.Count == 0) return false;
                return SelectedItems[0].Level == MaxLevel;
            }
        }

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

        public int MaxLevel
        {
            get
            {
                if (_groupFieldList != null)
                {
                    return _groupFieldList.Length - 1;
                }
                return -1;
            }
        }

        private SortedDictionary<string, object> GetItemData(TreeListViewItem item)
        {
            try
            {
                if (item.Level == MaxLevel) return ((DataRow)item.Tag).ToDataDictionary();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("TreeListViewAuto " + ex.Message, Application.ProductName);
            }

            var result = new SortedDictionary<string, object>();
            if (item.Level > 0)
            {
                result = GetItemData(item.Parent);
            }
            result.Add(_groupFieldList[item.Level].ToUpper(), item.Name);
            
            return result;
        }

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


        /// <summary>
        /// Kiểm tra mã có nằm trong danh sách nhóm hay không.
        /// </summary>
        /// <param name="NAME">UPPER</param>
        /// <returns></returns>
        private bool IsInGroup(string NAME)
        {
            foreach (string group in _groupFieldList)
            {
                if (NAME == group.ToUpper()) return true;
            }
            return false;
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
            //    Logger.WriteToLog("TreeListViewAuto " + ex.Message, Application.ProductName);
            //}
        }

        private void ResetViewData()
        {
            Items.Clear();
            if (_data == null) return;
            foreach (DataRow row in _data.Rows)
            {
                AddTreeRow(row);
            }
            ExpandAll();
        }

        public void AddData(IDictionary<string, object> data)
        {
            var newRow = _data.AddRow(data);
            AddTreeRow(newRow);
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
                AddTreeRow(newRow);
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
        private void AddTreeRow(DataRow data)//IDictionary<string, object>
        {
            if (_groupFieldList == null || _groupFieldList.Length == 0)
            {
                throw new Exception("No GroupFieldList");
            }
            if (_groupNameFieldList == null || _groupNameFieldList.Length == 0)
            {
                throw new Exception("No GroupNameFieldList");
            }
            var GROUP_FIELD = _groupFieldList[0].ToUpper();
            var GROUP_CODE = data[GROUP_FIELD].ToString().Trim();
            var NAME_FIELD = _groupNameFieldList[0];
            var GROUP_TEXT = data[NAME_FIELD].ToString().Trim();

            // Find parrent node
            TreeListViewItem node = null;
            foreach (TreeListViewItem item in Items)
            {
                if (item.Name == GROUP_CODE)
                {
                    node = item;
                    break;
                }
            }
            // If not found, create new node
            if (node == null)
            {
                node = new TreeListViewItem(GROUP_CODE, 0);
                node.Name = GROUP_CODE;
                if (ViewName) node.Text = GROUP_TEXT;
                Items.Add(node);
            }

            //Add tiếp các cấp khác
            AddNode(1, node, data);
            
            OnTreeRowAdded(data.ToDataDictionary());
        }



        private void AddNode(int level, TreeListViewItem parent, DataRow data)//IDictionary<string, object>
        {
            if (level == _groupFieldList.Length)
            {
                parent.Tag = data;
                if (_viewColumns == null || _viewColumns.Length == 0)
                {
                    for (int i = 1; i < Columns.Count; i++)
                    {
                        var columnNAME = Columns[i].Name.ToUpper();
                        //if (IsInGroup(columnNAME)) continue;
                        if (data.Table.Columns.Contains(columnNAME))
                        {
                            var value = data[columnNAME];
                            parent.SubItems.Add(ObjectAndString.ObjectToString(value));
                        }
                        else
                        {
                            parent.SubItems.Add("");
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < Columns.Count; i++)
                    {
                        var columnNAME = Columns[i].Name.ToUpper();
                        //if (IsInGroup(columnNAME)) continue;
                        if (data.Table.Columns.Contains(columnNAME))
                        {
                            var value = data[columnNAME];
                            if (ObjectAndString.IsNumberType(value.GetType()))
                            {
                                var numberString = ObjectAndString.NumberToString(value, _viewDecimals[i-1],
                                    V6Options.M_NUM_POINT, ".");
                                parent.SubItems.Add(numberString);
                            }
                            else
                            {
                                parent.SubItems.Add(ObjectAndString.ObjectToString(value));
                            }
                        }
                        else
                        {
                            parent.SubItems.Add("");
                        }
                    }
                }
            }
            else // Đoạn code này giống với code của hàm ở trên.
            {
                var GROUP_FIELD = _groupFieldList[level].ToUpper();
                var GROUP_CODE = data[GROUP_FIELD].ToString().Trim();
                var NAME_FIELD = _groupNameFieldList[level];
                var GROUP_TEXT = data[NAME_FIELD].ToString().Trim();

                // Find parrent node
                TreeListViewItem node = null;
                foreach (TreeListViewItem item in parent.Items)
                {
                    if (item.Name == GROUP_CODE)
                    {
                        node = item;
                        break;
                    }
                }
                // If not found, create new node
                if (node == null)
                {
                    node = new TreeListViewItem(GROUP_CODE, level);
                    node.Name = GROUP_CODE;
                    if(ViewName) node.Text = GROUP_TEXT;
                    parent.Items.Add(node);
                }

                //Add tiếp các cấp khác
                AddNode(level+1, node, data);
            }
        }

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