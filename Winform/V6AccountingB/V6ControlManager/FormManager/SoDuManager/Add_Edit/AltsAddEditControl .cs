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
    public partial class AltsAddEditControl  : SoDuAddEditControlVirtual
    {
        private V6TableStruct _table2Struct, _table3Struct, _table4Struct;
        private string _sttRec0_11 = "", _sttRec0_33 = "", _sttRec0_44 = "";
        
        public AltsAddEditControl()
        {
            InitializeComponent();
            
            MyInit();
        }

        private void MyInit()
        {
            _maCt = "S02";
            _table2Name = "ADALTS";
            _table3Name = "ADCTTS";
            _table4Name = "ADCTTSBP";

            txtNhom_TS1.SetInitFilter("LOAI_NH = 1");
            txtNhom_TS2.SetInitFilter("LOAI_NH = 2");
            txtNhom_TS3.SetInitFilter("LOAI_NH = 3");

            txtTk_cp.FilterStart = true;
            txtTk_cp.SetInitFilter("loai_tk = 1");

            txtTk_kh.FilterStart = true;
            txtTk_kh.SetInitFilter("loai_tk = 1");

            txtTk_ts.FilterStart = true;
            txtTk_ts.SetInitFilter("loai_tk = 1");

            txtLyDoTang.SetInitFilter("loai_tg_ts ='T'");

            txtMaCt.Text = _maCt;

            _table2Struct = V6BusinessHelper.GetTableStruct(_table2Name);
            _table3Struct = V6BusinessHelper.GetTableStruct(_table3Name);
            _table4Struct = V6BusinessHelper.GetTableStruct(_table4Name);

            
            LoadDetailControls();

         
            detail1.MODE = V6Mode.View;
            detail1.lblName.AccessibleName = "TEN_NV";
            detail3.MODE = V6Mode.View;
            detail4.MODE = V6Mode.View;
        }

        public override void DoBeforeAdd()
        {
            if (V6Login.MadvcsCount == 1)
            {
                txtMaDVCS.Text = V6Login.Madvcs;
            }
            txtSttRec.Text = V6BusinessHelper.GetNewSttRec(_maCt);
        }

        public override void DoBeforeEdit()
        {
            var v = Categories.IsExistOneCode_List("ADKHTS,ADHSTS", "SO_THE_TS", txtMaTaiSan.Text);
            txtMaTaiSan.Enabled = !v;//Nếu tồn tại thì khóa lại.
        }


        private void LoadDetailControls()
        {
            LoadDetail2Controls();
            LoadDetail3Controls();
            LoadDetail4Controls();
        }

        private V6VvarTextBox ma_nv;
        private V6NumberTextBox nguyen_gia, gt_da_kh,  gt_cl, gt_kh_ky;
        private V6ColorTextBox so_ct, dien_giai;
        private V6DateTimeColor ngay_ct;
        private void LoadDetail2Controls()
        {
            detail1.lblName.AccessibleName = "TEN_NV";
            ma_nv = new V6VvarTextBox
            {
                AccessibleName = "ma_nv",
                VVar = "ma_nv",
                GrayText = "Nguồn vốn",
                BrotherFields = "TEN_NV"
            };
            ma_nv.Upper();
            ngay_ct = new V6DateTimeColor()
            {
                AccessibleName = "Ngay_ct", GrayText = "Ngày ct"
            };
            so_ct = new V6ColorTextBox()
            {
                AccessibleName = "So_ct",
                GrayText = "Số ct"
            };
            nguyen_gia = new NumberTien()
            {
                AccessibleName = "nguyen_gia", GrayText = "Nguyên giá"
            };
            nguyen_gia.V6LostFocus += delegate
            {
                TinhGiaTriKhauHao();
            };

            gt_da_kh = new NumberTien()
            {
                AccessibleName = "gt_da_kh",
                GrayText = "Giá trị đã kh"
            };
            gt_da_kh.V6LostFocus += delegate
            {
                TinhGiaTriKhauHao();
            };

            gt_cl = new NumberTien()
            {
                AccessibleName = "gt_cl",
                GrayText = "Giá trị còn lại",
                ReadOnly = true,
                Tag = "readonly",
                TabStop = false
            };

            gt_kh_ky = new NumberTien()
            {
                AccessibleName = "gt_kh_ky",
                GrayText = "Giá kh 1 kỳ"
            };
            gt_kh_ky.V6LostFocus += delegate
            {
                if (gt_kh_ky.Value == 0)
                {
                    TinhGiaTriKhauHao();
                }
            };
            
            dien_giai = new V6ColorTextBox()
            {
                AccessibleName = "dien_giai",
                GrayText = "Diễn giải"
            };
            

            var dynamicControlList = new SortedDictionary<int, Control>();
            
            dynamicControlList.Add(0, ma_nv);
            dynamicControlList.Add(1, ngay_ct);
            dynamicControlList.Add(2, so_ct);
            dynamicControlList.Add(3, nguyen_gia);
            dynamicControlList.Add(4, gt_da_kh);
            dynamicControlList.Add(5, gt_cl);
            dynamicControlList.Add(6, gt_kh_ky);
            dynamicControlList.Add(7, dien_giai);

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

        private void XuLyThayDoiSoKyKhauHao()
        {
            try
            {
                //gView2.DataSource
                var sokykh = txtSoKyKhauHao.Value;
                var ppkh = ObjectAndString.ObjectToInt(V6Options.V6OptionValues["M_PP_KH"]);
                foreach (DataRow row in AD.Rows)
                {
                    //gt_cl.Value = nguyen_gia.Value - gt_da_kh.Value;
                    decimal nguyen_gia_decimal = ObjectAndString.ObjectToDecimal(row["Nguyen_gia"]);
                    decimal gt_da_kh_decimal = ObjectAndString.ObjectToDecimal(row["gt_da_kh"]);
                    decimal gt_cl_decimal = nguyen_gia_decimal - gt_da_kh_decimal;
                    row["gt_cl"] = gt_cl_decimal;
                    
                    if (sokykh > 0)
                    {
                        row["gt_kh_ky"] = V6BusinessHelper.Vround(
                            ppkh == 1
                            ? nguyen_gia_decimal / sokykh
                            : gt_cl_decimal / sokykh,
                            V6Options.M_ROUND);
                    }
                }

                if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit)
                {
                    TinhGiaTriKhauHao(); //Da co tinh tong cong
                }
                else
                {
                    TinhTongCong();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyThayDoiSoKyKhauHao: " + ex.Message);
            }
        }

        private void TinhGiaTriKhauHao()
        {
            try
            {
                var ppkh = ObjectAndString.ObjectToInt(V6Options.V6OptionValues["M_PP_KH"]);
                gt_cl.Value = nguyen_gia.Value - gt_da_kh.Value;
                if (txtSoKyKhauHao.Value > 0)
                {
                    gt_kh_ky.Value = V6BusinessHelper.Vround(
                        ppkh == 1
                        ? nguyen_gia.Value/txtSoKyKhauHao.Value
                        : gt_cl.Value/txtSoKyKhauHao.Value,
                        V6Options.M_ROUND);
                    
                }
                TinhTongCong();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".TinhGiaTriKhauHao: " + ex.Message);
            }
        }

        private void TinhTongCong()
        {
            txttong_ng.Value = TinhTong("Nguyen_gia");
            txttong_da_kh.Value = TinhTong("Gt_da_kh");
            txtTong_kh_ky.Value = TinhTong("Gt_kh_ky");
            txtTong_cl.Value = TinhTong("Gt_cl");
        }
        private decimal TinhTong(string colName)
        {
            var total = 0m;
            try
            {
                if (AD != null && AD.Columns.Contains(colName))
                {
                    for (var j = 0; j < AD.Rows.Count; j++)
                    {
                        total += ObjectAndString.ObjectToDecimal(AD.Rows[j][colName]);
                    }
                    return total;
                }
                return total;
            }
            catch
            {
                return total;
            }
        }

        private V6ColorTextBox ten_ptkt, so_luong, gia_tri;
        private V6VvarTextBox dvt;

        private void LoadDetail3Controls()
        {
            detail3.ShowLblName = false;
            detail3.lblName.Tag = "hide";
            ten_ptkt = new V6ColorTextBox()
            {
                AccessibleName = "ten_ptkt",
                GrayText = "Tên phụ tùng kèm theo",
            };
            
            dvt = new V6VvarTextBox()
            {
                AccessibleName = "dvt",
                GrayText = "Đơn vị tính"
            };
            so_luong = new NumberSoluong()
            {
                AccessibleName = "so_luong",
                GrayText = "Số lượng"
            };
            gia_tri = new NumberTien()
            {
                AccessibleName = "gia_tri",
                GrayText = "Giá trị"
            };

            var dynamicControlList = new SortedDictionary<int, Control>();
            dynamicControlList.Add(0, ten_ptkt);
            dynamicControlList.Add(1, dvt);
            dynamicControlList.Add(2, so_luong);
            dynamicControlList.Add(3, gia_tri);

            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);
            }

            //Add detail controls
            foreach (Control control in dynamicControlList.Values)
            {
                detail3.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail3, _table3Struct);
        }

        private V6NumberTextBox he_so;

        private V6VvarTextBox 
            ma_bp_i,
            ma_bpht_i,
            ma_vv_i,
            tk_kh_i,
            tk_cp_i,
            ma_phi,
            ma_td_i,
            ma_td2_i,
            ma_td3_i,
            ma_sp;
        private void LoadDetail4Controls()
        {
            detail4.ShowLblName = false;
            detail4.lblName.Tag = "hide";

            ma_bp_i = new V6VvarTextBox
            {
                AccessibleName = "ma_bp_i",
                VVar = "MA_BPTS",
                GrayText = "Mã bộ phận"
            };
            ma_bp_i.Upper();
            ma_bpht_i = new V6VvarTextBox
            {
                AccessibleName = "ma_bpht_i",
                VVar = "MA_BPHT",
                GrayText = "Mã BPHT"
            };
            ma_vv_i = new V6VvarTextBox
            {
                AccessibleName = "ma_vv_i",
                VVar = "ma_vv",
                GrayText = "Mã vụ việc"
            };

            tk_kh_i = new V6VvarTextBox
            {
                AccessibleName = "tk_kh_i",
                VVar = "tk",
                GrayText = "Tk khấu hao"
            };
            tk_kh_i.FilterStart = true;
            tk_cp_i = new V6VvarTextBox
            {
                AccessibleName = "tk_cp_i",
                VVar = "tk",
                GrayText = "Tk chi phí"
            };
            tk_cp_i.FilterStart = true;
            he_so = new NumberSoluong()
            {
                AccessibleName = "he_so",
                GrayText = "Hệ số"
            };
            ma_phi = new V6VvarTextBox()
            {
                AccessibleName = "ma_phi",
                VVar = "MA_PHI",
                GrayText = "Mã phí"
            };
            ma_td_i = new V6VvarTextBox()
            {
                AccessibleName = "ma_td_i",
                VVar = "MA_TD",
                GrayText = "Mã ĐN"
            };
            ma_td2_i = new V6VvarTextBox()
            {
                AccessibleName = "ma_td2_i",
                VVar = "MA_TD2",
                GrayText = "Mã ĐN2"
            };
            ma_td3_i = new V6VvarTextBox()
            {
                AccessibleName = "ma_td3_i",
                VVar = "MA_TD3",
                GrayText = "Mã ĐN3"
            };

            ma_sp = new V6VvarTextBox()
            {
                AccessibleName = "ma_sp",
                VVar = "MA_VT",                
                GrayText = "Mã sản phẩm"
            };
            ma_sp.SetInitFilter("Loai_vt='51'");


            var dynamicControlList = new SortedDictionary<int, Control>();
            
            dynamicControlList.Add(0, ma_bp_i);
            dynamicControlList.Add(1, ma_bpht_i);
            dynamicControlList.Add(2, ma_vv_i);
            dynamicControlList.Add(3, tk_kh_i);
            dynamicControlList.Add(4, tk_cp_i);
            dynamicControlList.Add(5, he_so);
            dynamicControlList.Add(6, ma_phi);
            dynamicControlList.Add(7, ma_td_i);
            dynamicControlList.Add(8, ma_td2_i);
            dynamicControlList.Add(9, ma_td3_i);
            dynamicControlList.Add(10, ma_sp);

            foreach (KeyValuePair<int, Control> item in dynamicControlList)
            {
                var control = item.Value;
                ApplyControlEnterStatus(control);
            }

            //Add detail controls
            foreach (Control control in dynamicControlList.Values)
            {
                detail4.AddControl(control);
            }

            V6ControlFormHelper.SetFormStruct(detail4, _table4Struct);
        }
        
        /// <summary>
        /// Cập nhập các trường chi tiết giống AM
        /// Đang viết cứng riêng cho mỗi form
        /// </summary>
        /// <param name="dataAM"></param>
        private void UpdateDKlistAll(SortedDictionary<string, object> dataAM)
        {
            try
            {
                V6ControlFormHelper.UpdateDKlistAll(dataAM, "MA_CT,SO_THE_TS,ky,nam,ts0,Tang_giam,Ma_tg_ts".Split(','), AD);
                V6ControlFormHelper.UpdateDKlistAll(dataAM, "MA_CT,SO_THE_TS,Ts0".Split(','), data3);
                V6ControlFormHelper.UpdateDKlistAll(dataAM, "MA_CT,SO_THE_TS,ky,nam,ts0".Split(','), data4);

                V6ControlFormHelper.UpdateField2Field(AD, "Ngay_ct", "Ngay_tg");
                //FormManagerHelper.UpdateFromField2Field(data3, "A", "B");
                var tong = V6BusinessHelper.TinhTong(data4, "He_so");
                V6ControlFormHelper.UpdateDKlist(data4, "T_he_so", tong);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".UpdateDKlistAll: " + ex.Message);
            }
        }

        public override bool InsertNew()
        {
            try
            {
                ValidateData();


                if (txtSoKyKhauHao.Value > 0)
                    txtTinh_kh.Value = 1;
                else txtTinh_kh.Value = 0;

                if (dateNgayTinhKh.Value != null)
                {
                    txtKy.Value = dateNgayTinhKh.Value.Value.Month;
                    txtNam.Value = dateNgayTinhKh.Value.Value.Year;
                }
                DataDic = GetData();
                UpdateDKlistAll(DataDic);
                

                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("InsertAlts");

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
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table2Name, row);
                        }
                        if (!ok) goto fail;
                        adList = data3.ToListDataDictionary(txtSttRec.Text);
                        foreach (SortedDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table3Name, row);
                        }
                        if (!ok) goto fail;
                        adList = data4.ToListDataDictionary(txtSttRec.Text);
                        foreach (SortedDictionary<string, object> row in adList)
                        {
                            ok = ok && V6BusinessHelper.Insert(TRANSACTION, _table4Name, row);
                        }

                        ok = ok && V6BusinessHelper.Insert(TRANSACTION, "ADBPTS", DataDic);

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
                    this.ShowErrorMessage(GetType() + ".Alts InsertData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Alts InsertNew: " + ex.Message);
            }
            return false;
        }

        public override int UpdateData()
        {
            try
            {
                ValidateData();

                if (txtSoKyKhauHao.Value > 0)
                    txtTinh_kh.Value = 1;
                else txtTinh_kh.Value = 0;

                if (dateNgayTinhKh.Value != null)
                {
                    txtKy.Value = dateNgayTinhKh.Value.Value.Month;
                    txtNam.Value = dateNgayTinhKh.Value.Value.Year;
                }
                DataDic = GetData();
                UpdateDKlistAll(DataDic);

                var keys = new SortedDictionary<string, object>
                {
                    {"STT_REC", DataDic["STT_REC"]}
                };
                var keysAD = new SortedDictionary<string, object>();
                keysAD.AddRange(keys);
                keysAD.Add("TS0", txtTs0.Value);


                SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction("UpdateAlts");

                try
                {
                    //Delete AD
                    var deleteAdSql = SqlGenerator.GenDeleteSql(_table2Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
                    var deleteAd3Sql = SqlGenerator.GenDeleteSql(_table3Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd3Sql);
                    var deleteAd4Sql = SqlGenerator.GenDeleteSql(_table4Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd4Sql);

                    var _table5Struct = V6BusinessHelper.GetTableStruct("ADBPTS");
                    var deleteAd5Sql = SqlGenerator.GenDeleteSql(_table5Struct, keysAD);
                    SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAd5Sql);

                    //Update AM
                    var amSql = SqlGenerator.GenUpdateSql(V6Login.UserId, TableName.ToString(), _TableStruct, DataDic, keys);
                    var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, amSql) > 0;
                    
                    int j = 0, k = 0, l = 0;
                    //Insert AD
                    var adList = AD.ToListDataDictionary(txtSttRec.Text);
                    foreach (SortedDictionary<string, object> adRow in adList)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table2Struct, adRow, false);
                        j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    var adList3 = data3.ToListDataDictionary(txtSttRec.Text);
                    foreach (SortedDictionary<string, object> adRow in adList3)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table3Struct, adRow, false);
                        k += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }
                    var adList4 = data4.ToListDataDictionary(txtSttRec.Text);
                    foreach (SortedDictionary<string, object> adRow in adList4)
                    {
                        var adSql = SqlGenerator.GenInsertAMSql(V6Login.UserId, _table4Struct, adRow, false);
                        l += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
                    }

                    var ok = V6BusinessHelper.Insert(TRANSACTION, "ADBPTS", DataDic);

                    if (insert_success
                        && j == adList.Count
                        && k == adList3.Count
                        && l == adList4.Count
                        && ok)
                    {
                        TRANSACTION.Commit();
                        return 1;
                    }
                    else
                    {
                        TRANSACTION.Rollback();
                        this.ShowErrorMessage(GetType() + ".Alts UpdateData Transaction Rollback: "
                            + string.Format("table1:{0}, table2:{1}, table3:{2}, table4:{3}",
                            insert_success, j, k, l));
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    TRANSACTION.Rollback();
                    this.ShowErrorMessage(GetType() + ".Alts UpdateData Transaction: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Alts UpdateData: " + ex.Message);
            }
            return -1;
        }


        

        public override void ValidateData()
        {
            if (data4.Rows.Count == 0)
            {
                var newData4 = new SortedDictionary<string, object>();
                newData4["MA_BP_I"] = txtMa_bpts.Text;
                newData4["HE_SO"] = 100m;
                newData4["TK_KH_I"] = txtTk_kh.Text;
                newData4["TK_CP_I"] = txtTk_cp.Text;
                XuLyThemDetail4(newData4);
            }

            var errors = "";
            if (txtMaTaiSan.Text.Trim() == "") errors += "Chưa nhập mã!\r\n";
            if (txtTenTaiSan.Text.Trim() == "") errors += "Chưa nhập tên !\r\n";
            if (txtLyDoTang.Text.Trim() == "") errors += "Chưa nhập lý do tăng  !\r\n";
            if (txtMa_bpts.Text.Trim() == "") errors += "Chưa nhập mã bộ phận  !\r\n";

            if (V6Login.MadvcsTotal > 0 && txtMaDVCS.Text.Trim() == "")
                errors += "Chưa nhập đơn vị cơ sở !\r\n";

            if (Mode == V6Mode.Edit)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 0, "SO_THE_TS",
                 txtMaTaiSan.Text.Trim(), DataOld["SO_THE_TS"].ToString());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "SO_THE_TS = " + txtMaTaiSan.Text.Trim());
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "SO_THE_TS",
                 txtMaTaiSan.Text.Trim(), txtMaTaiSan.Text.Trim());
                if (!b)
                    throw new Exception("Không được thêm mã đã tồn tại: "
                                                    + "SO_THE_TS = " + txtMaTaiSan.Text.Trim());
            }

            if (detail1.MODE == V6Mode.Add || detail1.MODE == V6Mode.Edit ||
                detail3.MODE == V6Mode.Add || detail3.MODE == V6Mode.Edit||
                detail4.MODE == V6Mode.Add || detail4.MODE == V6Mode.Edit)
            {
                errors += "Chưa hoàn tất chi tiết!\r\n";
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void LoadDetails()
        {
            try
            {
                if(DataOld != null)
                {
                    //Load AD
                    string sttRec = DataOld["STT_REC"].ToString();
                    {
                        string sql = "SELECT * FROM " + _table2Name + "  Where stt_rec = @rec and TS0=1";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                            .Tables[0];
                        gView1.DataSource = AD;
                        gView1.HideColumnsAldm(_table2Name);
                        gView1.SetCorplan2();
                    }

                    //Data3
                    {
                        string sql = "SELECT * FROM " + _table3Name + "  Where stt_rec = @rec";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        data3 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                            .Tables[0];
                        gView3.DataSource = data3;
                        gView3.HideColumnsAldm(_table3Name);
                        gView3.SetCorplan2();
                    }

                    //Data4
                    {
                        string sql = "SELECT * FROM " + _table4Name + "  Where stt_rec = @rec and TS0=1";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        data4 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                        
                        gView4.DataSource = data4;
                        gView4.HideColumnsAldm(_table4Name);
                        gView4.SetCorplan2();
                    }

                    TinhTongCong();
                }
                else
                {
                    //Load AD
                    string sttRec = "";
                    {
                        string sql = "SELECT * FROM " + _table2Name + "  Where stt_rec = @rec and TS0=1";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        AD = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                            .Tables[0];
                        gView1.DataSource = AD;
                        gView1.HideColumnsAldm(_table2Name);
                        gView1.SetCorplan2();
                    }

                    //Data3
                    {
                        string sql = "SELECT * FROM " + _table3Name + "  Where stt_rec = @rec";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        data3 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist)
                            .Tables[0];
                        gView3.DataSource = data3;
                        gView3.HideColumnsAldm(_table3Name);
                        gView3.SetCorplan2();
                    }

                    //Data4
                    {
                        string sql = "SELECT * FROM " + _table4Name + "  Where stt_rec = @rec and TS0=1";
                        SqlParameter[] plist = { new SqlParameter("@rec", sttRec) };
                        data4 = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];
                        
                        gView4.DataSource = data4;
                        gView4.HideColumnsAldm(_table4Name);
                        gView4.SetCorplan2();
                    }
                }

                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LoadDetails: " + ex.Message, "Alts");
            }
        }

        #region ==== Detail control events ====
        
        private void XuLyDetailClickAdd()
        {
            try
            {
                SetDefaultDetail();
                ma_nv.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd: " + ex.Message);
            }
        }
        private void XuLyDetail3ClickAdd()
        {
            try
            {
                ten_ptkt.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd: " + ex.Message);
            }
        }
        private void XuLyDetail4ClickAdd()
        {
            try
            {
                ma_bp_i.Text = txtMa_bpts.Text;
                he_so.Value = 100;
                tk_kh_i.Text = txtTk_kh.Text;
                tk_cp_i.Text = txtTk_cp.Text;
                ma_bp_i.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyDetailClickAdd: " + ex.Message);
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
                _sttRec0_11 = V6BusinessHelper.GetNewSttRec0(AD);
                data["STT_REC0"] = _sttRec0_11;
                data["STT_REC"] = txtSttRec.Text;
                //Thêm thông tin...
                //data["MA_CT"] = Invoice.Mact;
                //data["NGAY_CT"] = dateNgayCT.Value.Date;

                //Kiem tra du lieu truoc khi them sua
                var error = "";
                if (!data.ContainsKey("MA_NV") || data["MA_NV"].ToString().Trim() == "") error += "\nMã nguồn vốn rỗng.";
                
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
                    gView1.DataSource = AD;
                    //TinhTongThanhToan(GetType() + "." + MethodBase.GetCurrentMethod().Name);

                    if (AD.Rows.Count > 0)
                    {
                        gView1.Rows[AD.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage(V6Text.CheckData + error);
                    return false;
                }

                TinhTongCong();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết: " + ex.Message);
            }
            return true;
        }
        private bool XuLyThemDetail3(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.AddDenied + "\nMode: " + Mode);
                return true;
            }
            try
            {
                _sttRec0_33 = V6BusinessHelper.GetNewSttRec0(data3);
                data["STT_REC0"] = _sttRec0_33;
                data["STT_REC"] = txtSttRec.Text;
                
                var error = "";
                //if (!data.ContainsKey("") || data[""].ToString().Trim() == "") error += "\nMã vật tư rỗng.";

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
                    //TinhTongThanhToan("xu ly them detail3");

                    if (data3.Rows.Count > 0)
                    {
                        gView3.Rows[data3.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage("Kiểm tra lại dữ liệu 3:" + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết 3: " + ex.Message);
            }
            return true;
        }
        private bool XuLyThemDetail4(SortedDictionary<string, object> data)
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
                //if (!data.ContainsKey("") || data[""].ToString().Trim() == "") error += "\nMã vật tư rỗng.";

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
                    //TinhTongThanhToan("xu ly them detail4");

                    if (data4.Rows.Count > 0)
                    {
                        gView4.Rows[data4.Rows.Count - 1].Selected = true;
                    }
                }
                else
                {
                    this.ShowWarningMessage("Kiểm tra lại dữ liệu 4:" + error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Thêm chi tiết 4: " + ex.Message);
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
                        if (!data.ContainsKey("MA_NV") || data["MA_NV"].ToString().Trim() == "")
                            error += "\nMã nguồn vốn rỗng.";
                        
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
                            gView1.DataSource = AD;
                            //TinhTongThanhToan("xy ly sua detail");
                        }
                        else
                        {
                            this.ShowWarningMessage(V6Text.CheckData + error);
                            return false;
                        }
                    }

                    TinhTongCong();
                }
                else
                {
                    this.ShowWarningMessage(V6Text.NoSelection);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết: " + ex.Message);
            }
            return true;
        }

        
        private bool XuLySuaDetail3(SortedDictionary<string, object> data)
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
                        //if (!data.ContainsKey("MA_NV") || data["MA_NV"].ToString().Trim() == "")
                        //    error += "\nMã nguồn vốn rỗng.";

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

        
        private bool XuLySuaDetail4(SortedDictionary<string, object> data)
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.EditDenied + " Mode: " + Mode);
                return true;
            }
            try
            {
                if (_gv4EditingRow != null)
                {
                    var cIndex = _gv4EditingRow.Index;

                    if (cIndex >= 0 && cIndex < data4.Rows.Count)
                    {
                        //Kiem tra du lieu truoc khi them sua
                        var error = "";
                        //if (!data.ContainsKey("MA_NV") || data["MA_NV"].ToString().Trim() == "")
                        //    error += "\nMã nguồn vốn rỗng.";

                        if (error == "")
                        {
                            //Sửa dòng dữ liệu.
                            var currentRow = data4.Rows[cIndex];
                            foreach (DataColumn column in data4.Columns)
                            {
                                var key = column.ColumnName.ToUpper();
                                if (data.ContainsKey(key))
                                {
                                    object value = ObjectAndString.ObjectTo(column.DataType, data[key]);
                                    currentRow[key] = value;
                                }
                            }
                            gView4.DataSource = data4;
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
                this.ShowErrorMessage(GetType() + ".Sửa chi tiết 4: " + ex.Message);
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
                if (gView1.CurrentRow != null)
                {
                    var cIndex = gView1.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < AD.Rows.Count)
                    {
                        var currentRow = AD.Rows[cIndex];
                        var details = "Mã nguồn vốn: " + currentRow["Ma_nv"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            AD.Rows.Remove(currentRow);
                            gView1.DataSource = AD;
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết: " + ex.Message);
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
                        var details = "Mã nguồn vốn: " + currentRow["Ma_nv"];
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data3.Rows.Remove(currentRow);
                            detail3.SetData(null);
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
        private void XuLyXoaDetail4()
        {
            if (Mode != V6Mode.Add && Mode != V6Mode.Edit)
            {
                this.ShowInfoMessage(V6Text.DeleteDenied + "\nMode: " + Mode);
                return;
            }
            try
            {
                if (gView4.CurrentRow != null)
                {
                    var cIndex = gView4.CurrentRow.Index;
                    if (cIndex >= 0 && cIndex < data4.Rows.Count)
                    {
                        var currentRow = data4.Rows[cIndex];
                        var details = string.Format("{0}: {1}", CorpLan2.GetFieldHeader("MA_BP_I"), currentRow["MA_BP_I"]);
                        if (this.ShowConfirmMessage(V6Text.DeleteRowConfirm + "\n" + details)
                            == DialogResult.Yes)
                        {
                            data4.Rows.Remove(currentRow);
                            detail4.SetData(null);
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
                this.ShowErrorMessage(GetType() + ".Xóa chi tiết 4: " + ex.Message);
            }
        }

        private bool ValidateData_Detail(SortedDictionary<string, object> data)
        {
            try
            {
                if (data == null || data.Count == 0)
                {
                    this.ShowWarningMessage(V6Text.NoData);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "SoDuManager AltsAddEdit ValidateData_Detail");
            }
            return true;
        }

        private bool ValidateData_Detail3(SortedDictionary<string, object> dic)
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
        private bool ValidateData_Detail4(SortedDictionary<string, object> dic)
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
                this.ShowErrorMessage(ex.Message, "SoDuManager AltsAddEdit ValidateData_Detail4");
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
            throw new Exception(V6Text.AddFail);
        }

        private void detail1_ClickEdit(object sender)
        {
            try
            {
                if (AD != null && AD.Rows.Count > 0 && gView1.DataSource != null)
                {
                    detail1.ChangeToEditMode();
                    _sttRec0_11 = ChungTu.ViewSelectedDetailToDetailForm(gView1, detail1, out _gv1EditingRow);
                    
                    ma_nv.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ClickEdit: " + ex.Message);
            }
        }
        private void detail3_ClickEdit(object sender)
        {
            try
            {
                if (data3 != null && data3.Rows.Count > 0 && gView3.DataSource != null)
                {
                    detail3.ChangeToEditMode();
                    _sttRec0_33 = ChungTu.ViewSelectedDetailToDetailForm(gView3, detail3, out _gv3EditingRow);
                    ten_ptkt.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ClickEdit: " + ex.Message);
            }
        }
        private void detail4_ClickEdit(object sender)
        {
            try
            {
                if (data4 != null && data4.Rows.Count > 0 && gView4.DataSource != null)
                {
                    detail4.ChangeToEditMode();
                    _sttRec0_44 = ChungTu.ViewSelectedDetailToDetailForm(gView4, detail4, out _gv4EditingRow);
                    ma_bp_i.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ClickEdit: " + ex.Message);
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
        private void detail3_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data) && XuLySuaDetail3(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }
        private void detail4_EditHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail4(data) &&  XuLySuaDetail4(data))
            {
                return;
            }
            throw new Exception(V6Text.EditFail);
        }

        private void Control_Load(object sender, EventArgs e)
        {
            //SetADtoGrid();
        }

        private string table2Fields = ",ma_nv,ngay_ct,so_ct,nguyen_gia,gt_da_kh,gt_cl,gt_kh_ky,dien_giai,";// from ADALTS
        private string table3Fields = ",ten_ptkt,dvt,so_luong,gia_tri,";// from ADCTTS

        private string table4Fields =
            ",ma_bp_i,ma_bpht_i,ma_vv_i,tk_kh_i,tk_cp_i,he_so,ma_phi,ma_td_i,ma_td2_i,ma_td3_i,ma_sp,";// from ADCTTSBP
        private void gView2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                var field = e.Column.DataPropertyName.ToLower();
                e.Column.Visible = table2Fields.Contains(field);

                //bool test_contains = "gt_kh_ky".Contains("ky") sai
                if (field.ToUpper() == "KY")
                    e.Column.Visible = false;



            }
            catch
            {
                // ignored
            }
        }
        private void gView3_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                var field = e.Column.DataPropertyName.ToLower();
                e.Column.Visible = table3Fields.Contains(field);

            }
            catch
            {
                // ignored
            }
        }
        private void gView4_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                var field = e.Column.DataPropertyName.ToLower();
                e.Column.Visible = table4Fields.Contains(field);

            }
            catch
            {
                // ignored
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (detail1.IsViewOrLock)
                detail1.SetData(gView1.CurrentRow.ToDataDictionary());
        }

        private void detail3_AddHandle(SortedDictionary<string, object> data)
        {
            if (ValidateData_Detail3(data) && XuLyThemDetail3(data))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
        }
        private void detail4_AddHandle(SortedDictionary<string, object> dataDic)
        {
            if (ValidateData_Detail4(dataDic) && XuLyThemDetail4(dataDic))
            {
                return;
            }
            throw new Exception(V6Text.AddFail);
        }

        private void detail3_ClickAdd(object sender)
        {
            XuLyDetail3ClickAdd();
        }

        private void detail3_ClickCancelEdit(object sender)
        {
            detail3.SetData(_gv3EditingRow.ToDataDictionary());
        }

        private void detail3_DeleteHandle(object sender)
        {
            XuLyXoaDetail3();
        }
        
        private void detail4_ClickAdd(object sender)
        {
            XuLyDetail4ClickAdd();
        }

        private void detail4_ClickCancelEdit(object sender)
        {
            detail4.SetData(_gv4EditingRow.ToDataDictionary());
        }

        private void detail4_DeleteHandle(object sender)
        {
            XuLyXoaDetail4();
        }

        private void txtTong_kh_ky_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void txtSoKyKhauHao_V6LostFocus(object sender)
        {
            XuLyThayDoiSoKyKhauHao();
        }

        private void tabDetails_Enter(object sender, EventArgs e)
        {
            if (tabDetails.SelectedTab == tabChiTiet)
            {
                detail1.AutoFocus();
            }
            else if (tabDetails.SelectedTab == tabDetailPhuTung)
            {
                detail3.AutoFocus();
            }
            else if (tabDetails.SelectedTab == tabDetailChiTietChiPhi)
            {
                detail4.AutoFocus();
            }
        }

    }
}
