using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;
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
        private bool _tblsLoaded, _allowClient;

        private void MyInit()
        {
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


        private void ReadyLoginForm()
        {
            
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Text = V6Setting.LASTUSERW;
            //Khởi tạo với rad đang check. Mặc định local.
            //ResetInfos(radAPIDataMode.Checked ? GetDataMode.API : GetDataMode.Local);
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

        private bool flagCheckConnectFinish;
        private bool flagCheckConnectSuccess;
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
                cboLang.SelectedIndex = GetLangIndex(TblLanguage, V6Setting.Language);

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
                    lblStatus.Text = "______________________________________________";
                    panel1.Enabled = true;
                    txtUserName.Focus();
                }
                else
                {
                    lblStatus.Text = "___________ / _";
                    V6Message.Show("Kiểm tra lại kết nối và cấu hình!\n" + exMessage);
                    
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

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
                V6Login.GetInfo(txtUserName.Text);
                var countDvcs =
                    Convert.ToInt32(SqlConnect.ExecuteScalar(CommandType.Text, "Select Count(Ma_dvcs) from Aldvcs"));

                V6Login.MadvcsTotal = countDvcs;

                var key = V6Login.IsAdmin
                    ? ""
                    : countDvcs > 0 ? "dbo.VFA_Inlist_MEMO(ma_dvcs, '" + V6Login.UserInfo["r_dvcs"] + "')=1" : "";
                
                DataTable agentData = V6Login.GetAgentTable(key);

                V6Login.MadvcsCount = agentData.Rows.Count;

                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = "Name";
                cboAgent.DataSource = agentData;
                cboAgent.ValueMember = "Ma_dvcs";
                cboAgent.DisplayMember = "Name";
            }
            catch// (Exception ex)
            {
                //V6ControlFormHelper
            }
        }
        
        private int GetLangIndex(DataTable data, string langID)
        {
            int result = 0;
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
            catch
            {
                result = 0;
            }
            return result;
        }

        private static bool CheckLicenseKeyAllow(string seri, string key)
        {
            try
            {
                SqlParameter[] prList =
                {
                    new SqlParameter("@name", V6Login.ClientName), 
                    new SqlParameter("@seri", seri), 
                    new SqlParameter("@key", key), 
                };
                var data = SqlConnect.Select("V6ONLINES", "*", "name=@name and seri=@seri and [key]=@key", "", "", prList).Data;
                if (data != null && data.Rows.Count == 1)
                {
                    var row = data.Rows[0].ToDataDictionary();

                    var seri0 = License.ConvertHexToString(seri);
                    var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
                    var key0 = License.ConvertHexToString(key);
                    var check_seri = mahoa_seri0 == key0;

                    var allow = 1 == ObjectAndString.ObjectToInt(row["ALLOW"]);
                    var eCodeName = (row["CODE_NAME"] ?? "").ToString().Trim();
                    var rCodeName = eCodeName == "" ? "" : UtilityHelper.DeCrypt(eCodeName);
                    //var allow = row["Allow"].ToString().Trim();
                    var checkCode = row["CHECKCODE"].ToString().Trim();
                    var is_allow =
                            allow
                            && rCodeName.Length > V6Login.ClientName.Length + 1
                            && rCodeName.StartsWith(V6Login.ClientName)
                            && rCodeName.Substring(V6Login.ClientName.Length, 1) == "1"
                            && rCodeName.EndsWith(checkCode);

                    return allow && check_seri && is_allow;
                }
                else
                {
                    Program.InsertLicenseToDatabase(seri, key);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.Message);
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (V6Login.IsNetwork || CheckLicenseKeyAllow(License.Seri, License.Key))
                {
                    V6Login.SelectedLanguage = cboLang.SelectedValue.ToString().Trim().ToUpper();
                    V6Login.SelectedModule = cboModule.SelectedValue.ToString();
                    var dvcs = (cboAgent.SelectedValue ?? "").ToString().Trim();

                    if (V6Login.Login(txtUserName.Text.Trim(), txtPassword.Text.Trim(), dvcs))
                    {
                        _allowClient = V6Login.CheckAllowClient(Application.StartupPath);
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
                        int M_DAY_CHANGEPASS = int.Parse(V6Options.V6OptionValues["M_DAY_CHANGEPASS"]);

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

                        //Khởi tạo giá trị ban đầu
                        V6Setting.M_SV_DATE = V6BusinessHelper.GetServerDateTime();
                        V6Setting.M_ngay_ct1 = V6Setting.M_SV_DATE;
                        V6Setting.M_ngay_ct2 = V6Setting.M_SV_DATE;

                        V6Options.MODULE_ID = V6Login.SelectedModule;
                        V6Setting.GetDataMode = V6Login.GetDataMode;

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (++_count == 3)
                        {
                            DialogResult = DialogResult.No;
                        }
                        errorProvider1.SetError(label0, "Nhập sai " + _count + " lần!\n" + V6Login.Message);
                    }
                }
                else
                {
                    this.ShowInfoMessage(V6Text.NotAllowed
                        + "\r\nSeri: " + License.Seri
                        + "\r\nLiên hệ V6: 0936 976 976");
                    Clipboard.SetText(License.Seri);
                    DialogResult = DialogResult.Cancel;
                }
            }
            catch (SqlException ex)
            {
                this.ShowErrorMessage(GetType() + ".Có lỗi kết nối!\n" + ex.Message, "V6Soft");
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
        
    }    
}
