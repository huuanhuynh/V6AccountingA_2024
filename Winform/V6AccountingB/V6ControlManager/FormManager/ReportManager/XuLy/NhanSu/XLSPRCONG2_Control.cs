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
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public class XLSPRCONG2_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();
        private const string TABLE_NAME = "PRCONG2";
        private string[] ID_FIELDS = "NGAY,MA_NS,MA_CONG".Split(',');
        private const string CHECK_FIELDS = "NGAY,MA_NS,MA_CONG", IDS_CHECK = "NGAY,MA_NS,MA_CONG";
        /// <summary>
        /// <para>01: IsExistOneCode_List</para>
        /// <para>21: IsValidTwoCode_OneDate(</para>
        /// </summary>
        string TYPE_CHECK = "21";
        private DataTable data;
        public XLSPRCONG2_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                var dataDic = row.ToDataDictionary();
                i++; stt++;
                try
                {
                    if (dataDic.HaveValues(ID_FIELDS))
                    {
                        if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                        {
                            remove_list_g.Add(row);
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

        private bool CheckTableHaveFields(DataTable table, IList<string> fields)
        {
            if (table == null) return false;
            foreach (string field in fields)
            {
                if (!table.Columns.Contains(field)) return false;
            }
            return true;
        }

        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (data != null)
                {
                    check_list = ObjectAndString.SplitString(CHECK_FIELDS);
                    if (CheckTableHaveFields(data, ID_FIELDS))
                    {
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
                        V6ControlFormHelper.ShowMessage("Dữ liệu không đủ thông tin");
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

                int stt = 0;
                DateTime last_day = V6Setting.M_SV_DATE;
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
                            var id_list = ObjectAndString.SplitString(IDS_CHECK);
                            var ma_ns = dataDic["MA_NS"].ToString().Trim();
                            var ma_cong = dataDic["MA_CONG"].ToString().Trim();
                            DateTime ngay = ObjectAndString.ObjectToFullDateTime(dataDic["NGAY"]);
                            last_day = ngay;
                            var valid = false;
                            switch (TYPE_CHECK)
                            {
                                case "21":
                                    valid = V6BusinessHelper.IsValidTwoCode_OneDate(
                                        TABLE_NAME, 1,
                                        "MA_NS", ma_ns, ma_ns,
                                        "MA_CONG", ma_cong, ma_cong,
                                        "NGAY", ngay.ToString("yyyyMMdd"), ngay.ToString("yyyyMMdd"));
                                    break;
                            }

                            if (FilterControl.Check2) //Chỉ cập nhập mã mới.
                            {
                                if (valid)
                                {
                                    if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                    {
                                        remove_list_d.Add(row);
                                    }
                                    else
                                    {
                                        var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ma_ns);
                                        f9Error += s;
                                        f9ErrorAll += s;
                                    }
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                if (!valid) //Xóa cũ thêm mới.
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
                                   remove_list_d.Add(row);
                                }
                                else
                                {
                                    var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, ma_ns);
                                    f9Error += s;
                                    f9ErrorAll += s;
                                }
                            }

                        }
                        else
                        {
                            var s = "Dòng " + stt + " không có đủ " + CHECK_FIELDS + "\r\n";
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

                if (total > 0)
                {
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@dWork", last_day),
                        new SqlParameter("@nUserID", V6Login.UserId)
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery("HPRCONG2", plist);
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

        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running " + index + "/" + total + ". " + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveDataRows(data);
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
