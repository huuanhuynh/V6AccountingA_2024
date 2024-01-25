using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6Controls;
using V6Controls.Controls;
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
        private V6TableStruct _table3Struct;
        private string _sttRec0 = "";
        private string _sttRec022 = "";

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
            _table3Name = "Althauct2";
            txtMaCt.Text = Mact;

            //TxtTk.SetInitFilter("loai_tk=1 and tk_cn=1");
            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            _table3Struct = V6BusinessHelper.GetTableStruct(_table3Name);
            //LoadDetailControls();

        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDetailControls();
                LoadDetail2Controls();
                detail1.MODE = V6Mode.View;
                detail3.MODE = V6Mode.View;
                detail1.lblName.AccessibleName = "TEN_VT";
                detail3.lblName.AccessibleName = "TEN_VT";
                SetDataToGrid(dataGridView1, AD, txtMaCt.Text);

                LoadImageData();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Form_Load", ex);
            }
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

        public override void DoBeforeView()
        {
            try
            {
                
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
            List<string> _orderList;
            SortedDictionary<string, DataRow> _alct1Dic;
            Dictionary<string, AlctControls> dynamicControlList_New = V6ControlFormHelper.GetDynamicControlStructsAlct(Mact, Alct1Data, out _orderList, out _alct1Dic);
            
            foreach (KeyValuePair<string, AlctControls> item in dynamicControlList_New)
            {
                var control = item.Value.DetailControl;

                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                if (control is V6ColorTextBox && item.Value.IsCarry)
                {
                    detail1.CarryFields.Add(NAME);
                }
                
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects);

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
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "2");
            }
            
            //Add detail controls
            foreach (AlctControls control in dynamicControlList_New.Values)
            {
                detail1.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail1, _table2Struct);
        }


        private V6VvarTextBox _ma_vt22, _dvt22;
        private V6NumberTextBox _t_sl122, _gia222, _t_sl222, _gia_km22, _sl_km22, _tien_km22;
        private V6ColorTextBox _Ghi_chukm22, _Ghi_chuck22;
        private void LoadDetail2Controls()
        {
            List<string> _orderList2;
            SortedDictionary<string, DataRow> _alct2Dic;
            Dictionary<string, AlctControls> dynamicControlList3_New = V6ControlFormHelper.GetDynamicControlStructsAlct(Mact, Alct2Data, out _orderList2, out _alct2Dic);

            foreach (KeyValuePair<string, AlctControls> item in dynamicControlList3_New)
            {
                var control = item.Value.DetailControl;

                ApplyControlEnterStatus(control);

                var NAME = control.AccessibleName.ToUpper();
                All_Objects[NAME] = control;
                if (control is V6ColorTextBox && item.Value.IsCarry)
                {
                    detail1.CarryFields.Add(NAME);
                }

                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "_DETAIL3");

                switch (NAME)
                {
                    case "MA_VT":
                        _ma_vt22 = (V6VvarTextBox)control;
                        _ma_vt22.Upper();
                        _ma_vt22.FilterStart = true;
                        _ma_vt22.V6LostFocus += delegate
                        {
                            //GetThongTinVt22();
                        };
                        break;
                   
                    //==============
                    case "DVT":
                        _dvt22 = (V6VvarTextBox)control;
                        _dvt22.GotFocus += (s, e) =>
                        {
                            if (detail3.IsAddOrEdit)
                            {
                                _dvt22.SetInitFilter("ma_vt='" + _ma_vt22.Text.Trim() + "'");
                                _dvt22.ExistRowInTable(true);
                            }
                        };
                        _dvt22.Upper();
                        break;
                    case "T_SL1":
                        _t_sl122 = (V6NumberTextBox)control;
                        _t_sl122.V6LostFocus += sender =>
                        {
                            _tien_km22.Value = V6BusinessHelper.Vround(_t_sl122.Value * _gia222.Value, V6Options.M_ROUND);
                        };
                        break;
                    case "T_SL2":
                        _t_sl222 = (V6NumberTextBox)control;
                        break;
                    case "GIA2":
                        _gia222 = (V6NumberTextBox)control;
                        _gia222.V6LostFocus += sender =>
                        {
                            _tien_km22.Value = V6BusinessHelper.Vround(_t_sl122.Value * _gia222.Value, V6Options.M_ROUND);
                        };
                        break;
                    case "GIA_KM":
                        _gia_km22 = (V6NumberTextBox)control;
                        break;
                    case "SL_KM":
                        _sl_km22 = (V6NumberTextBox)control;
                        break;
                    case "TIEN_KM":
                        _tien_km22 = (V6NumberTextBox)control;
                        break;
                    case "GHI_CHUKM":
                        _Ghi_chukm22 = (V6ColorTextBox)control;
                        break;
                    case "GHI_CHUCK":
                        _Ghi_chuck22 = (V6ColorTextBox)control;
                        break;


                }
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Form_program, All_Objects, "2_DETAIL3");
            }

            //Add detail controls
            foreach (AlctControls control in dynamicControlList3_New.Values)
            {
                detail3.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail3, _table3Struct);
            
        }


        private void GetThongTinVt()
        {
            if (_ma_vt.Data != null)
            {
                //_ten_vt.Text = _ma_vt.Data["Ten_vt"].ToString().Trim();
                _dvt.Text = _ma_vt.Data["Dvt"].ToString().Trim();
            }
            else
            {
                //_ten_vt.Clear();
                _dvt.Clear();
            }
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
                {
                    var sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
                              " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
                    SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                    AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                    SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
                }
                //Data3
                {
                    string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table3Name +
                                 " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
                    SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                    data3 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                    SetDataToGrid(gView3, data3, txtMaCt.Text, _table3Name);
                }
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
                        if (!ok) goto fail;

                        adList = data3.ToListDataDictionary(txtSttRec.Text);
                        foreach (IDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table3Name, row);
                        }
                        if (!ok) goto fail;

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


                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("AlthauUpdate");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keys);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                    //Delete AD2
                    var deleteAd2Sql = SqlGenerator.GenDeleteSql(_table3Struct, keys);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd2Sql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, _MA_DM, _TableStruct, DataDic, keys);
                    var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                    int j = 0, k = 0;

                    //Insert AD
                    var adList = AD.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table2Struct, adRow, false);
                        j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }

                    //Insert AD2
                    var ad2List = data3.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in ad2List)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table3Struct, adRow, false);
                        k += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }


                    if (insert_success && j == adList.Count && k == ad2List.Count)
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


        private void LoadImageData()
        {
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
            keys.Add("MA_THAU", txtMaThau.Text);
            var data = Categories.Select("Althauct1", keys).Data;
            if (data != null && data.Rows.Count > 0)
            {
                var rowData = data.Rows[0].ToDataDictionary();
                SetSomeData(new SortedDictionary<string, object>()
                {
                    {"PHOTOGRAPH", rowData["PHOTOGRAPH"] },
                    {"SIGNATURE", rowData["SIGNATURE"] },
                    {"PDF1", rowData["PDF1"] },
                    {"PDF2", rowData["PDF2"] },
                    {"FILE_NAME1", rowData["FILE_NAME1"] },
                    {"FILE_NAME2", rowData["FILE_NAME2"] },
                });
            }
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
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_THAU",
                 txtMaThau.Text.Trim(), DataOld["MA_THAU"].ToString());
                if (!b)
                    throw new Exception(V6Text.Exist + V6Text.EditDenied
                                                    + lblMaThau.Text + " = " + txtMaThau.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_THAU",
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

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_THAU", "MA_THAU0", "MA_DVCS", "MA_KH", "NGAY_HL", "NGAY_HL2", "MA_CT" }, AD);
            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_THAU", "MA_THAU0", "MA_DVCS", "MA_KH", "NGAY_HL", "NGAY_HL2", "MA_CT" }, data3);
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
            throw new Exception(V6Text.AddFail);
        }

        private void detail1_ClickEdit(object sender, HD_Detail_Eventargs e)
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

        #endregion detail1


        #region ==== Detail3 control events ====

        private void XuLyDetail3ClickAdd()
        {
            try
            {
                SetDefaultDetail3();
                _ma_vt22.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyDetail3ClickAdd", ex);
            }
        }

        private void SetDefaultDetail3()
        {
            
        }

        public bool XuLyThemDetail3(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                _sttRec022 = V6BusinessHelper.GetNewSttRec0(data3);
                data["STT_REC0"] = _sttRec022;
                data["STT_REC"] = txtSttRec.Text;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n"
                        + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;

                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = data3.NewRow();
                    foreach (DataColumn column in data3.Columns)
                    {
                        var key = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(key) ? data[key] : "") ?? DBNull.Value;
                        newRow[key] = value;
                    }
                    //Them du lieu chung
                    data3.Rows.Add(newRow);
                    gView3.DataSource = data3;
                    //TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);

                    if (data3.Rows.Count > 0)
                    {
                        gView3.Rows[data3.Rows.Count - 1].Selected = true;
                        V6ControlFormHelper.SetGridviewCurrentCellToLastRow(gView3, "Ma_vt");
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
                this.ShowErrorException(GetType() + ".XulyThemDetail3", ex);
                return false;
            }
            return true;
        }
        

        private void tabThongTinKhac_Click(object sender, EventArgs e)
        {
            DoNothing();
        }

        private void btnChonhinh_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void btnChonHinhS_Click(object sender, EventArgs e)
        {
            ChonHinhS();
        }

        private void btnXoahinh_Click(object sender, EventArgs e)
        {
            XoaHinh();
        }

        private void btnXoaHinhS_Click(object sender, EventArgs e)
        {
            XoaHinhS();
        }


        private void ChonHinh()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                ptbPHOTOGRAPH.Image = chooseImage;

                var ma_thau_new = txtMaThau.Text.Trim();
                var ma_thau_old = Mode == V6Mode.Edit ? DataOld["MA_THAU"].ToString().Trim() : ma_thau_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALTHAUCT1",
                    new SqlParameter("@cMa_thau_old", ma_thau_old),
                    new SqlParameter("@cMa_thau_new", ma_thau_new));

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonHinh", ex);
            }
        }

        private void ChonHinhS()
        {
            try
            {
                var chooseImage = V6ControlFormHelper.ChooseImage(this);
                if (chooseImage == null) return;

                pictureBoxS.Image = chooseImage;

                var ma_thau_new = txtMaThau.Text.Trim();
                var ma_thau_old = Mode == V6Mode.Edit ? DataOld["MA_THAU"].ToString().Trim() : ma_thau_new;
                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALTHAUCT1",
                    new SqlParameter("@cMa_thau_old", ma_thau_old),
                    new SqlParameter("@cMa_thau_new", ma_thau_new));

                var photo = Picture.ToJpegByteArray(pictureBoxS.Image);
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { "SIGNATURE", photo } };//, {"SIGNATURE", sign}};
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonHinhS", ex);
            }
        }


        private void XoaHinh()
        {
            try
            {
                ptbPHOTOGRAPH.Image = null;

                var photo = Picture.ToJpegByteArray(ptbPHOTOGRAPH.Image);
                var data = new SortedDictionary<string, object> { { "PHOTOGRAPH", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + "PHOTOGRAPH");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".XoaHinh " + ex.Message);
            }
        }

        private void XoaHinhS()
        {
            try
            {
                pictureBoxS.Image = null;

                var photo = Picture.ToJpegByteArray(pictureBoxS.Image);
                var data = new SortedDictionary<string, object> { { "SIGNATURE", photo } };
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + "SIGNATURE");
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XoaHinhS", ex);
            }
        }

        private void btnChonPDF_Click(object sender, EventArgs e)
        {
            ChonPDF("PDF1", "PDF files|*.PDF");
        }

        private void btnChonPDF2_Click(object sender, EventArgs e)
        {
            ChonPDF("PDF2", "PDF files|*.PDF");
        }

        private void btnXoaPDF_Click(object sender, EventArgs e)
        {
            XoaPDF("PDF1");
        }

        private void btnXoaPDF2_Click(object sender, EventArgs e)
        {
            XoaPDF("PDF2");
        }

        private void btnXemPDF_Click(object sender, EventArgs e)
        {
            XemPDF("PDF1");
        }

        private void btnXemPDF2_Click(object sender, EventArgs e)
        {
            XemPDF("PDF2");
        }

        private void ChonPDF(string FIELD, string fileFilter)
        {
            try
            {
                var filePath = V6ControlFormHelper.ChooseOpenFile(this, fileFilter);
                if (filePath == null) return;

                //var photo = ;
                byte[] fileBytes = File.ReadAllBytes(filePath);
                //DataOld là chỗ chứa tạm.
                if (DataOld == null) DataOld = new SortedDictionary<string, object>();
                DataOld[FIELD] = fileBytes;
                //var sign = Picture.ToJpegByteArray(pictureBox2.Image);
                var data = new SortedDictionary<string, object> { { FIELD, fileBytes } };
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + FIELD);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChonPDF " + FIELD, ex);
            }
        }

        private void XoaPDF(string FIELD)
        {
            try
            {
                var data = new SortedDictionary<string, object> { { FIELD, null } };
                var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };
                var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                if (result == 1)
                {
                    ShowMainMessage(V6Text.Updated + FIELD);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XoaPDF " + FIELD, ex);
            }
        }

        private void XemPDF(string FIELD)
        {
            try
            {
                //DataOld là chỗ chứa tạm.
                if (DataOld != null && DataOld.ContainsKey(FIELD))
                {
                    V6ControlFormHelper.OpenFileBytes((byte[])DataOld[FIELD], "pdf");
                }
                else
                {
                    ShowMainMessage(V6Text.NoData);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XemPDF", ex);
            }
        }


        private void btnChonFile0_AfterProcess(object sender, FileButton.Event_Args e)
        {
            try
            {
                string FIELD = e.Sender.AccessibleName.Trim().ToUpper();
                if (e.Mode == FileButton.FileButtonMode.ChooseFile)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(e.Sender.FileName)) return;


                        var data = new SortedDictionary<string, object> { { FIELD, e.Sender.FileName } };
                        var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };

                        var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                        if (result == 1)
                        {
                            ShowMainMessage(V6Text.Updated + FIELD);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ChooseFile: " + ex.Message);
                    }
                }
                else if (e.Mode == FileButton.FileButtonMode.Clear)
                {
                    try
                    {
                        var data = new SortedDictionary<string, object> { { FIELD, null } };
                        var keys = new SortedDictionary<string, object> { { "MA_THAU", txtMaThau.Text } };
                        var result = V6BusinessHelper.UpdateTable("Althauct1", data, keys);

                        if (result == 1)
                        {
                            ShowMainMessage(V6Text.Updated + FIELD);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Clear:" + FIELD + ex.Message);
                    }
                }
                else if (e.Mode == FileButton.FileButtonMode.OpenFile)
                {
                    string openFile = e.OpenFile;
                    //File.Delete(openFile);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".btnChonFile_AfterProcess", ex);
            }
        }

        private void btnChonFile0_FileNameChanged(object sender, FileButton.Event_Args e)
        {
            toolTipV6FormControl.SetToolTip(e.Sender, e.NewFileName);
        }
        

        private bool XuLySuaDetail3(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv3EditingRow != null)
                {
                    var cIndex = _gv3EditingRow.Index;

                    if (cIndex >= 0 && cIndex < data3.Rows.Count)
                    {
                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "") error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;

                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = data3.Rows[cIndex];
                            foreach (DataColumn column in data3.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            gView3.DataSource = data3;
                            //TinhTongThanhToan("xy ly sua detail3");
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
                this.ShowErrorException(GetType() + ".XulySuaDetail3", ex);
                return false;
            }
            return true;
        }

        private void XuLyXoaDetail3()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (gView3.CurrentRow != null)
                {
                    var cIndex = gView3.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < data3.Rows.Count)
                    {
                        var currentRow = data3.Rows[cIndex];
                        var details = "Mã vật tư: " + currentRow["Ma_vt"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data3.Rows.Remove(currentRow);
                            gView3.DataSource = data3;
                            detail3.SetData(gView3.CurrentRow.ToDataDictionary());
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
                this.ShowErrorException(GetType() + ".XuLyXoaDetail3", ex);
            }
        }


        private bool ValidateData_Detail3(IDictionary<string, object> data)
        {
            try
            {
                if (data == null) return false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ValidateData_Detail3", ex);
            }
            return true;
        }

        private void detail3_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            if (e.Mode == V6Mode.Add)
            {
                XuLyDetail3ClickAdd();
            }
            else
            {
                dataGridView1.UnLock();
            }
        }

        private void detail3_AddHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data))
            {
                if (XuLyThemDetail3(data)) return;
                throw new Exception(V6Text.AddFail);
            }
            throw new Exception(V6Text.ValidateFail);
            throw new Exception(V6Text.AddFail);
        }

        private void detail3_ClickEdit(object sender, HD_Detail_Eventargs e)
        {
            try
            {
                if (data3 != null && data3.Rows.Count > 0 && gView3.DataSource != null)
                {
                    detail3.ChangeToEditMode();
                    ChungTu.ViewSelectedDetailToDetailForm(gView3, detail3, out _gv3EditingRow, out _sttRec022);

                    _ma_vt22.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".detail3_ClickEdit", ex);
            }
        }

        private void detail3_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            detail3.SetData(_gv3EditingRow.ToDataDictionary());
        }

        private void detail3_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail3();
        }

        private void detail3_EditHandle(IDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data))
            {
                if (XuLySuaDetail3(data)) return;
                throw new Exception(V6Text.EditFail);
            }
            throw new Exception(V6Text.ValidateFail);
        }

        #endregion detail3

        private void TxtDu_co00_V6LostFocus(object sender)
        {
            
        }
        
        
        
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
            }
        }

        private void gView3_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail3.IsViewOrLock)
            {
                detail3.SetData(gView3.CurrentRow.ToDataDictionary());
            }
        }

        private void tabControl1_Enter(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabChiTiet)
            {
                detail1.AutoFocus();
            }
            else if (tabControl1.SelectedTab == tabChiTiet2)
            {
                detail3.AutoFocus();
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

    }
}
