using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls.GridView;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    public class V6ColorDataGridView:DataGridView
    {
        public V6ColorDataGridView()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // V6ColorDataGridView
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.V6ColorDataGridView_CellPainting);
            this.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.V6ColorDataGridView_ColumnAdded);
            this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.V6ColorDataGridView_RowPostPaint);
            this.SelectionChanged += new System.EventHandler(this.V6ColorDataGridView_SelectionChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.V6ColorDataGridView_KeyDown);
            this.CellBeginEdit += V6ColorDataGridView_CellBeginEdit;
            this.EditingControlShowing += V6ColorDataGridView_EditingControlShowing;
            //this.DataError += V6ColorDataGridView_DataError;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        //void V6ColorDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    //Loại bỏ thông báo lỗi khi có gì đó sai trong gridview.
        //    e.ThrowException = false;
        //}

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            //this.WriteExLog(GetType() + ".OnDataError", e.Exception);
            V6ControlFormHelper.AddLastError(GetType() + ".OnDataError " + e.Exception.Message);
        }

        void V6ColorDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var textBox = e.Control as TextBox;
            if (textBox != null)
            {
                if (ObjectAndString.IsNumberType(CurrentCell.OwningColumn.ValueType))
                {
                    textBox.Text = "" + CurrentCell.Value;
                }
            }
        }

        void V6ColorDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditingCell = CurrentCell;
            EditingRow = CurrentCell.OwningRow;
            EditingColumn = CurrentCell.OwningColumn;
        }

        [DefaultValue(false)]
        [Description("Dùng Control + S để lọc số liệu hiển thị, Thêm Shift để reset.")]
        public bool Control_S { get { return control_s; } set { control_s = value; } }
        private bool control_s = false;
        
        [DefaultValue(false)]
        [Description("Dùng Control + A để chọn tất cả các dòng.")]
        public bool Control_A { get { return control_a; } set { control_a = value; } }
        private bool control_a = false;

        [DefaultValue(false)]
        [Description("Dùng Control + E để chỉnh sửa code.")]
        public bool Control_E { get { return control_e; } set { control_e = value; } }
        private bool control_e = false;
        
        [DefaultValue(false)]
        [Description("Dùng Control + Space để chọn dòng đang đứng.")]
        public bool Control_Space { get { return control_space; } set { control_space = value; } }
        private bool control_space = false;
        
        [DefaultValue(false)]
        [Description("Dùng Space_Bar để thay đổi trạng thái chọn của dòng đang đứng.")]
        public bool Space_Bar { get { return space_bar; } set { space_bar = value; } }
        private bool space_bar = false;

        public event Action FilterChange;
        protected virtual void OnFilterChange()
        {
            var handler = FilterChange;
            if (handler != null) handler();
        }

        /// <summary>
        /// Cột đang có cell chỉnh sửa. Vẫn giữ là cột đó sau khi sửa (cell_end_edit).
        /// </summary>
        public DataGridViewColumn EditingColumn { get; private set; }
        /// <summary>
        /// Hàng đang có cell chỉnh sửa. Vẫn giữ là hàng đó sau khi sửa (cell_end_edit).
        /// </summary>
        public DataGridViewRow EditingRow { get; private set; }
        public DataGridViewCell EditingCell { get; private set; }

        /// <summary>
        /// Ẩn tất cả các trường được chỉ định, các trường khác để nguyên.
        /// </summary>
        /// <param name="columns"></param>
        public void HideColumns(params string[] columns)
        {
            foreach (string column in columns)
            {
                if(string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null) g_column.Visible = false;
            }
        }
        /// <summary>
        /// Ẩn tất cả các trường khác, chỉ hiện các trường được chỉ định.
        /// </summary>
        /// <param name="columns"></param>
        public void ShowColumns(params string[] columns)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                column.Visible = false;
            }
            foreach (string column in columns)
            {
                if (string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null) g_column.Visible = true;
            }
        }

        /// <summary>
        /// Ẩn tất cả các trường được chỉ định, các trường khác để nguyên.
        /// </summary>
        /// <param name="columns"></param>
        public void HideColumns(Dictionary<string, string> columns)
        {
            foreach (string column in columns.Keys)
            {
                if (string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null) g_column.Visible = false;
            }
        }
        /// <summary>
        /// Ẩn tất cả các trường khác, chỉ hiện các trường được chỉ định.
        /// </summary>
        /// <param name="columns"></param>
        public void ShowColumns(Dictionary<string, string> columns)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                column.Visible = false;
            }
            foreach (string column in columns.Keys)
            {
                if (string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null) g_column.Visible = true;
            }
        }

        public void EnableEdit(params string[] columns)
        {
            ReadOnly = false;
            foreach (DataGridViewColumn column in Columns)
            {
                column.ReadOnly = true;
            }

            foreach (string column in columns)
            {
                if(string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null) g_column.ReadOnly = false;
            }
        }

        private void V6ColorDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            
            if (e.Column.ValueType == typeof (DateTime))
            {
                e.Column.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            else
            {
                var dataType = e.Column.ValueType;
                var field = e.Column.DataPropertyName.ToUpper();
                if (V6BusinessHelper.V6Struct1.ContainsKey(field))
                //&& V6BusinessHelper.V6Struct1[field].MaxLength>60)
                {
                    e.Column.Width = V6BusinessHelper.V6Struct1[field].ColumnWidth;
                }

                if (ObjectAndString.IsNumberType(dataType))
                {
                    e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    e.Column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
                    
                    e.Column.DefaultCellStyle.FormatProvider = V6Setting.V6_number_format_info;
                }
                else if (dataType == typeof (string))
                {
                    
                }

            }
        }

        private void V6ColorDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (CurrentRow!=null && CurrentRow.IsSelect())
            {
                DefaultCellStyle.SelectionBackColor = Color.Brown;
                DefaultCellStyle.SelectionForeColor = Color.White;
            }
            else
            {
                DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                DefaultCellStyle.SelectionForeColor = Color.White;
            }
        }

        private void V6ColorDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
                if (Rows[e.RowIndex].IsSelect())
                {
                    e.Graphics.DrawString("x", e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
                }
            }

        }

        /// <summary>
        /// Ẩn các cột được định nghĩa trong Aldm - trường A_Field
        /// </summary>
        /// <param name="tableName">ma_dm</param>
        public void HideColumnsAldm(string tableName)
        {
            try
            {
                var aldm_data = V6BusinessHelper.Select("aldm",
                            V6Setting.Language == "V" ? "A_Field" : "A_Field2",
                            "ma_dm='" + tableName + "'").Data;
                var s = "";
                if (aldm_data != null && aldm_data.Rows.Count > 0)
                    s = aldm_data.Rows[0][0].ToString().Trim();
                HideColumns(s.Split(','));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".HideColumnsAldm", ex);
            }
        }

        /// <summary>
        /// Ẩn tất cả các trường khác, chỉ hiện các trường được chỉ định trong B_Field.
        /// </summary>
        /// <param name="tableName"></param>
        public void ShowColumnsAldm(string tableName)
        {
            try
            {
                var aldm_data = V6BusinessHelper.Select("aldm",
                            V6Setting.Language == "V" ? "B_Field" : "B_Field2",
                            "ma_dm='" + tableName + "'").Data;
                var s = "";
                if (aldm_data != null && aldm_data.Rows.Count > 0)
                    s = aldm_data.Rows[0][0].ToString().Trim();
                if(!string.IsNullOrEmpty(s))
                ShowColumns(s.Split(','));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ShowColumnsAldm", ex);
            }
        }

        private bool _already_set_max_length;
        private V6TableStruct _tableStruct;
        private string _type;

        /// <summary>
        /// Gán maxlength cho textbox lúc edit trên gridview. Và type khác.
        /// </summary>
        /// <param name="tableStruct"></param>
        /// <param name="type">hỗ trợ: UPPER</param>
        public void FormatEditTextBox(V6TableStruct tableStruct, string type)
        {
            if (_already_set_max_length) return;
            _already_set_max_length = true;
            try
            {
                _tableStruct = tableStruct;
                _type = type;
                EditingControlShowing += FormatEditTextBox_EditingControlShowing;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatEditTextBox", ex);
            }
        }

        public void SetDataSource(object data)
        {
            AutoGenerateColumns = true;
            DataSource = null;
            DataSource = data;
        }

        /// <summary>
        /// Thuộc tính DataSource cải tiến dùng với kiểu DataTable
        /// AutoGenerateColumns = true;
        /// DataSource = null;
        /// DataSource = value;
        /// </summary>
        [DefaultValue(null)]
        public DataTable TableSource
        {
            get
            {
                var table = DataSource as DataTable;
                return table;
            }
            set
            {
                if (value == null || value.Columns.Count == 0)
                {
                    if(Rows.Count>0) TableSource.Rows.Clear();
                    Refresh();
                }
                else
                {
                    AutoGenerateColumns = true;
                    DataSource = null;
                    DataSource = value;
                }
            }
        }

        void FormatEditTextBox_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var textbox = e.Control as TextBox;
            if (textbox != null)
            {
                var field = CurrentCell.OwningColumn.DataPropertyName;
                var maxlength = _tableStruct[field.ToUpper()].MaxLength;
                textbox.MaxLength = maxlength;
                if(_type == "UPPER") textbox.CharacterCasing = CharacterCasing.Upper;
            }
        }

        private void V6ColorDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!Focused || CurrentCell == null) return;
            if (e.ColumnIndex == CurrentCell.ColumnIndex
                && e.RowIndex == CurrentCell.RowIndex)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                using (Pen p = new Pen(Color.LightSalmon, 1))
                {
                    Rectangle rect = e.CellBounds;
                    rect.Width -= 2;
                    rect.Height -= 2;
                    e.Graphics.DrawRectangle(p, rect);
                }
                e.Handled = true;
            }
        }

        private void V6ColorDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataSource != null)
            {
                
                if (Control_E && e.KeyData == (Keys.Control | Keys.E))
                {
                    DoCodeEditor();
                }

                if (e.KeyData == (Keys.Control | Keys.F))
                {
                    var f = new FindForm();
                    f.Find += f_Find;
                    f.ShowDialog(this);
                }
                else if (e.KeyData == (Keys.Control | Keys.A) && Control_A)
                {
                    e.Handled = true;
                    this.SelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.Space) && Control_Space)
                {
                    if (CurrentRow != null)
                    {
                        e.Handled = true;
                        CurrentRow.Select();
                    }
                }
                else if (e.KeyData == (Keys.Space) && Space_Bar)
                {
                    if (CurrentRow != null)
                    {
                        CurrentRow.ChangeSelect();
                    }
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    this.UnSelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.S) && Control_S)
                {
                    ShowFilterForm();
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.S) && Control_S)
                {
                    RemoveFilter();
                }
                else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.Z))
                {
                    FixSize();
                }
            }
        }

        private void DoCodeEditor()
        {
            try
            {
                CodeEditorForm form = new CodeEditorForm();
                form.UsingText = GetUsingText();
                form.ContentText = CurrentCell.Value.ToString();
                form.ShowDialog();
                string text = form.ContentText;
                CurrentCell.Value = text;
                CurrentCell.OwningColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                CurrentCell.OwningColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                CurrentCell.OwningRow.Height = 22*text.Split('\n').Length;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoCodeEditor", ex);
            }
        }

        private string GetUsingText()
        {
            string result = "";
            if (Columns.Contains("using"))
            {
                foreach (DataGridViewRow row in Rows)
                {
                    result += row.Cells["using"].Value;
                }
            }
            return result;
        }

        /// <summary>
        /// Cờ đánh dấu đã chạy fix size rồi.
        /// </summary>
        private bool runFixSize1 = false;
        private Size originalSize = new Size();
        private int fixSizeMode = 0;
        private void FixSize()
        {
            if (!runFixSize1)
            {
                originalSize = Size;
                runFixSize1 = true;
            }
            switch (fixSizeMode)
            {
                case 0:
                    fixSizeMode = 1;
                    var parent = this.Parent;
                    var newWidth = parent.Width - Left - 3;
                    var newHeight = parent.Height - Top - 3;
                    Size = new Size(newWidth, newHeight);
                    break;
                case 1:
                    fixSizeMode = 0;
                    Size = originalSize;
                    break;
                case 2:
                    fixSizeMode = 0;
                    break;
                default:
                    fixSizeMode = 0;
                    break;
            }
        }

        void f_Find(string text, bool up, bool first)
        {
            text = text.ToLower();
            if (up)
            {
                //Dòng đang đứng
                if (CurrentRow != null && CurrentCell != null)
                {
                    var startIndex = CurrentCell.ColumnIndex - (first ? 0 : 1);
                    for (int i = startIndex; i >= 0; i--)
                    {
                        var cell = CurrentRow.Cells[i];
                        if (cell.Visible && cell.Value.ToString().ToLower().Contains(text))
                        {
                            SelectCell(cell);
                            return;
                        }
                    }
                }

                //Các dòng tiếp theo
                var startRowIndex = RowCount-1;
                if (CurrentRow != null) startRowIndex = CurrentRow.Index - 1;
                for (int i = startRowIndex; i >= 0; i--)
                {
                    var cRow = Rows[i];
                    var startIndex = ColumnCount - 1;
                    for (int j = startIndex; j >= 0; j--)
                    {
                        var cell = cRow.Cells[j];
                        if (cell.Visible && cell.Value.ToString().ToLower().Contains(text))
                        {
                            SelectCell(cell);
                            return;
                        }
                    }
                }
            }
            else
            {
                //Dòng đang đứng
                if (CurrentRow != null && CurrentCell != null)
                {
                    var startIndex = CurrentCell.ColumnIndex + (first ? 0 : 1);
                    for (int i = startIndex; i < Columns.Count; i++)
                    {
                        var cell = CurrentRow.Cells[i];
                        if (cell.Visible && cell.Value.ToString().ToLower().Contains(text))
                        {
                            SelectCell(cell);
                            return;
                        }
                    }
                }

                //Các dòng tiếp theo
                var startRowIndex = 0;
                if (CurrentRow != null) startRowIndex = CurrentRow.Index + 1;
                for (int i = startRowIndex; i < Rows.Count; i++)
                {
                    var cRow = Rows[i];
                    foreach (DataGridViewCell cell in cRow.Cells)
                    {
                        if (cell.Visible && cell.Value.ToString().ToLower().Contains(text))
                        {
                            SelectCell(cell);
                            return;
                        }
                    }
                }
            }
            

        }

        private void SelectCell(DataGridViewCell cell)
        {
            CurrentCell = cell;
        }


        public void ShowFilterForm()
        {
            var form = new GridViewFilterForm(this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if(CurrentCell.ColumnIndex>=0)
                Filter(form.Field, form.Operator, form.Value, form.Value2, form.FindNext, form.FindOR);
            }
        }

        private DataView _view;
        private DataGridViewColumn _filter_column;
        private void Filter(string field, string operat0r, object value, object value2, bool next, bool or)
        {
            if (_filter_column != null)
            {
                if (_filter_column.HeaderText.StartsWith("["))
                    _filter_column.HeaderText = _filter_column.HeaderText
                        .Substring(1, _filter_column.HeaderText.Length - 2);
            }
            _filter_column = Columns[field];
            var table = TableSource;
            var view = DataSource as DataView;
            if (_filter_column != null && (table != null || view != null))
            {
                string row_filter = null;
                if (ObjectAndString.IsStringType(_filter_column.ValueType))
                {
                    if (string.IsNullOrEmpty(operat0r)) operat0r = "=";
                    string svalue = FormatValue(ObjectAndString.ObjectToString(value), operat0r);
                    if (operat0r == "start") operat0r = "like";
                    row_filter = string.Format("{0} {1} '{2}'", field, operat0r, svalue);
                }
                else if (ObjectAndString.IsNumberType(_filter_column.ValueType))
                {
                    var num1 = ObjectAndString.ObjectToDecimal(value);
                    var num2 = ObjectAndString.ObjectToDecimal(value2);
                    if (num1 > num2)
                    {
                        row_filter = string.Format("{0} = {1}", field,
                            num1.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        row_filter = string.Format("({0} >= {1} and {0} <= {2})", field,
                            num1.ToString(CultureInfo.InvariantCulture), num2.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else if(ObjectAndString.IsDateTimeType(_filter_column.ValueType))
                {
                    var date1 = ObjectAndString.ObjectToFullDateTime(value).ToString("yyyy-MM-dd");
                    var date2 = ObjectAndString.ObjectToFullDateTime(value2).ToString("yyyy-MM-dd");
                    if (String.CompareOrdinal(date1, date2) > 0)
                    {
                        row_filter = string.Format("{0} = #{1}#", field, date1);
                    }
                    else
                    {
                        row_filter = string.Format("({0} >= #{1}# and {0} <= #{2}#)", field, date1, date2);
                    }
                }

                _view = view ?? new DataView(table);
                if (next)
                {
                    var old_filter = string.IsNullOrEmpty(_view.RowFilter) ? "" : string.Format("({0})", _view.RowFilter);
                    if (old_filter.Length > 0) old_filter += " and ";
                    var new_filter = old_filter + row_filter;
                    _view.RowFilter = new_filter;
                }
                else if (or)
                {
                    var old_filter = string.IsNullOrEmpty(_view.RowFilter) ? "" : string.Format("({0})", _view.RowFilter);
                    if (old_filter.Length > 0) old_filter += " or ";
                    var new_filter = old_filter + row_filter;
                    _view.RowFilter = new_filter;
                }
                else
                {
                    _view.RowFilter = row_filter;
                }
                DataSource = _view;
                
                //_filter_column.HeaderCell.Style.BackColor = Color.Red;
                //EnableHeadersVisualStyles = false;
                if(!_filter_column.HeaderText.StartsWith("["))
                _filter_column.HeaderText = string.Format("[{0}]", _filter_column.HeaderText);

                OnFilterChange();
            }
        }

        private string FormatValue(string value, string Operator)
        {
            if (",=,<>,>,>=,<,<=,".Contains("," + Operator + ","))
                return string.Format("{0}", value.Replace("'", "''"));
            else if (Operator == "like")
                return string.Format("%{0}%", value.Replace("'", "''"));
            else if (Operator == "start")
                return string.Format("{0}%", value.Replace("'", "''"));
            return "";
        }

        private void RemoveFilter()
        {
            if (_view != null)
            {
                _view.RowFilter = null;
                //_filter_column.HeaderCell.Style.BackColor = Color.FromArgb(0, 0, 0, 0);
                //EnableHeadersVisualStyles = true;
                if (_filter_column.HeaderText.StartsWith("["))
                    _filter_column.HeaderText = _filter_column.HeaderText
                        .Substring(1, _filter_column.HeaderText.Length - 2);
                
                Refresh();
                OnFilterChange();
            }
        }

        /// <summary>
        /// Cho phép sửa trên những cột truyền vào.
        /// </summary>
        /// <param name="columns"></param>
        public void SetEditColumn(IList<string> columns)
        {
            ReadOnly = false;
            foreach (DataGridViewColumn column in Columns)
            {
                column.ReadOnly = true;
            }
            foreach (string column in columns)
            {
                var c = Columns[column];
                if (c != null)
                {
                    c.ReadOnly = false;
                }
            }
        }

    }
}
