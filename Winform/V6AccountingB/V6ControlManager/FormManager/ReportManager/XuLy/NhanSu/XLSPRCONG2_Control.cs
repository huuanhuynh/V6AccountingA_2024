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
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public class XLSPRCONG2_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();

        private const string TABLE_NAME = "PRCONG2";
        private string[] ID_FIELDS = "NGAY,MA_NS".Split(',');
        private string CHECK_FIELDS = "NGAY,MA_NS", IDS_CHECK = "NGAY,MA_NS,MA_CONG";
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
            V6ControlFormHelper.SetStatusText2(string.Format("F9: {0}", V6Text.Text("CHUYEN")));
        }

        protected override void MakeReport2()
        {
            try
            {
                FilterControl.UpdateValues();

                data = Excel_File.AllSheetToDataTable(FilterControl.String1);
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
                        data = Data_Table.ChuyenMaTiengViet(data, from, to);
                    }
                    else
                    {
                        V6ControlFormHelper.ShowMessage(V6Text.Text("NoFromTo"));
                    }
                }
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MakeReport2", ex);
            }
        }

        
        private bool CheckTableHaveFields(DataTable table, IList<string> fields)
        {
            if (table == null) return false;
            foreach (string field in fields)
            {
                if (!table.Columns.Contains(field)) return false;
            }
            return true;
        }

        private string danhSachCacCot;
        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
                if (data != null)
                {
                    //"GIO:X,SL_TD2:O,SL_TD3:D,GC_TD1";
                    int soMaCong = Convert.ToInt32(FilterControl.Number1);
                    //"GIO,SL_TD2,SL_TD3,GC_TD1";
                    var dsCot = FilterControl.ObjectDictionary["DSCOT"].ToString().Trim().Split(',');
                    string temp1 = "";
                    string checkfield = "";
                    string[] field_ma_congs;
                    for (int j = 0; j < soMaCong; j++)
                    {
                        field_ma_congs = dsCot[j].Split(':');
                        checkfield = field_ma_congs[0];
                        if (checkfield != "")
                        {
                            if (temp1 != "")
                            {
                                temp1 = temp1 + "," + checkfield;
                            }
                            else
                            {
                                temp1 = checkfield;
                            }
                        }
                    }
                    if (temp1.Length > 0)
                    {
                        danhSachCacCot = temp1;
                        checkfield = CHECK_FIELDS + "," + temp1;
                        temp1 = "NGAY,MA_NS," + temp1;
                    }
                    else
                    {
                        checkfield = CHECK_FIELDS;
                        temp1 = "NGAY,MA_NS";
                        danhSachCacCot = "";
                    }

                    string[] ID_FIELDS_NEW = temp1.Split(',');


                    check_list = ObjectAndString.SplitString(checkfield);

                    if (CheckTableHaveFields(data, ID_FIELDS_NEW))
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
                        V6ControlFormHelper.ShowMessage(V6Text.Text("LACKINFO"));
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
        private int total, index;

        private string f9Error = "";
        private string f9ErrorAll = "";
        private string[] check_list = { };

        private bool CheckValidDSCot(IDictionary<string, object> currentRow, string dsFields)
        {
            string[] columns = dsFields.Split(',');
            bool result = false;
            foreach (var field in columns)
            {
                if (ObjectAndString.ObjectToString(currentRow[field]) != "")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private void F9Thread()
        {
            try
            {
                f9Running = true;
                f9ErrorAll = "";

                if (data == null)
                {
                    f9ErrorAll = V6Text.Text("INVALIDDATA");
                    goto End;
                }
                int soCot = Convert.ToInt32(FilterControl.Number1);
                var dsCot = FilterControl.ObjectDictionary["DSCOT"].ToString().Trim().Split(',');
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
                        var dataDic = row.ToDataDictionary();

                        var check_ok = false;
                        //foreach (string field in check_list)
                        //{
                        //Kiem tra xem 1 trong 4 cot cuoi co thoa man dieu kien.
                        bool checkResult1 = CheckValidDSCot(dataDic, danhSachCacCot);
                        bool checkResult2 = (ObjectAndString.ObjectToDate(dataDic["NGAY"]) != null) && (ObjectAndString.ObjectToString(dataDic["MA_NS"])!= "");
                        //}
                        check_ok = checkResult1 && checkResult2;
                        if (check_ok)
                        {
                            var id_list = ObjectAndString.SplitString(IDS_CHECK);
                            var ma_cong = "";
                            var ma_ns = dataDic["MA_NS"].ToString().Trim();
                            DateTime ngay = ObjectAndString.ObjectToFullDateTime(dataDic["NGAY"]);
                            last_day = ngay;
                            var valid = false;
                            if (soCot == 0)
                            {
                                soCot = 1;
                            }
                            var insert_ok = false;
                            for (int j = 0; j < soCot; j++)
                            {
                                bool check = false;
                                var field_ma_cong = dsCot[j].Split(':');
                                if (field_ma_cong.Length == 2)
                                {
                                    if (dataDic.ContainsKey(field_ma_cong[0]))
                                    {

                                        dataDic["MA_CONG"] = field_ma_cong[1];
                                        dataDic["GIO"] = dataDic[field_ma_cong[0]];
                                        
                                        ma_cong = field_ma_cong[1];
                                        if (ma_cong == "" || ObjectAndString.ObjectToDecimal(dataDic["GIO"]) == 0)
                                        {
                                            check = false;
                                        }
                                        else
                                        {
                                            check = true;
                                        }
                                        
                                    }
                                }
                                else
                                {
                                    if (dataDic.ContainsKey(field_ma_cong[0]))
                                    {

                                        dataDic["MA_CONG"] = dataDic[field_ma_cong[0]]; 
                                        dataDic["GIO"] = 0;
                                        ma_cong = dataDic[field_ma_cong[0]].ToString();
                                        if (ma_cong == "")
                                        {
                                            check = false;
                                        }
                                        else
                                        {
                                            check = true;
                                        }

                                    }
                                }
                                if (check)
                                {

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
                                    // insert

                                    if (FilterControl.Check2) //Chỉ cập nhập mã mới.
                                    {
                                        if (valid)
                                        {
                                            if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                                            {
                                                insert_ok = true;
                                                //remove_list_d.Add(row);
                                            }
                                            else
                                            {
                                                var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ma_ns, V6Text.Text("ADD0"));
                                                f9Error += s;
                                                f9ErrorAll += s;
                                            }
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
                                            //remove_list_d.Add(row);
                                            insert_ok = true;
                                        }
                                        else
                                        {
                                            var s = string.Format("Dòng {0,3}-ID:{1} {2}", stt, ma_ns, V6Text.Text("ADD0"));
                                            f9Error += s;
                                            f9ErrorAll += s;
                                        }
                                    }

                                }
                            }
                            if (insert_ok)
                            {
                                remove_list_d.Add(row);
                            }

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
                ShowMainMessage("F9 " + V6Text.Finish + " " + f9ErrorAll);
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
