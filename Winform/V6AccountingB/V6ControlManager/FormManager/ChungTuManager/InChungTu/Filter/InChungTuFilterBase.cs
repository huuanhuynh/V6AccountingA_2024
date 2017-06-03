using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6ReportControls;

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
 
{
    public partial class InChungTuFilterBase : V6FormControl
    {
        public InChungTuFilterBase()
        {
            InitializeComponent();
        }

        public bool F3 { get; set; }
        public bool F5 { get; set; }

        public SortedDictionary<string, string> _hideFields; 

        public List<SqlParameter> InitFilters = new List<SqlParameter>();
        private DataGridViewRow _parentRow;
        /// <summary>
        /// Tham số thêm cho file rpt
        /// </summary>
        public SortedDictionary<string, object> RptExtraParameters { get; set; } 

        /// <summary>
        /// Lấy danh sach tham so
        /// </summary>
        /// <returns></returns>
        public virtual List<SqlParameter> GetFilterParameters()
        {
            var result = "";
            foreach (Control control in Controls)
            {
                var lineControl = control as FilterLineBase;
                if (lineControl != null && lineControl.IsSelected)
                {
                    result += "\nand " + lineControl.Query;
                }
            }
            var lresult = new List<SqlParameter>();
            
            lresult.Add(new SqlParameter("@cKey", result.Length > 4 ? result.Substring(4) : ""));
            return lresult;
        }

        public virtual string GetFilterStringByFields(List<string> fields, bool and)
        {
            var result = GetFilterStringByFieldsR(this, fields, and);
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        private string GetFilterStringByFieldsR(Control control, List<string> fields, bool and)
        {
            var result = "";
            var line = control as FilterLineBase;
            if (line != null && fields.Contains(line.FieldName.ToUpper()))
            {
                result += line.IsSelected ? ((and ? "\nand " : "\nor  ") + line.Query) : "";
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result += GetFilterStringByFieldsR(c, fields, and);
                }
            }
            return result;
        }

        protected delegate void SetParentRowHandle(DataGridViewRow row);
        public delegate void SetFieldValueHandle(string sttrec);

        protected event SetParentRowHandle SetParentRowEvent;
        public event SetFieldValueHandle SetFieldValueEvent;
        public void SetParentRow(DataGridViewRow currentRow)
        {
            _parentRow = currentRow;
            OnSetParentRow(_parentRow);
        }

        public void SetFieldValue(string sttrec)
        {
            OnSetFieldValueEvent(sttrec);
        }

        protected virtual void OnSetParentRow(DataGridViewRow currentRow)
        {
            var handler = SetParentRowEvent;
            if (handler != null) handler(currentRow);
        }

        protected virtual void OnSetFieldValueEvent(string sttrec)
        {
            var handler = SetFieldValueEvent;
            if (handler != null) handler(sttrec);
        }

        //Các biến xài tùy ý.
        public event StringValueChanged String1ValueChanged;
        public event CheckValueChanged Check1ValueChanged;
        protected virtual void OnString1ValueChanged(string oldvalue, string newvalue)
        {
            var handler = String1ValueChanged;
            if (handler != null) handler(oldvalue, newvalue);
        }
        protected virtual void OnCheck1ValueChanged(bool oldvalue, bool newvalue)
        {
            var handler = Check1ValueChanged;
            if (handler != null) handler(oldvalue, newvalue);
        }
        private string _string1;
        public string String1
        {
            get
            {
                return _string1;
            }
            set
            {
                var old = _string1;
                _string1 = value;
                OnString1ValueChanged(old, value);
            }
        }
        public string String2, String3;
        /// <summary>
        /// Dùng procedure này để lấy dữ liệu. Code trong phần gọi proc của formReportBase.
        /// </summary>
        public string ProcedureName;
        public decimal Number1, Number2, Number3;
        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date1 { get; set; }
        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date2 { get; set; }
        /// <summary>
        /// Gán giá trị ở Filter GetFilterParameters
        /// </summary>
        public DateTime Date3 { get; set; }
        public bool Check2, Check3;

        private bool _check1 = false;
        [DefaultValue(false)]
        public bool Check1
        {
            get
            {
                return _check1;
            }
            set
            {
                var old = _check1;
                _check1 = value;
                OnCheck1ValueChanged(old, value);
            }
        }

        public virtual void Call1(object s = null)
        {

        }
        public virtual void Call2(object s = null)
        {

        }
        public virtual void Call3(object s = null)
        {

        }
    }
}
