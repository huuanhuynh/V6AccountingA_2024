using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6AccountingB
{
    public partial class FormLogin : V6Form
    {
        private int _count;
        private string _lblStatusOriginalText;
        public DataTable TblLanguage { get; set; }
        public DataTable TblModule { get; set; }
        
        public FormLogin()
        {
            InitializeComponent();
            MyInit();
        }

        public bool ReadyLogin { get; set; }
        bool ReadyReportLanguage = false;
        private bool _tblsLoaded, _allowClient;
        FileInfo fi;

        private void MyInit()
        {
            fi = new FileInfo(Application.ExecutablePath);
            this.Text = "LOGIN - Version " + Application.ProductVersion + " (" + fi.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss") + ")";
            //Phần này có thể đọc setting
            radLocalDataMode.Checked = true;

            _lblStatusOriginalText = lblStatus.Text;
            DatabaseConfig.LoadDatabaseConfig("V6Soft", Application.StartupPath);
            cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
            cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
            cboDatabase.DataSource = DatabaseConfig.ConnectionConfigData;
            cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
            cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
            cboDatabase.SelectedIndex = DatabaseConfig.GetConfigDataRunIndex();

            CheckCrystalInstaledAndOthers();
        }

        protected override void LoadLanguage()
        {
            
        }

        /// <summary>
        /// Reset lại biến trên form theo DataMode
        /// </summary>
        /// <param name="mode"></param>
        private void ResetInfos(GetDataMode mode)
        {
            try
            {
                V6Login.GetDataMode = mode;
                lblStatus.Text = _lblStatusOriginalText;
                panel1.Enabled = false;
                CheckConnectThread();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ResetInfos: " + ex.Message, "Login");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = V6Setting.LASTUSERW;
            //Khởi tạo với rad đang check. Mặc định local.
            //ResetInfos(radAPIDataMode.Checked ? GetDataMode.API : GetDataMode.Local);
            if (V6Setting.ReportLanguage == "V")
            {
                rTiengViet.Checked = true;
            }
            else if (V6Setting.ReportLanguage == "E")
            {
                rEnglish.Checked = true;
            }
            else if (V6Setting.ReportLanguage == "B")
            {
                rBothLang.Checked = true;
            }
            else // C
            {
                rCurrent.Checked = true;
            }

            Ready();
            if (string.IsNullOrEmpty(V6Setting.ReportLanguage))
            {
                rTiengViet.Checked = true;
                rbtLanguage_CheckedChanged(rTiengViet, new EventArgs());
            }
            
        }

        private string lblStatusSuccess = "______________________________________________";
        private string lblStatusSuccessIP = "________________________________";
        private string lblStatusFail = "___________ / _";
        private void CheckCrystalInstaledAndOthers()
        {
            try
            {
                if (Directory.Exists(@"C:\Windows\assembly\GAC_MSIL\CrystalDecisions.CrystalReports.Engine"))
                {
                    if (!File.Exists(Path.Combine(Application.StartupPath, "V6AccountingB.exe.config")))
                    {
                        lblStatusSuccess = "Connection OK! Lost exe.config";
                        lblStatusFail = "Connection Fail! Lost exe.config";
                    }
                }
                else
                {
                    lblStatusSuccess =  "Connection OK! Check CrystalReports!";
                    lblStatusFail =     "Connection Fail! Check CrystalReports!";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Font = new Font(lblStatus.Font, FontStyle.Bold);
                }
                //13.0.2000.0 is Crystal Reports for VS 2010
                //Assembly a = Assembly.Load("CrystalDecisions.CrystalReports.Engine");
            }
            catch (System.IO.FileNotFoundException)
            {
                //Not Installed
            }
            catch
            {
                //Runtime is in GAC but something else prevents it from loading. (bad install?, etc)
            }
        }

        private void CheckConnectThread()
        {
            Timer checkConnect = new Timer();
            checkConnect.Interval = 500;
            checkConnect.Tick += checkConnect_Tick;
            flagCheckConnectFinish = false;
            flagCheckConnectSuccess = false;

            new Thread(DoCheckConect)
            {
                IsBackground = true
            }
            .Start();

            checkConnect.Start();
        }

        Thread checkUpdateThread = null;
        private void CheckUpdateThread()
        {
            Timer checkUpdateTimer = new Timer();
            checkUpdateTimer.Interval = 500;
            checkUpdateTimer.Tick += checkUpdate_Tick;
            flagCheckUpdateFinish = false;
            flagCheckUpdateSuccess = false;
            auto_update = false;

            lastCheckConnectStatus = lblStatus.Text;
            lblStatus.Text = "Check update... Check update...";

            if (checkUpdateThread != null && checkUpdateThread.IsAlive) checkUpdateThread.Abort();

            checkUpdateThread = new Thread(DoCheckUpdate)
            {
                IsBackground = true
            };
            checkUpdateThread.Start();

            checkUpdateTimer.Start();
        }

        private bool flagCheckConnectFinish;
        private bool flagCheckUpdateFinish;
        private bool flagCheckConnectSuccess;
        private bool flagCheckUpdateSuccess;
        private bool flagUpdateAvailable;
        private bool auto_update;
        private string lastCheckConnectStatus = "";
        private string ftp_folder = "";
        private string exMessage = "";

        void checkConnect_Tick(object sender, EventArgs e)
        {
            if (_tblsLoaded)
            {
                cboLang.ValueMember = "Lan_Id";
                cboLang.DisplayMember = "Lan_name2";
                cboLang.DataSource = TblLanguage;
                cboLang.ValueMember = "Lan_Id";
                cboLang.DisplayMember = "Lan_name2";
                if (string.IsNullOrEmpty(V6Setting.Language)) V6Setting.Language = "V";
                cboLang.SelectedValue = V6Setting.Language;
                //cboLang.SelectedIndex = GetLangIndex(TblLanguage, V6Setting.Language);
                ReadyReportLanguage = true;
                
                cboModule.ValueMember = "module_id";
                cboModule.DisplayMember = "name";
                cboModule.DataSource = TblModule;
                cboModule.ValueMember = "module_id";
                cboModule.DisplayMember = "name";
            }
            if (flagCheckConnectFinish)
            {
                ((Timer)sender).Stop();
                if (flagCheckConnectSuccess && _tblsLoaded)
                {
                    if (DatabaseConfig.IsIPServer)
                    {
                        lblStatus.Text = "____" + DatabaseConfig.Server_IP + lblStatusSuccessIP;
                    }
                    else
                    {
                        lblStatus.Text = lblStatusSuccess;
                    }
                    
                    panel1.Enabled = true;
                    txtUserName.Focus();
                    V6Options.LoadValue();
                    this.Text = "LOGIN - Version " + Application.ProductVersion + " (" + fi.LastWriteTime.ToString("dd/MM/yyyy HH:mm:ss") + ")";
                    //Ready();
                }
                else
                {
                    lblStatus.Text = lblStatusFail;
                    V6Message.Show("Kiểm tra lại kết nối và cấu hình!\n" + exMessage, 0, this);
                }

                if (!V6Login.IsNetwork) CheckUpdateThread();

                ((Timer)sender).Dispose();
            }
            else
            {
                lblStatus.Text = lblStatus.Text.Right(1) + lblStatus.Text.Left(lblStatus.Text.Length - 1);
            }            
        }

        void checkUpdate_Tick(object sender, EventArgs e)
        {
            //((Timer)sender).Stop();
            //return;

            if (flagCheckUpdateFinish)
            {
                ((Timer)sender).Stop();
                if (flagCheckUpdateSuccess && flagUpdateAvailable)
                {
                    if (auto_update)
                    {
                        btnUpdate_Click(null, null);
                    }
                    else
                    {
                        btnUpdate.Visible = true;
                        lblStatus.Text = "Update available! ________________";
                        lblStatus.ForeColor = Color.Green;
                    }
                }
                else
                {
                    lblStatus.Text = lastCheckConnectStatus;
                }

                ((Timer)sender).Dispose();
            }
            else
            {
                lblStatus.Text = lblStatus.Text.Right(1) + lblStatus.Text.Left(lblStatus.Text.Length - 1);
            }
        }


        private void DoCheckConect()
        {
            try
            {
                flagCheckConnectSuccess = false;
                
                _tblsLoaded = false;
                V6Login.StartSqlConnect("V6Soft", Application.StartupPath);
                TblLanguage = V6Login.GetLanguageTable();
                TblModule = V6Login.GetModuleTable();
                if (TblLanguage != null && TblModule != null && TblLanguage.Rows.Count > 0 && TblModule.Rows.Count > 0)
                {
                    _tblsLoaded = true;
                    ReadyLogin = true;
                }
                else
                {
                    _tblsLoaded = false;
                    ReadyLogin = false;
                }
                
                flagCheckConnectSuccess = true;                
            }
            catch (Exception ex)
            {
                exMessage = ex.Message;
                flagCheckConnectSuccess = false;
                flagCheckConnectSuccess = false;
            }
            flagCheckConnectFinish = true;
        }

        private void DoCheckUpdate()
        {
            try
            {
                flagCheckUpdateSuccess = false;

                // Để tránh check update quá nhiều. Đọc thông tin file updated.txt. Nếu mới update gần đây thì bỏ qua (vd 5 ngày).

                ftp_folder = V6Options.GetValueNull("M_DIR_FTPV6_UPDATE");
                if (string.IsNullOrEmpty(ftp_folder)) goto End;

                // Tải update.txt
                var _setting = new H.Setting(Path.Combine(V6Login.StartupPath, "Setting.ini"));
                V6IOInfo info = new V6IOInfo()
                {
                    FileName = "update.txt",
                    FTP_IP = _setting.GetSetting("FTP_IP"),
                    FTP_USER = _setting.GetSetting("FTP_USER"),
                    FTP_EPASS = _setting.GetSetting("FTP_EPASS"),
                    FTP_SUBFOLDER = ftp_folder,
                    LOCAL_FOLDER = V6Setting.V6SoftLocalAppData_Directory,
                };
                bool copy = V6FileIO.CopyFromVPN(info);
                if (copy)
                {
                    string update_txt_filename = Path.Combine(info.LOCAL_FOLDER, info.FileName);
                    // Đọc thông tin file tải.
                    string[] update_lines = File.ReadAllLines(update_txt_filename);
                    // So sánh với file updated.txt tại local (so sánh xem file nào khác nhau thì cần update, nếu chưa có file updated.txt thì tất cả file đều cần update).
                    string updated_txt_filename = Path.Combine(V6Login.StartupPath, "updated.txt");
                    string[] updated_lines = { };
                    if (File.Exists(updated_txt_filename)) updated_lines = File.ReadAllLines(updated_txt_filename);
                    var updated_dic = new SortedDictionary<string, string>();
                    foreach (string line in updated_lines)
                    {
                        if (line.StartsWith(";") || line.Trim() == "") continue;
                        var ss = line.Split(':');
                        if (ss.Length == 2)
                        {
                            updated_dic[ss[0]] = ss[1];
                        }
                    }

                    var update_available_lines = new List<string>();
                    int update_available_count = 0;
                    foreach (string line in update_lines)
                    {
                        if (line.StartsWith(";") || line.Trim() == "") continue;
                        var ss = line.Split(':');
                        if (ss.Length == 2 && (!updated_dic.ContainsKey(ss[0]) || updated_dic[ss[0]] != ss[1]))
                        {
                            update_available_lines.Add(line);
                            update_available_count++;
                        }
                        else if (ss.Length == 1)
                        {
                            var ss2 = line.Split(';');
                            if (ss2.Length > 1) auto_update = "1" == ss2[1];
                            //sub_folder = line;
                            update_available_lines.Add(ss2[0]);
                        }
                    }

                    if (update_available_count > 0)
                    {
                        // write update_available.txt
                        File.WriteAllLines("update_available.txt", update_available_lines.ToArray());
                        flagUpdateAvailable = true;
                        goto End;
                    }
                    else
                    {
                        goto End;
                    }
                    
                }
                else
                {
                    goto End;
                }

                End:
                flagCheckUpdateSuccess = true;
                End_not_success: ;
            }
            catch (Exception ex)
            {
                this.WriteExLog("DoCheckUpdate", ex);
            }
            flagCheckUpdateFinish = true;
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
                V6Login.GetInfo(txtUserName.Text);
                var countDvcs =
                    Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Count(Ma_dvcs) from Aldvcs"));

                V6Login.MadvcsTotal = countDvcs;

                var key = "";
                if (V6Login.UserInfo == null)
                {
                    key = "1=0";
                }
                else if (!V6Login.IsAdmin)
                {
                    key = countDvcs > 0 ? "dbo.VFA_Inlist_MEMO(ma_dvcs, '" + V6Login.UserInfo["r_dvcs"] + "')=1" : "";
                }
                
                DataTable agentData = V6Login.GetAgentTable(key);
                V6Login.MadvcsCount = agentData.Rows.Count;
                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = V6Setting.IsVietnamese ? "Name" : "Name2";
                cboAgent.DataSource = agentData;
                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = V6Setting.IsVietnamese ? "Name" : "Name2";
            }
            catch (Exception ex)
            {
                this.WriteExLog("FormLogin", ex);
            }
        }
        
        private int GetLangIndex(DataTable data, string langID)
        {
            int result = 3;
            try
            {
                for(int i = 0; i<data.Rows.Count;i++)
                {
                    DataRow row = data.Rows[i];
                    if (row["Lan_ID"].ToString().Trim().ToUpper() == langID.ToUpper())
                    {
                        return i;
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetLangIndex", ex);
            }
            return result;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F12))
            {
                if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                {
                    string message = "SQL version: " + V6Options.CurrentVersion;
                    if (ActiveControl == txtPassword && V6Login.UserInfo != null)
                    {
                        message += "\r\n" + V6Login.UserInfo["PASSWORD"];
                        this.WriteToLog(GetType() + ".Control_F12", message);
                    }
                    this.ShowInfoMessage(message, 1000);
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                bool _ctr_is_down = (ModifierKeys & Keys.Control) == Keys.Control;
                bool _alt_is_down = (ModifierKeys & Keys.Alt) == Keys.Alt;

                if (V6Login.IsNetwork || License.CheckLicenseV6Online(License.Seri, License.Key))
                {
                    V6Login.SelectedLanguage = cboLang.SelectedValue.ToString().Trim().ToUpper();
                    V6Login.SelectedLanguageName = cboLang.Text;
                    V6Login.SelectedModule = cboModule.SelectedValue.ToString();
                    var dvcs = (cboAgent.SelectedValue ?? "").ToString().Trim();

                    if (cboAgent.SelectedIndex >=0 && V6Login.Login(txtUserName.Text.Trim(), txtPassword.Text.Trim(), dvcs))
                    {
                        V6Setting.IsLoggedIn = true;
                        V6ControlsHelper.CreateKtmpDirectory();
                        //Khởi tạo giá trị ban đầu
                        V6Setting.M_SV_DATE = V6BusinessHelper.GetServerDateTime();
                        V6Setting.M_ngay_ct1 = V6Setting.M_SV_DATE;
                        V6Setting.M_ngay_ct2 = V6Setting.M_SV_DATE;
                        V6Options.MODULE_ID = V6Login.SelectedModule;
                        V6Setting.GetDataMode = V6Login.GetDataMode;

                        
                        if (!V6Login.CheckAllowVersion(Application.ProductVersion))
                        {
                            var message = V6Login.SelectedLanguage == "V"
                                ? V6Login.ClientName + " Phiên bản chương trình không phù hợp."
                                : V6Login.ClientName + " CHECK PROGRAM VERSION.";
                            this.ShowInfoMessage(message);
                            Logger.WriteToLog(V6Login.ClientName + " " + GetType() + " " + message);
                            DialogResult = DialogResult.No;
                            return;
                        }

                        _allowClient = V6Login.CheckAllowClient(Application.StartupPath);
                        if (!_allowClient && _ctr_is_down && _alt_is_down)
                        {
                            if (new ConfirmPasswordV6().ShowDialog(this) == DialogResult.OK)
                            {
                                V6Login.AllowClient();
                                _allowClient = true;
                            }
                        }
                        if (!_allowClient)
                        {
                            var message = V6Login.SelectedLanguage == "V"
                                ? V6Login.ClientName + " chưa được cấp quyền truy cập máy chủ."
                                : V6Login.ClientName + " ACCESS DENIED.";
                            this.ShowInfoMessage(message);
                            Logger.WriteToLog(V6Login.ClientName + " " + GetType() + " " + message);
                            DialogResult = DialogResult.No;
                            return;
                        }

                        //Check pass_date
                        int M_DAY_CHANGEPASS = int.Parse(V6Options.GetValue("M_DAY_CHANGEPASS"));

                        if (M_DAY_CHANGEPASS > 0)
                        {
                            DateTime? pass_date = (DateTime?) V6Login.UserInfo["PASS_DATE"];
                            //Tuanmh 24/09/2017 pass_exp=1=Y 
                            var pass_exp = V6Login.UserInfo["PASS_EXP"].ToString().Trim();

                            if (pass_exp == "1")
                            {
                                int passed = 0;
                                if (pass_date != null) passed = (V6Setting.M_SV_DATE - pass_date).Value.Days;
                                if (pass_date == null || passed >= M_DAY_CHANGEPASS)
                                {
                                    if (new ChangePassword().ShowDialog(this) != DialogResult.OK)
                                    {
                                        return;
                                    }
                                }
                            }
                        }

                        if (checkUpdateThread != null && checkUpdateThread.IsAlive) checkUpdateThread.Abort();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (++_count == 3)
                        {
                            DialogResult = DialogResult.No;
                        }

                        errorProvider1.SetError(label0, "Nhập sai " + _count + " lần!\n" + V6Login.Message);
                        txtUserName.Focus();
                        txtUserName.SelectAll();
                    }
                }
                else // Hiển thị form nhập code_name = mahoa (tenmay + 1 + checkcode) 
                {
                    FormKeyV6Online f = new FormKeyV6Online(License.Seri);
                    f.ShowDialog(this);
                    //this.ShowInfoMessage(V6Text.NotAllowed
                    //    + "\r\nSeri: " + License.Seri
                    //    + "\r\nLiên hệ V6: 0936 976 976");
                    //Clipboard.SetText(License.Seri);
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (SqlException ex)
            {
                this.ShowErrorMessage(GetType() + (V6Setting.IsVietnamese ? ".Có lỗi kết nối!\n" : ". There is a connection error!") + ex.Message, "V6Soft");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message, "V6Soft");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void radAPIDataMode_Click(object sender, EventArgs e)
        {
            ResetInfos(radAPIDataMode.Checked ? GetDataMode.API : GetDataMode.Local);
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConfig.SelectedIndex = cboDatabase.SelectedIndex;
            ResetInfos(radAPIDataMode.Checked ? GetDataMode.API : GetDataMode.Local);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //var f = new ConfirmPasswordV6();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
                
            //}
        }

        private void cboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReadyReportLanguage)
            {
                V6Login.SelectedLanguage = cboLang.SelectedValue.ToString().Trim().ToUpper();
                if (cboAgent.DataSource != null)
                {
                    cboAgent.DisplayMember = V6Setting.IsVietnamese ? "Name" : "Name2";
                }

                if (V6Login.SelectedLanguage == "V")
                {
                    rTiengViet.Checked = true;
                }
                else if (V6Login.SelectedLanguage == "E")
                {
                    rEnglish.Checked = true;
                }
            }
        }

        bool _radioRunning = false;
        private void rbtLanguage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsReady) return;
                _radioRunning = true;
                if (rTiengViet.Checked)
                {
                    V6Setting.ReportLanguage = "V";
                }
                else if (rEnglish.Checked)
                {
                    V6Setting.ReportLanguage = "E";
                }
                else if (rBothLang.Checked)
                {
                    V6Setting.ReportLanguage = "B";
                }
                else if (rCurrent.Checked)
                {
                    V6Setting.ReportLanguage = V6Setting.Language;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".rbtLanguage_CheckedChanged", ex);
            }
            _radioRunning = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists("V6AccountingB_Update.exe"))
                {
                    lblStatus.Text = "Check V6AccountingB_Update.exe " + lastCheckConnectStatus;
                    return;
                }
                // Call updater
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = "",
                        FileName = "V6AccountingB_Update.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                process.StartInfo.Arguments ="\"" + ftp_folder + "\" \"update_available.txt\"";
                process.Start();
                Application.Exit();
            }
            catch (Exception ex)
            {
                this.ShowErrorException("Update_Click", ex);
            }
        }
        
    }    
}
