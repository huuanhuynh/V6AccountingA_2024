using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls.GridView
{
    /// <summary>
    /// made by: huuan_huynh v6soft 20181115
    /// </summary>

    #region V6LookupGridViewEditingControl

    class V6LookupGridViewEditingControl : V6LookupTextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;  // grid owning this editing control
        private bool valueChanged;  // editing control's value has changed or not
        private int rowIndex;  //  row index in which the editing control resides
        
        public V6LookupGridViewEditingControl()
        {
            this.TabStop = false;  // control must not be part of the tabbing loop
            this.VisibleChanged += V6LookupGridViewEditingControl_VisibleChanged;
        }
        /// <summary>
        /// Sửa lại giá trị của EditingControl cho đúng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void V6LookupGridViewEditingControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                var cell = dataGridView.CurrentCell;
                if (cell != null) Text = ObjectAndString.ObjectToString(cell.Value).Trim();
                //if (cell.OwningColumn.DefaultCellStyle.Format != null && cell.OwningColumn.DefaultCellStyle.Format.StartsWith("N"))
                //    this.DecimalPlaces = ObjectAndString.ObjectToInt(cell.OwningColumn.DefaultCellStyle.Format.Substring(1));
            }
            else
            {
                // Gây sự kiện lên V6ColorDataGridView để Refresh form nếu cần thiết
                if (dataGridView is V6ColorDataGridView)
                    ((V6ColorDataGridView)dataGridView).OnV6Changed();
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
            return this.Text;
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
                //var cell = dataGridView.CurrentCell;
                //this.Text = ObjectAndString.ObjectToString(cell.Value);
                //if (cell.OwningColumn.DefaultCellStyle.Format != null && cell.OwningColumn.DefaultCellStyle.Format.StartsWith("N"))
                //    this.DecimalPlaces = ObjectAndString.ObjectToInt(cell.OwningColumn.DefaultCellStyle.Format.Substring(1));
                this.SelectAll();
            }
            else
            {
                this.SelectionStart = this.Text.Length;
            }
        }

        // End of the IDataGridViewEditingControl interface implementation

        /// <summary>
        /// Small utility function that updates the local dirty state and 
        /// notifies the grid of the value change.
        /// </summary>
        private void NotifyDataGridViewOfValueChange()
        {
            if (!this.valueChanged)
            {
                this.valueChanged = true;
                this.dataGridView.NotifyCurrentCellDirty(true);
            }
        }

        ///// <summary>
        ///// Listen to the KeyPress notification to know when the value changed, and 
        ///// notify the grid of the change.
        ///// </summary>
        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //}

        /// <summary>
        /// Listen to the ValueChanged notification to forward the change to the grid.
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            dataGridView.CurrentCell.Value = Text;
            //if (this.Focused)
            {
                // Let the DataGridView know about the value change
                NotifyDataGridViewOfValueChange();
            }
        }
    }

    #endregion

    #region V6LookupGridViewColumn
    public class V6LookupGridViewColumn : DataGridViewTextBoxColumn//DataGridViewColumn//
    {
        public V6LookupGridViewColumn()
        {
            V6LookupGridViewCell cell = new V6LookupGridViewCell();
            base.CellTemplate = cell;

            base.SortMode = DataGridViewColumnSortMode.Automatic;
            //base.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //base.DefaultCellStyle.Format = "F" + this.DecimalLength.ToString();
        }

        private V6LookupGridViewCell LookupCellTemplate
        {
            get
            {
                V6LookupGridViewCell cell = this.CellTemplate as V6LookupGridViewCell;
                if (cell == null)
                {
                    throw new InvalidOperationException("V6LookupGridViewColumn does not have a CellTemplate.");
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
                V6LookupGridViewCell cell = value as V6LookupGridViewCell;
                if (value != null && cell == null)
                {
                    throw new InvalidCastException("Value provided for CellTemplate must be of type V6LookupGridViewCell or derive from it.");
                }
                base.CellTemplate = value;
            }
        }

        public string MA_DM
        {
            // Thuộc tính kiểu mới lưu trong Tag.
            get
            {
                IDictionary<string,object> tagDic = ObjectAndString.ObjectToDictionary(Tag);
                if (tagDic.ContainsKey("MA_DM")) return tagDic["MA_DM"].ToString();
                return null;
            }
            set
            {
                IDictionary<string, object> tagDic = ObjectAndString.ObjectToDictionary(Tag);
                tagDic["MA_DM"] = value;
                Tag = tagDic;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
            sb.Append("V6LookupGridViewColumn { Name=");
            sb.Append(this.Name);
            sb.Append(", Index=");
            sb.Append(this.Index.ToString(CultureInfo.CurrentCulture));
            sb.Append(" }");
            return sb.ToString();
        }
    }

    #endregion

    #region V6LookupGridViewCell

    public class V6LookupGridViewCell : DataGridViewTextBoxCell
    {
        private static Type defaultEditType = typeof(V6LookupGridViewEditingControl);
        private static Type defaultValueType = typeof(System.String);

        public V6LookupGridViewCell(){}
        
        private V6LookupGridViewEditingControl EditingControl
        {
            get
            {
                return this.DataGridView.EditingControl as V6LookupGridViewEditingControl;
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
        ///// Làm tròn khi gán giá trị vào cell.
        ///// If set 0/1.23 to two cells, it will throw Exception when sort by clicking column header.
        ///// Override this method to ensure the type of value.
        ///// </summary>
        //protected override bool SetValue(int rowIndex, object value)
        //{
        //    //Value = ("" + value).Trim();
        //    //return true;
        //    //decimal val = 0.0m;
        //    //try
        //    //{
        //    //    if (EditingControl != null)
        //    //        val = Math.Round(Convert.ToDecimal(value), EditingControl.DecimalPlaces);
        //    //    else
        //    //        val = ObjectAndString.ObjectToDecimal(value);
        //    //}
        //    //catch { }
        //    return base.SetValue(rowIndex, value.ToString().Trim());  // if set 0 and 1.23, it will throw exception when sort
        //}

        //public override object Clone()
        //{
        //    V6LookupGridViewCell dataGridViewCell = base.Clone() as V6LookupGridViewCell;
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
            V6LookupTextBox txt = this.DataGridView.EditingControl as V6LookupTextBox;
            if (txt != null)
            {
                txt.Ma_dm = ((V6LookupGridViewColumn)DataGridView.CurrentCell.OwningColumn).MA_DM;
                txt.CharacterCasing = CharacterCasing.Upper;
                txt.BorderStyle = BorderStyle.None;
                //if (dataGridViewCellStyle.Format != null && dataGridViewCellStyle.Format.StartsWith("N"))
                //    numEditBox.DecimalPlaces = ObjectAndString.ObjectToInt(dataGridViewCellStyle.Format.Substring(1));
                //numEditBox.DecimalLength = this.DecimalLength;
                //numEditBox.AllowNegative = this.AllowNegative;

                //string initialFormattedValueStr = initialFormattedValue as string;

                //if (string.IsNullOrEmpty(initialFormattedValueStr))
                //{
                //    txt.Text = "0";
                //}
                //else
                //{
                //    txt.Text = ("" + initialFormattedValueStr).Trim();
                //}
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

            V6LookupTextBox numEditBox = dataGridView.EditingControl as V6LookupTextBox;
            if (numEditBox != null)
            {
                numEditBox.ClearUndo();  // avoid interferences between the editing sessions
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
            V6LookupGridViewEditingControl editingControl = this.DataGridView.EditingControl as V6LookupGridViewEditingControl;
            return editingControl != null && rowIndex == ((IDataGridViewEditingControl)editingControl).EditingControlRowIndex;
        }

        public override string ToString()
        {
            return "V6LookupGridViewCell { ColIndex=" + ColumnIndex.ToString(CultureInfo.CurrentCulture) + 
                ", RowIndex=" + RowIndex.ToString(CultureInfo.CurrentCulture) + " }";
        }
    }

    #endregion
}
