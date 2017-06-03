using System;
using System.Drawing;
using System.Windows.Forms;

namespace H_Controls.Controls
{
    public class ColorGridView:DataGridView
    {
        public ColorGridView()
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
            // ColorDataGridView
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
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ColorDataGridView_ColumnAdded);
            this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.ColorDataGridView_RowPostPaint);
            this.SelectionChanged += new System.EventHandler(this.ColorDataGridView_SelectionChanged);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void ColorDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
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
                    || dataType == typeof (sbyte)
                    || dataType == typeof (Single))
                {
                    e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (dataType == typeof (string))
                {
                    
                }

            }
        }

        private void ColorDataGridView_SelectionChanged(object sender, EventArgs e)
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

        private void ColorDataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(RowHeadersDefaultCellStyle.ForeColor))
            {
                //e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
                if (Rows[e.RowIndex].IsSelect())
                {
                    e.Graphics.DrawString("x", e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
                }
            }

        }
    }
}
