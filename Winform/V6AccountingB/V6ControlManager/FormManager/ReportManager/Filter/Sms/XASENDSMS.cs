using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using GSM;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter.Sms
{
    public partial class XASENDSMS : FilterBase
    {
        public XASENDSMS()
        {
            InitializeComponent();
            
            MyInit();
        }

        DataTable tableData;
        //private string Program_dataType0, Program_file_proc, Program_from_where, Program_to_conString;

        void MyInit()
        {
            try
            {
                ExecuteMode = ExecuteMode.ExecuteProcedure;
                lineMact.SetValue("BC1,TA1");
                ViewConnecting();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@ngay_ct1", dateNgay_ct1.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@ngay_ct2", dateNgay_ct2.Value.ToString("yyyyMMdd")));
            result.Add(new SqlParameter("@LSTMA_CT", lineMact.StringValue));
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_KH", "TK"
            }, true);
            result.Add(new SqlParameter("@advance", key0));

            return result;
        }

        public override void LoadDataFinish(DataSet ds)
        {
            try
            {
                _ds = ds;
                ViewData(_ds.Tables[0]);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadDataFinish", ex);
            }
        }

        public override void FormatGridView(V6ColorDataGridView dataGridView1)
        {
            dataGridView1 = this.dataGridView1;
            try
            {
                AldmConfig aldm = V6ControlsHelper.GetAldmConfig("ASENDSMS");
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, aldm.GRDS_V1, aldm.GRDF_V1,
                    V6Setting.IsVietnamese ? aldm.GRDHV_V1 : aldm.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private void ViewConnecting()
        {
            try
            {
                if (V6ControlFormHelper.SmsModem != null && V6ControlFormHelper.SmsModem.GSM_PORT.IsOpen)
                {
                    txtConnectPort.Text = "Đã kết nối " + V6ControlFormHelper.SmsModem.PortName + ":" + V6ControlFormHelper.SmsModem.Operator;
                }
                else
                {
                    txtConnectPort.Text = "Chưa kết nối";
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ViewConnecting", ex);
            }
        }
        
        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnTuKetNoi_Click(object sender, EventArgs e)
        {
            AutoConnect();
        }

        private void btnGui1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Gửi tin nhắn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;
            if (txtSmsTo.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập số!");
                return;
            }

            try
            {
                if (V6ControlFormHelper.SmsModem.GSM_PORT != null)
                {
                    if (!V6ControlFormHelper.SmsModem.GSM_PORT.IsOpen)
                        V6ControlFormHelper.SmsModem.OpenPort();
                    
                    
                    var a = V6ControlFormHelper.SmsModem.SendMessage_PDU(txtSmsTo.Text.Trim(), "V6 test send mmessage.", true);
                    switch (a)
                    {
                        case GSM_Phone.SendSmsStatus.ERROR:
                            //lblNoiDung.Text = "Gửi lỗi!";
                            break;
                        case GSM_Phone.SendSmsStatus.NONE:
                            //lblNoiDung.Text = "Không gửi được hay gì đó!";
                            break;
                        case GSM_Phone.SendSmsStatus.OK:
                            //lblNoiDung.Text = "Gửi Ok";
                            break;
                        case GSM_Phone.SendSmsStatus.UNKNOWN:
                            //lblNoiDung.Text = "Không biết gửi được không";
                            break;
                        default:
                            //lblNoiDung.Text = "???";
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
        
        private void btnGuiDanhSach_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Gửi tin nhắn cho danh sách đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;
            
            if(sending)
            {
                MessageBox.Show("Đang gửi");
                return;
            }

            try
            {
                if (V6ControlFormHelper.SmsModem.GSM_PORT != null)
                {
                    if (!V6ControlFormHelper.SmsModem.GSM_PORT.IsOpen)
                        V6ControlFormHelper.SmsModem.OpenPort();

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
        
        private void Connect()
        {
            V6ControlFormHelper.ConnectModemSms();
            //new SmsModemSettingForm().ShowDialog();
            ViewConnecting();
        }
        private void AutoConnect()
        {
            V6ControlFormHelper.ConnectModemSms(true);
            ViewConnecting();
        }

        bool sending = false;
        List<int> indexDaGui = new List<int>();
        List<int> indexGuiLoi = new List<int>();
        string messageText = "";
        private string sendto;

        string columnCheck = "Check";
        string columnSoDienThoai = "SMS_PHONE";
        string columnNoiDung = "SMS_NOTE";
        

        //int timerIndex = 0;
        private void SendFromGrid()
        {
            sending = true;
            CheckForIllegalCrossThreadCalls = false;
            indexDaGui = new List<int>();
            indexGuiLoi = new List<int>();
            //messageText = "";
            
            string error_messages = "";

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
                    sms = row.Cells[columnNoiDung].Value.ToString();

                    string smsRieng = TaoTinNhanRieng(sms, tt);
                    try
                    {
                        var a =
                            V6ControlFormHelper.SmsModem.SendMessage_PDU(
                                row.Cells[columnSoDienThoai].Value.ToString().Trim(), smsRieng);
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
                        error_messages += "SMS: " + ex.Message + "\n";
                        indexGuiLoi.Add(i);
                    }
                }
            }

            if (error_messages.Length > 0)
            {
                this.ShowErrorMessage(error_messages);
            }
            //sau khi gui xong
            sending = false;
        }
        
        private string TaoTinNhanRieng(string message, Dictionary<string,string> tt)
        {
            //message = message.Replace("<sdt>", "<" + columnSoDienThoai + ">");
            foreach (var item in tt)
            {
                message = message.Replace("{"+item.Key+"}", item.Value);
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
                    //Exit();
                }
                else
                {
                    //this.Text = "V6MultiSms";
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
                    //this.Text = "" + Text.Substring(1) + Text[0];

                    int count = indexDaGui.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexDaGui[0];
                        //!!!!! check đã gửi, nên đổi thành màu sắc.
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        string currentText = currentRow.Cells[columnSoDienThoai].Value.ToString();
                        dataGridView1.Rows[currentIndex].Cells[columnSoDienThoai].Value = EditNameDaGui(currentText);
                        indexDaGui.RemoveAt(0);
                    }

                    count = indexGuiLoi.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexGuiLoi[0];
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        string currentText = currentRow.Cells[columnSoDienThoai].Value.ToString();
                        dataGridView1.Rows[currentIndex].Cells[columnSoDienThoai].Value
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
        
        /// <summary>
        /// Hiển thị dữ liệu lên gridview
        /// </summary>
        /// <param name="data">Dữ liệu sẽ hiển thị</param>
        /// <param name="add">Cộng dồn dữ liệu cũ</param>
        private void ViewData(DataTable data, bool add = false)
        {
            if (add)//reset
            {   
                //thêm data vào tbl
                foreach (DataRow row in data.Rows)
                {
                    tableData.AddRow(row.ToDataDictionary());
                }
            }
            else
            {
                tableData = data;
                dataGridView1.DataSource = tableData;

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
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool old = row.Cells[columnCheck].Value != null && row.Cells[columnCheck].Value.ToString() == "1";
                row.Cells[columnCheck].Value = old ? "0" : "1";
            }
        }
        
        private void btnChonTheoTen_Click(object sender, EventArgs e)
        {
            //string s = txtChonTen.Text.Trim();
            //string ten = "";
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    ten = row.Cells[columnTenNguoiNhan].Value == null?
            //        "" : row.Cells[columnTenNguoiNhan].Value.ToString();
            //    if (chkIgnore.Checked)
            //    {
            //        row.Cells[columnCheck].Value = ten.ToLower().Contains(s.ToLower()) ? "1" : "0";
            //    }
            //    else
            //    {
            //        row.Cells[columnCheck].Value = ten.Contains(s) ? "1" : "0";
            //    }
            //}
        }
        
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Cancel = true;
            e.ThrowException = false;
        }
    }
}
