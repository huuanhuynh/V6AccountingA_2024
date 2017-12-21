using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6ThuePostApi;
using V6ThuePostApi.PostObjects;
using V6ThuePostApi.ResponseObjects;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    /// <summary>
    /// Chuyển sang hóa đơn điện tử.
    /// </summary>
    public class AAPPR_SOA2 : XuLyBase
    {
        public AAPPR_SOA2(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            dataGridView1.Control_S = true;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F9: Duyệt chứng từ, F8: Hủy duyệt.");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        //protected override void XuLyBoSungThongTinChungTuF4()
        //{
        //    try
        //    {
        //        if (dataGridView1.CurrentRow != null)
        //        {
        //            var currentRow = dataGridView1.CurrentRow;
        //            if (dataGridView1.Columns.Contains("Stt_rec") && dataGridView1.Columns.Contains("Ma_ct"))
        //            {
        //                var selectedMaCt = currentRow.Cells
        //                    ["Ma_ct"].Value.ToString().Trim();
        //                var selectedSttRec = currentRow.Cells
        //                    ["Stt_rec"].Value.ToString().Trim();

        //                if (!string.IsNullOrEmpty(selectedSttRec) && !string.IsNullOrEmpty(selectedMaCt))
        //                {
        //                    var plist = new List<SqlParameter>
        //                    {
        //                        new SqlParameter("@stt_rec", selectedSttRec),
        //                        new SqlParameter("@maCT", selectedMaCt)
        //                    };
        //                    var alctRow = V6BusinessHelper.Select("Alct", "m_phdbf,m_ctdbf",
        //                        "ma_CT = '" + selectedMaCt + "'").Data.Rows[0];
        //                    var amName = alctRow["m_phdbf"].ToString().Trim();

        //                    if (amName != "")
        //                    {
        //                        var am = V6BusinessHelper.Select(amName, "*", "STT_REC=@stt_rec and MA_CT=@maCT",
        //                            "", "", plist.ToArray()).Data;

        //                        var fText = "Ghi chú chứng từ";
        //                        var f = new V6Form
        //                        {
        //                            Text = fText,
        //                            AutoSize = true,
        //                            FormBorderStyle = FormBorderStyle.FixedSingle
        //                        };

        //                        var hoaDonForm = new AAPPR_SOA2_F4(selectedSttRec, am.Rows[0]);
        //                        hoaDonForm.txtSoCtXuat.Enabled = false;
        //                        hoaDonForm.txtGhiChu01.Enabled = false;
                                
        //                        //var so_ctx = hoaDonForm.So_ctx.Trim();
        //                        //if (so_ctx == "")
        //                        //{
        //                        //    if (so_ctx_temp != null)
        //                        //    {
        //                        //        try
        //                        //        {
        //                        //            var int_so_ctx = int.Parse(so_ctx_temp);
        //                        //            if (int_so_ctx > 0)
        //                        //            {
        //                        //                var s0 = "".PadRight(so_ctx_temp.Length, '0');
        //                        //                var s = (s0 + (int_so_ctx + 1)).Right(so_ctx_temp.Length);
        //                        //                hoaDonForm.So_ctx = s;
        //                        //            }
        //                        //        }
        //                        //        catch (Exception)
        //                        //        {
        //                        //            // ignored
        //                        //        }
        //                        //    }
        //                        //}

        //                        hoaDonForm.UpdateSuccessEvent += data =>
        //                        {
        //                            currentRow.Cells["GHI_CHU01"].Value = data["GHI_CHU01"];
        //                            currentRow.Cells["GHI_CHU02"].Value = data["GHI_CHU02"];

        //                            currentRow.Cells["SO_CTX"].Value = data["SO_CTX"];
        //                            //so_ctx_temp = data["SO_CTX"].ToString().Trim();
        //                            //FilterControl.String1 = so_ctx_temp;
        //                        };
        //                        f.Controls.Add(hoaDonForm);
        //                        hoaDonForm.Disposed += delegate
        //                        {
        //                            f.Dispose();
        //                        };

        //                        f.ShowDialog(this);
        //                        SetStatus2Text();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                this.ShowWarningMessage("Không được phép sửa chi tiết!");
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ShowErrorMessage(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4:\n" + ex.Message);
        //    }
        //}

        #region ==== Xử lý F8 ====

        private bool F8Running;
        private string F8Error = "";
        private string F8ErrorAll = "";
        protected override void XuLyF8()
        {
            try
            {
                V6ControlFormHelper.ShowMessage("Chưa xử lý.");
                return;
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
        private void F8Thread()
        {
            F8Running = true;
            F8ErrorAll = "";

            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                        //string soct = row.Cells["So_ct"].Value.ToString();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@Set_kieu_post", FilterControl.Kieu_post),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        V6BusinessHelper.ExecuteProcedureNoneQuery(_program + "F8", plist);
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {

                    F8Error += ex.Message;
                    F8ErrorAll += ex.Message;
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
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F8 finish "
                    + (F8ErrorAll.Length > 0 ? "Error: " : "")
                    + F8ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F8 Xử lý xong!");
            }
        }
        #endregion xulyF8


        #region ==== Xử lý F9 ====
        
        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string f9MessageAll = "";
        protected override void XuLyF9()
        {
            try
            {
                Timer tF9 = new Timer();
                tF9.Interval = 500;
                tF9.Tick += tF9_Tick;
                Thread t = new Thread(F9Thread);
                t.SetApartmentState(ApartmentState.STA);
                CheckForIllegalCrossThreadCalls = false;
                remove_list_g = new List<DataGridViewRow>();
                t.IsBackground = true;
                t.Start();
                tF9.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
            }
        }
        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";
            f9MessageAll = "";

            int i = 0;
            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {
                       
                        string soct = row.Cells["So_ct"].Value.ToString().Trim();
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", (row.Cells["Stt_rec"].Value ?? "").ToString()),
                            new SqlParameter("@Ma_ct", (row.Cells["Ma_ct"].Value ?? "").ToString()),
                            new SqlParameter("@HoaDonMau","0"),
                            new SqlParameter("@isInvoice","1"),
                            new SqlParameter("@ReportFile",""),
                            new SqlParameter("@UserID", V6Login.UserId)
                        };

                        DataSet ds = V6BusinessHelper.ExecuteProcedure(_program + "F9", plist);
                        //DataTable data0 = ds.Tables[0];

                        string jsonBody = "";
                        jsonBody = ReadData(ds);
                        string result = POST(jsonBody);
                        CreateInvoiceResponse responseObject = null;
                        if (RequestSender.Response != null)
                        {
                            responseObject = MyJson.ConvertJson<CreateInvoiceResponse>(result);
                        }
                        else
                        {
                            responseObject = new CreateInvoiceResponse()
                            {
                                description = "Response is null.",
                                result = result
                            };

                            this.WriteToLog(GetType() + ".F9Thread", string.Format("{0}-{1}:{2}\njson:{3}",
                                soct, responseObject.description, responseObject.result, jsonBody));
                        }
                        //
                        _message = responseObject.description;
                        f9MessageAll += string.Format("\n{0}: {1} {2}", soct, responseObject.errorCode, responseObject.description, responseObject.result);
                        
                        remove_list_g.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }
            f9Running = false;
        }

        public string ReadData(DataSet ds)
        {
            string result = "";
            try
            {
                //PostObject obj = new PostObject();
                postObject = new PostObject();
                DataTable map_table = ds.Tables[0];
                DataTable ad_table = ds.Tables[1];
                DataTable am_table = ds.Tables[2];
                DataRow row0 = am_table.Rows[0];
                DataTable ad2_table = ds.Tables[3];

                ReadConfigInfo(map_table);
                //DataTable dataDbf = ParseDBF.ReadDBF(dbfFile);
                //DataTable data = V6Tools.V6Convert.Data_Table.FromTCVNtoUnicode(dataDbf);
                //Fill data to postObject
                
                //private Dictionary<string, XmlLine> generalInvoiceInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in generalInvoiceInfoConfig)
                {
                    postObject.generalInvoiceInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> buyerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in buyerInfoConfig)
                {
                    postObject.buyerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> sellerInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in sellerInfoConfig)
                {
                    postObject.sellerInfo[item.Key] = GetValue(row0, item.Value);
                }
                //private Dictionary<string, XmlLine> paymentsConfig = null;
                Dictionary<string, object> payment = new Dictionary<string, object>();
                foreach (KeyValuePair<string, ConfigLine> item in paymentsConfig)
                {
                    payment[item.Key] = GetValue(row0, item.Value);
                }
                postObject.payments.Add(payment);//One payment only!

                //itemInfo
                foreach (DataRow row in ad_table.Rows)
                {
                    if (row["LOAI"].ToString() != "0") continue;
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in itemInfoConfig)
                    {
                        rowData[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.itemInfo.Add(rowData);
                }

                //private Dictionary<string, XmlLine> summarizeInfoConfig = null;
                foreach (KeyValuePair<string, ConfigLine> item in summarizeInfoConfig)
                {
                    postObject.summarizeInfo[item.Key] = GetValue(row0, item.Value);
                }

                //taxBreakdowns 
                foreach (DataRow row in ad2_table.Rows)
                {
                    Dictionary<string, object> taxBreakdown = new Dictionary<string, object>();
                    foreach (KeyValuePair<string, ConfigLine> item in taxBreakdownsConfig)
                    {
                        taxBreakdown[item.Key] = GetValue(row, item.Value);
                    }
                    postObject.taxBreakdowns.Add(taxBreakdown);
                }

                result = postObject.ToJson();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ReadData", ex);
            }
            return result;
        }

        private object GetValue(DataRow row, ConfigLine config)
        {
            object fieldValue = config.Value;
            //if (string.IsNullOrEmpty(config.Type))
            //{
            //    return fieldValue;
            //}

            string configFIELD = null, configDATATYPE = null;
            if (!string.IsNullOrEmpty(config.Type))
            {
                string[] ss = config.Type.Split(':');
                configFIELD = ss[0].ToUpper();
                if (ss.Length > 1) configDATATYPE = ss[1].ToUpper();
            }
            if (string.IsNullOrEmpty(configDATATYPE))
            {
                configDATATYPE = config.DataType.ToUpper();
            }

            if (configFIELD == "ENCRYPT")
            {
                return UtilityHelper.DeCrypt(fieldValue.ToString());
            }

            if (configFIELD == "FIELD"
                && !string.IsNullOrEmpty(config.FieldV6)
                && row.Table.Columns.Contains(config.FieldV6))
            {
                fieldValue = row[config.FieldV6];
                if (row.Table.Columns[config.FieldV6].DataType == typeof (string))
                {
                    //Trim
                    fieldValue = fieldValue.ToString().Trim();
                }
            }

            if (!string.IsNullOrEmpty(configDATATYPE))
            {
                if (configDATATYPE == "BOOL")
                {
                    if (fieldValue is bool)
                    {
                        return fieldValue;
                    }
                    else
                    {
                        return fieldValue.ToString() == "1" ||
                               fieldValue.ToString().ToLower() == "true" ||
                               fieldValue.ToString().ToLower() == "yes";
                    }
                }
                else if (configDATATYPE == "N2C") // Đọc số tiền thành chữ.
                {
                    return V6BusinessHelper.MoneyToWords(ObjectAndString.ObjectToDecimal(fieldValue), "V", "VND");
                }
                else
                {
                    //Chưa xử lý
                    return fieldValue;
                }
            }
            else
            {
                return fieldValue;
            }

            return fieldValue;
        }

        public string POST(string jsonBody)
        {
            try
            {
                V6Request.Login(username, password);
                string requestUrl = string.Format(baseUrl + methodUrl + mst);
                string result = V6Request.Request(requestUrl, jsonBody);
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string username, password;
        private string baseUrl = "", methodUrl = "", mst = "";

        private PostObject postObject;

        private Dictionary<string, ConfigLine> generalInvoiceInfoConfig = null;
        private Dictionary<string, ConfigLine> buyerInfoConfig = null;
        private Dictionary<string, ConfigLine> sellerInfoConfig = null;
        private Dictionary<string, ConfigLine> paymentsConfig = null;
        private Dictionary<string, ConfigLine> itemInfoConfig = null;
        private Dictionary<string, ConfigLine> summarizeInfoConfig = null;
        private Dictionary<string, ConfigLine> taxBreakdownsConfig = null;
        public void ReadConfigInfo(DataTable map_table)
        {
            generalInvoiceInfoConfig = new Dictionary<string, ConfigLine>();
            buyerInfoConfig = new Dictionary<string, ConfigLine>();
            sellerInfoConfig = new Dictionary<string, ConfigLine>();
            paymentsConfig = new Dictionary<string, ConfigLine>();
            itemInfoConfig = new Dictionary<string, ConfigLine>();
            summarizeInfoConfig = new Dictionary<string, ConfigLine>();
            taxBreakdownsConfig = new Dictionary<string, ConfigLine>();
            
            try
            {
                foreach (DataRow row in map_table.Rows)
                {
                    string GROUP_NAME = row["GroupName"].ToString().Trim().ToUpper();
                    ConfigLine line = ReadConfigLine(row);
                    switch (GROUP_NAME)
                    {
                        case "V6INFO":
                            {
                                if (line.Field.ToUpper() == "USERNAME")
                                {
                                    username = line.Value;
                                }
                                else if (line.Field.ToUpper() == "PASSWORD")
                                {
                                    password = UtilityHelper.DeCrypt(line.Value);
                                }
                                else if (line.Field.ToUpper() == "LINK")
                                {
                                    baseUrl = line.Value;
                                    methodUrl = "";
                                    mst = "";
                                }
                                break;
                            }
                        case "GENERALINVOICEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    generalInvoiceInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "BUYERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    buyerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SELLERINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    sellerInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "PAYMENTS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    paymentsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "ITEMINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    itemInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "SUMMARIZEINFO":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    summarizeInfoConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        case "TAXBREAKDOWNS":
                            {
                                if (!string.IsNullOrEmpty(line.Field))
                                {
                                    taxBreakdownsConfig.Add(line.Field, line);
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

            }
            catch (Exception ex)
            {
                f9Error += ex.Message;
                f9ErrorAll += ex.Message;
            }
        }

        private static ConfigLine ReadConfigLine(DataRow reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader["Field"].ToString().Trim();
            config.Value = reader["Value"].ToString().Trim();
            config.FieldV6 = reader["FieldV6"].ToString().Trim();
            config.Type = reader["Type"].ToString().Trim();
            config.DataType = reader["DataType"].ToString().Trim();
            return config;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length>0?"Error: ":"")
                    + cError + _message);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
                this.ShowMessage("F9 finish " + f9MessageAll);
            }
        }
        #endregion xulyF9

        V6Invoice81 invoice = new V6Invoice81();
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                var data = invoice.LoadAd81(sttRec);
                dataGridView2.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA2 ViewDetails: " + ex.Message);
            }
        }
    }

    internal class ConfigLine
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string FieldV6 { get; set; }
        /// <summary>
        /// <para>Field -> lấy từ dữ liệu theo field.</para>
        /// <para>Field:Date -> Date là kiểu dữ liệu để xử lý.</para>
        /// </summary>
        public string Type { get; set; }

        public string DataType { get; set; }
    }
}
