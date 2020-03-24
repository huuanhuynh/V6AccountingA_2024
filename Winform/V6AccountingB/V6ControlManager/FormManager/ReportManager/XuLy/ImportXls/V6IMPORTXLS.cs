using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6IMPORTXLS : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        /// <summary>
        /// Kiem tra du lieu hop le
        /// </summary>
        private string check = null;
        private DataTable ALIMXLS_DATA;

        private string _table_name;
        private Type XLS_program;

        private DataRow SelectedRow
        {
            get
            {
                if (cboDanhMuc.DataSource != null && cboDanhMuc.SelectedItem is DataRowView && cboDanhMuc.SelectedIndex >= 0)
                {
                    return ((DataRowView)cboDanhMuc.SelectedItem).Row;
                }
                return null;
            }
        }

        /// <summary>
        /// <para>Định nghĩa kiểm tra dữ liệu ràng buộc. FIELD:Table.FIELDX.</para>
        /// <para>Các định nghĩa cách nhau bằng dấu '~'</para>
        /// </summary>
        private string ADV_AL2
        {
            get
            {
                var result = "";
                var dataRow = SelectedRow;
                if (dataRow != null)
                {
                    result = dataRow["ADV_AL2"].ToString().Trim();
                }
                return result;
            }
        }

        /// <summary>
        /// KHOA, Cách nhau bởi ;
        /// </summary>
        private string CHECK_FIELDS
        {
            get
            {
                var result = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                {
                    result = ALIMXLS_DATA.Rows[cboDanhMuc.SelectedIndex]["khoa"].ToString().Trim();
                }
                return result;
            }
        }

        private string MA_IMEX
        {
            get
            {
                var result = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                {
                    result = ALIMXLS_DATA.Rows[cboDanhMuc.SelectedIndex]["MA_IMEX"].ToString().Trim().ToUpper();
                }
                return result;
            }
        }
        private string ID_CHECK
        {
            get
            {
                var result = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                {
                    result = ALIMXLS_DATA.Rows[cboDanhMuc.SelectedIndex]["ID_CHECK"].ToString().Trim().ToUpper();
                }
                return result;
            }
        }
        private string TYPE_CHECK
        {
            get
            {
                var result = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                {
                    result = ALIMXLS_DATA.Rows[cboDanhMuc.SelectedIndex]["TYPE_CHECK"].ToString().Trim();
                }
                return result;
            }
        }

        public V6IMPORTXLS(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                FilterControl.F9 = true;
                FilterControl.F10 = true;

                LoadListALIMXLS();
                CreateXlsDmethodProgram();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init1", ex);
            }
        }

        private void CreateXlsDmethodProgram()
        {
            try
            {
                //IDictionary<string, object> keys = new Dictionary<string, object>();
                //keys.Add("MA_FILE", _program);
                
                if (ALIMXLS_DATA == null || ALIMXLS_DATA.Rows.Count == 0) return;

                string using_text = "";
                string method_text = "";

                foreach (DataRow dataRow in ALIMXLS_DATA.Rows)
                {
                    var xml = dataRow["DMETHOD"].ToString().Trim();
                    if (xml == "") continue;
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xml));
                    if (ds.Tables.Count <= 0) continue;

                    var data = ds.Tables[0];
                    foreach (DataRow event_row in data.Rows)
                    {
                        //var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                        //var method_name = event_row["method"].ToString().Trim();
                        //Event_Methods[EVENT_NAME] = method_name;

                        using_text += data.Columns.Contains("using") ? event_row["using"] : "";
                        method_text += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";
                    }
                }

                XLS_program = V6ControlsHelper.CreateProgram("XlsProgramNameSpace", "XlsProgramClass", "XlsProgram" + _program, using_text, method_text);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CreateXlsDmethodProgram", ex);
            }
        }


        private void LoadListALIMXLS()
        {
            //ALIMXLS_DATA = V6BusinessHelper.Select("ALIMXLS", "*", "IMPORT_YN='1'", "", "stt").Data;
            SqlParameter[] plist =
            {
                new SqlParameter("@isAdmin", V6Login.IsAdmin),
                new SqlParameter("@mrights", V6Login.UserRight.Mrights),
                new SqlParameter("@r_add", V6Login.UserInfo["r_add"].ToString().Trim()),
                new SqlParameter("@moduleID", V6Login.SelectedModule),
            };
            ALIMXLS_DATA = V6BusinessHelper.Select("ALIMXLS", "*", "Rtrim(MO_TA) in (Select Itemid from V6menu Where (((1=@isAdmin or (dbo.VFA_Inlist_MEMO([Itemid], @mrights)=1 and dbo.VFA_Inlist_MEMO([Itemid], @r_add)=1))) AND hide_yn<>1 AND Module_id=@moduleID))",
                "", "STT", plist).Data;

            cboDanhMuc.ValueMember = "dbf_im";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            cboDanhMuc.DataSource = ALIMXLS_DATA;
            cboDanhMuc.ValueMember = "dbf_im";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
        }

        private void Lock()
        {
            groupBox1.Enabled = false;
            btnNhan.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void Unlock()
        {
            groupBox1.Enabled = true;
            btnNhan.Enabled = true;
            btnHuy.Enabled = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(string.Format("F9 {0}, F10: {1}.", V6Text.Text("CHUYEN"), V6Text.Text("UPDATE")));
        }

        protected override void MakeReport2()
        {
            try
            {
                _tbl = Excel_File.Sheet1ToDataTable(txtFile.Text, 0, V6Options.M_MAXROWS_EXCEL);
                check = null;
                //Check1: chuyen ma, String12 A to U
                string from0 = comboBox1.Text, to0 = comboBox2.Text;
                if (chkChuyenMa.Checked)
                {
                    if (!string.IsNullOrEmpty(from0) && !string.IsNullOrEmpty(to0))
                    {
                        var from = "A";
                        if (from0.StartsWith("TCVN3")) from = "A";
                        if (from0.StartsWith("VNI")) from = "V";
                        var to = "U";
                        if (to0.StartsWith("TCVN3")) to = "A";
                        if (to0.StartsWith("VNI")) to = "V";
                        _tbl = Data_Table.ChuyenMaTiengViet(_tbl, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(V6Text.Text("NoFromTo"));
                    }
                }
                FixData();
                All_Objects["_data"] = _tbl;
                All_Objects["data"] = _tbl.Copy();
                V6ControlsHelper.InvokeMethodDynamic(XLS_program, MA_IMEX + "AFTERFIXDATA", All_Objects);
                dataGridView1.DataSource = _tbl;
                CheckDataInGridView(STATUS_INSERT);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MakeReport2", ex);
            }
        }

        private void FixData()
        {
            try
            {
                if (_tbl == null) return;
                
                //FixData
                List<DataRow> remove_list = new List<DataRow>();
                var check_fields1 = ObjectAndString.SplitString(CHECK_FIELDS);
                foreach (DataRow row in _tbl.Rows)
                {
                    bool remove = false;
                    foreach (string field in check_fields1)
                    {
                        if (!_tbl.Columns.Contains(field) || row[field] == null || row[field].ToString().Trim() == "")
                        {
                            remove = true;
                            break;
                        }
                    }
                    if(remove) remove_list.Add(row);
                }

                foreach (DataRow row in remove_list)
                {
                    _tbl.Rows.Remove(row);
                }

                //Thêm cột Excel_status
                if (!_tbl.Columns.Contains(EXCEL_STATUS))
                {
                    _tbl.Columns.Add(EXCEL_STATUS, typeof (string));
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixData", ex);
            }
        }

        private string EXCEL_STATUS = "EXCEL_STATUS";
        private string STATUS_CANCEL = "CANCEL", STATUS_INSERT = "INSERT", STATUS_CHECKINFO = "CHECKINFO", STATUS_UPDATE = "UPDATE";
        /// <summary>
        /// Kiểm tra và đánh dấu dữ liệu ko hợp lệ trên gridView.
        /// <para>Thêm thông tin Excel_status</para>
        /// </summary>
        private void CheckDataInGridView(string STATUS)
        {
            try
            {
                string adv_al2 = ADV_AL2;
                if (string.IsNullOrEmpty(adv_al2))
                {
                    if (_tbl.Columns.Contains(EXCEL_STATUS))
                    {
                        foreach (DataRow row in _tbl.Rows)
                        {
                            row[EXCEL_STATUS] = STATUS;
                        }
                    }
                    return;
                }
                var check_parts = adv_al2.Split('~');
                foreach (DataGridViewRow grow in dataGridView1.Rows)
                {
                    foreach (string part in check_parts)
                    {
                        string field, table, field1;
                        var s_ss = part.Split(':');
                        if (s_ss.Length != 2) continue;
                        field = s_ss[0];
                        var t_f = s_ss[1].Split('.');
                        if (t_f.Length != 2) continue;
                        //Kiem tra du lieu hop le
                        table = t_f[0];
                        field1 = t_f[1];
                        if (DuLieuHopLe(grow, field, table, field1))
                        {
                            grow.Cells[EXCEL_STATUS].Value = STATUS;
                            continue;
                        }
                        else
                        {
                            grow.DefaultCellStyle.BackColor = Color.Red;
                            grow.Cells[EXCEL_STATUS].Value = STATUS_CHECKINFO;
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".CheckDataInGridView", ex);
            }
        }

        private bool DuLieuHopLe(DataGridViewRow grow, string field, string table, string field1)
        {
            IDictionary<string, object> checkData = new SortedDictionary<string, object>();
            checkData.Add(field1.ToUpper(), grow.Cells[field].Value);
            return V6BusinessHelper.CheckDataExistStruct(table, checkData);
        }

        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (!string.IsNullOrEmpty(check))
                {
                    this.ShowWarningMessage(V6Text.Text("KiemTraDuLieu") + check);
                    return;
                }
                if (_tbl != null)
                {
                    All_Objects["_data"] = _tbl;
                    All_Objects["data"] = _tbl.Copy();
                    V6ControlsHelper.InvokeMethodDynamic(XLS_program, MA_IMEX + "F9", All_Objects);

                    check_field_list = CHECK_FIELDS.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries);
                    var check_ok = true;
                    foreach (string field in check_field_list)
                    {
                        if (!_tbl.Columns.Contains(field))
                        {
                            check_ok = false;
                            break;
                        }
                    }
                    if (check_ok)
                    {
                        Lock();

                        Timer timerF9 = new Timer { Interval = 1000 };
                        timerF9.Tick += tF9_Tick;
                        remove_list_d = new List<DataRow>();
                        Thread t = new Thread(F9Thread);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        timerF9.Start();
                        V6ControlFormHelper.SetStatusText("F9 running");
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(string.Format(V6Text.Text("DULIEUBITHIEU") + " {0}", CHECK_FIELDS));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage(V6Text.Text("NODATA"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        /// <summary>
        /// Tổng cộng, Vị trí đang chạy.
        /// </summary>
        private int total, index;

        private string f9Error = "";
        private string f9MessageAll = "";
        private string[] check_field_list = {};
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9MessageAll = "";

                if (_tbl == null)
                {
                    f9MessageAll = V6Text.Text("INVALIDDATA");
                    goto End;
                }

                int stt = 0;
                total = _tbl.Rows.Count;
                var id_list = ID_CHECK.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < total; i++)
                {
                    DataRow row = _tbl.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        // Bỏ qua các dòng có đánh dấu khác Insert.
                        if (_tbl.Columns.Contains(EXCEL_STATUS))
                        {
                            var row_status = row[EXCEL_STATUS].ToString().Trim();
                            if (row_status != STATUS_INSERT)
                            {
                                continue;
                            }
                        }

                        // Kiểm tra có các cột cần thiết
                        var check_ok = true;
                        foreach (string field in check_field_list)
                        {
                            if (row[field] == null || row[field] == DBNull.Value || row[field].ToString().Trim() == "")
                            {
                                check_ok = false;
                                break;
                            }
                        }
                        // Nếu kiểm tra ok thì thực hiện F9
                        if (check_ok)
                        {
                            var dataDic = row.ToDataDictionary();
                            //fix dataDic id fields.
                            foreach (string id in id_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }
                            foreach (string id in check_field_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }

                            
                            var keys = new SortedDictionary<string, object>();
                            foreach (string id in id_list)
                            {
                                keys.Add(id, row[id]);
                            }
                            var ID0 = dataDic[id_list[0].ToUpper()].ToString().Trim();
                            string ID_FIELD1, ID_FIELD2;
                            string ID1, ID2;
                            var exist = false;
                            switch (TYPE_CHECK)
                            {
                                case "AL":
                                    exist = V6BusinessHelper.CheckDataExistStruct(_table_name , keys);
                                    break;
                                case "00":// All
                                    exist = false;
                                    break;
                                case "01":// OneCode
                                    exist = _categories.IsExistOneCode_List(_table_name, id_list[0], ID0);
                                    break;
                                case "02"://TwoCode
                                    ID_FIELD1 = id_list[1].ToUpper();
                                    ID1 = dataDic[ID_FIELD1].ToString().Trim();
                                    exist = _categories.IsExistTwoCode_List(_table_name, id_list[0], ID0, ID_FIELD1, ID1);
                                    break;
                                case "03"://ThreeCode
                                    ID_FIELD1 = id_list[1];
                                    ID_FIELD2 = id_list[2].ToUpper();
                                    ID1 = dataDic[ID_FIELD1].ToString().Trim();
                                    ID2 = dataDic[ID_FIELD2].ToString().Trim();
                                    exist = V6BusinessHelper.IsExistThreeCode_List(_table_name,
                                        id_list[0], ID0, ID_FIELD1, ID1, ID_FIELD2, ID2);
                                    break;
                                
                            }
                            
                            if (chkChiNhapMaMoi.Checked) //Chỉ cập nhập mã mới.
                            {
                                if (!exist)
                                {
                                    InvokeBeforeInsert(dataDic);
                                    if (V6BusinessHelper.Insert(_table_name, dataDic))
                                    {
                                        if (_table_name.ToUpper() == "ALVT")
                                        {
                                            var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                            UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                        }
                                        remove_list_d.Add(row);
                                    }
                                    else
                                    {
                                        var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ID0, V6Text.Text("ADD0"));
                                        f9Error += s;
                                        f9MessageAll += s;
                                    }
                                }
                                else
                                {
                                    f9Error += "Skip " + ID0;
                                }
                            }
                            else
                            {
                                if (exist) //Xóa cũ thêm mới.
                                {
                                    keys = new SortedDictionary<string, object> ();
                                    foreach (string field in id_list)
                                    {
                                        keys.Add(field.ToUpper(), row[field]);
                                    }
                                    _categories.Delete(_table_name, keys);
                                }

                                InvokeBeforeInsert(dataDic);
                                if (V6BusinessHelper.Insert(_table_name, dataDic))
                                {
                                    if (_table_name.ToUpper() == "ALVT")
                                    {
                                        var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                        UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                    }
                                    remove_list_d.Add(row);
                                }
                                else
                                {
                                    var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ID0, V6Text.Text("ADD0"));
                                    f9Error += s;
                                    f9MessageAll += s;
                                }
                            }

                        }
                        else
                        {
                            var s = string.Format(V6Text.Text("DONG0KOCODU1"), stt, CHECK_FIELDS);
                            f9Error += s;
                            f9MessageAll += s;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Error += "Dòng " + stt + ": " + ex.Message;
                        f9MessageAll += "Dòng " + stt + ": " + ex.Message;
                    }

                }
            }
            catch (Exception ex)
            {
                f9Error += ex.Message;
                f9MessageAll += ex.Message;
            }

        End:
            f9Running = false;
        }

        /// <summary>
        /// Gọi hàm động sửa dữ liệu trước khi thêm vào csdl.
        /// </summary>
        /// <param name="dataDic"></param>
        private void InvokeBeforeInsert(IDictionary<string, object> dataDic)
        {
            try
            {
                All_Objects["dataDic"] = dataDic;
                V6ControlsHelper.InvokeMethodDynamic(XLS_program, MA_IMEX + "BEFOREINSERT", All_Objects);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".InvokeBeforeInsert", ex);
            }
        }

        private void UpdateAlqddvt(string ma_vt_old, string ma_vt_new)
        {
            try
            {
                //var  = DataDic["MA_VT"].ToString().Trim();
                //var  = EditMode == V6Mode.Edit ? DataEdit["MA_VT"].ToString().Trim() : ma_vt_new;

                V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_UPDATE_ALQDDVT",
                    new SqlParameter("@cMa_vt_old", ma_vt_old),
                    new SqlParameter("@cMa_vt_new", ma_vt_new));
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".UpdateAlqddvt: " + ex.Message);
            }
        }

        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running " + index + "/" + total + ". "  + cError);
            }
            else
            {
                Unlock();
                ((Timer)sender).Stop();
                RemoveDataRows(_tbl);
                V6ControlsHelper.InvokeMethodDynamic(XLS_program, MA_IMEX + "AFTERF9", All_Objects);
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9Error.Length > 0 ? "Error: " : "")
                    + f9Error);

                ShowMainMessage("F9 " + V6Text.Finish + " " + f9MessageAll);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        #endregion xử lý F9

        #region ==== Xử lý F10 ====
        protected override void XuLyF10()
        {
            try
            {
                if (_tbl != null)
                {
                    // 07/12/2017 Xu ly update
                    CheckDataInGridView(STATUS_UPDATE);


                    check_field_list = CHECK_FIELDS.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var check_ok = true;
                    foreach (string field in check_field_list)
                    {
                        if (!_tbl.Columns.Contains(field))
                        {
                            check_ok = false;
                            break;
                        }
                    }
                    if (check_ok)
                    {
                        Lock();

                        Timer timerF10 = new Timer { Interval = 1000 };
                        timerF10.Tick += tF10_Tick;
                        remove_list_d = new List<DataRow>();
                        Thread t = new Thread(F10Thread);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        timerF10.Start();
                        V6ControlFormHelper.SetStatusText("F10 running");
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(string.Format(V6Text.Text("DULIEUBITHIEU") + " {0}", CHECK_FIELDS));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage(V6Text.Text("NODATA"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF10: " + ex.Message);
            }
        }

        private bool F10Running;
        private string F10Error = "";
        private string F10ErrorAll = "";
        
        private void F10Thread()
        {
            try
            {
                F10Running = true;
                F10ErrorAll = "";

                if (_tbl == null)
                {
                    F10ErrorAll = V6Text.Text("INVALIDDATA");
                    goto End;
                }

                int stt = 0, skip = 0;
                total = _tbl.Rows.Count;
                var id_list = ID_CHECK.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                

                for (int i = 0; i < total; i++)
                {
                    DataRow row = _tbl.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        // Bỏ qua các dòng có đánh dấu khác Insert.
                        if (_tbl.Columns.Contains(EXCEL_STATUS))
                        {
                            var row_status = row[EXCEL_STATUS].ToString().Trim();
                            if (row_status != STATUS_UPDATE)
                            {
                                continue;
                            }
                        }

                        // Kiểm tra có các cột cần thiết
                        var check_ok = true;
                        foreach (string field in check_field_list)
                        {
                            if (row[field] == null || row[field] == DBNull.Value || row[field].ToString().Trim() == "")
                            {
                                check_ok = false;
                                break;
                            }
                        }
                        if (check_ok)
                        {
                            var dataDic = row.ToDataDictionary();
                            //fix dataDic id fields.
                            foreach (string id in id_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }
                            foreach (string id in check_field_list)
                            {
                                if (dataDic[id] is string)
                                {
                                    dataDic[id] = ObjectAndString.TrimSpecial(dataDic[id].ToString());
                                }
                            }

                            var ID0 = dataDic[id_list[0].ToUpper()].ToString().Trim();
                            var keys1 = new SortedDictionary<string, object>();
                            foreach (string id in id_list)
                            {
                                keys1.Add(id, row[id]);
                            }
                            
                            string ID_FIELD1, ID_FIELD2;
                            string ID1, ID2;
                            var exist = false;

                            switch (TYPE_CHECK)
                            {
                                case "AL":
                                    exist = V6BusinessHelper.CheckDataExistStruct(_table_name, keys1);
                                    break;
                                case "00":// All
                                    exist = false;
                                    break;
                                case "01":// OneCode
                                    exist = _categories.IsExistOneCode_List(_table_name, id_list[0], ID0);
                                    break;
                                case "02"://TwoCode
                                    ID_FIELD1 = id_list[1].ToUpper();
                                    ID1 = dataDic[ID_FIELD1].ToString().Trim();
                                    exist = _categories.IsExistTwoCode_List(_table_name, id_list[0], ID0, ID_FIELD1, ID1);
                                    break;
                                case "03"://ThreeCode
                                    ID_FIELD1 = id_list[1];
                                    ID_FIELD2 = id_list[2].ToUpper();
                                    ID1 = dataDic[ID_FIELD1].ToString().Trim();
                                    ID2 = dataDic[ID_FIELD2].ToString().Trim();
                                    exist = V6BusinessHelper.IsExistThreeCode_List(_table_name,
                                        id_list[0], ID0, ID_FIELD1, ID1, ID_FIELD2, ID2);
                                    break;

                            }

                            if (exist)
                            {
                                SortedDictionary<string, object> keys = new SortedDictionary<string, object>();
                                foreach (string khoa in id_list)
                                {
                                    var KEY_FIELD = khoa.ToUpper();
                                    keys[KEY_FIELD] = dataDic[KEY_FIELD];
                                    dataDic.Remove(KEY_FIELD);
                                }
                                if (V6BusinessHelper.UpdateSimple(_table_name, dataDic, keys) > 0)
                                {
                                    remove_list_d.Add(row);
                                }
                                else
                                {
                                    skip++;
                                    var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ID0, V6Text.Text("SUA0"));
                                    F10Error += s;
                                    F10ErrorAll += s;
                                }
                            }
                            else
                            {
                                skip++;
                            }
                        }
                        else
                        {
                            skip++;
                            var s = string.Format(V6Text.Text("DONG0KOCODU1"), stt, CHECK_FIELDS);
                            F10Error += s;
                            F10ErrorAll += s;
                        }
                    }
                    catch (Exception ex)
                    {
                        F10Error += "Dòng " + stt + ": " + ex.Message;
                        F10ErrorAll += "Dòng " + stt + ": " + ex.Message;
                    }

                }
            }
            catch (Exception ex)
            {
                F10Error += ex.Message;
                F10ErrorAll += ex.Message;
            }

        End:
            F10Running = false;
        }

        void tF10_Tick(object sender, EventArgs e)
        {
            if (F10Running)
            {
                var cError = F10Error;
                V6ControlFormHelper.SetStatusText("F10 running " + index + "/" + total + ". " + cError);
            }
            else
            {
                Unlock();

                ((Timer)sender).Stop();
                RemoveDataRows(_tbl);
                V6ControlFormHelper.SetStatusText("F10 finish "
                    + (F10Error.Length > 0 ? "Error: " : "")
                    + F10Error);

                V6ControlFormHelper.ShowMainMessage("F10 finish!");

                //V6ControlFormHelper.ShowInfoMessage("F10 finish "
                //    + (F10ErrorAll.Length > 0 ? "Error: " : "")
                //    + F10ErrorAll);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        #endregion xử lý F10

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string file = V6ControlFormHelper.ChooseExcelFile(this);
                if (!string.IsNullOrEmpty(file))
                {
                    txtFile.Text = file;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void chkChuyenMa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChuyenMa.Checked)
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _table_name = cboDanhMuc.SelectedValue.ToString().Trim();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnXemMauExcel_Click(object sender, EventArgs e)
        {
            V6ControlFormHelper.OpenExcelTemplate(_table_name + "_ALL.XLS", "IMPORT_EXCEL");
        }


    }
}
