using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using H_Utility.Converter;

namespace H_Controls.Controls
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
        public DataGridView DataGridView {
            get { return _dgv; }
            set
            {
                DeConnectGridView();
                _dgv = value;
                OnDataGridViewChanged(value);
            }
        }
        private DataGridView _dgv;

        /// <summary>
        /// Phần chữ đầu dòng.
        /// </summary>
        [DefaultValue("Sum:")]
        [Description("Phần chữ đầu dòng")]
        public string SumText
        {
            get { return _sumText; }
            set { _sumText = value; }
        }
        private string _sumText = "Sum:";

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

        private void ConnectGridView(DataGridView dgv)
        {
            if (dgv != null)
            {
                MyInit();
                FixThisSizeLocation(dgv);
                dgv.Paint += dgv_Paint;
                dgv.SizeChanged += dgv_SizeChanged;
                dgv.LocationChanged += dgv_LocationChanged;
                //DrawSummary();
            }
        }
        
        private void DeConnectGridView()
        {
            if (_dgv != null)
            {
                MyInit();
                //FixThisSizeLocation(dgv);
                _dgv.Paint -= dgv_Paint;
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
            textFont = _dgv.DefaultCellStyle.Font;
            Graphics g = CreateGraphics();
            g.FillRectangle(bBackGround, g.VisibleClipBounds);

            foreach (DataGridViewColumn col in _dgv.Columns)
            {
                if (!col.Visible) continue;
                //var rec0 = _dgv.GetCellDisplayRectangle(col.Index, 1, false);
                var rec = _dgv.GetColumnDisplayRectangle(col.Index, false);
                var newRec = new Rectangle(new Point(rec.Location.X - 1, 0), new Size(rec.Width, 22));
                var text = "";
                var dataType = col.ValueType;
                if (PrimitiveTypes.IsNumberType(dataType) && !_NO_SUM_COLUMNS_FOR_CHECK.Contains(";" + col.DataPropertyName.ToUpper() + ";"))
                {
                    text = SumOfSelectedRows(_dgv, col).ToString(col.DefaultCellStyle.Format);
                }

                if (rec.Right > 0)
                {
                    //g.DrawRectangle(pBoder, newRec);
                    g.DrawString(text, textFont, bTextColor, newRec, stringFormat);
                }
            }
            // Draw header
            g.DrawString(_sumText, textFont, bTextColor, g.VisibleClipBounds, new StringFormat() { LineAlignment = StringAlignment.Center });
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

        private decimal SumOfSelectedRows(DataGridView dgv, DataGridViewColumn col)
        {
            var sum = 0m;
            if (dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect && dgv.SelectedRows.Count > 1)
            {
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    sum += PrimitiveTypes.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
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
                        sum += PrimitiveTypes.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
                    }
                    return sum;
                }
            }

            SumAll:
            foreach (DataGridViewRow row in dgv.Rows)
            {
                sum += PrimitiveTypes.ObjectToDecimal(row.Cells[col.DataPropertyName].Value);
            }
            return sum;
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
