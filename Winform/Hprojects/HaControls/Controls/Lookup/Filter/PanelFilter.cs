using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace H_Controls.Controls.Lookup.Filter
{
    public class PanelFilter : Panel
    {
        private int _maxIndex = -1;
        private int _marginLeft = 10, _marginTop = 3;
        [Category("H")]
        [Description("Khoảng cách từ cạnh trái đến các filterLine tính bằng pixel.")]
        [DefaultValue(10)]
        public int LeftMargin
        {
            get { return _marginLeft; }
            set
            {
                _marginLeft = value;
                ResetLeft();
            }//viết sự kiện
        }
        [Category("H")]
        [Description("Khoảng cách từ cạnh trên đến filterLine đầu tiên tính bằng pixel.")]
        [DefaultValue(3)]
        public int TopMargin
        {
            get { return _marginTop; }
            set
            {
                var a = value - _marginTop;
                _marginTop = value; 
                ResetTop(a);
            }//viết sự kiện
        }

        private void ResetLeft()
        {
            foreach (FilterLineBase control in Controls)
            {
                control.Left = _marginLeft;
            }
        }

        private void ResetTop(int a)
        {
            foreach (FilterLineBase control in Controls)
            {
                control.Top += a;
            }
        }

        public void AddFilterLineControl(Type dataType, string fieldName)
        {
            try
            {
                var NAME = fieldName.Trim().ToUpper();
                FilterLineDynamic lineControl = new FilterLineDynamic();
                lineControl.FieldName = NAME;
                lineControl.FieldCaption = NAME;// CorpLan2.GetFieldHeader(NAME);
                //if (structTable.ContainsKey(NAME))
                {
                    if (dataType == typeof(string))
                    {
                        lineControl.AddTextBox();
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        lineControl.AddDateTimePick();
                    }
                    else
                    {
                        lineControl.AddNumberTextBox();
                    }
                }
                _maxIndex++;
                lineControl.Location = new Point(_marginLeft, _marginTop + 30 * _maxIndex);
                Controls.Add(lineControl);
            }
            catch (Exception ex)
            {
                HControlHelper.ShowErrorMessage(fieldName + ": " + ex.Message);
            }
        }
        
        public void AddMultiFilterLine(DataGridView structTable, string[] fields)
        {
            foreach (string field in fields)
            {
                string err = "";
                try
                {
                    var column = structTable.Columns[field];
                    if(!string.IsNullOrEmpty(field) && column != null)
                        AddFilterLineControl(column.ValueType, field);
                }
                catch (Exception ex)
                {
                    err += "\n" + ex.Message;
                }
                if (err.Length > 0)
                {
                    HControlHelper.ShowErrorMessage("AddMultiFilterLine error!" + err);
                }
            }
        }
        /// <summary>
        /// Kiểu And
        /// </summary>
        public string QueryString
        {
            get
            {
                string result = "";
                foreach (FilterLineDynamic c in Controls)
                {
                    if (c.IsSelected)
                        result += " And " + c.Query;
                }
                if (result.Length > 4) result = result.Substring(4);
                return result;
            }
        }

    }
}
