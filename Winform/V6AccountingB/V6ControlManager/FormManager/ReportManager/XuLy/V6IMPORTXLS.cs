using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public partial class V6IMPORTXLS : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private DataTable data;
        private DataTable listAl;

        private string _table_name;

        /// <summary>
        /// Cách nhau bởi ;
        /// </summary>
        private string CHECK_FIELDS;
        //{
        //    get
        //    {
        //        var result = "";
        //        if (cboDanhMuc.SelectedIndex >= 0)
        //        {
        //            result = listAl.Rows[cboDanhMuc.SelectedIndex]["khoa"].ToString().Trim();
        //        }
        //        return result;
        //    }
        //}
        private string IDS_CHECK
        {
            get
            {
                var result = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                {
                    result = listAl.Rows[cboDanhMuc.SelectedIndex]["ID_CHECK"].ToString().Trim();
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
                    result = listAl.Rows[cboDanhMuc.SelectedIndex]["TYPE_CHECK"].ToString().Trim();
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
            }
            catch (Exception ex)
            {
                
            }
        }

        
        private void LoadListALIMXLS()
        {
            listAl = V6BusinessHelper.Select("ALIMXLS", "*", "IMPORT_YN='1'", "", "stt").Data;

            cboDanhMuc.ValueMember = "dbf_im";
            cboDanhMuc.DisplayMember = V6Setting.IsVietnamese ? "Ten" : "Ten2";
            cboDanhMuc.DataSource = listAl;
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
            V6ControlFormHelper.SetStatusText2("F9 Chuyển. F10 Update.");
        }

        protected override void MakeReport2()
        {
            try
            {
                data = V6Tools.V6Convert.Excel_File
                    .Sheet1ToDataTable(txtFile.Text);
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
                        data = V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage("Chưa chọn mã nguồn và đích.");
                    }
                }
                dataGridView1.DataSource = data;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                CHECK_FIELDS = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                    CHECK_FIELDS = listAl.Rows[cboDanhMuc.SelectedIndex]["khoa"].ToString().Trim();

                if (data != null)
                {
                    check_list = CHECK_FIELDS.Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries);
                    var check_ok = true;
                    foreach (string field in check_list)
                    {
                        if (!data.Columns.Contains(field))
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
                        V6ControlFormHelper.ShowMessage(string.Format("Dữ liệu không có {0}", CHECK_FIELDS));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage("Chưa có dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
            }
        }

        private bool f9Running;
        private int total, index;

        private string f9Error = "";
        private string f9ErrorAll = "";
        private string[] check_list = {};
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9ErrorAll = "";

                if (data == null)
                {
                    f9ErrorAll = "Dữ liệu không hợp lệ!";
                    goto End;
                }

                int stt = 0;
                total = data.Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    DataRow row = data.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        var check_ok = true;
                        foreach (string field in check_list)
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
                            var id_list = IDS_CHECK.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                            var ID0 = dataDic[id_list[0].ToUpper()].ToString().Trim();
                            string ID_FIELD1, ID_FIELD2;
                            string ID1, ID2;
                            var exist = false;
                            switch (TYPE_CHECK)
                            {
                                case "00":
                                    exist = false;
                                    break;
                                case "01":
                                    exist = _categories.IsExistOneCode_List(_table_name, id_list[0], ID0);
                                    break;
                                case "02":
                                    ID_FIELD1 = id_list[1].ToUpper();
                                    ID1 = dataDic[ID_FIELD1].ToString().Trim();
                                    exist = _categories.IsExistTwoCode_List(_table_name, id_list[0], ID0, ID_FIELD1, ID1);
                                    break;
                                case "03":
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
                                        var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ID0);
                                        f9Error += s;
                                        f9ErrorAll += s;
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
                                    var keys = new SortedDictionary<string, object> ();
                                    foreach (string field in id_list)
                                    {
                                        keys.Add(field.ToUpper(), row[field]);
                                    }
                                    _categories.Delete(_table_name, keys);
                                }

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
                                    var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ID0);
                                    f9Error += s;
                                    f9ErrorAll += s;
                                }
                            }

                        }
                        else
                        {
                            var s = "Dòng " + stt + " không có đủ " + CHECK_FIELDS;
                            f9Error += s;
                            f9ErrorAll += s;
                        }
                    }
                    catch (Exception ex)
                    {
                        f9Error += "Dòng " + stt + ": " + ex.Message;
                        f9ErrorAll += "Dòng " + stt + ": " + ex.Message;
                    }

                }
            }
            catch (Exception ex)
            {
                f9Error += ex.Message;
                f9ErrorAll += ex.Message;
            }

        End:
            f9Running = false;
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
                RemoveDataRows(data);
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9Error.Length > 0 ? "Error: " : "")
                    + f9Error);

                ShowMainMessage("F9 " + V6Text.Finish);

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
                CHECK_FIELDS = "";
                if (cboDanhMuc.SelectedIndex >= 0)
                    CHECK_FIELDS = listAl.Rows[cboDanhMuc.SelectedIndex]["khoa"].ToString().Trim();

                if (data != null)
                {
                    check_list = CHECK_FIELDS.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var check_ok = true;
                    foreach (string field in check_list)
                    {
                        if (!data.Columns.Contains(field))
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
                        V6ControlFormHelper.ShowMessage(string.Format("Dữ liệu không có {0}", CHECK_FIELDS));
                    }
                }
                else
                {
                    V6ControlFormHelper.ShowMessage("Chưa có dữ liệu.");
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

                if (data == null)
                {
                    F10ErrorAll = "Dữ liệu không hợp lệ!";
                    goto End;
                }

                int stt = 0, skip = 0;
                total = data.Rows.Count;
                for (int i = 0; i < total; i++)
                {
                    DataRow row = data.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        var check_ok = true;
                        foreach (string field in check_list)
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
                            var id_list = IDS_CHECK.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            var ID0 = dataDic[id_list[0].ToUpper()].ToString().Trim();
                            var exist = false;
                            switch (TYPE_CHECK)
                            {
                                case "01":
                                    exist = _categories.IsExistOneCode_List(_table_name, id_list[0], ID0);
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
                                    var s = string.Format("Dòng {0,3}-ID:{1} sửa không được", stt, ID0);
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
                            var s = "Dòng " + stt + " không có đủ " + CHECK_FIELDS;
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
                if (cError.Length > 0)
                {
                    F10Error = F10Error.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText("F10 running "
                                                      + (cError.Length > 0 ? "Error: " : "")
                                                      + cError);
                }
            }
            else
            {
                Unlock();

                ((Timer)sender).Stop();
                RemoveDataRows(data);
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


    }
}
