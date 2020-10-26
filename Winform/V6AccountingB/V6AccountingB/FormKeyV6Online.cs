using System;
using System.Threading;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6AccountingB
{
    public partial class FormKeyV6Online : V6Form
    {
        private string _lblStatusOriginalText;
        public FormKeyV6Online()
        {
            InitializeComponent();
            txtSeriClient.Text = License.GetSeriClient();
            MyInit();
        }

        public FormKeyV6Online(string seri)
        {
            InitializeComponent();
            txtSeriClient.Text = License.GetSeriClient();
            MyInit();
        }

        private void MyInit()
        {
            _lblStatusOriginalText = lblStatus.Text;
            //DatabaseConfig.LoadDatabaseConfig("V6Soft", Application.StartupPath);
            //cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
            //cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
            //cboDatabase.DataSource = DatabaseConfig.ConnectionConfigData;
            //cboDatabase.DisplayMember = DatabaseConfig.ConfigDataDisplayMember;
            //cboDatabase.ValueMember = DatabaseConfig.ConfigDataValueMember;
            //cboDatabase.SelectedIndex = DatabaseConfig.GetConfigDataRunIndex();
            //cboDatabase.SelectedValue = V6SqlConnect.DatabaseConfig.Database;
            lblStatus.Text = "______________________________________________";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                var sericlient = txtSeriClient.Text;
                var code_name = txtCodeName.Text.Trim();
                if (License.CheckNewLicenseV6Online(License.Seri, License.Key, code_name))
                {
                    //License.UpdateLicenseV6OnlineCodeName(License.Seri, License.Key, code_name);
                    this.ShowInfoMessage(V6Setting.IsVietnamese ? "Đăng ký thành công!" : "Registration successful!");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowWarningMessage("Sai khóa!");
                    DialogResult = DialogResult.No;
                }
            }
            catch (Exception ex)
            {
                this.ShowInfoMessage(V6Text.Text("COLOI1"));
                this.WriteExLog(GetType() + "btnOK_Click", ex);
            }
        }

        

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DatabaseConfig.SelectedIndex = cboDatabase.SelectedIndex;
            //ResetInfos(GetDataMode.Local);
        }

        private void ResetInfos(GetDataMode mode)
        {
            try
            {
                V6Login.GetDataMode = mode;
                lblStatus.Text = _lblStatusOriginalText;
                //panel1.Enabled = false;
                btnOK.Enabled = false;
                CheckConnectThread();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ResetInfos: " + ex.Message, "Login");
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

        private bool flagCheckConnectFinish;
        private bool flagCheckConnectSuccess;
        private string exMessage = "";
        private bool _tblsLoaded;

        void checkConnect_Tick(object sender, EventArgs e)
        {
            if (_tblsLoaded)
            {
                //cboLang.ValueMember = "Lan_Id";
                //cboLang.DisplayMember = "Lan_name2";
                //cboLang.DataSource = TblLanguage;
                //cboLang.ValueMember = "Lan_Id";
                //cboLang.DisplayMember = "Lan_name2";
                //cboLang.SelectedIndex = GetLangIndex(TblLanguage, V6Setting.Language);

                //cboModule.ValueMember = "module_id";
                //cboModule.DisplayMember = "name";
                //cboModule.DataSource = TblModule;
                //cboModule.ValueMember = "module_id";
                //cboModule.DisplayMember = "name";
            }
            if (flagCheckConnectFinish)
            {
                ((Timer)sender).Stop();

                if (flagCheckConnectSuccess && _tblsLoaded)
                {
                    lblStatus.Text = "______________________________________________";
                    //panel1.Enabled = true;
                    //txtUserName.Focus();
                    btnOK.Enabled = true;
                    txtCodeName.Focus();
                    
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
                var startSqlOk = V6Login.StartSqlConnect("V6Soft", Application.StartupPath);
                //TblLanguage = V6Login.GetLanguageTable();
                //TblModule = V6Login.GetModuleTable();
                if (startSqlOk)
                {
                    _tblsLoaded = true;
                    //ReadyLogin = true;
                }
                else
                {
                    _tblsLoaded = false;
                    //ReadyLogin = false;
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

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

    }
}
