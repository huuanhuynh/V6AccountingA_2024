using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using GSM;
using V6AccountingBusiness;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.Filter.Sms
{
    public partial class SendSmsForm : Form
    {
        GSM_Phone smsModem;
        List<GSM.GSM_Phone> listModem;
        private bool _hardExit = false;
        DataTable tableData;

        public SendSmsForm()
        {
            InitializeComponent();
            
            MyInit();
        }

        private string Program_dataType, Program_file_proc, Program_from_where, Program_to_conString;

        void MyInit()
        {
            smsModem = V6ControlFormHelper.SmsModem;
            string[] portNames = SerialPort.GetPortNames();
            foreach (var item in portNames)
            {
                comboBox1.Items.Add(item);
            }

            DataTable data = LoadData(Program_dataType, Program_file_proc, Program_from_where, Program_to_conString);
            bool b = this.Visible;

            ViewData(data, false);
            bool autoSend = false;
            if (autoSend) AutoSend();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnTimModem_Click(object sender, EventArgs e)
        {
            LoadModemList();
        }

        private void LoadModemList()
        {
            listModem = new List<GSM_Phone>();
            string[] portNames = SerialPort.GetPortNames();
            foreach (string port in portNames)
            {
                GSM_Phone newModem = new GSM_Phone();
                if(newModem.Connect(port, 9600, 8, 300, 300))
                {
                    newModem.ClosePort();
                    listModem.Add(newModem);
                }
            }

            comboBox1.Items.Clear();
            foreach (GSM_Phone modem in listModem)
            {
                comboBox1.Items.Add("" + modem.PortName + ": " + modem.Nhà_mạng);
            }
            if(comboBox1.Items.Count == 0)
            {
                comboBox1.Items.Add("Không kết nối được!");
            }
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnNgatKetNoi_Click(object sender, EventArgs e)
        {
            smsModem.ClosePort();
            txtConnectPort.Text = "Đã ngắt kết nối.";
        }


        private void btnThemNguoiNhan_Click(object sender, EventArgs e)
        {
            if (txtSoDienThoai.Text.Trim() != "")
            {
                var newRow = tableData.NewRow();
                var columnCount = tableData.Columns.Count;
                
                if (columnCount > 0) newRow[0] = txtSoDienThoai.Text;
                if (columnCount > 1) newRow[1] = txtTenNguoiNhan.Text;
                if (columnCount > 2) newRow[2] = txtThongTin1.Text;
                if (columnCount > 3) newRow[3] = txtThongTin2.Text;
                if (columnCount > 4) newRow[4] = txtThongTin3.Text;
                if (columnCount > 5) newRow[5] = txtThongTin4.Text;
                if (columnCount > 6) newRow[6] = txtThongTin5.Text;

                tableData.Rows.Add(newRow);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGui1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Gửi tin nhắn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.No)
                return;
            if (txtSendTo.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập số!");
                return;
            }

            try
            {
                if (smsModem.GSM_PORT != null)
                {
                    if (!smsModem.GSM_PORT.IsOpen)
                        smsModem.OpenPort();
                    
                    
                    var a = smsModem.SendMessage_PDU(txtSendTo.Text.Trim(), txtMessage.Text, true);
                    switch (a)
                    {
                        case GSM_Phone.SendSmsStatus.ERROR:
                            lblNoiDung.Text = "Gửi lỗi!";
                            break;
                        case GSM_Phone.SendSmsStatus.NONE:
                            lblNoiDung.Text = "Không gửi được hay gì đó!";
                            break;
                        case GSM_Phone.SendSmsStatus.OK:
                            lblNoiDung.Text = "Gửi Ok";
                            break;
                        case GSM_Phone.SendSmsStatus.UNKNOWN:
                            lblNoiDung.Text = "Không biết gửi được không";
                            break;
                        default:
                            lblNoiDung.Text = "???";
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Chưa kết nối!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            //int part7bitLength = 153, part8bitLength = 134, part16bitLength = 67;
            string messageText = txtMessage.Text;
            int textLength = txtMessage.Text.Length;
            int messageLength = textLength;
            int numberOfMessage = 1;

            if (GsmEncoding.IsUnicodeNotGSM7String(messageText))
            {
                #region unicode
                //1 message – 70 characters
                //2 messages – 134 characters
                //3 messages – 201 characters
                //4 messages – 268 characters
                if (messageLength > 70)
                {
                    numberOfMessage = messageLength / (70 - 3);
                    if (messageLength % (70 - 3) > 0) numberOfMessage++;
                }

                if (numberOfMessage == 1)
                {
                    lblNoiDung.Text = string.Format
                        ("độ dài {0}/70 = 1 tin có dấu Unicode!", textLength);
                }
                else
                {
                    lblNoiDung.Text = string.Format
                        ("độ dài {0}/67 = {1} tin có dấu Unicode!", textLength, numberOfMessage);
                }
                #endregion
            }
            else if (GsmEncoding.IsNotGSM7bitString(txtMessage.Text))
            {
                #region 8bit
                if (messageLength > 140)
                {
                    numberOfMessage = messageLength / (140 - 6);
                    if (messageLength % (140 - 6) > 0) numberOfMessage++;
                }

                if (numberOfMessage == 1)
                    lblNoiDung.Text = string.Format("độ dài {0}/140 = 1 tin 8 bit!", textLength, numberOfMessage);
                else
                    lblNoiDung.Text = string.Format("độ dài {0}/134 = {1} tin 8 bit!", textLength, numberOfMessage);
                #endregion
            }
            else//Gsm7bit string
            {
                #region 7bit
                //7-bit encoded characters (Standard GSM)
                //1 message – 160 characters
                //2 messages – 306 characters
                //3 messages – 459 characters
                //4 messages – 612 characters
                messageLength = GsmEncoding.GetGSM7Length(txtMessage.Text);
                if (messageLength > 160)
                {
                    numberOfMessage = messageLength / (160 - 7);
                    if (messageLength % (160 - 7) > 0) numberOfMessage++;
                }
                if (numberOfMessage == 1)
                    lblNoiDung.Text = string.Format("độ dài {0} = {1}bytes/160 = 1 tin 7 bit!", textLength, messageLength);
                else
                    lblNoiDung.Text = string.Format("độ dài {0} = {1}bytes/153 = {2} tin 7 bit!", textLength, messageLength, numberOfMessage);
                //if (messageLength > 2 && messageLength % (160) == 0) messageLength--;
                //numberOfMessage = messageLength / (160) + 1;
                //lblStatus.Text = string.Format("độ dài {0} = {1} tin không dấu!", txtMessage.Text.Length, numberOfMessage);
                ////lblStatus.Text = (160 - txtMessage.Text.Length).ToString();
                #endregion 7bit
            }

            string strConTuy = "";
            if (messageText.Contains("<sdt>")) strConTuy += "<sdt>";
            if (messageText.Contains("<ten>")) strConTuy += "<ten>";
            if (messageText.Contains("<tt1>")) strConTuy += "<tt1>";
            if (messageText.Contains("<tt2>")) strConTuy += "<tt2>";
            if (messageText.Contains("<tt3>")) strConTuy += "<tt3>";
            if (messageText.Contains("<tt4>")) strConTuy += "<tt4>";
            if (messageText.Contains("<tt5>")) strConTuy += "<tt5>";
            if (strConTuy.Length > 0)
            {
                lblNoiDung.Text += " Còn tùy vào " + strConTuy;
            }
        }

        private void btnGuiDanhSach_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Gửi tin nhắn cho danh sách đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.No)
                return;
            
            if (tugo_noidung_tinnhan && txtMessage.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập nội dung tin nhắn!");
                return;
            }

            if(sending)
            {
                MessageBox.Show("Đang gửi");
                return;
            }

            try
            {
                if (smsModem.GSM_PORT != null)
                {
                    if (!smsModem.GSM_PORT.IsOpen)
                        smsModem.OpenPort();

                    sending = true;
                    Thread t = new Thread(SendFromGrid);
                    t.IsBackground = true;
                    t.Start();
                    timerGuiDanhSach.Start();
                }
                else
                {
                    MessageBox.Show("Chưa kết nối!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AutoSend()
        {
            try
            {
                AutoConnect();

                if (smsModem.GSM_PORT != null)
                {
                    if (!smsModem.GSM_PORT.IsOpen)
                        smsModem.OpenPort();

                    sending = true;
                    Thread t = new Thread(SendFromTable);
                    t.IsBackground = true;
                    t.Start();
                    timerGuiDanhSach.Start();
                }
                else
                {
                    MessageBox.Show("Chưa kết nối!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string setting_PortName = "";
        private void Connect()
        {
            if (listModem == null) listModem = new List<GSM_Phone>();
            string s = comboBox1.Text;
            if (s != "" && s.StartsWith("COM") && s.Contains(":"))
            {
                string com = s.Substring(0, s.IndexOf(':'));
                foreach (var item in listModem)
                {
                    if (item.PortName == com)
                    {
                        smsModem = item;
                        smsModem.OpenPort();
                        txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                        btnNgatKetNoi.Enabled = true;
                        setting_PortName = smsModem.PortName;
                        return;
                    }
                }
                //khong co trong list modem
                smsModem = new GSM_Phone();
                if (smsModem.Connect(com, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                    setting_PortName = smsModem.PortName;
                    listModem.Add(smsModem);
                    return;
                }
            }
            else if (s != "" && s.StartsWith("COM") && s.Length > 3)
            {
                smsModem = new GSM_Phone();
                if (smsModem.Connect(s, 9600, 8, 300, 300))
                {
                    txtConnectPort.Text = "Đã kết nối " + smsModem.PortName + ":" + smsModem.Operator;
                    setting_PortName = smsModem.PortName;
                    listModem.Add(smsModem);
                    return;
                }
            }
        }
        private void AutoConnect()
        {
            try
            {
                smsModem = new GSM_Phone();
                string port=smsModem.AutoConnect(setting_PortName);
                if(port!=null)
                {
                    setting_PortName = smsModem.PortName;
                    //Program.setting.SaveSetting();
                }
                else
                {
                    MessageBox.Show("Không kết nối được với modem sms!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi kết nối với modem sms." + ex.Message);
            }
        }

        bool sending = false;
        List<int> indexDaGui = new List<int>();
        List<int> indexGuiLoi = new List<int>();
        string messageText = "";

        string columnCheck = "Check";
        string columnSoDienThoai = "";
        string columnTenNguoiNhan = "";
        string columnNoiDung = "";

        bool tugo_noidung_tinnhan = true;
        
        //int timerIndex = 0;
        private void SendFromGrid()
        {
            sending = true;
            Control.CheckForIllegalCrossThreadCalls = false;
            indexDaGui = new List<int>();
            indexGuiLoi = new List<int>();
            messageText = txtMessage.Text;

            columnSoDienThoai = cboSoDienThoai.Text;
            columnTenNguoiNhan = cboTenNguoiNhan.Text;
            columnNoiDung = cboTuDuLieu.Text;
            
            tugo_noidung_tinnhan = radTuGoNoiDung.Checked;

            ////Tim vi tri cua cot chon???
            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //{
            //    if(dataGridView1.Columns[i].Name)
            //}

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //ListViewItem item = ahaSortListView1.Items[i];
                DataGridViewRow row = dataGridView1.Rows[i];
                if (row.IsNewRow) continue;

                Dictionary<string, string> tt = new Dictionary<string, string>();
                string sms = "";
                foreach (DataGridViewCell item in row.Cells)
                {
                    tt.Add(dataGridView1.Columns[item.ColumnIndex].DataPropertyName,
                        item.Value == null ? "" : item.Value.ToString().Trim());
                }
                
                string checkSend = row.Cells[columnCheck].Value == null ? "" : row.Cells[columnCheck].Value.ToString();

                if(checkSend == "1")
                {
                    if(tugo_noidung_tinnhan)
                    {
                        sms = messageText;
                    }
                    else
                    {
                        sms = row.Cells[columnNoiDung].Value.ToString();
                    }

                    string smsRieng = TaoTinNhanRieng(sms, tt);
                    try
                    {
                        var a = smsModem.SendMessage_PDU(row.Cells[columnSoDienThoai].Value.ToString().Trim(), smsRieng);
                        switch (a)
                        {
                            case GSM_Phone.SendSmsStatus.ERROR:
                                indexGuiLoi.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.NONE:
                                indexGuiLoi.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.OK:
                                indexDaGui.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.UNKNOWN:
                                indexDaGui.Add(i);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        indexGuiLoi.Add(i);
                    }
                }
            }
            //sau khi gui xong
            sending = false;
        }
        private void SendFromTable()
        {
            sending = true;
            Control.CheckForIllegalCrossThreadCalls = false;
            indexDaGui = new List<int>();
            indexGuiLoi = new List<int>();
            messageText = txtMessage.Text;

            columnSoDienThoai = cboSoDienThoai.Text;
            columnTenNguoiNhan = cboTenNguoiNhan.Text;
            columnNoiDung = cboTuDuLieu.Text;

            tugo_noidung_tinnhan = radTuGoNoiDung.Checked;

            int columnCount = tableData.Columns.Count;
            int rowCount = tableData.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                //ListViewItem item = ahaSortListView1.Items[i];
                DataRow row = tableData.Rows[i];
                Dictionary<string, string> tt = new Dictionary<string, string>();
                string sms = "";
                
                for (int j = 0; j < columnCount; j++)
                {
                    tt.Add(tableData.Columns[j].ColumnName, row[j].ToString().Trim());
                }

                if (true)
                {
                    {
                        sms = row[2].ToString();
                    }

                    string smsRieng = TaoTinNhanRieng(sms, tt);

                    try
                    {
                        var a = smsModem.SendMessage_PDU(row[0].ToString().Trim(), smsRieng);
                        switch (a)
                        {
                            case GSM_Phone.SendSmsStatus.ERROR:
                                indexGuiLoi.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.NONE:
                                indexGuiLoi.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.OK:
                                indexDaGui.Add(i);
                                break;
                            case GSM_Phone.SendSmsStatus.UNKNOWN:
                                indexDaGui.Add(i);
                                break;
                            default:
                                break;
                        }
                    }
                    catch// (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        indexGuiLoi.Add(i);
                    }
                }
            }
            //sau khi gui xong
            sending = false;
        }

        private string TaoTinNhanRieng(string message, Dictionary<string,string> tt)
        {
            message = message.Replace("<ten>", "<" + columnTenNguoiNhan + ">");
            message = message.Replace("<sdt>", "<" + columnSoDienThoai + ">");
            foreach (var item in tt)
            {
                message = message.Replace("<"+item.Key+">", item.Value);
            }
            return message;
        }

        private bool Program_autoSend = false;
        private void timerGuiDanhSach_Tick(object sender, EventArgs e)
        {
            if(!sending && indexDaGui.Count==0 && indexGuiLoi.Count==0) //gui xong
            {
                timerGuiDanhSach.Stop();
                if (Program_autoSend)
                {
                    Exit();
                }
                else
                {
                    this.Text = "V6MultiSms";
                }
            }
            else
            {
                if (Program_autoSend)
                {
                    int count = indexDaGui.Count;
                    for (int i = 0; i < count; i++)
                    {
                        //int currentIndex = indexDaGui[0];
                        //DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        //string currentText = currentRow.Cells[columnTenNguoiNhan].Value.ToString();
                        //dataGridView1.Rows[currentIndex].Cells[columnTenNguoiNhan].Value = EditNameDaGui(currentText);
                        indexDaGui.RemoveAt(0);
                    }

                    count = indexGuiLoi.Count;
                    for (int i = 0; i < count; i++)
                    {
                        //int currentIndex = indexGuiLoi[0];
                        //DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        //string currentText = currentRow.Cells[columnTenNguoiNhan].Value.ToString();
                        //dataGridView1.Rows[currentIndex].Cells[columnTenNguoiNhan].Value = EditNameGuiLoi(currentText);

                        indexGuiLoi.RemoveAt(0);
                    }
                }
                else
                {
                    this.Text = "" + Text.Substring(1) + Text[0];

                    int count = indexDaGui.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexDaGui[0];
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        string currentText = currentRow.Cells[columnTenNguoiNhan].Value.ToString();
                        dataGridView1.Rows[currentIndex].Cells[columnTenNguoiNhan].Value
                            = EditNameDaGui(currentText);
                        indexDaGui.RemoveAt(0);
                    }

                    count = indexGuiLoi.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexGuiLoi[0];
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        string currentText = currentRow.Cells[columnTenNguoiNhan].Value.ToString();
                        dataGridView1.Rows[currentIndex].Cells[columnTenNguoiNhan].Value
                            = EditNameGuiLoi(currentText);

                        indexGuiLoi.RemoveAt(0);
                    }
                }
            }
        }

        private string EditNameDaGui(string currentText)
        {
            string result = "+" + currentText;
            return result;
        }
        private string EditNameGuiLoi(string currentText)
        {
            return "-" + currentText;
        }

        #region ==== Load Data ====
        //int readyLoad = 0;
        private void timerSend_Tick(object sender, EventArgs e)
        {
            //if(readyLoad == 3)
            //{
            //    LoadData();
            //    readyLoad++;
            //    timerLoad.Stop();
            //}
            //else
            //{
            //    readyLoad++;
            //}
        }


        private DataTable LoadData(string type, string file, string from_where = "", string to_con = "")
        {
            DataTable result = new DataTable();
            try
            {
                if (type == "dbf")
                {
                    result = V6Tools.ParseDBF.ReadDBF(file);
                    if (!string.IsNullOrEmpty(from_where) && !string.IsNullOrEmpty(to_con))
                        V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, from_where, to_con);
                }
                else if (type == "sql")
                {
                    SqlParameter[] plist = {};
                    result = V6BusinessHelper.ExecuteProcedure(file, plist).Tables[0];
                }
                else if (type == "excel")
                {
                    result = V6Tools.V6Convert.Excel_File.Sheet1ToDataTable(file);
                    if (!string.IsNullOrEmpty(from_where) && !string.IsNullOrEmpty(to_con))
                        V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, from_where, to_con);
                }
                else if (type == "text")
                {
                    result = V6Tools.V6Reader.TextFile.ToTable(file);
                    if (!string.IsNullOrEmpty(from_where) && !string.IsNullOrEmpty(to_con))
                        V6Tools.V6Convert.Data_Table.ChuyenMaTiengViet(result, from_where, to_con);
                }

                if (!result.Columns.Contains("Check"))
                    result.Columns.Add("Check");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        

        private void ViewData(DataTable data, bool add)
        {
            if (add)//reset
            {   
                //thêm data vào tbl
                foreach (DataRow row in data.Rows)
                {
                    DataRow newRow = tableData.NewRow();
                    for (int i = 0; i < tableData.Columns.Count && i<data.Columns.Count; i++)
                    {
                        newRow[i] = row[i].ToString().TrimEnd();
                    }
                    tableData.Rows.Add(newRow);
                }
            }
            else
            {
                //ahaSortListView1.Items.Clear();
                //dataGridView1.Rows.Clear();
                tableData = data;
                //dataGridView1.DataSource = null;
                dataGridView1.DataSource = tableData;

                //reset ten cot combobox
                cboSoDienThoai.Items.Clear();
                cboTenNguoiNhan.Items.Clear();
                cboTuDuLieu.Items.Clear();
                cboChenTT.Items.Clear();
                //Thêm tên cột vào các combobox
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    cboSoDienThoai.Items.Add(data.Columns[i].ColumnName);
                    cboTenNguoiNhan.Items.Add(data.Columns[i].ColumnName);
                    cboTuDuLieu.Items.Add(data.Columns[i].ColumnName);
                    cboChenTT.Items.Add(data.Columns[i].ColumnName);
                }
                cboSoDienThoai.SelectedIndex = 0;
                cboTenNguoiNhan.SelectedIndex = 1;
                cboTuDuLieu.SelectedIndex = 2;
            }

        }

        #endregion load data

        private void btnChonHet_Click(object sender, EventArgs e)
        {   
            //foreach (DataRow row in tableData.Rows)
            //{
            //    row[columnCheck] = "1";
            //}
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[columnCheck].Value = "1";
            }
        }

        private void btnBoChonHet_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[columnCheck].Value = "0";
            }
        }

        private void btnDaoLuaChon_Click(object sender, EventArgs e)
        {
            //foreach (DataRow row in tableData.Rows)
            //{
            //    bool old = row[columnCheck] == null ? false : row[columnCheck].ToString()=="1";
            //    row[columnCheck] = old?"0":"1";
            //}
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool old = row.Cells[columnCheck].Value == null ? false : row.Cells[columnCheck].Value.ToString() == "1";
                row.Cells[columnCheck].Value = old ? "0" : "1";
            }
        }

        

        private void btnChonTen_Click(object sender, EventArgs e)
        {
            string s = txtChonTen.Text.Trim();
            string ten = "";
            columnTenNguoiNhan = cboTenNguoiNhan.Text;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                ten = row.Cells[columnTenNguoiNhan].Value == null?
                    "" : row.Cells[columnTenNguoiNhan].Value.ToString();
                if (chkIgnore.Checked)
                {
                    row.Cells[columnCheck].Value = ten.ToLower().Contains(s.ToLower()) ? "1" : "0";
                }
                else
                {
                    row.Cells[columnCheck].Value = ten.Contains(s) ? "1" : "0";
                }
            }
        }

        
        private void Exit()
        {
            _hardExit = true;
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Program.setting.SaveSetting();

            if (!_hardExit)
                if (MessageBox.Show("Thoát?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                {
                    e.Cancel = true;
                }
        }

        private void btnChonTuFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                DataTable loadTable = new DataTable();
                string ext = Path.GetExtension(openFileDialog1.FileName).ToLower();
                switch (ext)
                {
                    case ".xlsx": case ".xls":
                        loadTable = LoadData("excel", openFileDialog1.FileName, "", "");
                        break;
                    case ".dbf":
                        loadTable = LoadData("dbf", openFileDialog1.FileName);
                        break;
                    case ".txt":
                        loadTable = LoadData("text", openFileDialog1.FileName);
                        break;
                    default:
                        break;
                }
                                
                ViewData(loadTable, chkThemVao.Checked);
            }
        }

        private void btnXuatFileText_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "V6MultiSms_Data_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".txt";
                if(V6Tools.V6Export.Data_Table.ToTextFile(tableData, fileName))
                    MessageBox.Show("Đã xuất file " + fileName);

                //V6Tools.V6Export.Data_Table.ToDBF(tableData, "test.dbf");
                V6Tools.ParseDBF.WriteDBF(tableData, "test.dbf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radTuGoNoiDung_CheckedChanged(object sender, EventArgs e)
        {
            if(radTuGoNoiDung.Checked)
            {
                txtMessage.Enabled = true;
                cboTuDuLieu.Enabled = false;
            }
            else
            {
                txtMessage.Enabled = false;
                cboTuDuLieu.Enabled = true;
            }
        }

        private void cboChenTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectionStar = txtMessage.SelectionStart;
            //string s1 = txtMessage.Text.Substring(0, txtMessage.SelectionStart);
            //string s2 = txtMessage.Text.Substring(0, txtMessage.SelectionStart);

            string s3 = "<" + cboChenTT.Text + ">";
            txtMessage.Paste(s3);
            txtMessage.SelectionStart = selectionStar + s3.Length;
            if(txtMessage.Enabled)
                txtMessage.Focus();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Cancel = true;
            e.ThrowException = false;
        }


    }
}
