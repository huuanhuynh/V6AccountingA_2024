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
    public partial class AlpbAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private string _sttRec0 = "";

        public AlpbAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            Mact = "S04";
            _table2Name = "ALPB1";
            txtMaCt.Text = Mact;

            //TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");

            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            
            LoadDetailControls();

            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_TK";
        }

        public override void DoBeforeAdd()
        {
            TxtNam.Value = V6Setting.YearFilter;
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(Mact);
        }


        private V6VvarTextBox _ma_sp, _ma_bpht, _ma_phi, _ma_td, _ma_td2, _ma_td3, _ma_hd, _ma_ku,_tk,_ma_vv;
        private V6NumberTextBox _t_sl1, _gia2, _t_sl2;
        private V6ColorTextBox _Ghi_chukm, _Ghi_chuck ;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có

            _tk = new V6VvarTextBox
            {
                AccessibleName = "tk",
                VVar = "tk",
                GrayText = "Tk nợ",
                BrotherFields = "TEN_TK"
            };
            _tk.Upper();
            _tk.FilterStart = true;
            _tk.SetInitFilter("Loai_tk=1");
            _tk.CheckOnLeave = true;

            _ma_vv = new V6VvarTextBox
            {
                AccessibleName = "ma_vv",
                VVar = "ma_vv",
                GrayText = "Mã vụ việc"

            };
            _ma_vv.Upper();

            _ma_sp = new V6VvarTextBox
            {
                AccessibleName = "ma_sp",
                VVar = "MA_VT",
                GrayText = "Mã sản phẩm",
                BrotherFields = "TEN_VT"
            };
            _ma_sp.Upper();
            _ma_sp.SetInitFilter("Loai_vt='55'");
            
             _ma_bpht = new V6VvarTextBox
            {
                AccessibleName = "ma_bpht",
                VVar = "ma_bpht",
                GrayText = "Mã BPHT"

            };
            _ma_bpht.Upper();
            
            _ma_phi = new V6VvarTextBox
            {
                AccessibleName = "ma_phi",
                VVar = "ma_phi",
                GrayText = "Mã phí"
            };
            _ma_phi.Upper();

              _ma_td = new V6VvarTextBox
            {
                AccessibleName = "ma_td",
                VVar = "ma_td",
                GrayText = "Mã ĐN"
            };
            _ma_td.Upper();

             _ma_td2 = new V6VvarTextBox
            {
                AccessibleName = "ma_td2",
                VVar = "ma_td2",
                GrayText = "Mã ĐN2"
            };
            _ma_td2.Upper();

             _ma_td3 = new V6VvarTextBox
            {
                AccessibleName = "ma_td3",
                VVar = "ma_td3",
                GrayText = "Mã ĐN3"
            };
            _ma_td3.Upper();

              _ma_hd = new V6VvarTextBox
            {
                AccessibleName = "ma_hd",
                VVar = "ma_hd",
                GrayText = "Mã hợp đồng"
            };
            _ma_hd.Upper();


             _ma_ku = new V6VvarTextBox
            {
                AccessibleName = "ma_ku",
                VVar = "ma_ku",
                GrayText = "Mã khế ước"
            };
            _ma_ku.Upper();

          
            
            dynamicControlList1 = new SortedDictionary<int, Control>();
            //t_sl1, t_sl2, t_tien1, t_tien2, sl_km, tien_km, Ghi_chukm, Ghi_chuck, T_SLKM;
            dynamicControlList1.Add(0, _tk);
            dynamicControlList1.Add(1, _ma_vv);
            dynamicControlList1.Add(2, _ma_bpht);
            dynamicControlList1.Add(3, _ma_sp);
            dynamicControlList1.Add(4, _ma_phi);
            dynamicControlList1.Add(5, _ma_td);
            dynamicControlList1.Add(6, _ma_td2);
            dynamicControlList1.Add(7, _ma_td3);
            dynamicControlList1.Add(8, _ma_hd);
            dynamicControlList1.Add(9, _ma_ku);
            
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
                string sttRec = DataOld == null ? "" : DataOld["STT_REC"].ToString();
                string sql = "SELECT a.*,b.ten_tk as ten_tk FROM " + _table2Name +
                             " as a left join altk b on a.tk=b.tk  Where stt_rec = @rec";
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
                
                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlPBInsert");

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


                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("ALPBUPDATE");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keys);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, _MA_DM, _TableStruct, DataDic, keys);
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

            if (txtTen_bt.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblTenBT.Text + "!\r\n";

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += V6Text.DetailNotComplete;
            }

            AldmConfig config = ConfigManager.GetAldmConfig(_MA_DM);
            if (config != null && config.HaveInfo && !string.IsNullOrEmpty(config.KEY))
            {
                var key_list = ObjectAndString.SplitString(config.KEY);
                errors += CheckValid(_MA_DM, key_list);
            }

            if (errors.Length > 0) throw new Exception(errors);

            string s = txtTenLoai0.Text.Trim() + "/" + txtten_loai.Text.Trim();
            if (s!= "/") DataDic["TEN_LOAI"] = s;
            V6ControlFormHelper.UpdateDKlistAll(DataDic, new []{"MA_CT"}, AD);
        }

        #region ==== Detail control events ====
        
        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                _tk.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyDetailClickAdd", ex);
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
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = txtSttRec.Text;
                
                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("TK") || data["TK"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00379") + " " + V6Text.Empty;
                
                if (error == "")
                {
                    UpdateDetailChangeLog(_sttRec0, dynamicControlList1, null, data);
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
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "TK");
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
                this.ShowErrorException(GetType() + ".XuLyThemDetail", ex);
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
                        if (!data.ContainsKey("TK") || data["TK"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00379") + " " + V6Text.Empty;
                       
                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = AD.Rows[cIndex];
                            var c_sttRec0 = currentRow["STT_REC0"].ToString().Trim();
                            UpdateDetailChangeLog(c_sttRec0, dynamicControlList1, currentRow.ToDataDictionary(), data);
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
                this.ShowErrorException(GetType() + ".XuLySuaDetail", ex);
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
                        var details = V6Text.FieldCaption("TK") + " : " + currentRow["tk"];
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
                this.ShowErrorException(GetType() + ".XuLyXoaDetail", ex);
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

                    _tk.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".detail1_ClickEdit", ex);
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

        private void txtloai_PBCP_TextChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;

            string list_loai = ",04,A1,A3,A4,A5,";
            if (list_loai.Contains(","+txtloai_PBCP.Text.Trim()+","))
            {
                txttk_cp.Enabled = true;
                txtTk_dt.Enabled = true;
            }
            else
            {
                txttk_cp.Enabled = false;
                txtTk_dt.Enabled = false;
                txttk_cp.Text = "";
                txtTk_dt.Text = "";
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void txtloai_PBCP_V6LostFocus(object sender)
        {
            try
            {
                if (txtloai_PBCP.Data != null && txtloai_PBCP.Data["MA_TD1"].ToString().Trim() == "TK")
                {
                    txttk_cp.Enabled = true;
                    txtTk_dt.Enabled = true;
                }
                else
                {
                    txttk_cp.Enabled = false;
                    txtTk_dt.Enabled = false;
                    txttk_cp.Text = "";
                    txtTk_dt.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".detail1_ClickEdit", ex);
            }
        }

    }
}
