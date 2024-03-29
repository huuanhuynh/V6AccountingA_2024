﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Plugin control that can view sum of number value columns on a datagridview
    /// </summary>
    public partial class GridViewSummary : UserControl
    {
        /// <summary>
        /// Gán DataGridView cần xem tổng cộng.
        /// </summary>
        [Description("Chọn một DataGridView để hiển thị tổng cộng.")]
        [TypeConverter(typeof(AConverter))]
        [DefaultValue(null)]
        public DataGridView DataGridView
        {
            get { return _dgv; }
            set
            {
                DeConnectGridView();
                _dgv = value;
                _sumText = V6Setting.IsVietnamese ? "Tổng:" : "Sum:";
                OnDataGridViewChanged(value);
            }
        }
        private DataGridView _dgv;

        /// <summary>
        /// Phần chữ đầu dòng.
        /// </summary>
        [Description("Phần chữ đầu dòng")]
        protected string SumText
        {
            get { return _sumText; }
            set { _sumText = value; }
        }
        private string _sumText = "Sum:";

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

        public string SumConditionString
        {
            get
            {
                string result = null;
                if (SumCondition != null && !string.IsNullOrEmpty(SumCondition.FIELD)) result = (V6Setting.IsVietnamese? "Điều kiện cộng: " : "Sum condition: ") + SumCondition;
                return result;
            }
        }

        Pen pBoder;
        Brush bBackGround;
        Brush bTextColor;
        Font gFont;
        Font textFont;
        StringFormat stringFormat;

        public event EventHandler DataGridViewChanged;
        protected virtual void OnDataGridViewChanged(DataGridView dgv)
        {
            ConnectGridView();
            CaculateSumValues();
            var handler = DataGridViewChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void FixThisSizeLocation(DataGridView dgv)
        {
            if (Parent != dgv.Parent)
                dgv.Parent.Controls.Add(this);
            Left = dgv.Left;
            Top = dgv.Bottom;
            Width = dgv.Width;
            DrawTempView();
        }

        private void ConnectGridView()
        {
            if (_dgv != null)
            {
                MyInit();
                FixThisSizeLocation(_dgv);

                _dgv.SelectionChanged += dgv_SelectionChanged;
                if (_dgv is V6ColorDataGridView)
                {
                    var dgv6 = _dgv as V6ColorDataGridView;
                    dgv6.RowSelectChanged += dgv_SelectionChanged_row;
                    dgv6.DataRowUpdated += dgv_DataRowChanged;
                }
                _dgv.Paint += dgv_Paint;
                _dgv.DataSourceChanged += dgv_DataSourceChanged;
                _dgv.SizeChanged += dgv_SizeChanged;
                _dgv.LocationChanged += dgv_LocationChanged;
            }
        }

        private void DeConnectGridView()
        {
            if (_dgv != null)
            {
                
                _dgv.SelectionChanged -= dgv_SelectionChanged;
                if (_dgv is V6ColorDataGridView)
                {
                    var dgv6 = _dgv as V6ColorDataGridView;
                    dgv6.RowSelectChanged -= dgv_SelectionChanged_row;
                    dgv6.DataRowUpdated -= dgv_DataRowChanged;
                }
                _dgv.Paint -= dgv_Paint;
                _dgv.DataSourceChanged -= dgv_DataSourceChanged;
                _dgv.SizeChanged -= dgv_SizeChanged;
                _dgv.LocationChanged -= dgv_LocationChanged;
            }
        }

        void dgv_DataRowChanged(IDictionary<string, object> data)
        {
            CaculateSumValues();
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
            // khi kéo qua kéo lại.
            DrawSummary();
        }

        Dictionary<string, Rectangle> REC_DIC = new Dictionary<string, Rectangle>();
        Graphics g;
        private void DrawSummary()
        {
            try
            {
                g = CreateGraphics();
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
                        text = SumOfSelectedRowsByColumn_DrawString(_dgv, col);
                    }

                    if (rec.Right > 0)
                    {
                        g.DrawRectangle(pBoder, newRec);
                        REC_DIC[col.DataPropertyName.ToUpper()] = newRec;
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
        
        public GridViewSummary()
        {
            InitializeComponent();
            this.MouseClick += GridViewSummary_MouseClick;
            menuCopy.Click += menuCopy_Click;
            menuCopyValue.Click += menuCopy_Click;
        }

        private void MyInit()
        {
            pBoder = new Pen(Color.Black);
            bBackGround = new SolidBrush(Color.AliceBlue);
            bTextColor = new SolidBrush(_dgv.ForeColor);

            gFont = _dgv.Font;
            textFont = new Font(gFont.FontFamily, gFont.Size, FontStyle.Bold);
            stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };
        }

        Point _point;
        void GridViewSummary_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _point = e.Location;
                copyMenuStrip1.Show(this, _point);
            }            
        }

        void menuCopy_Click(object sender, EventArgs e)
        {
            try
            {                
                foreach (KeyValuePair<string, Rectangle> ITEM in REC_DIC)
                {
                    if (ITEM.Value.Contains(_point))
                    {
                        var col = this._dgv.Columns[ITEM.Key];
                        if (sender == menuCopy)
                        {
                            if (ObjectAndString.IsNumberType(col.ValueType) && !_NO_SUM_COLUMNS_FOR_CHECK.Contains(";" + col.DataPropertyName.ToUpper() + ";"))
                            {
                                string text = SumOfSelectedRowsByColumn_DrawString(_dgv, col);
                                Clipboard.SetText(text);
                            }
                            else
                            {
                                Clipboard.Clear();
                            }
                        }
                        else if (sender == menuCopyValue)
                        {
                            if (ObjectAndString.IsNumberType(col.ValueType) && !_NO_SUM_COLUMNS_FOR_CHECK.Contains(";" + col.DataPropertyName.ToUpper() + ";"))
                            {
                                string text = SumOfSelectedRowsByColumn(_dgv, col).ToString(CultureInfo.InstalledUICulture);
                                Clipboard.SetText(text);
                            }
                            else
                            {
                                Clipboard.Clear();
                            }
                        }
                        return;
                    }
                }

                if (sender == menuCopy)
                {
                    Clipboard.SetText(_sumText);
                    return;
                }
            }
            catch (Exception ex)
            {
                Clipboard.SetText("ERR:" + ex.Message);
                return;
            }
            Clipboard.Clear();
        }

        private SortedDictionary<string, decimal> _SUM_VALUES; 
        private void CaculateSumValues()
        {
            try
            {
                if (_dgv == null) return;
                gFont = _dgv.Font;
                textFont = new Font(gFont.FontFamily, gFont.Size, FontStyle.Bold);
                // Khởi tạo danh sách kết quả
                _SUM_VALUES = new SortedDictionary<string, decimal>();
                foreach (DataGridViewColumn column in _dgv.Columns)
                {
                    if (ObjectAndString.IsNumberType(column.ValueType))
                    {
                        _SUM_VALUES[column.DataPropertyName.ToUpper()] = 0;
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
                                        _SUM_VALUES[cell.OwningColumn.DataPropertyName.ToUpper()] += ObjectAndString.ObjectToDecimal(cell.Value);
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
                                _SUM_VALUES[cell.OwningColumn.DataPropertyName.ToUpper()] += ObjectAndString.ObjectToDecimal(cell.Value);
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
            if (_SUM_VALUES != null && _SUM_VALUES.ContainsKey(col.DataPropertyName.ToUpper()))
            {
                return _SUM_VALUES[col.DataPropertyName.ToUpper()];
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

        private string SumOfSelectedRowsByColumn_DrawString(DataGridView dgv, DataGridViewColumn col)
        {
            string text = SumOfSelectedRowsByColumn(_dgv, col).ToString(col.DefaultCellStyle.Format);
            text = text.Replace(V6Setting.SystemDecimalSeparator, "#");
            text = text.Replace(",", ".");
            text = text.Replace(" ", ".");
            text = text.Replace("#", V6Options.M_NUM_POINT);
            return text;
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

    
}
