using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;

namespace V6Controls
{
    class StandardDAO
    {
        Standard standard = null;
        public DataTable tableRoot = null;
        BindingSource source = null;
        Boolean flagAllClick = false; //Dùng để nhận biết nút "All" có được nhấn
        StandardConfig lstConfig = null;
        
        string cKey = " and 1=1 ";

        string _initStrFilter, _vSearchFilter;
        TextBox textSender = null;
        
        
        public StandardDAO(Standard form, TextBox sender, string vSearchFilter)
        {
            standard = form;
            textSender = sender;
            
            lstConfig = form.LstConfig;
            _initStrFilter = form.InitStrFilter;
            _vSearchFilter = vSearchFilter;

            try
            {                   
                tableRoot = ThemDuLieuVaoBangChinh(
                    lstConfig.TableName, _initStrFilter, _vSearchFilter);

                
            }
            catch (Exception e)
            {
                V6ControlFormHelper.ShowErrorMessage(e.Message, "StandardDAO init");
            }
        }
        
        public StandardDAO(Standard form)
        {
            standard = form;
            lstConfig = form.LstConfig;
        }


        /// <summary>
        /// Lấy tất cả dữ liệu?
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="initFilter"></param>
        /// <param name="vSearchFilter"></param>
        /// <returns></returns>
        public DataTable ThemDuLieuVaoBangChinh(string tableName, string initFilter, string vSearchFilter)
        {
            if (tableName != "")
            {
                try
                {
                    if (lstConfig.Vvar.ToUpper() == "MA_LO")
                    {
                        //return new DataTable();
                        //Cần sửa lại config vField/eField...
                    }

                    if (standard._multiSelect && vSearchFilter.Contains(","))
                    {
                        var tbl = V6BusinessHelper.Select(tableName, "*", initFilter).Data;
                        return tbl;
                    }
                    else
                    {
                        var where = initFilter;
                        if (!string.IsNullOrEmpty(vSearchFilter))
                        {
                            where += " AND (" + vSearchFilter + ")";
                        }
                        var tbl = V6BusinessHelper.Select(tableName, "*", where).Data;
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

        public DataTable LayTatCaDanhMuc(string vSearchFilter = "")
        {
            //Khi bấm nút "ALL" thì sẽ không có điều kiện lọc
            try
            {
                tableRoot = ThemDuLieuVaoBangChinh(lstConfig.TableName, _initStrFilter, vSearchFilter);
            }
            catch (Exception e)
            {
                throw new Exception("StandardDAO.LayTatCaDanhMuc : " + e.Message);
            }
            myView = new DataView(tableRoot);
            //Huuan add: cập nhập lại tempTable
            tempTable = myView.ToTable();
            return tableRoot;
        }

        public void NapCacFieldDKLoc()
        {
            if (!String.IsNullOrEmpty(lstConfig.TableName))
            {
                try
                {
                    SqlParameter[] plist = {new SqlParameter("@p", lstConfig.TableName)};
                    var ds = V6BusinessHelper.Select("INFORMATION_SCHEMA.COLUMNS", "COLUMN_NAME,DATA_TYPE",
                        "TABLE_NAME = @p","","", plist).Data;

                    standard.cbbDieuKien.DisplayMember = "COLUMN_NAME";
                    standard.cbbDieuKien.ValueMember = "DATA_TYPE";
                    standard.cbbDieuKien.DataSource = ds;
                    standard.cbbDieuKien.DisplayMember = "COLUMN_NAME";
                    standard.cbbDieuKien.ValueMember = "DATA_TYPE";
                }
                catch (Exception e)
                {
                    throw new Exception("StandardDAO.NapCacFieldDKLoc : " + e.Message);
                }
            }
            else
            {
                //LibraryHelper.Log("StandardDAO.NapCacFieldDKLoc : \"categoryName\" không được trống");
            }
        }

        public void LayThongTinTieuDe()
        {
            string formName = V6Setting.Language=="V"?lstConfig.V1Title:lstConfig.E1Title;
            standard.Text = formName;
        }

        public void ChonGiaTriKhoiTaoCho_cbbKyHieu()
        {
            if (standard.cbbKyHieu.Items.Count != 0)
            {
                standard.cbbKyHieu.SelectedIndex = 0;
            }
        }

        public void AnDauTrongComBoBoxDau()
        {
            try
            {
                string comboboxValue = standard.cbbDieuKien.SelectedValue.ToString().Trim();
                if (comboboxValue == "char")
                {
                    standard.cbbKyHieu.Items.AddRange(new object[] { "$", "<>" });
                }
                else if (comboboxValue == "money" || comboboxValue == "tinyint" || comboboxValue == "smalldatetime" || comboboxValue.Contains("numeric"))
                {
                    standard.cbbKyHieu.Items.AddRange(new object[] { "=", ">", ">=", "<", "<=", "<>" });
                }
            }
            catch (Exception e)
            {
                throw new Exception("StandardDAO.AnDauTrongComBoBox : " + e.Message);
            }
        }

        public void KhoiTaoDataGridView()
        {
            if (tableRoot != null)
            {
                try
                {
                    standard.dataGridView1.DataSource = tableRoot;
                    V6ControlsHelper.ThietLapTruongHienThiTrongDataGridView(standard.dataGridView1,
                        V6Setting.Language == "V" ? lstConfig.VFields : lstConfig.EFields,
                        V6Setting.Language=="V"?lstConfig.VHeaders:lstConfig.EHeaders,
                        lstConfig.VWidths);

                    if (standard.dataGridView1.Rows.Count > 0)
                    {
                        standard.dataGridView1.Select();
                    }
                    else
                    {
                        standard.txtV_Search.Select();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("StandardDAO.KhoiTaoDataGridView : " + e.Message);
                }
            }
            else
            {
                //LibraryHelper.Log("StandardDAO.KhoiTaoDataGridView : \"tableRoot\" không được null");
            }
        }

        
        public DataView myView = null;
        public DataView tempView = null;
        public DataTable tempTable = null;
        public void TimKiemDMKH(bool isRapidSearch)
        {
            if (standard.rbtLocTuDau.Checked && standard.cbbGoiY.Text != "") // IF 1
            {
                try
                {
                    myView = new DataView(tableRoot);
                    //Thiết lập điều kiện lọc cho đối tượng "source"
                    if (isRapidSearch) //Mặc định là chọn theo điều kiện thuộc("$") khi tìm kiếm nhanh
                        source = LocTheoDieuKien(standard.cbbDieuKien.Text, "$", standard.cbbGoiY.Text);
                    else // trường hợp tìm kiếm bình thường
                        source = LocTheoDieuKien(standard.cbbDieuKien.Text, standard.cbbKyHieu.Text, standard.cbbGoiY.Text);
                    //Lọc đối tượng DataView theo điều kiện lọc "source" vừa được thiết lập
                    source.DataSource = myView;
                    standard.dataGridView1.DataSource = source;
                    tempTable = myView.ToTable(); //Lưu lại view đã lọc dùng để lọc tiếp tục
                }
                catch (Exception e)
                {
                    throw new Exception("StandardDAO.TimKiemDMKH if 1 : " + e.Message);
                }
            }
            if (standard.rbtLocTiep.Checked && standard.cbbGoiY.Text != "") //IF 2
            {
                try
                {
                    if (flagAllClick)
                    {
                        tempTable = tableRoot;
                        flagAllClick = false;
                    }
                    if (tempTable != null)
                    {
                        tempView = new DataView(tempTable); //Gán view vừa được lọc vào 1 view khác để tiếp tục lọc theo dk khác
                        //Mặc định là chọn theo điều kiện thuộc("$") khi tìm kiếm nhanh (isRapidSearch)
                        // trường hợp khác tìm kiếm bình thường
                        source = LocTheoDieuKien(standard.cbbDieuKien.Text, isRapidSearch ? "$" : standard.cbbKyHieu.Text, standard.cbbGoiY.Text);
                        source.DataSource = tempView;
                        standard.dataGridView1.DataSource = source;
                        tempTable = tempView.ToTable(); //Lưu lại các giá trị vừa lọc đưa vào bảng tạm để dùng trong trường hợp user muốn lọc tiếp
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("StandardDAO.TimKiemDMKH if 2 : " + e.Message);
                }
            }
        }

        private BindingSource LocTheoDieuKien(string _dkLoc, string _kyHieu, string _bieuThuc)
        {
            BindingSource dataSource = new BindingSource();
            string strFilter = "";
            switch (_kyHieu)
            {
                case "$":
                    {
                        strFilter = _dkLoc + " like '%" + _bieuThuc + "%'"; // BHN in BHN001
                        break;
                    }
                case "=":
                    {
                        strFilter = _dkLoc + " = '" + _bieuThuc + "'"; // BHN in BHN001
                        break;
                    }
                case ">":
                    {
                        strFilter = _dkLoc + " > '" + _bieuThuc + "'";
                        break;
                    }
                case ">=":
                    {
                        strFilter = _dkLoc + " >= '" + _bieuThuc + "'";
                        break;
                    }
                case "<":
                    {
                        strFilter = _dkLoc + " < '" + _bieuThuc + "'";
                        break;
                    }
                case "<=":
                    {
                        strFilter = _dkLoc + " <= '" + _bieuThuc + "'";
                        break;
                    }
                case "<>":
                    {
                        strFilter = _dkLoc + " <> '" + _bieuThuc + "'";
                        break;
                    }
            }
            dataSource.Filter = strFilter;
            return dataSource;
        }

        public void ThietLapGridViewKhiNhanLeft_Right()
        {
            standard.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //standard.dataGridView1.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            //standard.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        public void ThietLapGridViewKhiNhanUp_Down()
        {
            standard.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //standard.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
            //standard.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        public void XuLyEnterChonGiaTri(string selectedValue, V6VvarTextBox textbox)
        {
            if (standard._multiSelect)
            {
                try
                {
                    textbox.Text = selectedValue;
                }
                catch (Exception)
                {
                    
                }
            }
            else
            {
                ControlFunction.LISTVALUE.Add(selectedValue);
                if (textbox.ReadOnly) return;

                if (ControlFunction.LISTVALUE.Count > 0)
                {
                    try
                    {
                        if (ControlFunction.LISTVALUE.Count > 0)
                        {
                            cKey = " and a." + lstConfig.FieldName.Trim() + " = '" + selectedValue + "' ";
                        }
                        //Tạo bảng tạm
                        string tempTableName = "";
                        tempTableName = "@" + lstConfig.TableName;

                        var tblData = new DataTable();
                        tblData.Columns.Add("Id", typeof (string));
                        //Đưa dữ liệu từ List<string> vào DataTable
                        ControlFunction.LISTVALUE.ForEach(x => tblData.Rows.Add(x.Trim()));
                        //Kiểm tra bảng đã tồn tại trong danh sách đối tượng bảng chưa
                        if (V6ControlsHelper.KiemTraBangTonTai(tempTableName, ControlFunction.LSTDATATABLE) == null)
                            //Chưa tồn tại
                            ControlFunction.LSTDATATABLE.Add(new MyDataTable(tempTableName, tblData));
                        else // Đã tồn tại rồi
                        {
                            var result = ControlFunction.LSTDATATABLE.Find
                                (
                                    tbl => tbl.TableName == tempTableName
                                );

                            ControlFunction.LSTDATATABLE.Remove(result); // xóa đối tượng đã tồn tại
                            ControlFunction.LSTDATATABLE.Add(new MyDataTable(tempTableName, tblData));
                                // thêm đối tượng mới vào danh sách
                        }
                        textbox.ChangeText(selectedValue); // Gán giá trị vừa chọn vào trong textbox
                        var selectData = V6BusinessHelper.Select(lstConfig.TableName, "*",
                            "[" + lstConfig.FieldName + "]='" + selectedValue + "'");
                        if (selectData.Data != null && selectData.Data.Rows.Count == 1)
                        {
                            var oneRow = selectData.Data.Rows[0];
                            textbox.SetDataRow(oneRow);
                        }

                        ControlFunction.LISTVALUE.Clear();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("StandardDAO.XuLyEnterChonGiaTri : " + e.Message);
                    }
                }
            }
        }
    }//End Class
}
