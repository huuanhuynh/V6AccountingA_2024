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
using V6Controls.Structs;
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
            Mact = "S01";
            _sttRec0 = "";
            _table2Name = "Althauct";
            txtMaCt.Text = Mact;

            //TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");
            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            
            //LoadDetailControls();
            //detail1.MODE = V6Mode.View;
            //detail1.lblName.AccessibleName = "TEN_VT";
            
            
        }

        private void SoDu2AddEditControl0_Load(object sender, EventArgs e)
        {
            LoadDetailControls();
            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_VT";
            SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
        }

        public override void DoBeforeAdd()
        {
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(Mact);
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

        private V6VvarTextBox _ma_vt, _dvt;
        private V6NumberTextBox _t_sl1, _gia2, _t_sl2, _gia_km, _sl_km, _tien_km;
        private V6ColorTextBox _Ghi_chukm, _Ghi_chuck ;

        private void LoadDetailControls()
        {
            //Tạo trước control phòng khi alct1 không có

            //_ma_vt = new V6VvarTextBox
            //{
            //    AccessibleName = "ma_vt",
            //    VVar = "ma_vt",
            //    GrayText = "Mã vật tư",
            //    BrotherFields = "TEN_VT"
            //};
            //_ma_vt.V6LostFocus += delegate(object sender)
            //{
            //    XuLyDonViTinhKhiChonMaVt(_ma_vt.Text);
            //};
            //_ma_vt.Upper();
            //_ma_vt.FilterStart = true;

            //_dvt = new V6VvarTextBox
            //{
            //    AccessibleName = "dvt",
            //    VVar = "dvt1",
            //    GrayText = "Đơn vị tính",
            //    CheckNotEmpty = true,
            //    CheckOnLeave = true,
            //};
            //_dvt.GotFocus += (s, e) =>
            //{
            //    _dvt.SetInitFilter("ma_vt='" + _ma_vt.Text.Trim() + "'");
            //    _dvt.ExistRowInTable(true);
            //};
            //_dvt.Upper();

            //_t_sl1 = new NumberSoluong()
            //{
            //    AccessibleName = "t_sl1",
            //    GrayText = V6Text.Text("SLTRUNGTHAU")
            //};
            //_t_sl2 = new NumberSoluong()
            //{
            //    AccessibleName = "t_sl2",
            //    GrayText = V6Text.Text("SLDAXUAT")
            //};
            //_gia2 = new NumberGia()
            //{
            //    AccessibleName = "gia2",
            //    GrayText = V6Text.Text("DONGIA")
            //};
            //_gia_km = new NumberGia()
            //{
            //    AccessibleName = "gia_km",
            //    GrayText = V6Text.Text("DONGIATHAU")
            //};
            //_sl_km = new NumberSoluong()
            //{
            //    AccessibleName = "sl_km",
            //    GrayText = V6Text.Text("SLDUTHAU")
            //};
            //_Ghi_chukm = new V6ColorTextBox
            //{
            //    AccessibleName = "ghi_chukm",
            //    GrayText = V6Text.Text("GCDUTHAU"),
            //    Width = 200
            //};
            //_Ghi_chuck = new V6ColorTextBox
            //{
            //    AccessibleName = "ghi_chuck",
            //    GrayText = V6Text.Text("GCTRUNGTHAU"),
            //    Width = 200
            //};


            var dynamicControlList0 = new SortedDictionary<int, Control>();
            //t_sl1, t_sl2, t_tien1, t_tien2, sl_km, tien_km, Ghi_chukm, Ghi_chuck, T_SLKM;
            int stt = 0;
            dynamicControlList0.Add(stt++, _ma_vt);
            dynamicControlList0.Add(stt++, _dvt);
            dynamicControlList0.Add(stt++, _sl_km);
            dynamicControlList0.Add(stt++, _gia_km);
            dynamicControlList0.Add(stt++, _t_sl1);
            dynamicControlList0.Add(stt++, _t_sl2);
            dynamicControlList0.Add(stt++, _gia2);
            dynamicControlList0.Add(stt++, _Ghi_chukm);
            dynamicControlList0.Add(stt++, _Ghi_chuck);

            List<string> _orderList;
            SortedDictionary<string, DataRow> _alct1Dic;
            SortedDictionary<int, AlctControls> dynamicControlList_New = V6ControlFormHelper.GetDynamicControlStructsAlct(Alct1Data, out _orderList, out _alct1Dic);
            
            foreach (KeyValuePair<int, AlctControls> item in dynamicControlList_New)
            {
                var control = item.Value.DetailControl;

                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects);
                
                switch (NAME)
                {
                    case "MA_VT":
                        _ma_vt = (V6VvarTextBox) control;
                        _ma_vt.V6LostFocus += delegate(object sender)
                        {
                            XuLyDonViTinhKhiChonMaVt(_ma_vt.Text);
                        };
                        _ma_vt.Upper();
                        //_ma_vt.FilterStart = true; // Đã có trong config alct1???
                        break;
                    case "DVT":
                        _dvt = (V6VvarTextBox) control;
                        _dvt.GotFocus += (s, e) =>
                        {
                            if (detail1.IsAddOrEdit)
                            {
                                _dvt.SetInitFilter("ma_vt='" + _ma_vt.Text.Trim() + "'");
                                _dvt.ExistRowInTable(true);
                            }
                        };
                        _dvt.Upper();
                        break;
                    case "T_SL1":
                        _t_sl1 = (V6NumberTextBox) control;
                        _t_sl1.V6LostFocus += sender =>
                        {
                            _tien_km.Value = V6BusinessHelper.Vround(_t_sl1.Value * _gia2.Value, V6Options.M_ROUND);
                        };
                        break;
                    case "T_SL2":
                        _t_sl2 = (V6NumberTextBox) control;
                        break;
                    case "GIA2":
                        _gia2 = (V6NumberTextBox) control;
                        _gia2.V6LostFocus += sender =>
                        {
                            _tien_km.Value = V6BusinessHelper.Vround(_t_sl1.Value * _gia2.Value, V6Options.M_ROUND);
                        };
                        break;
                    case "GIA_KM":
                        _gia_km = (V6NumberTextBox) control;
                        break;
                    case "SL_KM":
                        _sl_km = (V6NumberTextBox) control;
                        break;
                    case "TIEN_KM":
                        _tien_km = (V6NumberTextBox) control;
                        break;
                    case "GHI_CHUKM":
                        _Ghi_chukm = (V6ColorTextBox) control;
                        break;
                    case "GHI_CHUCK":
                        _Ghi_chuck = (V6ColorTextBox) control;
                        break;
                        //TIEN_KM = T_SL1*GIA2 M_ROUND0
                    
                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "2");
            }
            
            //Add detail controls
            foreach (AlctControls control in dynamicControlList_New.Values)
            {
                detail1.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail1, _table2Struct);
        }

        
        private void XuLyDonViTinhKhiChonMaVt(string mavt, bool changeMavt = true)
        {
            try
            {
                //Gán lại dvt và dvt1
                var data = _ma_vt.Data;
                if (data == null)
                {
                    _dvt.ChangeText("");
                    _dvt.SetInitFilter("");
                    _dvt.ChangeText("");
                    return;
                }

                if (changeMavt)
                {
                    _dvt.Text = data["dvt"].ToString().Trim();
                    _dvt.SetInitFilter("ma_vt='" + mavt + "'");
                    _dvt.Text = _dvt.Text;
                    _dvt.ExistRowInTable(true);
                }

                if (data.Table.Columns.Contains("Nhieu_dvt"))
                {
                    var nhieuDvt = data["Nhieu_dvt"].ToString().Trim();
                    if (nhieuDvt == "1")
                    {
                        _dvt.Tag = null;
                        _dvt.ReadOnly = false;
                        //if (changeMavt) _heSo1.Value = 1;

                    }
                    else
                    {
                        _dvt.Tag = "readonly";
                        _dvt.ReadOnly = true;
                        if (changeMavt) _dvt.Focus();
                        //if (changeMavt) _heSo1.Value = 1;
                    }
                }
                else
                {
                    _dvt.ExistRowInTable(_dvt.Text);
                    _dvt.Tag = "readonly";
                    _dvt.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
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
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, TableName.ToString(), DataDic, keys, _TableStruct);
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


        public override void ValidateData()
        {
            var errors = "";
            if (txtMaThau.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblMaThau.Text + "!\r\n";
            if (txtTenThau.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblTenThau.Text + "!\r\n";
            if (txtMaKh.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblMaKH.Text + "!\r\n";
            if (txtMaDVCS.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblMaDVCS.Text + "!\r\n";

            //if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
            //    errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "MA_THAU",
                 txtMaThau.Text.Trim(), DataOld["MA_THAU"].ToString());
                if (!b)
                    throw new Exception(V6Text.Exist + V6Text.EditDenied
                                                    + lblMaThau.Text + " = " + txtMaThau.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "MA_THAU",
                 txtMaThau.Text.Trim(), txtMaThau.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.Exist + V6Text.AddDenied
                                                    + lblMaThau.Text + " = " + txtMaThau.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
            {
                errors += V6Text.DetailNotComplete;
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

        private bool XuLyThemDetail(IDictionary<string, object> data)
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
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                
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
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(dataGridView1, "Ma_vt");
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
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                        
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XulySuaDetail", ex);
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
                        var details = "Mã vật tư: " + currentRow["Ma_vt"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
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

        private void detail1_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLyThemDetail(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
            throw new Exception(V6Text.AddFail);
        }

        private void detail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && dataGridView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    ChungTu.ViewSelectedDetailToDetailForm(dataGridView1, detail1, out _gv1EditingRow, out _sttRec0);

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

        private void detail1_EditHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail(data))
            {
                if (XuLySuaDetail(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
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

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

    }
}
