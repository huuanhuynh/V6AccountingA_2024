using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Editor;
using V6Controls.Forms.Viewer;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.SoDuManager.Add_Edit
{
    public partial class AlkmbAddEditControl : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct;
        private V6TableStruct _table3Struct;
        private V6TableStruct _table4Struct;
        private V6TableStruct _table5Struct;

        private string _sttRec0 = "";
        private string _sttRec0_33 = "";
        private string _sttRec0_44 = "";
        private string _sttRec0_55 = "";

        public AlkmbAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            Mact = "S0M";
            _table2Name = "Alkmbct";
            _table3Name = "Alkmbct2";
            _table4Name = "Alkmbct3";
            _table5Name = "Alkmbct4";

            txtLNH_KH1.SetInitFilter("loai_nh=1");
            txtLNH_KH2.SetInitFilter("loai_nh=2");
            txtLNH_KH3.SetInitFilter("loai_nh=3");
            txtLNH_KH4.SetInitFilter("loai_nh=4");
            txtLNH_KH5.SetInitFilter("loai_nh=5");
            txtLNH_KH6.SetInitFilter("loai_nh=6");
            txtLNH_KH9.SetInitFilter("loai_nh=9");

            try
            {
                txtMaCt.Text = Mact;

                //txt.SetInitFilter("");

                _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
                _table3Struct = V6BusinessHelper.GetTableStruct(_table3Name);
                _table4Struct = V6BusinessHelper.GetTableStruct(_table4Name);
                _table5Struct = V6BusinessHelper.GetTableStruct(_table5Name);

                LoadDetailControls();
                LoadDetail2Controls();

                detail1.MODE = V6Mode.View;
                detail1.lblName.AccessibleName = "";
                detail3.MODE = V6Mode.View;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
            this.toolTipV6FormControl.SetToolTip(this.btnChonKH, V6Text.Text("ShiftClickForDelete"));
            this.toolTipV6FormControl.SetToolTip(this.BtchonExcel, V6Text.Text("ShiftClickForDelete"));
            this.toolTipV6FormControl.SetToolTip(this.btnChonKH5, V6Text.Text("ShiftClickForDelete"));
            this.toolTipV6FormControl.SetToolTip(this.BtchonExcel5, V6Text.Text("ShiftClickForDelete"));
            this.toolTipV6FormControl.SetToolTip(this.gView5, V6Text.Text("F8DELETEAROW"));
        }

        public override void DoBeforeAdd()
        {
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(Mact);
        }


        private V6VvarTextBox _ma_vt, _ma_hangkm;
        private V6NumberTextBox _tslkm, _tu_soluong, _den_soluong, _tu_sotien, _den_sotien,
            _soluong_km, _sotien_km, _pt_ck;
        private V6DateTimeColor _ngayKetThuc, _ngayBatDau;
        private V6ColorTextBox _ten_vt, _mo_ngoac, _dong_ngoac, _dvt, _ghichu_km, _ghichu_ck;
        private ComboBox _oper;

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

            _mo_ngoac = new V6ColorTextBox
            {
                AccessibleName = "DAU1",
                GrayText = "Dấu 1",
                Width = 30,
            };
            _ten_vt = new V6ColorTextBox
            {
                AccessibleName = "TEN_VT",
                GrayText = "Tên vật tư",
                Width = 300,
                Enabled = false,
                Tag = "disable"
            };
            _oper = new V6ComboBox()
            {
                AccessibleName = "OPER",
                //GrayText = "So sánh",
                Width = 60,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "", "and", "or" }
            };
            _dong_ngoac = new V6ColorTextBox
            {
                AccessibleName = "DAU2",
                GrayText = "Dấu 2",
                Width = 30,
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
                GrayText = "Mã vt KM",
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

            
            dynamicControlList1 = new SortedDictionary<int, Control>();
            int stt = 0;
            dynamicControlList1.Add(stt++, _ma_vt);
            //dynamicControlList.Add(stt++, _mo_ngoac);
            dynamicControlList1.Add(stt++, _ten_vt);
            //dynamicControlList.Add(stt++, _oper);
            //dynamicControlList.Add(stt++, _dong_ngoac);
            dynamicControlList1.Add(stt++, _dvt);
            dynamicControlList1.Add(stt++, _tslkm);
            dynamicControlList1.Add(stt++, _tu_soluong);
            dynamicControlList1.Add(stt++, _den_soluong);
            dynamicControlList1.Add(stt++, _tu_sotien);
            dynamicControlList1.Add(stt++, _den_sotien);

            dynamicControlList1.Add(stt++, _ma_hangkm);
            dynamicControlList1.Add(stt++, _soluong_km);

            dynamicControlList1.Add(stt++, _sotien_km);
            dynamicControlList1.Add(stt++, _ghichu_km);
            dynamicControlList1.Add(stt++, _pt_ck);
            dynamicControlList1.Add(stt++, _ghichu_ck);
            
            
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

        private V6VvarTextBox _ma_vt22, _ma_hangkm22;
        private V6NumberTextBox _tslkm22, _tu_soluong22, _den_soluong22, _tu_sotien22, _den_sotien22,
            _soluong_km22, _sotien_km22, _pt_ck22;
        private V6DateTimeColor _ngayKetThuc22, _ngayBatDau22;
        private V6ColorTextBox _ten_vt22, _mo_ngoac22, _dong_ngoac22, _dvt22, _ghichu_km22, _ghichu_ck22;
        private ComboBox _oper22;
        private void LoadDetail2Controls()
        {
            //Tạo trước control phòng khi alct1 không có

            _ma_vt22 = new V6VvarTextBox
            {
                AccessibleName = "ma_vt",
                VVar = "ma_vt",
                GrayText = "Mã vật tư",
                BrotherFields = "TEN_VT,DVT",
                //NeighborFields = "TEN_VT0"
            };
            _ma_vt22.Upper();
            _ma_vt22.FilterStart = true;
            _ma_vt22.V6LostFocus += delegate
            {
                GetThongTinVt();
            };

            _mo_ngoac22 = new V6ColorTextBox
            {
                AccessibleName = "DAU1",
                GrayText = "Dấu 1",
                Width = 30,
            };
            _ten_vt22 = new V6ColorTextBox
            {
                AccessibleName = "TEN_VT",
                GrayText = "Tên vật tư",
                Width = 300,
                Enabled = false,
                Tag = "disable"
            };
            _oper22 = new V6ComboBox()
            {
                AccessibleName = "OPER",
                //GrayText = "So sánh",
                Width = 60,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "", "and", "or" }
            };
            _dong_ngoac22 = new V6ColorTextBox
            {
                AccessibleName = "DAU2",
                GrayText = "Dấu 2",
                Width = 30,
            };

            _dvt22 = new V6ColorTextBox
            {
                AccessibleName = "DVT",
                GrayText = "ĐVT",
                Enabled = false,
                Tag = "disable"
            };
            _tslkm22 = new V6NumberTextBox()
            {
                AccessibleName = "T_SLKM",
                GrayText = "Tổng sl km",
            };
            _tu_soluong22 = new V6NumberTextBox()
            {
                AccessibleName = "T_SL1",
                GrayText = "Từ số lượng",
            };
            _den_soluong22 = new V6NumberTextBox()
            {
                AccessibleName = "T_SL2",
                GrayText = "Đến",
            };

            _tu_sotien22 = new V6NumberTextBox()
            {
                AccessibleName = "T_TIEN1",
                GrayText = "Từ số tiền",
            };
            _den_sotien22 = new V6NumberTextBox()
            {
                AccessibleName = "T_TIEN2",
                GrayText = "Đến",
            };
            _ma_hangkm22 = new V6VvarTextBox
            {
                AccessibleName = "MA_SP",
                VVar = "ma_vt",
                GrayText = "Mã vt KM",
                //BrotherFields = "TEN_VT",
                //NeighborFields = "TEN_VT0"
            };
            _soluong_km22 = new V6NumberTextBox()
            {
                AccessibleName = "SL_KM",
                GrayText = "Số lượng km",
            };
            _sotien_km22 = new V6NumberTextBox()
            {
                AccessibleName = "TIEN_KM",
                GrayText = "Số tiền km",
            };
            _ghichu_km22 = new V6ColorTextBox()
            {
                AccessibleName = "GHI_CHUKM",
                GrayText = "Ghi chú km",
            };
            _pt_ck22 = new V6NumberTextBox()
            {
                AccessibleName = "PT_CK",
                GrayText = "%CK",
            };
            _ghichu_ck22 = new V6ColorTextBox()
            {
                AccessibleName = "GHI_CHUCK",
                GrayText = "Ghi chú ck",
            };


            dynamicControlList2 = new SortedDictionary<int, Control>();
            int stt = 0;
            dynamicControlList2.Add(stt++, _ma_vt22);
            //dynamicControlList.Add(stt++, _mo_ngoac22);
            dynamicControlList2.Add(stt++, _ten_vt22);
            //dynamicControlList.Add(stt++, _oper22);
            //dynamicControlList.Add(stt++, _dong_ngoac22);
            dynamicControlList2.Add(stt++, _dvt22);
            dynamicControlList2.Add(stt++, _tslkm22);
            dynamicControlList2.Add(stt++, _tu_soluong22);
            dynamicControlList2.Add(stt++, _den_soluong22);
            dynamicControlList2.Add(stt++, _tu_sotien22);
            dynamicControlList2.Add(stt++, _den_sotien22);

            dynamicControlList2.Add(stt++, _ma_hangkm22);
            dynamicControlList2.Add(stt++, _soluong_km22);

            dynamicControlList2.Add(stt++, _sotien_km22);
            dynamicControlList2.Add(stt++, _ghichu_km22);
            dynamicControlList2.Add(stt++, _pt_ck22);
            dynamicControlList2.Add(stt++, _ghichu_ck22);


            foreach (KeyValuePair<int, Control> item in dynamicControlList2)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);
                V6ControlFormHelper.ApplyControlEventByAccessibleName(control, Event_program, All_Objects, "_DETAIL2");
            }

            //Add detail controls
            foreach (Control control in dynamicControlList2.Values)
            {
                detail3.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail3, _table3Struct);
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
                {
                    string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table2Name +
                                 " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
                    SqlParameter[] plist = {new SqlParameter("@rec", sttRec)};
                    AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                    SetDataToGrid(dataGridView1, AD, txtMaCt.Text);
                }
                //Data3
                {
                    string sql = "SELECT a.*,b.ten_vt as ten_vt FROM " + _table3Name +
                                 " as a left join alvt b on a.ma_vt=b.ma_vt  Where stt_rec = @rec";
                    SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                    data3 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                        .Tables[0];
                    SetDataToGrid(gView3, data3, txtMaCt.Text, _table3Name);
                    //gView3.DataSource = data3;
                    //gView3.HideColumnsAldm(_table3Name);
                    //gView3.SetCorplan2();
                }

                //Data4
                {
                    string sql = "SELECT a.*,b.ten_kh as ten_kh,b.dia_chi,b.nh_kh1,b.nh_kh2,b.nh_kh3,b.nh_kh4,b.nh_kh5,b.nh_kh6 FROM "
                                + _table4Name +
                                 " as a left join alkh b on a.ma_kh_i=b.ma_kh  Where stt_rec = @rec";
                    SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                    data4 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                        .Tables[0];
                    SetDataToGrid(gView4, data4, txtMaCt.Text, _table4Name);
                    //gView3.DataSource = data3;
                    //gView3.HideColumnsAldm(_table3Name);
                    //gView3.SetCorplan2();
                }
                //Data5
                {
                    string sql = "SELECT a.*,b.ten_kh as ten_kh,b.dia_chi,b.nh_kh1,b.nh_kh2,b.nh_kh3,b.nh_kh4,b.nh_kh5,b.nh_kh6 FROM "
                                + _table5Name +
                                 " as a left join alkh b on a.ma_kh_i=b.ma_kh  Where stt_rec = @rec";
                    SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                    data5 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                        .Tables[0];
                    SetDataToGrid(gView5, data5, txtMaCt.Text, _table5Name);
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

                        adList = data4.ToListDataDictionary(txtSttRec.Text);
                        foreach (IDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table4Name, row);
                        }
                        
                        adList = data5.ToListDataDictionary(txtSttRec.Text);
                        foreach (IDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table5Name, row);
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
                ShowMainMessage("Cần sửa lại updateData cho _table3Struct");
                DataDic = GetData();
                ValidateData();
                
                var keys = new SortedDictionary<string, object>
                {
                    {"STT_REC", DataDic["STT_REC"]}
                };
                var keysAD = new SortedDictionary<string, object>();
                keysAD.AddRange(keys);
                //keysAD.Add("TS0", txtTs0.Value);

                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("Alkmb Update");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                    var deleteAd3Sql = SqlGenerator.GenDeleteSql(_table3Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
                    var deleteAd4Sql = SqlGenerator.GenDeleteSql(_table4Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd4Sql);
                    var deleteAd5Sql = SqlGenerator.GenDeleteSql(_table5Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd5Sql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, _MA_DM, _TableStruct, DataDic, keys);
                    var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;

                    int j = 0, k = 0, l = 0;

                    //Insert AD
                    var adList = AD.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table2Struct, adRow, false);
                        j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    var adList3 = data3.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList3)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table3Struct, adRow, false);
                        k += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    var adList4 = data4.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList4)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table4Struct, adRow, false);
                        l += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    var adList5 = data5.ToListDataDictionary(txtSttRec.Text);
                    foreach (IDictionary<string, object> adRow in adList5)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table5Struct, adRow, false);
                        l += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    if (insert_success && j == adList.Count && k == adList3.Count)
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
            
            if (txtMaKm.Text.Trim() == "") errors += V6Text.Text("CHUANHAP") + lblMaKM.Text + "!\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 0, "MA_KM",
                 txtMaKm.Text.Trim(), DataOld["MA_KM"].ToString());
                if (!b)
                    throw new Exception(V6Text.Exist + V6Text.EditDenied
                                                    + lblMaKM.Text + " = " + txtMaKm.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(_MA_DM, 1, "MA_KM",
                 txtMaKm.Text.Trim(), txtMaKm.Text.Trim());
                if (!b)
                    throw new Exception(V6Text.Exist + V6Text.AddDenied
                                                    + lblMaKM.Text + " = " + txtMaKm.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit)
            {
                errors += V6Text.DetailNotComplete;
            }

            if (errors.Length > 0) throw new Exception(errors);

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_DVCS", "MA_KM", "MA_CT" }, AD);
            V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL", dateNgayHL.Date);
            V6ControlFormHelper.UpdateDKlist(AD, "NGAY_HL2", dateNgayHL2.Value);

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_DVCS", "MA_KM", "MA_CT" }, data3);
            V6ControlFormHelper.UpdateDKlist(data3, "NGAY_HL", dateNgayHL.Date);
            V6ControlFormHelper.UpdateDKlist(data3, "NGAY_HL2", dateNgayHL2.Value);

            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_DVCS", "MA_KM", "MA_CT" }, data4);
            V6ControlFormHelper.UpdateDKlist(data4, "NGAY_HL", dateNgayHL.Date);
            V6ControlFormHelper.UpdateDKlist(data4, "NGAY_HL2", dateNgayHL2.Value);
            
            V6ControlFormHelper.UpdateDKlistAll(DataDic, new[] { "MA_DVCS", "MA_KM", "MA_CT" }, data5);
            V6ControlFormHelper.UpdateDKlist(data5, "NGAY_HL", dateNgayHL.Date);
            V6ControlFormHelper.UpdateDKlist(data5, "NGAY_HL2", dateNgayHL2.Value);
        }

        /// <summary>
        /// Tạo thêm thông tin trước khi thêm chi tiết.
        /// </summary>
        /// <param name="data"></param>
        private void GenMoreDetails(IDictionary<string, object> data)
        {
            try
            {
                var gen_where1 = "test where 1";
                var gen_where2 = "test where 2";
                string where_sl = "", where_tien = "";
                if (ObjectAndString.ObjectToDecimal(data["T_SL2"]) != 0)
                {
                    where_sl = string.Format(" and (SO_LUONG >= {0} and SO_LUONG <= {1})",
                        data["T_ST1"].ToString().Replace(",", "."),
                        data["T_ST2"].ToString().Replace(",", "."));
                }
                if (ObjectAndString.ObjectToDecimal(data["T_TIEN2"]) != 0)
                {
                    where_tien = string.Format(" and TIEN2 >= {0}",
                        data["T_TIEN2"].ToString().Replace(",", "."));
                }
                gen_where1 = string.Format("MA_VT = {0}{1}{2}",
                    SqlGenerator.FormatStringValue( "" + data["MA_VT"]),
                    where_sl,
                    where_tien);
                gen_where2 = string.Format("{0} {1} {2} {3}", data["MA_VT"], "=", data["MA_SP"], data["OPER"]);
                //SqlGenerator.FormatStringValue()
                //SL from to, tien?
                //,[t_sl2] <> 0
                // or     ,[t_tien2] <> 0
                // ma_vt = 'ma_vt' and so_luong ... and tien2 >= 'tien2'
                data["GEN_WHERE1"] = gen_where1;
                data["GEN_WHERE2"] = gen_where2;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GenMoreDetails", ex);
            }
        }

        #region ==== Detail control events ====

        private void XuLyChonVatTu()
        {
            try
            {
                txtChonMavt.Lookup(LookupMode.Multi);
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
        private void XuLyChonVatTuKM()
        {
            try
            {
                txtChonMavtKM.Lookup(LookupMode.Multi);
                //xyly
                if (txtChonMavtKM.Text != "")
                {
                    var where = "Ma_vt='" + txtChonMavtKM.Text.Replace(",", "' or Ma_vt='") + "'";
                    var dataTable = V6BusinessHelper.Select("Alvt", "MA_VT AS MA_SP,TEN_VT AS TEN_SP,DVT", where).Data;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var DATA = row.ToDataDictionary();

                        XuLyThemDetail3(DATA);
                    }
                }
                txtChonMavtKM.Text = "";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyChonVatTuKM", ex);
            }
        }
        private void XuLyChonVatTuCK()
        {
            try
            {
                txtChonMavtCK.Lookup(LookupMode.Multi);
                //xyly
                if (txtChonMavtCK.Text != "")
                {
                    var where = "Ma_vt='" + txtChonMavtCK.Text.Replace(",", "' or Ma_vt='") + "'";
                    var dataTable = V6BusinessHelper.Select("Alvt", "MA_VT ,TEN_VT,DVT", where).Data;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var DATA = row.ToDataDictionary();

                        XuLyThemDetail3(DATA);
                    }
                }
                txtChonMavtCK.Text = "";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyChonVatTuCK", ex);
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
        private void XuLyDetail3ClickAdd()
        {
            try
            {
                //ten_ptkt.Focus();
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
                _sttRec0 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0;
                data["STT_REC"] = txtSttRec.Text;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                {
                    if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "")
                    {
                        error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                        error += "\nMã vật tư KM rỗng.";
                    }
                }
                
                
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
                this.ShowErrorException(GetType() + ".Thêm chi tiết", ex);
                return false;
            }
            return true;
        }

        private bool XuLyThemDetail3(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return false;
            }
            try
            {
                _sttRec0_33 = V6BusinessHelper.GetNewSttRec0(data3);
                data["STT_REC0"] = _sttRec0_33;
                data["STT_REC"] = txtSttRec.Text;
                
                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                {
                    if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "")
                    {
                        error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                        error += "\nMã vật tư KM rỗng.";
                    }
                }
                

                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = data3.NewRow();
                    foreach (DataColumn column in data3.Columns)
                    {
                        var KEY = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                        newRow[KEY] = value;
                    }
                    //Them du lieu chung
                    data3.Rows.Add(newRow);
                    gView3.DataSource = data3;
                    //TinhTongThanhToan("xu ly them detail3");

                    if (data3.Rows.Count > 0)
                    {
                        gView3.Rows[data3.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKDATA") + "3:" + error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết 3: " + ex.Message);
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
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                        {
                            if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "")
                            {
                                error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                                error += "\nMã vật tư KM rỗng.";
                            }
                        }

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
                this.ShowErrorException(GetType() + ".Sửa chi tiết", ex);
                return false;
            }
            return true;
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
                        if (!data.ContainsKey("MA_VT") || data["MA_VT"].ToString().Trim() == "")
                        {
                            if (!data.ContainsKey("MA_SP") || data["MA_SP"].ToString().Trim() == "")
                            {
                                error += "\n" + CorpLan.GetText("ADDEDITL00195") + " " + V6Text.Empty;
                                error += "\nMã vật tư KM rỗng.";
                            }
                        }


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
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết 3: " + ex.Message);
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
                this.ShowErrorException(GetType() + ".Xóa chi tiết", ex);
            }
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
                        var details = string.Format("{0}: {1}", CorpLan2.GetFieldHeader("MA_VT"), currentRow["MA_VT"]);
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data3.Rows.Remove(currentRow);
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết 3: " + ex.Message);
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

        private bool ValidateData_Detail3(IDictionary<string, object> dic)
        {
            try
            {
                if (dic == null || dic.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoData);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "SoDuManager AltsAddEdit ValidateData_Detail3");
            }
            return true;
        }

        #endregion details
        
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
            GenMoreDetails(data);
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
                    GetThongTinVt();
                    _ma_vt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ClickEdit", ex);
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
            GenMoreDetails(data);
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
            txtMaDVCS.Focus();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
            {
                detail1.SetData(dataGridView1.CurrentRow.ToDataDictionary());
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

        private void txtKieuCk_V6LostFocus(object sender)
        {
            if (sender == txtKieuCk)
            {
                XuLyChonKieuCk();
            }
            if (sender == txtWhere0)
            {
                GenGet2Xml();
            }

            GenSql();
        }

        private void XuLyChonKieuCk()
        {
            try
            {
                var data = txtKieuCk.Data;
                if (data == null)
                {
                    return;
                }
                txtXtype.Text = data["MA_TD1"].ToString().Trim();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyChonKieuCk", ex);
            }
        }

        private string Gen_Where1_And(string list_in)
        {
            string result = "";
            
            list_in = list_in.Replace("~", "'~'");
            
            string[] sss = list_in.Split('~');
            foreach (string s in sss)
            {
                result += " AND " + string.Format("MA_VT in ({0})", s);
            }

            if (result.Length > 5)
            {
                result = result.Substring(5);
            }
            return result;
        }

        private void GenSql()
        {
            try
            {
                string xType = txtXtype.Text.ToUpper();
                string list_in = "";
                //KMCTL_LC,KMCTL_PC,KMTHT_LC,KMTHL_LC
                //CKCTT_PC,CKCTL_PC,CKTHL_PC,CKTHT_PC
                //GGCTT_TT,GGCTT_PC,GGTHT_TT
                //KCCTL_LC

                string where0TextTrim = txtWhere0.Text.Trim();
                while (where0TextTrim.Contains(", "))
                {
                    where0TextTrim = where0TextTrim.Replace(", ", ",");
                }
                while (where0TextTrim.Contains(" ,"))
                {
                    where0TextTrim = where0TextTrim.Replace(" ,", ",");
                }

                switch (xType)
                {
                    case "KMCTL_LC"://Chi tiet luong
                    case "KMCTL_PC"://Chi tiet luong
                    case "CKCTL_PC"://Chi tiet luong
                    case "KCCTL_LC"://Chi tiet luong

                        txtSelect0.Text = "SUM(So_luong)";
                        txtSelect1.Text = "SUM(So_luong) AS SO_LUONG";
                        txtFrom1.Text = "AD";
                        list_in = "," + where0TextTrim + ",";
                        list_in = list_in.Replace(",", "','");
                        list_in = list_in.Substring(2, list_in.Length - 4);
                        if (txtOper0.Text.Trim().ToUpper() == "AND")
                        {
                            txtWhere1.Text = Gen_Where1_And(list_in);
                        }
                        else
                        {
                            txtWhere1.Text = string.Format("MA_VT in ({0})", list_in);
                        }
                        txtGroupBy1.Text = "stt_rec";
                        if (txtTSL1.Value != 0)
                        {
                            if (txtTSL2.Value == 0)
                                txtHaving1.Text = " sum(so_luong)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture);
                            else
                                txtHaving1.Text = " sum(so_luong)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture) + " and sum(so_luong)<=" + txtTSL2.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        break;
                    case "CKCTT_PC"://Chi tiet tien
                    case "GGCTT_PC"://Chi tiet tien
                    case "GGCTT_TT"://Chi tiet tien

                         txtSelect0.Text = "SUM(TIEN2)";
                         txtSelect1.Text = "SUM(TIEN2) AS TIEN2";
                        txtFrom1.Text = "AD";
                        list_in = "," + where0TextTrim + ",";
                        list_in = list_in.Replace(",", "','");
                        list_in = list_in.Substring(2, list_in.Length - 4);
                        if (txtOper0.Text.Trim().ToUpper() == "AND")
                        {
                            txtWhere1.Text = Gen_Where1_And(list_in);
                        }
                        else
                        {
                            txtWhere1.Text = string.Format("MA_VT in ({0})", list_in);
                        }
                        txtGroupBy1.Text = "stt_rec";
                        if (txtTSL1.Value != 0)
                        {
                            if (txtTSL2.Value == 0)
                                txtHaving1.Text = " sum(TIEN2)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture);
                            else
                                txtHaving1.Text = " sum(TIEN2)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture) + " and sum(TIEN2)<=" + txtTSL2.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        break;
                    case "KMTHL_LC"://Tong hop luong
                    case "CKTHL_PC"://Tong hop luong

                        
                        txtSelect0.Text = "SUM(T_So_luong)";
                        txtSelect1.Text = "SUM(T_So_luong) AS SO_LUONG";
                        txtFrom1.Text = "AM";
                        list_in = "," + where0TextTrim + ",";
                        list_in = list_in.Replace(",", "','");
                        list_in = list_in.Substring(2, list_in.Length - 4);
                        if (txtOper0.Text.Trim().ToUpper() == "AND")
                        {
                            txtWhere1.Text = Gen_Where1_And(list_in);
                        }
                        else
                        {
                            txtWhere1.Text = string.Format("MA_VT in ({0})", list_in);
                        }
                        txtGroupBy1.Text = "stt_rec";
                        if (txtTSL1.Value != 0)
                        {
                            if (txtTSL2.Value == 0)
                                txtHaving1.Text = " sum(t_so_luong)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture);
                            else
                                txtHaving1.Text = " sum(t_so_luong)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture) + " and sum(t_so_luong)<=" + txtTSL2.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        break;
                    case "KMTHT_LC"://Tong hop tien
                    case "CKTHT_PC"://Tong hop tien
                    case "GGTHT_TT"://Tong hop tien
                         txtSelect0.Text = "SUM(T_TIEN2)";
                         txtSelect1.Text = "SUM(T_TIEN2) AS TIEN2";
                        txtFrom1.Text = "AM";
                        list_in = "," + where0TextTrim + ",";
                        list_in = list_in.Replace(",", "','");
                        list_in = list_in.Substring(2, list_in.Length - 4);
                        if (txtOper0.Text.Trim().ToUpper() == "AND")
                        {
                            txtWhere1.Text = Gen_Where1_And(list_in);
                        }
                        else
                        {
                            txtWhere1.Text = string.Format("MA_VT in ({0})", list_in);
                        }
                        txtGroupBy1.Text = "stt_rec";
                        if (txtTSL1.Value != 0)
                        {
                            if (txtTSL2.Value == 0)
                                txtHaving1.Text = " sum(T_TIEN2)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture);
                            else
                                txtHaving1.Text = " sum(T_TIEN2)>=" + txtTSL1.Value.ToString(CultureInfo.InvariantCulture) + " and sum(T_TIEN2)<=" + txtTSL2.Value.ToString(CultureInfo.InvariantCulture);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GenSql", ex);
            }
        }

        /// <summary>
        /// Tạo get2 xml từ danh sách mã vật tư.
        /// </summary>
        private void GenGet2Xml()
        {
            try
            {
                DataSet ds = new DataSet();
                //Tạo dữ liệu cho ds từ list ma_vt
                ds.Tables.Add(new DataTable("Get2"));
                var table = ds.Tables[0];
                //var columns = "MA_VT,SO_LUONG,TIEN2".Split(',');
                table.Columns.Add("MA_VT", typeof (string));
                table.Columns.Add("SO_LUONG", typeof (decimal));
                table.Columns.Add("TIEN2", typeof (decimal));
                
                //Them du lieu list ma_vt
                var mavts = ObjectAndString.SplitString(txtWhere0.Text.Trim());
                foreach (string mavt in mavts)
                {
                    var newRow = table.NewRow();
                    newRow[0] = mavt;
                    newRow[1] = 0m;
                    newRow[2] = 0m;
                    table.Rows.Add(newRow);
                }
                StringBuilder sb = new StringBuilder();
                TextWriter tw = new StringWriter(sb);
                ds.WriteXml(tw);
                txtGet2.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + ".Write", ex);
            }
        }

        private void DoEditXml()
        {
            try
            {
                var file_xml = txtKieuCk.Text.Trim().ToUpper() + ".xml";
                new XmlEditorForm(txtGet2, file_xml, "Table0", "MA_VT,SO_LUONG,TIEN2".Split(',')).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoEditXml", ex);
            }
        }

        private void DoEditTable()
        {
            //V6ControlFormHelper.ShowDataEditorForm(this, data, );
        }

        private void v6FormButton1_Click(object sender, EventArgs e)
        {
            if (txtGet2.Text.Length > 2)
            {
                DoEditXml();
            }
            else
            {
                ShowMainMessage(V6Text.NoData);
            }
        }

        private void detail3_AddHandle(IDictionary<string, object> dataDic)
        {
            if (ValidateData_Detail3(dataDic) && XuLyThemDetail3(dataDic))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
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

        private void detail3_ClickAdd(object sender, HD_Detail_Eventargs e)
        {
            XuLyDetail3ClickAdd();
        }
        private void detail3_ClickEdit(object sender, HD_Detail_Eventargs e)
        {
            try
            {
                if (data3 != null && data3.Rows.Count > 0 && gView3.DataSource != null)
                {
                    detail3.ChangeToEditMode();
                    ChungTu.ViewSelectedDetailToDetailForm(gView3, detail3, out _gv3EditingRow, out _sttRec0_33);
                    //ma_bp_i.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }
        private void detail3_ClickCancelEdit(object sender, HD_Detail_Eventargs e)
        {
            detail3.SetData(_gv4EditingRow.ToDataDictionary());
        }

        private void detail3_ClickDelete(object sender, HD_Detail_Eventargs e)
        {
            XuLyXoaDetail3();
        }

        private void btnChonMaVtKM_Click(object sender, EventArgs e)
        {
            XuLyChonVatTuKM();
        }

        private void btnChonMaVtCK_Click(object sender, EventArgs e)
        {
            XuLyChonVatTuCK();
        }
        private void ChucNang_ChonTuExcel()
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                List<string> dateColumns = new List<string>();
                foreach (DataColumn column in AD.Columns)
                {
                    if (ObjectAndString.IsDateTimeType(column.DataType))
                    {
                        dateColumns.Add(column.ColumnName);
                    }
                }
                var chonExcel = new LoadExcelDataForm();
                chonExcel.CheckDateFields = dateColumns;
                chonExcel.CheckFields = "MA_KH_I";
                chonExcel.LoadDataComplete += chonExcel_LoadDataComplete;
                chonExcel.AcceptData += chonExcel_AcceptData;
                chonExcel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChonTuExcel", ex);
            }
        }

        void chonExcel_LoadDataComplete(object sender)
        {
            try
            {
                LoadExcelDataForm chonExcel = (LoadExcelDataForm)sender;
                DataTable errorData = new DataTable("ErrorData");
                List<DataGridViewRow> removeList = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in chonExcel.dataGridView1.Rows)
                {
                    string cMaKh_I = ("" + row.Cells["MA_KH_I"].Value).Trim();
                    if (cMaKh_I == string.Empty)
                    {
                        removeList.Add(row);
                        continue;
                    }
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALKH", "MA_KH", cMaKh_I);
                    if (!exist)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        errorData.AddRow(row.ToDataDictionary(), true);
                    }
                }
                while (removeList.Count > 0)
                {
                    chonExcel.dataGridView1.Rows.Remove(removeList[0]);
                    removeList.RemoveAt(0);
                }
                if (errorData.Rows.Count > 0)
                {
                    DataViewerForm viewer = new DataViewerForm(errorData);
                    viewer.Text = V6Text.WrongData;
                    viewer.FormClosing += (o, args) =>
                    {
                        if (V6ControlFormHelper.ShowConfirmMessage(V6Text.Export + " " + V6Text.WrongData + "?") == DialogResult.Yes)
                        {
                            V6ControlFormHelper.ExportExcel_ChooseFile(this, errorData, "errorData");
                        }
                    };
                    viewer.ShowDialog(chonExcel);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        private void ChucNang_ChonTuExcel5()
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                var chonExcel5 = new LoadExcelDataForm();
                chonExcel5.CheckFields = "MA_KH_I";
                chonExcel5.LoadDataComplete += chonExcel5_LoadDataComplete;
                chonExcel5.AcceptData += chonExcel5_AcceptData;
                chonExcel5.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChonTuExcel", ex);
            }
        }

        void chonExcel5_LoadDataComplete(object sender)
        {
            try
            {
                LoadExcelDataForm chonExcel = (LoadExcelDataForm)sender;
                DataTable errorData = new DataTable("ErrorData");
                List<DataGridViewRow> removeList = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in chonExcel.dataGridView1.Rows)
                {
                    string cMaKh_I = ("" + row.Cells["MA_KH_I"].Value).Trim();
                    if (cMaKh_I == string.Empty)
                    {
                        removeList.Add(row);
                        continue;
                    }
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALKHO", "MA_KH", cMaKh_I);
                    if (!exist)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        errorData.AddRow(row.ToDataDictionary(), true);
                    }
                }
                while (removeList.Count > 0)
                {
                    chonExcel.dataGridView1.Rows.Remove(removeList[0]);
                    removeList.RemoveAt(0);
                }
                if (errorData.Rows.Count > 0)
                {
                    DataViewerForm viewer = new DataViewerForm(errorData);
                    viewer.Text = V6Text.WrongData;
                    viewer.FormClosing += (o, args) =>
                    {
                        if (V6ControlFormHelper.ShowConfirmMessage(V6Text.Export + " " + V6Text.WrongData + "?") == DialogResult.Yes)
                        {
                            V6ControlFormHelper.ExportExcel_ChooseFile(this, errorData, "errorData");
                        }
                    };
                    viewer.ShowDialog(chonExcel);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _sttRec), ex);
            }
        }

        public void chonExcel_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_KH_I") && table.Rows.Count > 0)
            {
               
                foreach (DataRow row in table.Rows)
                {
                    var data = row.ToDataDictionary(_sttRec);
                    var cMaKh = data["MA_KH_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALKH", "MA_KH", cMaKh);
                    
                  
                    if (exist)
                    {
                        if (XuLyThemDetail4(data))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaKh);
                    }
                }
                ShowMainMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
            }
            else
            {
                ShowMainMessage(V6Text.Text("LACKINFO"));
            }
        }
        
        void chonExcel5_AcceptData(DataTable table)
        {
            var count = 0;
            _message = "";

            if (table.Columns.Contains("MA_KH_I") && table.Rows.Count > 0)
            {
               
                foreach (DataRow row in table.Rows)
                {
                    var data = row.ToDataDictionary(_sttRec);
                    var cMaKh = data["MA_KH_I"].ToString().Trim();
                    var exist = V6BusinessHelper.IsExistOneCode_List("ALKH", "MA_KH", cMaKh);
                    
                  
                    if (exist)
                    {
                        if (XuLyThemDetail5(data))
                        {
                            count++;
                        }
                    }
                    else
                    {
                        _message += string.Format("{0} [{1}]", V6Text.NotExist, cMaKh);
                    }
                }
                ShowMainMessage(string.Format(V6Text.Added + "[{0}].", count) + _message);
            }
            else
            {
                ShowMainMessage(V6Text.Text("LACKINFO"));
            }
        }

        private bool XuLyThemDetail4(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec0_44 = V6BusinessHelper.GetNewSttRec0(data4);
                data["STT_REC0"] = _sttRec0_44;
                data["STT_REC"] = txtSttRec.Text;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_KH_I") || data["MA_KH_I"].ToString().Trim() == "")
                {
                    error += V6Text.FieldCaption("MA_KH") + " " + V6Text.Empty;
                }

                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = data4.NewRow();
                    foreach (DataColumn column in data4.Columns)
                    {
                        var KEY = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                        newRow[KEY] = value;
                    }
                    //Them du lieu chung
                    data4.Rows.Add(newRow);
                    gView4.DataSource = data4;
                    
                    if (data4.Rows.Count > 0)
                    {
                        gView4.Rows[data4.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKDATA") + "4:" + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết 4: " + ex.Message);
            }
            return true;
        }
        
        private bool XuLyThemDetail5(IDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec0_55 = V6BusinessHelper.GetNewSttRec0(data5);
                data["STT_REC0"] = _sttRec0_55;
                data["STT_REC"] = txtSttRec.Text;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_KH_I") || data["MA_KH_I"].ToString().Trim() == "")
                {
                    error += V6Text.FieldCaption("MA_KH") + " " + V6Text.Empty;
                }
                
                if (error == "")
                {
                    //Tạo dòng dữ liệu mới.
                    var newRow = data5.NewRow();
                    foreach (DataColumn column in data5.Columns)
                    {
                        var KEY = column.ColumnName.ToUpper();
                        object value = ObjectAndString.ObjectTo(column.DataType,
                            data.ContainsKey(KEY) ? data[KEY] : "") ?? DBNull.Value;
                        newRow[KEY] = value;
                    }
                    //Them du lieu chung
                    data5.Rows.Add(newRow);
                    gView5.DataSource = data5;
                    
                    if (data5.Rows.Count > 0)
                    {
                        gView5.Rows[data5.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.Text("CHECKDATA") + "5:" + error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết 5: " + ex.Message);
            }
            return true;
        }

        private void BtchonExcel_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Xoa_gView4();
            }
            else
            {
                ChucNang_ChonTuExcel();
            }
        }
        
        private void BtchonExcel5_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Xoa_gView5();
            }
            else
            {
                ChucNang_ChonTuExcel5();
            }
        }

        private void Xoa_gView4()
        {
            if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
            {
                data4.Rows.Clear();
            }
        }
        
        private void Xoa_gView5()
        {
            if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
            {
                data5.Rows.Clear();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Mode != V6Mode.Add && Mode != V6Mode.Edit) return;

                if (e.KeyData == (Keys.Control | Keys.F3))
                {
                    if (this.ShowConfirmMessage(V6Text.Text("COLVALWITHSELECTED")) != DialogResult.Yes) return;

                    var currentRow = dataGridView1.CurrentRow;
                    var currentCell = dataGridView1.CurrentCell;

                    if (currentRow == null || currentCell == null)
                    {
                        ShowMainMessage(V6Text.NoSelection);
                        return;
                    }

                    int column_index = currentCell.ColumnIndex;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row != currentRow)
                        {
                            row.Cells[column_index].Value = currentCell.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".dataGridView1_KeyDown", ex);
            }
        }

        private void btnChonKH_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Xoa_gView4();
            }
            else
            {
                V6lookupConfig lookupInfo = V6Lookup.GetV6lookupConfig("MA_KH");
                V6VvarTextBoxForm lookupForm = new V6VvarTextBoxForm(txtMaKH, lookupInfo, txtMaKH.InitFilter, LookupMode.Data, false);
                lookupForm.AcceptSelectedtData += lookupForm_AcceptSelectedtData;
                lookupForm.ShowDialog(this);
            }
        }

        void lookupForm_AcceptSelectedtData(string idList, List<IDictionary<string, object>> dataList)
        {
            try
            {
                foreach (IDictionary<string, object> data in dataList)
                {
                    if(data.ContainsKey("MA_KH")) data["MA_KH_I"] = data["MA_KH"];
                    data4.AddRow(data);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".sf_AcceptSelectedtData", ex);
            }
        }

        private void BtchonExcel_MouseEnter(object sender, EventArgs e)
        {
            V6ControlFormHelper.SetStatusText(V6Text.Text("ShiftClickForDelete"));
        }

        private void BtchonExcel_MouseHover(object sender, EventArgs e)
        {
            V6ControlFormHelper.SetStatusText(V6Text.Text("ShiftClickForDelete"));
        }

        private void btnChonKH5_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Xoa_gView5();
            }
            else
            {
                V6lookupConfig lookupInfo = V6Lookup.GetV6lookupConfig("MA_KH");
                V6VvarTextBoxForm lookupForm = new V6VvarTextBoxForm(txtMaKH, lookupInfo, txtMaKH.InitFilter, LookupMode.Data, false);
                lookupForm.AcceptSelectedtData += lookupForm_AcceptSelectedtData5;
                lookupForm.ShowDialog(this);
            }
        }

        void lookupForm_AcceptSelectedtData5(string idList, List<IDictionary<string, object>> dataList)
        {
            try
            {
                foreach (IDictionary<string, object> data in dataList)
                {
                    if (data.ContainsKey("MA_KH")) data["MA_KH_I"] = data["MA_KH"];
                    data5.AddRow(data);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".sf_AcceptSelectedtData5", ex);
            }
        }

        private void gView4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F8)
            try
            {
                if (gView4.CurrentRow != null)
                {
                    var cIndex = gView4.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < data4.Rows.Count)
                    {
                        var currentRow = data4.Rows[cIndex];
                        var details = V6Text.FieldCaption("MA_KH") + ": " + currentRow["Ma_kh_i"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data4.Rows.Remove(currentRow);
                            gView4.DataSource = data4;
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

        private void gView5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            try
            {
                if (gView5.CurrentRow != null)
                {
                    var cIndex = gView5.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < data5.Rows.Count)
                    {
                        var currentRow = data5.Rows[cIndex];
                        var details = V6Text.FieldCaption("MA_KH") + ": " + currentRow["Ma_kh_i"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data5.Rows.Remove(currentRow);
                            gView5.DataSource = data5;
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

        private void gView4_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void gView5_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void gView4_MouseHover(object sender, EventArgs e)
        {
            string id = "ST2F8DELETEAROW";
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("F8DELETEAROW");
            }
            V6ControlFormHelper.SetStatusText2(text, id);
            V6ControlFormHelper.SetStatusText(text);
        }

        private void gView5_MouseHover(object sender, EventArgs e)
        {
            string id = "ST2F8DELETEAROW";
            var text = CorpLan.GetTextNull(id);
            if (string.IsNullOrEmpty(text))
            {
                text = V6Text.Text("F8DELETEAROW");
            }
            V6ControlFormHelper.SetStatusText2(text, id);
            V6ControlFormHelper.SetStatusText(text);
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
