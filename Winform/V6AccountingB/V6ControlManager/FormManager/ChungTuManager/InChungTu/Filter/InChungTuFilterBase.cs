using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

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
        public IDictionary<string, object> RptExtraParameters { get; set; } 

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

        protected DataSet _ds = null;
        /// <summary>
        /// Nhận dữ liệu khi tải dữ liệu xong.
        /// </summary>
        /// <param name="ds"></param>
        public virtual void LoadDataFinish(DataSet ds)
        {
            _ds = ds;
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

        /// <summary>
        /// Lấy tham số cho rpt theo định nghĩa Extra_para
        /// </summary>
        /// <param name="ExtraParameterInfo"></param>
        /// <param name="LAN"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetRptParametersD(string ExtraParameterInfo, string LAN)
        {
            try
            {
                var result = new SortedDictionary<string, object>();
                var lineList = GetFilterLineList();
                try
                {
                    if (string.IsNullOrEmpty(ExtraParameterInfo)) return null;
                    var sss = ExtraParameterInfo.Split('~');

                    foreach (string ss in sss)
                    {
                        DefineInfo di = new DefineInfo(ss);
                        if (string.IsNullOrEmpty(di.Name)) continue;

                        if (di.Ptype.ToUpper() == "TABLE2")
                        {
                            var dataTable2 = _ds.Tables[1];
                            var _tbl2Row = dataTable2.Rows[0];
                            if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBANNT")
                            {
                                var t_tien_nt2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);// "T_TIEN_NT2_IN"]);
                                var ma_nt = _tbl2Row["MA_NT"].ToString().Trim();
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien_nt2_in, LAN, ma_nt);
                            }
                            else if (di.Name.ToUpper() == "SOTIENVIETBANGCHU_TIENBAN")
                            {
                                var t_tien2_in = ObjectAndString.ObjectToDecimal(_tbl2Row[di.Field]);//"T_TIEN2_IN"]);
                                result[di.Name] = V6BusinessHelper.MoneyToWords(t_tien2_in, LAN,
                                    V6Options.M_MA_NT0);
                            }
                            else
                            {
                                result[di.Name.ToUpper()] = _tbl2Row[di.Field];
                            }
                        }
                        else if (di.Ptype.ToUpper() == "PARENT")
                        {

                        }
                        else if (di.Ptype.ToUpper() == "FILTER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line.IsSelected)
                                {
                                    //Bỏ qua giá trị rỗng.
                                    if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                    result[di.Name] = line.ObjectValue;
                                }
                                else
                                {
                                    if (di.NotEmpty) continue;

                                    if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                        result[di.Name] = 0;
                                    else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                        result[di.Name] = new DateTime(1900, 1, 1);
                                    else
                                        result[di.Name] = "";
                                }
                            }
                        }
                        else if (di.Ptype.ToUpper() == "FILTER_BROTHER")
                        {
                            var lineKey = "line" + di.Field.ToUpper();
                            if (lineList.ContainsKey(lineKey))
                            {
                                var line = lineList[lineKey];
                                if (line is FilterLineVvarTextBox)
                                {
                                    var lineV = line as FilterLineVvarTextBox;

                                    var vvar_data = lineV.VvarTextBox.Data;
                                    if (line.IsSelected == false)
                                    {
                                        vvar_data = null;
                                    }

                                    if (vvar_data != null && vvar_data.Table.Columns.Contains(di.Fname))
                                    {
                                        if (line.IsSelected)
                                        {
                                            //Bỏ qua giá trị rỗng.
                                            if (di.NotEmpty && string.IsNullOrEmpty("" + line.ObjectValue)) continue;
                                            result[di.Name] = vvar_data[di.Fname];
                                        }
                                        else
                                        {
                                            if (di.NotEmpty) continue;

                                            if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                                result[di.Name] = 0;
                                            else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                                result[di.Name] = new DateTime(1900, 1, 1);
                                            else
                                                result[di.Name] = "";
                                        }
                                    }
                                    else
                                    {
                                        if (di.NotEmpty) continue;
                                        // Tuanmh Null loi
                                        if (ObjectAndString.IsNumberType(line.ObjectValue.GetType()))
                                            result[di.Name] = 0;
                                        else if (ObjectAndString.IsDateTimeType(line.ObjectValue.GetType()))
                                            result[di.Name] = new DateTime(1900, 1, 1);
                                        else
                                            result[di.Name] = "";
                                    }
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".GetRprParameterD", ex);
                }
                return result;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetRptParametersD", ex);
                return null;
            }
        }

        /// <summary>
        /// Trả về từ điển dạng &lt;"lineField",lineControl&gt;
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, FilterLineBase> GetFilterLineList()
        {
            return GetFilterLineListRecursive(this);
        }
        public SortedDictionary<string, FilterLineBase> GetFilterLineListRecursive(Control control)
        {
            SortedDictionary<string, FilterLineBase> result = new SortedDictionary<string, FilterLineBase>();
            foreach (Control control1 in control.Controls)
            {
                var line = control1 as FilterLineBase;
                if (line != null)
                {
                    result.Add("line" + line.FieldName.ToUpper(), line);
                }
                else
                {
                    result.AddRange(GetFilterLineListRecursive(control1));
                }
            }
            return result;
        }

        public virtual void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            
        }
    }
}
