using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
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
            this.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
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
            this.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.V6ColorDataGridView_CellBeginEdit);
            this.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(V6ColorDataGridView_CellEndEdit);
            this.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.V6ColorDataGridView_CellPainting);
            this.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.V6ColorDataGridView_CellParsing);
            this.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.V6ColorDataGridView_ColumnAdded);
            this.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.V6ColorDataGridView_EditingControlShowing);
            this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.V6ColorDataGridView_RowPostPaint);
            this.SelectionChanged += new System.EventHandler(this.V6ColorDataGridView_SelectionChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.V6ColorDataGridView_KeyDown);
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

        /// <summary>
        /// Khóa sự kiện.
        /// </summary>
        private bool LockGridView = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (LockGridView) return;
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (LockGridView) e.Handled = true;
            base.OnKeyDown(e);
        }

        void V6ColorDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ObjectAndString.IsNumberType(EditingColumn.ValueType) && !(EditingColumn is V6NumberDataGridViewColumn))
            {
                V6ControlFormHelper.ApplyNumberTextBox(e.Control);
            }
            //var textBox = e.Control as TextBox;
            //if (textBox != null)
            //{
            //    if (ObjectAndString.IsNumberType(CurrentCell.OwningColumn.ValueType))
            //    {
            //        textBox.Text = "" + CurrentCell.Value;
            //    }
            //}
        }

        void V6ColorDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditingCell = CurrentCell;
            EditingRow = CurrentCell.OwningRow;
            EditingColumn = CurrentCell.OwningColumn;
        }

        
        void V6ColorDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CongThuc_CellEndEdit_ApplyAllRow)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    ApCongThuc(i, e.ColumnIndex);
                }
            }
            else
            {
                ApCongThuc(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Áp dụng công thức cho tất cả các dòng khi sửa 1 ô trên gridview.
        /// </summary>
        [DefaultValue(false)]
        public bool CongThuc_CellEndEdit_ApplyAllRow { get; set; }
        private readonly Dictionary<string, string> CongThuc = new Dictionary<string, string>();
        /// <summary>
        /// Gán công thức tính toán, ghi đè nếu đã có.
        /// </summary>
        /// <param name="FIELD">Trường gây sự kiện khi CellEndEdit.</param>
        /// <param name="congThuc">Danh sách công thức tính toán, cách nhau bằng dấu ;</param>
        public void GanCongThuc(string FIELD, string congThuc)
        {
            CongThuc[FIELD.ToUpper()] = congThuc;
        }
        /// <summary>
        /// Thêm công thức tính toán, cộng dồn công thức nếu đã có.
        /// </summary>
        /// <param name="FIELD">Trường gây sự kiện khi CellEndEdit.</param>
        /// <param name="congThuc">
        /// <para>Danh sách công thức tính toán, cách nhau bằng dấu ;</para>
        /// <para>Dạng công thức: Field=Bieu_thuc</para>
        /// <para>Trong đó biểu thức là các phép toán +-*/!^Round(,)Int()[Sqrt()]</para></param>
        public void ThemCongThuc(string FIELD, string congThuc)
        {
            FIELD = FIELD.ToUpper();
            if (CongThuc.ContainsKey(FIELD) && !string.IsNullOrEmpty(CongThuc[FIELD]))
            {
                CongThuc[FIELD] += ";" + congThuc;
            }
            else
            {
                CongThuc[FIELD] = congThuc;
            }
        }

        public void ApCongThuc(int rowIndex, int columnIndex)
        {
            ApCongThuc(Rows[rowIndex], Columns[columnIndex].Name);
        }

        public void ApCongThuc(int rowIndex, string COLUMN_NAME)
        {
            ApCongThuc(Rows[rowIndex], COLUMN_NAME);
        }

        /// <summary>
        /// Biến lưu trữ các biến trong nhóm công thức.
        /// </summary>
        private SortedDictionary<string, decimal> APCONGTHUC_VARS = new SortedDictionary<string, decimal>(); 
        private void ApCongThuc(DataGridViewRow row, string COLUMN_NAME)
        {
            try
            {
                APCONGTHUC_VARS = new SortedDictionary<string, decimal>();
                COLUMN_NAME = COLUMN_NAME.ToUpper();
                if (CongThuc.ContainsKey(COLUMN_NAME))
                {
                    string chuoi_cac_cong_thuc = CongThuc[COLUMN_NAME] ?? "";
                    string[] list_congThuc = chuoi_cac_cong_thuc.Split(new[] {';'},
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (string cong_thuc in list_congThuc)
                    {
                        try
                        {
                            XuLyCongThucTinhToan(cong_thuc, row);
                        }
                        catch (Exception ex)
                        {
                            this.WriteExLog(GetType() + ".ApCongThuc Lỗi công thức: " + cong_thuc, ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApCongThuc", ex);
            }
            APCONGTHUC_VARS.Clear();
        }

        //private List<string> updateFieldList = new List<string>();
        private void XuLyCongThucTinhToan(string congThuc, DataGridViewRow cRow)
        {
            var ss = congThuc.Split('=');
            if (ss.Length == 2)
            {
                var field = ss[0].Trim();
                //updateFieldList.Add(field.ToUpper());
                var bieu_thuc = ss[1].Trim();

                //var cRow = dataGridView1.CurrentRow;
                cRow.Cells[field].Value = GiaTriBieuThuc(bieu_thuc, cRow);
            }
        }

        private decimal GiaTriBieuThuc(string bieu_thuc, DataGridViewRow cRow)
        {
            if (string.IsNullOrEmpty(bieu_thuc)) return 0;

            //alert(bieu_thuc);
            bieu_thuc = bieu_thuc.Replace(" ", ""); //Bỏ hết khoảng trắng
            bieu_thuc = bieu_thuc.Replace("--", "+"); //loại bỏ lặp dấu -
            bieu_thuc = bieu_thuc.Replace("+-", "-");
            bieu_thuc = bieu_thuc.Replace("-+", "-");
            bieu_thuc = bieu_thuc.Replace("++", "+"); //loại bỏ lặp dấu +
            bieu_thuc = bieu_thuc.Replace("*+", "*");
            bieu_thuc = bieu_thuc.Replace("/+", "/");

            //xử lý Int();
            bieu_thuc = bieu_thuc.ToUpper();
            int intOpenIndex = bieu_thuc.IndexOf("INT(", StringComparison.Ordinal);
            if (intOpenIndex >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, intOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                
                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + (int)GiaTriBieuThuc(b, cRow) + after + c, cRow);
            }

            //xử lý Round();
            bieu_thuc = bieu_thuc.ToUpper();
            int roundOpenIndex = bieu_thuc.IndexOf("ROUND(", StringComparison.Ordinal);
            if (roundOpenIndex >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, roundOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                var phayindex = b.LastIndexOf(',');
                var round1 = b.Substring(0, phayindex);
                var round2 = b.Substring(phayindex + 1);

                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + V6BusinessHelper.Vround(GiaTriBieuThuc(round1, cRow), (int)GiaTriBieuThuc(round2, cRow)) + after + c, cRow);//RoundV(giatribt(a),giatribt(b))
            }
            //xử lý phép toán trong ngoặc trước.
            if (bieu_thuc.IndexOf('(', 0) >= 0 || bieu_thuc.IndexOf(')', 0) >= 0)
            {
                var iopen = bieu_thuc.IndexOf('(', 0);
                var iclose = bieu_thuc.Length;
                for (var i = iopen; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '(') iopen = i;
                    else if (bieu_thuc[i] == ')')
                    {
                        iclose = i;
                        break;
                    }
                }
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, iopen);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + GiaTriBieuThuc(b, cRow) + after + c, cRow);//RoundV(giatribt(a),giatribt(b))
            }

            // có phép cộng trong biểu thức
            if (bieu_thuc.IndexOf('+') >= 0)
            {

                var values = bieu_thuc.Split('+');
                decimal sum = 0;
                for (var i = 0; i < values.Length; i++)
                {
                    sum += GiaTriBieuThuc(values[i], cRow);
                }
                return sum;

                //        var sp = bieu_thuc.LastIndexOf('+');//split point
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
                //        return GiaTriBieuThuc(values1) + GiaTriBieuThuc(values2);
            }

            // làm cho hết phép cộng rồi tới phép trừ    ////////////////////////// xử lý số âm hơi vất vả.
            if ((bieu_thuc.Split('-').Length - 1) >
                (bieu_thuc.Split(new[] { "*-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^-" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('-');
                //tim vi tri sp cuoi khong có */^ dung truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '-' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, cRow) - GiaTriBieuThuc(values2, cRow);
            }

            //phép nhân
            if (bieu_thuc.IndexOf('*', 0) >= 0)
            {

                var values = bieu_thuc.Split('*');
                decimal sum = 1;
                for (var i = 0; i < values.Length; i++)
                {
                    sum *= GiaTriBieuThuc(values[i], cRow);
                }
                return sum;

                //Phép Round (Round012.345)

                //        var sp = bieu_thuc.LastIndexOf('*') > bieu_thuc.LastIndexOf("*-") ? bieu_thuc.LastIndexOf('*') : bieu_thuc.LastIndexOf("*-");
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
                //        return GiaTriBieuThuc(values1) * GiaTriBieuThuc(values2);
            }
            if (bieu_thuc.IndexOf('/', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('/') > bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('/')
                    : bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, cRow) / GiaTriBieuThuc(values2, cRow);
            }
            if (bieu_thuc.IndexOf('^', 0) >= 0)
            {
                var sp = bieu_thuc.LastIndexOf('^') < bieu_thuc.LastIndexOf("^-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('^')
                    : bieu_thuc.LastIndexOf("^-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return (decimal)Math.Pow((double)GiaTriBieuThuc(values1, cRow), (double)GiaTriBieuThuc(values2, cRow));
            }
            // giai thừa
            if (bieu_thuc.IndexOf('!', 0) > 0)
            {
                var sp = bieu_thuc.LastIndexOf('!');
                var values1 = bieu_thuc.Substring(0, sp);
                return factorial((int)GiaTriBieuThuc(values1, cRow));
            }
            else if (bieu_thuc.Trim() == "")
            {
                return 0;
            }
            else
            {
                // Biểu thức lúc này là 1 trường hoặc một số cụ thể.
                if (Columns.Contains(bieu_thuc))
                {
                    return ObjectAndString.ObjectToDecimal(cRow.Cells[bieu_thuc].Value);
                }
                else if (bieu_thuc.StartsWith("{") && bieu_thuc.EndsWith("}") && APCONGTHUC_VARS.ContainsKey(bieu_thuc))
                {
                    return APCONGTHUC_VARS[bieu_thuc];
                }
                else if (bieu_thuc.StartsWith("M_ROUND"))
                {
                    return ObjectAndString.ObjectToInt(V6Options.V6OptionValues[bieu_thuc.ToUpper()]);
                }
                return ObjectAndString.ObjectToDecimal(bieu_thuc);
            }
        }

        private int factorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            var f = 1;
            for (var i = 2; i <= n; i++)
            {
                f *= i;
            }
            return f;
        }


        private void V6ColorDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (ObjectAndString.IsDateTimeType(Columns[e.ColumnIndex].ValueType))
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(e.Value.ToString(), "d/M/yyyy", null, DateTimeStyles.None, out dateTime))
                {
                    e.Value = dateTime;
                    e.ParsingApplied = true;
                }
            }
        }

        /// <summary>
        /// Dùng Control + S để lọc số liệu hiển thị, Thêm Shift để reset.
        /// </summary>
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
        /// Đổi kiểu cột DataGridView.
        /// </summary>
        /// <param name="columnName">Tên cột.</param>
        /// <param name="columnType">typeof(DataGridViewColumn)</param>
        /// <param name="newFormat">bỏ qua hoặc mặc định nếu null.</param>
        /// <returns></returns>
        public DataGridViewColumn ChangeColumnType(string columnName, Type columnType, string newFormat)
        {
            var oldTypeColumn = Columns[columnName];
            if (oldTypeColumn == null) return null;

            DataGridViewColumn newTypeColumn = null;
            if (columnType == typeof(V6NumberDataGridViewColumn))
            {
                newTypeColumn = new V6NumberDataGridViewColumn();
            }
            else if (columnType == typeof (V6VvarDataGridViewColumn))
            {
                newTypeColumn = new V6VvarDataGridViewColumn();
            }
            else if (columnType == typeof(V6DateTimeColorGridViewColumn))
            {
                newTypeColumn = new V6DateTimeColorGridViewColumn();
            }
            else if (columnType == typeof(V6DateTimePickerGridViewColumn))
            {
                newTypeColumn = new V6DateTimePickerGridViewColumn();
            }
            else if (columnType == typeof (DataGridViewTextBoxColumn))
            {
                newTypeColumn = new DataGridViewTextBoxColumn();
            }

            if (newTypeColumn == null) return null;
            newTypeColumn.Name = columnName;
            newTypeColumn.DataPropertyName = columnName;
            if (!string.IsNullOrEmpty(newFormat))
            {
                newTypeColumn.DefaultCellStyle.Format = newFormat;
            }
            else
            {
                if (!string.IsNullOrEmpty(oldTypeColumn.DefaultCellStyle.Format))
                {
                    newTypeColumn.DefaultCellStyle.Format = oldTypeColumn.DefaultCellStyle.Format;
                }
                else
                {
                    if (columnType == typeof (V6NumberDataGridViewColumn))
                    {
                        newTypeColumn.DefaultCellStyle.Format = "N2";
                    }
                }
            }

            Columns.Remove(oldTypeColumn);
            Columns.Add(newTypeColumn);
            newTypeColumn.DisplayIndex = oldTypeColumn.DisplayIndex;
            return newTypeColumn;
        }

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
            ShowColumns(false, columns);
        }

        /// <summary>
        /// Ẩn tất cả các trường khác, chỉ hiện các trường được chỉ định.
        /// </summary>
        /// <param name="order">Có sắp xếp lại hay không.</param>
        /// <param name="columns">Danh sách các cột hiển thị.</param>
        public void ShowColumns(bool order, params string[] columns)
        {
            if (columns == null || columns.Length == 0) return;
            int order_index = 0;
            foreach (DataGridViewColumn column in Columns)
            {
                column.Visible = false;
            }
            foreach (string column in columns)
            {
                if (string.IsNullOrEmpty(column)) continue;
                var g_column = Columns[column];
                if (g_column != null)
                {
                    g_column.Visible = true;
                    if (order)
                    {
                        g_column.DisplayIndex = order_index++;
                    }
                }
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
                    //e.Column.DefaultCellStyle.Format = V6Options.V6OptionValues["M_IP_R_TIEN"];
                    string COLUMN_NAME = e.Column.Name.ToUpper();
                    if (COLUMN_NAME.StartsWith("USER_ID")) goto FormatProvider;
                    if (COLUMN_NAME.StartsWith("STT")) goto FormatProvider;
                    
                    //var dataType = e.Column.ValueType;
                    if (dataType == typeof(decimal)
                        //|| dataType == typeof (int)
                        || dataType == typeof(double)
                        //|| dataType == typeof (long)
                        //|| dataType == typeof (short)
                        || dataType == typeof(float)
                        //|| dataType == typeof (Int16)
                        //|| dataType == typeof (Int32)
                        //|| dataType == typeof (Int64)
                        //|| dataType == typeof (uint)
                        //|| dataType == typeof (UInt16)
                        //|| dataType == typeof (UInt32)
                        //|| dataType == typeof (UInt64)
                        //|| dataType == typeof (byte)
                        //|| dataType == typeof (sbyte)
                        //|| dataType == typeof (Single)
                        )
                    {
                        e.Column.DefaultCellStyle.Format = "N2";
                    }
                FormatProvider:
                    e.Column.DefaultCellStyle.FormatProvider = V6Setting.V6_number_format_info;
                }
                else if (dataType == typeof (string))
                {
                    
                }

            }
        }

        public event EventHandler RowSelectChanged;
        public virtual void OnRowSelectChanged()
        {
            var handler = RowSelectChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler V6Changed;
        public virtual void OnV6Changed()
        {
            var handler = V6Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public delegate void EditingEventHandler(object sender, EditingEventArgs e);
        public event EditingEventHandler EditingPrepare;

        public class EditingEventArgs : EventArgs
        {
            public Control Control { get; set; }
            public V6NumberTextBox V6NumberTextBox
            {
                get { return Control as V6NumberTextBox; }
            }
            public V6VvarTextBox V6VvarTextBox
            {
                get { return Control as V6VvarTextBox; }
            }
            public V6LookupTextBox V6LookupTextBox
            {
                get { return Control as V6LookupTextBox; }
            }
            public V6DateTimeColor V6DateTimeColor
            {
                get { return Control as V6DateTimeColor; }
            }
            public V6DateTimePicker V6DateTimePicker
            {
                get { return Control as V6DateTimePicker; }
            }

            public DataGridView DataGridView { get; set; }
            public DataGridViewRow CurrentRow { get; set; }
            public DataGridViewCell CurrentCell { get; set; }
            public DataGridViewColumn CurrentColumn { get; set; }
        }
        
        public virtual void OnEditingPrepare(EditingEventArgs e)
        {
            var handler = EditingPrepare;
            if (handler != null) handler(this, e);
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

        /// <summary>
        /// Vẽ màu viền ô hiện tại.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void V6ColorDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    var eRow = Rows[e.RowIndex];
                    var eColumn = Columns[e.ColumnIndex];
                    var eCell = eRow.Cells[e.ColumnIndex];
                    if(ObjectAndString.IsNumberType(e.Value.GetType()) && ObjectAndString.ObjectToDecimal(e.Value) == 0)
                    {
                        e.Handled = true;
                        Color eBackColor = e.CellStyle.BackColor;
                        if (MultiSelect && SelectedRows.Contains(eRow) || SelectedCells.Contains(eCell))
                        {
                            eBackColor = DefaultCellStyle.SelectionBackColor;
                        }
                        else if (CurrentRow == eRow)
                        {
                            if (SelectionMode == DataGridViewSelectionMode.CellSelect && eCell == CurrentCell
                                || SelectionMode == DataGridViewSelectionMode.FullRowSelect)
                            {
                                // CurrentCell đang bị vẽ số đè ở phần vẽ viền ô được chọn
                                eBackColor = DefaultCellStyle.SelectionBackColor;
                            }
                        }
                        else if (SelectedColumns.Contains(eColumn) || SelectedCells.Contains(eCell))
                        {
                            eBackColor = DefaultCellStyle.SelectionBackColor;
                        }

                        // Tô màu nền
                        using (Brush b = new SolidBrush(eBackColor))
                        {
                            Rectangle rect = e.CellBounds;
                            rect.Width -= 1;
                            rect.Height -= 1;
                            e.Graphics.FillRectangle(b, rect);
                        }
                        // Vẽ viền ô
                        using (Pen p = new Pen(GridColor, 1))
                        {
                            Rectangle rect = e.CellBounds;
                            rect.X -= 1;
                            rect.Y -= 1;
                            e.Graphics.DrawRectangle(p, rect);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
            
            //Tô viền đỏ ô đang chọn.
            if (CurrentCell != null && e.ColumnIndex == CurrentCell.ColumnIndex && e.RowIndex == CurrentCell.RowIndex)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                Color c = Focused ? Color.LightSalmon : Color.White;
                using (Pen p = new Pen(c, 1))
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

                if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C))
                {
                    object o = GetClipboardContent() ?? (object) "";
                    Clipboard.SetDataObject(o, true);
                }
                else if (CurrentCell != null && e.KeyData == (Keys.Control | Keys.C))
                {
                    e.Handled = true;
                    Clipboard.SetText(ObjectAndString.ObjectToString(CurrentCell.Value));
                }
                else if (e.KeyData == (Keys.Control | Keys.F))
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
                form.ShowDialog(this);
                string text = form.ContentText;
                CurrentCell.Value = text;
                //Tự động phóng to ô hiển thị
                //CurrentCell.OwningColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //CurrentCell.OwningColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //CurrentCell.OwningRow.Height = 20*text.Split('\n').Length;
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
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if(CurrentCell.ColumnIndex>=0)
                Filter(form.Field, form.Operator, form.Value, form.Value2, form.FindNext, form.FindOR);
            }
        }

        private DataView _view;
        private DataGridViewColumn _filter_column;
        /// <summary>
        /// Lọc dữ liệu hiển thị trên gridView
        /// </summary>
        /// <param name="field">Trường lọc dữ liệu.</param>
        /// <param name="operat0r">Kiểu so sánh lọc, =, start, like, >...</param>
        /// <param name="value">Giá trị so sánh.</param>
        /// <param name="value2">Giá trị thứ 2 phải lớn hơn giá trị 1 cho trường hợp khoảng số.</param>
        /// <param name="_and_">Lọc chồng nếu đã lọc trước đó.</param>
        /// <param name="_or_">Lọc thêm nếu đã lọc trước đó.</param>
        public void Filter(string field, string operat0r, object value, object value2, bool _and_, bool _or_)
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
                if (_and_)
                {
                    var old_filter = string.IsNullOrEmpty(_view.RowFilter) ? "" : string.Format("({0})", _view.RowFilter);
                    if (old_filter.Length > 0) old_filter += " and ";
                    var new_filter = old_filter + row_filter;
                    _view.RowFilter = new_filter;
                }
                else if (_or_)
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

        /// <summary>
        /// Loại bỏ filter từ hàm Filer hoặc chức năng Control|S.
        /// </summary>
        public void RemoveFilter()
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

        ///// <summary>
        ///// <para>Gán format mặc định.</para>
        ///// <para>2 số lẻ</para>
        ///// <para>dd/MM/yyyy</para>
        ///// </summary>
        ///// <param name="exceptFields">Các trường dữ liệu không cần định dạng.</param>
        //public void Format(string exceptFields = null) { }
        //{
        //    if (!string.IsNullOrEmpty(exceptFields)) exceptFields = exceptFields.ToUpper();
        //    foreach (DataGridViewColumn column in Columns)
        //    {
        //        string COLUMN_NAME = column.Name.ToUpper();
        //        if (COLUMN_NAME.StartsWith("USER_ID")) continue;
        //        if (COLUMN_NAME.StartsWith("STT")) continue;
        //        if (!string.IsNullOrEmpty(exceptFields))
        //        {
        //            if (exceptFields.Contains(COLUMN_NAME)) continue;
        //        }
        //        var dataType = column.ValueType;
        //        if (dataType == typeof (decimal)
        //            //|| dataType == typeof (int)
        //            || dataType == typeof (double)
        //            //|| dataType == typeof (long)
        //            //|| dataType == typeof (short)
        //            || dataType == typeof (float)
        //            //|| dataType == typeof (Int16)
        //            //|| dataType == typeof (Int32)
        //            //|| dataType == typeof (Int64)
        //            //|| dataType == typeof (uint)
        //            //|| dataType == typeof (UInt16)
        //            //|| dataType == typeof (UInt32)
        //            //|| dataType == typeof (UInt64)
        //            //|| dataType == typeof (byte)
        //            //|| dataType == typeof (sbyte)
        //            //|| dataType == typeof (Single)
        //            )
        //        {
        //            column.DefaultCellStyle.Format = "N2";
        //        }
        //    }
        //}
        public void Format(string GRDSV1, string GRDFV1, string GRDH)
        {
            V6ControlFormHelper.FormatGridViewAndHeader(this, GRDSV1, GRDFV1, GRDH);
        }

        /// <summary>
        /// Tạo cột đông cứng.
        /// </summary>
        /// <param name="frozen"></param>
        public void SetFrozen(int frozen)
        {
            int frozen_count = 0;
            for (int i = 0; i < ColumnCount; i++)
            {
                var column = Columns[i];
                if (column.Visible && frozen_count < frozen)
                {
                    column.Frozen = true;
                    frozen_count++;
                }
                else
                {
                    column.Frozen = false;
                }
            }
        }

        public void Lock()
        {
            LockGridView = true;
        }
        public void UnLock()
        {
            LockGridView = false;
        }

        
    }
}
