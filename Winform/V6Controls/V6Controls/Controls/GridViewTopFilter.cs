using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Plugin control that can view sum of number value columns on a datagridview
    /// </summary>
    public partial class GridViewTopFilter : UserControl
    {
        /// <summary>
        /// Gán DataGridView cần xem tổng cộng.
        /// </summary>
        [Description("Chọn một DataGridView trên form.")]
        [TypeConverter(typeof(AConverter))]
        [DefaultValue(null)]
        public V6ColorDataGridView DataGridView
        {
            get { return _dgv; }
            set
            {
                DeConnectGridView();
                _dgv = value;
                _sumText = V6Setting.IsVietnamese ? "Lọc:" : "Filter:";
                OnDataGridViewChanged(value);
            }
        }
        private V6ColorDataGridView _dgv;

        /// <summary>
        /// Phần chữ đầu dòng.
        /// </summary>
        [Description("Phần chữ đầu dòng")]
        [DefaultValue("Filter")]
        protected string SumText
        {
            get { return _sumText; }
            set { _sumText = value; }
        }
        private string _sumText = "Filter:";

        /// <summary>
        /// Những cột không tính tổng, cách nhau bởi dấu chấm phẩy(;).
        /// </summary>
        [DefaultValue(null)]
        [Description("Những cột không tính tổng, cách nhau bởi dấu chấm phẩy(;).")]
        public string NoSumColumns { get { return _noSumColumns; }
            set
            {
                _noSumColumns = value.Replace(',', ';');
                _noSumColumns = _noSumColumns.Replace(" ", "");
                _NO_SUM_COLUMNS_FOR_CHECK = ";" + _noSumColumns.ToUpper() + ";";
            } }
        protected string _noSumColumns;
        protected string _NO_SUM_COLUMNS_FOR_CHECK="";

        public Condition SumCondition { get; set; }

        Pen pBoder;
        Brush bBackGround;
        Brush bTextColor;
        Font gFont;
        Font textFont;
        StringFormat stringFormat;

        public event EventHandler DataGridViewChanged;
        protected virtual void OnDataGridViewChanged(DataGridView dgv)
        {
            ConnectGridView(dgv);
            CaculateSumValues();
            var handler = DataGridViewChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void FixThisSizeLocation(DataGridView dgv)
        {
            if (Parent != dgv.Parent)
                dgv.Parent.Controls.Add(this);
            Left = dgv.Left;
            Top = dgv.Top - Height;
            Width = dgv.Width;
            DrawTempView();
        }

        private void ConnectGridView(DataGridView dgv)
        {
            if (dgv != null)
            {
                MyInit();
                FixThisSizeLocation(dgv);
                dgv.ColumnDisplayIndexChanged += dgv_DisplayIndexChanged;
                dgv.ColumnStateChanged += dgv_ColumnStateChanged;
                dgv.ColumnWidthChanged += dgv_ColumnWidthChanged;
                dgv.SelectionChanged += dgv_SelectionChanged;
                if (dgv is V6ColorDataGridView)
                {
                    var dgv6 = dgv as V6ColorDataGridView;
                    dgv6.RowSelectChanged += dgv_SelectionChanged_row;
                }
                dgv.DataBindingComplete += dgv_DataBindingComplete;
                dgv.Paint += dgv_Paint;
                //dgv.DataSourceChanged += dgv_DataSourceChanged;
                dgv.DataBindingComplete += dgv_DataSourceChanged;
                dgv.SizeChanged += dgv_SizeChanged;
                //dgv.columnheaderw
                dgv.LocationChanged += dgv_LocationChanged;
                //DrawSummary();
                
            }
        }

        private void dgv_DisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                if (_filterItems.ContainsKey(e.Column))
                {
                    if (!e.Column.Visible)
                        _filterItems[e.Column].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }


        void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                if (_filterItems.ContainsKey(e.Column))
                {
                    RelocationFlyingFilter(e.Column);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        void dgv_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            try
            {
                if (_filterItems.ContainsKey(e.Column))
                {
                    if (!e.Column.Visible)
                        _filterItems[e.Column].Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MadeFilterItems();
        }

        private Dictionary<DataGridViewColumn, V6ColorTextBox> _filterItems = new Dictionary<DataGridViewColumn, V6ColorTextBox>();
        private bool _created;
        private void MadeFilterItems()
        {
            try
            {
                if (!_created)
                {
                    _created = true;
                    foreach (DataGridViewColumn column in _dgv.Columns)
                    {
                        if (column.Visible)
                        {
                            AddFilterItem(column);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void AddFilterItem(DataGridViewColumn column)
        {
            try
            {
                var colorTextBox = new V6ColorTextBox();
                colorTextBox.Name = "txt" + column.DataPropertyName;
                //colorTextBox.GrayText = column.HeaderText;
                colorTextBox.BackColor = Color.Yellow;
                colorTextBox.LeaveColor = Color.Yellow;
                colorTextBox.UseSendTabOnEnter = false;
                _filterItems[column] = colorTextBox;
                colorTextBox.KeyDown += delegate(object sender, KeyEventArgs args)
                {
                    if (args.KeyData == (Keys.Shift | Keys.Enter))
                    {
                        _dgv.SaveSelectedCellLocation();
                        _dgv.RemoveFilter();
                        _dgv.LoadSelectedCellLocation();
                    }
                    else if (args.KeyCode == Keys.Enter)
                    {
                        _dgv.SaveSelectedCellLocation();
                        ApplyFilter();
                        _dgv.LoadSelectedCellLocation();
                    }
                };
                this.Controls.Add(colorTextBox);
                
                //colorTextBox.BringToFront();
                RelocationFlyingFilter(column);
                //colorTextBox.Focus();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ApplyFilter()
        {
            try
            {
                string filter_string = GenFilterString();
                
                var table = _dgv.TableSource;
                var view = _dgv.DataSource as DataView;
                // _view là view cũ trước đó hoặc tạo view mới nếu chưa có.
                var _view = view ?? new DataView(table);
                _view.RowFilter = filter_string;
                _dgv.DataSource = _view;

                _dgv.RecheckColor();
                _dgv.OnFilterChange();
            }
            catch (Exception ex)
            {
                
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

        private string GenFilterString()
        {
            string result = "";
            string operat0r = "like";
            foreach (KeyValuePair<DataGridViewColumn, V6ColorTextBox> item in _filterItems)
            {
                DataGridViewColumn _filter_column = item.Key;
                string value = item.Value.Text;
                if (!item.Key.Visible || value == string.Empty) continue;
                string value2 = null;
                var sss = ObjectAndString.SplitStringBy(value, '~');
                if (sss.Length == 2)
                {
                    value = sss[0];
                    value2 = sss[1];
                }
                string row_filter = "";
                if (ObjectAndString.IsStringType(_filter_column.ValueType))
                {
                    if (string.IsNullOrEmpty(operat0r)) operat0r = "=";
                    string svalue = FormatValue(ObjectAndString.ObjectToString(value), operat0r);
                    if (operat0r == "start") operat0r = "like";
                    row_filter = string.Format("{0} {1} '{2}'", _filter_column.DataPropertyName, operat0r, svalue);
                }
                else if (ObjectAndString.IsNumberType(_filter_column.ValueType))
                {
                    var num1 = ObjectAndString.ObjectToDecimal(value);
                    var num2 = ObjectAndString.ObjectToDecimal(value2);
                    if (num1 > num2)
                    {
                        row_filter = string.Format("{0} = {1}", _filter_column.DataPropertyName,
                            num1.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        row_filter = string.Format("({0} >= {1} and {0} <= {2})", _filter_column.DataPropertyName,
                            num1.ToString(CultureInfo.InvariantCulture), num2.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else if (ObjectAndString.IsDateTimeType(_filter_column.ValueType))
                {
                    var date1 = ObjectAndString.ObjectToFullDateTime(value).ToString("yyyy-MM-dd");
                    var date2 = ObjectAndString.ObjectToFullDateTime(value2).ToString("yyyy-MM-dd");
                    if (String.CompareOrdinal(date1, date2) > 0)
                    {
                        row_filter = string.Format("{0} = #{1}#", _filter_column.DataPropertyName, date1);
                    }
                    else
                    {
                        row_filter = string.Format("({0} >= #{1}# and {0} <= #{2}#)", _filter_column.DataPropertyName, date1, date2);
                    }
                }

                result += "and " + row_filter;
            }

            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        private void RelocationFlyingFilter(DataGridViewColumn column)
        {
            try
            {
                V6ColorTextBox colorTextBox = _filterItems[column];
                colorTextBox.Width = column.Width;
                var rec = _dgv.GetColumnDisplayRectangle(column.Index, false);
                colorTextBox.Top = 0;
                colorTextBox.Left = rec.Left;
                //_flyingFilterTextBox.GrayText = _flyColumn.HeaderText;
                //_flyingFilterTextBox.Text = _flyingSearchText.ContainsKey(_flyColumn.DataPropertyName) ? _flyingSearchText[_flyColumn.DataPropertyName] : string.Empty;
            }
            catch (Exception)
            {

            }
        }


        void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            CaculateSumValues();     
        }

        void dgv_SelectionChanged_row(object sender, SelectRowEventArgs row)
        {
            if (_dgv.RowCount > 1) CaculateSumValues();
        }

        void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgv.RowCount > 1) CaculateSumValues();
        }

        private void DeConnectGridView()
        {
            if (_dgv != null)
            {
                MyInit();
                //FixThisSizeLocation(dgv);
                _dgv.Paint -= dgv_Paint;
                _dgv.DataSourceChanged -= dgv_DataSourceChanged;
                _dgv.Paint -= dgv_SelectionChanged;
                _dgv.SizeChanged -= dgv_SizeChanged;
                _dgv.LocationChanged -= dgv_LocationChanged;
            }
        }

        void dgv_LocationChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_dgv);
        }

        void dgv_SizeChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_dgv);
        }

        void dgv_Paint(object sender, PaintEventArgs paintEventArgs)
        {
            DrawSummary();
        }

        private void DrawSummary()
        {
            return;
            try
            {
                Graphics g = CreateGraphics();
                g.FillRectangle(bBackGround, g.VisibleClipBounds);
                if (SumCondition != null && string.IsNullOrEmpty(SumCondition.OPER)) SumCondition.OPER = "=";
                //CaculateSumValues();

                foreach (DataGridViewColumn col in _dgv.Columns)
                {
                    if (!col.Visible) continue;
                    //var rec0 = _dgv.GetCellDisplayRectangle(col.Index, 1, false);
                    var rec = _dgv.GetColumnDisplayRectangle(col.Index, false);
                    var newRec = new Rectangle(new Point(rec.Location.X - 2, -1), new Size(rec.Width, 22));
                    var text = "";
                    var dataType = col.ValueType;
                    if (ObjectAndString.IsNumberType(dataType) && !_NO_SUM_COLUMNS_FOR_CHECK.Contains(";" + col.DataPropertyName.ToUpper() + ";"))
                    {
                        text = SumOfSelectedRowsByColumn(_dgv, col).ToString(col.DefaultCellStyle.Format);
                        text = text.Replace(V6Setting.SystemDecimalSeparator, "#");
                        text = text.Replace(",", ".");
                        text = text.Replace(" ", ".");
                        text = text.Replace("#", V6Options.M_NUM_POINT);
                    }

                    if (rec.Right > 0)
                    {
                        g.DrawRectangle(pBoder, newRec);
                        g.DrawString(text, textFont, bTextColor, newRec, stringFormat);
                    }
                }
                // Draw header
                g.DrawString(_sumText, textFont, bTextColor, g.VisibleClipBounds, new StringFormat() { LineAlignment = StringAlignment.Center });
            }
            catch
            {
                //
            }
        }

        public override void Refresh()
        {
            //base.Refresh();
            if(_dgv != null) DrawSummary();
        }

        private void DrawTempView()
        {
            if (_dgv == null) return;
            Graphics g = CreateGraphics();
            g.FillRectangle(bBackGround, g.VisibleClipBounds);
            g.DrawString(_sumText, textFont, bTextColor, g.VisibleClipBounds, new StringFormat() { LineAlignment = StringAlignment.Center });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if(DesignMode) DrawTempView();
        }
        
        public GridViewTopFilter()
        {
            InitializeComponent();
        }

        private void MyInit()
        {
            pBoder = new Pen(Color.Black);
            bBackGround = new SolidBrush(Color.AliceBlue);
            bTextColor = new SolidBrush(_dgv.ForeColor);

            gFont = _dgv.DefaultCellStyle.Font;
            textFont = new Font(gFont.FontFamily, gFont.Size, FontStyle.Bold);
            stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
        }

        private SortedDictionary<string, decimal> _sumValues; 
        private void CaculateSumValues()
        {
            try
            {
                if (_dgv == null) return;
                // Khởi tạo danh sách kết quả
                _sumValues = new SortedDictionary<string, decimal>();
                foreach (DataGridViewColumn column in _dgv.Columns)
                {
                    if (ObjectAndString.IsNumberType(column.ValueType))
                    {
                        _sumValues[column.DataPropertyName] = 0;
                    }
                }


                //if ((_dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect && _dgv.SelectedRows.Count > 1)
                //    || (_dgv.SelectionMode != DataGridViewSelectionMode.FullRowSelect && _dgv.SelectedCells.Count > 1))
                {
                    // Lấy những dòng được chọn để tính tổng
                    var rows = new SortedDictionary<int, DataGridViewRow>();

                    foreach (DataGridViewRow row in _dgv.Rows)
                    {
                        if (row.IsSelect()) rows[row.Index] = row;
                    }

                    if (rows.Count == 0)
                        foreach (DataGridViewCell cell in _dgv.SelectedCells)
                        {
                            rows[cell.RowIndex] = cell.OwningRow;
                        }

                    // Trường hợp chọn từ 2 dòng trở lên
                    if (rows.Count > 1)
                    {
                        foreach (DataGridViewRow row in rows.Values)
                        {
                            if (CheckSumCondition(row))
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (ObjectAndString.IsNumberType(cell.ValueType))
                                    {
                                        _sumValues[cell.OwningColumn.DataPropertyName] += ObjectAndString.ObjectToDecimal(cell.Value);
                                    }
                                }
                        }
                        return;
                    }
                }

                foreach (DataGridViewRow row in _dgv.Rows)
                {
                    if (CheckSumCondition(row))
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (ObjectAndString.IsNumberType(cell.ValueType))
                            {
                                _sumValues[cell.OwningColumn.DataPropertyName] += ObjectAndString.ObjectToDecimal(cell.Value);
                            }
                        }
                }
            }
            catch
            {
                //
            }
            
        }

        /// <summary>
        /// Tính tổng 1 cột của các dòng đang chọn, nếu chỉ chọn 1 dòng thì tính hết.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private decimal SumOfSelectedRowsByColumn(DataGridView dgv, DataGridViewColumn col)
        {
            var sum = 0m;
            if (_sumValues != null && _sumValues.ContainsKey(col.DataPropertyName))
            {
                return _sumValues[col.DataPropertyName];
            }

            if (dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect && dgv.SelectedRows.Count > 1)
            {
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (CheckSumCondition(row)) sum += ObjectAndString.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
                }
                return sum;
            }
            else if (dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
            {
                goto SumAll;
            }

            SortedDictionary<int, DataGridViewRow> rows = new SortedDictionary<int, DataGridViewRow>();
            if (dgv.SelectedCells.Count > 1)
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    rows[cell.RowIndex] = cell.OwningRow;
                }
                if (rows.Count > 1)
                {
                    foreach (DataGridViewRow row in rows.Values)
                    {
                        if (CheckSumCondition(row)) sum += ObjectAndString.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
                    }
                    return sum;
                }
            }

            SumAll:
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (CheckSumCondition(row)) sum += ObjectAndString.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
            }
            return sum;
        }

        private bool CheckSumCondition(DataGridViewRow row)
        {
            if (SumCondition == null || string.IsNullOrEmpty(SumCondition.FIELD)) return true;
            if(_dgv.Columns.Contains(SumCondition.FIELD))
                return ObjectAndString.CheckCondition(row.Cells[SumCondition.FIELD].Value, SumCondition.OPER,SumCondition.VALUE, true);

            return false;
        }

        internal class AConverter : ReferenceConverter
        {
            public AConverter()
                : base(typeof(DataGridView))
            {
            }
        }

        
    }

    //public class Condition
    //{
    //    public string FIELD { get; set; }
    //    public string OPER { get; set; }
    //    public string VALUE { get; set; }
    //}
}
