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

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class FilterBase : V6Control
    {
        public FilterBase()
        {
            InitializeComponent();
        }

        private void FilterBase_Load(object sender, EventArgs e)
        {
            
        }

        //Các biến xài tùy ý.
        public SortedDictionary<string, object> ObjectDictionary = new SortedDictionary<string, object>(); 
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
        /// Advance filter get albc
        /// </summary>
        public string Advance { get; set; }
        /// <summary>
        /// Dùng procedure này để lấy dữ liệu. Code trong phần gọi proc của formReportBase.
        /// </summary>
        public string ProcedureName;

        public decimal Number1;
        public decimal Number2;
        public decimal Number3;

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
        /// XuLyHienThiFormSuaChungTuF3
        /// </summary>
        public bool F3 { get; set; }
        /// <summary>
        /// XuLyBoSungThongTinChungTuF4
        /// </summary>
        public bool F4 { get; set; }
        /// <summary>
        /// XuLyXemChiTietF5
        /// </summary>
        public bool F5 { get; set; }
        /// <summary>
        /// Đồ thị
        /// </summary>
        public bool F7 { get; set; }
        /// <summary>
        /// Duyệt
        /// </summary>
        public bool F8 { get; set; }
        /// <summary>
        /// View / In hóa đơn
        /// </summary>
        public bool F9 { get; set; }
        /// <summary>
        /// Xử lý khác.
        /// </summary>
        public bool F10 { get; set; }
        /// <summary>
        /// Hiển thị phần tổng cộng của GridView. Mặc định true.
        /// </summary>
        [DefaultValue(true)]
        [Description("Hiển thị phần tổng cộng của GridView. Mặc định true.")]
        public bool ViewSum { get { return _viewSum; } set { _viewSum = value; } }
        private bool _viewSum = true;
        /// <summary>
        /// Ngôn ngữ tùy chọn khi xuất báo cáo
        /// </summary>
        public string RLan = "V";
        public string RTien ="VN";
        /// <summary>
        /// vị trí bắt đầu cột dữ liệu (cho báo cáo cột động)
        /// </summary>
        public int fstart =5;
        /// <summary>
        /// số cột data (cho báo cáo cột động)
        /// </summary>
        public int ffixcolumn = 6;
        /// <summary>
        /// Tham số thêm cho file rpt
        /// </summary>
        public SortedDictionary<string, object> RptExtraParameters { get; set; } 
        /// <summary>
        /// Chứa dữ liệu gán vào từ hàm GetFilterData của lớp cha
        /// </summary>
        public SortedDictionary<string, object> ParentFilterData { get; set; }
        public SortedDictionary<string, object> FilterData { get; set; }
        /// <summary>
        /// Hàm override lấy các dữ liệu cần thiết để gán vào ParentFilterData cho lớp sau.
        /// </summary>
        /// <returns></returns>
        protected virtual void GetFilterData()
        {
            FilterData = new SortedDictionary<string, object>();
        }
        /// <summary>
        /// Các trường ẩn trên gridview
        /// </summary>
        public SortedDictionary<string, string> _hideFields; 
        /// <summary>
        /// Tùy trường hợp có dùng hay không khi gọi từ report này qua report khác
        /// </summary>
        public List<SqlParameter> InitFilters = new List<SqlParameter>();

        public IDictionary<string, object> ParentRowData;
        public virtual string Kieu_post { get; set; }

        /// <summary>
        /// Lấy danh sách tham số.
        /// </summary>
        /// <returns></returns>
        public virtual List<SqlParameter> GetFilterParameters()
        {
            var list_result = new List<SqlParameter>();
            list_result.AddRange(InitFilters);

            var lineQuery = "";
            foreach (Control control in Controls)
            {
                var lineControl = control as FilterLineBase;
                if (lineControl != null && lineControl.IsSelected)
                {
                    lineQuery += (true ? "\nand " : "\nor  ") + lineControl.Query;
                }
            }
            
            if(lineQuery.Length>0)
            list_result.Add(new SqlParameter("@cKey", lineQuery.Length > 4 ? lineQuery.Substring(4) : ""));

            return list_result;
        }

        public virtual string GetFilterStringByFields(List<string> fields, bool and, string table = null)
        {
            var result = GetFilterStringByFieldsR(this, fields, and, table);
            if (result.Length > 4) result = result.Substring(4);
            return result;
        }

        private string GetFilterStringByFieldsR(Control control, List<string> fields, bool and, string table)
        {
            var result = "";
            var line = control as FilterLineBase;
            if (line != null && line.IsSelected && fields.Contains(line.FieldName.ToUpper()))
            {
                result += (and ? "\nand " : "\nor  ") + line.GetQuery(table);
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result += GetFilterStringByFieldsR(c, fields, and, table);
                }
            }
            return result;
        }

        public delegate void SetParentRowHandle(IDictionary<string,object> row);

        public event SetParentRowHandle SetParentRowEvent;
        
        public void SetParentRow(IDictionary<string,object> currentRow)
        {
            ParentRowData = currentRow;
            OnSetParentRow(ParentRowData);
        }

        public virtual void OnSetParentRow(IDictionary<string,object> currentRow)
        {
            var handler = SetParentRowEvent;
            if (handler != null) handler(currentRow);
        }

        //{tuanmh 11/09/2016
        private DataGridView _parentGridView;
        /// <summary>
        /// Mã liên kết giữa 2 bảng.
        /// Mặc định không thay đổi là stt_rec
        /// Thay đổi ở Init của FilterControl
        /// </summary>
        public string Ref_key = "Stt_rec";

        public string ReportFileF5 = null;
        public string ReportTitleF5 = null;
        public string ReportTitle2F5 = null;

        protected delegate void SetParentAllRowHandle(DataGridView DataGridView1);
        protected event SetParentAllRowHandle SetParentAllRowEvent;
        public void SetParentAllRow(DataGridView DataGridView1)
        {
            _parentGridView = DataGridView1;
            OnSetParentAllRow(_parentGridView);
        }

        protected virtual void OnSetParentAllRow(DataGridView DataGridView1)
        {
            var handler = SetParentAllRowEvent;
            if (handler != null) handler(DataGridView1);
        }
        //}tuanmh 11/09/2016

        /// <summary>
        /// Gán các giá trị String1, Number1...
        /// </summary>
        public virtual void UpdateValues()
        {
            
        }
        
        /// <summary>
        /// Tạo bảng dữ liệu cho cbo trong ChartReportForm
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GenTableForReportType()
        {
            throw new NotImplementedException();
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

        public virtual void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            
        }

        public virtual void FormatGridView2(V6ColorDataGridView dataGridView2)
        {
            
        }

        /// <summary>
        /// Cần override
        /// </summary>
        /// <param name="data"></param>
        public virtual void SetData(IDictionary<string, object> data)
        {
            V6ControlFormHelper.SetSomeDataDictionary(this, data);
        }

        public virtual void SetStatus2Text()
        {
            var text = "";

            if (F3) text += "F3-Sửa chứng từ";
            if (F5)
            {
                if (F3) text += "; ";
                text += "F5-Xem chi tiết";
            }
            if (F7)
            {
                if (F3|F5) text += "; ";
                text += "F7-Xem biểu đồ.";
            }
            V6ControlFormHelper.SetStatusText2(text);
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
                                //Bỏ qua giá trị rỗng.
                                if (di.NotEmpty && string.IsNullOrEmpty("" + _tbl2Row[di.Field])) continue;
                                result[di.Name.ToUpper()] = _tbl2Row[di.Field];
                            }
                        }
                        else if (di.Ptype.ToUpper() == "PARENT")
                        {

                        }
                            // Tuanmh 23/08/2017
                        else if (di.Ptype.ToUpper() == "ONEVALUE")
                        {
                            result[di.Name.ToUpper()] = di.Fname;
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
                catch (Exception)
                {

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
        public SortedDictionary<string,FilterLineBase> GetFilterLineListRecursive(Control control)
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
    }
}
