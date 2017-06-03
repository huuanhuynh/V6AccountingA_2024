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
using V6Structs;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class XLSALVT_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string TABLE_NAME = "ALVT", ID_FIELD = "MA_VT", NAME_FIELD = "TEN_VT";
        private const string CHECK_FIELDS = "MA_VT", IDS_CHECK = "MA_VT", TYPE_CHECK="01";//S Cách nhau bởi (;)
        private DataTable data;
        public XLSALVT_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {

        }
        
        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9 Chuyển");
        }

        protected override void MakeReport2()
        {
            try
            {
                FilterControl.UpdateValues();

                data = V6Tools.V6Convert.Excel_File
                    .Sheet1ToDataTable(FilterControl.String1);
                if (FilterControl.Check1)
                {
                    if (!string.IsNullOrEmpty(FilterControl.String2) && !string.IsNullOrEmpty(FilterControl.String3))
                    {
                        var from = "A";
                        if (FilterControl.String2.StartsWith("TCVN3")) from = "A";
                        if (FilterControl.String2.StartsWith("VNI")) from = "V";
                        var to = "U";
                        if (FilterControl.String3.StartsWith("TCVN3")) to = "A";
                        if (FilterControl.String3.StartsWith("VNI")) to = "V";
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

        #region ==== Xử lý F8 ==== Đang chạy giống F9 (copy chưa sửa lại)
        protected override void XuLyF8()
        {
            return;
            try
            {
                Timer tF8 = new Timer();
                tF8.Interval = 500;
                tF8.Tick += tF8_Tick;
                Thread t = new Thread(F8Thread);
                CheckForIllegalCrossThreadCalls = false;
                t.IsBackground = true;
                t.Start();
                tF8.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF8: " + ex.Message);
            }
        }

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0, stt = 0;

            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++; stt++;
                try
                {
                    if (row.Cells[ID_FIELD].Value != DBNull.Value && row.Cells[ID_FIELD].Value.ToString().Trim() != ""
                        && row.Cells[NAME_FIELD].Value != DBNull.Value && row.Cells[NAME_FIELD].Value.ToString().Trim() != "")
                    {
                        var dataDic = new SortedDictionary<string, object>();
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            dataDic.Add(dataGridView1.Columns[j].DataPropertyName, row.Cells[j].Value);
                        }

                        if (V6BusinessHelper.Insert(V6TableName.Alvt, dataDic))
                        {
                            remove_list_g.Add(row);
                            //i--;
                        }
                        else
                        {
                            F8Error += "Dòng " + stt + " thêm không được.";
                            F8ErrorAll += "Dòng " + stt + " thêm không được.";
                        }
                    }
                    else
                    {
                        F8Error += "Dòng " + stt + " không có đủ MA_KH và TEN_KH.";
                        F8ErrorAll += "Dòng " + stt + " không có đủ MA_KH và TEN_KH.";
                    }
                }
                catch (Exception ex)
                {

                    F8Error += "Dòng " + stt + ": " + ex.Message;
                    F8ErrorAll += "Dòng " + stt + ": " + ex.Message;
                }

            }
            F8Running = false;
        }

        void tF8_Tick(object sender, EventArgs e)
        {
            if (F8Running)
            {
                var cError = F8Error;
                F8Error = F8Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (cError.Length > 0 ? "Error: " : "")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                //btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F8 running "
                    + (F8Error.Length > 0 ? "Error: " : "")
                    + F8Error);

                this.ShowErrorMessage(GetType() + ".F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);
                V6ControlFormHelper.ShowMainMessage("F8 finish!");
            }
        }
        #endregion xử lý F8


        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (data != null)
                {
                    check_list = CHECK_FIELDS.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Columns.Contains(ID_FIELD) && data.Columns.Contains(NAME_FIELD))
                    {
                        Timer timerF9 = new Timer { Interval = 1000 };
                        timerF9.Tick += tF9_Tick;
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
                        V6ControlFormHelper.ShowMessage(string.Format("Dữ liệu không có {0} và {1}", ID_FIELD, NAME_FIELD));
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
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string[] check_list = { };
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

                int stt = 0, skip = 0;

                while (data.Rows.Count > skip)
                {
                    DataRow row = data.Rows[skip];
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
                            var ID0 = dataDic[id_list[0]].ToString().Trim();
                            var exist = false;
                            switch (TYPE_CHECK)
                            {
                                case "01":
                                    exist = _categories.IsExistOneCode_List(TABLE_NAME, id_list[0], ID0);
                                    break;
                            }

                            if (FilterControl.Check2) //Chỉ cập nhập mã mới.
                            {
                                if (!exist)
                                {
                                    if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                    {
                                        var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                        UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                        data.Rows.Remove(row);
                                    }
                                    else
                                    {
                                        skip++;
                                        var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ID0);
                                        f9Error += s;
                                        f9ErrorAll += s;
                                    }
                                }
                                else
                                {
                                    skip++;
                                }
                            }
                            else
                            {
                                if (exist) //Xóa cũ thêm mới.
                                {
                                    var keys = new SortedDictionary<string, object>();
                                    foreach (string field in id_list)
                                    {
                                        keys.Add(field.ToUpper(), row[field]);
                                    }
                                    _categories.Delete(TABLE_NAME, keys);
                                }

                                if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                {
                                    var ma_vt_new = dataDic["MA_VT"].ToString().Trim();
                                    UpdateAlqddvt(ma_vt_new, ma_vt_new);
                                    data.Rows.Remove(row);
                                }
                                else
                                {
                                    skip++;
                                    var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ID0);
                                    f9Error += s;
                                    f9ErrorAll += s;
                                }
                            }

                        }
                        else
                        {
                            skip++;
                            var s = "Dòng " + stt + " không có đủ " + CHECK_FIELDS;
                            f9Error += s;
                            f9ErrorAll += s;
                        }
                    }
                    catch (Exception ex)
                    {
                        skip++;
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
                if (cError.Length > 0)
                {
                    f9Error = f9Error.Substring(cError.Length);
                    V6ControlFormHelper.SetStatusText("F9 running "
                                                      + (cError.Length > 0 ? "Error: " : "")
                                                      + cError);
                }
            }
            else
            {
                ((Timer)sender).Stop();
                //btnNhan.PerformClick();
                SetStatusText("F9 finish " + (f9Error.Length > 0 ? "Error: " : "") + f9Error);
                ShowMainMessage("F9 " + V6Text.Finish);
                this.ShowInfoMessage("F9 " + V6Text.Finish
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        #endregion xử lý F9

        
    }
}
