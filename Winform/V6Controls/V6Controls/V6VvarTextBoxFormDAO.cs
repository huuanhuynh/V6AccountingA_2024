using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;

namespace V6Controls
{
    class V6VvarTextBoxFormDAO
    {
        V6VvarTextBoxForm _lookupForm = null;
        
        
        
        V6lookupConfig _config = null;
        
        string cKey = " and 1=1 ";

        string _initStrFilter, _vSearchFilter;
        TextBox textSender = null;
        
        
        //public V6VvarTextBoxFormDAO_0(V6VvarTextBoxForm form, TextBox sender, string vSearchFilter)
        //{
        //    _lookupForm = form;
        //    textSender = sender;            
        //    _config = form._config;
        //    _initStrFilter = form.InitStrFilter;
        //    _vSearchFilter = vSearchFilter;
        //    try
        //    {                   
        //        tableRoot = ThemDuLieuVaoBangChinh(
        //            _config.TableName, _initStrFilter, _vSearchFilter);                
        //    }
        //    catch (Exception e)
        //    {
        //        V6ControlFormHelper.ShowErrorMessage(e.Message, "V6VvarTextBoxFormDAO init");
        //    }
        //}
        
        public V6VvarTextBoxFormDAO(V6VvarTextBoxForm form)
        {
            _lookupForm = form;
            _config = form._config;
        }


        /// <summary>
        /// Lấy tất cả dữ liệu?
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="initFilter"></param>
        /// <param name="vSearchFilter"></param>
        /// <returns></returns>
        public DataTable ThemDuLieuVaoBangChinh0(string tableName, string initFilter, string vSearchFilter)
        {
            if (tableName != "")
            {
                try
                {
                    if (_config.vVar.ToUpper() == "MA_LO")
                    {
                        //return new DataTable();
                        //Cần sửa lại config vField/eField...
                    }

                    var where = initFilter;
                    // Lọc quyền proc
                    try
                    {
                        string right_proc = V6BusinessHelper.GetWhereAl(tableName);
                        if (!string.IsNullOrEmpty(right_proc))
                        {
                            where += string.Format("{0}({1})", where.Length > 0 ? " and " : "", right_proc);
                        }
                    }
                    catch (Exception ex)
                    {

                        //ShowMainMessage("DanhMucView GetWhereAl " + ex.Message);
                    }

                    if ((_lookupForm._lookupMode == LookupMode.Multi || _lookupForm._lookupMode == LookupMode.Data)
                        && vSearchFilter.Contains(","))
                    {
                        var tbl = V6BusinessHelper.Select(tableName, "*", where, "", _config.vOrder).Data;
                        return tbl;
                    }
                    else
                    {
                        
                        if (!string.IsNullOrEmpty(vSearchFilter))
                        {
                            if (string.IsNullOrEmpty(where))
                            {
                                where = vSearchFilter;
                            }
                            else
                            {
                                where += " AND (" + vSearchFilter + ")";
                            }                            
                        }

                        var tbl = V6BusinessHelper.Select(tableName, "*", where, "", _config.vOrder).Data;
                        return tbl;
                    }
                }
                catch (Exception ex)
                {
                    V6ControlFormHelper.WriteExLog(GetType() + ".ThemDuLieuVaoBangChinh", ex);
                }
            }
            else
            {
                throw new ArgumentException("V6ControlsHelper.ThemDuLieuVaoBangChinh : tham số không hợp lệ");
            }
            return null;
        }

        

        //public void NapCacFieldDKLoc0()
        //{
        //    if (!String.IsNullOrEmpty(_config.TableName))
        //    {
        //        try
        //        {
        //            SqlParameter[] plist = {new SqlParameter("@p", _config.TableName)};
        //            var ds = V6BusinessHelper.Select("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME,DATA_TYPE",
        //                "TABLE_NAME = @p","","", plist).Data;

        //            _lookupForm.cbbDieuKien.DisplayMember = "COLUMN_NAME";
        //            _lookupForm.cbbDieuKien.ValueMember = "DATA_TYPE";
        //            _lookupForm.cbbDieuKien.DataSource = ds;
        //            _lookupForm.cbbDieuKien.DisplayMember = "COLUMN_NAME";
        //            _lookupForm.cbbDieuKien.ValueMember = "DATA_TYPE";
        //        }
        //        catch (Exception e)
        //        {
        //            throw new Exception("V6VvarTextBoxFormDAO.NapCacFieldDKLoc : " + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        //LibraryHelper.Log("V6VvarTextBoxFormDAO.NapCacFieldDKLoc : \"categoryName\" không được trống");
        //    }
        //}

        //public void ChonGiaTriKhoiTaoCho_cbbKyHieu0()
        //{
        //    if (_lookupForm.cbbKyHieu.Items.Count != 0)
        //    {
        //        _lookupForm.cbbKyHieu.SelectedIndex = 0;
        //    }
        //}

        //public void AnDauTrongComBoBoxDau0()
        //{
        //    try
        //    {
        //        string comboboxValue = _lookupForm.cbbDieuKien.SelectedValue.ToString().Trim();
        //        if (comboboxValue == "char")
        //        {
        //            _lookupForm.cbbKyHieu.Items.AddRange(new object[] { "$", "<>" });
        //        }
        //        else if (comboboxValue == "money" || comboboxValue == "tinyint" || comboboxValue == "smalldatetime" || comboboxValue.Contains("numeric"))
        //        {
        //            _lookupForm.cbbKyHieu.Items.AddRange(new object[] { "=", ">", ">=", "<", "<=", "<>" });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("V6VvarTextBoxFormDAO.AnDauTrongComBoBox : " + e.Message);
        //    }
        //}

        //public void KhoiTaoDataGridView0()
        //{
        //    if (tableRoot != null)
        //    {
        //        try
        //        {
        //            _lookupForm.dataGridView1.DataSource = tableRoot;
        //            V6ControlFormHelper.FormatGridViewAndHeader(_lookupForm.dataGridView1,
        //                V6Setting.Language == "V" ? _config.vFields : _config.eFields,
        //                _config.vWidths,
        //                V6Setting.Language=="V"?_config.vHeaders:_config.eHeaders);

        //            if (_lookupForm.dataGridView1.Rows.Count > 0)
        //            {
        //                _lookupForm.dataGridView1.Select();
        //            }
        //            else
        //            {
        //                _lookupForm.txtV_Search.Select();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            V6ControlFormHelper.ShowErrorException(GetType() + ".KhoiTaoDataGridView", ex);
        //        }
        //    }
        //    else
        //    {
        //        //LibraryHelper.Log("V6VvarTextBoxFormDAO.KhoiTaoDataGridView : \"tableRoot\" không được null");
        //    }
        //}
        
        
        

        

        //public void ThietLapGridViewKhiNhanLeft_Right()
        //{
        //    _lookupForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //    //_lookupForm.dataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        //    //_lookupForm.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
        //}

        //public void ThietLapGridViewKhiNhanUp_Down()
        //{
        //    _lookupForm.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    //_lookupForm.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
        //    //_lookupForm.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        //}

        //public void XuLyEnterChonGiaTri0(string selectedValue, V6VvarTextBox textbox)
        //{
        //    if (_lookupForm._lookupMode == LookupMode.Multi)
        //    {
        //        try
        //        {
        //            textbox.Text = selectedValue;
        //        }
        //        catch (Exception)
        //        {
                    
        //        }
        //    }
        //    else if (_lookupForm._lookupMode == LookupMode.Single)
        //    {
        //        ControlFunction.LISTVALUE.Add(selectedValue);
        //        if (textbox.ReadOnly) return;

        //        if (ControlFunction.LISTVALUE.Count > 0)
        //        {
        //            try
        //            {
        //                if (ControlFunction.LISTVALUE.Count > 0)
        //                {
        //                    cKey = " and a." + _config.vValue.Trim() + " = '" + selectedValue + "' ";
        //                }
        //                //Tạo bảng tạm
        //                string tempTableName = "";
        //                tempTableName = "@" + _config.TableName;

        //                var tblData = new DataTable();
        //                tblData.Columns.Add("Id", typeof (string));
        //                //Đưa dữ liệu từ List<string> vào DataTable
        //                ControlFunction.LISTVALUE.ForEach(x => tblData.Rows.Add(x.Trim()));
        //                //Kiểm tra bảng đã tồn tại trong danh sách đối tượng bảng chưa
        //                if (V6ControlsHelper.KiemTraBangTonTai(tempTableName, ControlFunction.LSTDATATABLE) == null)
        //                    //Chưa tồn tại
        //                    ControlFunction.LSTDATATABLE.Add(new MyDataTable(tempTableName, tblData));
        //                else // Đã tồn tại rồi
        //                {
        //                    var result = ControlFunction.LSTDATATABLE.Find
        //                        (
        //                            tbl => tbl.TableName == tempTableName
        //                        );

        //                    ControlFunction.LSTDATATABLE.Remove(result); // xóa đối tượng đã tồn tại
        //                    ControlFunction.LSTDATATABLE.Add(new MyDataTable(tempTableName, tblData));
        //                        // thêm đối tượng mới vào danh sách
        //                }
        //                textbox.ChangeText(selectedValue); // Gán giá trị vừa chọn vào trong textbox
        //                var selectData = V6BusinessHelper.Select(_config.TableName, "*",
        //                    "[" + _config.vValue + "]='" + selectedValue + "'");
        //                if (selectData.Data != null && selectData.Data.Rows.Count == 1)
        //                {
        //                    var oneRow = selectData.Data.Rows[0];
        //                    textbox.SetDataRow(oneRow);
        //                }

        //                ControlFunction.LISTVALUE.Clear();
        //            }
        //            catch (Exception e)
        //            {
        //                throw new Exception("V6VvarTextBoxFormDAO.XuLyEnterChonGiaTri : " + e.Message);
        //            }
        //        }
        //    }
        //}

    }//End Class
}
