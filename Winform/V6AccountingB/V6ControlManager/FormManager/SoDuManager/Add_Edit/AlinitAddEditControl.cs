﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit.PhanQuyen;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AlinitAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private string _sttRec0 = "";

        public AlinitAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            Mact = "S06";
            _table2Name = "Alinit1";
            txtMaCt.Text = Mact;

            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            
            LoadDetailControls();

            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_VT";
            
        }

        public override void DoBeforeAdd()
        {
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(Mact);
        }


        private V6VvarTextBox _user_name;
        //private V6NumberTextBox _t_sl1, _gia2, _t_sl2,_gia_km,_sl_km;
        private V6ColorTextBox _default1V, _default1E, _default11,_default12,_default13,_default14;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có

            _user_name = new V6VvarTextBox
            {
                AccessibleName = "user_name",
                VVar = "USER_NAME",
                GrayText = V6Text.Text("TENDANGNHAP"),
                BrotherFields = "Comment"
            };
            _user_name.Upper();
            _user_name.FilterStart = true;
            
           _default1V = new V6ColorTextBox()
            {
                AccessibleName = "default1V",
                GrayText = "Giá trị 1 Việt"
            };
            _default1V.Width = 200;
            _default1E = new V6ColorTextBox()
            {
                AccessibleName = "default1E",
                GrayText = "Giá trị 1 English"
            };
            _default1E.Width = 200;

            _default11 = new V6ColorTextBox()
            {
                AccessibleName = "default11",
                GrayText = "Giá trị 11"
            };
            _default11.Width = 100;
            _default12 = new V6ColorTextBox()
            {
                AccessibleName = "default12",
                GrayText = "Giá trị 12",
                Width = 100
            };
            _default13 = new V6ColorTextBox()
            {
                AccessibleName = "default13",
                GrayText = "Giá trị 13",
                Width = 100
            };
            _default14 = new V6ColorTextBox()
            {
                AccessibleName = "default14",
                GrayText = "Giá trị 14",
                Width = 100
            };

            dynamicControlList1 = new SortedDictionary<int, Control>();
            
            dynamicControlList1.Add(0, _user_name);
            dynamicControlList1.Add(6, _default1V);
            dynamicControlList1.Add(7, _default1E);
            dynamicControlList1.Add(8, _default11);
            dynamicControlList1.Add(9, _default12);
            dynamicControlList1.Add(10, _default13);
            dynamicControlList1.Add(11, _default14);
            
            foreach (KeyValuePair<int, Control> item in dynamicControlList1)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);
            }
            
            //Add detail controls
            foreach (Control control in dynamicControlList1.Values)
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
                string sttRec = DataOld != null && DataOld.ContainsKey("STT_REC") ? DataOld["STT_REC"].ToString() : "";
                string sql = "SELECT a.*,b.comment as comment FROM " + _table2Name +
                             " as a left join v6user b on a.user_name=b.user_name  Where stt_rec = @rec";
                SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
                AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadDetails: " + ex.Message, "Alinit");
            }
        }


        private readonly string[] update_dklist_fields = { "MA_CT", "USER_NAME", "LOAI", "KIEU", "NHOM", "MA_DM", "NAMETAG", "NAMEVAL" };

        public override bool InsertNew()
        {
            try
            {
                ValidateData();

                DataDic = GetData();
                V6ControlFormHelper.UpdateDKlistAll(DataDic, update_dklist_fields, AD);
                
                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlinitInsert");

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
                        foreach (IDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, V6TableHelper.ToV6TableName(_table2Name), row);
                        }

                        if (ok)
                        {
                            TRANSACTION.Commit();
                            return true;
                        }
                    }

                fail:
                    TRANSACTION.Rollback();
                    return false;
                    
                }
                catch (Exception ex)
                {
                    TRANSACTION.Rollback();
                    this.ShowErrorMessage(GetType() + ".Alinit InsertData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Alinit InsertNew: " + ex.Message);
            }
            return false;
        }

        public override int UpdateData()
        {
            try
            {
                ValidateData();
                DataDic = GetData();
                V6ControlFormHelper.UpdateDKlistAll(DataDic, update_dklist_fields, AD);

                var keys = new SortedDictionary<string, object>
                {
                    {"STT_REC", DataDic["STT_REC"]}
                };


                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlinitUpdate");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keys);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, _MA_DM, DataDic, keys, _TableStruct);
                    var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                    var j = 0;

                    //Insert AD
                    var adList = AD.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList)
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
                    this.ShowErrorMessage(GetType() + ".Alinit UpdateData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Alinit UpdateData: " + ex.Message);
            }
            return -1;
        }


        public override void DoBeforeEdit()
        {
            try
            {
                if (txtLoai.Text != "1")
                {
                    txtMaCtMe.Visible = false;
                    lblMaCtMe.Visible = false;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void ValidateData()
        {
            var errors = "";
            if (txtLoai.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblLoai.Text + "!\r\n";
            if (txtNhom.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblNhom.Text + "!\r\n";
            
            if (Mode == V6Mode.Edit)
            {
                //bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_THAU",
                // txtLoai.Text.Trim(), DataOld["MA_THAU"].ToString());
                //if (!b)
                //    throw new Exception(V6Text.Exist + V6Text.EditDenied
                //                                    + lblLoai.Text + " = " + txtLoai.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                //bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_THAU",
                // txtLoai.Text.Trim(), txtLoai.Text.Trim());
                //if (!b)
                //    throw new Exception(V6Text.Exist + V6Text.EditDenied
                //                                    + lblLoai.Text + " = " + txtLoai.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += V6Text.DetailNotComplete;
            }

            if (errors.Length > 0) throw new Exception(errors);

           
        }

        #region ==== Detail control events ====
        
        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                _user_name.Focus();
                _default1V.Text = txtDefaultV.Text;
                _default1E.Text = txtDefaultE.Text;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public override bool XuLyThemDetail(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("USER_NAME") || data["USER_NAME"].ToString().Trim() == "")
                    error += "\n" + V6Text.Text("USER_NAME") + " " + V6Text.Empty;
                if (_user_name.Data == null)
                    error += "\n" + V6Text.Text("USERDATANULL");
                
                if (error == "" && _user_name.Data != null)
                {
                    UpdateDetailChangeLog(_sttRec0, dynamicControlList1, null, data);
                    //Gom đầy đủ dữ liệu
                    _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                    data["STT_REC0"] = _sttRec0;
                    data["STT_REC"] = txtSttRec.Text;
                    data["MA_CT"] = txtMaCt.Text;
                    data["MA_CT_ME"] = txtMaCtMe.Text;
                    data["LOAI"] = txtLoai.Text;
                    data["NHOM"] = txtNhom.Text;
                    data["MA_DM"] = txtMaDm.Text;
                    data["DEFAULTV"] = txtDefaultV.Text;
                    data["DEFAULTE"] = txtDefaultE.Text;

                    data["USER_ID"] = _user_name.Data["user_id"];
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
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "User_name");
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
                return false;
            }
            return true;
        }
        
        private bool XuLySuaDetail(IDictionary<string, object> data)
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
                        if (!data.ContainsKey("USER_NAME") || data["USER_NAME"].ToString().Trim() == "")
                            error += "\nMã người dùng rỗng.";
                        
                        if (error == "")
                        {
                            var currentRow = AD.Rows[cIndex];
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, dynamicControlList1, currentRow.ToDataDictionary(), data);
                            //_sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                            //data["STT_REC0"] = _sttRec0;
                            data["STT_REC"] = txtSttRec.Text;
                            data["MA_CT"] = txtMaCt.Text;
                            data["MA_CT_ME"] = txtMaCtMe.Text;
                            data["LOAI"] = txtLoai.Text;
                            data["NHOM"] = txtNhom.Text;
                            data["MA_DM"] = txtMaDm.Text;
                            data["DEFAULTV"] = txtDefaultV.Text;
                            data["DEFAULTE"] = txtDefaultE.Text;

                            data["USER_ID"] = _user_name.Data["user_id"];

                            //Sửa dòng dữ liệu.
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
                return false;
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
                        var details = "Mã: " + currentRow["User_name"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details) == DialogResult.Yes)
                        {
                            var delete_data = currentRow.ToDataDictionary();
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, dynamicControlList1, delete_data, null);
                            AD.Rows.Remove(currentRow);
                            dataGridView1.DataSource = AD;
                            detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
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


        private bool ValidateData_Detail(IDictionary<string, object> data)
        {
            try
            {
                
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
        
        private void detail1_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetailClickAdd();
            }
            else
            {
                dataGridView1.UnLock();
            }
        }

        private void detail1_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }

        private void detail1_ClickEdit(object sender, HD_Detail_Eventargs e)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);

                    _user_name.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void detail1_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            detail1.SetData(_gv1EditingRow.ToDataDictionary());
        }

        private void detail1_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail();
        }

        private void detail1_EditHandle(IDictionary<string, object> data)
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
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                detail1.AutoFocus();
            }
        }

        private void txtLoai_Leave(object sender, EventArgs e)
        {
            txtMaCtMe.Visible = txtLoai.Text == "1";
        }

        private void btnPhanQuyenUser_Click(object sender, EventArgs e)
        {
            try
            {
                using (PhanQuyenNSD phnQuyenF = new PhanQuyenNSD
                {
                    Text = "Phân quyền người sử dụng.",
                    Vrights_user = txtRightUser.Text
                })
                {
                    if (phnQuyenF.ShowDialog(this) == DialogResult.OK)
                    {
                        txtRightUser.Text = phnQuyenF.Vrights_user;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".PhanQuyenSnb: " + ex.Message);
            }
        }

        private void txtLoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        
        private void txtMaCtMe_VisibleChanged(object sender, EventArgs e)
        {
            if (!txtMaCtMe.Visible) txtMaCtMe.Text = "";
        }

    }
}
