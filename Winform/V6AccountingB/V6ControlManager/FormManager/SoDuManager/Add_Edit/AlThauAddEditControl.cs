using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class AlThauAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private string _sttRec0 = "";
        
        public AlThauAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            _maCt = "S01";
            _sttRec0 = "";
            _table2Name = "Althauct";
            txtMaCt.Text = _maCt;

            //TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");
            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            
            LoadDetailControls();

            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_VT";
        }

        public override void DoBeforeAdd()
        {
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(_maCt);
        }

        public override void DoBeforeEdit()
        {
            try
            {
                if (!DataOld.ContainsKey("MA_THAU")) txtMaThau.Enabled = true;//vidu
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        private V6VvarTextBox _ma_vt;
        private V6NumberTextBox _t_sl1, _gia2, _t_sl2,_gia_km,_sl_km;
        private V6ColorTextBox _Ghi_chukm, _Ghi_chuck ;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có

            _ma_vt = new V6VvarTextBox
            {
                AccessibleName = "ma_vt",
                VVar = "ma_vt",
                GrayText = "Mã vật tư",
                BrotherFields = "TEN_VT"
            };
            _ma_vt.Upper();
            _ma_vt.FilterStart = true;
            

            _t_sl1 = new NumberSoluong()
            {
                AccessibleName = "t_sl1",
                GrayText = "Số lượng trúng thầu "
            };
            _t_sl2 = new NumberSoluong()
            {
                AccessibleName = "t_sl2",
                GrayText = "Số lượng đã xuất "
            };
            _gia2 = new NumberGia()
            {
                AccessibleName = "gia2",
                GrayText = "Đơn giá "
            };
            _gia_km = new NumberGia()
            {
                AccessibleName = "gia_km",
                GrayText = "Đơn giá dự thầu "
            };
            _sl_km = new NumberSoluong()
            {
                AccessibleName = "sl_km",
                GrayText = "Số lượng dự thầu "
            };
            _Ghi_chukm = new V6ColorTextBox
            {
                AccessibleName = "ghi_chukm",
                GrayText = "Ghi chú dự thầu",
                Width = 200
            };
            _Ghi_chuck = new V6ColorTextBox
            {
                AccessibleName = "ghi_chuck",
                GrayText = "Ghi chú trúng thầu",
                Width = 200
            };


            var dynamicControlList = new SortedDictionary<int, Control>();
            //t_sl1, t_sl2, t_tien1, t_tien2, sl_km, tien_km, Ghi_chukm, Ghi_chuck, T_SLKM;
            dynamicControlList.Add(0, _ma_vt);
            dynamicControlList.Add(1, _sl_km);
            dynamicControlList.Add(2, _gia_km);
            dynamicControlList.Add(3, _t_sl1);
            dynamicControlList.Add(4, _t_sl2);
            dynamicControlList.Add(5, _gia2);
            dynamicControlList.Add(6, _Ghi_chukm);
            dynamicControlList.Add(7, _Ghi_chuck);
            
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

        public override void LoadDetails()
        {
            try
            {
                //Load AD
                var sttRec = DataOld == null ? "" : DataOld["STT_REC"].ToString();
                var sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
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
                
                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlthauInsert");

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


                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlthauUpdate");

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


        public override void ValidateData()
        {
            var errors = "";
            if (txtMaThau.Text.Trim() == "") errors += "Chưa nhập mã!\r\n";
            if (txtTenThau.Text.Trim() == "") errors += "Chưa nhập tên!\r\n";
            if (txtMaKh.Text.Trim() == "") errors += "Chưa nhập mã khách!\r\n";
            if (txtMaDVCS.Text.Trim() == "") errors += "Chưa nhập đơn vị!\r\n";

            if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_THAU",
                 txtMaThau.Text.Trim(), DataOld["MA_THAU"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_THAU = " + txtMaThau.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_THAU",
                 txtMaThau.Text.Trim(), txtMaThau.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "MA_THAU = " + txtMaThau.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += "Chưa hoàn tất chi tiết!\r\n";
            }

            if (errors.Length > 0) throw new Exception(errors);

            V6ControlFormHelper.UpdateDKlistAll(DataDic,
                     new[] { "MA_THAU", "MA_THAU0", "MA_DVCS", "MA_KH", "NGAY_HL", "NGAY_HL2", "MA_CT" }, AD);
        }

        #region ==== Detail control events ====
        
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
                //Thêm thắt vài thứ
                //data["MA_CT"] = Invoice.Mact;
                //data["NGAY_CT"] = dateNgayCT.Value.Date;

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
                    this.ShowWarningMessage(V6Text.CheckData + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XulyThemDetail", ex);
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
                this.ShowErrorException(GetType() + ".XulySuaDetail", ex);
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
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyXoaDetail", ex);
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

        private void TxtDu_co00_V6LostFocus(object sender)
        {
            
        }
        
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
            throw new Exception(V6Text.AddFail);
        }

        private void detail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    _sttRec0 = ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow);

                    _ma_vt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".detail1_ClickEdit", ex);
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
            throw new Exception(V6Text.EditFail);
        }
        
        private void SoDu2AddEditControl0_Load(object sender, EventArgs e)
        {
            SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
                detail1.SetData(dataGridView1.GetCurrentRowData());
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
