using System;
using System.Collections.Generic;
using System.Data;
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
    public class XLSHRPERSONAL_Control : XuLyBase
    {
        private readonly V6Categories _categories = new V6Categories();

        private const string TABLE_NAME = "HRPERSONAL";
        //  private string[] ID_FIELDS = "NGAY,MA_NS".Split(',');
        private string ID_FIELDS = "EMP_ID";
        private string CHECK_FIELDS = "EMP_ID", IDS_CHECK = "EMP_ID";
        /// <summary>
        /// <para>01: IsExistOneCode_List</para>
        /// <para>21: IsValidTwoCode_OneDate(</para>
        /// </summary>
        string TYPE_CHECK = "21";
        private DataTable data;
        public XLSHRPERSONAL_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
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
                int beginRow = (int) FilterControl.Number2 - 1;
                data = V6Tools.V6Convert.Excel_File
                    .Sheet1ToDataTable(FilterControl.String1, beginRow);
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
                    //"GIO:X,SL_TD2:D,SL_TD3:O,GC_TD1";
                    int soCot = Convert.ToInt32(FilterControl.Number1);
                    //"GIO,SL_TD2,SL_TD3,GC_TD1";
                    var danhSachFied1 = FilterControl.ObjectDictionary["DSCOT1"].ToString().Trim().Split(',');
                    string temp1 = "";
                    string checkfield = "";
                    string[] field_genders;
                    //cắt chuỗi lần 2
                    for (int j = 0; j < soCot; j++)
                    {
                        field_genders = danhSachFied1[j].Split(':');
                        checkfield = field_genders[0];
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
                        temp1 = "EMP_ID," + temp1;
                    }
                    else
                    {
                        checkfield = CHECK_FIELDS;
                        temp1 = "EMP_ID";
                        danhSachCacCot = "";
                    }

                    string[] ID_FIELDS_NEW = temp1.Split(',');


                    check_list = ObjectAndString.SplitString(checkfield);
                    DataTable table = new DataTable();
                    if (!table.Columns.Contains("EMP_ID"))
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
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }

        private bool f9Running;
        private int total, index;

        private string f9Error = "";
        private string f9ErrorAll = "";
        private string[] check_list = { };

        private bool CheckValidDSCot(SortedDictionary<string, object> currentRow, string dsFields)
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
                    f9ErrorAll = "Dữ liệu không hợp lệ!";
                    goto End;
                }
                int soCot = Convert.ToInt32(FilterControl.Number1);
                var dsCot1 = FilterControl.ObjectDictionary["DSCOT1"].ToString().Trim();
                var dsCot2 = FilterControl.ObjectDictionary["DSCOT2"].ToString().Trim();
                
                int stt = 0;
                //DateTime last_day = V6Setting.M_SV_DATE;
                total = data.Rows.Count;

                for (int i = 0; i < total; i++)
                {
                    DataRow row = data.Rows[i];
                    index = i;
                    stt++;
                    try
                    {
                        
                        XuLyThemMotNhanSu(stt, row, soCot, dsCot1, dsCot2);
                        

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

        private void XuLyThemMotNhanSu(int stt, DataRow row, int soCot, string danhSachCot, string danhSachCot2)
        {
            var dataDic = row.ToDataDictionary();
            var check_ok = false;
            //foreach (string field in check_list)
            //{
            //Kiem tra xem 1 trong 4 cot cuoi co thoa man dieu kien.
            var checkResult1 = CheckValidDSCot(dataDic, danhSachCacCot);
            bool checkResult2 = ObjectAndString.ObjectToString(dataDic["EMP_ID"]) != "";
            //}
            check_ok = checkResult1 && checkResult2;

            if (check_ok)
            {
                var id_list = ObjectAndString.SplitString(IDS_CHECK);
                var emp_id = dataDic["EMP_ID"].ToString().Trim();

                var valid = false;
                if (soCot == 0)
                {
                    soCot = 1;
                }
                var insert_ok = false;
                bool check = check_ok;

                var splitColums = danhSachCot.Split(',');
                if (soCot > splitColums.Length) soCot = splitColums.Length;

                for (int j = 0; j < soCot; j++)
                {
                    var excel_field_value = splitColums[j].Split(':');
                    string EXCEL_FIELD = excel_field_value[0].Trim().ToUpper();
                    // TRẢ VỀ S11,1
                    if (excel_field_value.Length > 1)
                    {
                        if (dataDic.ContainsKey(EXCEL_FIELD) && dataDic[EXCEL_FIELD].ToString().Trim() == "1")
                        {
                            string field_value = excel_field_value[1];
                            string[] ss = field_value.Split('=');
                            if (ss.Length == 2)
                            {
                                dataDic[ss[0].ToUpper()] = ss[1];
                            }
                        }
                    }
                }

                if (check)
                {

                    switch (TYPE_CHECK)
                    {
                        case "21":
                            valid = V6BusinessHelper.IsValidOneCode_Full(
                                TABLE_NAME, 1,
                                "EMP_ID", emp_id, emp_id
                                );
                            break;
                    }
                    // insert

                    if (FilterControl.Check2) //Chỉ cập nhập mã mới.
                    {
                        if (valid)
                        {
                            dataDic["STT_REC"] = V6BusinessHelper.GetNewLikeSttRec("HR1", "STT_REC", "M");
                            dataDic["MA_CT"] = "HR1";
                            if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                            {
                                insert_ok = true;
                                //remove_list_d.Add(row);
                            }
                            else
                            {
                                var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, emp_id);
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
                        dataDic["STT_REC"] = V6BusinessHelper.GetNewLikeSttRec("HR1", "STT_REC", "M");
                        dataDic["MA_CT"] = "HR1";
                        if (V6BusinessHelper.Insert(TABLE_NAME, dataDic))
                        {
                            //remove_list_d.Add(row);
                            insert_ok = true;
                        }
                        else
                        {
                            var s = string.Format("Dòng {0,3}-ID:{1} thêm không được", stt, emp_id);
                            f9Error += s;
                            f9ErrorAll += s;
                        }
                    }
                }

                if (insert_ok)
                {
                    XuLyThemBangBoSung(dataDic, danhSachCot2);
                    remove_list_d.Add(row);
                }

            }
        }

        private void XuLyThemBangBoSung(SortedDictionary<string, object> dataDic, string danhSachCot2)
        {
            try
            {
                var dscot2ds = ObjectAndString.XmlStringToDataSet(danhSachCot2);
                DataTable xmlConfig = dscot2ds.Tables[0];
                string[] columns = "TABLENAME,FIRST_NAME,MID_NAME,LAST_NAME,TACH3,MAPCOLUMNS,DATA".Split(',');
                foreach (string column in columns)
                {
                    if (!xmlConfig.Columns.Contains(column))
                    {
                        xmlConfig.Columns.Add(column);
                    }
                }

                Dictionary<string, int> table_stt = new Dictionary<string, int>();
                foreach (DataRow row in xmlConfig.Rows)
                {
                    try
                    {
                        string TABLENAME = row["TABLENAME"].ToString().Trim().ToUpper();
                        string firstName = row["FIRST_NAME"].ToString().Trim();
                        string midName = row["MID_NAME"].ToString().Trim();
                        string lastName = row["LAST_NAME"].ToString().Trim();
                        string mapColumns = row["MAPCOLUMNS"].ToString().Trim();

                        //Khởi tạo stt
                        if (!table_stt.ContainsKey(TABLENAME))
                        {
                            table_stt[TABLENAME] = 1;
                        }

                        var insert_data = new SortedDictionary<string, object>();
                        insert_data["STT_REC"] = dataDic["STT_REC"];
                        insert_data["STT_REC0"] = ("00000" + table_stt[TABLENAME]).Right(5);
                        
                        

                        if (firstName != "" && dataDic.ContainsKey(firstName))
                        {
                            firstName = dataDic[firstName].ToString().Trim();
                            insert_data["FIRST_NAME"] = firstName;
                        }
                        else
                        {
                            firstName = "";
                        }
                        
                        if (midName != "" && dataDic.ContainsKey(midName))
                        {
                            midName = dataDic[midName].ToString().Trim();
                            insert_data["MID_NAME"] = midName;
                        }
                        else
                        {
                            midName = "";
                        }
                        
                        if (lastName != "" && dataDic.ContainsKey(lastName))
                        {
                            lastName = dataDic[lastName].ToString().Trim();
                            insert_data["LAST_NAME"] = lastName;
                        }
                        else
                        {
                            lastName = "";
                        }

                        string tach3 = row["TACH3"].ToString().Trim();
                        if (tach3 != "" && dataDic.ContainsKey(tach3))
                        {
                            string fullName = dataDic[tach3].ToString().Trim();
                            if (string.IsNullOrEmpty(fullName))
                            {
                                continue;
                            }

                            var sss = fullName.Split(' ');
                            if (sss.Length <= 1)
                            {
                                firstName = fullName;
                                insert_data["FIRST_NAME"] = firstName;
                            }
                            else
                            {
                                lastName = sss[0];
                                insert_data["LAST_NAME"] = lastName;
                                firstName = sss[sss.Length - 1];
                                insert_data["FIRST_NAME"] = firstName;
                                int length = fullName.Length - lastName.Length - firstName.Length - 2;
                                midName = length > 0 ? fullName.Substring(lastName.Length + 1, length) : "";
                                insert_data["MID_NAME"] = midName;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(firstName + midName + lastName))
                            {
                                continue;
                            }
                        }

                        if (mapColumns != "")
                        {
                            var sss = mapColumns.Split('~');
                            foreach (string s in sss)
                            {
                                var ss = s.Split(':');
                                if (ss.Length >= 2)
                                {
                                    if (ss.Length >= 3)
                                    {
                                        string dataType = ss[2].ToUpper();
                                        switch (dataType)
                                        {
                                            case "DATE":
                                            case "DATETIME":
                                                insert_data[ss[0]] = ObjectAndString.ObjectToDate(dataDic[ss[1].ToUpper()]);
                                                break;
                                            case "NUMBER":
                                                insert_data[ss[0]] = ObjectAndString.ObjectToDecimal(dataDic[ss[1].ToUpper()]);
                                                break;
                                            default:
                                                insert_data[ss[0]] = dataDic[ss[1].ToUpper()];
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        insert_data[ss[0]] = dataDic[ss[1].ToUpper()];
                                    }
                                }
                            }
                        }

                        string dataFix = row["DATA"].ToString().Trim();
                        if (dataFix != "")
                        {
                            var sss = dataFix.Split('~');
                            foreach (string s in sss)
                            {
                                var ss = s.Split(':');
                                if (ss.Length >= 2)
                                {
                                    if (ss.Length >= 3)
                                    {
                                        string dataType = ss[2].ToUpper();
                                        switch (dataType)
                                        {
                                            case "DATE":
                                            case "DATETIME":
                                                insert_data[ss[0]] = ObjectAndString.ObjectToDate(ss[1]);
                                                break;
                                            case "NUMBER":
                                                insert_data[ss[0]] = ObjectAndString.ObjectToDecimal(ss[1]);
                                                break;
                                            default:
                                                insert_data[ss[0]] = ss[1];
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        insert_data[ss[0]] = ss[1];
                                    }
                                }
                            }
                        }

                        if (V6BusinessHelper.Insert(TABLENAME, insert_data))
                        {
                            table_stt[TABLENAME]++;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.WriteExLog(GetType() + ".XuLyThemBangBoSung_For", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyThemBangBoSung", ex);
            }
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
                    + (f9ErrorAll.Length > 0 ? "\nError: " : "")
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
