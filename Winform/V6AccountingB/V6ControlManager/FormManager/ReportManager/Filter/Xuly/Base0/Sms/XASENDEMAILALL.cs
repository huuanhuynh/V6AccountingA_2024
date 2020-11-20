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
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.Filter.Base0.Sms
{
    public partial class XASENDEMAILALL : FilterBase
    {
        public XASENDEMAILALL()
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
                Anchor = (AnchorStyles) 0xF;
                ExecuteMode = ExecuteMode.ExecuteProcedure;
                lineMact.SetValue("POH");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public override List<SqlParameter> GetFilterParameters()
        {
            var result = new List<SqlParameter>();
            result.Add(new SqlParameter("@Ngay_ct1", dateNgay_ct1.YYYYMMDD));
            result.Add(new SqlParameter("@Ngay_ct2", dateNgay_ct2.YYYYMMDD));
            result.Add(new SqlParameter("@LSTMA_CT", lineMact.StringValue));
            var key0 = GetFilterStringByFields(new List<string>()
            {
                "MA_DVCS","MA_BP", "MA_KH"
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
                if (dataGridView1.RowCount > 0) btnGuiDanhSach.Enabled = true;
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
                AldmConfig aldm = ConfigManager.GetAldmConfig("ASENDEMAILALL");
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridView1, aldm.GRDS_V1, aldm.GRDF_V1,
                    V6Setting.IsVietnamese ? aldm.GRDHV_V1 : aldm.GRDHE_V1);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView", ex);
            }
        }

        private void btnGui1_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmMessage("Gửi email?", "Xác nhận") != DialogResult.Yes) return;

            if (txtSendTo_Test.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập địa chỉ nhận!");
                return;
            }

            try
            {
                if (!string.IsNullOrEmpty(V6Login.XmlInfo.Email))
                {
                    var a = V6ControlFormHelper.SendEmail(V6Login.XmlInfo.Email, V6Login.XmlInfo.EmailPassword,
                        txtSendTo_Test.Text.Trim(), "V6SOFTWARE", txtNoiDung_Test.Text, "");
                    if (a)
                    {
                        ShowMainMessage("Đã thực hiện.");
                    }
                    else
                    {
                        this.ShowErrorMessage("Gửi lỗi.");
                    }
                }
                else
                {
                    MessageBox.Show("Chưa cấu hình user emal (xmlinfor)!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnGuiDanhSach_Click(object sender, EventArgs e)
        {
            if (this.ShowConfirmMessage("Gửi email cho danh sách đã chọn?", "Xác nhận") != DialogResult.Yes) return;
            
            if(sending)
            {
                MessageBox.Show("Đang gửi");
                return;
            }

            try
            {
                btnGuiDanhSach.Enabled = false;
                if (!string.IsNullOrEmpty(V6Login.XmlInfo.Email))
                {
                    sending = true;
                    Thread t = new Thread(SendFromGrid);
                    t.IsBackground = true;
                    t.Start();
                    timerGuiDanhSach.Start();
                }
                else
                {
                    MessageBox.Show("Chưa cấu hình user emal (xmlinfor)!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        bool sending = false;
        List<int> indexDaGui = new List<int>();
        List<int> indexGuiLoi = new List<int>();
        string messageText = "";
        private string sendto;

        string columnCheck = "Check";
        string columnSendTo = "EMAIL_ADDR";
        string columnNoiDung = "EMAIL_NOTE";
        

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
                string noiDung = "";
                foreach (DataGridViewCell item in row.Cells)
                {
                    tt.Add(dataGridView1.Columns[item.ColumnIndex].DataPropertyName,
                        item.Value == null ? "" : item.Value.ToString().Trim());
                }
                
                string checkSend = row.Cells[columnCheck].Value == null ? "" : row.Cells[columnCheck].Value.ToString();

                if(checkSend == "1")
                {
                    noiDung = row.Cells[columnNoiDung].Value.ToString();

                    string noiDungRieng = TaoTinNhanRieng(noiDung, tt);
                    try
                    {
                        var a = V6ControlFormHelper.SendEmail(V6Login.XmlInfo.Email, V6Login.XmlInfo.EmailPassword,
                            row.Cells[columnSendTo].Value.ToString().Trim(), "V6SOFTWARE", noiDungRieng, "");
                        if (a)
                        {

                        }
                        else
                        {
                            indexGuiLoi.Add(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        error_messages += "EMAIL: " + ex.Message + "\n";
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

        private void timerGuiDanhSach_Tick(object sender, EventArgs e)
        {
            if(!sending) //gui xong
            {
                timerGuiDanhSach.Stop();
                btnGuiDanhSach.Enabled = true;
                ShowMainMessage(V6Text.Finish);

                try
                {
                    // Hiển thị trạng thái gửi bằng màu sắc, gom thông tin.

                    int count = indexDaGui.Count;
                    string Lstt_rec = "";
                    string Lma_ct = "";
                    List<DataGridViewRow> listRow = new List<DataGridViewRow>();
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexDaGui[0];
                        // check đã gửi, nên đổi thành màu sắc.
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        listRow.Add(currentRow);
                        //currentRow.DefaultCellStyle.BackColor = Color.GreenYellow;
                        //string currentText = currentRow.Cells[columnSoDienThoai].Value.ToString();
                        //dataGridView1.Rows[currentIndex].Cells[columnSoDienThoai].Value = EditNameDaGui(currentText);
                        Lstt_rec += ";" + currentRow.Cells["STT_REC"].Value.ToString().Trim();
                        Lma_ct += ";" + currentRow.Cells["MA_CT"].Value.ToString().Trim();
                        indexDaGui.RemoveAt(0);
                    }

                    // Update
                    if (Lstt_rec.Length > 0 && Lma_ct.Length > 0)
                    {
                        Lstt_rec = Lstt_rec.Substring(1);
                        Lma_ct = Lma_ct.Substring(1);
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Lstt_rec", Lstt_rec),
                            new SqlParameter("@Lma_ct", Lma_ct),
                            new SqlParameter("@UserID", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery("AAPPR_XULY_ALL_EMAIL", plist);
                    }

                    foreach (DataGridViewRow row in listRow)
                    {
                        dataGridView1.Rows.Remove(row);
                    }


                    count = indexGuiLoi.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int currentIndex = indexGuiLoi[0];
                        DataGridViewRow currentRow = dataGridView1.Rows[currentIndex];
                        currentRow.DefaultCellStyle.BackColor = Color.Red;
                        //string currentText = currentRow.Cells[columnSoDienThoai].Value.ToString();
                        //dataGridView1.Rows[currentIndex].Cells[columnSoDienThoai].Value = EditNameGuiLoi(currentText);

                        indexGuiLoi.RemoveAt(0);
                    }
                }
                catch (Exception ex)
                {
                    this.WriteExLog(GetType() + ".timerGuiDanhSach_Tick", ex);
                }
            }
            else
            {
                SetStatusText("Đã gửi " + indexDaGui.Count);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = dataGridView1.SelectedCells[0].RowIndex;
                _oldIndex = index;

                UpdateGridView2(dataGridView1.Rows[index]);

            }
            else
            {
                dataGridView2.DataSource = null;
            }
        }

        protected int _oldIndex = -1;
        public string _sttRec = null;
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                if (row != null && _oldIndex != row.Index)
                {
                    _oldIndex = row.Index;
                    if (dataGridView1.Columns.Contains("STT_REC"))
                    {
                        _sttRec = row.Cells["STT_REC"].Value.ToString();
                    }
                    UpdateGridView2(row);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Gridview1Select", ex);
            }
        }

        public void UpdateGridView2(DataGridViewRow row)
        {
            ViewDetails(row);
            FormatGridView2();
        }

        private string mact_format = null;
        protected void FormatGridView2()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                string mact = dataGridView1.CurrentRow.Cells["Ma_ct"].Value.ToString().Trim();
                if (mact != mact_format)
                {
                    mact_format = mact;
                    //var alctconfig = ConfigManager.GetAlctConfig(mact);
                    var aldmConfig = ConfigManager.GetAldmConfig("AAPPR_ALL_AD_" + mact);
                    if (!aldmConfig.HaveInfo) return;

                    var headerString = V6Setting.IsVietnamese ? aldmConfig.GRDHV_V1 : aldmConfig.GRDHE_V1;
                    V6ControlFormHelper.FormatGridViewAndHeader(dataGridView2, aldmConfig.GRDS_V1, aldmConfig.GRDF_V1, headerString);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FormatGridView2", ex);
            }
        }

        protected void ViewDetails(DataGridViewRow row)
        {
            try
            {
                if (row != null)
                {
                    var ngay_ct = ObjectAndString.ObjectToFullDateTime(row.Cells["ngay_ct"].Value);
                    var sttRec = row.Cells["Stt_rec"].Value.ToString().Trim();
                    var ma_ct = row.Cells["Ma_ct"].Value.ToString().Trim();
                    DataTable data = null;

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@ngay_ct", ngay_ct.ToString("yyyyMMdd")),
                        new SqlParameter("@ma_ct", ma_ct),
                        new SqlParameter("@stt_rec", sttRec),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@advance", ""),
                    };
                    data = V6BusinessHelper.ExecuteProcedure("AAPPR_XULY_ALL_EMAIL_AD", plist).Tables[0];

                    dataGridView2.AutoGenerateColumns = true;
                    dataGridView2.DataSource = data;
                }
                else
                {
                    dataGridView2.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XASENDEMAILALL ViewDetails: " + ex.Message);
            }
        }


    }
}
