using GSM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using V6Tools;

namespace V6ThreadLibrary
{
    public class SmsThread
    {
        public SmsThread()
        {
            _log = new Logger(__dir, __logName);

            _Setting = new SmsSetting(Path.Combine(__dir,"V6SmsServiceSetting.ini"));
            _Setting._autoSave = true;
            _MiniLicence = new MiniLicence.MiniLicence("V6MultiSms", "1.0.0.0");
            _MiniLicence.CheckLicence("E62695A4B4", "506E423037355A5563512B617168615448314467494C527253644F48695067567164337276374C717A4B506B696E63657A395931345A43355A374D726B617A7A41636C6E536F51485A59515350395250436F2B582B773D3D", "HuyHu@");

        }

        string __dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        const string __logName = "V6SmsServiceLog";
        string _conString = "";
        Logger _log;// = new Logger(__dir, __logName);

        private void Log(string message)
        {
            _log.WriteLog(__dir, __logName, message);
        }

        public string _sqlxmlfilename = "";
        SmsSetting _Setting;
        GSM_Phone _smsModem;
        MiniLicence.MiniLicence _MiniLicence;
        bool sending = false;
        
        
        int columnSoDienThoai = 0;
        int columnTenNguoiNhan = 1;
        int columnNoiDung = 2;

        DateTime time;
        public void SendSmsAndEmail(ref string message)
        {
            _conString = V6Tools.UtilityHelper.GetConnectionFromXML(_sqlxmlfilename);
            time = DateTime.Now;

            //Nếu = hoặc đã qua giờ hẹn
            string[] HHMM = _Setting.SendTimeHHMM.Split(':');
            int hh = 0; int.TryParse(HHMM[0],out hh);
            int mm = 0; int.TryParse(HHMM[1], out mm);

            if( time.Hour > hh||
                (time.Hour == hh && time.Minute>=mm))
            {
                //Nếu ngày này đã gửi sms thì return
                if (time.ToString("yyyyMMdd") == _Setting.LastRunDate)
                {
                    message = "Sent " + _Setting.LastRunDate;
                    
                    return;
                }
                else
                {
                    //Ghi lai lan chay nay.
                    _Setting.LastRunDate = time.ToString("yyyyMMdd");
                }

                
                try
                {
                    message = "Send start.";
                    AutoConnect();
                    DataTable data = ReadDataForSend();
                    SendFromTable(data);//<<=
                    message = "Send finish.";
                }
                catch (Exception ex)
                {
                    message = "Send error: " + ex.Message;
                    Log(message);
                }
            }
            else
            {
                message = "Time to send " + _Setting.SendTimeHHMM;
            }
        }

        private void AutoConnect()
        {
            try
            {
                _smsModem = new GSM_Phone();

                string port = _smsModem.AutoConnect(_Setting.PortName);
                if (port != null)
                {
                    _smsModem.Mini_Licence = _MiniLicence;
                    if (_Setting.PortName != port)
                    {
                        _Setting.PortName = _smsModem.PortName;
                    }
                }
                else
                {
                    Log("Không kết nối được với modem sms!");
                }
            }
            catch (Exception ex)
            {
                Log("Có lỗi khi kết nối với modem sms." + ex.Message);
            }
        }

        private DataTable ReadData(string file)
        {
            DataTable result = new DataTable();
            string type = Path.GetExtension(file).ToLower();
            try
            {
                if (type == ".dbf")
                {
                    result = V6Tools.ParseDBF.ReadDBF(file);
                }
                else if (type == ".xls" || type == ".xlsx")
                {
                    result = V6Tools.V6Convert.Excel_File.Sheet1ToDataTable(file);
                }
                else if (type == ".txt")
                {
                    result = V6Tools.V6Reader.TextFile.ToTable(file);
                }
                else if (type == ".sql")
                {
                    string sql = File.ReadAllText(file);
                    result = V6Tools.SqlHelper.ExecuteDataset(_conString, CommandType.Text, sql).Tables[0];
                }
                else
                {
                    DateTime sqlDate = (DateTime)V6Tools.SqlHelper.ExecuteDataset
                        (_conString, CommandType.Text, "select getdate()").Tables[0].Rows[0][0];

                    //List<MyProcedureParam> paras = new List<MyProcedureParam>();
                    //paras.Add(new MyProcedureParam("key1","1=1"));
                    //paras.Add(new MyProcedureParam("key2","1=1"));
                    //paras.Add(new MyProcedureParam("key3","1=1"));
                    //paras.Add(new MyProcedureParam("date1", sqlDate));
                    //paras.Add(new MyProcedureParam("date2", sqlDate));
                    //sql
                    //V6Tools.SqlHelper.ExecuteDataset("con", file, "keys");
                    var plist = new []
                    {
                        new SqlParameter("@key1","1=1"),
                        new SqlParameter("@key2","1=1"),
                        new SqlParameter("@key3","1=1"),
                        new SqlParameter("@date1",sqlDate),
                        new SqlParameter("@date2",sqlDate),
                    };
                    result = V6Tools.SqlHelper.ExecuteDataset(_conString,CommandType.StoredProcedure,file,plist).Tables[0];// V6Tools.UtilityHelper.getDataTableFromProcedure(_conString, file, paras);
                }
            }
            catch (Exception ex)
            {
                Log("ReadData " + ex.Message);
            }
            return result;
        }
        private DataTable ReadDataForSend()
        {
            DataTable result = ReadData(_Setting.DataFile);
            if (!string.IsNullOrEmpty(_Setting.From) && !string.IsNullOrEmpty(_Setting.To))
                V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, _Setting.From, _Setting.To);
            if (!result.Columns.Contains("Check"))
                result.Columns.Add("Check");
            
            return result;
        }

        private void SendFromTable(DataTable tableData)
        {
            sending = true;
            string fileToSend = CreateFileToSend();
            bool isSendEmail = tableData.Columns.Contains("Email");
            if (!isSendEmail) Log("No Email column in data.");
            //Control.CheckForIllegalCrossThreadCalls = false;
            string indexSmsDaGui = "";
            string indexSmsGuiLoi = "";
            string indexEmailDaGui = "";
            string indexEmailGuiLoi = "";
            //messageText = txtMessage.Text;

            int columnCount = tableData.Columns.Count;
            int rowCount = tableData.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                //ListViewItem item = ahaSortListView1.Items[i];
                DataRow row = tableData.Rows[i];
                Dictionary<string, string> thong_tin = new Dictionary<string, string>();
                string messageString = "";

                for (int j = 0; j < columnCount; j++)
                {
                    thong_tin.Add(tableData.Columns[j].ColumnName, row[j].ToString().Trim());
                }

                if (true)
                {
                    {
                        messageString = row[columnNoiDung].ToString();
                    }

                    string sendMessage = TaoTinNhanRieng(messageString, thong_tin);

                    #region ==== Try send sms ====
                    try
                    {
                        string number = row[0].ToString().Trim();
                        if (number != "")
                        {
                            var send_status = _smsModem.SendMessage_PDU(number, sendMessage);
                            switch (send_status)
                            {
                                case GSM_Phone.SendSmsStatus.ERROR:
                                    indexSmsGuiLoi += i + ",";
                                    break;
                                case GSM_Phone.SendSmsStatus.NONE:
                                    indexSmsGuiLoi += i + ",";
                                    break;
                                case GSM_Phone.SendSmsStatus.OK:
                                    indexSmsDaGui += i + ",";
                                    break;
                                case GSM_Phone.SendSmsStatus.UNKNOWN:
                                    indexSmsDaGui += i + ",";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            indexSmsGuiLoi += i + ",";
                        }
                    }
                    catch (Exception ex)
                    {
                        indexSmsGuiLoi += i + ",";
                        Log("Try send sms error. index:" + i + ". " + ex.Message);
                    }
                    #endregion try send sms

                    #region ==== Try send email ====
                    try
                    {
                        if (isSendEmail)
                        {
                            string sendto = row["Email"].ToString().Trim();
                            if (sendto != "")
                            {
                                bool sent = _MailSender.SendEmail(
                                    _Setting.EmailSender,
                                    UtilityHelper.DeCrypt(_Setting.EmailPassword),
                                    sendto,
                                    _Setting.EmailSubject,
                                    sendMessage,
                                    fileToSend);

                                if (sent) indexEmailDaGui += i + ",";
                            }
                            else
                            {
                                indexEmailGuiLoi += i + ",";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        indexEmailGuiLoi += i + ",";
                        Log("SendEmailError:" + i + ex.Message);
                    }
                    #endregion try send email
                }
            }
            //sau khi gui xong
            sending = false;
            Log("Sent Sms indexes: " + indexSmsDaGui);
            Log("NoSent Sms indexes: " + indexSmsGuiLoi);
            Log("Sent Email indexes: " + indexEmailDaGui);
            Log("NoSent Email indexes: " + indexEmailGuiLoi);
        }

        private string TaoTinNhanRieng(string message, Dictionary<string, string> tt)
        {
            message = message.Replace("<ten>", "<" + columnTenNguoiNhan + ">");
            message = message.Replace("<sdt>", "<" + columnSoDienThoai + ">");
            foreach (var item in tt)
            {
                message = message.Replace("<" + item.Key + ">", item.Value);
            }
            return message;
        }


        #region ==== Send Email ====
        V6MailSender _MailSender = new V6MailSender();

        private string CreateFileToSend()
        {
            string fileToSend = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    _Setting.SendFileName + DateTime.Now.ToString("_yyyyMMdd_HHmmss")
                    + ".xlsx");
            string flag = "";
            try
            {
                SmartXLS.WorkBook wb = new SmartXLS.WorkBook();
                if (_Setting.XlsTemplate.ToLower().EndsWith(".xlsx"))
                    wb.readXLSX(_Setting.XlsTemplate);
                else if (_Setting.XlsTemplate.ToLower().EndsWith(".xls"))
                    wb.read(_Setting.XlsTemplate);

                for (int i = 0; i < _Setting.NumOfSheet; i++)
                {
                    flag = "0";
                    //Chon sheet
                    wb.Sheet = i;
                    flag = "1";
                    string title = _Setting.GetSetting("Title" + (i+1));
                    title = title.Replace("[dd]", time.Day.ToString());
                    title = title.Replace("[MM]", time.Month.ToString());
                    title = title.Replace("[yyyy]", time.Year.ToString());
                    string titleaddr = _Setting.GetSetting("TitleAddress" + (i+1));
                    if (titleaddr == "") titleaddr = "A1";
                    //int[] taddr = ExcelAddressToRowCol(titleaddr);
                    flag = "2";
                    wb.setText(titleaddr, title);
                    flag = "3";
                    DataTable data = ReadData(_Setting.GetSetting("Data" + (i + 1)));
                    flag = "4";

                    if (!string.IsNullOrEmpty(_Setting.From) && !string.IsNullOrEmpty(_Setting.To))
                        V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(data, _Setting.From, _Setting.To);

                    string address = _Setting.GetSetting("Address" + (i + 1));
                    if (address == "") address = "A3";
                    int[] addr = ExcelAddressToRowCol(address);

                    int lastRow = data.Rows.Count;
                    int lastCol = data.Columns.Count;
                    flag = "5";
                    //datas
                    for (int row = 0; row < lastRow; row++)
                    {
                        //Neu lam setting columns thi sua cho nay
                        for (int col = 0; col < lastCol; col++)
                        {
                            string type = data.Columns[col].DataType.ToString();
                            switch (type)
                            {
                                case "System.Byte":
                                case "System.SByte":
                                case "System.Decimal":
                                case "System.Double":
                                case "System.Single":
                                case "System.Int16":
                                case "System.Uint16":
                                case "System.Int32":
                                case "System.Uint32":
                                case "System.Int64":
                                case "System.UInt64":
                                    flag = "for: " + row + "," + col;
                                    wb.setNumber(row + addr[0], col + addr[1], Convert.ToDouble(data.Rows[row][col]));
                                    break;
                                default:
                                    wb.setText(row + addr[0], col + addr[1], data.Rows[row][col].ToString());
                                    break;
                            }
                            
                        }
                    }
                    
                    //wb.ImportDataTable(data, true, addr[0], addr[1], data.Rows.Count, data.Columns.Count);
                }
                wb.Sheet = 0;
                //Field names

                //datas
                //for (int i = 0; i < rowCount; i++)
                //{
                //    for (int j = 0; j < colCount; j++)
                //    {
                //        wb.setText(i+1, j, data1.Rows[i][j].ToString());
                //    }
                //}

                wb.writeXLSX(fileToSend);
                wb.Dispose();
            }
            catch (Exception ex)
            {
                Log("Create file to send error " + flag + ": " + ex.Message);
            }
            return fileToSend;
        }

        private int[] ExcelAddressToRowCol(string address)
        {
            int[] a = new int[] { 0, 0 };
            //int ba = 1 + 'Z' - 'A';//= 26
            try
            {
                address = address.ToUpper();
                int startIndex = address.IndexOfAny("1234567890".ToCharArray());
                string col = address.Substring(0,startIndex);
                string row = address.Substring(startIndex);
                a[0] = int.Parse(row) - 1;
                for (int i = col.Length-1; i >= 0; i--)
                {
                    char c = col[i];
                    a[1] += (int)Math.Pow(26, i) * (c - 'A');
                }
                return a;
            }
            catch
            {
                return new int[] { 0, 0 };
            }
        }
        #endregion send email

        
    }
}
