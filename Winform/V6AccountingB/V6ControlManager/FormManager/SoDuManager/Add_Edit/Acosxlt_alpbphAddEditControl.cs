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
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class Acosxlt_alpbphAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private string _sttRec0 = "";

        public Acosxlt_alpbphAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            _maCt = "S12";
            _table2Name = "acosxlt_alpbct";
            txtMaCt.Text = _maCt;

            
            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);

            LoadDetailControls();

            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_SP";
            
        }

        public override void DoBeforeAdd()
        {
            try
            {
                dateNgayHL.Value = DateTime.Now.Date;
                txtSttRec.Text = V6BusinessHelper.GetNewSttRec(_maCt);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeAdd", ex);
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                //SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
                //LoadDetails(); // Auto call
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }
        
        public override void DoBeforeView()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeView", ex);
            }
        }


        private V6VvarTextBox _ma_sp;
        private V6NumberTextBox _heso;
        private V6DateTimeColor _ngayKetThuc, _ngayBatDau;
        private V6ColorTextBox _ten_sp, _dvt;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có
            
            _ma_sp = new V6VvarTextBox
            {
                AccessibleName = "ma_sp",
                VVar = "MA_VT",
                GrayText = "Mã sản phẩm",
                BrotherFields = "TEN_SP",
                NeighborFields = "TEN_SP0"
            };
            _ma_sp.Upper();
            _ma_sp.SetInitFilter("Loai_vt='55'");
            _ma_sp.FilterStart = true;
            _ma_sp.V6LostFocus += delegate
            {
                GetThongTinVt();
            };

            _ten_sp = new V6ColorTextBox
            {
                AccessibleName = "TEN_SP0",
                GrayText = "Tên sản phẩm",
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

            _heso = new NumberSoluong
            {
                AccessibleName = "he_so",
                GrayText = "Hệ số "
            };
            
            _ngayBatDau = new V6DateTimeColor
            {
                AccessibleName = "ngay_hl1",
                GrayText = "Ngày bắt đầu",
                Visible = false,
                Tag = "hide"
            };
            _ngayKetThuc = new V6DateTimeColor
            {
                AccessibleName = "ngay_hl2",
                GrayText = "Ngày kết thúc",
                Visible = false,
                Tag = "hide"
            };
            
            var dynamicControlList = new SortedDictionary<int, Control>();
            
            dynamicControlList.Add(0, _ma_sp);
            dynamicControlList.Add(1, _ten_sp);
            dynamicControlList.Add(2, _dvt);
            dynamicControlList.Add(3, _heso);
            dynamicControlList.Add(4, _ngayBatDau);
            dynamicControlList.Add(5, _ngayKetThuc);
            
            
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
            if (_ma_sp.Data != null)
            {
                _ten_sp.Text = _ma_sp.Data["Ten_vt"].ToString().Trim();
                _dvt.Text = _ma_sp.Data["Dvt"].ToString().Trim();
            }
            else
            {
                _ten_sp.Clear();
                _dvt.Clear();
            }
        }

        public override void LoadDetails()
        {
            try
            {
                string sttRec = DataOld == null ? "" : DataOld["STT_REC"].ToString();
                string sql = "SELECT a.*,b.ten_vt as ten_sp FROM " + _table2Name +
                             " as a left join alvt b on a.ma_sp=b.ma_vt  Where stt_rec = @rec";
                SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
                AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

                SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public override bool InsertNew()
        {
            try
            {
                DataDic = GetData();
                ValidateData();

                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("acosxlt_alpbphInsert");

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
                    this.ShowErrorMessage(GetType() + ".acosxlt_alpbph InsertData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".acosxlt_alpbph InsertNew: " + ex.Message);
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

                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("acosxlt_alpbph Update");

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
                    this.ShowErrorMessage(GetType() + ".acosxlt_alpbph UpdateData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".acosxlt_alpbph UpdateData: " + ex.Message);
            }
            return -1;
        }




        public override void ValidateData()
        {
            var errors = "";
            if (txtMaYtcp.Text.Trim() == "") errors += "Chưa nhập mã YTCP!\r\n";
            if (txtMaBpht.Text.Trim() == "") errors += "Chưa nhập mã!\r\n";
            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 0, "MA_YTCP",
                 txtMaYtcp.Text.Trim(), DataOld["MA_YTCP"].ToString(),
                 "MA_BPHT",
                 txtMaBpht.Text.Trim(), DataOld["MA_BPHT"].ToString()
                 );
                if (!b)
                    throw new Exception("Không được sửa mã đã tồn tại: "
                                                    + "MA_YTCP = " + txtMaYtcp.Text.Trim() + ",MA_BPHT = " + txtMaBpht.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidTwoCode_Full(TableName.ToString(), 1, "MA_YTCP",
                 txtMaYtcp.Text.Trim(), txtMaYtcp.Text.Trim(),
                 "MA_BPHT",
                 txtMaBpht.Text.Trim(), txtMaBpht.Text.Trim()
                 );
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_YTCP = " + txtMaYtcp.Text.Trim() + ",MA_BPHT = " + txtMaBpht.Text.Trim());
            }
            

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += "Chưa hoàn tất chi tiết!\r\n";
            }

            if (errors.Length > 0) throw new Exception(errors);

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_YTCP", "MA_BPHT", "MA_CT" }, AD);
            V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL1", dateNgayHL.Value);
            //V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL2", dateNgayHL2.Value);
        }

        #region ==== Detail control events ====

        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                _ma_sp.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "") error += "\nMã SP rỗng.";
                
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
                            = dataGridView1.Rows[AD.Rows.Count - 1].Cells["MA_SP"];
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckData + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "")
                            error += "\nMã SP rỗng.";
                        
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
                            this.ShowWarningMessage(V6Text.CheckData + error);
                            return false;
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
                        var details = "Mã sản phẩm: " + currentRow["Ma_sp"];
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
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
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
                    _ma_sp.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
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
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }
        
        private void SoDu2AddEditControl0_Load(object sender, EventArgs e)
        {
            SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            txtMaBpht.Focus();
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


    }
}
