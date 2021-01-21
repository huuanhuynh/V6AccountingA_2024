using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls.GridView
{

    #region V6DateTimePickerFullGridViewColumn
    public class V6DateTimePickerFullGridViewColumn : DataGridViewTextBoxColumn//DataGridViewColumn//
    {
        public V6DateTimePickerFullGridViewColumn()
        {
            V6DateTimePickerFullDataGridViewCell cell = new V6DateTimePickerFullDataGridViewCell();
            base.CellTemplate = cell;

            base.SortMode = DataGridViewColumnSortMode.Automatic;
            base.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //base.DefaultCellStyle.Format = "F" + this.DecimalLength.ToString();
        }

        private V6DateTimePickerFullDataGridViewCell NumEditCellTemplate
        {
            get
            {
                V6DateTimePickerFullDataGridViewCell cell = this.CellTemplate as V6DateTimePickerFullDataGridViewCell;
                if (cell == null)
                {
                    throw new InvalidOperationException("V6DateTimePickerFullGridViewEditingControl does not have a CellTemplate.");
                }
                return cell;
            }
        }

        [
            Browsable(false),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                V6DateTimePickerFullDataGridViewCell cell = value as V6DateTimePickerFullDataGridViewCell;
                if (value != null && cell == null)
                {
                    throw new InvalidCastException("Value provided for CellTemplate must be of type V6DateTimePickerFullDataGridViewCell or derive from it.");
                }
                base.CellTemplate = value;
            }
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
            sb.Append("V6DateTimePickerFullGridViewColumn { Name=");
            sb.Append(this.Name);
            sb.Append(", Index=");
            sb.Append(this.Index.ToString(CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }
    }

    #endregion


    #region V6DateTimePickerFullGridViewEditingControl

    class V6DateTimePickerFullGridViewEditingControl : V6DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;  // grid owning this editing control
        private bool valueChanged;  // editing control's value has changed or not
        private int rowIndex;  //  row index in which the editing control resides

        public V6DateTimePickerFullGridViewEditingControl()
        {
            this.TabStop = false;  // control must not be part of the tabbing loop
            this.VisibleChanged += V6DateTimePickerFullGridViewEditingControl_VisibleChanged;
        }
        /// <summary>
        /// Sửa lại giá trị của EditingControl cho đúng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V6DateTimePickerFullGridViewEditingControl_VisibleChanged(object sender, EventArgs e)
        {
            var cell = dataGridView.CurrentCell;
            if (Visible)
            {
                Value = ObjectAndString.ObjectToFullDateTime(cell.Value);
                //if (cell.OwningColumn.DefaultCellStyle.Format != null && cell.OwningColumn.DefaultCellStyle.Format.StartsWith("N"))
                //    this.DecimalPlaces = ObjectAndString.ObjectToInt(cell.OwningColumn.DefaultCellStyle.Format.Substring(1));
            }
            else
            {
                cell.Value = Value;
            }
        }

        public virtual DataGridView EditingControlDataGridView
        {
            get { return this.dataGridView; }
            set { this.dataGridView = value; }
        }

        public virtual object EditingControlFormattedValue
        {
            get
            {
                return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            }
            set
            {
                this.Text = (string)value;
            }
        }

        public virtual int EditingControlRowIndex
        {
            get { return this.rowIndex; }
            set { this.rowIndex = value; }
        }

        public virtual bool EditingControlValueChanged
        {
            get { return this.valueChanged; }
            set { this.valueChanged = value; }
        }

        /// <summary>
        /// Property which determines which cursor must be used for the editing panel,
        /// i.e. the parent of the editing control.
        /// </summary>
        public virtual Cursor EditingPanelCursor
        {
            get { return Cursors.Default; }
        }

        /// <summary>
        /// Property which indicates whether the editing control needs to be repositioned 
        /// when its value changes.
        /// </summary>
        public virtual bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        /// <summary>
        /// Method called by the grid before the editing control is shown so it can adapt to the 
        /// provided cell style.
        /// </summary>
        public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            //this.Write();
            if (dataGridViewCellStyle.BackColor.A < 255)
            {
                // The NumericUpDown control does not support transparent back colors
                Color opaqueBackColor = Color.FromArgb(255, dataGridViewCellStyle.BackColor);
                this.BackColor = opaqueBackColor;
                this.dataGridView.EditingPanel.BackColor = opaqueBackColor;
            }
            else
            {
                this.BackColor = dataGridViewCellStyle.BackColor;
            }
            this.ForeColor = dataGridViewCellStyle.ForeColor;
        }

        /// <summary>
        /// Method called by the grid on keystrokes to determine if the editing control is
        /// interested in the key or not.
        /// </summary>
        public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Down:
                case Keys.Up:
                case Keys.Home:
                case Keys.End:
                case Keys.Delete:
                    return true;
            }
            return !dataGridViewWantsInputKey;
        }

        /// <summary>
        /// Returns the current value of the editing control.
        /// </summary>
        public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return ObjectAndString.ObjectToString(this.Value);
        }

        /// <summary>
        /// Called by the grid to give the editing control a chance to prepare itself for
        /// the editing session.
        /// </summary>
        public virtual void PrepareEditingControlForEdit(bool selectAll)
        {
            if (dataGridView is V6ColorDataGridView)
                ((V6ColorDataGridView)dataGridView).OnEditingPrepare(new V6ColorDataGridView.EditingEventArgs()
                {
                    CurrentCell = dataGridView.CurrentCell,
                    CurrentColumn = dataGridView.CurrentCell.OwningColumn,
                    CurrentRow = dataGridView.CurrentRow,
                    DataGridView = dataGridView,
                    Control = this
                });

            if (selectAll)
            {
                var cell = dataGridView.CurrentCell;
                //this.Value = ObjectAndString.ObjectToFullDateTime(cell.Value);
                if (string.IsNullOrEmpty(cell.OwningColumn.DefaultCellStyle.Format))
                    this.CustomFormat = "dd/MM/yyyy";
                else this.CustomFormat = cell.OwningColumn.DefaultCellStyle.Format;
                //this.SelectAll();
            }
            else
            {
                //this.SelectionStart = 0;
            }
        }

        // End of the IDataGridViewEditingControl interface implementation

        /// <summary>
        /// Small utility function that updates the local dirty state and 
        /// notifies the grid of the value change.
        /// </summary>
        public void NotifyDataGridViewOfValueChange()
        {
            if (!this.valueChanged)
            {
                this.valueChanged = true;
                this.dataGridView.NotifyCurrentCellDirty(true);
            }
        }

        /// <summary>
        /// Listen to the KeyPress notification to know when the value changed, and 
        /// notify the grid of the change.
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            
            // The value changes when a digit, the decimal separator or the negative sign is pressed.
            bool notifyValueChange = false;

            if (char.IsDigit(e.KeyChar))
            {
                notifyValueChange = true;
            }
            else
            {
                System.Globalization.NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
                string decimalSeparatorStr = numberFormatInfo.NumberDecimalSeparator;
                string groupSeparatorStr = numberFormatInfo.NumberGroupSeparator;
                string negativeSignStr = numberFormatInfo.NegativeSign;
                if (!string.IsNullOrEmpty(decimalSeparatorStr) && decimalSeparatorStr.Length == 1)
                {
                    notifyValueChange = decimalSeparatorStr[0] == e.KeyChar;
                }
                if (!notifyValueChange && !string.IsNullOrEmpty(groupSeparatorStr) && groupSeparatorStr.Length == 1)
                {
                    notifyValueChange = groupSeparatorStr[0] == e.KeyChar;
                }
                if (!notifyValueChange && !string.IsNullOrEmpty(negativeSignStr) && negativeSignStr.Length == 1)
                {
                    notifyValueChange = negativeSignStr[0] == e.KeyChar;
                }
            }

            if (notifyValueChange)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }

        /// <summary>
        /// Listen to the ValueChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Focused)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);

            if (this.Focused)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }
    }

    #endregion


    #region V6DateTimePickerFullDataGridViewCell

    public class V6DateTimePickerFullDataGridViewCell : DataGridViewTextBoxCell
    {
        //private bool m_showNullWhenZero = false;
        //private bool m_allowNegative = true;
        //private int m_decimalLength = 0;

        private static Type defaultEditType = typeof(V6DateTimePickerFullGridViewEditingControl);
        private static Type defaultValueType = typeof(System.DateTime);

        public V6DateTimePickerFullDataGridViewCell(){}
        
        private V6DateTimePickerFullGridViewEditingControl EditingV6DateTimePickerTextBox
        {
            get
            {
                return this.DataGridView.EditingControl as V6DateTimePickerFullGridViewEditingControl;
            }
        }

        public override Type EditType
        {
            get { return defaultEditType; }
        }

        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;
                if (valueType != null)
                {
                    return valueType;
                }
                return defaultValueType;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //protected override bool SetValue(int rowIndex, object value)
        //{
        //   return base.SetValue(rowIndex, value);  // if set 0 and 1.23, it will throw exception when sort
        //}

        //public override object Clone()
        //{
        //    V6DateTimePickerFullDataGridViewCell dataGridViewCell = base.Clone() as V6DateTimePickerFullDataGridViewCell;
        //    //if (dataGridViewCell != null)
        //    //{
        //    //    //dataGridViewCell.DecimalLength = this.DecimalLength;
        //    //    //dataGridViewCell.AllowNegative = this.AllowNegative;
        //    //    //dataGridViewCell.ShowNullWhenZero = this.ShowNullWhenZero;
        //    //}
        //    return dataGridViewCell;
        //}

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            OnCommonChange();
            V6DateTimePickerFullGridViewEditingControl editBox = this.DataGridView.EditingControl as V6DateTimePickerFullGridViewEditingControl;
            if (editBox != null)
            {
                editBox.NotifyDataGridViewOfValueChange();
                //numEditBox.BorderStyle = BorderStyle.None;
                editBox.Format = DateTimePickerFormat.Custom;
                if (string.IsNullOrEmpty(dataGridViewCellStyle.Format))
                     editBox.CustomFormat = "dd/MM/yyyy";
                else editBox.CustomFormat = dataGridViewCellStyle.Format;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = this.DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
            {
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
            }

            V6DateTimePicker numEditBox = dataGridView.EditingControl as V6DateTimePicker;
            if (numEditBox != null)
            {
                //numEditBox.ClearUndo();  // avoid interferences between the editing sessions
            }

            base.DetachEditingControl();
        }

        ///// <summary>
        ///// Consider the decimal in the formatted representation of the cell value.
        ///// </summary>
        //protected override object GetFormattedValue(object value,
        //                                            int rowIndex,
        //                                            ref DataGridViewCellStyle cellStyle,
        //                                            TypeConverter valueTypeConverter,
        //                                            TypeConverter formattedValueTypeConverter,
        //                                            DataGridViewDataErrorContexts context)
        //{
        //    object baseFormattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        //    string formattedText = baseFormattedValue as string;

        //    if (value == null || string.IsNullOrEmpty(formattedText))
        //    {
        //        return baseFormattedValue;
        //    }

        //    Decimal unformattedDecimal = System.Convert.ToDecimal(value); // 123.1 to "123.1"
        //    Decimal formattedDecimal = System.Convert.ToDecimal(formattedText); // 123.1 to "123.12" if DecimalLength is 2

        //    if (unformattedDecimal == 0.0m && m_showNullWhenZero)
        //    {
        //        return base.GetFormattedValue(null, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        //    }

        //    if (unformattedDecimal == formattedDecimal)
        //    {
        //        return formattedDecimal.ToString("F" + m_decimalLength.ToString());
        //    }
        //    return formattedText;
        //}

        private void OnCommonChange()
        {
            if (this.DataGridView != null && !this.DataGridView.IsDisposed && !this.DataGridView.Disposing)
            {
                if (this.RowIndex == -1)
                {
                    this.DataGridView.InvalidateColumn(this.ColumnIndex);
                }
                else
                {
                    this.DataGridView.UpdateCellValue(this.ColumnIndex, this.RowIndex);
                }
            }
        }

        private bool OwnsEditingControl(int rowIndex)
        {
            if (rowIndex == -1 || this.DataGridView == null)
            {
                return false;
            }
            V6DateTimePickerFullGridViewEditingControl editingControl = this.DataGridView.EditingControl as V6DateTimePickerFullGridViewEditingControl;
            return editingControl != null && rowIndex == ((IDataGridViewEditingControl)editingControl).EditingControlRowIndex;
        }
        
        public override string ToString()
        {
            return "V6DateTimePickerFullDataGridViewCell { ColIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + 
                ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
    }

    #endregion
}
