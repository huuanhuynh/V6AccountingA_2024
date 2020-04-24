using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia;
using V6ControlManager.FormManager.DanhMucManager;
using V6ControlManager.FormManager.SoDuManager;
using V6ControlManager.FormManager.VitriManager;
using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;
using V6Controls.Forms.Viewer;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.MenuManager
{
    public partial class Menu3Control : V6Control
    {
        private MenuButton _menu1Button;
        private TabPage _menu2TabPage;
        private string _moduleID;
        private string _v2ID;
        private string _jobID;

        public bool IsShow { get { return lblShowHide.IsShowing; } }

        public Menu3Control()
        {
            InitializeComponent();
            MyInit();
        }

        public Menu3Control(MenuButton menu1Button, TabPage tabPage, string moduleID, string v2ID, string jobID)
        {   
            InitializeComponent();
            _menu1Button = menu1Button;
            _menu2TabPage = tabPage;
            _moduleID = moduleID;
            _v2ID = v2ID;
            _jobID = jobID;
            MyInit();
        }

        
        private void MyInit()
        {
            menuControl1.ViewStatusMode = 3;
            DataTable v6MenuData = V6Menu.GetMenu3TableFilter(V6Login.UserId, _moduleID, _v2ID, _jobID);
            foreach (DataRow row in v6MenuData.Rows)
            {
                AddMenu(row);
            }
            lblShowHide.MouseEnter += V6ControlFormHelper.HaveStatusControl_MouseEnter;
            lblShowHide.MouseHover += V6ControlFormHelper.HaveStatusControl_MouseHover;
            lblShowHide.MouseMove += V6ControlFormHelper.HaveStatusControl_MouseMove;
            lblShowHide.MouseLeave += V6ControlFormHelper.HaveStatusControl_MouseLeave;
        }

        private void AddMenu2Status(int num)
        {
            var oldValue = ObjectAndString.ObjectToInt(_menu2TabPage.AccessibleDefaultActionDescription);
            var newValue = oldValue + num;
            _menu2TabPage.AccessibleDefaultActionDescription = "" + newValue;
            var tabControl = _menu2TabPage.Parent as V6TabControl;
            if (tabControl != null)
            {
                tabControl.Invalidate(tabControl.GetTabRect(tabControl.SelectedIndex));
            }
            //_menu2TabPage.Invalidate(_menu2TabPage.GetTabRect());
            //_menu2TabPage.Parent.Invalidate();
        }

        private void AddMenu(DataRow v6MenuDataRow)
        {
            var mb = new MenuButton
            {
                ItemID = v6MenuDataRow["itemid"].ToString().Trim().ToUpper(),
                Text = (v6MenuDataRow["hotkey"]??"")
                + (V6Setting.IsVietnamese
                    ? v6MenuDataRow["vbar"].ToString().Trim()
                    : v6MenuDataRow["vbar2"].ToString().Trim()),
                CodeForm = v6MenuDataRow["codeform"].ToString().Trim(),
                Pro_old = v6MenuDataRow["pro_old"] == null ? null : v6MenuDataRow["pro_old"].ToString().Trim(),
                Exe = v6MenuDataRow["program"].ToString().Trim(),
                MaChungTu = v6MenuDataRow["ma_ct"].ToString().Trim(),
                NhatKy = v6MenuDataRow["nhat_ky"].ToString().Trim(),

                ReportFile = v6MenuDataRow["rep_file"].ToString().Trim(),
                ReportTitle = v6MenuDataRow["title"].ToString().Trim(),
                ReportTitle2 = v6MenuDataRow["title2"].ToString().Trim(),
                ReportFileF5 = v6MenuDataRow["rep_fileF5"].ToString().Trim(),
                ReportTitleF5 = v6MenuDataRow["titleF5"].ToString().Trim(),
                ReportTitle2F5 = v6MenuDataRow["title2F5"].ToString().Trim(),

                Key1 = v6MenuDataRow["Key1"].ToString().Trim(),
                Key2 = v6MenuDataRow["Key2"].ToString().Trim(),
                Key3 = v6MenuDataRow["Key3"].ToString().Trim(),
                Key4 = v6MenuDataRow["Key4"].ToString().Trim(),

                Xtra = v6MenuDataRow["page"].ToString().Trim(),
            };

            var file0 = Path.Combine("Pictures\\", v6MenuDataRow["PICTURE"].ToString().Trim());
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
            
            menuControl1.Buttons.Add(mb);
            menuControl1.Height = menuControl1.ButtonHeight * (menuControl1.Buttons.Count+1);
        }
        
        private void Form_Load(object sender, EventArgs e)
        {

        }

        public SortedDictionary<string, V6Control> ControlsDictionary = new SortedDictionary<string, V6Control>();

        private MenuButton _currentMenuButton = null;
        private void menuControl_Click(object sender, MenuControl.ButtonClickEventArgs e)
        {
            _have_alt_change_menu = false;
            _currentMenuButton = e.SelectedButton;
            string item_id = e.SelectedButton.ItemID;
            V6ControlFormHelper.CurrentItemID = item_id;
            string codeform = e.SelectedButton.CodeForm;
            try
            {
                V6ControlFormHelper.AddLastAction(string.Format("ItemID({0}) CodeForm({1}) Text({2})",
                    e.SelectedButton.ItemID, e.SelectedButton.CodeForm, e.SelectedButton.Text));
                FormManagerHelper.CurrentMenu3Control = this;
                V6ControlFormHelper.SetHideMenuLabel(lblShowHide, e.SelectedButton.Text);
                
                if (V6Options.GetValue("M_USER_LOG").Trim() == "1")
                {
                    SqlParameter[] plist =
                    {
                        new SqlParameter("@user_name", V6Login.UserName),
                        new SqlParameter("@user_id", V6Login.UserId),
                        new SqlParameter("@itemid", item_id),
                        new SqlParameter("@action", e.SelectedButton.Text),
                        new SqlParameter("@status", "1"),
                        new SqlParameter("@type", string.IsNullOrEmpty(codeform) ? "N" : codeform[0].ToString())
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_00_POST_USERLOG", plist);
                }

                foreach (var control in ControlsDictionary)
                {
                    if (control.Key != item_id) control.Value.Visible = false;
                    //else control.Value.Visible = true;
                }

                if (ControlsDictionary.ContainsKey(item_id) && !ControlsDictionary[item_id].IsDisposed)
                {
                    bool check = true, mouse_left = false, ctrl_is_down = false, shift_is_down;
                    if (codeform != null)
                    {
                        var code = string.IsNullOrEmpty(codeform) ? "" : codeform.Substring(0, 1);
                        var FORM_NAME = string.IsNullOrEmpty(codeform) ? "" : codeform.Substring(1).ToUpper();
                        var TABLE_NAME = string.IsNullOrEmpty(codeform) ? "" : codeform.Substring(1).ToUpper();
                        
                        if (e == null || e.Button == MouseButtons.Left)
                        {
                            mouse_left = true;
                        }
                        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        {
                            ctrl_is_down = true;
                        }
                        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                        {
                            shift_is_down = true;
                        }

                        //var TABLE_NAME = (codeform.Length > 1 ? codeform.Substring(1) : "").ToUpper();
                        if (code == "8" || MenuManager.CheckAdminTables.ContainsKey(TABLE_NAME))
                        {
                            if (mouse_left && ctrl_is_down)
                            {
                                check = MenuManager.CheckPasswordV6(this);
                            }
                            else
                            {
                                check = MenuManager.CheckPassword(this);
                            }
                            //check = new ConfirmPassword().ShowDialog(this) == DialogResult.OK;
                        }
                    }

                    if (check)
                    {
                        if (!panelView.Contains(ControlsDictionary[item_id]))
                        {
                            panelView.Controls.Add(ControlsDictionary[item_id]);
                        }
                        ControlsDictionary[item_id].Visible = true;
                        ControlsDictionary[item_id].Focus();
                    }
                }
                else
                {
                    V6Control c = MenuManager.GenControl(this, e.SelectedButton, e);
                    
                    if (c != null)
                    {
                        e.SelectedButton.StatusNumber++;
                        _menu1Button.StatusNumber++;
                        AddMenu2Status(1);
                        menuControl1.Invalidate();
                        V6ControlFormHelper.MainMenu.Invalidate();
                        
                        var cName = c.Name;
                        c.Disposed += delegate(object sender1, EventArgs e1)
                        {
                            e.SelectedButton.StatusNumber--;
                            _menu1Button.StatusNumber--;
                            AddMenu2Status(-1);
                            menuControl1.Invalidate();
                            V6ControlFormHelper.MainMenu.Invalidate();
                            try
                            {
                                ControlsDictionary.Remove(cName);
                                FormManagerHelper.ShowCurrentMenu3Menu();
                            }
                            catch (Exception ex)
                            {
                                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, item_id), ex);
                            }
                        };
                        ControlsDictionary[item_id] = c;
                        panelView.Controls.Add(c);
                        c.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.SetStatusText("ErrorID: " + item_id + "\n" + ex.Message);
                this.WriteExLog(GetType() + ".MenuClick " + item_id + ":", ex);
            }
        }

        private void menuControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MenuButton mButton = menuControl1.SelectedButton;

                var c = MenuManager.GenControl(this, mButton, null);
                if (c is ChungTuChungContainer)
                {
                    ((ChungTuChungContainer)c).DisableZoomButton();
                }
                else if (c is BaoGiaContainer)
                {
                    ((BaoGiaContainer)c).DisableZoomButton();
                }
                else if (c is VitriCafeContainer)
                {
                    ((VitriCafeContainer)c).DisableZoomButton();
                }
                else if (c is DanhMucView)
                {
                    ((DanhMucView)c).DisableZoomButton();
                }
                else if (c is CategoryView)
                {
                    ((CategoryView)c).DisableZoomButton();
                }
                else if (c is SoDuView)
                {
                    ((SoDuView)c).DisableZoomButton();
                }
                else if (c is SoDuView2)
                {
                    ((SoDuView2)c).DisableZoomButton();
                }

                if (c != null)
                {
                    c.ShowToForm(this, mButton.Text, true, false);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MenuDoubleClick", ex);
            }
        }
        
        
        private void lblShowHide_Click(object sender, EventArgs e)
        {
            try
            {
                var button = (V6VeticalLabel)sender;
                var selectedText = menuControl1.SelectedButton == null ? V6Text.ShowMenu : menuControl1.SelectedButton.Text;
                V6ControlFormHelper.ShowHideMenu((V6VeticalLabel)sender, selectedText,
                    panelMenu, panelMenuShow, panelView, this, new Point(0,0), !button.IsShowing, 3);

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".LblMenuShow_Click " + ex.Message, ex.Source);
            }
        }

        public void HideMenu()
        {
            try
            {
                if (IsShow)
                {
                    var selectedText = menuControl1.SelectedButton == null ? V6Text.ShowMenu : menuControl1.SelectedButton.Text;
                    V6ControlFormHelper.ShowHideMenu(lblShowHide, selectedText,
                    panelMenu, panelMenuShow, panelView, this, new Point(0, 0), false, 3);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".HideMenu: " + ex.Message, ex.Source);
            }
        }
        public void ShowMenu()
        {
            try
            {
                if (!IsShow)
                {
                    var selectedText = menuControl1.SelectedButton == null ? V6Text.ShowMenu : menuControl1.SelectedButton.Text;
                    V6ControlFormHelper.ShowHideMenu(lblShowHide, selectedText,
                    panelMenu, panelMenuShow, panelView, this, new Point(0, 0), true, 3);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ShowMenu: " + ex.Message, ex.Source);
            }
        }

        private void Menu3Control_VisibleChanged(object sender, EventArgs e)
        {
            if (((Control) sender).Visible)
            {
                //Neu control nay hien ra.
            }
        }

        //Remove mainform_keydown
        /// <summary>
        /// Bật tính năng menu
        /// </summary>
        private bool _alt_m, _ctrl_alt_m, _have_alt_change_menu;
        /// <summary>
        /// Đếm Ctrol + Alt + I
        /// </summary>
        private int _ctrl_alt_i;
        public string _dohotkey = "";
        /// <summary>
        /// Điểu khiển phím tắt
        /// </summary>
        /// <param name="keyData"></param>
        public override void DoHotKey (Keys keyData)
        {
            _dohotkey = "";
            try
            {
                if (_alt_m)
                {
                    _alt_m = false;
                    string keyChar = keyData.ToString().ToUpper();

                    int index = 0;
                    var button = menuControl1.SelectedButton;
                    //var button_old = button;
                    if (button != null) index = menuControl1.Buttons.IndexOf(button);
                    int old_index = index;

                    //Tìm thử bên dưới nút menu đang chọn
                    for (int i = index + 1; i < menuControl1.Buttons.Count; i++)
                    {
                        button = menuControl1.Buttons[i];
                        if (button.Text.ToUpper().StartsWith(keyChar))
                        {
                            menuControl1.SelectedButton = button;
                            menuControl_Click(menuControl1,
                                new MenuControl.ButtonClickEventArgs(button,
                                    new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                            return;
                        }
                    }

                    //Nếu không thấy thì chạy lại đoạn trên
                    for (int i = 0; i < old_index; i++)
                    {
                        button = menuControl1.Buttons[i];
                        if (button.Text.ToUpper().StartsWith(keyChar))
                        {
                            menuControl1.SelectedButton = button;
                            menuControl_Click(menuControl1,
                                new MenuControl.ButtonClickEventArgs(button,
                                    new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                            return;
                        }
                    }
                    return;
                }

                if (_ctrl_alt_m)
                {
                    _ctrl_alt_m = false;
                    if (keyData == Keys.T)
                    {

                        ExpressionsCalculatorForm calc = new ExpressionsCalculatorForm();
                        calc.TopMost = true;
                        calc.Show(this);
                    }
                }

                if (keyData == (Keys.Control | Keys.Alt | Keys.I))
                {
                    ResetAltUpDownMenu();
                    _ctrl_alt_i++;
                    if (_ctrl_alt_i == 1)
                    {
                        Clipboard.SetText(menuControl1.SelectedButton.ItemID + "," + menuControl1.SelectedButton.CodeForm);
                        V6ControlFormHelper.SetStatusText(menuControl1.SelectedButton.ItemID + "," + menuControl1.SelectedButton.CodeForm);
                    }
                    if (_ctrl_alt_i == 2)
                    {
                        Control c = V6ControlFormHelper.GetControlUnderMouse(FindForm());
                        if (c != null)
                        {
                            string c_Text = c.Text;
                            var box = c as TextBox;
                            if (box != null && box.PasswordChar != '\0')
                            {
                                c_Text = "***";
                            }
                            string s = string.Format("{0}({1}), Text({2}), Aname({3}), Adescription({4}).",
                                c.GetType(), c.Name, c_Text, c.AccessibleName, c.AccessibleDescription);
                            Clipboard.SetText(s);
                            V6ControlFormHelper.SetStatusText(s);
                        }
                    }
                    if (_ctrl_alt_i >= 3)
                    {
                        if (!MenuManager.CheckPasswordV6(this)) return;

                        if (menuControl1.SelectedButton != null &&
                        ControlsDictionary.ContainsKey(menuControl1.SelectedButton.ItemID))
                        {
                            V6ControlFormHelper.ShowControlsProperties(ControlsDictionary[menuControl1.SelectedButton.ItemID]);
                        }
                        _ctrl_alt_i = 0;
                        return;
                    }
                }
                else
                {
                    _ctrl_alt_i = 0;
                }

                if (keyData == (Keys.Alt | Keys.Up))
                {
                    var button = menuControl1.SelectedButton;
                    var index = menuControl1.Buttons.IndexOf(button);
                    index--;
                    if (index < 0) index = menuControl1.Buttons.Count - 1;
                    button = menuControl1.Buttons[index];

                    menuControl1.SelectedButton = button;
                    _have_alt_change_menu = true;
                    //menuControl_Click(menuControl1, new MenuControl.ButtonClickEventArgs(button, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                }
                else if (keyData == (Keys.Alt | Keys.Down))
                {
                    var button = menuControl1.SelectedButton;
                    var index = menuControl1.Buttons.IndexOf(button);
                    index++;
                    if (index >= menuControl1.Buttons.Count) index = 0;
                    button = menuControl1.Buttons[index];

                    menuControl1.SelectedButton = button;
                    _have_alt_change_menu = true;
                    //menuControl_Click(menuControl1, new MenuControl.ButtonClickEventArgs(button, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                }
                else if (keyData == (Keys.Control | Keys.Alt | Keys.I))
                {
                    
                }
                else if (keyData == (Keys.Control | Keys.Alt | Keys.C))
                {
                    ResetAltUpDownMenu();
                    CalculatorForm calc = new CalculatorForm();
                    calc.TopMost = true;
                    calc.Show(this);
                }
                else if (keyData == (Keys.Control | Keys.Alt | Keys.M))
                {
                    ResetAltUpDownMenu();
                    _ctrl_alt_m = true;
                }
                else if (keyData == (Keys.Alt | Keys.Enter) && _have_alt_change_menu)
                {
                    _have_alt_change_menu = false;
                    menuControl_Click(menuControl1, new MenuControl.ButtonClickEventArgs(menuControl1.SelectedButton, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 1)));
                }
                else if (keyData == Keys.F1)
                {
                    ResetAltUpDownMenu();
                    if(menuControl1.SelectedButton != null)
                    V6ControlFormHelper.ShowHelp(menuControl1.SelectedButton.Key1, menuControl1.SelectedButton.Text, this);
                }
                else if (keyData == (Keys.Alt | Keys.M))
                {
                    ResetAltUpDownMenu();
                    _alt_m = true;
                }
                else
                {
                    ResetAltUpDownMenu();
                    
                    if (menuControl1.SelectedButton != null &&
                        ControlsDictionary.ContainsKey(menuControl1.SelectedButton.ItemID))
                    {
                        if (keyData == Keys.Escape)
                        {
                            _dohotkey += "menu3.Esc. ";
                        }
                        ControlsDictionary[menuControl1.SelectedButton.ItemID].DoHotKey(keyData);
                        //if (ControlsDictionary.ContainsKey(menuControl1.SelectedButton.ItemID) &&
                        //    !ControlsDictionary[menuControl1.SelectedButton.ItemID].IsDisposed)
                        //{
                        //    _dohotkey += ControlsDictionary[menuControl1.SelectedButton.ItemID]._d;
                        //}
                    }
                    else
                    {
                        if(IsShow)
                            FormManagerHelper.ShowMainMenu();
                        else
                            FormManagerHelper.ShowCurrentMenu3Menu();
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoHotKey", ex);
            }
        }

        private void ResetAltUpDownMenu()
        {
            _have_alt_change_menu = false;
            menuControl1.SelectedButton = _currentMenuButton;
        }

        private void lblShowHide_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
