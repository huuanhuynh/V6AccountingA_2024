using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
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
                OnDataGridViewChanged(value);
            }
        }
        private V6ColorDataGridView _dgv;

        [Description("Tự động tạo filter hoặc phải gọi hàm MadeFilterItems")]
        [DefaultValue(true)]
        public bool Auto
        {
            get { return _auto;}
            set { _auto = value; }
        }
        protected bool _auto = true;
        
        //Pen pBoder;
        Brush bBackGround;
        //Brush bTextColor;
        //Font gFont;
        //Font textFont;
        //StringFormat stringFormat;

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
            Top = dgv.Top - Height;
            Width = dgv.Width;
            //DrawTempView();
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
                //dgv.DataSourceChanged += dgv_DataSourceChanged;
                
                dgv.SizeChanged += dgv_SizeChanged;
                //dgv.columnheaderw
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
                _dgv.SizeChanged -= dgv_SizeChanged;
                _dgv.LocationChanged -= dgv_LocationChanged;
            }
        }

        private void dgv_DisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                //RelocationAll();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }


        void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                if (_filterItems.ContainsKey(e.Column.DataPropertyName.ToUpper()))
                {
                    RelocationAll();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }

        void dgv_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            try
            {
                RelocationAll();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }

        void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if(Auto) MadeFilterItems();
        }

        private Dictionary<string, V6ColorTextBox> _filterItems = new Dictionary<string, V6ColorTextBox>();
        private bool _created;
        /// <summary>
        /// Tạo filter input items. Chỉ tạo 1 lần.
        /// </summary>
        public void MadeFilterItems()
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
                            AddFilterItem((DataGridViewTextBoxColumn)column);
                        }
                    }
                    if (!_auto) RelocationAll();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }

        private void AddFilterItem(DataGridViewColumn column)
        {
            try
            {
                string FIELD = column.DataPropertyName.ToUpper();
                var colorTextBox = new V6ColorTextBox();
                colorTextBox.Name = "txt" + FIELD;
                //colorTextBox.GrayText = column.HeaderText;
                //colorTextBox.BackColor = Color.Yellow;
                //colorTextBox.LeaveColor = Color.Yellow;
                colorTextBox.UseSendTabOnEnter = false;
                _filterItems[FIELD] = colorTextBox;
                colorTextBox.KeyDown += delegate(object sender, KeyEventArgs args)
                {
                    if (args.KeyCode == Keys.Enter)
                    {
                        _dgv.SaveSelectedCellLocation();
                        ApplyFilter();
                        _dgv.LoadSelectedCellLocation();
                    }
                };
                colorTextBox.Enter += delegate(object sender, EventArgs args)
                {
                    RelocationAll();
                };
                //if (ObjectAndString.IsDateTimeType(column.ValueType))
                //{
                //    colorTextBox.KeyUp += delegate(object sender, KeyEventArgs args)
                //    {
                //        var txt = (V6ColorTextBox) sender;
                //        if (txt.Text.Length < 4) return;
                //        int i1 = txt.Text.IndexOf('/');
                //        int i2 = txt.Text.LastIndexOf('/');
                //        if(i)

                //    };
                //}
                this.Controls.Add(colorTextBox);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }

        public void ApplyFilter()
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
                toolTip1.SetToolTip(lblHelp, filter_string);
                _dgv.RecheckColor();
                _dgv.OnFilterChange();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
            }
        }

        public void ResetFilter()
        {
            try
            {
                if (!_created) return;
                foreach (KeyValuePair<string, V6ColorTextBox> item in _filterItems)
                {
                    DataGridViewColumn _filter_column = _dgv.Columns[item.Key];
                    if (_filter_column == null || !_filter_column.Visible || item.Value.Text == string.Empty) continue;
                    item.Value.Text = string.Empty;

                }

                string filter_string = "";
                var table = _dgv.TableSource;
                var view = _dgv.DataSource as DataView;
                // _view là view cũ trước đó hoặc tạo view mới nếu chưa có.
                var _view = view ?? new DataView(table);
                _view.RowFilter = filter_string;
                _dgv.DataSource = _view;
                toolTip1.SetToolTip(lblHelp, filter_string);
                _dgv.RecheckColor();
                _dgv.OnFilterChange();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex, Name);
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
            foreach (KeyValuePair<string, V6ColorTextBox> item in _filterItems)
            {
                DataGridViewColumn _filter_column = _dgv.Columns[item.Key];
                if (_filter_column == null||!_filter_column.Visible || item.Value.Text == string.Empty) continue;
                string FIELD_NAME = _filter_column.DataPropertyName.ToUpper();
                string value = item.Value.Text;
                string value2 = null;
                var sss = ObjectAndString.SplitStringBy(value, '~');
                string row_filter = "";
                if (ObjectAndString.IsStringType(_filter_column.ValueType))
                {
                    if (string.IsNullOrEmpty(operat0r)) operat0r = "=";
                    string svalue = FormatValue(ObjectAndString.ObjectToString(value), operat0r);
                    if (operat0r == "start") operat0r = "like";
                    row_filter = string.Format("{0} {1} '{2}'", FIELD_NAME, operat0r, svalue);
                }
                else if (ObjectAndString.IsNumberType(_filter_column.ValueType))
                {
                    sss = ObjectAndString.SplitStringBy(value, '~');
                    value = item.Value.Text;
                    value2 = null;
                    if (sss.Length == 2)
                    {
                        value = sss[0];
                        value2 = sss[1];
                    }
                    var num1 = ObjectAndString.ObjectToDecimal(value);
                    if (value2 == null)
                    {
                        row_filter = string.Format("{0} = {1}", FIELD_NAME,
                            num1.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        var num2 = ObjectAndString.ObjectToDecimal(value2);
                        row_filter = string.Format("({0} >= {1} and {0} <= {2})", FIELD_NAME,
                            num1.ToString(CultureInfo.InvariantCulture), num2.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else if (ObjectAndString.IsDateTimeType(_filter_column.ValueType))
                {
                    sss = ObjectAndString.SplitStringBy(value,'~');
                    value = item.Value.Text;
                    value2 = null;
                    if (sss.Length == 2)
                    {
                        value = sss[0];
                        value2 = sss[1];
                    }

                    var date1 = ObjectAndString.ObjectToFullDateTime(value).ToString("yyyy-MM-dd");
                    if (value2 == null)
                    {
                        row_filter = string.Format("{0} = #{1}#", FIELD_NAME, date1);
                    }
                    else
                    {
                        var date2 = ObjectAndString.ObjectToFullDateTime(value2).ToString("yyyy-MM-dd");
                        row_filter = string.Format("({0} >= #{1}# and {0} <= #{2}#)", FIELD_NAME, date1, date2);
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
                V6ColorTextBox colorTextBox = _filterItems[column.DataPropertyName.ToUpper()];
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

        private void RelocationAll()
        {
            try
            {
                if (_filterItems == null) return;
                foreach (KeyValuePair<string, V6ColorTextBox> item in _filterItems)
                {
                    var column = _dgv.Columns[item.Key];
                    if (column == null) continue;
                    item.Value.Visible = column.Visible;
                    V6ColorTextBox colorTextBox = item.Value;
                    colorTextBox.Width = column.Width;
                    var rec = _dgv.GetColumnDisplayRectangle(column.Index, false);
                    if (rec.Width > 0)
                    {
                        colorTextBox.Top = 0;
                        colorTextBox.Left = rec.X - (column.Width - rec.Width);
                        colorTextBox.Visible = true;
                    }
                    else
                    {
                        colorTextBox.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        void dgv_SelectionChanged_row(object sender, SelectRowEventArgs row)
        {
            //if (_dgv.RowCount > 1) CaculateSumValues();
        }

        void dgv_SelectionChanged(object sender, EventArgs e)
        {
            //if (_dgv.RowCount > 1) CaculateSumValues();
        }

        
        void dgv_LocationChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_dgv);
        }

        void dgv_SizeChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_dgv);
        }
        
        public override void Refresh()
        {
            //base.Refresh();
        }

        private void DrawTempView()
        {
            if (_dgv == null) return;
            Graphics g = CreateGraphics();
            g.FillRectangle(bBackGround, g.VisibleClipBounds);
            //g.DrawString("          GridView Top Filter", textFont, bTextColor, g.VisibleClipBounds, new StringFormat() { LineAlignment = StringAlignment.Center });
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
            //pBoder = new Pen(Color.Black);
            bBackGround = new SolidBrush(Color.AliceBlue);
            //bTextColor = new SolidBrush(_dgv.ForeColor);

            //gFont = _dgv.DefaultCellStyle.Font;
            //textFont = new Font(gFont.FontFamily, gFont.Size, FontStyle.Bold);
            //stringFormat = new StringFormat
            //{
            //    Alignment = StringAlignment.Far,
            //    LineAlignment = StringAlignment.Center
            //};
        }
        
        internal class AConverter : ReferenceConverter
        {
            public AConverter()
                : base(typeof(DataGridView))
            {
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            try
            {
                string message = null;
                if (V6Setting.IsVietnamese)
                {
                    message = "Tìm chữ gõ bình thường và nhấn Enter." +
                              "\nTìm số gõ 1 số hoặc 2 số cách nhau bằng ~ " +
                              "\nTìm 1 ngày gõ ngày/tháng/năm hoặc 2 ngày cách nhau ~.";
                }
                else
                {
                    message = "Find text, normal typing and press Enter." +
                              "\nFind numbers enter 1 or 2 numbers separated by ~ " +
                              "\nFind in date enter day/month/year. Add ~day/month/year for period.";
                }
                this.ShowInfoMessage(message);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void lblReset_Click(object sender, EventArgs e)
        {
            _dgv.SaveSelectedCellLocation();
            ResetFilter();
            _dgv.LoadSelectedCellLocation();
        }
    }

    //public class Condition
    //{
    //    public string FIELD { get; set; }
    //    public string OPER { get; set; }
    //    public string VALUE { get; set; }
    //}
}
