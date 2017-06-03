using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using V6ThreadLibrary;
using V6Library;

namespace V6MDBtoSQLthread
{
    public partial class FormMain : Form
    {

        void MyInit()
        {
            V6SyncUtility.ReadAndSetEnableSetting(this, "EnableSetting.ini");
            LoadSettingToForm();

            string xmltablefile = _Setting.GetSetting("LastOpenXmlTable");
            if (string.IsNullOrEmpty(xmltablefile))
            {
                //btnLoadXmlTable_Click(null, null);
            }
            else
            {
                LoadXmlTableToGrid(xmltablefile);
            }
            
        }

        
           

        public FormMain()
        {   
            InitializeComponent();
            MyInit();
            if (checkBoxRunSyncOnStart.Checked)
            {
                btnRun_Click(null, null);
            }
        }
        
        #region ==== Variables ====
        MyThreads MultiThreads;
        V6SyncSetting _Setting;// = new Setting(Application.StartupPath + "\\Setting.ini");

        public static bool _Exit;
        private bool _IsRunning;
        long _Sleepedtime;
        long _SleepTime_m, _SleepTime_s;

        public static string CONSTRING = "";
        public static DataTable dtServerList;
        public static DataView dvServerList;

        public const string __DialogTitle = "V6MDBtoSQL";

        #endregion ========
        
        #region ==== Form function ====

        private void LoadSettingToForm()
        {
            _Setting = new V6SyncSetting(Application.StartupPath + "\\Setting.ini");
            
            var readString = _Setting.GetSetting("SleepTime_m");
            #region ==== SleepTime ====
            try
            {
                _SleepTime_m = int.Parse(readString);
                txtSleepTime_m.Text = readString;
            }
            catch
            {
                _SleepTime_m = 10;
                txtSleepTime_m.Text = "10";
                _Setting.SetSetting("SleepTime_m", "10");
            }
            readString = _Setting.GetSetting("SleepTime_s");
            try
            {
                _SleepTime_s = int.Parse(readString);
                txtSleepTime_s.Text = readString;
            }
            catch
            {
                _SleepTime_s = 10;
                txtSleepTime_s.Text = "10";
                _Setting.SetSetting("SleepTime_s", "10");
            }
            #endregion SleepTime
            
            #region ==== RunSyncOnStart ====
            
            try
            {
                checkBoxRunSyncOnStart.Checked = _Setting.GetSetting("RunSyncOnStart") == "1";
            }
            catch
            {
                checkBoxRunSyncOnStart.Checked = false;
                _Setting.SetSetting("RunSyncOnStart", "0");
            }
            #endregion RunSyncOnStart

            #region ==== ShowOnStart ====
            checkBoxShowOnStart.Checked = V6SyncUtility.CheckShowOnStartUp();
            #endregion ShowOnStart
            
            #region ==== StopWhenFinish ====
            
            try
            {
                bool booltemp = _Setting.GetSetting("StopWhenFinish") == "1";
                checkBoxStopWhenFinish.Checked = booltemp;
                txtSleepTime_m.Enabled = !booltemp;
                txtSleepTime_s.Enabled = !booltemp;
                btnSetSleepTime.Enabled = !booltemp;
            }
            catch
            {
                checkBoxStopWhenFinish.Checked = false;
                _Setting.SetSetting("StopWhenFinish", "0");
            }
            #endregion StopWhenFinish
        }
        
        void CreateThreads()
        {   
            try
            {
                MultiThreads = new MyThreads(_Setting);
                
                if (dvServerList != null)
                {
                    DataTable dtbSLt = dvServerList.ToTable();
                    for (int i = 0; i < dvServerList.Count; i++)
                    {
                        if (dgvServerConfig.Rows[i].Cells["Run"].Value.ToString() == "1")
                            MultiThreads.Add(new
                                MyThread(
                                _Setting,
                                i,
                                dtbSLt.Rows[i][1].ToString(),
                                dtbSLt.Rows[i]));
                    }
                }
                else
                {
                    MessageBox.Show("Chưa có cấu hình server nào!", __DialogTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StopThread();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CreateThreads\n" + ex.Message, __DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MultiThreads = new MyThreads(_Setting);
                dtServerList = null;
            }
        }
        
        void SaveNewXmlTable(DataTable dtb)
        {
            SaveFileDialog sf = new SaveFileDialog
            {
                Filter = "XmlTable file|*.xtb",
                DefaultExt = "xtb"
            };
            if (sf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dtb.WriteXml(sf.FileName);
                    dgvServerConfig.AccessibleName = sf.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ExitProgram()
        {   
            if (MessageBox.Show("Thoát chương trình?", __DialogTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question)
                    == DialogResult.OK)
            {
                _Exit = true;
                StopThread();
                Application.Exit();
                
                notifyIcon1.Visible = false;
            }
            else
            {
                _Exit = false;
            }
        }
        void HideProgram()
        {
            Visible = false;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(100, "Chú ý!", "Để hiện lại chương trình hãy nhấp đôi chuột vào đây!", ToolTipIcon.Info);
        }
        void ShowProgram()
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            Activate();
        }

        /// <summary>
        /// Trả về value của Key, không có trả về chuỗi rỗng ""
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetXmlValue(string XmlFile, string Key)
        {
            //string xmlFile = APPPATH + xmlFileName;
            XmlTextReader reader = new XmlTextReader(XmlFile.ToLower());
            try
            {
                string value = "";
                while (reader.Read())
                {
                    if (reader.Name.ToUpper() == Key.ToUpper())
                    {
                        value = reader.GetAttribute("value");
                        break;
                    }
                }
                reader.Close();
                return value;
            }
            catch
            {
                reader.Close();
                return "";
            }
        }

        /// <summary>
        /// Đọc lại main config xml.
        /// </summary>
        private void LoadMainConfigXml()
        {
            try
            {
                string mainxmlfile = Path.Combine(Application.StartupPath, Process.GetCurrentProcess().ProcessName + ".xml");
                string __hhfrom = GetXmlValue(mainxmlfile, "Timefrom");
                string __hhto = GetXmlValue(mainxmlfile, "Timeto");
                string __sqlYN = GetXmlValue(mainxmlfile, "SqlYN");
                CONSTRING = UtilityHelper.ReadConnectionStringFromFileXML(mainxmlfile);

                if (__hhfrom == "")
                    __hhfrom = "07";
                if (__hhto == "")
                    __hhto = "21";
                if (__sqlYN == "")
                    __sqlYN = "1";

                _hhfrom = Convert.ToInt16(__hhfrom);
                _hhto = Convert.ToInt16(__hhto);
                _sqlYN = "1" == __sqlYN;
            }
            catch (Exception ex)
            {
                throw new Exception("InitMainConfig error. " + ex.Message);
            }

        }

        private int _hhfrom = 07, _hhto = 21;
        private int _outTimeCount;
        private bool _sqlYN;

        bool IsInTime()
        {
            LoadMainConfigXml();

            int hh;//Lấy thời gian hiện tại theo kiểu HH
            if (_sqlYN)
            {
                string strSQL0 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108) AS curTime";
                DataTable tb3 = SqlHelper.ExecuteDataset(CONSTRING, CommandType.Text, strSQL0).Tables[0];
                hh = Convert.ToInt16(tb3.Rows[0][0].ToString().Trim().Substring(0, 2));
            }
            else
            {
                hh = DateTime.Now.Hour;
            }

            if (hh >= _hhfrom && hh <= _hhto)
            {
                return true;
            }
            else { return false; }
        }

        void StartThread()
        {
            try
            {
                //foreach (DataGridViewRow item in dgvServerConfig.Rows)
                //{
                //    item.DefaultCellStyle.ForeColor = Color.Black;
                //}
                //MultiThreads.Start();

                //_IsRunning = true;
                //btn1By1.Enabled = false;
                //start1By1ToolStripMenuItem.Enabled = false;
                //startMultiToolStripMenuItem.Enabled = false;
                //btnStop.Enabled = true;

                //StopToolStripMenuItem.Enabled = true;
                //timerRunning.Start();


                //Newcode
                if (IsInTime())
                {
                    _outTimeCount = 0;
                    foreach (DataGridViewRow item in dgvServerConfig.Rows)
                    {
                        item.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    MultiThreads.IsInTime = true;
                    MultiThreads.Start();
                    _IsRunning = true;
                    timerRunning.Start();
                }
                else//outTime
                {
                    _outTimeCount++;
                    MultiThreads.IsInTime = false;
                    if (_outTimeCount == 1)
                    {
                        MultiThreads.Start();
                        _IsRunning = true;
                        //timerRunning.Start();
                    }
                    else if (_outTimeCount == 10)
                    {

                    }
                    timerRunning.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FormMain StartThread: " + ex.Message);
            }
        }
        void StopThread()
        {
            try
            {
                _IsRunning = false;
                btn1By1.Enabled = true;
                start1By1ToolStripMenuItem.Enabled = true;
                
                btnStop.Enabled = false;

                StopToolStripMenuItem.Enabled = false;
                //timerStatus.Stop();
                timerSleep.Stop();
                if (MultiThreads != null) MultiThreads.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ViewStatus()
        {
            lblMultiThreadStatus.Text = MultiThreads.Message;
            foreach (MyThread item in MultiThreads)
            {
                if (!dtServerList.Columns.Contains("Status"))
                {
                    dtServerList.Columns.Add("Status");
                }

                dgvServerConfig.Rows[item._Index].Cells["Status"].Value =
                    item._ThreadName + 
                    ", " + item._Status +
                    ", " + item._Message;

                if (item._Error)//_Status == -2
                {
                    dgvServerConfig.Rows[item._Index].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (item._Status == -1)
                {
                    dgvServerConfig.Rows[item._Index].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if(item._Status == -3)
                {
                    dgvServerConfig.Rows[item._Index].DefaultCellStyle.ForeColor = Color.DarkGray;
                }
                
            }
        }
        void UpdateProgress()
        {
            string p = lblProgress.Text;
            lblProgress.Text = p.Substring(p.Length - 1) + p.Substring(0, p.Length - 1);
            //if (lblProgress.Text.Length < 20)
            //    lblProgress.Text += "|";
            //else lblProgress.Text = "";
        }

        void SaveXmlTableConfig()
        {
            if (!string.IsNullOrEmpty(dgvServerConfig.AccessibleName))
            {
                try
                {
                    dtServerList.WriteXml(dgvServerConfig.AccessibleName,true);
                    MessageBox.Show("Đã lưu!", __DialogTitle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu lỗi!\n" + ex.Message, __DialogTitle);
                }
            }
            else
            {
                SaveNewXmlTable(dtServerList);
            }
        }
        void OpenXmlTableConfig()
        {
            if (_IsRunning)
            {
                MessageBox.Show("Hãy dừng các tiến trình trước!",__DialogTitle);
                return;
            }

            OpenFileDialog o = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "XmlTable file|*.xtb|All files|*.*",
                DefaultExt = "xtb"
            };
            try
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    LoadXmlTableToGrid(o.FileName);
                    _Setting.SetSetting("LastOpenXmlTable", o.FileName);
                    
                    //CreateThreads(_CurrentRunmode);
                    _IsRunning = false;
                    btn1By1.Enabled = true;
                    btnStop.Enabled = false;
                    //timerStatus.Stop();
                    timerSleep.Stop();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadXmlTableToGrid(string fileName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileName);                

                if (ds.Tables.Count > 0)
                {
                    dtServerList = ds.Tables[0];
                    //Thêm các cột thiếu.
                    //Nếu chưa có cột server
                    if (!dtServerList.Columns.Contains("STT"))
                        dtServerList.Columns.Add("STT", typeof(string));
                    if (!dtServerList.Columns.Contains("Server"))
                        dtServerList.Columns.Add("Server", typeof(string));
                    //if (!dtServerList.Columns.Contains("Database"))
                    //    dtServerList.Columns.Add("Database", typeof(string));
                    //if (!dtServerList.Columns.Contains("User"))
                    //    dtServerList.Columns.Add("User", typeof(string));
                    //if (!dtServerList.Columns.Contains("EPass"))
                    //    dtServerList.Columns.Add("EPass", typeof(string));
                    if (!dtServerList.Columns.Contains("Run"))
                        dtServerList.Columns.Add("Run", typeof(string));
                    if (!dtServerList.Columns.Contains("Status"))
                        dtServerList.Columns.Add("Status", typeof(string));
                    //
                    dgvServerConfig.AccessibleName = fileName;
                }
                else
                {
                    dtServerList = new DataTable();
                    dgvServerConfig.AccessibleName = "";
                    MessageBox.Show("Không đọc được bảng dữ liệu nào!");
                }
                //change(tbl);

                BindingSource bSource = new BindingSource();
                dvServerList = new DataView(dtServerList) {Sort = "STT asc"};
                
                bSource.DataSource = dvServerList;

                //dataGridView1.Columns.Clear();
                dgvServerConfig.DataSource = bSource;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error! - " + __DialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        #endregion ==== Form function ====




        #region ==== SystemTray MenuTrip ====
        
        private void hienChuongTrinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProgram();
        }
        
        private void thoatChuongTrinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void start1By1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRun_Click(sender, e);
        }
        
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStop_Click(sender, e);
        }
        
        #endregion ==== SystemTray MenuTrip ====

        #region ==== Form Event ====
        private void FormMain_Load(object sender, EventArgs e)
        {
            checkBoxRunWithSystem.Checked = V6SyncUtility.CheckRunOnStartUp();
            
            

            //giá trị Checked thay đổi sẽ chạy hàm checkBox1_CheckedChanged
            // SetRunOnStartUp(checkBox1.Checked);
            
        }
        private void FormHuuanSms_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                HideProgram();
            }
        }
        private void FormHuuanSms_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_Exit)
            {
                HideProgram();
                e.Cancel = true;
            }            
        }
        
        #endregion ==== Form Event ====

        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        #region ==== GroupBox options ====

        private void checkBoxRun_CheckedChanged(object sender, EventArgs e)
        {
            //V6SyncUtility.SetRunOnStartUp(((CheckBox)sender).Checked);
            V6SyncUtility.SetRunForLocalMachineOnStartUp(((CheckBox)sender).Checked);
        }
        
        private void checkBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            V6SyncUtility.SetShowOnStartUp(((CheckBox)sender).Checked);
        }
        
        private void checkBoxRunSyncOnStart_Click(object sender, EventArgs e)
        {
            _Setting.SetSetting("RunSyncOnStart", ((CheckBox)sender).Checked ? "1" : "0");
        }

        private void checkBoxStopWhenFinish_Click(object sender, EventArgs e)
        {
            _Setting.SetSetting("StopWhenFinish", checkBoxStopWhenFinish.Checked ? "1" : "0");
            if (checkBoxStopWhenFinish.Checked)
            {
                txtSleepTime_m.Enabled = false;
                txtSleepTime_s.Enabled = false;
                btnSetSleepTime.Enabled = false;
            }
            else
            {
                txtSleepTime_m.Enabled = true;
                txtSleepTime_s.Enabled = true;
                btnSetSleepTime.Enabled = true;
            }
        }

        #endregion groupbox options

        private void btnRun_Click(object sender, EventArgs e)
        {         
            try
            {
                CreateThreads();
                
                if (MultiThreads != null)
                {
                    StartThread();
                    //StartSeriServer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timerRunning_Tick(object sender, EventArgs e)
        {
            ViewStatus();
            if (_IsRunning) UpdateProgress();
            if (MultiThreads.Status != MyThreadStatus.Run)
            {
                if (checkBoxStopWhenFinish.Checked)
                {
                    StopThread();
                }
                else
                {
                    
                    timerRunning.Stop();

                    if (_IsRunning)
                    {
                        _IsRunning = false;
                        timerSleep.Start();
                    }
                }
            }
        }
        
        private void timerSleep_Tick(object sender, EventArgs e)
        {
            _Sleepedtime++;
            long sleeped_m = _Sleepedtime / 60, sleeped_s = _Sleepedtime % 60;

            lblSleep_m.Text = sleeped_m.ToString();
            lblSleep_s.Text = sleeped_s.ToString();
            if (sleeped_m == _SleepTime_m && sleeped_s > _SleepTime_s)
            {
                _Sleepedtime = 0;
                timerSleep.Stop();
                if (dvServerList != null)
                {
                    //CreateThreads(_CurrentRunmode);
                    StartThread();

                    _IsRunning = true;
                    lblSleep_s.Text = "Run";
                    timerRunning.Start();
                }
                else
                {
                    btnStop_Click(sender, e);
                }
            }
        }
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            //Nếu không chạy thì dừng luôn, nếu đang chạy thì hỏi!
            if (!_IsRunning ||
                (_IsRunning && MessageBox.Show("Vẫn có tiến trình đang chạy!\nBạn thực sự muốn dừng tiến trình?", __DialogTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                StopThread();
            }
        }


        private void btnHide_Click(object sender, EventArgs e)
        {
            try
            {
                HideProgram();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLoadXmlTable_Click(object sender, EventArgs e)
        {
            //if (new FormLogin().ShowDialog() == DialogResult.OK)
            {
                OpenXmlTableConfig();
            }
        }

        private void btnSaveXmlTable_Click(object sender, EventArgs e)
        {
            //if (new FormLogin().ShowDialog() == DialogResult.OK)
            {
                SaveXmlTableConfig();
            }            
        }

        private void btnEditTableFile_Click(object sender, EventArgs e)
        {
            {
                new FormXmlTable(dgvServerConfig.AccessibleName).Show();
                LoadXmlTableToGrid(dgvServerConfig.AccessibleName);
            }
        }


        private void btnStartNew1By1_Click(object sender, EventArgs e)
        {
            if (!_IsRunning || (_IsRunning && MessageBox.Show("Vẫn có tiến trình đang chạy!\nBạn thực sự muốn tạo và chạy lại tiến trình khác?", __DialogTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                CreateThreads();
                btnRun_Click(sender, e);
            }
        }
        
        private void NumberOnly(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSetSleepTime_Click(object sender, EventArgs e)
        {
            try
            {
                _SleepTime_m = int.Parse(txtSleepTime_m.Text);
                _SleepTime_s = int.Parse(txtSleepTime_s.Text);

                _Setting.SetSetting("SleepTime_m", _SleepTime_m.ToString());
                _Setting.SetSetting("SleepTime_s", _SleepTime_s.ToString());
                MessageBox.Show("OK");
            }
            catch
            {
                _SleepTime_m = 0;
                _SleepTime_s = 60;
                txtSleepTime_m.Text = "0";
                txtSleepTime_s.Text = "60";
            }
            
        }

        

        private void txtSleepTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSetSleepTime_Click(sender, e);
        }
        

        #region ==== DataGridView ====

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
                switch (e.Column.DataPropertyName.ToUpper())
                {
                        
                    case "GHICHU":
                        e.Column.Visible = true;
                        e.Column.ReadOnly = false;
                        break;

                    case "STT":
                    case "SERVER":
                    case "RUN":
                    case "STATUS":
                        e.Column.Visible = true;
                        e.Column.ReadOnly = true;
                        break;

                    default:
                        e.Column.Visible = true;
                        e.Column.ReadOnly = true;
                        break;
                }
            }
            catch
            {
                // ignored
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
    
            if (!dtServerList.Columns.Contains("Run"))
                dtServerList.Columns.Add("Run");

            string s = dgvServerConfig.Columns[e.ColumnIndex].DataPropertyName;

            if (s.ToUpper() == "RUN")
            {
                var cell = dgvServerConfig.Rows[e.RowIndex].Cells["Run"];

                cell.Value = cell.Value.ToString() == "1" ? "0" : "1";
            }
        }
       

        #endregion DataGridView

        
        private void dgvServerConfig_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        //Them vao su kien KeyDown cua DatagridView
        private void dgvServerConfig_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGridViewSelectedCellCollection selectedCells = dgvServerConfig.SelectedCells;

                if (selectedCells.Count > 0)
                {
                    int totalColumns = dgvServerConfig.Columns.Count;
                    int totalRows = dgvServerConfig.Rows.Count;
                    int selectedRowIndex = selectedCells[0].RowIndex;
                    int swapRow = -1;

                    DataGridViewRow dgvr =
                        dgvServerConfig.Rows[selectedRowIndex].Clone() as DataGridViewRow;

                    for (int i = 0; i < totalColumns; i++)
                    {
                        if (dgvr != null) dgvr.Cells[i].Value = dgvServerConfig.Rows[selectedRowIndex].Cells[i].Value;
                    }

                    switch (e.KeyCode)
                    {
                        case Keys.PageUp:   // Doi dong dang chon len tren
                            if (selectedRowIndex > 0)
                                swapRow = selectedRowIndex - 1;
                            break;

                        case Keys.PageDown:
                            if (selectedRowIndex < totalRows - 1)
                                swapRow = selectedRowIndex + 1;
                            break;

                        case Keys.Home:
                            if (selectedRowIndex > 0)
                                swapRow = 0;
                            break;

                        case Keys.End:
                            if (selectedRowIndex < totalRows - 1)
                                swapRow = totalRows - 1;
                            break;
                    }

                    if (swapRow != selectedRowIndex && swapRow >= 0)
                    {
                        for (int i = 0; i < totalColumns; i++)
                        {
                            dgvServerConfig.Rows[selectedRowIndex].Cells[i].Value =
                                dgvServerConfig.Rows[swapRow].Cells[i].Value;
                        }
                        for (int i = 0; i < dgvServerConfig.Columns.Count; i++)
                        {
                            if (dgvr != null)
                                dgvServerConfig.Rows[swapRow].Cells[i].Value = dgvr.Cells[i].Value;
                        }
                        foreach (DataGridViewCell cell in selectedCells)
                        {
                            cell.Selected = false;
                            dgvServerConfig.Rows[swapRow]
                                .Cells[cell.ColumnIndex].Selected = true;
                        }
                        e.Handled = true;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
				
			

        private void dgvServerConfig_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!dtServerList.Columns.Contains("Run"))
                dtServerList.Columns.Add("Run");

            string s = dgvServerConfig.Columns[e.ColumnIndex].DataPropertyName;

            if (s.ToUpper() == "RUN")
            {
                var value = e.Button == MouseButtons.Left ? 1 : 0;

                for (int i = 0; i < dgvServerConfig.Rows.Count; i++)
                {
                    dgvServerConfig.Rows[i].Cells["Run"].Value = value;
                }
            }
        }
       

    }
}
