using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Controls.GridView;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.Viewer
{
    public partial class DataEditorForm : V6Form
    {
        public DataEditorForm()
        {
            InitializeComponent();
        }

        public V6ColorDataGridView DataGridView { get { return dataGridView1; } }
        private object _data;
        private string _tableName, _showFields;
        private string[] _keyFields;
        private IDictionary<string, object> _defaultData = null;
        private bool newRowNeeded;
        private bool _updateDatabase;

        public DataEditorForm(object data, string tableName, string showFields, string keyFields, string title,
            bool allowAdd, bool allowDelete, bool showSum = true, bool updateDatabase = true, IDictionary<string, object> defaultData = null)
        {
            InitializeComponent();
            _updateDatabase = updateDatabase;
            if (!showSum)
            {
                dataGridView1.Height = dataGridView1.Bottom - dataGridView1.Top + gridViewSummary1.Height;
                gridViewSummary1.Visible = false;
            }
            dataGridView1.AllowUserToAddRows = allowAdd;
            dataGridView1.AllowUserToDeleteRows = allowDelete;
            _defaultData = defaultData;
            Text = title;
            _data = data;
            _tableName = tableName;
            _showFields = showFields;
            _keyFields = ObjectAndString.SplitString(keyFields);
            
            MyInit();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private void MyInit()
        {
            try
            {
                dataGridView1.DataSource = _data;
                FormatGridView();
                GetCongThuc();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit " + ex.Message);
            }
        }

        private string congthuc1 = "", congthuc2 = "", congthuc3 = "";
        private void GetCongThuc()
        {
            try
            {
                IDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("MA_DM", _tableName);
                var getData = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                if (getData.Rows.Count == 1)
                {
                    var row = getData.Rows[0];
                    congthuc1 = row["CACH_TINH1"].ToString().Trim();
                    congthuc2 = row["CACH_TINH2"].ToString().Trim();
                    congthuc3 = row["CACH_TINH3"].ToString().Trim();
                }
                else
                {
                    congthuc1 = congthuc2 = congthuc3 = "";
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetCongThuc", ex);
            }
        }

        private void XuLyCongThucTinhToan()
        {
            try
            {
                if (!string.IsNullOrEmpty(congthuc1))
                {
                    XuLyCongThucTinhToan(congthuc1);
                }
                if (!string.IsNullOrEmpty(congthuc2))
                {
                    XuLyCongThucTinhToan(congthuc2);
                }
                if (!string.IsNullOrEmpty(congthuc3))
                {
                    XuLyCongThucTinhToan(congthuc3);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XyLyCongThucTinhToan", ex);
            }
        }

        private void XuLyCongThucTinhToan(string s)
        {
            var ss = s.Split('=');
            if (ss.Length == 2)
            {
                var field = ss[0].Trim();
                updateFieldList.Add(field.ToUpper());
                var bieu_thuc = ss[1].Trim();

                var cRow = dataGridView1.CurrentRow;
                cRow.Cells[field].Value = GiaTriBieuThuc(bieu_thuc);
            }
        }

        private decimal GiaTriBieuThuc(string bieu_thuc)
        {

            //alert(bieu_thuc);
            bieu_thuc = bieu_thuc.Replace(" ", ""); //Bỏ hết khoảng trắng
            bieu_thuc = bieu_thuc.Replace("--", "+"); //loại bỏ lặp dấu -
            bieu_thuc = bieu_thuc.Replace("+-", "-");
            bieu_thuc = bieu_thuc.Replace("-+", "-");
            bieu_thuc = bieu_thuc.Replace("++", "+"); //loại bỏ lặp dấu +
            bieu_thuc = bieu_thuc.Replace("*+", "*");
            bieu_thuc = bieu_thuc.Replace("/+", "/");


            //xử lý Round();
            bieu_thuc = bieu_thuc.ToUpper();
            int roundOpenIndex = bieu_thuc.IndexOf("ROUND(", StringComparison.Ordinal);
            if (roundOpenIndex >= 0)
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
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, roundOpenIndex);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                var phayindex = b.LastIndexOf(',');
                var round1 = b.Substring(0, phayindex);
                var round2 = b.Substring(phayindex + 1);

                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before +V6BusinessHelper.Vround(GiaTriBieuThuc(round1),(int)GiaTriBieuThuc(round2)) + after + c);//RoundV(giatribt(a),giatribt(b))
            }
            //xử lý phép toán trong ngoặc trước.
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
                //
                string before = "", after = "";
                if (iopen <= 0) before = "+";
                else if ("+-*/(".IndexOf(bieu_thuc[iopen - 1], 0) < 0) before = "*"; //Nếu trước dấu ( không phải là
                //alert("sau ) la: " + bieu_thuc[iclose + 1]);
                //alert("IndexOf: " + ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0)));
                if (iclose >= bieu_thuc.Length - 1) after = "+";
                else if ("+-*/)!".IndexOf(bieu_thuc[iclose + 1], 0) < 0) after = "*"; //nếu sau dấu ) không có +-*/)!

                var a = bieu_thuc.Substring(0, iopen);
                if (a.Trim() == "") before = "";
                var b = bieu_thuc.Substring(iopen + 1, iclose - iopen - 1); //a(b)c
                var c = bieu_thuc.Substring(iclose + 1);
                if (c.Trim() == "") after = "";
                //alert(a + ';' + b + ';' + c);
                return GiaTriBieuThuc("" + a + before + GiaTriBieuThuc(b) + after + c);//RoundV(giatribt(a),giatribt(b))
            }

            // có phép cộng trong biểu thức
            if (bieu_thuc.IndexOf('+') >= 0)
            {

                var values = bieu_thuc.Split('+');
                decimal sum = 0;
                for (var i = 0; i < values.Length; i++)
                {
                    sum += GiaTriBieuThuc(values[i]);
                }
                return sum;

                //        var sp = bieu_thuc.LastIndexOf('+');//split point
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
//        return GiaTriBieuThuc(values1) + GiaTriBieuThuc(values2);
            }

            // làm cho hết phép cộng rồi tới phép trừ    ////////////////////////// xử lý số âm hơi vất vả.
            if ((bieu_thuc.Split('-').Length - 1) >
                (bieu_thuc.Split(new[] {"*-"}, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new[] {"/-"}, StringSplitOptions.None).Length +
                 bieu_thuc.Split(new string[] {"^-"}, StringSplitOptions.None).Length - 3))
            {

                var sp = bieu_thuc.IndexOf('-');
                //tim vi tri sp cuoi khong có */^ dung truoc
                for (var i = sp; i < bieu_thuc.Length; i++)
                {
                    if (bieu_thuc[i] == '-' && "*/^".IndexOf(bieu_thuc[i - 1]) < 0)
                    {
                        sp = i;
                    }
                }
                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1) - GiaTriBieuThuc(values2);
            }

            //phép nhân
            if (bieu_thuc.IndexOf('*', 0) >= 0)
            {

                var values = bieu_thuc.Split('*');
                decimal sum = 1;
                for (var i = 0; i < values.Length; i++)
                {
                    sum *= GiaTriBieuThuc(values[i]);
                }
                return sum;

                //Phép Round (Round012.345)

                //        var sp = bieu_thuc.LastIndexOf('*') > bieu_thuc.LastIndexOf("*-") ? bieu_thuc.LastIndexOf('*') : bieu_thuc.LastIndexOf("*-");
                //        var values1 = bieu_thuc.Substring(0, sp);
                //        var values2 = bieu_thuc.Substring(sp + 1);
//        return GiaTriBieuThuc(values1) * GiaTriBieuThuc(values2);
            }
            if (bieu_thuc.IndexOf('/', 0) >= 0)
            {

                var sp = bieu_thuc.LastIndexOf('/') > bieu_thuc.LastIndexOf("/-")
                    ? bieu_thuc.LastIndexOf('/')
                    : bieu_thuc.LastIndexOf("/-");

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return GiaTriBieuThuc(values1)/GiaTriBieuThuc(values2);
            }
            if (bieu_thuc.IndexOf('^', 0) >= 0)
            {
                var sp = bieu_thuc.LastIndexOf('^') < bieu_thuc.LastIndexOf("^-")
                    ? bieu_thuc.LastIndexOf('^')
                    : bieu_thuc.LastIndexOf("^-");

                var values1 = bieu_thuc.Substring(0, sp);
                var values2 = bieu_thuc.Substring(sp + 1);
                return (decimal) Math.Pow((double) GiaTriBieuThuc(values1), (double) GiaTriBieuThuc(values2));
            }
            // giai thừa
            if (bieu_thuc.IndexOf('!', 0) > 0)
            {
                var sp = bieu_thuc.LastIndexOf('!');
                var values1 = bieu_thuc.Substring(0, sp);
                return factorial((int) GiaTriBieuThuc(values1));
            }
            else if (bieu_thuc.Trim() == "") return 0;
            else
            {
                // Bieu thuc luc nay la 1 so cu the.
                if (dataGridView1.Columns.Contains(bieu_thuc))
                    return ObjectAndString.ObjectToDecimal(dataGridView1.CurrentRow.Cells[bieu_thuc].Value);
                return ObjectAndString.ObjectToDecimal(bieu_thuc);
            }
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

        private void FormatGridView()
        {
            try
            {
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("MA_DM", _tableName);
                var data_aldm = V6BusinessHelper.Select(V6TableName.Aldm, keys, "*").Data;
                if (data_aldm.Rows.Count == 1)
                {
                    var row_data = data_aldm.Rows[0];
                    var showFields = row_data["GRDS_V1"].ToString().Trim();
                    var formatStrings = row_data["GRDF_V1"].ToString().Trim();
                    var headerString = row_data[V6Setting.IsVietnamese?"GRDHV_V1":"GRDHE_V1"].ToString().Trim();
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, showFields, formatStrings, headerString);
                }

                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    var showFieldList = new List<string>();
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            showFieldList.Add(ss[0]);
                            var column = dataGridView1.Columns[ss[0]];
                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                        }
                        else
                        {
                            showFieldList.Add(field);
                        }
                    }

                    dataGridView1.ShowColumns(true, showFieldList.ToArray());
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".FormatGridView " + ex.Message);
            }
        }

        public event Action<Keys> HotKeyAction;
        protected virtual void OnHotKeyAction(Keys keyData)
        {
            var handler = HotKeyAction;
            if (handler != null) handler(keyData);
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            if (keyData == Keys.Delete && dataGridView1.AllowUserToDeleteRows && !dataGridView1.IsCurrentCellInEditMode)
            {
                if (!_updateDatabase)
                {
                    if (dataGridView1.CurrentRow != null) dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    //toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteSuccess, delete_info);
                    return true;
                }
                if (dataGridView1.CurrentRow != null)
                {
                    var selectedRowIndex = dataGridView1.CurrentRow.Index;
                    if (selectedRowIndex < dataGridView1.NewRowIndex)
                    {
                        //var rowData = dataGridView1.CurrentRow.ToDataDictionary();
                        var keys = new SortedDictionary<string, object>();
                        delete_info = "";
                        foreach (string field in _keyFields)
                        {
                            var UPDATE_FIELD = field.ToUpper();
                            var update_value = dataGridView1.CurrentRow.Cells[field].Value;
                            keys.Add(UPDATE_FIELD, update_value);
                            delete_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                        }
                        if (keys.Count > 0) DeleteData(keys, selectedRowIndex);
                    }
                }
                return true;
            }
            if (keyData == Keys.F3 && !dataGridView1.ReadOnly)
            {
                var data = dataGridView1.CurrentRow.ToDataDictionary();
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("UID", data["UID"]);
                FormAddEdit f = new FormAddEdit(_tableName, V6Mode.Edit, keys, data);
                //f.AfterInitControl += f_AfterInitControl;
                f.UpdateSuccessEvent += f_UpdateSuccess;
                f.InitFormControl();
                f.ShowDialog(this);
            }

            OnHotKeyAction(keyData);
            return base.DoHotKey0(keyData);
        }

        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(SortedDictionary<string, object> data)
        {
            try
            {
                if (data == null) return;
                DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                V6ControlFormHelper.UpdateGridViewRow(row, data);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
        }
        
        string delete_info = "";
        
        /// <summary>
        /// Thêm dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private IDictionary<string, object> AddData(IDictionary<string, object> data)
        {
            try
            {
                toolStripStatusLabel1.Text = V6Text.Add + _tableName;

                //Gán dữ liệu mặc định
                if (_defaultData != null)
                {
                    data.AddRange(_defaultData, true);
                }
                //Remove UID in data
                if (data.ContainsKey("UID")) data.Remove("UID");
                //Tạo keys giả
                IDictionary<string, object> keys = new Dictionary<string, object>();
                foreach (string field in _keyFields)
                {
                    var FIELD = field.ToUpper();
                    if(FIELD != "UID")
                    {
                        object value = "0";
                        if (_defaultData != null && _defaultData.ContainsKey(FIELD))
                        {
                            value = _defaultData[FIELD];
                        }
                        data[FIELD] = value;
                        keys.Add(FIELD, value);
                    }
                }
                //Full string keys neu key rong
                if (keys.Count == 0)
                {
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        string FIELD = column.DataPropertyName.ToUpper();
                        if (FIELD != "TIME0" && FIELD != "TIME2"
                            && column.ValueType == typeof (string))
                        {
                            if(data.ContainsKey(FIELD)) keys[FIELD] = data[FIELD];
                        }
                    }
                    //keys.AddRange(data);
                }
                
                var result = V6BusinessHelper.Insert(_tableName, data);
                if (result)
                {
                    toolStripStatusLabel1.Text = string.Format("{0} {1}", _tableName, V6Text.AddSuccess);
                    
                    var selectResult = V6BusinessHelper.Select(_tableName, keys, "*");
                    if (selectResult.TotalRows == 1)
                    {
                        return selectResult.Data.Rows[0].ToDataDictionary();
                    }
                    else if (selectResult.TotalRows > 1)
                    {
                        toolStripStatusLabel1.Text = "Có 2 dòng gần giống nhau.";
                        return selectResult.Data.Rows[selectResult.TotalRows - 1].ToDataDictionary();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Dữ liệu không xác định.";
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1}", _tableName, V6Text.AddFail);
                this.WriteExLog(GetType() + ".AddData", ex);
            }
            return null;
        }
        
        /// <summary>
        /// Xóa dữ liệu trong cơ sở dữ liệu, nếu thành công xóa luôn trên dataGridview.
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="rowIndex"></param>
        private void DeleteData(IDictionary<string, object> keys, int rowIndex)
        {
            try
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", V6Text.Delete, _tableName, delete_info);
                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    var result = V6BusinessHelper.Delete(_tableName, keys);
                    if (result > 0)
                    {
                        dataGridView1.Rows.RemoveAt(rowIndex);
                        toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteSuccess, delete_info);
                    }
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.DeleteFail, delete_info);
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private object _cellBeginEditValue;
        /// <summary>
        /// Update dữ liệu vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        private void UpdateData(int rowIndex, int columnIndex)
        {
            var update_info = "";
            try
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1}", V6Text.Edit, _tableName);
                var row = dataGridView1.Rows[rowIndex];
                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                foreach (string field in _keyFields)
                {
                    var currentKeyFieldColumnIndex = row.Cells[field].ColumnIndex;
                    if (columnIndex == currentKeyFieldColumnIndex)
                    {
                        keys.Add(field.ToUpper(), _cellBeginEditValue);
                    }
                    else
                    {
                        keys.Add(field.ToUpper(), row.Cells[field].Value);
                    }
                }

                SortedDictionary<string, object> updateData = new SortedDictionary<string, object>();
                var UPDATE_FIELD = dataGridView1.Columns[columnIndex].DataPropertyName.ToUpper();
                var update_value = row.Cells[columnIndex].Value;
                updateData.Add(UPDATE_FIELD, update_value);
                update_info += UPDATE_FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                foreach (string FIELD in updateFieldList)
                {
                    update_value = row.Cells[FIELD].Value;
                    updateData[FIELD] = update_value;
                    update_info += FIELD + " = " + ObjectAndString.ObjectToString(update_value) + ". ";
                }

                var result = V6BusinessHelper.UpdateSimple(_tableName, updateData, keys);
                if (result > 0)
                {
                    toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.EditSuccess, update_info);
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = string.Format("{0} {1} {2}", _tableName, V6Text.EditFail, update_info);
                Logger.WriteExLog(V6Login.ClientName + " " + GetType() + ".UpdateData",
                    ex, V6ControlFormHelper.LastActionListString, Application.ProductName);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _cellBeginEditValue = dataGridView1.CurrentCell.Value;
        }

        private List<string> updateFieldList = new List<string>();
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var UPDATE_FIELD = dataGridView1.Columns[e.ColumnIndex].DataPropertyName.ToUpper();
            //Xu ly cong thuc tinh toan
            updateFieldList = new List<string>();
            if(CheckUpdateField(UPDATE_FIELD)) XuLyCongThucTinhToan();
            
            if(_updateDatabase) UpdateData(e.RowIndex, e.ColumnIndex);
        }

        private bool CheckUpdateField(string UPDATE_FIELD)
        {
            if (!string.IsNullOrEmpty(congthuc1) && congthuc1.IndexOf(UPDATE_FIELD, congthuc1.IndexOf("=", StringComparison.Ordinal), StringComparison.Ordinal) >= 0) return true;
            if (!string.IsNullOrEmpty(congthuc2) && congthuc2.IndexOf(UPDATE_FIELD, congthuc2.IndexOf("=", StringComparison.Ordinal), StringComparison.Ordinal) >= 0) return true;
            if (!string.IsNullOrEmpty(congthuc3) && congthuc3.IndexOf(UPDATE_FIELD, congthuc3.IndexOf("=", StringComparison.Ordinal), StringComparison.Ordinal) >= 0) return true;
            return false;
        }


        private void dataGridView1_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
        {
            newRowNeeded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (newRowNeeded)
            {
                newRowNeeded = false;
                //numberOfRows = numberOfRows + 1;
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            var currentRow = dataGridView1.CurrentRow;
            if (currentRow == null) return;

            if (_updateDatabase)
            {
                var newData = currentRow.ToDataDictionary();
                var afterData = AddData(newData);
                if (afterData != null)
                {
                    foreach (KeyValuePair<string, object> item in afterData)
                    {
                        if (dataGridView1.Columns.Contains(item.Key))
                        {
                            currentRow.Cells[item.Key].Value = item.Value;
                        }
                    }
                }
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            try
            {
                if (_showFields != null)
                {
                    var showFieldSplit = ObjectAndString.SplitString(_showFields);
                    foreach (string field in showFieldSplit)
                    {
                        if (field.Contains(":"))
                        {
                            var ss = field.Split(':');
                            DataGridViewColumn column = null;
                            
                            if (ss.Length > 2)
                            {
                                string NM_IP = ss[2].ToUpper(); // N2 hoac NM_IP_SL
                                if (NM_IP.StartsWith("N"))
                                {
                                    string newFormat = NM_IP.Length == 2 ? NM_IP : V6Options.GetValueNull(NM_IP.Substring(1));
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6NumberDataGridViewColumn), newFormat);
                                }
                                else if (NM_IP.StartsWith("C")) // CVvar
                                {
                                    column = dataGridView1.ChangeColumnType(ss[0], typeof(V6VvarDataGridViewColumn), null);
                                    ((V6VvarDataGridViewColumn)column).Vvar = NM_IP.Substring(1);
                                    //((V6VvarDataGridViewColumn)column).DecimalLength = NM_IP.Substring(1);
                                    ((V6VvarDataGridViewColumn)column).Tag = NM_IP.Substring(1);
                                }
                            }
                            
                            if (ss[1].ToUpper() == "R" && column != null)
                            {
                                column.ReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_DataSourceChanged", ex);
            }
        }

        
    }
}
