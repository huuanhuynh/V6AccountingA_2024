using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AlkmbAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private string _sttRec0 = "";

        public AlkmbAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            _maCt = "S0M";
            _table2Name = "Alkmbct";
            try
            {
                txtMaCt.Text = _maCt;

                //txt.SetInitFilter("");

                _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);

                LoadDetailControls();

                detail1.MODE = V6Mode.View;
                detail1.lblName.AccessibleName = "";
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
            
        }

        public override void DoBeforeAdd()
        {
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(_maCt);
        }


        private V6VvarTextBox _ma_vt, _ma_hangkm;
        private V6NumberTextBox _tslkm, _tu_soluong, _den_soluong, _tu_sotien, _den_sotien,
            _soluong_km, _sotien_km, _pt_ck;
        private V6DateTimeColor _ngayKetThuc, _ngayBatDau;
        private V6ColorTextBox _ten_vt, _dvt, _ghichu_km, _ghichu_ck;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có
            
            _ma_vt = new V6VvarTextBox
            {
                AccessibleName = "ma_vt",
                VVar = "ma_vt",
                GrayText = "Mã vật tư",
                BrotherFields = "TEN_VT,DVT",
                //NeighborFields = "TEN_VT0"
            };
            _ma_vt.Upper();
            _ma_vt.FilterStart = true;
            _ma_vt.V6LostFocus += delegate
            {
                GetThongTinVt();
            };

            _ten_vt = new V6ColorTextBox
            {
                AccessibleName = "TEN_VT",
                GrayText = "Tên vật tư",
                Width = 300,
                Enabled = false,
                Tag = "disable"
            };
            _dvt = new V6ColorTextBox
            {
                AccessibleName = "DVT",
                GrayText = "ĐVT",
                Enabled = false,
                Tag = "disable"
            };
            _tslkm = new V6NumberTextBox()
            {
                AccessibleName = "T_SLKM",
                GrayText = "Tổng sl km",
            };
            _tu_soluong = new V6NumberTextBox()
            {
                AccessibleName = "T_SL1",
                GrayText = "Từ số lượng",
            };
            _den_soluong = new V6NumberTextBox()
            {
                AccessibleName = "T_SL2",
                GrayText = "Đến",
            };

            _tu_sotien = new V6NumberTextBox()
            {
                AccessibleName = "T_TIEN1",
                GrayText = "Từ số tiền",
            };
            _den_sotien = new V6NumberTextBox()
            {
                AccessibleName = "T_TIEN2",
                GrayText = "Đến",
            };
            _ma_hangkm = new V6VvarTextBox
            {
                AccessibleName = "MA_SP",
                VVar = "ma_vt",
                GrayText = "Mã sản phẩm",
                //BrotherFields = "TEN_VT",
                //NeighborFields = "TEN_VT0"
            };
            _soluong_km = new V6NumberTextBox()
            {
                AccessibleName = "SL_KM",
                GrayText = "Số lượng km",
            };
            _sotien_km = new V6NumberTextBox()
            {
                AccessibleName = "TIEN_KM",
                GrayText = "Số tiền km",
            };
            _ghichu_km = new V6ColorTextBox()
            {
                AccessibleName = "GHI_CHUKM",
                GrayText = "Ghi chú km",
            };
            _pt_ck = new V6NumberTextBox()
            {
                AccessibleName = "PT_CK",
                GrayText = "%CK",
            };
            _ghichu_ck = new V6ColorTextBox()
            {
                AccessibleName = "GHI_CHUCK",
                GrayText = "Ghi chú ck",
            };

            
            var dynamicControlList = new SortedDictionary<int, Control>();
            
            dynamicControlList.Add(0, _ma_vt);
            dynamicControlList.Add(1, _ten_vt);
            dynamicControlList.Add(2, _dvt);
            dynamicControlList.Add(3, _tslkm);
            dynamicControlList.Add(4, _tu_soluong);
            dynamicControlList.Add(5, _den_soluong);
            dynamicControlList.Add(6, _tu_sotien);
            dynamicControlList.Add(7, _den_sotien);

            dynamicControlList.Add(8, _ma_hangkm);
            dynamicControlList.Add(9, _soluong_km);

            dynamicControlList.Add(10, _sotien_km);
            dynamicControlList.Add(11, _ghichu_km);
            dynamicControlList.Add(12, _pt_ck);
            dynamicControlList.Add(13, _ghichu_ck);
            
            
            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);
            }
            
            //Add detail controls
            foreach (Control control in dynamicControlList.Values)
            {
                
                detail1.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail1, _table2Struct);
        }

        private void GetThongTinVt()
        {
            if (_ma_vt.Data != null)
            {
                _ten_vt.Text = _ma_vt.Data["Ten_vt"].ToString().Trim();
                _dvt.Text = _ma_vt.Data["Dvt"].ToString().Trim();
            }
            else
            {
                _ten_vt.Clear();
                _dvt.Clear();
            }
        }

        public override void LoadDetails()
        {
            try
            {
                //Load AD
                string sttRec = DataOld == null ? "" : DataOld["STT_REC"].ToString();
                string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
                             " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
                SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
                AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

                SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadDetails", ex);
            }
        }

        public override bool InsertNew()
        {
            try
            {
                DataDic = GetData();
                ValidateData();
                
                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlkmbInsert");

                try
                {
                    //goi base insert bảng chính
                    var amSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _TableStruct, DataDic);
                    var ok = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;

                    //nếu ok
                    //insert từng dòng bảng chi tiết
                    if (ok)
                    {
                        var adList = AD.ToListDataDictionary(txtSttRec.Text);
                        foreach (SortedDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, V6TableHelper.ToV6TableName(_table2Name), row);
                        }

                        if (ok)
                        {
                            TRANSACTION.Commit();
                            return true;
                        }
                    }

                    TRANSACTION.Rollback();
                    return false;
                    
                }
                catch (Exception ex)
                {
                    TRANSACTION.Rollback();
                    this.ShowErrorException(GetType() + ".InsertNew Rollback", ex);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".InsertNew", ex);
            }
            return false;
        }

        public override int UpdateData()
        {
            try
            {
                DataDic = GetData();
                ValidateData();
                
                var keys = new SortedDictionary<string, object>
                {
                    {"STT_REC", DataDic["STT_REC"]}
                };

                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Alkmb Update");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keys);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, TableName.ToString(), _TableStruct, DataDic, keys);
                    var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                    var j = 0;

                    //Insert AD
                    var adList = AD.ToListDataDictionary(txtSttRec.Text);
                    foreach (SortedDictionary<string, object> adRow in adList)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table2Struct, adRow, false);
                        j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    if (insert_success && j == adList.Count)
                    {
                        TRANSACTION.Commit();
                        return 1;
                    }
                    else
                    {
                        TRANSACTION.Rollback();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    TRANSACTION.Rollback();
                    this.ShowErrorException(GetType() + ".UpdateData Rollback", ex);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".UpdateData", ex);
            }
            return -1;
        }


        public override void DoBeforeEdit()
        {
            try
            {
                
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            
            if (txtMaKm.Text.Trim() == "") errors += "Chưa nhập mã khuyến mãi!\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_KM",
                 txtMaKm.Text.Trim(), DataOld["MA_KM"].ToString());
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_KM = " + txtMaKm.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_KM",
                 txtMaKm.Text.Trim(), txtMaKm.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_KM = " + txtMaKm.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += "Chưa hoàn tất chi tiết!\r\n";
            }

            if (errors.Length > 0) throw new Exception(errors);

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_DVCS", "MA_KM", "MA_CT" }, AD);
            V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL", dateNgayHL.Value);
            V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL2", dateNgayHL2.Value);
        }

        #region ==== Detail control events ====

        private void XuLyChonVatTu()
        {
            try
            {
                txtChonMavt.Lookup(true);
                //xyly
                if (txtChonMavt.Text != "")
                {
                    var where = "Ma_vt='" + txtChonMavt.Text.Replace(",", "' or Ma_vt='") + "'";
                    var dataTable = V6BusinessHelper.Select("Alvt", "MA_VT,TEN_VT,DVT", where).Data;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var DATA = row.ToDataDictionary();

                        XuLyThemDetail(DATA);
                    }
                }
                txtChonMavt.Text = "";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyChonVatTu", ex);
            }
        }
        
        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                _ma_vt.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyDetailClickAdd", ex);
            }
        }

        private bool XuLyThemDetail(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = txtSttRec.Text;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\nMã vật tư rỗng.";
                
                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = AD.NewRow();
                    foreach (DataColumn column in AD.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(key) ? data[key] : "") ?? DBNull.Value;
                        newRow[key] = value;
                    }
                    //Them du lieu chung
                    AD.Rows.Add(newRow);
                    dataGridView1.DataSource = AD;
                    //TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);

                    if (AD.Rows.Count > 0)
                    {
                        dataGridView1.Rows[AD.Rows.Count - 1].Selected = true;
                        dataGridView1.CurrentCell
                            = dataGridView1.Rows[AD.Rows.Count - 1].Cells["Ma_vt"];
                    }
                }
                else
                {
                    this.ShowWarningMessage("Kiểm tra lại dữ liệu:" + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Thêm chi tiết", ex);
            }
            return true;
        }

        
        private bool XuLySuaDetail(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv1EditingRow != null)
                {
                    var cIndex = _gv1EditingRow.Index;

                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        //Thêm thắt vài thứ
                        //data["MA_CT"] = Invoice.Mact;
                        //data["NGAY_CT"] = dateNgayCT.Value.Date;


                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                            error += "\nMã vật tư rỗng.";
                        
                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = AD.Rows[cIndex];
                            foreach (DataColumn column in AD.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            dataGridView1.DataSource = AD;
                            //TinhTongThanhToan("xy ly sua detail");
                        }
                        else
                        {
                            this.ShowWarningMessage("Kiểm tra lại dữ liệu:" + error);
                            return false;
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn một dòng.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Sửa chi tiết", ex);
            }
            return true;
        }

        private void XuLyXoaDetail()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    var cIndex = dataGridView1.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        var currentRow = AD.Rows[cIndex];
                        var details = "Mã vật tư: " + currentRow["Ma_vt"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            AD.Rows.Remove(currentRow);
                            dataGridView1.DataSource = AD;
                            detail1.SetData(null);
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Hãy chọn 1 dòng!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Xóa chi tiết", ex);
            }
        }


        private bool ValidateData_Detail(SortedDictionary<string, object> data)
        {
            try
            {
                if (data == null) return false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail", ex);
            }
            return true;
        }

        #endregion details
        
        private void detail1_ClickAdd(object sender)
        {
            XuLyDetailClickAdd();
        }

        private void detail1_AddHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail(data) && XuLyThemDetail(data))
            {
                return;
            }
            throw new Exception("Add failed.");
        }

        private void detail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);
                    GetThongTinVt();
                    _ma_vt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ClickEdit", ex);
            }
        }

        private void detail1_ClickCancelEdit(object sender)
        {
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

        private void detail1_DeleteHandle(object sender)
        {
            XuLyXoaDetail();
        }

        private void detail1_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail(data) && XuLySuaDetail(data))
            {
                return;
            }
            throw new Exception("Edit failed.");
        }
        
        private void SoDu2AddEditControl0_Load(object sender, EventArgs e)
        {
            SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            txtMaDvcs.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.GetCurrentRowData());
                GetThongTinVt();
            }
        }
        
        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                detail1.AutoFocus();
            }
        }

        private void btnChonMaVt_Click(object sender, EventArgs e)
        {
            XuLyChonVatTu();
        }

    }
}
