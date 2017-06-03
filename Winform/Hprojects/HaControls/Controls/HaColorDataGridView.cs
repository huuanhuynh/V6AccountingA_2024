using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using H_DatabaseAccess;

namespace H_Controls.Controls
{
    public class HaColorDataGridView:DataGridView
    {
        public HaColorDataGridView()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            SuspendLayout();
            // 
            // HaColorDataGridView
            // 
            dataGridViewCellStyle1.BackColor = Color.LightCyan;
            AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.BackColor = Color.LightYellow;
            RowsDefaultCellStyle = dataGridViewCellStyle3;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ColumnAdded += GridView_ColumnAdded;
            RowPostPaint += GridView_RowPostPaint;
            SelectionChanged += GridView_SelectionChanged;
            ((System.ComponentModel.ISupportInitialize)this).EndInit();
            ResumeLayout(false);

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

        private void GridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            
            if (e.Column.ValueType == typeof (DateTime))
            {
                e.Column.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            else
            {
                var dataType = e.Column.ValueType;
                var field = e.Column.DataPropertyName.ToUpper();

                if (dataType == typeof (int)
                    || dataType == typeof(decimal)
                    || dataType == typeof (double)
                    || dataType == typeof (long)
                    || dataType == typeof (short)
                    || dataType == typeof (float)
                    || dataType == typeof (Int16)
                    || dataType == typeof (Int32)
                    || dataType == typeof (Int64)
                    || dataType == typeof (uint)
                    || dataType == typeof (UInt16)
                    || dataType == typeof (UInt32)
                    || dataType == typeof (UInt64)
                    || dataType == typeof (byte)
                    || dataType == typeof (sbyte)
                    || dataType == typeof (Single))
                {
                    e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //e.Column.DefaultCellStyle.Format = "N2";
                    
                    var a = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                    a.NumberGroupSeparator = " ";
                    a.NumberDecimalSeparator = ",";
                    e.Column.DefaultCellStyle.FormatProvider = a;
                }
                else if (dataType == typeof (string))
                {
                    
                }

            }
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
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

        private void GridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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


        private bool _already_set_max_length;
        private TableStruct _tableStruct;
        private string _type;

        /// <summary>
        /// Gán maxlength cho textbox lúc edit trên gridview. Và type khác.
        /// </summary>
        /// <param name="tableStruct"></param>
        /// <param name="type">hỗ trợ: UPPER</param>
        public void FormatEditTextBox(TableStruct tableStruct, string type)
        {
            if (_already_set_max_length) return;
            _already_set_max_length = true;
            try
            {
                _tableStruct = tableStruct;
                _type = type;
                EditingControlShowing += GridView_EditingControlShowing;
            }
            catch (Exception ex)
            {
                //Logger.WriteToLog("ColorGridView FormatEditTextBox: " + ex.Message);
            }
        }

        /// <summary>
        /// Thuộc tính DataSource cải tiến dùng với kiểu DataTable
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
                    DataSource = value;
                }
            }
            
        }

        void GridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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

        
    }
}
