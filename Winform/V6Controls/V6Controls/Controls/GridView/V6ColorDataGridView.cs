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
            this.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.V6ColorDataGridView_CellEndEdit);
            this.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.V6ColorDataGridView_CellPainting);
            this.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.V6ColorDataGridView_CellParsing);
            this.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.V6ColorDataGridView_ColumnAdded);
            this.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.V6ColorDataGridView_ColumnHeaderMouseClick);
            this.CurrentCellChanged += new System.EventHandler(this.V6ColorDataGridView_CurrentCellChanged);
            this.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.V6ColorDataGridView_EditingControlShowing);
            this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.V6ColorDataGridView_RowPostPaint);
            this.SelectionChanged += new System.EventHandler(this.V6ColorDataGridView_SelectionChanged);
            this.Enter += new System.EventHandler(this.V6ColorDataGridView_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.V6ColorDataGridView_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        void V6ColorDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var column = Columns[e.ColumnIndex];
                V6ControlFormHelper.SetStatusText(string.Format("{0}:{1} {2}",
                    column.DataPropertyName, column.HeaderText, column.ValueType));
            }
        }

        

        //void V6ColorDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    //Loại bỏ thông báo lỗi khi có gì đó sai trong gridview.
        //    e.ThrowException = false;
        //}

        public override DataObject GetClipboardContent()
        {
            if (lock_copy)
            {
                return new DataObject(CurrentCell == null ? "" : CurrentCell.Value);
            }
            if (use_v6_copy && EditingCell == null && (ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                return GetClipboardContentV6();
            }

            return base.GetClipboardContent();
        }

        private DataObject GetClipboardContentV6()
        {
            string result = "";
            SortedDictionary<int,DataGridViewRow> rows = new SortedDictionary<int, DataGridViewRow>();
            SortedDictionary<int, DataGridViewColumn> cols = new SortedDictionary<int, DataGridViewColumn>();
            foreach (DataGridViewCell cell in SelectedCells)
            {
                rows[cell.RowIndex] = cell.OwningRow;
                cols[cell.OwningColumn.DisplayIndex] = cell.OwningColumn;
            }

            // Column header
            result += "\r\n";
            foreach (KeyValuePair<int, DataGridViewColumn> colitem in cols)
            {
                if (!result.EndsWith("\r\n")) result += "\t";
                result += colitem.Value.HeaderText;
            }

            // Data tab
            foreach (KeyValuePair<int, DataGridViewRow> rowitem in rows)
            {
                result += "\r\n";
                foreach (KeyValuePair<int, DataGridViewColumn> colitem in cols)
                {
                    if (!result.EndsWith("\r\n")) result += "\t";
                    result += GetCellStringValue(rowitem.Value.Cells[colitem.Value.Index]);
                }
            }
            //for (int i = row_min; i <= row_max; i++)
            //{
            //    result += "\r\n";
            //    for (int j = col_min; j <= col_max; j++)
            //    {
            //        if (!Columns[j].Visible) continue;
            //        if (!result.EndsWith("\r\n")) result += "\t";
            //        result += GetCellStringValue(Rows[i].Cells[j]);
            //    }
            //}
            if (result.Length > 2) result = result.Substring(2);
            DataObject result_do = new DataObject(result);
            return result_do;
        }

        private string GetCellStringValue(DataGridViewCell cell)
        {
            if (ObjectAndString.IsNumberType(cell.OwningColumn.ValueType) && !string.IsNullOrEmpty(cell.OwningColumn.DefaultCellStyle.Format))
            {
                return ObjectAndString.ObjectToString(cell.Value, cell.OwningColumn.DefaultCellStyle.Format);
            }
            if (ObjectAndString.IsStringType(cell.OwningColumn.ValueType))
            {
                return ObjectAndString.ObjectToString(cell.Value).Replace("\t", "");
            }
            return ObjectAndString.ObjectToString(cell.Value);
        }

        /// <summary>
        /// Di chuyển xuống dòng tiếp theo. Nếu hết dòng trả về null.
        /// </summary>
        /// <returns></returns>
        public DataGridViewRow GotoNextRow()
        {
            if (CurrentRow == null && RowCount > 0)
            {
                return CurrentRow;
            }
            if (CurrentRow.Index < RowCount - 1 && CurrentCell != null)
            {
                SaveSelectedCellLocation();
                _saveRowIndex++;
                LoadSelectedCellLocation();
                return CurrentRow;
            }
            return null;
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            //this.WriteExLog(GetType() + ".OnDataError", e.Exception);
            V6ControlFormHelper.WriteExLog(GetType() + ".OnDataError ", e.Exception);
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
            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.Handled = true;
            //    ProcessDialogKey(Keys.Tab);
            //}
            base.OnKeyDown(e);
        }

        // Chuyển Enter thành Tab khi Edit cell
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter && enter_to_tab)
                return base.ProcessDialogKey(Keys.Tab);
            else
                return base.ProcessDialogKey(keyData);
        }

        private void V6ColorDataGridView_Enter(object sender, EventArgs e)
        {
            ChangeEnterToTab();
        }

        /// <summary>
        /// Cờ đã áp dụng chuyển Enter thành tab.
        /// </summary>
        private bool apply_keydown2;
        /// <summary>
        /// Chuyển Enter thành Tab trong Gridview.
        /// </summary>
        private void ChangeEnterToTab()
        {
            if (!apply_keydown2)
            {
                this.KeyDown += V6ColorDataGridView_KeyDown2;
                apply_keydown2 = true;
            }
        }

        /// <summary>
        /// Cờ thay đổi tắt/bật enter_to_tab
        /// </summary>
        public bool enter_to_tab = true;
        void V6ColorDataGridView_KeyDown2(object sender, KeyEventArgs e)
        {
            if (LockGridView) return;
            if (e.KeyCode == Keys.Enter && enter_to_tab)
            {
                e.Handled = true;
                ProcessDialogKey(Keys.Tab);
            }
        }

        void V6ColorDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ObjectAndString.IsNumberType(EditingColumn.ValueType) && !(EditingColumn is V6NumberDataGridViewColumn))
            {
                ApplyNumberTextBox(e.Control);
            }
            else
            {
                DeApplyNumberTextBox(e.Control);
            }
            //var textBox = e.Control as TextBox;
            //if (textBox != null)
            //{
            //    if (ObjectAndString.IsNumberType(CurrentCell.OwningColumn.ValueType))
            //    {
            //        textBox.Text = "" + CurrentCell.Value;
            //    }
            //}

            var textBox = e.Control as V6VvarTextBox;
            if (textBox != null)
            {
                textBox.ResetLookupInfo();
                textBox.ResetAutoCompleteSource();
            }
        }

        private bool _numberTextBoxApplyed = false;
        public void ApplyNumberTextBox(Control control)
        {
            if (!_numberTextBoxApplyed)
            {
                control.KeyPress += control_KeyPress;
                _numberTextBoxApplyed = true;
            }
        }

        public void DeApplyNumberTextBox(Control control)
        {
            try
            {
                if (_numberTextBoxApplyed) control.KeyPress -= control_KeyPress;
            }
            catch
            {
                //
            }
        }

        static void control_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control control = (Control)sender;
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && control.Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && control.Text.Length > 0)
            {
                e.Handled = true;
            }
        }


        void V6ColorDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            EditingCell = CurrentCell;
            EditingRow = CurrentCell.OwningRow;
            EditingColumn = CurrentCell.OwningColumn;
            //for (int i = 0; i < RowCount; i++)
            //{
            //    _old_values[i] = Rows[i].Cells[EditingColumn.DataPropertyName].Value;
            //}
        }

        
        void V6ColorDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (CongThuc_CellEndEdit_ApplyAllRow)
            {
                for (int i = 0; i < RowCount; i++)
                {
                    ApCongThuc(i, e.ColumnIndex);
                    ApValid(i, e.ColumnIndex);
                }
            }
            else
            {
                ApCongThuc(e.RowIndex, e.ColumnIndex);
                ApValid(e.RowIndex, e.ColumnIndex);
            }
        }

        /// <summary>
        /// Áp dụng công thức cho tất cả các dòng khi sửa 1 ô trên gridview.
        /// </summary>
        [DefaultValue(false)]
        public bool CongThuc_CellEndEdit_ApplyAllRow { get; set; }
        public readonly Dictionary<string, string> CongThuc = new Dictionary<string, string>();
        private readonly Dictionary<string, string> CongThuc_Valid = new Dictionary<string, string>();
        /// <summary>
        /// Gán công thức tính toán, ghi đè nếu đã có.
        /// </summary>
        /// <param name="FIELD">Trường gây sự kiện khi CellEndEdit.</param>
        /// <param name="congThuc">
        /// <para>Danh sách công thức tính toán, cách nhau bằng dấu ;</para>
        /// <para>Dạng công thức: Field=Bieu_thuc</para>
        /// <para>Trong đó biểu thức là các phép toán +-*/!^Round(,)Int()[Sqrt()]</para>
        /// <para>Các giá trị là số đơn thuần hoặc tên các trường.</para>
        /// <para>Các biến số được đặt trong ngoặc vuông.</para></param>
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
        /// <para>Trong đó biểu thức là các phép toán +-*/!^Round(,)Int()[Sqrt()]</para>
        /// <para>Các giá trị là số đơn thuần hoặc tên các trường.</para>
        /// <para>Các biến số được đặt trong ngoặc vuông.</para></param>
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

        public void ApValid(int rowIndex, int columnIndex)
        {
            ApValid(Rows[rowIndex], Columns[columnIndex].Name);
        }

        public void ApValid(int rowIndex, string COLUMN_NAME)
        {
            ApValid(Rows[rowIndex], COLUMN_NAME);
        }

        public void ApCongThuc(int rowIndex, string COLUMN_NAME)
        {
            ApCongThuc(Rows[rowIndex], COLUMN_NAME);
        }

        /// <summary>
        /// Biến lưu trữ các biến trong nhóm công thức.
        /// </summary>
        private SortedDictionary<string, decimal> CONGTHUC_VARS = new SortedDictionary<string, decimal>(); 
        private void ApCongThuc(DataGridViewRow row, string COLUMN_NAME)
        {
            try
            {
                CONGTHUC_VARS = new SortedDictionary<string, decimal>();
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
            CONGTHUC_VARS.Clear();
        }

        //private Dictionary<int,object> _old_values = null;
        private void ApValid(DataGridViewRow row, string COLUMN_NAME)
        {
            try
            {
                CONGTHUC_VARS = new SortedDictionary<string, decimal>();
                COLUMN_NAME = COLUMN_NAME.ToUpper();
                if (CongThuc_Valid.ContainsKey(COLUMN_NAME))
                {
                    string chuoi_cac_cong_thuc = CongThuc_Valid[COLUMN_NAME] ?? "";
                    var value = GiaTriBieuThuc(chuoi_cac_cong_thuc, row);
                    if (value < 0)
                    {
                        row.Cells[COLUMN_NAME].Value = row.Cells[COLUMN_NAME_VALID].Value;
                        ApCongThuc(row, COLUMN_NAME);
                    }
                    //string[] list_congThuc = chuoi_cac_cong_thuc.Split(new[] { ';' },
                    //    StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string cong_thuc in list_congThuc)
                    //{
                    //    try
                    //    {
                    //        XuLyCongThucTinhToan(cong_thuc, row);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        this.WriteExLog(GetType() + ".ApCongThuc Lỗi công thức: " + cong_thuc, ex);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ApCongThuc", ex);
            }
            CONGTHUC_VARS.Clear();
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

                if (Columns.Contains(field))
                    cRow.Cells[field].Value = GiaTriBieuThuc(bieu_thuc, cRow);
                else
                    CONGTHUC_VARS[field.ToUpper()] = GiaTriBieuThuc(bieu_thuc, cRow);
                
            }
        }

        /// <summary>
        /// Tìm vị trí dấu đóng ngoặc
        /// </summary>
        /// <param name="bieu_thuc">Biểu thức đại số (giới hạn trong chương trình này).</param>
        /// <param name="iopen">Vị trí dấu mở ngoặc biết trước, nếu cho sai, hàm vẫn cho là có mở ngoặc ngay vị trí đó.</param>
        /// <returns>Vị trí đóng ngoặc hoặc vị độ dài chuỗi (lastindex+1) nếu không tìm thấy.</returns>
        private int FindCloseBrackets(string bieu_thuc, int iopen)
        {
            int length = bieu_thuc.Length;
            if (iopen < 0 || iopen >= length)
            {
                throw new Exception("iopen out of range!");
            }

            int opencount = 1;
            for (var i = iopen + 1; i < bieu_thuc.Length; i++)
            {
                if (bieu_thuc[i] == '(')
                {
                    opencount++;
                }
                else if (bieu_thuc[i] == ')')
                {
                    opencount--;
                    if (opencount == 0)
                    {
                        return i;
                    }
                }
            }
            return bieu_thuc.Length;
        }

        /// <summary>
        /// Liền trước biểu thức trong ngoặc, hàm hoặc số là những dấu này thì không gán phép nhân
        /// </summary>
        private string NoMultiplicationBefore = "+-*/(,^";
        /// <summary>
        /// Liền sau biểu thức trong ngoặc, hàm hoặc số là những dấu này thì không gán phép nhân
        /// </summary>
        private string NoMultiplicationAfter = "+-*/),^!";

        private decimal GiaTriBieuThuc(string bieu_thuc, DataGridViewRow DATA)
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

            bieu_thuc = bieu_thuc.ToUpper();
            // function open index
            int foi = -1;

            #region === INT(x) ===
            foi = bieu_thuc.IndexOf("INT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;// bieu_thuc.IndexOf('(', intOpenIndex+1);
                var iclose = FindCloseBrackets(bieu_thuc, iopen);// bieu_thuc.Length;

                string before = "", after = "";

                // a là biểu thức trước hàm int()
                // b là biểu thức trong hàm int()
                // c là biểu thức sau hàm int()
                // a int(b) c cuối a và đầu c đã có dấu phép tính kết nối.
                var a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";

                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);

                var c = "";
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc("" + a + before + (int)GiaTriBieuThuc(b, DATA) + after + c, DATA);
            }
            #endregion === INT(x) ===

            #region === SIN(x) ===
            foi = bieu_thuc.IndexOf("SIN(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a sin(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var result = GiaTriBieuThuc(a + before + ((decimal)Math.Sin((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
                return result;
            }
            #endregion === SIN(x) ===

            #region === COS(x) ===
            foi = bieu_thuc.IndexOf("COS(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a cos(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + ((decimal)Math.Cos((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === COS(x) ===

            #region === TAN(x) ===
            foi = bieu_thuc.IndexOf("TAN(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a tan(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + ((decimal)Math.Tan((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === TAN(x) ===

            #region === COT(x) ===
            foi = bieu_thuc.IndexOf("COT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a cot(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + (1m / (decimal)Math.Tan((double)GiaTriBieuThuc(b, DATA))).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === COT(x) ===

            #region === SQRT(x) ===
            foi = bieu_thuc.IndexOf("SQRT(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 4;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a sqrt(b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + Sqrt(GiaTriBieuThuc(b, DATA)).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === SQRT(x) ===

            #region === ROUND(x,a) ===
            foi = bieu_thuc.LastIndexOf("ROUND(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 5;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a round(b1,b2) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var phayindex = b.LastIndexOf(',');
                var b1 = b.Substring(0, phayindex);
                var b2 = b.Substring(phayindex + 1);

                return GiaTriBieuThuc(a + before + V6BusinessHelper.Vround(GiaTriBieuThuc(b1, DATA), (int)GiaTriBieuThuc(b2, DATA)).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === ROUND(x,a) ===

            #region === MOD(x,Y) ===
            foi = bieu_thuc.LastIndexOf("MOD(", StringComparison.Ordinal);
            if (foi >= 0)
            {
                var iopen = foi + 3;
                var iclose = FindCloseBrackets(bieu_thuc, iopen);
                // a mod(b1,b2) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, foi);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[foi - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                var phayindex = b.LastIndexOf(',');
                var b1 = b.Substring(0, phayindex);
                var b2 = b.Substring(phayindex + 1);

                return GiaTriBieuThuc(
                    a + before
                    + (GiaTriBieuThuc(b1, DATA) % (int)GiaTriBieuThuc(b2, DATA)).ToString(CultureInfo.InvariantCulture)
                    + after + c, DATA);
            }
            #endregion === MOD(x,a) ===

            #region === () xử lý phép toán trong ngoặc trong cùng trước. ===
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
                // a (b) c
                string a = "", before = "", b = "", after = "", c = "";
                a = bieu_thuc.Substring(0, iopen);
                if (a.Length > 0 && NoMultiplicationBefore.IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*";
                b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1);
                if (iclose + 1 < bieu_thuc.Length) c = bieu_thuc.Substring(iclose + 1);
                if (c.Length > 0 && NoMultiplicationAfter.IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*";

                return GiaTriBieuThuc(a + before + GiaTriBieuThuc(b, DATA).ToString(CultureInfo.InvariantCulture) + after + c, DATA);
            }
            #endregion === () ===

            #region === + ===
            if (bieu_thuc.IndexOf('+') > 0 &&
                (bieu_thuc.Split('+').Length - 1) >
                (bieu_thuc.Split(new[] { "*+" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/+" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^+" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('+');
                //tim vi tri sp cuoi khong có */^ đứng truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '+' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) + GiaTriBieuThuc(values2, DATA);
            }
            #endregion === + ===

            #region === - === làm cho hết phép cộng rồi tới phép trừ    ////////////////////////// xử lý số âm hơi vất vả.
            if (bieu_thuc.IndexOf('-') > 0 &&
                (bieu_thuc.Split('-').Length - 1) >
                (bieu_thuc.Split(new[] { "*-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "/-" }, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] { "^-" }, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('-');
                //tim vi tri sp cuoi khong có */^ đứng truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '-' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) - GiaTriBieuThuc(values2, DATA);
            }
            #endregion === - ===

            #region === * ===//phép nhân
            if (bieu_thuc.IndexOf('*', 0) >= 0)
            {
                var values = bieu_thuc.Split('*');
                decimal sum = 1;
                for (var i = 0; i < values.Length; i++)
                {
                    sum *= GiaTriBieuThuc(values[i], DATA);
                }
                return sum;
            }
            #endregion === * ===

            #region === / ===  Chia
            if (bieu_thuc.IndexOf('/', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('/') > bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('/')
                    : bieu_thuc.LastIndexOf("/-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) / GiaTriBieuThuc(values2, DATA);
            }
            #endregion === / ===

            #region === % ===   MOD(x,y) chia lấy dư
            if (bieu_thuc.IndexOf('%', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('%') > bieu_thuc.LastIndexOf("%-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('%')
                    : bieu_thuc.LastIndexOf("%-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1, DATA) % GiaTriBieuThuc(values2, DATA);
            }
            #endregion === % ===

            if (bieu_thuc.IndexOf('^', 0) >= 0)
            {
                var sp = bieu_thuc.LastIndexOf('^') < bieu_thuc.LastIndexOf("^-", StringComparison.InvariantCulture)
                    ? bieu_thuc.LastIndexOf('^')
                    : bieu_thuc.LastIndexOf("^-", StringComparison.InvariantCulture);

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return (decimal)Math.Pow((double)GiaTriBieuThuc(values1, DATA), (double)GiaTriBieuThuc(values2, DATA));
            }
            // giai thừa
            if (bieu_thuc.IndexOf('!', 0) > 0)
            {
                var sp = bieu_thuc.LastIndexOf('!');
                var values1 = bieu_thuc.Substring(0, sp);
                return factorial((int)GiaTriBieuThuc(values1, DATA));
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
                    return ObjectAndString.ObjectToDecimal(DATA.Cells[bieu_thuc].Value);
                }
                else if (CONGTHUC_VARS.ContainsKey(bieu_thuc.ToUpper()))
                {
                    return CONGTHUC_VARS[bieu_thuc.ToUpper()];
                }
                else if (bieu_thuc.StartsWith("{") && bieu_thuc.EndsWith("}") && CONGTHUC_VARS.ContainsKey(bieu_thuc))
                {
                    return CONGTHUC_VARS[bieu_thuc];
                }
                else if (bieu_thuc.StartsWith("M_ROUND"))
                {
                    return ObjectAndString.ObjectToInt(V6Options.V6OptionValues[bieu_thuc.ToUpper()]);
                }

                int pointindex = bieu_thuc.IndexOf('.');
                while (pointindex >= 0 && bieu_thuc.Length > pointindex && (bieu_thuc.EndsWith("0") || bieu_thuc.EndsWith(".")))
                {
                    bieu_thuc = bieu_thuc.Substring(0, bieu_thuc.Length - 1);
                }
                return ObjectAndString.ObjectToDecimal(bieu_thuc);
            }
        }

        private static decimal Sqrt(decimal value)
        {
            return (decimal)Math.Sqrt((double)value);
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
        [Description("Dùng Space_Bar để thay đổi trạng thái chọn của dòng đang đứng.")]
        public bool Space_Bar { get { return space_bar; } set { space_bar = value; } }
        private bool space_bar = false;

        [DefaultValue(false)]
        [Description("Khóa copy nhiều ô.")]
        public bool LockCopy { get { return lock_copy; } set { lock_copy = value; } }
        private bool lock_copy = false;

        [DefaultValue(true)]
        [Description("Dùng hàm GetClipboardContentV6 để lấy dữ liệu khi Shift copy.")]
        public bool UseV6Copy { get { return use_v6_copy; } set { use_v6_copy = value; } }
        private bool use_v6_copy = true;


        public event Action FilterChange;

        public virtual void OnFilterChange()
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
        /// <param name="newFormat">bỏ qua hoặc mặc định nếu null. Cvvar-N2</param>
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
                if (columnType == typeof (V6VvarDataGridViewColumn) && newFormat.StartsWith("C"))
                {
                    ((V6VvarDataGridViewColumn) newTypeColumn).Vvar = newFormat.Substring(1);
                }
                else
                {
                    newTypeColumn.DefaultCellStyle.Format = newFormat;
                }
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

            // Get old column properties
            newTypeColumn.HeaderText = oldTypeColumn.HeaderText;

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

        /// <summary>
        /// Format mặc định cho các loại dữ liệu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        public delegate void RowSelectEventHandler(object sender, SelectRowEventArgs e);
        [Description("Sự kiện bắt đầu thay đổi trạng thái select khi dùng hàm Select mở rộng.")]
        public event RowSelectEventHandler RowSelect;
        public virtual void OnRowSelect(SelectRowEventArgs e)
        {
            if (RowSelectChanged_running) return;
            var handler = RowSelect;
            if (handler != null)
            {
                try
                {
                    RowSelectChanged_running = true;
                    handler(this, e);
                    RowSelectChanged_running = false;
                }
                catch
                {
                    RowSelectChanged_running = false;
                    throw;
                }
            }
        }
        /// <summary>
        /// Sự kiện thay đổi trạng thái select khi dùng hàm Select mở rộng.
        /// </summary>
        [Description("Sự kiện kết thúc thay đổi trạng thái select khi dùng hàm Select mở rộng.")]
        public event RowSelectEventHandler RowSelectChanged;

        private bool RowSelectChanged_running = false;
        public virtual void OnRowSelectChanged(SelectRowEventArgs e)
        {
            if (RowSelectChanged_running) return;
            var handler = RowSelectChanged;
            if (handler != null)
            {
                try
                {
                    RowSelectChanged_running = true;
                    handler(this, e);
                    RowSelectChanged_running = false;
                }
                catch
                {
                    RowSelectChanged_running = false;
                    throw;
                }
            }
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
        /// Gridview đã được format thông qua hàm FormatGridViewAldm của chính nó hay chưa?
        /// </summary>
        public bool IsFormated { get { return _formated; } }
        private bool _formated;
        /// <summary>
        /// Chuyển trạng thái IsFormated thành true;
        /// </summary>
        public void Formated()
        {
            _formated = true;
        }
        /// <summary>
        /// Định dạng lại gridview theo cấu hình trong bảng Aldm.
        /// </summary>
        /// <param name="ma_dm">Mã danh mục [MA_DM] trong bảng Aldm.</param>
        /// <param name="force">Format bất chấp, mặc định false - format 1 lần không format lại.</param>
        /// <returns>Có hoặc không format được gridview.</returns>
        public bool FormatGridViewAldm(string ma_dm, bool force = false)
        {
            if (!force && _formated) return true;
            try
            {
                AldmConfig config = ConfigManager.GetAldmConfig(ma_dm);
                if (config.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(this, config.GRDS_V1, config.GRDF_V1, config.GRDH_LANG_V1);
                    _formated = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridViewAldm", ex);
            }
            return false;
        }

        public bool FormatGridViewAldm(AldmConfig config, bool force = false)
        {
            if (!force && _formated) return true;
            try
            {
                if (config.HaveInfo)
                {
                    V6ControlFormHelper.FormatGridViewAndHeader(this, config.GRDS_V1, config.GRDF_V1, config.GRDH_LANG_V1);
                    _formated = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridViewAldm", ex);
            }
            return false;
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
                    if((ObjectAndString.IsNumberType(eColumn.ValueType) && ObjectAndString.ObjectToDecimal(e.Value) == 0)
                        || (ObjectAndString.IsDateTimeType(eColumn.ValueType) && ObjectAndString.ObjectToFullDateTime(e.Value).Date == _1900))
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

                        //Tô viền đỏ ô đang chọn.
                        if (CurrentCell != null && e.ColumnIndex == CurrentCell.ColumnIndex && e.RowIndex == CurrentCell.RowIndex)
                        {
                            // e.Paint... là dòng vẽ giá trị 0
                            if (ObjectAndString.IsNumberType(eColumn.ValueType))
                                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                            Color c = Focused ? Color.LightSalmon : Color.White;
                            using (Pen p = new Pen(c, 1))
                            {
                                Rectangle rect = e.CellBounds;
                                rect.Width -= 2;
                                rect.Height -= 2;
                                e.Graphics.DrawRectangle(p, rect);
                            }
                            //e.Handled = true;
                        }
                    }
                    else
                    {
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
                }
            }
            catch (Exception ex)
            {
                ;
            }
            
            
        }
        private DateTime _1900 = new DateTime(1900,1,1);
        
        private void V6ColorDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
            if (DataSource != null)
            {
                
                if (Control_E && e.KeyData == (Keys.Control | Keys.E))
                {
                    DoCodeEditor();
                }
                
                if (e.KeyData == (Keys.Control | Keys.Shift | Keys.C))
                {
                    //object o = GetClipboardContent() ?? (object) "";
                    object o = null;
                    if (lock_copy)
                    {
                        o = new DataObject(CurrentCell == null ? "" : CurrentCell.Value);
                    }
                    else if (use_v6_copy)
                    {
                        o = GetClipboardContentV6();
                    }
                    else
                    {
                        o = base.GetClipboardContent();
                    }
                    if (o == null) o = "";
                    Clipboard.SetDataObject(o, true);
                }
                else if (CurrentCell != null && e.KeyData == (Keys.Control | Keys.C))
                {
                    e.Handled = true;
                    string text = "";
                    if (ObjectAndString.IsNumberType(CurrentCell.OwningColumn.ValueType))
                    {
                        text = ObjectAndString.ObjectToDecimal(CurrentCell.Value).ToString(CultureInfo.InstalledUICulture);
                    }
                    else
                    {
                        text = ObjectAndString.ObjectToString(CurrentCell.Value).TrimEnd();
                    }
                    
                    if (string.IsNullOrEmpty(text))
                    {
                        Clipboard.Clear();
                    }
                    else
                    {
                        Clipboard.SetText(text);
                    }
                }
                else if (e.KeyData == (Keys.Control | Keys.F))
                {
                    var f = new FindForm();
                    f.Find += f_Find;
                    f.ShowDialog(this);
                }
                //else if (e.KeyData == (Keys.Control | Keys.Shift | Keys.F))
                //{
                //    SwitchFlyingFilter();
                //}
                else if (e.KeyData == (Keys.Control | Keys.A) && Control_A)
                {
                    e.Handled = true;
                    SelectAllRow();
                }
                else if (e.KeyData == (Keys.Control | Keys.U))
                {
                    UnSelectAllRow();
                }
                else if (Space_Bar && (e.KeyData == (Keys.Space) || e.KeyData == (Keys.Control | Keys.Space)))
                {
                    if (CurrentRow != null)
                    {
                        ChangeSelect(CurrentRow);
                    }
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

#region ====== FLYING_FILTER ======

        private V6ColorTextBox _flyingFilterTextBox = null;
        private bool _flyingFilterActive = false;
        private DataGridViewColumn _flyColumn;
        private SortedDictionary<string, string> _flyingSearchText;
        private void SwitchFlyingFilter()
        {
            if (_flyingFilterActive)
            {
                SwitchFlyingFilterOff();
            }
            else
            {
                SwitchFlyingFilterOn();
                GetInfoFlyingTextBox();
                RelocationFlyingFilter();
            }
        }

        private void SwitchFlyingFilterOn()
        {
            if (_flyingFilterTextBox == null) CreateFlyingFilterTextBox();
            else
            {
                _flyingFilterTextBox.Visible = true;
                _flyingFilterActive = true;
            }
        }

        private void SwitchFlyingFilterOff()
        {
            _flyingFilterTextBox.Visible = false;
            _flyingFilterActive = false;
        }

        private void CreateFlyingFilterTextBox()
        {
            try
            {
                _flyingSearchText = new SortedDictionary<string, string>();
                _flyingFilterTextBox = new V6ColorTextBox();
                _flyingFilterTextBox.BackColor = Color.Yellow;
                _flyingFilterTextBox.LeaveColor = Color.Yellow;
                _flyingFilterTextBox.UseSendTabOnEnter = false;
                _flyingFilterTextBox.KeyDown += delegate(object sender, KeyEventArgs args)
                {
                    if (args.KeyData == (Keys.Shift | Keys.Enter))
                    {
                        SaveSelectedCellLocation();
                        RemoveFilter();
                        LoadSelectedCellLocation();
                    }
                    else if (args.KeyCode == Keys.Enter)
                    {
                        SaveSelectedCellLocation();
                        FilterFlyingTextBox();
                        LoadSelectedCellLocation();
                    }
                };
                Parent.Controls.Add(_flyingFilterTextBox);
                _flyingFilterTextBox.BringToFront();
                _flyingFilterActive = true;
                RelocationFlyingFilter();
                _flyingFilterTextBox.Focus();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void GetInfoFlyingTextBox()
        {
            var cell = CurrentCell;
            if (cell == null) return;
            _flyColumn = cell.OwningColumn;
        }

        private void FilterFlyingTextBox()
        {
            _flyingSearchText[_flyColumn.DataPropertyName] = _flyingFilterTextBox.Text;
            if (ObjectAndString.IsNumberType(_flyColumn.ValueType))
            {
                var sss = ObjectAndString.SplitStringBy(_flyingFilterTextBox.Text, '~');
                if (sss.Length == 0)
                {
                    RemoveFilter();
                }
                else if (sss.Length == 1)
                {
                    Filter(_flyColumn.DataPropertyName, "=", sss[0], null, false, false);
                }
                else
                {
                    Filter(_flyColumn.DataPropertyName, "=", sss[0], sss[1], false, false);
                }
            }
            else
            {
                Filter(_flyColumn.DataPropertyName, "like", _flyingFilterTextBox.Text, null, false, false);
            }
        }

        private void RelocationFlyingFilter()
        {
            try
            {
                _flyColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                _flyingFilterTextBox.Width = _flyColumn.Width;
                var rec = GetColumnDisplayRectangle(_flyColumn.Index, false);
                _flyingFilterTextBox.Top = Top + 1;// - 25;
                _flyingFilterTextBox.Left = Left + rec.Left;
                _flyingFilterTextBox.GrayText = _flyColumn.HeaderText;
                _flyingFilterTextBox.Text = _flyingSearchText.ContainsKey(_flyColumn.DataPropertyName) ? _flyingSearchText[_flyColumn.DataPropertyName] : string.Empty;
            }
            catch (Exception)
            {

            }
        }

        

#endregion flying_filter

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

        /// <summary>
        /// Hàm mở rộng, chọn dòng, đổi thành màu cam, Gán Tag = "x"
        /// </summary>
        /// <param name="row"></param>
        public void Select(DataGridViewRow row)
        {
            if (row.DataGridView != this) return;   // Check

            SelectRowEventArgs eventArgs = new SelectRowEventArgs(row){Select=true};
            OnRowSelect(eventArgs);
            if (eventArgs.CancelSelect) goto Cancel;

            row.DefaultCellStyle.BackColor = Color.FromArgb(247, 192, 91);

            if (row.DataGridView.Columns.Contains("Tag"))
            {
                row.Cells["Tag"].Value = "x";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                row.HeaderCell.Tag = "x";
            }

            if (row.DataGridView.CurrentRow == row)
            {
                row.DataGridView.DefaultCellStyle.SelectionBackColor = Color.Brown;
                row.DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            }

            OnRowSelectChanged(eventArgs);
        Cancel:
            ;
        }
        /// <summary>
        /// Hàm mở rộng, bỏ chọn, gán Tag = "";
        /// </summary>
        /// <param name="row"></param>
        public void UnSelect(DataGridViewRow row)
        {
            if (row.DataGridView != this) return;   // Check
            SelectRowEventArgs eventArgs = new SelectRowEventArgs(row);
            OnRowSelect(eventArgs);
            if (eventArgs.CancelSelect) goto Cancel;
            if (row.Index % 2 == 0)
                row.DefaultCellStyle.BackColor = row.DataGridView
                    .RowsDefaultCellStyle.BackColor;
            else
                row.DefaultCellStyle.BackColor = row.DataGridView
                    .AlternatingRowsDefaultCellStyle.BackColor;

            if (row.DataGridView.Columns.Contains("Tag"))
            {
                row.Cells["Tag"].Value = "";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                row.HeaderCell.Tag = "";
            }

            if (row.DataGridView.CurrentRow == row)
            {
                row.DataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                row.DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            }

            OnRowSelectChanged(eventArgs);
        Cancel:
            ;
        }
        /// <summary>
        /// Hàm mở rộng, chọn theo điều kiện
        /// </summary>
        /// <param name="row"></param>
        /// <param name="select"></param>
        public void Select(DataGridViewRow row, bool select)
        {
            if (select) Select(row);
            else UnSelect(row);
        }

        /// <summary>
        /// Chọn tất cả các dòng
        /// </summary>
        public void SelectAllRow()
        {
            foreach (DataGridViewRow row in Rows)
            {
                Select(row);
            }
        }
        /// <summary>
        /// Hàm mở rộng, chọn tất cả dòng theo điều kiện
        /// </summary>
        /// <param name="select"></param>
        public void SelectAllRow(bool select)
        {
            if (select)
            {
                SelectAllRow();
            }
            else
            {
                UnSelectAllRow();
            }
        }
        /// <summary>
        /// Hàm mở rộng, bỏ chọn tất cả dòng
        /// </summary>
        public void UnSelectAllRow()
        {
            foreach (DataGridViewRow row in Rows)
            {
                UnSelect(row);
            }
        }

        /// <summary>
        /// Kiểm tra cell [Tag].Trim() != "" hoặc HeaderCell.Tag là được chọn
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsSelect(DataGridViewRow row)
        {
            //Có 2 trường hợp, Nếu có Cột Tag
            if (row.DataGridView.Columns.Contains("Tag"))
            {
                var tag = (row.Cells["Tag"].Value ?? "").ToString().Trim();
                return tag != "";
            }
            else // Hoặc dùng HeaderCell.Tag
            {
                if (!(row.HeaderCell.Tag is string)) return false;
                var tag = row.HeaderCell.Tag.ToString().Trim();
                return tag != "";
            }
        }

        public void ChangeSelect(DataGridViewRow row)
        {
            Select(row, !IsSelect(row));
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

        public DataView _view;
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

                // _view là view cũ trước đó hoặc tạo view mới nếu chưa có.
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

                RecheckColor();
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
                RecheckColor();
                OnFilterChange();
            }
        }

        public void RecheckColor()
        {
            try
            {
                foreach (DataGridViewRow row in Rows)
                {
                    if (row.IsSelect())
                    {
                        row.DefaultCellStyle.BackColor = Color. FromArgb(247, 192, 91);

                        if (row.DataGridView.CurrentRow == row)
                        {
                            row.DataGridView.DefaultCellStyle.SelectionBackColor = Color.Brown;
                            row.DataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        /// <summary>
        /// Cho phép sửa trên những cột truyền vào.
        /// </summary>
        /// <param name="columns">Field:CVvar:InitFilter; Field:CVvar - Field:N2</param>
        public void SetEditColumn(IList<string> columns)
        {
            ReadOnly = false;
            foreach (DataGridViewColumn column in Columns)
            {
                column.ReadOnly = true;
            }
            foreach (string column in columns)
            {
                var sss = column.Split(new[] { ':' }, 5);
                var key = sss[0];
                string cVvar_N2 = "", filter = null, tableLabel = null, oper = null;

                if (sss.Length >= 2)
                {
                    cVvar_N2 = sss[1].Trim();
                }
                if (sss.Length >= 3)
                {
                    filter = sss[2].Replace("''", "'");
                }

                var c = Columns[key];
                if (c != null)
                {
                    if (cVvar_N2.StartsWith("C"))
                    {
                        var newTypeColumn = ChangeColumnType(key, typeof(V6VvarDataGridViewColumn), cVvar_N2);
                        newTypeColumn.ReadOnly = false;
                        ((V6VvarDataGridViewColumn)newTypeColumn).InitFilter = filter;
                    }
                    else if (cVvar_N2.StartsWith("N"))
                    {
                        var newTypeColumn = ChangeColumnType(key, typeof(V6NumberDataGridViewColumn), cVvar_N2);
                        newTypeColumn.ReadOnly = false;
                    }
                    else
                    {
                        c.ReadOnly = false;
                    }
                    
                    
                }
            }
        }
        
        /// <summary>
        /// Cho phép sửa trên những cột truyền vào.
        /// </summary>
        /// <param name="columns">Nhiều cột trong 1 mảng hoặc trong các biến khác nhau. Field:CVvar:InitFilter; Field:CVvar - Field:N2</param>
        public void SetEditColumnParams(params string[] columns)
        {
            SetEditColumn(columns);
        }

        /// <summary>
        /// Đổi kiểu cột và cho phép edit nếu E hoặc không định nghĩa.
        /// </summary>
        /// <param name="editFieldsFormat">Field:E:Cvvar;Field:E:N2;Field:E:D0;Field:R:D1...</param>
        public void ChangeEditColumnType(string editFieldsFormat)
        {
            try
            {
                List<DataGridViewColumn> cList = new List<DataGridViewColumn>();
                foreach (DataGridViewColumn column in Columns)
                {
                    cList.Add(column);
                }
                foreach (DataGridViewColumn column in cList)
                {
                    string FIELD = column.Name.Trim().ToUpper();
                    DataGridViewColumn newTypeColumn = null;

                    var editFieldDic = ObjectAndString.StringToStringDictionary(editFieldsFormat);
                    if (editFieldDic.ContainsKey(column.Name.ToUpper()))
                    {
                        string EorR_FORMAT = editFieldDic[FIELD];
                        var ss = EorR_FORMAT.Split(':');
                        if (ss[0].ToUpper() != "E")
                        {
                            column.ReadOnly = true;
                            continue;
                        }
                        if (!EorR_FORMAT.Contains(":")) goto Default;
                        
                        string NM_IP = ss[1].ToUpper(); // N2 hoac NM_IP_SL
                        if (NM_IP.StartsWith("N"))
                        {
                            string newFormat = NM_IP.Length == 2 ? NM_IP : V6Options.GetValueNull(NM_IP.Substring(1));
                            newTypeColumn = ChangeColumnType(FIELD, typeof(V6NumberDataGridViewColumn), newFormat);
                            newTypeColumn.ReadOnly = false;
                        }
                        else if (NM_IP.StartsWith("C")) // CVvar
                        {
                            newTypeColumn = ChangeColumnType(FIELD, typeof(V6VvarDataGridViewColumn), null);
                            ((V6VvarDataGridViewColumn)newTypeColumn).Vvar = NM_IP.Substring(1);
                            newTypeColumn.ReadOnly = false;
                        }
                        else if (NM_IP.StartsWith("D0")) // ColorDateTime
                        {
                            newTypeColumn = ChangeColumnType(FIELD, typeof(V6DateTimeColorGridViewColumn), null);
                            newTypeColumn.ReadOnly = false;
                        }
                        else if (NM_IP.StartsWith("D1")) // DateTimePicker
                        {
                            newTypeColumn = ChangeColumnType(FIELD, typeof(V6DateTimePickerGridViewColumn), null);
                            newTypeColumn.ReadOnly = false;
                        }

                        continue;
                    }

                    goto Last;

                Default:
                    {
                        if (ObjectAndString.IsNumberType(column.ValueType))
                        {
                            newTypeColumn = ChangeColumnType(column.Name, typeof(V6NumberDataGridViewColumn), null);
                            newTypeColumn.ReadOnly = false;
                        }
                        //else if (NM_IP.StartsWith("C")) // CVvar
                        //{
                        //    column1 = dataGridView1.ChangeColumnType(ss[0], typeof(V6VvarDataGridViewColumn), null);
                        //    ((V6VvarDataGridViewColumn)column).Vvar = NM_IP.Substring(1);
                        //}
                        else if (ObjectAndString.IsDateTimeType(column.ValueType)) // ColorDateTime
                        {
                            newTypeColumn = ChangeColumnType(column.Name, typeof(V6DateTimeColorGridViewColumn), null);
                            newTypeColumn.ReadOnly = false;
                        }
                        //else if (NM_IP.StartsWith("D1")) // DateTimePicker
                        //{
                        //    column1 = dataGridView1.ChangeColumnType(ss[0], typeof(V6DateTimePickerGridViewColumn), null);
                        //}
                    }
                Last:
                    if (newTypeColumn != null)
                    {
                        newTypeColumn.Width = column.Width;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_DataSourceChanged", ex);
            }
        }

        /// <summary>
        /// Dữ liệu phục hồi cho field
        /// </summary>
        private string COLUMN_NAME_VALID = null;
        public void SetValid(string field, string field_valid, string congThuc)
        {
            COLUMN_NAME_VALID = field_valid;
            CongThuc_Valid[field.ToUpper()] = congThuc;
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

        /// <summary>
        /// ReadOnly = true
        /// </summary>
        /// <param name="name"></param>
        public void LockColumn(string name)
        {
            if (Columns.Contains(name)) Columns[name].ReadOnly = true;
        }
        public void LockColumn(string name, bool read_only)
        {
            if (Columns.Contains(name)) Columns[name].ReadOnly = read_only;
        }
        /// <summary>
        /// ReadOnly = false
        /// </summary>
        /// <param name="name"></param>
        public void UnLockColumn(string name)
        {
            if (Columns.Contains(name)) Columns[name].ReadOnly = false;
        }


        public void EnableSelect()
        {
            Space_Bar = true;
            Control_A = true;
        }

        /// <summary>
        /// Hiển thị form chỉnh sửa dữ liệu dòng đang đứng.
        /// </summary>
        /// <param name="columnsInfo"></param>
        public void ShowRowEditor(string[] columnsInfo)
        {
            if (CurrentRow == null) return;
            try
            {
                GridViewRowEditorForm form = new GridViewRowEditorForm(this, columnsInfo);
                form.ShowDialog(this);
                if (form.DialogResult == DialogResult.OK)
                {
                    var data = form.GetData();
                    V6ControlFormHelper.UpdateGridViewRow(CurrentRow, data);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoCodeEditor", ex);
            }
        }

        private void V6ColorDataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_flyingFilterActive)
            {
                GetInfoFlyingTextBox();
                RelocationFlyingFilter();
            }
        }

        protected int _saveRowIndex = -1;
        protected int _saveCellIndex = -1;
        
        public void SaveSelectedCellLocation()
        {
            try
            {
                if (CurrentCell != null)
                {
                    _saveRowIndex = CurrentCell.RowIndex;
                    _saveCellIndex = CurrentCell.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SaveSelectedCellLocation " + ex.Message);
            }
        }

        public void LoadSelectedCellLocation()
        {
            V6ControlFormHelper.SetGridviewCurrentCellByIndex(this, _saveRowIndex, _saveCellIndex, Parent);
        }

        public event HandleData DataRowUpdated;
        public void OnDataRowUpdated(IDictionary<string, object> data)
        {
            if (DataRowUpdated != null)
            {
                DataRowUpdated(data);
            }
        }
    }

    public class SelectRowEventArgs : DataGridViewRowEventArgs
    {
        public SelectRowEventArgs(DataGridViewRow dataGridViewRow) : base(dataGridViewRow){}
        public bool CancelSelect { get; set; }
        public bool Select { get; set; }
    }
}
