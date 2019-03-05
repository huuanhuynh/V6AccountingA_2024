using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager;
using V6ControlManager.FormManager.HeThong.QuanLyHeThong;
using V6ControlManager.FormManager.MenuManager;
using V6ControlManager.FormManager.ReportManager;
using V6ControlManager.FormManager.ReportManager.Filter;
using V6ControlManager.FormManager.ReportManager.XuLy.NhanSu;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6AccountingB
{
    public partial class MainForm : V6Form
    {
        SortedDictionary<string, Control> controlsDictionary = new SortedDictionary<string, Control>();
        private Control currentControl;
        public MainForm()
        {
            InitializeComponent();
            MyInit();
            //Test change code on .net35
        }

        private void MyInit()
        {
            V6ControlFormHelper.StatusTextViewControl = lblStatus;
            V6ControlFormHelper.StatusTextViewControl2 = lblStatus2;
            lblMainMessage.Top = -lblMainMessage.Height;
            
            V6ControlFormHelper.MainForm = this;
            V6ControlFormHelper.MessageLable = lblMainMessage;
            V6ControlFormHelper.lblMenuMain = lblMenuShow;
            V6ControlFormHelper.MainMenu = menuMain;
            
            lblMenuShow.MouseEnter += V6ControlFormHelper.HaveStatusControl_MouseEnter;
            lblMenuShow.MouseHover += V6ControlFormHelper.HaveStatusControl_MouseHover;
            lblMenuShow.MouseMove += V6ControlFormHelper.HaveStatusControl_MouseMove;
            lblMenuShow.MouseLeave += V6ControlFormHelper.HaveStatusControl_MouseLeave;

            menuMain.Buttons.Clear();
            MakeMenu1();
            FixQuickMenu();
            
            lblCompanyName.Text = V6Soft.V6SoftValue["M_TEN_CTY"].ToUpper();
            controlsDictionary.Add("lblCompanyName", lblCompanyName);
            this.Text += " " + DatabaseConfig.Note;

            LoadMainFormInit();
        }
        
        private void MakeMenu1()
        {
            
            DataTable data = V6Menu.GetMenuTable1(V6Login.UserId, V6Options.MODULE_ID);
            foreach (DataRow row in data.Rows)
            {
                var mb = new MenuButton
                {
                    ItemID = row["v2id"].ToString().Trim().ToUpper(),
                    Text = V6Setting.IsVietnamese ?
                        row["vbar"].ToString().Trim() :
                        row["vbar2"].ToString().Trim()
                };

                var file0 = Path.Combine("Pictures\\", row["PICTURE"].ToString().Trim());
                var file = file0 + ".png";

                if (File.Exists(file))
                {
                    mb.Image = V6ControlFormHelper.LoadCopyImage(file);
                }
                else
                {
                    file = file0 + ".jpg";
                    if (!File.Exists(file))
                    {
                        file = file0 + ".gif";
                        if (!File.Exists(file)) file = file0 + ".bmp";
                    }
                    if (File.Exists(file)) mb.Image = V6ControlFormHelper.LoadCopyImage(file);
                }

                menuMain.Buttons.Add(mb);
                mb.Tag = "D"+ menuMain.Buttons.Count;
                menuMain.Height = menuMain.ButtonHeight*(menuMain.Buttons.Count+1);
            }

            V6ControlFormHelper.SetStatusText("Loading...");

            //Bỏ nhóm dòng này để chạy LoadMenuThread

            //_complete = 100;
            //progressBar1.Visible = false;
            //V6ControlFormHelper.SetStatusText("Ready.");
            //timer2.Start();
            //return;

            CheckForIllegalCrossThreadCalls = false;
            var t = new Thread(LoadMenuThread) { IsBackground = true };
            t.Start();
            timer1.Start();

        }

        private void FixQuickMenu()
        {
            quickMenu1.Top = menuMain.Bottom + 5;
            quickMenu1.Height = panelLogin.Top - quickMenu1.Top;
        }


        public static bool LoadMainFormInit()
        {
            if (V6Login.IsNetwork) return V6Login.IsNetwork;
            var seri0 = License.ConvertHexToString(License.Seri);
            var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
            var key0 = License.ConvertHexToString(License.Key);
            var result = mahoa_seri0 == key0;
            if (mahoa_seri0 != key0)
            {
                throw new Exception(V6Text.NotSupported);
            }
            return result;
        }
        
        private int _complete;
        private void LoadMenuThread()
        {
            try
            {
                progressBar1.Value = _complete;
                V6Text.LoadText("");
                V6ControlsHelper.DeleteAllFileInV6SoftLocalAppData();
                V6ControlsHelper.DeleteAllRptTempFiles();
                _complete = 5;

                var total = menuMain.Buttons.Count;
                if (total == 0) total = 1;
                var step = 95 / total;
                for (int i = 0; i < total; i++)
                {
                    var button = menuMain.Buttons[i];
                    var v2ID = button.ItemID;
                    var c = new Menu2Control(button, button.Text, V6Options.MODULE_ID, v2ID)
                    {
                        Dock = DockStyle.Fill,
                        Location = new Point(0, 0)
                    };
                    //if(i>0)c.Visible = false;
                    controlsDictionary.Add(v2ID, c);
                    //panelView.Controls.Add(c);
                    _complete += step;
                }

                //Load quick menu
                quickMenu1.LoadMenuData();
                
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadMenuThread", ex);
                Logger.WriteToLog(V6Login.ClientName + " " + GetType() + ".LoadMenuThread " + ex.Message, Application.ProductName);
            }
            _complete = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = _complete;
            if (_complete == 100)
            {
                timer1.Stop();
                V6ControlFormHelper.CreateV6TopMessageForm();
                V6ControlsHelper.CreateFlyLabelForm();
                Focus();
                timer2.Start();
                
                progressBar1.Visible = false;
                progressBar1.Dispose();
                
                _ready = true;
                V6ControlFormHelper.SetStatusText(V6Text.Ready);
                if (menuMain.SelectedButton != null)
                {
                    menuMain_Click(menuMain, new MenuControl.ButtonClickEventArgs(menuMain.SelectedButton, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                }
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            lblUserName.Text = V6Login.UserName;
            lblComment.Text = V6Login.Comment;
            ShowDVCS();
            WindowState = FormWindowState.Maximized;

            GoiBaoCaoNhanh();
        }

        private void ShowDVCS()
        {
            lblDVCS.Text = "Agent: " + V6Login.Madvcs;
        }

        
        private void menuMain_Click(object sender, MenuControl.ButtonClickEventArgs e)
        {
            if (_complete < 100) return;
            string v2ID = e.SelectedButton.ItemID;
            V6ControlFormHelper.SetHideMenuLabel(lblMenuShow, e.SelectedButton.Text);

            //Hiện
            foreach (var item in controlsDictionary)
            {
                if (item.Key == v2ID)
                {
                    currentControl = item.Value;
                    item.Value.Visible = true;
                    item.Value.BringToFront();
                    
                    if (!panelView.Contains(item.Value))
                    {
                        panelView.Controls.Add(item.Value);
                    }

                    if (currentControl is Menu2Control)
                    {
                       FormManagerHelper.CurrentMenu3Control
                            = ((Menu2Control) currentControl).CurrentMenu3Control;
                    }
                    break;
                }
            }
            //Ẩn
            foreach (var item in controlsDictionary)
            {
                if (item.Key != v2ID)
                {   
                    item.Value.Visible = false;
                }
            }
            
            //return;//Phần này tạo control nếu chưa có.//Debug
            //Nếu đã load full ở init thì không chạy tới đây.
            if (!controlsDictionary.ContainsKey(v2ID))
            {
                UserControl c;

                c = new Menu2Control(e.SelectedButton, e.SelectedButton.Text, V6Options.MODULE_ID, v2ID);
                currentControl = c;
                c.Dock = DockStyle.Fill;
                panelView.Controls.Add(c);
                controlsDictionary.Add(v2ID, c);

                {
                    FormManagerHelper.CurrentMenu3Control
                         = ((Menu2Control)c).CurrentMenu3Control;
                }
            }

            if (BackgroundImage != null)
                BackgroundImage = null;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //??Gửi tín hiệu bàn phím. Nếu bỏ dòng này không gõ text được??
            var a = base.ProcessCmdKey(ref msg, keyData);
            try
            {
                DoHotKey(keyData);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(GetType() + ".ProcessCmdKey Main: " + ex.Message, ex.StackTrace);
            }
            return a;// base.ProcessCmdKey(ref msg, keyData);
        }

        //Remove mainform_keydown
        /// <summary>
        /// Bật tính năng menu
        /// </summary>
        private bool _control_m;
        /// <summary>
        /// Định nghĩa phím nóng cho chính mình. Nếu không có sẽ gọi vào bên trong currentControl.
        /// </summary>
        /// <param name="keyData"></param>
        public override void DoHotKey (Keys keyData)
        {
            try
            {
                do_hot_key = true;
                if (_locked) return;

                if (_control_m)
                {
                    _control_m = false;
                    string keyChar = keyData.ToString().ToUpper();

                    int index = 0;
                    var button = menuMain.SelectedButton;
                    //var button_old = button;
                    if (button != null) index = menuMain.Buttons.IndexOf(button);
                    int old_index = index;
                    
                    //Tìm thử bên dưới nút menu đang chọn
                    for (int i = index + 1; i < menuMain.Buttons.Count; i++)
                    {
                        button = menuMain.Buttons[i];
                        if (button.Text.ToUpper().StartsWith(keyChar))
                        {
                            menuMain.SelectedButton = button;
                            menuMain_Click(menuMain,
                                new MenuControl.ButtonClickEventArgs(button,
                                    new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                            return;
                        }
                    }

                    //Nếu không thấy thì chạy lại đoạn trên
                    for (int i = 0; i < old_index; i++)
                    {
                        button = menuMain.Buttons[i];
                        if (button.Text.ToUpper().StartsWith(keyChar))
                        {
                            menuMain.SelectedButton = button;
                            menuMain_Click(menuMain,
                                new MenuControl.ButtonClickEventArgs(button,
                                    new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                            return;
                        }
                    }
                    return;
                }

                if (keyData == (Keys.Control | Keys.Up))
                {
                    V6ControlFormHelper.ShowMainMenu();

                    var button = menuMain.SelectedButton;
                    var index = menuMain.Buttons.IndexOf(button);
                    index--;
                    if (index < 0) index = menuMain.Buttons.Count - 1;
                    button = menuMain.Buttons[index];

                    menuMain.SelectedButton = button;
                    menuMain_Click(menuMain, new MenuControl.ButtonClickEventArgs(button, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));

                }
                else if (keyData == (Keys.Control | Keys.Down))
                {
                    V6ControlFormHelper.ShowMainMenu();

                    var button = menuMain.SelectedButton;
                    var index = menuMain.Buttons.IndexOf(button);
                    index++;
                    if (index >= menuMain.Buttons.Count) index = 0;
                    button = menuMain.Buttons[index];

                    menuMain.SelectedButton = button;
                    menuMain_Click(menuMain, new MenuControl.ButtonClickEventArgs(button, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                }
                else if (keyData == (Keys.Control | Keys.M))
                {
                    V6ControlFormHelper.ShowMainMenu();
                    _control_m = true;
                }
                else// if ((keyData & Keys.Control) != 0 || (keyData & Keys.Alt) != 0)
                {
                    if (currentControl != null && currentControl is V6Control)
                    {
                        ((V6Control)currentControl).DoHotKey(keyData);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void lblMenuShow_Click(object sender, EventArgs e)
        {
            var lable = (V6VeticalLabel)sender;

            V6ControlFormHelper.ShowHideMenu(lable,
                menuMain.SelectedButton == null ? V6Text.ShowMenu : menuMain.SelectedButton.Text,
                panelMenu, panelMenuShow, panelView, this, new Point(0, 12), !lable.IsShowing, 1);
        }
        
        private void menuMain_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "";
            if (V6ControlFormHelper.RunningList.Count>0)
            {
                message = V6Setting.IsVietnamese ? "Có chứng từ đang mở. Vẫn muốn thoát?" : "There are open documents. Still want to exit?";
                message +="\r\n" + V6ControlFormHelper.RunningListString;
            }

            if (message == "")
            {
                if (this.ShowConfirmMessage(V6Text.ExitQuestion) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (V6Message.Show(
                    (V6Setting.IsVietnamese ? "Thoát chương trình?\n" : "Exit?\n") + message,
                    V6Setting.IsVietnamese? "Cảnh báo!" : "Warning!", 0,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Stop
                    )
                    != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                else // Hỏi thêm lần nữa.
                {
                    if (V6Message.Show(
                    (V6Setting.IsVietnamese ? "Xác nhận lần 2.\n" : "Asking again.\n") + message,
                    V6Setting.IsVietnamese ? "Cảnh báo!" : "Warning!", 0,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                    )
                    != DialogResult.Yes)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void statusStrip1_Resize(object sender, EventArgs e)
        {
            lblStatus.Width = statusStrip1.Width/2;
            lblStatus2.Width = statusStrip1.Width - lblStatus.Width - 20;
        }

        private int timeCount2 = -1;
        private void timer2_Tick(object sender, EventArgs e)
        {
            timeCount2++;
            //if (!_locked && Win32.GetIdleTime() > 600 * 5)//Test
            if (V6Options.M_LOCK_TIME > 0 && !_locked && Win32.GetIdleTime() > 60000 * V6Options.M_LOCK_TIME)
            {
                LockProgram();
            }
            else if (!_locked)
            {
                if (timeCount2%(60*3) == 0)
                {
                    timeCount2 = 0;         //Reset timeCount
                    LoadV6ViewMessage();    //Tải message
                }
                else if(timeCount2%5==0)
                    ViewV6Message();
            }
        }

        private void ViewV6Message()
        {
            try
            {
                if (have_news) //reset
                {
                    timeCount2 = 0;
                    have_news = false;
                    messageIndex = -1;
                }

                messageIndex++;
                GetMessage();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void GetMessage()
        {
            try
            {
                if (v6Message != null && v6Message.Rows.Count > 0)
                {
                    if (messageIndex >= v6Message.Rows.Count) messageIndex = 0;
                    var row = v6Message.Rows[messageIndex];
                    Color color = Color.Blue;
                    string colorRGB = row["Color"].ToString();
                    var sss = ObjectAndString.SplitString(colorRGB);
                    if (sss.Length >= 3)
                    {
                        color = ObjectAndString.StringToColor(colorRGB);
                    }
                    lblV6Message.ForeColor = color;
                    lblV6Message.Text = (row["Mess1"] ?? "").ToString().Trim()
                        + " <--------##-------> " + (row["Mess2"] ?? "").ToString().Trim();

                   
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GetMessage", ex);
            }
        }

        private DataTable v6Message;
        /// <summary>
        /// Có thông báo mới.
        /// </summary>
        private bool have_news;
        private int messageIndex = -1;
        //private string newMessage = "Chưa tải thông báo! sửa lại thành rỗng!";

        private void LoadV6ViewMessage()
        {
            try
            {
                var thread = new Thread(LoadMessageThread) {IsBackground = true};
                thread.Start();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, ex.Message), Application.ProductName);
            }
        }

        private void LoadMessageThread()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("User_id", V6Login.UserId),
                    new SqlParameter("Type", ""),
                    new SqlParameter("Advance1", ""),
                    new SqlParameter("Advance2", ""),
                    new SqlParameter("Advance3", "")
                };
                v6Message = V6BusinessHelper.ExecuteProcedure("VPA_V6VIEW_MESSAGE", plist).Tables[0];
                have_news = true;
                messageIndex = -1;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadMessageThread", ex);
            }
        }

        private LockForm _locker;
        private bool _locked;

        private void LockProgram()
        {
            try
            {
                _locked = true;

                if (_locker == null)
                {
                    _locker = new LockForm(V6Login.UserName)
                    {
                        WindowState = FormWindowState.Maximized
                    };
                    _locker.VisibleChanged += delegate
                    {
                        if (_locker.Visible)
                        {
                            _locker.txtPassword.Focus();
                        }
                        else
                        {
                            _locked = false;
                        }
                    };
                }
                V6ControlFormHelper.SetStatusText("Locked!");
                
                foreach (Form form in Application.OpenForms)
                {
                    form.Hide();
                }
                
                _locker.ShowDialog(this);
                
                foreach (Form form in Application.OpenForms)
                {
                    form.Show();
                }

                V6ControlFormHelper.SetStatusText("");
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void GoiBaoCaoNhanh()
        {
            try
            {
                QuickReportManager.ShowQuickReport(this, "CallFromMain");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GoiBaoCaoNhanh:" + ex.Message, ex.Source);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Environment.Exit(0);
            Close();
            //Application.Exit();
        }

        private CalculatorForm cal = null;
        private void ShowCalculator()
        {
            try
            {
                if (cal == null || cal.IsDisposed)
                {
                    cal = new CalculatorForm();
                }

                if (cal.Visible)
                {
                    cal.Hide();
                }
                else
                {
                    cal.Show(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ShowCalculator:" + ex.Message, ex.Source);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowCalculator();
        }

        private void ChangeDVCS()
        {
            try
            {
                new ChangeDVCSControl().ShowToForm(this, "Change Agent", false, true, false);
                ShowDVCS();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ChangeDVCS:" + ex.Message, ex.Source);
            }
        }

        private void changeDVCSToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ChangeDVCS();
        }

        private void stickNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StickNoteForm f = new StickNoteForm(V6Mode.Add);
                f.Show(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".stickNote", ex);
            }
        }

        private void lblDVCS_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeDVCS();
        }

        private void lblHotLine_Click(object sender, EventArgs e)
        {
            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.v6soft.com.vn");
        }
        

        
    }
}
