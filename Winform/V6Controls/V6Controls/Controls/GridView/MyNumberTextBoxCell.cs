using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls.GridView
{
    public class MyNumberTextBoxCell : DataGridViewTextBoxCell
    {
        //public MyTextBoxCell()
        //{
            
        //}

        //public override void PositionEditingControl(bool setLocation, bool setSize,
        //    Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle,
        //    bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded,
        //    bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        //{
        //    cellClip.Height = cellClip.Height * 8; // ← Or any other suitable height
        //    cellBounds.Height = cellBounds.Height * 8;
        //    var r = base.PositionEditingPanel(cellBounds, cellClip, cellStyle,
        //        singleVerticalBorderAdded, singleHorizontalBorderAdded,
        //        isFirstDisplayedColumn, isFirstDisplayedRow);
        //    this.DataGridView.EditingControl.Location = r.Location;
        //    this.DataGridView.EditingControl.Size = r.Size;
        //}
        public override void InitializeEditingControl(int rowIndex,
            object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            ((TextBox)this.DataGridView.EditingControl).Multiline = true;
            ((TextBox)this.DataGridView.EditingControl).BorderStyle = BorderStyle.Fixed3D;
        }

        protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyDown(e, rowIndex);
        }

        protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
        {
            base.OnKeyUp(e, rowIndex);
        }

        public string Text
        {
            get { return ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(Value), 2, ".", " "); }
        }

        protected override void OnKeyPress(KeyPressEventArgs e, int rowIndex)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == '-' && Text.Length > 0)
            {
                e.Handled = true;
            }
            base.OnKeyPress(e, rowIndex);
        }
    }
}
