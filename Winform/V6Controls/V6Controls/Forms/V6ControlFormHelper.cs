using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using GSM;
using V6AccountingBusiness;
using V6Controls.Controls;
using V6Controls.Controls.Label;
using V6Controls.Forms.DanhMuc.Add_Edit.ThongTinDinhNghia;
using V6Controls.Forms.Viewer;
using V6Init;
using V6ReportControls;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;
using Timer = System.Windows.Forms.Timer;

namespace V6Controls.Forms
{
    /// <summary>
    /// Các hàm chức năng liên quan đến form và control.
    /// </summary>
    public static class V6ControlFormHelper
    {
        /// <summary>
        /// ,cancel,disable,
        /// </summary>
        public static object TagHelp { get; set; }

        #region ==== ACTION LIST ====
        public static List<string> LastActionList = new List<string>();
        public static List<string> LastErrorList = new List<string>();
        public static int MaxActionListCount = 100;
        public static int MaxErrorListCount = 100;

        public static void AddLastAction(string s)
        {
            LastActionList.Add(s);
            while (LastActionList.Count > MaxActionListCount)
            {
                LastActionList.RemoveAt(0);
            }
        }
        
        public static void AddLastError(string s)
        {
            LastErrorList.Add(s);
            while (LastErrorList.Count > MaxErrorListCount)
            {
                LastErrorList.RemoveAt(0);
            }
        }

        public static string LastActionListString
        {
            get
            {
                string result = "";
                foreach (string s in LastActionList)
                {
                    result += "/" + s;
                }
                return result;
            }
        }
        
        public static string LastErrorListString
        {
            get
            {
                string result = "";
                foreach (string s in LastErrorList)
                {
                    result += "/" + s;
                }
                return result;
            }
        }
        #endregion action_list

        #region ==== RUNNING LIST ====
        public static SortedDictionary<string, string> RunningList = new SortedDictionary<string, string>();

        public static string RunningListString
        {
            get
            {
                string result = "";
                foreach (string s in RunningList.Values)
                {
                    result += "\r\n" + s;
                }
                return result.Trim();
            }
        }
        /// <summary>
        /// Thêm hoặc sửa thông tin chứng từ đang mở theo key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="description"></param>
        public static void AddRunningList(string key, string description)
        {
            RunningList[key] = description;
        }

        public static void RemoveRunningList(string key)
        {
            if (key != null && RunningList.ContainsKey(key))
            {
                RunningList.Remove(key);
            }
        }
        #endregion running list

        #region ==== TAG ====
        public static void AddTagString(Control control, string tagString)
        {
            var newTagString = control.Tag + "";
            if (!(";" + newTagString + ";").Contains("" + tagString + ";"))
            {
                newTagString += ";" + tagString;
                newTagString = newTagString.Replace(";;", ";");
                control.Tag = newTagString;
            }
            
            var checkTagString = ";" + newTagString + ";";
            control.Enabled = !checkTagString.Contains(";disable;");
            var visible = !checkTagString.Contains(";hide;");
            if (checkTagString.Contains(";invisible;")) visible = false;
            control.Visible = visible;
            var textbox = control as TextBox;
            if (textbox != null)
            {
                textbox.ReadOnly = checkTagString.Contains(";readonly;");
            }
        }

        public static void RemoveTagString(Control control, string tagString)
        {
            var checkTagString = ";" + tagString + ";";
            //control.Enabled = checkTagString.Contains(";disable;");
            if (checkTagString.Contains(";visible;")) control.Visible = false;
            if (checkTagString.Contains(";hide;")) control.Visible = true;

            if (control is TextBox)
            {
                var c = (TextBox)control;
                if (checkTagString.Contains(";readonly;"))
                {
                    c.ReadOnly = false;
                }
                if (checkTagString.Contains(";hide;") || checkTagString.Contains(";invisible;"))
                {
                    c.Visible = true;
                }
            }
            else if (control is DataGridView)
            {
                var c = (DataGridView)control;
                if (checkTagString.Contains(";readonly;"))
                {
                    c.ReadOnly = false;
                }
                if (checkTagString.Contains(";hide;") || checkTagString.Contains(";invisible;"))
                {
                    c.Visible = true;
                }
            }

            var newTagString = ";" + control.Tag + ";";//Lấy tagString cũ
            if (newTagString.Contains(checkTagString)) //Remove checkTag khỏi tagString cũ.
                newTagString = newTagString.Replace(checkTagString, ";");
            newTagString = newTagString.Trim(';');
            control.Tag = newTagString;
            //control.Tag = null;
            //control.AddTagString(newTagString);
        }
        #endregion tag

        #region ==== SHOW HIDE MESSAGE ====

        public static Form MainForm;
        public static V6Label MessageLable { get;set; }
        private static Timer _mainMessageTimer;
        private static int _mainTime = -1;
        /// <summary>
        /// Hiển thị một thông báo nổi ở trên chính giữa màn hình và mờ dần.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMainMessage(string message)
        {
            ShowTopMessage(message, null);
        }

        public static V6TopMessageForm TopMessageForm;
        /// <summary>
        /// Tạo sẵn form thông báo nổi.
        /// </summary>
        public static void CreateV6TopMessageForm()
        {
            if (TopMessageForm == null)
            {
                TopMessageForm = new V6TopMessageForm();
                TopMessageForm.Top = -TopMessageForm.Height;
                //TopMessageForm.Visible = false;
                TopMessageForm.Show();
            }
        }
        /// <summary>
        /// Hiển thị một form TopMost chứa thông báo, (Không ảnh hưởng đến focus đang làm việc).
        /// </summary>
        /// <param name="message">Nội dung thông báo.</param>
        /// <param name="owner">Form chủ.</param>
        public static void ShowTopMessage(string message, IWin32Window owner)
        {
            //CreateV6TopMessageForm();
            TopMessageForm.Message = message;
        }
        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên phải chương trình.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="owner"></param>
        public static void ShowTopRightMessage(string message, IWin32Window owner)
        {
            if (MessageLable.Parent != MainForm)
            {
                MainForm.Controls.Add(MessageLable);
            }

            if (_mainMessageTimer != null && _mainMessageTimer.Enabled)
            {
                _mainMessageTimer.Stop();
                //MessageLable.Top 
            }

            MessageLable.Text = message;
            _mainMessageTimer = new Timer { Interval = 200 };
            _mainMessageTimer.Tick += _mainMessageTimer_Tick;
            _mainTime = -1;
            _mainMessageTimer.Start();
        }
        

        static void _mainMessageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _mainTime++;
                if (_mainTime < 10)//Hiện ra
                {
                    MessageLable.Top -= MessageLable.Top/3;
                    
                    if (MessageLable.Top == -1) MessageLable.Top = 0;
                    if (MessageLable.Top == -2) MessageLable.Top = -1;
                    return;
                }
                if (_mainTime < 20)//Dừng lại
                {
                    return;
                }
                if (_mainTime < 30)//Ẩn đi
                {
                    MessageLable.Top -= MessageLable.Bottom/3;
                    if (MessageLable.Bottom == 1) MessageLable.Top = -MessageLable.Height;
                    if (MessageLable.Bottom == 2) MessageLable.Top = -MessageLable.Height+1;
                    return;
                }
                _mainMessageTimer.Stop();
            }
            catch// (Exception)
            {
                // ignored
            }
        }

        #endregion show hide message

        #region ==== SetStatusText ====
        public static void HaveStatusControl_MouseEnter(object sender, EventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseHover(object sender, EventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseMove(object sender, MouseEventArgs e)
        {
            SetStatusText(((Control)sender).Tag.ToString());
        }

        public static void HaveStatusControl_MouseLeave(object sender, EventArgs e)
        {
            SetStatusText("");
        }

        public static ToolStripStatusLabel StatusTextViewControl { get; set; }
        public static ToolStripStatusLabel StatusTextViewControl2 { get; set; }

        /// <summary>
        /// Gán status text phía dưới bên trái
        /// </summary>
        /// <param name="text"></param>
        public static void SetStatusText(string text)
        {
            StatusTextViewControl.Text = text ?? "";
        }
        /// <summary>
        /// Gán status text phía dưới bên phải
        /// </summary>
        /// <param name="text"></param>
        public static void SetStatusText2(string text)
        {
            if (StatusTextViewControl2 != null)
                StatusTextViewControl2.Text = text ?? "";
        }
        #endregion

        #region ==== SHOW HELP ====

        public static void ShowHelp(string itemId, string title = "", IWin32Window owner = null)
        {
            var file = Path.Combine(Application.StartupPath, "V6HELP\\" + itemId + ".pdf");
            if (!File.Exists(file))
            {
                if (owner is V6Form)
                {
                    ((V6Form)owner).ShowTopLeftMessage(V6Text.NotExist + "\n..." + file.Right(50));
                }
                else if (owner is V6Control)
                {
                    ((V6Control)owner).ShowTopLeftMessage(V6Text.NotExist + "\n..." + file.Right(50));
                }
                return;
            }
            var ext = Path.GetExtension(file).ToLower();
            if (ext == ".pdf")
            {
                var view = new PdfiumViewerForm(file, title);
                view.Show(owner);
            }
            else if (ext == ".doc")
            {
                
            }
            else
            {
                
            }
        }
        #endregion show help

        #region ==== SHOW HIDE MENU ====

        public static MenuControl MainMenu;
        public static V6VeticalLabel lblMenuMain { get; set; }
        //private static Timer _hide, _show;
        private static V6VeticalLabel _menu_v_label;
        private static Control _menuPanel, _menuShowControl, _viewControl, _containerControl;
        private static Point _menuPanelLocation;
        private static bool _checkMove;
        private static bool _isMoving;
        private static string _selectedText = "";
        static Timer _hide, _show;
        private static int _menuLever = 0;

        public static void SetHideMenuLabel(V6VeticalLabel label, string selectedText)
        {
            _menu_v_label = label;
            _selectedText = selectedText;
            if(_menu_v_label != null)
                _menu_v_label.HideText = _selectedText;
        }

        public static void HideMainMenu()
        {
            if(lblMenuMain==null)return;
            if(lblMenuMain.IsShowing) lblMenuMain.PerformClick();
        }
        public static void ShowMainMenu()
        {
            if(lblMenuMain==null)return;
            if(!lblMenuMain.IsShowing) lblMenuMain.PerformClick();
        }

        public static void ShowHideMenu(V6VeticalLabel sender, string selectText,
            Control panelMenuControl, Control panelMenuShowControl, Control panelViewControl,
            Control container, Point panelMenuLocation, bool isShow, int menuLever)
        {
            if (_isMoving) return;

            
            _menu_v_label = sender;
            _selectedText = selectText;
            _menuPanel = panelMenuControl;
            _menuShowControl = panelMenuShowControl;
            _viewControl = panelViewControl;
            _containerControl = container;
            _menuPanelLocation = panelMenuLocation;
            _menuLever = menuLever;
            if (isShow)
            {
                _menuPanel.Visible = true;
                _show = new Timer();
                _show.Tick += show_Tick;
                _show.Start();
            }
            else
            {
                if (_menuLever == 1)
                {
                    _viewControl.Width = _containerControl.Width - 33;
                }
                else if (_menuLever == 3)
                {
                    _viewControl.Width = _containerControl.Width - 23;
                }
                
                _hide = new Timer();
                _hide.Tick += hide_Tick;
                _hide.Start();
            }
        }

        #region ==== CHECK ====

        private static string hcheck = "";
        private static void Hcheck(Keys keyData)
        {
            switch (hcheck.Length)
            {
                case 0:
                    if (keyData == Keys.H) { hcheck += "h"; return; } break;
                case 1:
                    if (keyData == Keys.U) { hcheck += "u"; return; } break;
                case 2:
                    if (keyData == Keys.U) { hcheck += "u"; return; } break;
                case 3:
                    if (keyData == Keys.A) { hcheck += "a"; return; } break;
                case 4:
                    if (keyData == Keys.N) { hcheck += "n"; return; } break;
                case 5:
                    if (keyData == Keys.H) { hcheck += "h"; return; } break;
                case 6:
                    if (keyData == Keys.U) { hcheck += "u"; return; } break;
                case 7:
                    if (keyData == Keys.Y) { hcheck += "y"; return; } break;
                case 8:
                    if (keyData == Keys.N) { hcheck += "n"; return; } break;
                case 9:
                    if (keyData == Keys.H)
                    {
                        hcheck += "h";
                        ShowInfoMessage(hcheck);
                    } break;
            }
            hcheck = "";
        }
        #endregion check

        static void show_Tick(object sender, EventArgs e)
        {
            _checkMove = false;
            MoveTo(_menuPanel, _menuPanelLocation);
            MoveTo(_menuShowControl, _menuPanelLocation);
            MoveTo(_viewControl, new Point(_menuPanel.Right + 20, _viewControl.Top));
            //_viewControl.Width = _containerControl.Width - _viewControl.Left -10;

            if (!_checkMove)
            {
                ((Timer) sender).Stop();
                _isMoving = false;
                //_sender.HideText = _sender.Text;
                _menu_v_label.IsShowing = true;
                if (_menuLever == 1)
                {
                    _viewControl.Width = _containerControl.Width - _menuPanel.Width - 33;
                    _viewControl.Height = _menuPanel.Height;
                }
                else  if (_menuLever == 3)
                {
                    _viewControl.Width = _containerControl.Width - _menuPanel.Width - 23;
                    _viewControl.Height = _menuPanel.Height - 3;
                }
            }
            else
            {
                _isMoving = true;
            }
        }

        static void hide_Tick(object sender, EventArgs e)
        {
            _checkMove = false;
            MoveTo(_menuPanel, new Point(0 - _menuPanel.Width, _menuPanel.Top));
            MoveTo(_menuShowControl, new Point(20 - _menuShowControl.Width, _menuShowControl.Top));//Thừa ra 20xp
            MoveTo(_viewControl, new Point(_menuPanel.Right + 20, _viewControl.Top));
            //_viewControl.Width = _containerControl.Width - _viewControl.Left -10;

            if (!_checkMove)
            {
                _menuPanel.Visible = false;
                ((Timer)sender).Stop();
                _isMoving = false;
                _menu_v_label.IsShowing = false;
                _menu_v_label.HideText = _selectedText;
                if (_menuLever == 1)
                {
                    _viewControl.Width = _containerControl.Width - 33;
                    _viewControl.Height = _menuPanel.Height;
                }
                else if (_menuLever == 3)
                {
                    _viewControl.Width = _containerControl.Width - 23;
                    _viewControl.Height = _menuPanel.Height - 3;
                }
            }
            else
            {
                _isMoving = true;
            }
        }

        ///// <summary>
        ///// Không dùng nữa.
        ///// </summary>
        ///// <param name="panelMenuControl"></param>
        ///// <param name="panelMenuShowControl"></param>
        ///// <param name="panelViewControl"></param>
        ///// <param name="container"></param>
        ///// <param name="panelMenuLocation"></param>
        ///// <param name="isActive"></param>
        //private static void AutoHideMenu(Control panelMenuControl, Control panelMenuShowControl, Control panelViewControl,
        //    Control container, Point panelMenuLocation, bool isActive)
        //{
        //    if (!isActive || IsMouseHover(panelMenuControl, container) || IsMouseHover(panelMenuShowControl, container))
        //    {
        //        MoveTo(panelMenuControl, panelMenuLocation);
        //        MoveTo(panelMenuShowControl, panelMenuLocation);
        //    }
        //    else
        //    {
        //        MoveTo(panelMenuControl, new Point(-5 - panelMenuControl.Width, panelMenuControl.Top));
        //        MoveTo(panelMenuShowControl, new Point(15 - panelMenuControl.Width, panelMenuShowControl.Top));
        //    }
        //    MoveTo(panelViewControl, new Point(panelMenuControl.Right + 5, panelViewControl.Top));
        //}

        public static bool IsMouseHover(Control c, Control container)
        {
            Point p = Control.MousePosition;
            Point p1 = c.PointToClient(p);
            Point p2 = container.PointToClient(p);
            if (c.DisplayRectangle.Contains(p1) && container.DisplayRectangle.Contains(p2))
            {
                return true;
            }
            return false;
        }

        public static Image LoadCopyImage(string path)
        {
            Bitmap bm;
            using (Image im = Image.FromFile(path))
            {
                bm = new Bitmap(im);
            }
            return bm;
        }
        
        public static void MoveTo(Control c, Point p)
        {
            
            if (c.Location.X != p.X || c.Location.Y != p.Y)
            {
                _checkMove = true;
                Point p1 = new Point(
                    (c.Location.X + p.X)/2,
                    (c.Location.Y + p.Y)/2);
                if (Math.Abs(p1.X - p.X) == 1) p1.X = p.X;
                if (Math.Abs(p1.Y - p.Y) == 1) p1.Y = p.Y;
                c.Location = p1;
            }
        }

        #endregion show hide menu

        #region ==== GET / MAKE ====

        public static int GetAllTabTitleWidth(TabControl tabControl1)
        {
            var sum = 0;
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                sum += tabControl1.GetTabRect(i).Width;
            }
            return sum;
        }

        /// <summary>
        /// Lấy động danh sách control (textbox) từ bảng Alct
        /// </summary>
        /// <param name="alct1"></param>
        /// <param name="orderList">Dùng để xắp xếp lại gridview_columns khi cần.</param>
        /// <param name="alct1Dic">Dùng để lấy thông tin field khi cần.</param>
        /// <returns></returns>
        public static SortedDictionary<int, Control> GetDynamicControlsAlct(DataTable alct1,
            out List<string> orderList, out SortedDictionary<string, DataRow> alct1Dic)
            //out List<Control> carryList)
        {
            //exec [VPA_GET_AUTO_COLULMN] 'SOA','','','','';//08/12/2015
            var result = new SortedDictionary<int, Control>();

            //var alct1 = Invoice.Alct1;
            var _orderList = new List<string>();
            var _carryList = new List<Control>();
            var _alct1Dic = new SortedDictionary<string, DataRow>();

            Control temp_control = new Control();
            foreach (DataRow row in alct1.Rows)
            {
                //var visible = 1 == Convert.ToInt32(row["visible"]);
                //if (!visible) continue;

                var fcolumn = row["fcolumn"].ToString().Trim().ToUpper();
                _orderList.Add(fcolumn);
                _alct1Dic.Add(fcolumn, row);

                var fcaption = row[V6Setting.Language == "V" ? "caption" : "caption2"].ToString().Trim();
                var fvvar = row["fvvar"].ToString().Trim();
                var fstatus = Convert.ToBoolean(row["fstatus"]);

                var width = Convert.ToInt32(row["width"]);
                var ftype = row["ftype"].ToString().Trim();
                var fOrder = Convert.ToInt32(row["forder"]);
                var carry = Convert.ToInt32(row["carry"]) == 1;

                int decimals;

                Control c = temp_control;
                switch (ftype)
                {
                    #region Create controls
                    case "A0":
                        if (fcolumn == "TANG")
                        {
                            c = CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus, carry);
                        }
                        else if (fcolumn == "PX_GIA_DDI")
                        {
                            c = CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus, carry);
                        }
                        else if (fcolumn == "PN_GIA_TBI")
                        {
                            c = CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus, carry);
                        }
                        break;
                    case "A1":
                        c = CreateCheckTextBox(fcolumn, "a", fcaption, width, fstatus, carry);
                        break;
                    case "C0":
                        if (fvvar != "")
                        {
                            var checkvvar = Convert.ToBoolean(row["checkvvar"]);
                            var notempty = Convert.ToBoolean(row["notempty"]);
                            c = CreateVvarTextBox(fcolumn, fvvar, fcaption, width, fstatus, checkvvar, notempty, carry);
                        }
                        else
                        {
                            c = CreateColorTextBox(fcolumn, fcaption, width, fstatus, carry);
                        }
                        break;
                    case "N9"://Kieu so bat ky
                        decimals = row["fdecimal"] == null ? V6Setting.DecimalsNumber : Convert.ToInt32(row["fdecimal"]);
                        c = CreateNumberTextBox(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;

                    case "N0"://Tien
                        decimals = V6Options.M_IP_TIEN;// row["fdecimal"] == null ? V6Setting.DecilalsNumber : Convert.ToInt32(row["fdecimal"]);
                        c = CreateNumberTien(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;

                    case "N1"://Ngoai te
                        decimals = V6Options.M_IP_TIEN_NT;

                        c = CreateNumberTienNt(fcolumn, fcaption, decimals, width, fstatus, carry);
                        break;
                    case "N2"://so luong

                        decimals = V6Options.M_IP_SL;

                        c = CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;
                    case "N3"://GIA

                        decimals = V6Options.M_IP_GIA;
                        //Tuanmh 06/08/2017 - loi CreateNumberSoLuong
                        c = CreateNumberGia(fcolumn, fcaption, decimals, width, fstatus, carry);


                        break;
                    case "N4"://Gia nt

                        decimals = V6Options.M_IP_GIA_NT;
                        //Tuanmh 06/08/2017 - loi CreateNumberSoLuong
                        c = CreateNumberGiaNt(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;
                    case "N5"://Ty gia
                        decimals = V6Options.M_IP_TY_GIA;

                        c = CreateNumberTyGia(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;
                    case "D0": // Allow null
                        c = CreateDateTimeColor(fcolumn, fcaption, width, fstatus, carry);
                        break;
                    case "D1": // Not null
                        c = CreateDateTimePick(fcolumn, fcaption, width, fstatus, carry);
                        break;
                    #endregion
                }
                if (c != temp_control)
                {
                    result.Add(fOrder, c);
                    if (carry)
                    {
                        _carryList.Add(c);
                    }
                }
            }
            orderList = _orderList;
            alct1Dic = _alct1Dic;
            //carryList = _carryList;
            return result;
        }

        /// <summary>
        /// Lấy 1 giá trị thông qua AccessibleName
        /// </summary>
        /// <param name="control">Container hoặc chính control cần lấy giá trị.</param>
        /// <param name="accessibleName"></param>
        /// <returns></returns>
        public static object GetFormValue(Control control, string accessibleName)
        {
            try
            {
                if (control == null) return null;
                object result = null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && control.AccessibleName.ToUpper() == accessibleName.ToUpper())
                {
                    var box = control as V6IndexComboBox;
                    if (box != null)
                    {
                        result = box.SelectedIndex;
                    }
                    else if (control is ComboBox)
                    {
                        result = ((ComboBox)control).SelectedValue;
                    }
                    else if (control is DateTimePicker)
                    {
                        result = ((DateTimePicker)control).Value;
                    }
                    else if (control is V6NumberTextBox)
                    {
                        result = ((V6NumberTextBox)control).Value;
                    }
                    else if (control is V6CheckTextBox)
                    {
                        result = ((V6CheckTextBox)control).StringValue;
                    }
                    else if (control is CheckBox)
                    {
                        result = ((CheckBox)control).Checked;
                    }
                    else if (control is RadioButton)
                    {
                        result = ((RadioButton) control).Checked;
                    }
                    else if (control is GioiTinhControl)
                    {
                        result = ((GioiTinhControl)control).Value;
                    }
                    else
                    {
                        result = control.Text;
                    }
                }


                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        object o = GetFormValue(c, accessibleName);
                        if (o != null) return o;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormValue error!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Tìm một control trên form (hoặc control) thông qua Name không phân biệt HOA thường.
        /// Nếu không có trả về null
        /// </summary>
        /// <param name="control">Control chứa hoặc chính control cần tìm.</param>
        /// <param name="name">Name của control cần tìm không phân biệt HOA thường.</param>
        /// <returns></returns>
        public static Control GetControlByName(Control control, string name)
        {
            try
            {
                if (control == null) return null;

                if (!string.IsNullOrEmpty(control.Name)
                    && string.Equals(control.Name, name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return control;
                }
                return control.Controls.Count > 0 ?
                    (from Control c in control.Controls select GetControlByName(c, name)).FirstOrDefault(o => o != null)
                    : null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormControl error!\n" + ex.Message);
            }
        }
        /// <summary>
        /// Tìm một control trên form (hoặc control) thông qua accesibleName không phân biệt HOA thường.
        /// Nếu không có trả về null
        /// </summary>
        /// <param name="control">Control chứa hoặc chính control cần tìm.</param>
        /// <param name="accessibleName">AccessibleName của control cần tìm không phân biệt HOA thường.</param>
        /// <returns></returns>
        public static Control GetControlByAccessibleName(Control control, string accessibleName)
        {
            try
            {
                if (control == null) return null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && string.Equals(control.AccessibleName, accessibleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return control;
                }
                return control.Controls.Count > 0 ?
                    (from Control c in control.Controls select GetControlByAccessibleName(c, accessibleName)).FirstOrDefault(o => o != null)
                    : null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormControl error!\n" + ex.Message);
            }
        }

        public static List<Control> GetListControlByAccessibleNames(Control container, IList<string> accessibleName)
        {
            try
            {
                List<Control> ControlList = new List<Control>();
                foreach (Control c in container.Controls)
                {
                    if (accessibleName.Contains(c.AccessibleName, StringComparer.InvariantCultureIgnoreCase))
                    {
                        ControlList.Add(c);
                    }

                    List<Control> cl = GetListControlByAccessibleNames(c, accessibleName);
                    ControlList.AddRange(cl);
                }
                return ControlList;
            }
            catch (Exception ex)
            {
                throw new Exception("GetListControlByAccessibleNames error!\n" + ex.Message);
            }
        }

        
        public static List<Control> GetAllControls(Control container)
        {
            List<Control> ControlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                ControlList.Add(c);
                List<Control> cl = GetAllControls(c);
                ControlList.AddRange(cl);
            }
            return ControlList;
        }

        static List<string> _debugList = new List<string>(); 
        /// <summary>
        /// Lấy dữ liệu qua AccessibleName
        /// Khong biet co nen dung AccessibleDescription de danh dau data khong.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>Dic with UPPER keys</returns>
        public static SortedDictionary<string, object> GetFormDataDictionary(Control container)
        {
            string errors = "";
            _debugList = new List<string>();
            var d = GetFormDataDictionaryRecursive(container, ref errors);
            if (errors.Length > 0)
            {
                var ex = new Exception("GetFormDataDictionary error: " + errors);
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType
                    + ".GetFormDataDictionary", ex, LastActionListString);
                throw ex;
            }
            return d;
        }

        private static SortedDictionary<string, object> GetFormDataDictionaryRecursive(Control container, ref string errors)
        {
            var d = new SortedDictionary<string, object>();

            var tagString = string.Format(";{0};", container.Tag ?? "");

            var cancelall = container is DataGridView || tagString.Contains(";cancelall;");
            var canceldata = tagString.Contains(";canceldata;");
            var cancelget = tagString.Contains(";cancelget;");
            if (cancelall||canceldata||cancelget) goto CANCELALL;
            
            var cNAME = "";
            try
            {
                foreach (Control control in container.Controls)
                {
                    if (!string.IsNullOrEmpty(control.AccessibleName))
                    {
                        _debugList.Add(control.AccessibleName);
                        cNAME = control.AccessibleName.Trim().ToUpper();
                        if (!(control is RadioButton) && d.ContainsKey(cNAME))
                        {
                            Control f = FindParent<V6Control>(control);
                            var formName = (f == null ? "null" : f.Name);
                            var formType = (f == null ? "null" : f.GetType().ToString());
                            var message = string.Format("Form [{0}] type [{1}] trùng AccessibleName [{2}]",
                                formName, formType, cNAME);
                            ShowMainMessage(string.Format("Trùng AccessibleName [{0}]", cNAME));
                            WriteToLog("V6ControlFormHelper.GetFormDataDictionaryRecusive", message);
                        }

                        FillControlValueToDictionary(d, control);
                    }

                    if (control.Controls.Count > 0)
                    {
                        var t = GetFormDataDictionaryRecursive(control, ref errors);
                        foreach (KeyValuePair<string, object> keyValuePair in t)
                        {
                            cNAME = keyValuePair.Key;
                            d.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {
                errors += "\r\nAccessibleName: " + cNAME + "\r\nException: " + ex.Message;
            }
        CANCELALL:
            return d;
        }

        /// <summary>
        /// Lấy 1 đối tượng dữ liệu (model) từ form dựa theo model và AccessibleName trên form.
        /// </summary>
        /// <typeparam name="A">Kiểu model cần lấy</typeparam>
        /// <param name="container">Form chứa dữ liệu (this)</param>
        /// <returns></returns>
        public static A GetFormObjectValues<A>(Control container) where A : new()
        {
            var a = new A();
            var valDic = GetFormDataDictionary(container);
            foreach (PropertyInfo propertyInfo in a.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    //object value = GetFormValue(container, propertyInfo.Name);
                    object o = "";
                    if (valDic.ContainsKey(propertyInfo.Name.ToUpper()))
                        o = valDic[propertyInfo.Name.ToUpper()];
                    var value = ObjectAndString.ObjectTo(propertyInfo.PropertyType, o);
                    propertyInfo.SetValue(a, value, null);
                }
            }

            return a;
        }

        /// <summary>
        /// Tạo filterLine control từ chuỗi thông tin.
        /// Name = "line" + lineInfo.Field.ToUpper()
        /// </summary>
        /// <param name="define">Chuỗi thông tin (field:ngay_ct1;textv:Từ ngày;textE:From;where_field:ngay_ct;type:D;loai_key:10;oper:and;sqltype:smalldatetime;limitchar:ABCabc123;defaultValue:m_ngay_ct1)</param>
        /// <returns></returns>
        public static UserControl MadeLineDynamicControl(string define)
        {
            //string define = row["filter"].ToString().Trim();
            DefineInfo lineInfo = new DefineInfo(define);
            if (string.IsNullOrEmpty(lineInfo.Field)) return null;
            string CONTROL_TYPE = "";
            if (string.IsNullOrEmpty(lineInfo.ControlType) == false)
            {
                CONTROL_TYPE = lineInfo.ControlType.ToUpper();
            }

            //Trường hợp control đặc biệt, không phải FilterLineDynamic
            if (CONTROL_TYPE == "FILTERGROUP")
            {
                FilterGroup filter = new FilterGroup()
                {
                    Name = "line" + lineInfo.Field.ToUpper(),
                    //FieldName = lineInfo.Field.ToUpper(),
                    Caption = V6Setting.IsVietnamese ? lineInfo.TextV : lineInfo.TextE,
                    DefineInfo = lineInfo,
                    Enabled = lineInfo.Enabled,
                    Visible = lineInfo.Visible,
                    AccessibleName = lineInfo.AccessibleName,
                    //AccessibleName2 = lineInfo.AccessibleName2,
                };
                filter.GenControls(V6Setting.IsVietnamese ? lineInfo.DescriptionV : lineInfo.DescriptionE);
                return filter;
            }

            FilterLineDynamic lineControl;
            if (CONTROL_TYPE == "MAUBC")
            {
                lineControl = new FilterLineMauBC();
                //Gán các thuộc tính chung.
                lineControl.DefineInfo = lineInfo;
                lineControl.FieldName = lineInfo.Field.ToUpper();
                lineControl.Name = "line" + lineInfo.Field.ToUpper();
                //accessibleName
                lineControl.AccessibleName = lineInfo.AccessibleName;
                lineControl.AccessibleName2 = lineInfo.AccessibleName2;
                lineControl.Caption = lineInfo.TextLang(V6Setting.IsVietnamese);
                lineControl.Enabled = lineInfo.Enabled;
                lineControl.Visible = lineInfo.Visible;
            }
            else if (CONTROL_TYPE == "MAUALL")
            {
                var lineMauAll = new FilterLineMauALL();
                lineControl = lineMauAll;
                //Gán các thuộc tính chung.
                lineMauAll.DefineInfo = lineInfo;
                lineMauAll.FieldName = lineInfo.Field.ToUpper();
                
                lineControl.Name = "line" + lineInfo.Field.ToUpper();
                //accessibleName
                lineControl.AccessibleName = lineInfo.AccessibleName;
                lineControl.AccessibleName2 = lineInfo.AccessibleName2;
                lineControl.Caption = lineInfo.TextLang(V6Setting.IsVietnamese);
                lineControl.Enabled = lineInfo.Enabled;
                lineControl.Visible = lineInfo.Visible;
            }
            else if (CONTROL_TYPE == "DSNS")
            {
                lineControl = new FilterLineDSNS();

                var lineControl0 = lineControl as FilterLineDSNS;

                //Gán các thuộc tính chung.
                lineControl0.DefineInfo = lineInfo;
                lineControl0.FieldName = lineInfo.Field.ToUpper();
                lineControl0.Name = "line" + lineInfo.Field.ToUpper();
                //accessibleName
                lineControl0.AccessibleName = lineInfo.AccessibleName;
                lineControl0.AccessibleName2 = lineInfo.AccessibleName2;
                lineControl0.Caption = lineInfo.TextLang(V6Setting.IsVietnamese);
                lineControl0.EnabledTxt = lineInfo.Enabled;
                lineControl0.Visible = lineInfo.Visible;
            }
            else
            {
                lineControl = new FilterLineDynamic();

                //Gán các thuộc tính chung.
                lineControl.DefineInfo = lineInfo;
                lineControl.FieldName = lineInfo.Field.ToUpper();
                lineControl.Name = "line" + lineInfo.Field.ToUpper();
                //accessibleName
                lineControl.AccessibleName = lineInfo.AccessibleName;
                lineControl.AccessibleName2 = lineInfo.AccessibleName2;
                lineControl.Caption = lineInfo.TextLang(V6Setting.IsVietnamese);
                lineControl.Enabled = lineInfo.Enabled;
                lineControl.Visible = lineInfo.Visible;
            }

            V6VvarTextBox vT;
            V6LookupTextBox vL;
            V6LookupProc vP;
            V6FormButton bT;
            
            
            if (CONTROL_TYPE == "VVARTEXTBOX")
            {
                vT = lineControl.AddVvarTextBox(lineInfo.Vvar, lineInfo.InitFilter);
                //
                vT.BrotherFields = lineInfo.BField;
                vT.BrotherFields2 = lineInfo.BField2;
                if (!string.IsNullOrEmpty(lineInfo.ShowName)) vT.ShowName = lineInfo.ShowName == "1";
                vT.F2 = lineInfo.F2;
                vT.FilterStart = lineInfo.FilterStart;
            }
            else if (CONTROL_TYPE == "LOOKUPTEXTBOX") 
            {
                vL = lineControl.AddLookupTextBox(lineInfo.MA_DM, lineInfo.InitFilter,
                    lineInfo.Field, lineInfo.Field2, lineInfo.BField, lineInfo.NField);
                //
                vL.F2 = lineInfo.F2;
                vL.FilterStart = lineInfo.FilterStart;
            }
            else if (CONTROL_TYPE == "LOOKUPPROC") 
            {
                vP = lineControl.AddLookupProc(lineInfo.MA_DM, lineInfo.InitFilter,
                    lineInfo.Field, lineInfo.Field2, lineInfo.BField, lineInfo.NField);
                //
                vP.F2 = lineInfo.F2;
                vP.FilterStart = lineInfo.FilterStart;
            }
            else if (CONTROL_TYPE == "BUTTON")
            {
                bT = lineControl.AddButton(lineInfo.TextLang(V6Setting.IsVietnamese));
                int bT_width = ObjectAndString.ObjectToInt(lineInfo.Width);
                if (bT_width > 0)
                {
                    bT.AutoSize = false;
                    bT.Width = bT_width;
                }
            }
            else if (CONTROL_TYPE == "DATETIME")
            {
                lineControl.AddDateTimePick();
            }
            else if (CONTROL_TYPE == "DATETIMECOLOR")
            {
                lineControl.AddDateTimeColor();
            }
            else if (ObjectAndString.IsDateTimeType(lineInfo.DataType))
            {
                lineControl.AddDateTimePick();
            }
            else if (ObjectAndString.IsNumberType(lineInfo.DataType))
            {
                lineControl.AddNumberTextBox();
            }
            else// if (ObjectAndString.IsStringType(lineInfo.DataType))
            {

                if (lineInfo.Loai_key == "A1")
                {
                    lineControl.AddCheckBox();
                }
                else if (string.IsNullOrEmpty(lineInfo.Vvar))
                {
                    lineControl.AddTextBox();
                }
                else
                {
                    vT = lineControl.AddVvarTextBox(lineInfo.Vvar, lineInfo.InitFilter);
                    //
                    vT.BrotherFields = lineInfo.BField;
                    vT.BrotherFields2 = lineInfo.BField2;
                    if (!string.IsNullOrEmpty(lineInfo.ShowName)) vT.ShowName = lineInfo.ShowName == "1";
                    vT.F2 = lineInfo.F2;
                    vT.FilterStart = lineInfo.FilterStart;
                }
            }

            //Set giá trị thuộc tính
            //Dấu so sánh
            if (!string.IsNullOrEmpty(lineInfo.Oper)) lineControl.Operator = lineInfo.Oper;
            //Giá trị mặc định
            if (!string.IsNullOrEmpty(lineInfo.DefaultValue)) lineControl.SetValue(lineInfo.DefaultValue);
            //LimitChar
            if (!string.IsNullOrEmpty(lineInfo.LimitChars)) lineControl.SetLimitChars(lineInfo.LimitChars);
            //NotEmpty
            if (lineInfo.NotEmpty) lineControl.SetNotEmpty(true);

            return lineControl;
        }

        #endregion

        #region ==== SEND ====

        public static GSM_Phone SmsModem = new GSM_Phone();
        public static Dictionary<string, GSM_Phone> listModem = null;
        //public static string SmsModem_SettingPort = "";

        public static void ConnectModemSms(bool isAuto = false)
        {
            if (V6Options.M_AUTO_MODEM_SMS || isAuto)
            {
                var f = new SmsModemSettingForm();
                f.AutoConnect();
                //f.Close();
                f.Dispose();
            }
            else
            {
                new SmsModemSettingForm().ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="number"></param>
        /// <param name="returnMessage"></param>
        /// <returns>GSM_Phone.SendSmsStatus.OK/ERROR/NONE/UNKNOWN</returns>
        public static string SendSms(string message, string number, out string returnMessage)
        {
            GSM_Phone.SendSmsStatus result = GSM_Phone.SendSmsStatus.UNKNOWN;

            if (SmsModem.GSM_PORT == null || !SmsModem.IsConnected)
            {
                ConnectModemSms();
            }

            if (SmsModem.GSM_PORT == null || !SmsModem.IsConnected)
            {
                returnMessage = "Không có kết nối.";
                return GSM_Phone.SendSmsStatus.ERROR.ToString();
            }

            if (SmsModem.GSM_PORT != null)
            {
                if (!SmsModem.GSM_PORT.IsOpen)
                    SmsModem.OpenPort();


                result = SmsModem.SendMessage_PDU(number, message, true);
                switch (result)
                {
                    case GSM_Phone.SendSmsStatus.ERROR:
                        returnMessage = "Gửi lỗi!";
                        break;
                    case GSM_Phone.SendSmsStatus.NONE:
                        returnMessage = "Không gửi được hay gì đó!";
                        break;
                    case GSM_Phone.SendSmsStatus.OK:
                        returnMessage = "OK";
                        break;
                    case GSM_Phone.SendSmsStatus.UNKNOWN:
                        returnMessage = "Không biết gửi được không";
                        break;
                    default:
                        returnMessage = "???";
                        break;
                }

            }
            else
            {
                returnMessage = "Chưa kết nối!";
            }

            return result.ToString();
        }

        /// <summary>
        /// Gửi email và chờ kết quả.
        /// </summary>
        /// <param name="sender">Địa chỉ email người gửi.</param>
        /// <param name="password">Đối với Gmail nên dùng App password.</param>
        /// <param name="sendto">Địa chỉ email người nhận.</param>
        /// <param name="subject">Chủ đề.</param>
        /// <param name="body">Nội dung.</param>
        /// <param name="attachments">Files đính kèm.</param>
        /// <returns></returns>
        public static bool SendEmail(string sender, string password, string sendto, string subject, string body, params string[] attachments)
        {
            try
            {
                EmailSender eSender = new EmailSender();
                eSender.SendEmail(sender, password, sendto, subject, body, attachments);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gửi email Thread (không chờ đợi hoàn thành).
        /// </summary>
        /// <param name="sender">Địa chỉ email người gửi.</param>
        /// <param name="password">Đối với Gmail nên dùng App password.</param>
        /// <param name="sendto">Địa chỉ email người nhận.</param>
        /// <param name="subject">Chủ đề.</param>
        /// <param name="body">Nội dung.</param>
        /// <param name="attachments">Files đính kèm.</param>
        public static void SendEmailT(string sender, string password, string sendto, string subject, string body, params string[] attachments)
        {
            try
            {
                _sender = sender;
                _password = password;
                _sendto = sendto;
                _subject = subject;
                _body = body;
                _attachments = attachments;
                Thread t = new Thread(SendEmailThread);
                t.Start();
            }
            catch (Exception ex)
            {
                WriteExLog("V6ControlFormHelper.SendEmailT", ex);
            }
        }

        private static string _sender, _password, _sendto, _subject, _body;
        private static string[] _attachments;
        private static void SendEmailThread()
        {
            SendEmail(_sender, _password, _sendto, _subject, _body, _attachments);
        }

        #endregion send


        #region ==== SET... ====

        /// <summary>
        /// Gán value cho vài control anh em trên form có AccessibleName nằm trong list
        /// </summary>
        /// <param name="control"></param>
        /// <param name="row"></param>
        /// <param name="fields">Các field ngôn ngữ V.</param>
        /// <param name="fields2">Các field ngôn ngữ khác.</param>
        public static void SetBrotherData(Control control, DataRow row, string fields, string fields2)
        {
            Control parent = control.Parent;
            if (parent == null) return;
            if (string.IsNullOrEmpty(fields)) return;
            
            try
            {
                if (V6Setting.IsVietnamese || string.IsNullOrEmpty(fields2))
                {
                    fields = "," + fields.ToLower() + ",";
                    foreach (Control c in parent.Controls)
                    {
                        if (string.IsNullOrEmpty(c.AccessibleName)) continue;
                        string baseFIELD = c is RadioButton ? c.Name : (c.AccessibleName ?? "").ToUpper();
                        //Chỉ xử lý các control có AccessibleName trong fields
                        if (fields.Contains("," + baseFIELD.ToLower() + ","))
                        {
                            if (row == null || !row.Table.Columns.Contains(baseFIELD))
                            {
                                //Gán rỗng hoặc mặc định
                                SetControlValue(c, null);
                                continue;
                            }
                            SetControlValue(c, row[baseFIELD]);
                        }
                    }
                }
                else
                {
                    var field1list = ObjectAndString.SplitString(fields);
                    var field2list = ObjectAndString.SplitString(fields2);
                    for (int i = 0; i < field2list.Length; i++)
                    {
                        string f1 = field1list[i];
                        string f2 = field2list[i];
                        Control c = GetControlByAccessibleName(parent, f1);
                        if (c != null)
                        {
                            if (row == null || !row.Table.Columns.Contains(f2))
                            {
                                SetControlValue(c, null);
                            }
                            else
                            {
                                SetControlValue(c, row[f2]);
                            }
                        }
                    }
                    return;
                    fields2 = "," + fields2.ToLower() + ",";
                    foreach (Control c in parent.Controls)
                    {
                        if (string.IsNullOrEmpty(c.AccessibleName)) continue;
                        string baseFIELD = c is RadioButton ? c.Name : (c.AccessibleName ?? "").ToUpper();
                        //Chỉ xử lý các control có AccessibleName trong fields2
                        if (fields2.Contains("," + baseFIELD.ToLower() + ","))
                        {
                            if (row == null || !row.Table.Columns.Contains(baseFIELD))
                            {
                                //Gán rỗng hoặc mặc định
                                SetControlValue(c, null);
                                continue;
                            }
                            SetControlValue(c, row[baseFIELD]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetBrotherData error!\n" + (control.AccessibleName ?? "") + ": " + ex.Message);
            }
        } 
        
        public static void SetBrotherDataProc(Control control, IDictionary<string, object> row, string fields, string fields2)
        {
            Control parent = control.Parent;
            if (parent == null) return;
            if (string.IsNullOrEmpty(fields)) return;
            
            try
            {
                if (V6Setting.IsVietnamese || string.IsNullOrEmpty(fields2))
                {
                    fields = "," + fields.ToLower() + ",";
                    foreach (Control c in parent.Controls)
                    {
                        if (string.IsNullOrEmpty(c.AccessibleName)) continue;
                        string baseFIELD = c is RadioButton ? c.Name : (c.AccessibleName ?? "").ToUpper();
                        //Chỉ xử lý các control có AccessibleName trong fields
                        if (fields.Contains("," + baseFIELD.ToLower() + ","))
                        {
                            //if (row == null || !row.Table.Columns.Contains(baseFIELD))
                            if (row == null || !row.ContainsKey(baseFIELD))
                            {
                                //Gán rỗng hoặc mặc định
                                SetControlValue(c, null);
                                continue;
                            }
                            SetControlValue(c, row[baseFIELD]);
                        }
                    }
                }
                else
                {
                    var field1list = ObjectAndString.SplitString(fields);
                    var field2list = ObjectAndString.SplitString(fields2);
                    for (int i = 0; i < field2list.Length; i++)
                    {
                        string f1 = field1list[i];
                        string f2 = field2list[i];
                        Control c = GetControlByAccessibleName(parent, f1);
                        if (c != null)
                        {
                            //if (row == null || !row.Table.Columns.Contains(f2))
                            if (row == null || !row.ContainsKey(f2.ToUpper()))
                            {
                                SetControlValue(c, null);
                            }
                            else
                            {
                                SetControlValue(c, row[f2.ToUpper()]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetBrotherData error!\n" + (control.AccessibleName ?? "") + ": " + ex.Message);
            }
        }
        
        /// <summary>
        /// Gán value cho vài control hàng xóm ánh xạ theo tên.
        /// </summary>
        /// <param name="control">Control chứa</param>
        /// <param name="row">Dữ liệu</param>
        /// <param name="neighbor_field">key là Neighbor, value là field ánh xạ</param>
        public static void SetNeighborData(Control control, DataRow row, IDictionary<string, string> neighbor_field)
        {
            //if (string.IsNullOrEmpty(fields)) return;
            //if (string.IsNullOrEmpty(fields2)) return;

            //fields = "," + fields.ToLower() + ",";
            try
            {
                //if (row == null) return;
                Control parent = control.Parent;
                if (parent != null)
                {
                    foreach (Control c in parent.Controls)
                    {
                        var aNAME = c is RadioButton ? c.Name : c.AccessibleName;
                        if (string.IsNullOrEmpty(aNAME)) continue;
                        aNAME = aNAME.ToUpper();
                        if (!neighbor_field.ContainsKey(aNAME)) continue;
                        var fieldName = neighbor_field[aNAME];

                        if (row != null && row.Table.Columns.Contains(fieldName))
                        {
                            SetControlValue(c, row[fieldName]);
                        }
                        else // Có trong neighbor nhưng không có trong data
                        {
                            //Gán rỗng hoặc mặc định
                            SetControlValue(c, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetBrotherData error!\n" + (control.AccessibleName ?? "") + ": " + ex.Message);
            }
        }

        public static void SetNeighborDataProc(Control control, IDictionary<string, object> row, IDictionary<string, string> neighbor_field)
        {
            try
            {
                //if (row == null) return;
                Control parent = control.Parent;
                if (parent != null)
                {
                    foreach (Control c in parent.Controls)
                    {
                        var aNAME = c is RadioButton ? c.Name : c.AccessibleName;
                        if (string.IsNullOrEmpty(aNAME)) continue;
                        aNAME = aNAME.ToUpper();
                        if (!neighbor_field.ContainsKey(aNAME)) continue;
                        var FIELD_NAME = neighbor_field[aNAME].ToUpper();

                        if (row != null && row.ContainsKey(FIELD_NAME))
                        {
                            SetControlValue(c, row[FIELD_NAME]);
                        }
                        else // Có trong neighbor nhưng không có trong data
                        {
                            //Gán rỗng hoặc mặc định
                            SetControlValue(c, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetBrotherData error!\n" + (control.AccessibleName ?? "") + ": " + ex.Message);
            }
        }

        /// <summary>
        /// Set các control nhập liệu thành readonly (hoặc không)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="readOnly"></param>
        public static void SetFormControlsReadOnly(Control control, bool readOnly)
        {
            var tagString = string.Format(";{0};", control.Tag ?? "");

            var cancelall = tagString.Contains(";cancelall;");
            if (cancelall)
                goto CANCELALL;
            var cancel = tagString != "" && tagString.Contains(";cancel;");
            if (cancel) goto CANCEL;

            var readonl2 = tagString.Contains(";readonly;");
            var disable = tagString.Contains(";disable;");
            var enable = tagString.Contains(";enable;");

            if (control is TextBox)
            {
                var txt = control as TextBox;
                txt.ReadOnly = readOnly || readonl2;
                if (disable) txt.Enabled = false;
                if (enable) txt.Enabled = true;
            }
            else if (control is V6DateTimePicker)
            {
                var dat = control as V6DateTimePicker;
                dat.ReadOnly = (readOnly || readonl2);
                if (disable) dat.Enabled = false;
                if (enable) dat.Enabled = true;
            }
            else if (control is DateTimePicker
                || control is CheckBox
                || control is RadioButton
                || control is ComboBox
                || control is V6FormButton
                || control is GioiTinhControl
                )
            {
                control.Enabled = !(readOnly || readonl2);
                if (disable) control.Enabled = false;
                if (enable) control.Enabled = true;
            }
            else if (control is DataGridView)
            {
                var dgv = (DataGridView)control;
                dgv.ReadOnly = readOnly || readonl2;

                if (disable) control.Enabled = false;
                if (enable) control.Enabled = true;
                
                goto CANCELALL;
            }
            else if (control is TextBoxBase)
            {
                ((TextBoxBase)control).ReadOnly = readOnly || readonl2;
            }
            //else
            //{
            //    if (readonli || readonl2) control.Enabled = false;
            //    else control.Enabled = true;
            //}
            
            CANCEL:
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    SetFormControlsReadOnly(c, readOnly);
                }
            }
            CANCELALL:;
        }

        /// <summary>
        /// Lưu giá trị hiện tại vào carry
        /// </summary>
        /// <param name="control"></param>
        public static void SetCarryValues(Control control)
        {
            try
            {
                _errors = "";
                SetCarryValuesRecursive(control);
            }
            catch (Exception ex)
            {
                _errors += "\nSetCarryValues: " + control.AccessibleName + ": " + ex.Message;
            }
            if (_errors != "")
                throw new Exception("SetCarryValuesRecursive: " + _errors);
        }
        public static void SetCarryValuesRecursive(Control control)
        {
            try
            {
                var box = control as V6ColorTextBox;
                if (box != null)
                {
                    //Gồm cả vvar, number va date (override)
                    box.CarryValue();
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetCarryValuesRecursive(c);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetCarryValue error!\n" + control.AccessibleName + ": " + ex.Message);
            }
        }

        public static void UseCarryValues(Control control)
        {
            try
            {
                _errors = "";
                UseCarryValuesRecursive(control);
            }
            catch (Exception ex)
            {
                _errors += "\nSetCarryValues: " + control.AccessibleName + ": " + ex.Message;
            }
            if (_errors != "")
                throw new Exception("SetCarryValuesRecursive: " + _errors);
        }
        public static void UseCarryValuesRecursive(Control control)
        {
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var canceldata = tagString != "" && tagString.Contains(";canceldata;");
                if (canceldata) goto CANCELALL;

                {
                    #region === Gán giá trị ===

                    var box = control as V6ColorTextBox;
                    if (box != null)
                    {
                        //Gồm cả vvar, number va date (override)
                        box.UseCarry();
                    }
                    #endregion gán giá trị
                }

                //CANCEL:
                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        UseCarryValuesRecursive(c);
                    }
                }
            CANCELALL: ;
            }
            catch (Exception ex)
            {
                throw new Exception("UseCarryValue error!\n" + control.AccessibleName + ": " + ex.Message);
            }
        }


        /// <summary>
        /// Gán value cho tất cả control trên form có AccessibleName
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data">data chỉ gồm 1 dòng dữ liệu</param>
        public static void SetFormDataRow(Control control, DataRow data)
        {
            SetFormDataDictionary(control, data.ToDataDictionary());
        }


        private static string _errors = "";
        /// <summary>
        /// Gán value cho các control trên form có AccessibleName
        /// Không có data thì sẽ set rỗng nếu set_default = true
        /// Có cancelall, canceldata
        /// </summary>
        /// <param name="control">Form cần điền dữ liệu, thường dùng từ khóa this</param>
        /// <param name="data">Lưu ý. nên dùng key UPPER</param>
        /// <param name="set_default">Nếu không có dữ liệu thì gán rỗng hoặc mặc định.</param>
        public static SortedDictionary<string, Control> SetFormDataDictionary(Control control, IDictionary<string, object> data, bool set_default = true)
        {
            SortedDictionary<string, Control> result = new SortedDictionary<string, Control>();
            try
            {
                _errors = "";
                result = SetFormDataDicRecursive(control, data, set_default);
            }
            catch (Exception ex)
            {
                _errors += "\nSetFormDataDic: " + control.AccessibleName + ": " + ex.Message;
            }
            if (_errors != "")
            {
                var ex = new Exception("SetFormDataDictionary: " + _errors);
                WriteExLog("V6ControlFormHelper.SetFormDataDictionary", ex);
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// Gán value cho một số control có AccessibleName trùng data
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        public static SortedDictionary<string, Control> SetSomeDataDictionary(Control control, IDictionary<string, object> data)
        {
            SortedDictionary<string, Control> result = new SortedDictionary<string, Control>();
            try
            {
                _errors = "";
                result = SetFormDataDicRecursive(control, data, false);
            }
            catch (Exception ex)
            {
                _errors += "\nSetSomeDataDictionary: " + control.AccessibleName + ": " + ex.Message;
            }
            if (_errors != "")
            {
                var ex = new Exception("SetSomeDataDictionary: " + _errors);
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType
                    + ".SetSomeDataDictionary", ex, LastActionListString);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Gán dữ liệu lên form hoặc control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        /// <param name="set_default"></param>
        /// <returns>Các control được gán dữ liệu. key=AccessibleName</returns>
        private static SortedDictionary<string, Control> SetFormDataDicRecursive(Control control, IDictionary<string, object> data, bool set_default = true)
        {
            SortedDictionary<string, Control> result = new SortedDictionary<string, Control>();
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var cancelall = control is DataGridView || control is ICrystalReportViewer || tagString.Contains(";cancelall;");
                var canceldata = tagString.Contains(";canceldata;");
                var cancelset = tagString.Contains(";cancelset;");
                if (canceldata||cancelset||cancelall) goto CANCELALL;
                
                var NAME = control is RadioButton ? control.Name : control.AccessibleName;
                if (data != null && !string.IsNullOrEmpty(NAME) && data.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();
                    result[NAME] = control;
                    
                    SetControlValue(control, data[NAME]);
                }
                else if (set_default && !string.IsNullOrEmpty(NAME))
                {
                    // === Gán rỗng hoặc mặc định ===
                    SetControlValue(control, null);
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        var result1 = SetFormDataDicRecursive(c, data, set_default);
                        result.AddRange(result1);
                    }
                }
            CANCELALL: ;
            }
            catch (Exception ex)
            {
                _errors += "\r\nAccessibleName: " + control.AccessibleName
                    + "\r\nException: " + ex.Message;
            }
            return result;
        }

        public static void SetFormTagDictionary(Control control, SortedDictionary<string, string> tagData)
        {
            try
            {
                _errors = "";
                SetFormTagDicRecursive(control, tagData);
            }
            catch (Exception ex)
            {
                _errors += "\r\nControlName: " + control.Name + "\r\nException: " + ex.Message;
            }
            if (_errors != "")
            {
                var ex = new Exception("SetFormTagDictionary: " + _errors);
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType
                    + ".SetFormTagDictionary", ex, LastActionListString);
                throw ex;
            }
        }

        private static void SetFormTagDicRecursive(Control control, SortedDictionary<string, string> tagData)
        {
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var cancelall = control is DataGridView || control is ICrystalReportViewer || tagString.Contains(";cancelall;");
                var canceldata = tagString.Contains(";canceldata;");
                var cancelset = tagString.Contains(";cancelset;");
                if (canceldata || cancelset || cancelall) goto CANCELALL;

                var NAME = control.Name;
                if (tagData != null && !string.IsNullOrEmpty(NAME) && tagData.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();

                    control.AddTagString(tagData[NAME]);
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormTagDicRecursive(c, tagData);
                    }
                }

                control.ControlAdded += (object sender, ControlEventArgs e) =>
                {
                    SetFormTagDicRecursive(e.Control, tagData);
                };
        
            CANCELALL: ;
            }
            catch (Exception ex)
            {
                _errors += "\r\nControlName: " + control.Name + "\r\nException: " + ex.Message;
            }
        }

        



        /// <summary>
        /// Tìm control anh em (cùng nằm chung trong 1 control chứa)
        /// </summary>
        /// <param name="accName"></param>
        /// <param name="brother"></param>
        /// <returns></returns>
        private static Control FindBrotherControlByAccessibleName(string accName, Control brother)
        {
            if (brother.Parent == null) return null;
            accName = accName.ToUpper();
            return brother.Parent.Controls.Cast<Control>()
                .FirstOrDefault(c => c.AccessibleName != null && c.AccessibleName.ToUpper() == accName);
        }

        /// <summary>
        /// Tải, xử lý thông tin tự định nghĩa của người dùng.
        /// </summary>
        /// <param name="ma_dm">Mã dm định nghĩa trong Altt (MaCt, TableName)</param>
        /// <param name="control">Form chứa các control tự định nghĩa.</param>
        /// <param name="parent">Form cha đang chứa (this)</param>
        /// <param name="debug">Thông tin _sttRec hoặc chi tiết khác</param>
        public static void ProcessUserDefineInfo(string ma_dm, Control control, Control parent, string debug = "")
        {
            try
            {
                if (V6BusinessHelper.CheckAltt(ma_dm))
                {
                    var f = new ThongTinDinhNghiaForm(ma_dm);
                    f.UpdateSuccessEvent += data =>
                    {
                        LoadAndSetFormInfoDefine(ma_dm, control, parent);
                    };
                    f.ShowDialog(control);
                }
            }
            catch (Exception ex)
            {
                parent.ShowErrorException(string.Format("{0}.{1} {2}", parent.GetType(), "ProcessUserDefineInfo", debug), ex);
            }
        }

        /// <summary>
        /// Lấy thông tin trường tự định nghĩa gán lên form.
        /// <param name="ma_dm">Mã dm định nghĩa trong Altt (MaCt, tableName)</param>
        /// <param name="control">Form chứa các control tự định nghĩa.</param>
        /// <param name="parent">Form cha đang chứa (this)</param>
        /// </summary>
        public static void LoadAndSetFormInfoDefine(string ma_dm, Control control, Control parent)
        {
            try
            {
                var key = new SortedDictionary<string, object> { { "ma_dm", ma_dm } };
                var selectResult = V6BusinessHelper.Select(V6TableName.Altt, key, "", "", "");
                SetFormInfoDefine(control, selectResult.Data, V6Setting.Language);
            }
            catch (Exception ex)
            {
                control.ShowErrorException(parent.GetType() + ".Load user define info error!", ex);
            }
        }

        /// <summary>
        /// Dùng cho các trường tự định nghĩa, gán thông tin trường định nghĩa lên form
        /// </summary>
        /// <param name="control">Control (panel, container) chứa các control định nghĩa</param>
        /// <param name="data">Dữ liệu bản Altt</param>
        /// <param name="lang">Ngôn ngữ hiển thị</param>
        public static void SetFormInfoDefine(Control control, DataTable data, string lang)
        {
            //Visible=1,Caption=M· §N 1,English=User define code 1,Format=N2
            try
            {
                if (data == null || data.Rows.Count == 0) return;
                DataRow row = data.Rows[0];

                if (!string.IsNullOrEmpty(control.AccessibleDescription))
                {
                    var descriptions = control.AccessibleDescription.Split(',');
                    if (descriptions.Length == 2)
                    {
                        var labelField = descriptions[0];
                        var dataField = descriptions[1];
                        Control dataControl = FindBrotherControlByAccessibleName(dataField, control);
                        if (data.Columns.Contains(labelField))
                        {
                            string s = row[labelField].ToString().Trim();
                            string[] ssss = s.Split(new[] {','}, 4);
                            //check visible
                            bool visible = true;
                            string[] ss = ssss[0].Split('=');
                            if (ss.Length == 2 && ss[1] != "1") visible = false;

                            control.Visible = visible;
                            if (!string.IsNullOrEmpty(dataField))
                            {
                                if (dataControl != null)
                                {
                                    dataControl.Visible = visible;
                                    control.Visible = visible;
                                }
                            }

                            if (visible) //Gán text
                            {
                                ss = lang.ToUpper() == "E" ? ssss[2].Split('=') : ssss[1].Split('=');
                                control.Text = ss[1];
                            }

                            // format
                            if (ssss.Length >= 4 && ssss[3].Length > 1)
                            {
                                ss = ssss[3].Split('=');
                                if (ss.Length < 2 || ss[1].Trim() == "") goto EndFormat;

                                var format = ss[1];

                                int formatNum = ObjectAndString.ObjectToInt(format.Substring(1));
                                if (dataControl is V6NumberTextBox)
                                {
                                    // set DecimalPlaces format.
                                    ((V6NumberTextBox)dataControl).DecimalPlaces = formatNum;
                                }
                                else
                                {
                                    //Dành cho các loại khác
                                    // set MaxLength???
                                }
                            }EndFormat:;
                        }
                    }
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormInfoDefine(c, data, lang);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("SetFormInfo error!\n" + ex.Message);
            }
        }

        /// <summary>
        /// Chỉ mới có MaxLength cho TextBox
        /// </summary>
        /// <param name="control"></param>
        /// <param name="structTable"></param>
        public static void SetFormStruct(Control control, V6TableStruct structTable)
        {
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");

                var cancelall = control is DataGridView || control is ICrystalReportViewer || tagString.Contains(";cancelall;");
                if (cancelall) goto CANCELALL;
                var cancel = tagString != "" && tagString.Contains(";cancel;");
                if (cancel) goto CANCEL;

                var NAME = control.AccessibleName;

                if (control is TextBox && !string.IsNullOrEmpty(NAME)
                        && structTable.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();
                    var num = control as V6NumberTextBox;
                    if (num != null)
                    {
                        if (string.IsNullOrEmpty(num.LimitCharacters))
                        {
                            try
                            {
                                if (num is NumberTien)
                                {
                                    num.DecimalPlaces = V6Options.M_IP_TIEN;
                                }
                                else if (num is NumberTienNt)
                                {
                                    num.DecimalPlaces = V6Options.M_IP_TIEN_NT;
                                }
                                else if (num is NumberGia)
                                {
                                    num.DecimalPlaces = V6Options.M_IP_GIA;
                                }
                                else if (num is NumberGiaNt)
                                {
                                    num.DecimalPlaces = V6Options.M_IP_GIA_NT;
                                }
                                else if (num is NumberSoluong)
                                {
                                    num.DecimalPlaces = V6Options.M_IP_SL;
                                }
                                else if (!string.IsNullOrEmpty(num.NumberFormatName))
                                {
                                    num.DecimalPlaces
                                        = Convert.ToInt32(V6Options.GetValue(num.NumberFormatName));
                                }
                                else
                                {
                                    num.MaxNumLength = structTable[NAME].MaxNumLength;

                                    num.MaxNumDecimal = structTable[NAME].MaxNumDecimal;
                                    num.DecimalPlaces = structTable[NAME].MaxNumDecimal;
                                }

                                int tempMaxLength = 0;
                                var num_MaxNumLength = structTable[NAME].MaxNumLength;
                                tempMaxLength += num_MaxNumLength;
                                var temp3 = (int)Math.Ceiling((decimal)tempMaxLength / 3);
                                if (temp3 > 0) tempMaxLength += temp3 - 1;

                                var num_MaxNumDecimal = structTable[NAME].MaxNumDecimal;
                                if(num_MaxNumDecimal>0) tempMaxLength += (num_MaxNumDecimal + 1);

                                num.MaxLength = tempMaxLength;
                            }
                            catch
                            {
                                // ignored
                            }
                            
                        }
                        else
                        {
                             // limit textbox
                        }
                    }
                    else
                    {
                        var txt = control as V6ColorTextBox;
                        if (txt != null && string.IsNullOrEmpty(txt.LimitCharacters))
                        {
                            int ml = structTable[control.AccessibleName.ToUpper()].MaxLength;
                            if (ml > 0) txt.MaxLength = ml < 0 ? 32767 : ml;
                        }
                    }
                }
                else if (control is TextBox)
                {
                    //Không chứa trong cấu trúc bảng, không cần thiết accname
                    var num = control as V6NumberTextBox;
                    if (num != null)
                    {
                        if (string.IsNullOrEmpty(num.LimitCharacters))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(num.NumberFormatName))
                                {
                                    num.DecimalPlaces = Convert.ToInt32(V6Options.GetValue(num.NumberFormatName));
                                }
                                else
                                {
                                    
                                }
                            }
                            catch
                            {
                                // ignored
                            }

                        }
                        else
                        {
                            // limit textbox
                        }
                    }
                    else// textbox thuong
                    {

                        
                    }
                }
                CANCEL:
                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormStruct(c, structTable);
                    }
                }
                CANCELALL:
                ;
            }
            catch (Exception ex)
            {
                throw new Exception("SetFormStruct error!\n" + ex.Message);
            }
        }
        #endregion set

        #region ==== Set form text ====

        /// <summary>
        /// CorpLan
        /// </summary>
        /// <param name="form"></param>
        public static void SetFormText(Control form)
        {
            try
            {
                //var tagString = string.Format(";{0};", form.Tag ?? "");
                //var cancellang = form is DataGridView || tagString.Contains(";cancellang;");
                //if (cancellang) return;

                //if (V6Options.V6OptionValues == null || V6Options.V6OptionValues.Count == 0) return;
                if (!V6Options.ContainsKey("M_TRAN_LANG") || V6Options.GetValue("M_TRAN_LANG") != "1") return;
                
                var idList = GetFormAccessibleDescriptions(form);
                var dic = CorpLan.GetTextDic(idList, V6Setting.Language, form.Name + "-" + form.GetType());
                SetFormTextRecursive(form, dic);
                form.AddTagString("cancellang");
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(string.Format("{0} {1}.{2}", V6Login.ClientName,
                MethodBase.GetCurrentMethod().DeclaringType, MethodBase.GetCurrentMethod().Name),
                ex, LastActionListString);
            }
        }

        private static void SetFormTextRecursive(Control control, SortedDictionary<string, string> textDic)
        {
            if (!string.IsNullOrEmpty(control.AccessibleDescription))
            {
                var KEY = (control.AccessibleDescription??"").ToUpper();
                if (textDic.ContainsKey(KEY) && !string.IsNullOrEmpty(textDic[KEY]))
                {
                    control.Text = textDic[KEY];
                }
            }

            var menuControl = control as MenuControl;
            if (menuControl != null)
            {
                foreach (MenuButton b in menuControl.Buttons)
                {
                    if (!string.IsNullOrEmpty(b.AccessibleDescription))
                    {
                        var KEY = b.AccessibleDescription.ToUpper();
                        if (textDic.ContainsKey(KEY) && !string.IsNullOrEmpty(textDic[KEY]))
                        {
                            b.Text = textDic[KEY];
                        }
                    }
                }
            }

            if (control.ContextMenuStrip != null)
            {
                foreach (ToolStripMenuItem menu_item in control.ContextMenuStrip.Items)
                {
                    var KEY = (menu_item.AccessibleDescription ?? "").ToUpper();
                    if (textDic.ContainsKey(KEY) && !string.IsNullOrEmpty(textDic[KEY]))
                    {
                        menu_item.Text = textDic[KEY];
                    }
                }
            }
            if (control is DropDownButton)
            {
                var button = control as DropDownButton;
                if (button.Menu != null)
                {
                    foreach (ToolStripMenuItem menu_item in button.Menu.Items)
                    {
                        var KEY = (menu_item.AccessibleDescription ?? "").ToUpper();
                        if (textDic.ContainsKey(KEY) && !string.IsNullOrEmpty(textDic[KEY]))
                        {
                            menu_item.Text = textDic[KEY];
                        }
                    }
                }
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    SetFormTextRecursive(c, textDic);
                }
            }
            else if (control is StatusStrip)
            {
                var sts = (StatusStrip)control;
                foreach (ToolStripItem item in sts.Items)
                {
                    var KEY = item.AccessibleDescription??"";
                    if (textDic.ContainsKey(KEY) && !string.IsNullOrEmpty(textDic[KEY]))
                    {
                        item.Text = textDic[KEY];
                    }
                }
            }
        }


        public static V6ColorTextBox CreateColorTextBox(string accessibleName, string caption, int width, bool visible, bool carry = false)
        {
            return new V6ColorTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static V6NumberTextBox CreateNumberTextBox(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new V6NumberTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static NumberTien CreateNumberTien(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberTien
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static NumberTienNt CreateNumberTienNt(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberTienNt
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static NumberSoluong CreateNumberSoLuong(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberSoluong
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        //Tuanmh 06/08/2017
        public static NumberGia CreateNumberGia(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberGia
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        //Tuanmh 06/08/2017
        public static NumberGiaNt CreateNumberGiaNt(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberGiaNt
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static NumberTygia CreateNumberTyGia(string accessibleName, string caption, int decimals, int width, bool visible, bool carry = false)
        {
            return new NumberTygia
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                DecimalPlaces = decimals,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }

        public static V6VvarTextBox CreateVvarTextBox(string accessibleName, string vvar, string caption, int width, bool visible,
            bool checkOnLeave, bool checkNotEmpty, bool carry = false)
        {
            return new V6VvarTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                VVar = vvar,
                CheckOnLeave = checkOnLeave,
                CheckNotEmpty = checkNotEmpty,
                F5 = checkOnLeave, // F5 = false => chạy v6lostfocus (checkton)
                F2 = false,
                GrayText = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }

        public static V6LookupTextBox CreateLookupTextBox(string accessibleName,
            string ma_dm, string value_field, string text_field, string brother, string neighbor,
            string caption, int width, bool visible,
            bool checkOnLeave, bool checkNotEmpty, bool carry = false)
        {
            return new V6LookupTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                AccessibleName2 = accessibleName + "2",
                Carry = carry,
                Ma_dm = ma_dm,
                ValueField = value_field,
                ShowTextField = text_field,
                BrotherFields = brother,
                NeighborFields = neighbor,

                CheckOnLeave = checkOnLeave,
                CheckNotEmpty = checkNotEmpty,
                GrayText = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }

        public static V6ColorTextBox CreateLimitTextBox(string accessibleName, string limit, string caption, int width, bool visible, bool carry = false)
        {
            var a = new V6ColorTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                //LimitCharacters = limit,
                GrayText = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
            a.SetLimitCharacters(limit);
            return a;
        }
        public static V6CheckTextBox CreateCheckTextBox(string accessibleName, string textValue, string caption, int width, bool visible, bool carry = false)
        {
            var a = new V6CheckTextBox
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                //LimitCharacters = limit,
                GrayText = caption,
                TextValue = textValue,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
            
            return a;
        }
        public static V6DateTimePicker CreateDateTimePick(string accessibleName, string caption, int width, bool visible, bool carry = false)
        {
            return new V6DateTimePicker
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                //Text = caption,
                TextTitle = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }
        public static V6DateTimeColor CreateDateTimeColor(string accessibleName, string caption, int width, bool visible, bool carry = false)
        {
            return new V6DateTimeColor
            {
                Name = accessibleName,
                AccessibleName = accessibleName,
                Carry = carry,
                GrayText = caption,
                Width = width,
                Visible = visible,
                Tag = visible ? null : "hide"
            };
        }

        private static List<string> GetFormAccessibleDescriptions(Control control)
        {
            var result = new List<string>();
            if(!string.IsNullOrEmpty(control.AccessibleDescription))
                result.Add(control.AccessibleDescription);
            var menuControl = control as MenuControl;
            if (menuControl != null)
            {
                foreach (MenuButton b in menuControl.Buttons)
                {
                    if (!string.IsNullOrEmpty(b.AccessibleDescription))
                    {
                        result.Add(b.AccessibleDescription);
                    }
                }
            }

            if (control.ContextMenuStrip != null)
            {
                foreach (ToolStripMenuItem menu_item in control.ContextMenuStrip.Items)
                {
                    if (!string.IsNullOrEmpty(menu_item.AccessibleDescription))
                    {
                        result.Add(menu_item.AccessibleDescription);
                    }
                }
            }
            if (control is DropDownButton)
            {
                var button = control as DropDownButton;
                if (button.Menu != null)
                {
                    foreach (ToolStripMenuItem menu_item in button.Menu.Items)
                    {
                        if (!string.IsNullOrEmpty(menu_item.AccessibleDescription))
                        {
                            result.Add(menu_item.AccessibleDescription);
                        }
                    }
                }
            }
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    result.AddRange(GetFormAccessibleDescriptions(c));
                }
            }
            else if (control is StatusStrip)
            {
                var sts = (StatusStrip)control;
                foreach (ToolStripItem item in sts.Items)
                {
                    if (!string.IsNullOrEmpty(item.AccessibleDescription))
                    {
                        result.Add(item.AccessibleDescription);
                    }
                }
            }
            return result;
        }

        #endregion

        

        #region ==== Show...Message() ====

        /// <summary>
        /// Hiển thị Ucontrol lên form. Có xác nhận đóng khi nhấn X
        /// </summary>
        /// <param name="control"></param>
        /// <param name="owner">Form chủ, không có để null</param>
        /// <param name="title"></param>
        /// <param name="fullScreen"></param>
        /// <param name="dialog"></param>
        public static void ShowToForm(UserControl control, IWin32Window owner, string title = "Form", bool fullScreen = false, bool dialog = true)
        {
            control.ShowToForm(owner, title, fullScreen, dialog);
        }

        public static DialogResult ShowMessage(string message, IWin32Window owner = null)
        {
            return V6Message.Show(message, 0, owner);
        }

        public static DialogResult ShowMessage(string message, int showTime, IWin32Window owner = null)
        {
            return V6Message.Show(message, showTime, owner);
        }

        public static DialogResult ShowErrorMessage(string message, IWin32Window owner = null, int showTime = 0)
        {
            return ShowErrorMessage(message, null, owner, showTime);
        }
        public static DialogResult ShowErrorMessage(string message, string caption, IWin32Window owner = null, int showTime = 0)
        {
            try
            {
                AddLastError(message);
                Logger.WriteToLog(V6Login.ClientName + " " + message);
            }
            catch
            {
                // ignored
            }
            string message1;
            if (message.Contains("Timeout expired."))
            {
                message1 = V6Setting.IsVietnamese
                    ? "Kết nối tới máy chủ dữ liệu bị gián đoạn."
                    : "Connection to the data server is interrupted.";
            }
            else
            {
                message1 = message;
            }
            return V6Message.Show(message1, caption, showTime, MessageBoxButtons.OK, MessageBoxIcon.Error, owner);
        }

        public static DialogResult ShowErrorException(string address, Exception ex, IWin32Window owner = null, int showTime = 0)
        {
            return ShowErrorException(address, ex, null, owner, showTime);
        }
        public static DialogResult ShowErrorException(string address, Exception ex, string caption, IWin32Window owner = null, int showTime = 0)
        {
            try
            {
                var log = address
                    + "\r\nExceptionType: " + ex.GetType()
                    + "\r\nExceptionMessage: " + ex.Message
                    + "\r\nStackTrace: " + ex.StackTrace;
                AddLastError(log);
                Logger.WriteToLog(V6Login.ClientName + " " + log);
            }
            catch
            {
                // ignored
            }

            string message;
            if (ex.Message.Contains("Timeout expired."))
            {
                message = address + ": " + (V6Setting.IsVietnamese
                    ? "Kết nối tới máy chủ dữ liệu bị gián đoạn."
                    : "Connection to the data server is interrupted.");
            }
            else
            {
                message = address + ": " + ex.Message;
            }
            return V6Message.Show(message, caption, showTime, MessageBoxButtons.OK, MessageBoxIcon.Error, owner);
        }

        /// <summary>
        /// Hiển thị hộp thoại hỏi Yes/No
        /// </summary>
        /// <param name="message">Thông báo hiển thị.</param>
        /// <param name="caption">Tiêu đề hiển thị trên hộp thông báo.</param>
        /// <param name="dbutton">Nút được chọn sẵn (theo thứ tự 1,2,3).</param>
        /// <param name="owner">Control, nơi thông điệp phát sinh.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmMessage(string message, string caption = null, int dbutton = 0, IWin32Window owner = null)
        {
            return V6Message.Show(message, caption, 0, MessageBoxButtons.YesNo, MessageBoxIcon.Question, dbutton, owner);
        }

        /// <summary>
        /// Yes/No/Cancel
        /// </summary>
        /// <param name="message">Đoạn thông điệp chính</param>
        /// <param name="owner">Control, nơi thông điệp phát sinh.</param>
        /// <returns></returns>
        public static DialogResult ShowConfirmCancelMessage(string message, IWin32Window owner = null)
        {
            return V6Message.Show(message, null, 0, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 3, owner);
        }

        public static void ShowWarningMessage(string message, IWin32Window owner = null)
        {
            V6Message.Show(message, V6Setting.Language == "V" ? "Cảnh báo!" : "Warning!", 0, MessageBoxButtons.OK, MessageBoxIcon.Warning, owner);
        }

        public static void NoRightWarning(IWin32Window owner = null)
        {
            ShowWarningMessage(V6Text.NoRight, owner);
        }

        public static DialogResult ShowInfoMessage(string message, IWin32Window owner = null)
        {
            return V6Message.Show(message, V6Setting.Language == "V" ? "Thông báo" : "Information:", 0, MessageBoxButtons.OK, MessageBoxIcon.Information, owner);
        }

        /// <summary>
        /// Hiển thị thông báo có icon info.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="showTime">Thời gian hiển thị tính bằng phần trăm giây. 100 = 1s</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult ShowInfoMessage(string message, int showTime, IWin32Window owner = null)
        {
            return V6Message.Show(message, V6Setting.Language == "V" ? "Thông báo" : "Information:", showTime, MessageBoxButtons.OK, MessageBoxIcon.Information, owner);
        }
        #endregion showmessage

        /// <summary>
        /// Hiển thị Ucontrol lên form. Có xác nhận đóng khi nhấn X
        /// </summary>
        /// <param name="control">Đối tượng hiển thị trên form.</param>
        /// <param name="owner">Form chủ, không có để null.</param>
        /// <param name="title">Tiêu đề trên form.</param>
        /// <param name="fullScreen">Mở rộng form khi hiển thị</param>
        /// <param name="dialog">Hiển thị form kiểu dialog.</param>
        /// <param name="closeConfirm">Xác nhận khi đóng form.</param>
        public static DialogResult ShowToFormFull(UserControl control, IWin32Window owner, string title = "Form",
            bool fullScreen = false, bool dialog = true, bool closeConfirm = true)
        {
            try
            {
                var f = new V6Form
                {
                    Text = title,
                    AutoSize = true,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    Size = new Size(800, 600)
                };
                if (fullScreen) f.WindowState = FormWindowState.Maximized;
                if (closeConfirm)
                    f.FormClosing += (sender, e) =>
                    {
                        if (!f.IsDisposed && f.ShowConfirmMessage(V6Text.CloseConfirm) != DialogResult.Yes)
                        {
                            e.Cancel = true;
                        }
                    };

                f.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.Disposed += delegate
                {
                    if (!f.IsDisposed) f.Dispose();
                };
                f.KeyPreview = true;
                f.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        f.Close();
                    }
                };

                if (dialog)
                {
                    return f.ShowDialog(owner);
                }
                else
                {
                    f.Show(owner);
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException) return DialogResult.Abort;
                control.ShowErrorMessage("UserControl ShowToForm: " + ex.Message);
            }
            //Giả không có result (Abort ít dùng).
            return DialogResult.Abort;
        }


        public static void WriteExLog(string address, Exception ex, string logFile = "V6Log")
        {
            AddLastError(address + " " + ex.Message);
            address = V6Login.ClientName + " " + address;
            Logger.WriteExLog(address, ex, LastActionListString, logFile);
        }
        
        public static void WriteToLog(string address, string message, string logFile = "V6Log")
        {
            AddLastError(address + " " + message);
            address = V6Login.ClientName + " " + address;
            var log = address
                + "\r\nException: " + message
                + "\r\nLastAction: " + LastActionListString;
            Logger.WriteToLog(log, logFile);
        }

        #region ==== Print RptReport ====

        /// <summary>
        /// Chọn máy in và in, In xong trả về tên máy đã in.
        /// </summary>
        /// <param name="owner">Form hoặc control gọi hàm này.</param>
        /// <param name="rpDoc">Đổi tượng rpt cần in.</param>
        /// <param name="printerName">Máy in chọn sẵn.</param>
        /// <returns>Tên máy in đã chọn in.</returns>
        public static string PrintRpt(IWin32Window owner, ReportDocument rpDoc, string printerName)
        {
            PrintDialog pt = new PrintDialog();
            pt.PrinterSettings.PrinterName = printerName;
            pt.AllowPrintToFile = false;
            pt.AllowCurrentPage = true;
            pt.AllowSomePages = true;
            pt.UseEXDialog = true; //Fix win7
            if (pt.ShowDialog(owner) == DialogResult.OK)
            {
                bool is_printed = PrintRptToPrinter(rpDoc,
                    pt.PrinterSettings.PrinterName,
                    pt.PrinterSettings.Copies,
                    pt.PrinterSettings.FromPage,
                    pt.PrinterSettings.ToPage);
                if (is_printed) return pt.PrinterSettings.PrinterName;
            }
            return null;
        }

        public static bool PrintRptToPrinter(ReportDocument rpDoc, string printerName, int copies, int startPage = 0, int endPage = 0)
        {
            //rpDoc.PrintOptions.PrinterName = printerName; Câu này không có tác dụng.
            var _oldDefaultPrinter = PrinterStatus.GetDefaultPrinterName();
            try
            {
                bool printerOnline = PrinterStatus.CheckPrinterOnline(printerName);
                var setPrinterOk = PrinterStatus.SetDefaultPrinter(printerName);
                var printerError = string.Compare("Error", PrinterStatus.getDefaultPrinterProperties("Status"), StringComparison.OrdinalIgnoreCase) == 0;

                if (setPrinterOk && printerOnline && !printerError)
                {
                    rpDoc.PrintToPrinter(copies, false, startPage, endPage);
                    ShowMainMessage("Đã gửi in.");
                    return true;
                }
            }
            catch (Exception)
            {
                PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                throw;
            }
            PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
            return false;
        }
        #endregion print rpt

        public static void CallExe(string exe)
        {
            if (string.IsNullOrEmpty(exe)) return;
            if (File.Exists(exe))
            {
                if (!string.IsNullOrEmpty(exe) && exe.ToUpper().EndsWith(".EXE"))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = exe,
                        Arguments = string.Format("{0} {1}", V6Login.UserName, V6Login.LoginToken)
                    };
                    Process.Start(psi);
                }
            }
            else
            {
                ShowWarningMessage("Không tồn tại tập tin:\n" + exe);
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu có tồn tại trong danh mục hay không. Nếu không tồn tại sẽ chuyển màu đỏ.
        /// </summary>
        /// <param name="dataGridView1">GridView đang chứa dữ liệu kiểm tra</param>
        /// <param name="dataFields">Trường lấy dữ liệu kiểm tra</param>
        /// <param name="checkFields">Trường trong bảng cần kiểm tra</param>
        /// <param name="checkTables">Các bảng cần kiểm tra</param>
        /// <returns></returns>
        public static bool CheckDataInGridView(V6ColorDataGridView dataGridView1, string[] dataFields, string[] checkFields, string[] checkTables)
        {
            var check = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var max = dataFields.Length;
                if (checkFields.Length < max) max = checkFields.Length;
                if (checkTables.Length < max) max = checkTables.Length;
                for (int i = 0; i < max; i++)
                {
                    var table = checkTables[i];
                    var dataField = dataFields[i];
                    var checkField = checkFields[i];
                    var value = row.Cells[dataField].Value.ToString().Trim();
                    var notexist = V6BusinessHelper.IsValidOneCode_Full(table, 1, checkField, value, value);
                    if (notexist)
                    {
                        check = false;
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            return check;
        }

        /// <summary>
        /// Tag= keyString / click:keyString / focus:keyString
        /// Nhiều Tag cách nhau bằng ;
        /// </summary>
        /// <param name="control"></param>
        /// <param name="keyString">KeysData.ToString()</param>
        /// <returns></returns>
        public static bool ClickByTag(Control control, string keyString)
        {
            if (control.Enabled && control.Visible && control.Tag != null)
            {
                var tagString = control.Tag.ToString();
                if ((";" + tagString + ";").Contains(";" + keyString + ";")
                    || (";" + tagString + ";").Contains(";click:" + keyString + ";"))
                {
                    //Click
                    var button = control as Button;
                    if (button != null) button.PerformClick();
                    var label = control as V6Label;
                    if (label != null) label.PerformClick();
                    var text = control as TextBox;
                    if (text != null) text.Focus();
                    return true;
                }
                else if ((";" + tagString + ";").Contains(";focus:" + keyString + ";"))
                {
                    control.Focus();
                    return true;
                }
            }
            
            return control.Controls.Count > 0 && control.Controls.Cast<Control>().Any(c => ClickByTag(c, keyString));
        }

        public static void CreateGridViewStruct(DataGridView grid, V6TableStruct tableStruct)
        {
            foreach (KeyValuePair<string, V6ColumnStruct> item in tableStruct)
            {
                V6ColumnStruct columnStruct = item.Value;
                if (!grid.Columns.Contains(columnStruct.ColumnName))
                {
                    if (columnStruct.DataType == typeof (bool))
                    {
                        var gc = new DataGridViewCheckBoxColumn
                        {
                            Name = columnStruct.ColumnName,
                            DataPropertyName = columnStruct.ColumnName,
                            HeaderText = CorpLan2.GetFieldHeader(columnStruct.ColumnName),
                            ValueType = columnStruct.DataType
                        };
                        //var gc = new SpecialColumnHeader();
                        //gc.HeaderCell = new ComboNumericHeaderCell();

                        //Sự kiện


                        grid.Columns.Add(gc);
                    }
                    else
                    {


                        var gc = new DataGridViewTextBoxColumn
                        {
                            Name = columnStruct.ColumnName,
                            DataPropertyName = columnStruct.ColumnName,
                            HeaderText = CorpLan2.GetFieldHeader(columnStruct.ColumnName),
                            ValueType = columnStruct.DataType
                        };
                        //var gc = new SpecialColumnHeader();
                        //gc.HeaderCell = new ComboNumericHeaderCell();
                        if (columnStruct.MaxLength > 0)
                            gc.MaxInputLength = columnStruct.MaxLength;

                        //Sự kiện


                        grid.Columns.Add(gc);
                    }
                    //var gc2 = new DataGridViewTextBoxColumn()
                }
            }
        }

        /// <summary>
        /// Click vào control Button hoặc V6lable dựa vào Tag vd: Tag="F3"
        /// </summary>
        /// <param name="container"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public static bool DoKeyCommand(Control container, Keys keyData)
        {
            try
            {
                Hcheck(keyData);
                if (keyData == (Keys.Control | Keys.Alt | Keys.E))
                {
                    string file = ExportFormInfo(container);
                    SetStatusText("Export form info completed. " + file);
                }
                string keyString = keyData.ToString();
                //SetStatusText(keyString);//Test !!!!
                return ClickByTag(container, keyString);
            }
            catch
            {
                return false;
            }
        }

        private static string ExportFormInfo(Control form)
        {
            try
            {
                var data = new DataTable();
                data.Columns.Add("Name");
                data.Columns.Add("AccessibleName");
                data.Columns.Add("AccessibleDescription");
                data.Columns.Add("Tag");
                var ControlList = GetAllControls(form);
                foreach (Control c in ControlList)
                {
                    var newRow = data.NewRow();
                    newRow["Name"] = c.Name;
                    newRow["AccessibleName"] = c.AccessibleName;
                    newRow["AccessibleDescription"] = c.AccessibleDescription;
                    newRow["Tag"] = c.Tag;
                    data.Rows.Add(newRow);
                }
                string file = form.GetType().ToString();
                file = Path.GetFullPath(file);
                ExportData.ToTextFile(data, file + ".txt");
                ExportData.ToExcel(data, file + ".xls", form.Name);
                return file;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType + ".ExportFormInfo", ex, LastActionListString);
            }
            return null;
        }


        public static void EnableControls(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Enabled = true;
            }
        }
        public static void DisableControls(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Enabled = false;
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static Type TypeFromSqlType(string sqlType)
        {
            switch (sqlType)
            {
                case "date":
                case "smalldatetime":
                case "datetime":
                    return typeof(DateTime);
                case "bigint":
                    return typeof(long);
                case "numeric":
                    return typeof(decimal);
                case "bit":
                    return typeof(bool);
                case "smallint":
                    return typeof(short);
                case "decimal":
                    return typeof(decimal);
                case "smallmoney":
                    return typeof(decimal);
                case "int":
                    return typeof(int);
                case "tinyint":
                    return typeof(byte);
                case "money":
                    return typeof(decimal);
                default:
                    return typeof(string);
            }
        }

        /// <summary>
        /// Sắp xếp thứ tự cột cho DataGridView theo danh sách gửi vào. Hiện nó ra luôn nếu ẩn.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="i">Vị trí bắt đầu sắp xếp, các cột trước đó để nguyên</param>
        /// <param name="orderList">Danh sách các cột đã sắp xếp</param>
        public static void ReorderDataGridViewColumns(V6ColorDataGridView dgv, string[] orderList, int i=-1)
        {
            dgv.AutoGenerateColumns = dgv.Columns.Count == 0;
            try
            {
                var start = i;
                if (start == -1)
                    start = dgv.Columns.Cast<DataGridViewColumn>().Count(column => column.Frozen);

                foreach (string field in orderList)
                {
                    var column = dgv.Columns[field];
                    if (column != null && !column.Frozen)
                    {
                        column.Visible = true;
                        column.DisplayIndex = start;
                        start++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(V6Login.ClientName + " " + MethodBase.GetCurrentMethod().DeclaringType + ".ReorderDataGridViewColumns", ex, LastActionListString);
            }
            //dgv.AutoGenerateColumns = true;
        }

        /// <summary>
        /// Sắp xếp thứ tự cột cho DataGridView theo danh sách gửi vào. Hiện nó ra luôn nếu ẩn.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="i">Vị trí bắt đầu sắp xếp, các cột trước đó để nguyên, -1 mặc định để nguyên các cột dính.</param>
        /// <param name="orderList">Danh sách các cột đã sắp xếp</param>
        public static void ReorderDataGridViewColumns(V6ColorDataGridView dgv, List<string> orderList, int i = -1)
        {
            ReorderDataGridViewColumns(dgv, orderList.ToArray(), i);
        }


        #region ==== EXPORT EXCEL TEMPLATE ====

        /// <summary>
        /// Xuất Excel ra file theo file mẫu.
        /// </summary>
        /// <param name="owner">Form hoặc control chủ đang gọi hàm này.</param>
        /// <param name="data">Dữ liệu xuất ra.</param>
        /// <param name="tbl2">Dữ liệu phụ khi type = 2.</param>
        /// <param name="ReportDocumentParameters">Các tham số gửi vào report, dùng để xuất lên các vị trí được cấu hình xml.</param>
        /// <param name="MAU">key albc</param>
        /// <param name="LAN">key albc</param>
        /// <param name="ReportFile">key albc</param>
        /// <param name="ExcelTemplateFileFull">File excel mẫu.</param>
        /// <param name="defaultSaveName">Tên file lưu gợi ý.</param>
        public static void ExportExcelTemplate_ChooseFile(IWin32Window owner, DataTable data, DataTable tbl2,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string defaultSaveName)
        {
            if (data == null)
            {
                //ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    Title = "Xuất excel.",
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    ExportExcelTemplate(owner, data, tbl2,
                        ReportDocumentParameters, MAU, LAN,
                        ReportFile, ExcelTemplateFileFull, save.FileName);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
        }
        
        
        public static void ExportExcelTemplate(IWin32Window owner, DataTable data, DataTable tbl2,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string saveFileName)
        {
            ExportExcelTemplate_owner = owner;
            ExportExcelTemplate_data = data;
            ExportExcelTemplate_tbl2 = tbl2;
            ExportExcelTemplate_ReportDocumentParameters = ReportDocumentParameters;
            ExportExcelTemplate_MAU = MAU;
            ExportExcelTemplate_LAN = LAN;
            ExportExcelTemplate_ReportFile = ReportFile;
            ExportExcelTemplate_ExcelTemplateFileFull = ExcelTemplateFileFull;
            ExportExcelTemplate_saveFileName = saveFileName;
            ExportExcelTemplate_running = false;
            var thread1 = new Thread(ExportExcelTemplate_Thread);
            ExportExcelTemplate_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            time_count1 = 0;
            timer.Start();
        }

        static void timer1_Tick(object sender, EventArgs e)
        {
            if (ExportExcelTemplate_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_count1);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_count1);
            }
        }

        private static IWin32Window ExportExcelTemplate_owner;
        private static DataTable ExportExcelTemplate_data;
        private static DataTable ExportExcelTemplate_tbl2;
        private static IDictionary<string, object> ExportExcelTemplate_ReportDocumentParameters;
        private static string ExportExcelTemplate_MAU;
        private static string ExportExcelTemplate_LAN;
        private static string ExportExcelTemplate_ReportFile;
        private static string ExportExcelTemplate_ExcelTemplateFileFull;
        private static string ExportExcelTemplate_saveFileName;
        private static bool ExportExcelTemplate_running;
        private static int time_count1;
        private static void ExportExcelTemplate_Thread()
        {
            if (ExportExcelTemplate_data == null)
            {
                ShowMainMessage(V6Text.ExportFail + "\n" + V6Text.NoData);
                return;
            }
            try
            {
                try
                {
                    var albc_row = Albc.GetRow(ExportExcelTemplate_MAU, ExportExcelTemplate_LAN, ExportExcelTemplate_ReportFile);
                    if (albc_row != null)
                    {
                        var firstCell = "A4"; //auto
                        bool drawLine = true, insertRow = true;
                        var xlm = albc_row["EXCEL2"].ToString().Trim();
                        var excelColumns = albc_row["EXCEL1"].ToString().Trim();
                        DataSet ds = new DataSet();
                        StringReader sReader = new StringReader(xlm);
                        ds.ReadXml(sReader);

                        var parameters = new SortedDictionary<string, object>();
                        if (ds.Tables.Count > 0)
                        {
                            var paramTable = ds.Tables[0];
                            foreach (DataRow row in paramTable.Rows)
                            {
                                var type = row["type"].ToString().Trim();
                                var KEY = row["key"].ToString().Trim().ToUpper();
                                var content = row["content"].ToString().Trim();
                                if (type == "0")
                                {
                                    if (KEY == "FIRSTCELL")
                                        firstCell = content;
                                    else if (KEY == "DRAWLINE")
                                        drawLine = content == "1";
                                    else if (KEY == "INSERTROW")
                                        insertRow = content == "1";
                                }
                                else if (type == "1") //Lay value trong parameter
                                {
                                    if (ExportExcelTemplate_ReportDocumentParameters == null) continue;
                                    // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                                    // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                                    // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");
                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                            if (ExportExcelTemplate_ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(
                                                        ExportExcelTemplate_ReportDocumentParameters[MATCH_KEY]));
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        var P_KEY = content.ToUpper();
                                        if (ExportExcelTemplate_ReportDocumentParameters.ContainsKey(P_KEY))
                                        {
                                            parameters.Add(KEY, ExportExcelTemplate_ReportDocumentParameters[P_KEY]);
                                        }
                                    }
                                }
                                else if (type == "2" && ExportExcelTemplate_tbl2 != null && ExportExcelTemplate_tbl2.Rows.Count > 0) //Lay value trong tbl2
                                {
                                    var tbl2_row = ExportExcelTemplate_tbl2.Rows[0];

                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");
                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var matchKey = match.Groups[1].Value;
                                            if (ExportExcelTemplate_tbl2.Columns.Contains(matchKey))
                                            {
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(tbl2_row[matchKey]));
                                            }
                                        }
                                        if (parameters.ContainsKey(KEY))
                                        {
                                            ShowWarningMessage("Trùng khóa cấu hình excel: key=" + KEY);
                                            continue;
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        if (ExportExcelTemplate_tbl2.Columns.Contains(content))
                                        {
                                            parameters.Add(KEY, tbl2_row[content]);
                                        }
                                    }
                                }
                                else if (type == "3")//V6Soft.V6SoftValue
                                {
                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");

                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                            if (V6Soft.V6SoftValue.ContainsKey(MATCH_KEY))
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(V6Soft.V6SoftValue[MATCH_KEY]));
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        var P_KEY = content.ToUpper();
                                        if (V6Soft.V6SoftValue.ContainsKey(P_KEY))
                                        {
                                            parameters.Add(KEY, V6Soft.V6SoftValue[P_KEY]);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Không có thông tin xml
                        }

                        if (ExportData.ToExcelTemplate(
                            ExportExcelTemplate_ExcelTemplateFileFull, ExportExcelTemplate_data, ExportExcelTemplate_saveFileName, firstCell,
                            excelColumns.Replace("[", "").Replace("]", "").Split(excelColumns.Contains(";") ? ';' : ','),
                            parameters, V6Setting.V6_number_format_info,
                            insertRow, drawLine))
                        {
                            if (V6Options.AutoOpenExcel)
                            {
                                OpenFileProcess(ExportExcelTemplate_saveFileName);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                        }
                        else
                        {
                            ShowInfoMessage(V6Text.ExportFail + ExportData.Message);
                        }
                    }
                    else
                    {
                        ShowWarningMessage("Không lấy được thông tin cấu hình!");
                    }
                }
                catch (Exception ex)
                {
                    var methodInfo = MethodBase.GetCurrentMethod();
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                ShowErrorException(address, ex);
            }
            ExportExcelTemplate_running = false;
        }

        #endregion EXPORT EXCEL TEMPLATE


        #region ==== EXPORT EXCEL TEMPLATE D ====

        /// <summary>
        /// Xuất Excel ra file theo file mẫu Động.
        /// </summary>
        /// <param name="owner">Form hoặc control chủ đang gọi hàm này.</param>
        /// <param name="data">Dữ liệu xuất ra.</param>
        /// <param name="tbl2">Dữ liệu phụ khi type = 2.</param>
        /// <param name="MODE">"V" ? "EXCEL2_VIEW" : "EXCEL2"</param>
        /// <param name="ReportDocumentParameters">Các tham số gửi vào report, dùng để xuất lên các vị trí được cấu hình xml.</param>
        /// <param name="MAU">key albc</param>
        /// <param name="LAN">key albc</param>
        /// <param name="ReportFile">key albc</param>
        /// <param name="ExcelTemplateFileFull">File excel mẫu.</param>
        /// <param name="defaultSaveName">Tên file lưu gợi ý.</param>
        /// <param name="excelColumns">Các cột ,,</param>
        /// <param name="excelHeaders">Điền tên các cột ,,</param>
        public static void ExportExcelTemplateD(IWin32Window owner, DataTable data, DataTable tbl2, string MODE,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string defaultSaveName, string excelColumns, string excelHeaders)
        {
            ExportExcelTemplateD_owner = owner;
            ExportExcelTemplateD_data = data;
            ExportExcelTemplateD_tbl2 = tbl2;
            ExportExcelTemplateD_MODE = MODE;
            ExportExcelTemplateD_ReportDocumentParameters = ReportDocumentParameters;
            ExportExcelTemplateD_MAU = MAU;
            ExportExcelTemplateD_LAN = LAN;
            ExportExcelTemplateD_ReportFile = ReportFile;
            ExportExcelTemplateD_ExcelTemplateFileFull = ExcelTemplateFileFull;
            ExportExcelTemplateD_defaultSaveName = defaultSaveName;
            ExportExcelTemplateD_excelColumns = excelColumns;
            ExportExcelTemplateD_excelHeaders = excelHeaders;
            var thread1 = new Thread(ExportExcelTemplateD_Thread);
            ExportExcelTemplateD_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer3_Tick;
            time_count3 = 0;
            timer.Start();
        }

        static void timer3_Tick(object sender, EventArgs e)
        {
            if (ExportExcelTemplateD_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_count3);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_count3);
            }
        }

        private static IWin32Window ExportExcelTemplateD_owner;
        private static DataTable ExportExcelTemplateD_data;
        private static DataTable ExportExcelTemplateD_tbl2;
        private static string ExportExcelTemplateD_MODE;
        private static IDictionary<string, object> ExportExcelTemplateD_ReportDocumentParameters;
        private static string ExportExcelTemplateD_MAU;
        private static string ExportExcelTemplateD_LAN;
        private static string ExportExcelTemplateD_ReportFile;
        private static string ExportExcelTemplateD_ExcelTemplateFileFull;
        private static string ExportExcelTemplateD_defaultSaveName;
        private static string ExportExcelTemplateD_excelColumns;
        private static string ExportExcelTemplateD_excelHeaders;
        private static bool ExportExcelTemplateD_running;
        private static int time_count3 = 0;

        public static void ExportExcelTemplateD_Thread()
        {
            if (ExportExcelTemplateD_data == null)
            {
                //ShowTopMessage(V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    Title = "Xuất excel.",
                    FileName = ChuyenMaTiengViet.ToUnSign(ExportExcelTemplateD_defaultSaveName)
                };
                if (save.ShowDialog(ExportExcelTemplateD_owner) == DialogResult.OK)
                {
                    try
                    {
                        var albc_row = Albc.GetRow(ExportExcelTemplateD_MAU, ExportExcelTemplateD_LAN, ExportExcelTemplateD_ReportFile);
                        if (albc_row != null)
                        {
                            var firstCell = "A4"; //auto
                            bool drawLine = true, insertRow = true;
                            var xml_field = ExportExcelTemplateD_MODE == "V" ? "EXCEL2_VIEW" : "EXCEL2";
                            var xlm = albc_row[xml_field].ToString().Trim();
                            //var excelColumns = albc_row["EXCEL1"].ToString().Trim();
                            DataSet ds = new DataSet();
                            StringReader sReader = new StringReader(xlm);
                            ds.ReadXml(sReader);

                            var parameters = new SortedDictionary<string, object>();
                            if (ds.Tables.Count > 0)
                            {
                                var paramTable = ds.Tables[0];
                                foreach (DataRow row in paramTable.Rows)
                                {
                                    var type = row["type"].ToString().Trim();
                                    var KEY = row["key"].ToString().Trim().ToUpper();
                                    var content = row["content"].ToString().Trim();
                                    if (type == "0")
                                    {
                                        if (KEY == "FIRSTCELL")
                                            firstCell = content;
                                        else if (KEY == "DRAWLINE")
                                            drawLine = content == "1";
                                        else if (KEY == "INSERTROW")
                                            insertRow = content == "1";
                                    }
                                    else if (type == "1")//Lay value trong parameter
                                    {
                                        // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                                        // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                                        // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                                if (ExportExcelTemplateD_ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(
                                                        match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(ExportExcelTemplateD_ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ExportExcelTemplateD_ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ExportExcelTemplateD_ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && ExportExcelTemplateD_tbl2 != null && ExportExcelTemplateD_tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = ExportExcelTemplateD_tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (ExportExcelTemplateD_tbl2.Columns.Contains(matchKey))
                                                {
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(tbl2_row[matchKey]));

                                                }
                                            }
                                            if (parameters.ContainsKey(KEY))
                                            {
                                                ShowWarningMessage("Trùng khóa cấu hình excel: key=" + KEY);
                                                continue;
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            if (ExportExcelTemplateD_tbl2.Columns.Contains(content))
                                            {
                                                parameters.Add(KEY, tbl2_row[content]);
                                            }
                                        }
                                    }
                                }

                                // Add parameter for Excel Columns Name
                                var firstCellRowIndex = Excel_File.GetExcelRow(firstCell);
                                var firstCellColIndex = Excel_File.GetExcelColumn(firstCell);
                                var headers = ExportExcelTemplateD_excelHeaders.Split(ExportExcelTemplateD_excelHeaders.Contains(";") ? ';' : ',');
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    var key = "" + Excel_File.ToExcelColumnString(firstCellColIndex + i + 1) + firstCellRowIndex;
                                    parameters.Add(key, headers[i]);
                                }

                            }
                            else
                            {
                                //Không có thông tin xml
                            }

                            if (ExportData.ToExcelTemplate(
                                ExportExcelTemplateD_ExcelTemplateFileFull, ExportExcelTemplateD_data, save.FileName, firstCell,
                                ExportExcelTemplateD_excelColumns.Replace("[", "").Replace("]", "").Split(ExportExcelTemplateD_excelColumns.Contains(";") ? ';' : ','),
                                parameters, V6Setting.V6_number_format_info,
                                insertRow, drawLine))
                            {
                                if (V6Options.AutoOpenExcel)
                                {
                                    OpenFileProcess(save.FileName);
                                }
                                else
                                {
                                    ShowInfoMessage(V6Text.ExportFinish, 500);
                                }
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + ExportData.Message);
                            }

                        }
                        else
                        {
                            ShowWarningMessage("Không lấy được thông tin cấu hình!");
                        }
                    }
                    catch (Exception ex)
                    {
                        var methodInfo = MethodBase.GetCurrentMethod();
                        var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                        ShowErrorException(address, ex);
                    }

                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                ShowErrorException(address, ex);
            }
            ExportExcelTemplateD_running = false;
        }

        #endregion EXPORT EXCEL TEMPLATE D


        #region ==== EXPORT EXCEL GROUP ====

        /// <summary>
        /// Xuất Excel ra file theo file mẫu.
        /// </summary>
        /// <param name="owner">Form hoặc control chủ đang gọi hàm này.</param>
        /// <param name="data">Dữ liệu nhóm.</param>
        /// <param name="data2">Dữ liệu chi tiết.</param>
        /// <param name="tbl3">Dữ liệu phụ (thông tin) khi type = 2.</param>
        /// <param name="ReportDocumentParameters">Các tham số gửi vào report, dùng để xuất lên các vị trí được cấu hình xml.</param>
        /// <param name="MAU">key albc</param>
        /// <param name="LAN">key albc</param>
        /// <param name="ReportFile">key albc</param>
        /// <param name="ExcelTemplateFileFull">File excel mẫu.</param>
        /// <param name="defaultSaveName">Tên file lưu gợi ý.</param>
        public static void ExportExcelGroup_ChooseFile(IWin32Window owner, DataTable data, DataTable data2, DataTable tbl3,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string defaultSaveName)
        {
            if (data == null)
            {
                ShowMainMessage(V6Text.ExportFail + "\n" + V6Text.NoData);
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Excel files (*.xls)|*.xls|Xlsx|*.xlsx",
                    Title = "Xuất excel.",
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    ExportExcelGroup(owner, data, data2,
                        tbl3, ReportDocumentParameters,
                        MAU, LAN, ReportFile,
                        ExcelTemplateFileFull, save.FileName);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
        }

        /// <summary>
        /// Xuất Excel có nhóm lên file mẫu
        /// </summary>
        /// <param name="owner">Form hoặc control chủ đang gọi hàm này.</param>
        /// <param name="data">Dữ liệu nhóm.</param>
        /// <param name="data2">Dữ liệu chi tiết.</param>
        /// <param name="tbl3">Dữ liệu phụ (thông tin) khi type = 2.</param>
        /// <param name="ReportDocumentParameters">Các tham số gửi vào report, dùng để xuất lên các vị trí được cấu hình xml.</param>
        /// <param name="MAU">key albc</param>
        /// <param name="LAN">key albc</param>
        /// <param name="ReportFile">key albc</param>
        /// <param name="ExcelTemplateFileFull">File excel mẫu.</param>
        /// <param name="saveFileName">Tên file lưu gợi ý.</param>
        public static void ExportExcelGroup(IWin32Window owner, DataTable data, DataTable data2, DataTable tbl3,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string saveFileName)
        {
            ExportExcelGroup_owner = owner;
            ExportExcelGroup_data = data;
            ExportExcelGroup_data2 = data2;
            ExportExcelGroup_tbl3 = tbl3;
            ExportExcelGroup_ReportDocumentParameters = ReportDocumentParameters;
            ExportExcelGroup_MAU = MAU;
            ExportExcelGroup_LAN = LAN;
            ExportExcelGroup_ReportFile = ReportFile;
            ExportExcelGroup_ExcelTemplateFileFull = ExcelTemplateFileFull;
            ExportExcelGroup_saveFileName = saveFileName;
            var thread1 = new Thread(ExportExcelGroup_Thread);
            ExportExcelGroup_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer2_Tick;
            time_count2 = 0;
            timer.Start();
        }

        static void timer2_Tick(object sender, EventArgs e)
        {
            if (ExportExcelGroup_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_count2);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_count2);
            }
        }

        private static IWin32Window ExportExcelGroup_owner;
        private static DataTable ExportExcelGroup_data;
        private static DataTable ExportExcelGroup_data2;
        private static DataTable ExportExcelGroup_tbl3;
        private static IDictionary<string, object> ExportExcelGroup_ReportDocumentParameters;
        private static string ExportExcelGroup_MAU;
        private static string ExportExcelGroup_LAN;
        private static string ExportExcelGroup_ReportFile;
        private static string ExportExcelGroup_ExcelTemplateFileFull;
        private static string ExportExcelGroup_saveFileName;
        private static bool ExportExcelGroup_running;
        private static int time_count2 = 0;
        private static void ExportExcelGroup_Thread()
        {
            if (ExportExcelGroup_data == null)
            {
                ShowMainMessage(V6Text.ExportFail + "\n" + V6Text.NoData);
                return;
            }
            try
            {
                try
                {
                    var albc_row = Albc.GetRow(ExportExcelGroup_MAU, ExportExcelGroup_LAN, ExportExcelGroup_ReportFile);
                    if (albc_row != null)
                    {
                        var firstCell = "A4"; //auto
                        bool drawLine = true, insertRow = true;
                        var xlm = albc_row["EXCEL2"].ToString().Trim();
                        string excelColumns1 = "", excelColumns2 = "", excelColumns3 = "";
                        string ref_key = "";
                        DataSet ds = new DataSet();
                        StringReader sReader = new StringReader(xlm);
                        ds.ReadXml(sReader);

                        var parameters = new SortedDictionary<string, object>();
                        var column_config = new SortedDictionary<string, string>();
                        if (ds.Tables.Count > 0)
                        {
                            var paramTable = ds.Tables[0];
                            foreach (DataRow row in paramTable.Rows)
                            {
                                var type = row["type"].ToString().Trim();
                                var KEY = row["key"].ToString().Trim().ToUpper();
                                var content = row["content"].ToString().Trim();
                                if (type == "0")
                                {
                                    if (KEY == "FIRSTCELL")
                                        firstCell = content;
                                    else if (KEY == "DRAWLINE")
                                        drawLine = content == "1";
                                    else if (KEY == "INSERTROW")
                                        insertRow = content == "1";
                                }
                                else if (type == "1") //Lay value trong parameter
                                {
                                    if (ExportExcelGroup_ReportDocumentParameters == null) continue;
                                    // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                                    // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                                    // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");
                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                            if (ExportExcelGroup_ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(
                                                        ExportExcelGroup_ReportDocumentParameters[MATCH_KEY]));
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        var P_KEY = content.ToUpper();
                                        if (ExportExcelGroup_ReportDocumentParameters.ContainsKey(P_KEY))
                                        {
                                            parameters.Add(KEY, ExportExcelGroup_ReportDocumentParameters[P_KEY]);
                                        }
                                    }
                                }
                                else if (type == "2" && ExportExcelGroup_tbl3 != null && ExportExcelGroup_tbl3.Rows.Count > 0) //Lay value trong tbl2
                                {
                                    var tbl2_row = ExportExcelGroup_tbl3.Rows[0];

                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");
                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var matchKey = match.Groups[1].Value;
                                            if (ExportExcelGroup_tbl3.Columns.Contains(matchKey))
                                            {
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(tbl2_row[matchKey]));
                                            }
                                        }
                                        if (parameters.ContainsKey(KEY))
                                        {
                                            ShowWarningMessage("Trùng khóa cấu hình excel: key=" + KEY);
                                            continue;
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        if (ExportExcelGroup_tbl3.Columns.Contains(content))
                                        {
                                            parameters.Add(KEY, tbl2_row[content]);
                                        }
                                    }
                                }
                                else if (type == "3")//V6Soft.V6SoftValue
                                {
                                    if (content.Contains("{") && content.Contains("}"))
                                    {
                                        var regex = new Regex("{(.+?)}");

                                        foreach (Match match in regex.Matches(content))
                                        {
                                            var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                            if (V6Soft.V6SoftValue.ContainsKey(MATCH_KEY))
                                                content = content.Replace(match.Groups[0].Value,
                                                    ObjectAndString.ObjectToString(V6Soft.V6SoftValue[MATCH_KEY]));
                                        }
                                        parameters.Add(KEY, content);
                                    }
                                    else
                                    {
                                        var P_KEY = content.ToUpper();
                                        if (V6Soft.V6SoftValue.ContainsKey(P_KEY))
                                        {
                                            parameters.Add(KEY, V6Soft.V6SoftValue[P_KEY]);
                                        }
                                    }
                                }
                                else if (type == "E")
                                {
                                    if (KEY.StartsWith("COLUMNS"))
                                    {
                                        column_config[KEY] = content;
                                    }
                                    else if (KEY == "COLUMNS1")      // Các cột bảng chính
                                    {
                                        excelColumns1 = content;
                                    }
                                    else if (KEY == "COLUMNS2") // Các cột chi tiết
                                    {
                                        excelColumns2 = content;
                                    }
                                    else if (KEY == "COLUMNS3") // Các cột tổng trong bảng chính.
                                    {
                                        excelColumns3 = content;
                                    }
                                    else if (KEY == "REF_KEY")     // Các trường khóa liên kết.
                                    {
                                        ref_key = content;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Không có thông tin xml
                        }

                        if (string.IsNullOrEmpty(ref_key))
                        {
                            ShowWarningMessage("REF_KEY");
                            return;
                        }

                        if (ExportData.ToExcelTemplateGroup(
                            ExportExcelGroup_ExcelTemplateFileFull, ExportExcelGroup_data, ExportExcelGroup_data2, ObjectAndString.SplitString(ref_key), ExportExcelGroup_saveFileName, firstCell,
                            column_config,
                            null,//Headers
                            parameters, V6Setting.V6_number_format_info,
                            insertRow, drawLine))
                        {
                            if (V6Options.AutoOpenExcel)
                            {
                                OpenFileProcess(ExportExcelGroup_saveFileName);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                        }
                        else
                        {
                            ShowInfoMessage(V6Text.ExportFail + ExportData.Message);
                        }
                    }
                    else
                    {
                        ShowWarningMessage("Không lấy được thông tin cấu hình!");
                    }
                }
                catch (Exception ex)
                {
                    var methodInfo = MethodBase.GetCurrentMethod();
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                ShowErrorException(address, ex);
            }
            ExportExcelGroup_running = false;
        }

        #endregion EXPORT EXCEL GROUP


        #region ==== ExportExcelTemplateHTKK ====

        public static void ExportExcelTemplateHTKK(DataTable data, DataTable tbl2,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string excelTemplateFile, string saveFileName)
        {
            //ExportExcelTemplateHTKK_owner = owner;
            ExportExcelTemplateHTKK_data = data;
            ExportExcelTemplateHTKK_tbl2 = tbl2;
            ExportExcelTemplateHTKK_ReportDocumentParameters = ReportDocumentParameters;
            ExportExcelTemplateHTKK_MAU = MAU;
            ExportExcelTemplateHTKK_LAN = LAN;
            ExportExcelTemplateHTKK_ReportFile = ReportFile;
            ExportExcelTemplateHTKK_excelTemplateFile = excelTemplateFile;
            ExportExcelTemplateHTKK_saveFileName = saveFileName;
            ExportExcelTemplateHTKK_running = false;
            var thread1 = new Thread(ExportExcelTemplateHTKK_Thread);
            ExportExcelTemplateHTKK_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timerHTKK_Tick;
            time_countHTKK = 0;
            timer.Start();
        }
        
        static void timerHTKK_Tick(object sender, EventArgs e)
        {
            if (ExportExcelTemplateHTKK_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_countHTKK);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_countHTKK);
            }
        }

        //private static IWin32Window ExportExcelTemplateHTKK_owner;
        private static DataTable ExportExcelTemplateHTKK_data;
        private static DataTable ExportExcelTemplateHTKK_tbl2;
        private static IDictionary<string, object> ExportExcelTemplateHTKK_ReportDocumentParameters;
        private static string ExportExcelTemplateHTKK_MAU;
        private static string ExportExcelTemplateHTKK_LAN;
        private static string ExportExcelTemplateHTKK_ReportFile;
        private static string ExportExcelTemplateHTKK_excelTemplateFile;
        private static string ExportExcelTemplateHTKK_saveFileName;
        private static bool ExportExcelTemplateHTKK_running;
        private static int time_countHTKK;
        private static void ExportExcelTemplateHTKK_Thread()
        {
            if (ExportExcelTemplateHTKK_data == null)
            {
                ShowMainMessage(V6Text.ExportFail + "\n" + V6Text.NoData);
                return;
            }
            if (!File.Exists(ExportExcelTemplateHTKK_excelTemplateFile))
            {
                ShowWarningMessage("Không có file mẫu: " + ExportExcelTemplateHTKK_excelTemplateFile);
                //return;
            }
            try
            {
                SortedDictionary<string, DataTable> datas = new SortedDictionary<string, DataTable>();

                if (!string.IsNullOrEmpty(ExportExcelTemplateHTKK_saveFileName))
                {
                    try
                    {
                        var albc_row = Albc.GetRow(ExportExcelTemplateHTKK_MAU, ExportExcelTemplateHTKK_LAN, ExportExcelTemplateHTKK_ReportFile);
                        if (albc_row != null)
                        {
                            var firstCell = "A4";                           //auto, không dùng, đã có config riêng type 7
                            var excelColumnsHTKK = "";                      //Các cột xuất, config type 0 HTKK
                            var excelColumnsONLINE = "";                    //Các cột xuất, config type 0 ONLINE
                            bool drawLine = true, insertRow = true;         //Mặc định true, không config
                            var xlm = albc_row["EXCEL2"].ToString().Trim();
                            //var excelColumns = albc_row["EXCEL1"].ToString().Trim();//Không dùng
                            DataSet ds = new DataSet();
                            StringReader sReader = new StringReader(xlm);
                            ds.ReadXml(sReader);

                            var parameters = new SortedDictionary<string, object>();
                            if (ds.Tables.Count > 0)
                            {
                                var paramTable = ds.Tables[0];
                                foreach (DataRow row in paramTable.Rows)
                                {
                                    var type = row["type"].ToString().Trim();
                                    var KEY = row["key"].ToString().Trim().ToUpper();
                                    var content = row["content"].ToString().Trim();
                                    if (type == "0")
                                    {
                                        if (KEY == "FIRSTCELL")
                                            firstCell = content;
                                        //else if (KEY == "DRAWLINE")
                                        //    drawLine = content == "1";
                                        //else if (KEY == "INSERTROW")
                                        //    insertRow = content == "1";
                                        else if (KEY == "HTKK")
                                            excelColumnsHTKK = content;
                                        else if (KEY == "ONLINE")
                                            excelColumnsONLINE = content;
                                    }
                                    else if (type == "7")
                                    {
                                        if (KEY == "C1") //B18
                                        {
                                            //var irstCell = content;//B18
                                            //GetData for C1 filter ma_nh = '1'
                                            DataTable newData1 = ExportExcelTemplateHTKK_data.Filter("ma_nh='1'");
                                            datas.Add(content, newData1);
                                        }
                                        else if (KEY == "C2")
                                        {
                                            DataTable newData2 = ExportExcelTemplateHTKK_data.Filter("ma_nh='2'");
                                            datas.Add(content, newData2);
                                        }
                                        else if (KEY == "C3")
                                        {
                                            DataTable newData3 = ExportExcelTemplateHTKK_data.Filter("ma_nh='3'");
                                            datas.Add(content, newData3);
                                        }
                                        else if (KEY == "C4")
                                        {
                                            DataTable newData4 = ExportExcelTemplateHTKK_data.Filter("ma_nh='4'");
                                            datas.Add(content, newData4);
                                        }
                                    }
                                    else if (type == "1")//Lay value trong parameter
                                    {
                                        // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                                        // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                                        // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                                if (ExportExcelTemplateHTKK_ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ExportExcelTemplateHTKK_ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ExportExcelTemplateHTKK_ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ExportExcelTemplateHTKK_ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && ExportExcelTemplateHTKK_tbl2 != null && ExportExcelTemplateHTKK_tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = ExportExcelTemplateHTKK_tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (ExportExcelTemplateHTKK_tbl2.Columns.Contains(matchKey))
                                                {
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(tbl2_row[matchKey]));

                                                }
                                            }
                                            if (parameters.ContainsKey(KEY))
                                            {
                                                ShowWarningMessage("Trùng khóa cấu hình excel: key=" + KEY);
                                                continue;
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            if (ExportExcelTemplateHTKK_tbl2.Columns.Contains(content))
                                            {
                                                parameters.Add(KEY, tbl2_row[content]);
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                //Không có thông tin xml
                            }


                            if (ExportData.ToExcelTemplateHTKK(
                                ExportExcelTemplateHTKK_excelTemplateFile, parameters, datas, excelColumnsHTKK.Split(','),
                                ExportExcelTemplateHTKK_saveFileName, V6Setting.V6_number_format_info, insertRow, drawLine))
                            {
                                if (V6Options.AutoOpenExcel)
                                {
                                    OpenFileProcess(ExportExcelTemplateHTKK_saveFileName);
                                }
                                else
                                {
                                    ShowInfoMessage(V6Text.ExportFinish, 500);
                                }
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + ExportData.Message);
                            }

                        }
                        else
                        {
                            ShowWarningMessage("Không lấy được thông tin cấu hình!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("ExportFail: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!\n" + ex.Message);
            }
            ExportExcelTemplateHTKK_running = false;
        }
        
        #endregion ExportExcelTemplateHTKK


        #region ==== ExportExcelTemplateONLINE ====

        public static void ExportExcelTemplateONLINE(DataTable data, DataTable tbl2,
            IDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string excelTemplateFile, string saveFileName)
        {
            ExportExcelTemplateONLINE_data = data;
            ExportExcelTemplateONLINE_tbl2 = tbl2;
            ExportExcelTemplateONLINE_ReportDocumentParameters = ReportDocumentParameters;
            ExportExcelTemplateONLINE_MAU = MAU;
            ExportExcelTemplateONLINE_LAN = LAN;
            ExportExcelTemplateONLINE_ReportFile = ReportFile;
            ExportExcelTemplateONLINE_excelTemplateFile = excelTemplateFile;
            ExportExcelTemplateONLINE_saveFileName = saveFileName;
            ExportExcelTemplateONLINE_running = false;
            var thread1 = new Thread(ExportExcelTemplateONLINE_Thread);
            ExportExcelTemplateONLINE_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timerONLINE_Tick;
            time_countONLINE = 0;
            timer.Start();
        }

        static void timerONLINE_Tick(object sender, EventArgs e)
        {
            if (ExportExcelTemplateONLINE_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_countONLINE);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_countONLINE);
            }
        }

        private static DataTable ExportExcelTemplateONLINE_data;
        private static DataTable ExportExcelTemplateONLINE_tbl2;
        private static IDictionary<string, object> ExportExcelTemplateONLINE_ReportDocumentParameters;
        private static string ExportExcelTemplateONLINE_MAU;
        private static string ExportExcelTemplateONLINE_LAN;
        private static string ExportExcelTemplateONLINE_ReportFile;
        private static string ExportExcelTemplateONLINE_excelTemplateFile;
        private static string ExportExcelTemplateONLINE_saveFileName;
        private static bool ExportExcelTemplateONLINE_running;
        private static int time_countONLINE;
        private static void ExportExcelTemplateONLINE_Thread()
        {
            if (ExportExcelTemplateONLINE_data == null)
            {
                ShowMainMessage(V6Text.ExportFail + "\n" + V6Text.NoData);
                return;
            }
            if (!File.Exists(ExportExcelTemplateONLINE_excelTemplateFile))
            {
                ShowWarningMessage("Không có file mẫu: " + ExportExcelTemplateONLINE_excelTemplateFile);
                //return;
            }
            try
            {
                SortedDictionary<string, DataTable> datas = new SortedDictionary<string, DataTable>();

                if (!string.IsNullOrEmpty(ExportExcelTemplateONLINE_saveFileName))
                {
                    try
                    {
                        var albc_row = Albc.GetRow(ExportExcelTemplateONLINE_MAU, ExportExcelTemplateONLINE_LAN, ExportExcelTemplateONLINE_ReportFile);
                        if (albc_row != null)
                        {
                            var firstCell = "A4";                           //auto, không dùng, đã có config riêng type 7
                            var excelColumnsHTKK = "";                      //Các cột xuất, config type 0 HTKK
                            var excelColumnsONLINE = "";                    //Các cột xuất, config type 0 ONLINE
                            bool drawLine = true, insertRow = true;         //Mặc định true, không config
                            var xlm = albc_row["EXCEL2"].ToString().Trim();
                            //var excelColumns = albc_row["EXCEL1"].ToString().Trim();//Không dùng
                            DataSet ds = new DataSet();
                            StringReader sReader = new StringReader(xlm);
                            ds.ReadXml(sReader);

                            var parameters = new SortedDictionary<string, object>();
                            if (ds.Tables.Count > 0)
                            {
                                var paramTable = ds.Tables[0];
                                foreach (DataRow row in paramTable.Rows)
                                {
                                    var type = row["type"].ToString().Trim();
                                    var KEY = row["key"].ToString().Trim().ToUpper();
                                    var content = row["content"].ToString().Trim();
                                    if (type == "0")
                                    {
                                        if (KEY == "FIRSTCELL")
                                            firstCell = content;
                                        //else if (KEY == "DRAWLINE")
                                        //    drawLine = content == "1";
                                        //else if (KEY == "INSERTROW")
                                        //    insertRow = content == "1";
                                        else if (KEY == "HTKK")
                                            excelColumnsHTKK = content;
                                        else if (KEY == "ONLINE")
                                            excelColumnsONLINE = content;
                                    }
                                    else if (type == "7")
                                    {
                                        if (KEY == "C1") //B18
                                        {
                                            //var irstCell = content;//B18
                                            //GetData for C1 filter ma_nh = '1'
                                            DataTable newData1 = ExportExcelTemplateONLINE_data.Filter("ma_nh='1'");
                                            datas.Add(content, newData1);
                                        }
                                        else if (KEY == "C2")
                                        {
                                            DataTable newData2 = ExportExcelTemplateONLINE_data.Filter("ma_nh='2'");
                                            datas.Add(content, newData2);
                                        }
                                        else if (KEY == "C3")
                                        {
                                            DataTable newData3 = ExportExcelTemplateONLINE_data.Filter("ma_nh='3'");
                                            datas.Add(content, newData3);
                                        }
                                        else if (KEY == "C4")
                                        {
                                            DataTable newData4 = ExportExcelTemplateONLINE_data.Filter("ma_nh='4'");
                                            datas.Add(content, newData4);
                                        }
                                    }
                                    else if (type == "1")//Lay value trong parameter
                                    {
                                        // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                                        // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                                        // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var MATCH_KEY = match.Groups[1].Value.ToUpper();
                                                if (ExportExcelTemplateONLINE_ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ExportExcelTemplateONLINE_ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ExportExcelTemplateONLINE_ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ExportExcelTemplateONLINE_ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && ExportExcelTemplateONLINE_tbl2 != null && ExportExcelTemplateONLINE_tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = ExportExcelTemplateONLINE_tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (ExportExcelTemplateONLINE_tbl2.Columns.Contains(matchKey))
                                                {
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(tbl2_row[matchKey]));

                                                }
                                            }
                                            if (parameters.ContainsKey(KEY))
                                            {
                                                ShowWarningMessage("Trùng khóa cấu hình excel: key=" + KEY);
                                                continue;
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            if (ExportExcelTemplateONLINE_tbl2.Columns.Contains(content))
                                            {
                                                parameters.Add(KEY, tbl2_row[content]);
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                //Không có thông tin xml
                            }

                            if (ExportData.ToExcelTemplateHTKK(
                                ExportExcelTemplateONLINE_excelTemplateFile, parameters, datas, excelColumnsONLINE.Split(','),
                                ExportExcelTemplateONLINE_saveFileName, V6Setting.V6_number_format_info, insertRow, drawLine))
                            {
                                if (V6Options.AutoOpenExcel)
                                {
                                    OpenFileProcess(ExportExcelTemplateONLINE_saveFileName);
                                }
                                else
                                {
                                    ShowInfoMessage(V6Text.ExportFinish, 500);
                                }
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + ExportData.Message);
                            }
                        }
                        else
                        {
                            ShowWarningMessage("Không lấy được thông tin cấu hình!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("ExportFail: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error!\n" + ex.Message);
            }
            ExportExcelTemplateONLINE_running = false;
        }

        #endregion ExportExcelTemplateONLINE


        /// <summary>
        /// Mở ra 1 bản copy của file mấu
        /// </summary>
        /// <param name="file">Tên file không có đường dẫn. Ví dụ A.xls</param>
        /// <param name="folder">Thư mục con của chương trình. Ví dụ Template</param>
        public static void OpenExcelTemplate(string file, string folder)
        {
            try
            {
                string path1 = V6Login.StartupPath;
                path1 = Path.Combine(path1, folder);
                path1 = Path.Combine(path1, file);
                if (File.Exists(path1))
                {
                    //Copy to tempfolder
                    string path2 = V6ControlsHelper.CreateV6SoftLocalAppDataDirectory();
                    path2 = Path.Combine(path2, file);
                    if (File.Exists(path2)) File.Delete(path2);
                    File.Copy(path1, path2);

                    ProcessStartInfo info1 = new ProcessStartInfo(path2);
                    Process.Start(info1);
                }
                else
                {
                    ShowMainMessage(string.Format("{0} [{1}]", V6Text.NotExist, file));
                }
            }
            catch (Exception ex)
            {
                ShowErrorException("OpenExcelTemplate", ex);
            }
        }


        public static void ExportRptToPdf_As(IWin32Window owner, ReportDocument rpt, string defaultSaveName = "")
        {
            if (rpt == null)
            {
                return;
            }
            try
            {
                var save = new SaveFileDialog
                {
                    Filter = "Pdf files (*.pdf)|*.pdf",
                    Title = "Xuất PDF.",
                    FileName = ChuyenMaTiengViet.ToUnSign(defaultSaveName)
                };
                if (save.ShowDialog(owner) == DialogResult.OK)
                {
                    ExportRptToPdf(owner, rpt, save.FileName);
                }
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
        }

        public static void ExportRptToPdf(IWin32Window owner, ReportDocument rpt, string fileName)
        {
            ExportRptToPdf_owner = owner;
            ExportRptToPdf_rpt = rpt;
            ExportRptToPdf_fileName = fileName;
            var thread1 = new Thread(ExportRptToPdf_Thread);
            ExportRptToPdf_running = true;
            thread1.Start();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer4_Tick;
            time_count4 = 0;
            timer.Start();
        }

        static void timer4_Tick(object sender, EventArgs e)
        {
            if (ExportRptToPdf_running)
            {
                ShowMainMessage(V6Text.Exporting + ++time_count4);
            }
            else
            {
                ((Timer)sender).Stop();
                ShowMainMessage(V6Text.ExportFinish + ++time_count4);
            }
        }

        private static IWin32Window ExportRptToPdf_owner;
        private static ReportDocument ExportRptToPdf_rpt;
        private static string ExportRptToPdf_fileName;
        private static bool ExportRptToPdf_running;
        private static int time_count4 = 0;
        public static void ExportRptToPdf_Thread()
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = ExportRptToPdf_fileName;
                CrExportOptions = ExportRptToPdf_rpt.ExportOptions;

                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                CrExportOptions.FormatOptions = CrFormatTypeOptions;

                ExportRptToPdf_rpt.Export();
                //return true;
            }
            catch (Exception ex)
            {
                var methodInfo = MethodBase.GetCurrentMethod();
                if (methodInfo.DeclaringType != null)
                {
                    var address = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                    ShowErrorException(address, ex);
                }
            }
            //return false;
            ExportRptToPdf_running = false;
        }

        /// <summary>
        /// Sắp xếp thứ tự và gán formatString.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="showColumns">Các cột sẽ hiện ra theo thứ tự</param>
        /// <param name="formatStrings">N2:100:R</param>
        /// <param name="headerStrings"></param>
        public static void FormatGridViewColumnsShowOrder(DataGridView dgv, string[] showColumns, string[] formatStrings, string[] headerStrings)
        {
            try
            {
                if (showColumns == null || showColumns.Length == 0) return;
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    DataGridViewColumn column = dgv.Columns[i];
                    string COL = column.DataPropertyName.ToUpper();
                    bool contains = false;
                    foreach (string show_COL in showColumns)
                    {
                        string SHOW_COL = show_COL.ToUpper();
                        if (COL == SHOW_COL)
                        {
                            contains = true;
                            break;
                        }
                    }

                    if (contains)
                    {
                        column.Visible = true;
                    }
                    else
                    {
                        column.Visible = false;
                    }
                }

                dgv.AutoGenerateColumns = dgv.Columns.Count == 0;

                var index = dgv.Columns.Cast<DataGridViewColumn>().Count(column => column.Frozen && column.Visible);
                for (int i = 0; i < showColumns.Length; i++)
                {
                    int default_width = 100;
                    string default_format_number = "N2";
                    string field = showColumns[i].Trim();
                    var column = dgv.Columns[field];
                    if (column != null)
                    {
                        if (!column.Frozen)
                        {
                            column.Visible = true;
                            column.DisplayIndex = index++;
                        }

                        var dataType = column.ValueType;
                        if (formatStrings.Length > i && !string.IsNullOrEmpty(formatStrings[i]))
                        {
                            var format = formatStrings[i].Trim();
                            var fff = format.Split(':');

                            if (ObjectAndString.IsNumberType(dataType))
                            {
                                if (fff.Length == 1)
                                {
                                    var s = fff[0];
                                    if (s.Length > 0)
                                    {
                                        if (char.IsNumber(s[0]))
                                        {
                                            column.Width = ObjectAndString.ObjectToInt(s);
                                            column.DefaultCellStyle.Format = default_format_number;
                                        }
                                        else
                                        {
                                            column.DefaultCellStyle.Format = s;
                                            column.Width = default_width;
                                        }
                                    }
                                    else
                                    {
                                        column.Width = default_width;
                                        column.DefaultCellStyle.Format = default_format_number;
                                    }
                                }
                                else
                                {
                                    column.DefaultCellStyle.Format = fff[0];
                                    if (fff.Length > 1)
                                        column.Width = ObjectAndString.ObjectToInt(fff[1]);
                                    if (fff.Length > 2)
                                        column.ReadOnly = "R" == fff[2];
                                }
                            }
                            else if (column.ValueType == typeof (DateTime))
                            {
                                if (fff.Length > 0)
                                {
                                    var s = fff[0];
                                    if (s.Length > 0)
                                    {
                                        if(char.IsNumber(s[0])) column.Width = ObjectAndString.ObjectToInt(fff[0]);
                                        else column.Width = ObjectAndString.ObjectToInt(fff[0].Substring(1));
                                    }
                                    else
                                    {
                                        column.Width = default_width;
                                    }
                                }

                                if (fff.Length > 1)
                                {
                                    column.ReadOnly = "R" == fff[1];
                                }
                            }
                            else if (column.ValueType == typeof (string))
                            {
                                if (fff.Length > 0)
                                {
                                    var s = fff[0];
                                    if (s.Length > 0)
                                    {
                                        if (char.IsNumber(s[0])) column.Width = ObjectAndString.ObjectToInt(fff[0]);
                                        else column.Width = ObjectAndString.ObjectToInt(fff[0].Substring(1));
                                    }
                                    else
                                    {
                                        column.Width = default_width;
                                    }
                                }

                                if (fff.Length > 1)
                                {
                                    column.ReadOnly = "R" == fff[1];
                                }
                            }
                        }

                        if (headerStrings != null && headerStrings.Length > i && !string.IsNullOrEmpty(headerStrings[i]))
                        {
                            column.HeaderText = headerStrings[i];
                        }
                    }
                }
                //dgv.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                if (!ex.Message.StartsWith("Unfrozen column"))
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".FormatGridViewColumnsShowOrder", ex);
            }
        }

        /// <summary>
        /// <para>Định dạng gridview, sắp xếp thứ tự cột, gán header, format string.</para>
        /// <para>Nếu không có showFields thì chạy format mặc định của V6ColorDataGridView.</para>
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="showFields">cách nhau bởi (,) hoặc (;)</param>
        /// <param name="formatStrings"></param>
        /// <param name="headerString"></param>
        public static void FormatGridViewAndHeader(DataGridView dgv, string showFields, string formatStrings, string headerString)
        {
            //dgv.DataError += delegate(object sender, DataGridViewDataErrorEventArgs args)
            //{
            //    args.ThrowException = false;
            //};
            if (string.IsNullOrEmpty(showFields))
            {
                return;
            }
            //dgv.AutoGenerateColumns = true;//gây lỗi lặp ở tìm hóa đơn.
            var fieldList = showFields.Replace("[", "").Replace("]", "").Split(showFields.Contains(";") ? ';' : ',');
            var formatList = formatStrings.Split(formatStrings.Contains(";") ? ';' : ',');
            var headerList = headerString.Split(headerString.Contains(";") ? ';' : ',');

            FormatGridViewColumnsShowOrder(dgv, fieldList, formatList, headerList);

            //return;// <<<< Bỏ qua CorpLan2 >>>>
            if (!string.IsNullOrEmpty(headerString)) return;
            var listColumn = (from DataGridViewColumn column in dgv.Columns select column.DataPropertyName).ToList();
            var fieldsHeaderDictionary = CorpLan2.GetFieldsHeader(listColumn, V6Setting.Language);
            
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                var FIELD = column.DataPropertyName.ToUpper();
                if (fieldsHeaderDictionary.ContainsKey(FIELD))
                    column.HeaderText = fieldsHeaderDictionary[FIELD];
            }
            
        }

        /// <summary>
        /// Định dạng lại gridview theo điều kiện.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="field">Trường giá trị điều kiện</param>
        /// <param name="oper">Dấu so sánh</param>
        /// <param name="value">Giá trị điều kiện</param>
        /// <param name="bold">Tô chữ đậm nếu đk đúng</param>
        /// <param name="back_color">Tô màu nền nếu đk đúng.</param>
        /// <param name="color">Màu làm back_color. Nếu back_color=false thì đặt giá trị tùy ý (không ảnh hưởng).</param>
        public static void FormatGridView(V6ColorDataGridView grid, string field, string oper, object value, bool bold, bool back_color, Color color)
        {
            var column = grid.Columns[field];
            if (column != null)
            {
                if (string.IsNullOrEmpty(oper)) oper = "=";
                value = ObjectAndString.ObjectTo(column.ValueType, value);
                if (value is string) value = value.ToString().Trim();
                
                foreach (DataGridViewRow row in grid.Rows)
                {
                    var o1 = row.Cells[field].Value;
                    if (o1 is string) o1 = o1.ToString().Trim();

                    if (ObjectAndString.CheckCondition(o1, oper, value))
                    {
                        if (bold)
                        {
                            row.DefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
                        }
                        if (back_color && color != Color.Empty)
                        {
                            row.DefaultCellStyle.BackColor = color;
                        }
                    }

                    if (grid.Columns.Contains("RGB") && field.ToUpper() == "RGB" && !bold && !back_color)
                    {
                        string colorRGB = row.Cells["RGB"].Value.ToString();
                        if (colorRGB != "")
                        {
                            try
                            {
                                row.DefaultCellStyle.BackColor = ObjectAndString.StringToColor(colorRGB);
                            }
                            catch (Exception ex2)
                            {
                                grid.WriteExLog(grid.Parent.GetType() + ".FormatGridView in V6ControlFormHelper colorRGB: " + colorRGB, ex2);
                            }
                        }
                    }
                }//end for
            }
        }

        public static void FormatGridViewHideColumns(V6ColorDataGridView dgv, string Mact)
        {
            if (!V6Login.IsAdmin)
            {
                dgv.AutoGenerateColumns = dgv.Columns.Count == 0;
                var alctct = V6BusinessHelper.GetAlctCt(Mact);
                if (alctct != null && alctct.Rows.Count > 0)
                {
                    var GRD_HIDE = alctct.Rows[0]["GRD_HIDE"].ToString();
                    dgv.HideColumns(ObjectAndString.SplitString(GRD_HIDE));
                }
            }
        }

        /// <summary>
        /// Sửa lại tiêu đề cột cho dataGridView.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="alctDic">key là FIELD</param>
        /// <param name="maNt"></param>
        /// <param name="maNt0"></param>
        public static void RecaptionDataGridViewColumns(V6ColorDataGridView grid, SortedDictionary<string, DataRow> alctDic, string maNt, string maNt0)
        {
            try
            {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    var FIELD = column.DataPropertyName.ToUpper();
                    if (alctDic.ContainsKey(FIELD))
                    {
                        var row = alctDic[FIELD];
                        var fstatus2 = Convert.ToBoolean(row["fstatus2"]);
                        var fcaption = row[V6Setting.Language == "V" ? "caption" : "caption2"].ToString().Trim();
                        if (FIELD == "PS_CO_NT") fcaption += " " + maNt;
                        if (FIELD == "TIEN_NT") fcaption += " " + maNt;

                        if (FIELD == "PS_CO") fcaption += " " + maNt0;
                        if (FIELD == "TIEN") fcaption += " " + maNt0;

                        if (!fstatus2) column.Visible = false;

                        column.HeaderText = fcaption;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".RecaptionDataGridViewColumns", ex);
            }
        }

        public static void SetGridViewCaption(V6ColorDataGridView grid)
        {
            try
            {
                var listColumn = (from DataGridViewColumn column in grid.Columns select column.DataPropertyName).ToList();

                var FieldsHeaderDictionary = CorpLan2.GetFieldsHeader(listColumn, V6Setting.Language);
                for (int i = 0; i < grid.ColumnCount; i++)
                {
                    var field = grid.Columns[i].DataPropertyName.ToUpper();
                    if (FieldsHeaderDictionary.ContainsKey(field))
                    {
                        grid.Columns[i].HeaderText =
                            FieldsHeaderDictionary[field];
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog("V6ControlFormHelper.SetGridViewCaption", ex);
            }
        }

        public static void UpdateDataRow(DataRow row, IDictionary<string, object> data)
        {
            if (data == null) return;
            if (row == null) return;
            var dt = row.Table;

            foreach (KeyValuePair<string, object> item in data)
            {
                if (dt.Columns.Contains(item.Key))
                {
                    row[item.Key] = ObjectAndString.ObjectTo(dt.Columns[item.Key].DataType, item.Value ?? DBNull.Value);
                }
            }
        }

        /// <summary>
        /// Cập nhập dữ liệu lên một dòng của GridView. Dữ liệu có cột nào thì cập nhập cột đó.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="data"></param>
        public static void UpdateGridViewRow(DataGridViewRow row, IDictionary<string, object> data)
        {
            if (data == null) return;
            if (row == null) return;
            var dataGridView1 = row.DataGridView;

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                var column = dataGridView1.Columns[i];
                var FIELD = column.DataPropertyName.ToUpper();
                if (data.ContainsKey(FIELD))
                {
                    row.Cells[FIELD] .Value = ObjectAndString.ObjectTo(column.ValueType, data[FIELD] ?? DBNull.Value);
                }
            }
        }

        /// <summary>
        /// Cập nhập giá trị của bảng chi tiết (gridview)
        /// </summary>
        /// <param name="table">Bảng cần update</param>
        /// <param name="field">Tên trường</param>
        /// <param name="value">Giá trị - cần gửi đúng kiểu</param>
        public static void UpdateDKlist(DataTable table, string field, object value)
        {
            if (table != null && table.Columns.Contains(field))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    table.Rows[i][field] = value;
                }
            }
        }

        /// <summary>
        /// Cập nhập các giá trị từ AM vào các dòng trong AD theo fields.
        /// </summary>
        /// <param name="dataAM">Thông tin đầu vào</param>
        /// <param name="fields">Các trường cần update thông</param>
        /// <param name="AD">Bảng cần update</param>
        public static void UpdateDKlistAll(IDictionary<string, object> dataAM, string[] fields, DataTable AD)
        {
            if (AD == null) return;
            try
            {
                var use_data = new SortedDictionary<string, object>();

                foreach (string field in fields)
                {
                    var FIELD = field.ToUpper();
                    if (dataAM.ContainsKey(FIELD) && AD.Columns.Contains(FIELD))
                    {
                        use_data.Add(FIELD, dataAM[FIELD]);
                    }
                }

                foreach (DataRow row in AD.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;
                    foreach (KeyValuePair<string, object> item in use_data)
                    {
                        row[item.Key] = ObjectAndString
                            .ObjectTo(AD.Columns[item.Key].DataType, item.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("UpdateDKlistAll: " + ex.Message, "FormManagerHelper");
            }
        }

        /// <summary>
        /// Copy đè dữ liệu từ cột này qua cột kia.
        /// </summary>
        /// <param name="data">Bảng dữ liệu.</param>
        /// <param name="fieldFrom">Cột nguồn.</param>
        /// <param name="fieldTo">Cột đích cần thay đổi.</param>
        public static void UpdateField2Field(DataTable data, string fieldFrom, string fieldTo)
        {
            if (data != null && data.Columns.Contains(fieldFrom) && data.Columns.Contains(fieldTo))
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    data.Rows[i][fieldTo] = data.Rows[i][fieldFrom];
                }
            }
        }

        /// <summary>
        /// Mở một link file.
        /// </summary>
        /// <param name="path"></param>
        internal static void RunProcess(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            if (File.Exists(path))
            {                
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = path,
                };
                Process.Start(psi);
            }
            else
            {
                ShowWarningMessage("Không tồn tại tập tin:\n" + path);
            }
        }

        public static int Copy_Here2Data(V6TableName tableName, V6Mode mode,
            string keyField1, string keyField2, string keyField3,
            string newKey1, string newKey2, string newKey3,
            string oldKey1, string oldKey2, string oldKey3,
            string uid)
        {
            return Copy_Here2Data(tableName.ToString(), mode, keyField1, keyField2, keyField3,
                newKey1, newKey2, newKey3, oldKey1, oldKey2, oldKey3, uid);
        }

        public static int Copy_Here2Data(string tableName, V6Mode mode,
            string keyField1,string keyField2,string keyField3,
            string newKey1, string newKey2, string newKey3,
            string oldKey1, string oldKey2, string oldKey3,
            string uid)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@tableName", tableName), 
                new SqlParameter("@mode", mode == V6Mode.Add? "M" : mode == V6Mode.Edit? "S" : "X"),
                new SqlParameter("@keyField1", keyField1), 
                new SqlParameter("@keyField2", keyField2), 
                new SqlParameter("@keyField3", keyField3), 
                new SqlParameter("@newValue1", newKey1), 
                new SqlParameter("@newValue2", newKey2), 
                new SqlParameter("@newValue3", newKey3), 
                new SqlParameter("@oldValue1", oldKey1), 
                new SqlParameter("@oldValue2", oldKey2), 
                new SqlParameter("@oldValue3", oldKey3), 
                new SqlParameter("@uid", uid), 
            };
            return V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_COPY_HERE2DATA", plist);
        }

        /// <summary>
        /// Hiển thị Form chỉnh sửa dữ liệu trực tiếp.
        /// </summary>
        /// <param name="data">Dữ liệu.</param>
        /// <param name="tableName">Tên bảng trong csdl.</param>
        /// <param name="showFields">Các trường hiển thị, đánh dấu Readonly FIELD:R</param>
        /// <param name="keys">Các trường khóa để update, delete. Cách nhau bởi dấu (,) Vd: "STT_REC,STT_REC0"</param>
        /// <param name="allowAdd">Cho phép thêm dòng.</param>
        /// <param name="allowDelete">Cho phép xóa dòng.</param>
        /// <param name="showSum">Hiện phần tổng.</param>
        /// <param name="updateDatabase">Cập nhập trực tiếp database.</param>
        /// <param name="defaultData">Dữ liệu mặc định khi thêm dòng mới.</param>
        /// <param name="owner">Form đang gọi để chống chìm dialog.</param>
        public static void ShowDataEditorForm(DataTable data, string tableName, string showFields, string keys,
            bool allowAdd, bool allowDelete, bool showSum = true, bool updateDatabase = true, IDictionary<string, object> defaultData = null, IWin32Window owner = null)
        {
            var f = new DataEditorForm(data, tableName, showFields, keys, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language),
                allowAdd, allowDelete, showSum, updateDatabase, defaultData);
            f.ShowDialog(owner);
        }

        /// <summary>
        /// Khởi tạo form chỉnh sửa dữ liệu trực tiếp.
        /// </summary>
        /// <param name="data">Dữ liệu.</param>
        /// <param name="tableName">Tên bảng trong csdl.</param>
        /// <param name="showFields">Các trường hiển thị, đánh dấu Readonly FIELD:R</param>
        /// <param name="keys">Các trường khóa để update, delete. Cách nhau bởi dấu (,) Vd: "STT_REC,STT_REC0"</param>
        /// <param name="allowAdd">Cho phép thêm dòng.</param>
        /// <param name="allowDelete">Cho phép xóa dòng.</param>
        /// <param name="showSum">Hiện phần tổng.</param>
        /// <param name="updateDatabase">Cập nhập trực tiếp database.</param>
        /// <param name="defaultData">Dữ liệu mặc định khi thêm dòng mới.</param>
        /// <returns>DataEditorForm</returns>
        public static DataEditorForm MakeDataEditorForm(DataTable data, string tableName, string showFields, string keys,
            bool allowAdd, bool allowDelete, bool showSum = true, bool updateDatabase = true, IDictionary<string, object> defaultData = null)
        {
            var f = new DataEditorForm(data, tableName, showFields, keys, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language), allowAdd, allowDelete, showSum, updateDatabase, defaultData);
            return f;
        }

        public static Control FindParent<T>(Control child, int maxLevel = 10)
        {
            Control c = child;
            for (int i = 0; i < maxLevel; i++)
            {
                c = c.Parent;
                if (c is T)
                {
                    return c;
                }
            }
            return null;
        }

        public static Control FindChild<T>(Control parent, int level = 0, int maxLevel = 10)
        {
            if (level > maxLevel) return null; 
            Control c = parent;
            if (c is T) return c;
            foreach (Control control in c.Controls)
            {
                var c2 = FindChild<T>(control, ++level);
                if (c2 is T) return c2;
            }
            return null;
        }

        public static void SetGridviewCurrentCellByIndex(DataGridView gridView1, int rowIndex, int cellIndex, Control form = null)
        {
            try
            {
                if (gridView1.Rows.Count > 0)
                {
                    if (gridView1.RowCount <= rowIndex)
                    {
                        rowIndex = gridView1.Rows.Count - 1;
                    }
                    if (gridView1.ColumnCount <= cellIndex)
                    {
                        cellIndex = gridView1.RowCount - 1;
                    }
                    gridView1.CurrentCell = gridView1.Rows[rowIndex].Cells[cellIndex];
                }
            }
            catch (Exception ex)
            {
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".SetGridviewCurrentCellByIndex ", ex);
            }
        }

        public static void SetGridviewCurrentCellToLastRow(V6ColorDataGridView gview, string field)
        {
            try
            {
                if (gview.RowCount > 0)
                {
                    DataGridViewCell temp_cell = null;
                    DataGridViewRow lastRow = gview.Rows[gview.RowCount - 1];
                    if (gview.Columns.Contains(field)) temp_cell = lastRow.Cells[field];
                    if (temp_cell == null || !temp_cell.Visible)
                    {
                        foreach (DataGridViewCell cell in lastRow.Cells)
                        {
                            if (cell.Visible)
                            {
                                temp_cell = cell;
                                break;
                            }
                        }
                    }

                    if (temp_cell != null && temp_cell.Visible)
                    {
                        gview.CurrentCell = temp_cell;
                    }
                }
            }
            catch (Exception ex)
            {
                gview.WriteExLog(gview.FindForm() + ".SetCurrentCellToLastRow", ex);
            }
        }

        /// <summary>
        /// Áp dụng bấm chuột giữa để hiển thị thông tin, chuột phải cho chức năng ngôn ngữ.
        /// </summary>
        /// <param name="control"></param>
        public static void ApplyControlTripleClick(Control control)
        {
            if (V6Setting.Triple)
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var cancellang = control is DataGridView || control is ICrystalReportViewer || tagString.Contains(";canceltriple;");
                if (cancellang) return;

                ApplyControlTripleClickRecusive(control);
                control.AddTagString("canceltriple");
            }
            catch (Exception ex)
            {
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".ApplyLabelTripleClick", ex);
            }
        }

        private static void ApplyControlTripleClickRecusive(Control control)
        {
            foreach (Control c in control.Controls)
            {
                c.MouseUp += c_MouseDClick;

                if (c is ContainerControl || c is ScrollableControl || c is GroupBox || c is TabControl)
                {
                    ApplyControlTripleClickRecusive(c);
                }
            }
        }

        /// <summary>
        /// <para>Hiển thị thông tin ở Status bar khi bấm chuột giữa.</para> 
        /// <para>Edit ngôn ngữ khi bấm chuột phải [hai lần].</para> 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void c_MouseDClick(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            if (e.Button == MouseButtons.Middle)
            {
                var message = string.Format("{0}({1}), Aname({2}), Adescription({3}),",
                    control.GetType(), control.Name, control.AccessibleName, control.AccessibleDescription);
                
                SetStatusText(message);
                Clipboard.SetText(message);
            }
            else if (e.Button == MouseButtons.Right && !string.IsNullOrEmpty(control.AccessibleDescription)
                && !control.AccessibleDescription.Contains(',') && !control.AccessibleDescription.Contains(';'))
            {
                if (string.IsNullOrEmpty(control.AccessibleDescription))
                {
                    var vf = (V6FormControl) FindParent<V6FormControl>(control);
                    if (vf != null) vf.ShowMainMessage("No AccessibleDescription.");
                }
                else
                {
                    if (sender is V6Label) return;
                    new FormChangeControlLanguageText(control).ShowDialog(control);
                }
            }
        }

        /// <summary>
        /// Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static string FindFilterType(Control control)
        {
            if (control == null)
            {
                return "0";
            }
            
            if (control is V6Control)
            {
                string ft = ((V6Control)control).FilterType;
                if (!string.IsNullOrEmpty(ft)) return ft;
            }
            if (control is V6Form)
            {
                string ft = ((V6Form)control).FilterType;
                if (!string.IsNullOrEmpty(ft)) return ft;
            }

            return FindFilterType(control.Parent);
        }

        #region ==== APPLY LOOKUP ====

        private static TextBox txtApplyLookup;
        private static AldmConfig Aldm_config = null;
        private static AutoCompleteStringCollection auto1;
        private static string InitFilter = "";
        private static string LookupInfo_F_NAME;
        private static bool F2 = false;
        private static bool FilterStart = false;
        private static bool Looking = false;
        
        public static void ApplyLookup(TextBox textBox, string tablename, string fieldvalue)
        {
            if (textBox == null) return;
            txtApplyLookup = textBox;
            Aldm_config = ConfigManager.GetAldmConfigByTableName(tablename);
            string filterType = FindFilterType(textBox);
            InitFilter = V6Login.GetInitFilter(Aldm_config.TABLE_NAME, filterType);
            
            if (!string.IsNullOrEmpty(fieldvalue)) LookupInfo_F_NAME = fieldvalue;
            else LookupInfo_F_NAME = Aldm_config == null ? null : Aldm_config.F_NAME;

            //txt.GotFocus += ApplyLookup_GotFocus;
            txtApplyLookup.KeyDown += ApplyLookup_KeyDown;
            txtApplyLookup.LostFocus += ApplyLookup_LostFocus;
            txtApplyLookup.Disposed += ApplyLookup_Disposed;
        }

        public static void ApplyNumberTextBox(Control control)
        {
            //var textBox = control as TextBox;
            //if (textBox == null) return;
            //control.KeyDown += (sender, e) =>
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        control.Text = control.Text.Replace(" ", "");
            //        return;
            //    }
            //};

            control.KeyPress += (sender, e) =>
            {
                //if (textBox.ReadOnly) return;

                //if (e.KeyChar == ' ')
                //{
                //    control.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(control.Text), 2, ".", " ");
                //    e.Handled = true;
                //}



                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.' && control.Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '-' && control.Text.Length > 0)
                {
                    e.Handled = true;
                }

                //TextBox txt = control as TextBox;
                //if (txt != null)
                //{
                //    int sls = txt.SelectionStart;
                //    control.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(control.Text), 2, ".", " ");
                //    txt.SelectionStart = sls;
                //}

                // ==================================================================================================================
            //    if (txt == null) return;
            //    if (txt.ReadOnly) return;
            //    string StringValue = txt.Text.Replace(" ", "");
            //    bool _isNegative = StringValue.StartsWith("-");
            //    char _decimalSymbol = '.';
            //    string _system_decimal_symbol = ".";

            //    string t = e.KeyChar.ToString();
            //    if (Char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
                    
            //        InsertStringValue(txt, e.KeyChar);
            //    }
            //    else if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == _decimalSymbol || e.KeyChar == _system_decimal_symbol[0])
            //    {
            //        e.Handled = true;
            //        if (!StringValue.Contains(_system_decimal_symbol))
            //        {
            //            InsertStringValue(txt, _system_decimal_symbol[0]);
            //        }
            //        txt.SelectionStart = txt.Text.IndexOf(_decimalSymbol) + 1;
            //    }
            //    else if (e.KeyChar == '-')
            //    {
            //        e.Handled = true;
            //        if (_isNegative && txt.SelectionStart == 0)
            //            txt.SelectionStart = 1;
            //        _isNegative = !_isNegative;
            //        //txt.Write();                  // ========== xử lý âm.
            //    }
            //    else if (e.KeyChar != 3 && e.KeyChar != 22)
            //    {
            //        e.Handled = true;
            //    }
            };

            //control.LostFocus += (sender, e) =>
            //{
            //    control.Text = control.Text.Replace(" ", "");
            //};
        }

        private static void InsertStringValue(TextBox txt, char s)
        {
            string StringValue = txt.Text.Replace(" ", "");
            //if (true || StringValue.Length < MaxLength || (MaxLength == 1 && !string.IsNullOrEmpty(LimitCharacters)))
            {
                int dotIndex = StringValue.IndexOf(".", StringComparison.Ordinal);
                //int sls = SelectionStart;
                int right = txt.TextLength - txt.SelectionStart;
                int i = GetRealSelectionIndex(txt);
                int l = GetRealSelectionLength(txt);

                if (txt.MaxLength == 1)// && !string.IsNullOrEmpty(LimitCharacters))
                {
                    StringValue = "";
                }
                else if (txt.TextLength >= txt.MaxLength && dotIndex < 0)
                {
                    if (i <= StringValue.Length)
                    {
                        if (l > 0)//có bôi đen thì xử lý
                        {
                            StringValue = StringValue.Remove(i, l);
                            txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                            right = StringValue.Length - i;

                            if ((i == 0 || i == 1) && StringValue.StartsWith("0"))
                            {
                                StringValue = s + StringValue.Substring(1);
                                txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                                //if (right > 0) right--;
                            }
                            else
                            {
                                StringValue = StringValue.Insert(i, s.ToString());
                                txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                            }

                            var sls = txt.TextLength - right + ((dotIndex > 0 && i > dotIndex) ? 1 : 0);
                            if (sls <= 0) sls = 1;
                            txt.SelectionStart = sls;
                        }
                    }
                    return;
                }
                else if (dotIndex > 0 && ((txt.TextLength >= txt.MaxLength && i <= dotIndex) || txt.SelectionStart == txt.TextLength))
                {
                    return;
                }


                try
                {
                    if (i <= StringValue.Length)
                    {
                        if (l > 0)//có bôi đen
                        {
                            StringValue = StringValue.Remove(i, l);
                            txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                            right = StringValue.Length - i;
                        }

                        if ((i == 0 || i == 1) && StringValue.StartsWith("0"))
                        {
                            StringValue = s + StringValue.Substring(1);
                            txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                            //if (right > 0) right--;
                        }
                        else
                        {
                            StringValue = StringValue.Insert(i, s.ToString());
                            txt.Text = ObjectAndString.NumberToString(ObjectAndString.ObjectToDecimal(StringValue), 2, ".", " ");
                        }
                    }

                    var sls = txt.TextLength - right + ((dotIndex > 0 && i > dotIndex) ? 1 : 0);
                    if (sls <= 0) sls = 1;
                    txt.SelectionStart = sls;
                }
                catch { }

                
            }
        }

        private static int GetRealSelectionIndex(TextBox txt, int i = -1)
        {
            string StringValue = txt.Text.Replace(" ", "");
            if (i == -1) i = txt.SelectionStart;

            int returnInt = 0;
            //Lấy chuỗi tạm đến vị trí con trỏ
            string s = txt.Text.Substring(0, i);
            //Xử lý bỏ những thứ không cần thiết trong chuỗi
            //ký hiệu tiền tệ, dấu cách phần ngàn, khoảng trắng)
            //if (CurrencySymbolAtFirst && _currencySymbol.Length > 0 && s.Length >= _currencySymbol.Length)            
            //    s = s.Substring(_currencySymbol.Length);// .Replace(mCSymbol, "");
            if (s.Length > 0 && " ".Length > 0)
                s = s.Replace(" ", "");
            if (s.Length > 0)
                s = s.Replace(" ", "");
            if (s.Length > 0)
                s = s.Replace("-", "");

            returnInt = s.Length;
            if (returnInt > StringValue.Length) returnInt = StringValue.Length;
            return returnInt;
        }

        private static int GetRealSelectionLength(TextBox txt)
        {
            int i = txt.SelectionStart;
            int l = txt.SelectionLength;
            if (l == 0) return 0;
            int r = 0;
            string s = txt.Text.Substring(i, l);

            if (s.Length > 0)
                s = s.Replace(" ", "");
            if (s.Length > 0)
                s = s.Replace("-", "");

            r = s.Length;
            return r;
        }



        private static void RemoveLookupResource()
        {
            Aldm_config = null;
            auto1 = null;
            GC.Collect();
        }

        static void ApplyLookup_Disposed(object sender, EventArgs e)
        {
            RemoveLookupResource();
        }

        private static void ApplyLookup_GotFocus(object sender, EventArgs e)
        {
            try
            {
                LoadAutoCompleteSource((TextBox)sender);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("V6LookupTextBox_GotFocus: " + ex.Message);
            }
        }

        private static void ApplyLookup_KeyDown(object sender, KeyEventArgs e)
        {
            IWin32Window owner =  sender as IWin32Window;
            
            if (e.KeyData == (Keys.Control | Keys.Q))
            {
                V6ControlsHelper.DisableLookup = true;
                //SwitchAutoCompleteMode();
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //DoCharacterCasing();
                //if (_checkOnLeave && !ReadOnly && Visible)
                {
                    if (txtApplyLookup.Text.Trim() != "")
                    {
                        if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                        {
                            if (ExistRowInTable(txtApplyLookup.Text.Trim()))
                            {
                                if (e.KeyCode == Keys.Enter)
                                {
                                    e.SuppressKeyPress = true;
                                    SendKeys.Send("{TAB}");
                                }
                            }
                            else
                            {
                                DoLookup(owner);
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                    {
                        DoLookup(owner);
                    }
                }
            }
            else if (e.KeyCode == Keys.F5 && !string.IsNullOrEmpty(LookupInfo_F_NAME))
            {
                //LoadAutoCompleteSource();
                DoLookup(owner);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (F2)
                {
                    DoLookup(owner, LookupMode.Multi);
                }
            }
        }

        private static void ApplyLookup_LostFocus(object sender, EventArgs e)
        {
            if (Aldm_config.NoInfo) return;
            IWin32Window owner = sender as IWin32Window;
            //DoCharacterCasing();
            var textBox = (TextBox)sender;
            if (V6ControlsHelper.DisableLookup)
            {
                V6ControlsHelper.DisableLookup = false;
                return;
            }

            //if (EnableColorEffect)
            //{
            //    BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            //}

            if (true)// _checkOnLeave && !ReadOnly && Visible)
            {
                if (textBox.Text.Trim() != "")
                {
                    if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                    {
                        if (ExistRowInTable(textBox.Text.Trim()))
                        {
                            //if (!Looking && gotfocustext != Text) CallDoV6LostFocus();
                            //else CallDoV6LostFocusNoChange();
                        }
                        else
                        {
                            DoLookup(owner, LookupMode.Single);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                {
                    DoLookup(owner, LookupMode.Single);
                }
                else
                {
                    ExistRowInTable(textBox.Text.Trim());
                }
            }

            //if (_lockFocus)
            //{
            //    _lockFocus = false;
            //    Focus();
            //}
        }

        private static void DoLookup(IWin32Window owner, LookupMode multi = LookupMode.Single)
        {
            if (Aldm_config.NoInfo) return;
            //_frm = FindForm();
            var filter = InitFilter;
            if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
            var parentData = new SortedDictionary<string, object>();
            var lookup = new V6LookupTextboxForm(parentData, txtApplyLookup.Text, Aldm_config, " 1=1 " + filter, LookupInfo_F_NAME, multi, FilterStart);
            Looking = true;
            lookup.ShowDialog(owner);
        }

        private static void Lookup(IWin32Window owner, LookupMode multi = LookupMode.Single)
        {
            DoLookup(owner, multi);
        }

        private static string _text_data = "";
        private static DataRow _data;
        private static bool ExistRowInTable(string text)
        {
            try
            {
                _text_data = text;
                if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                {
                    string tableName = Aldm_config.TABLE_NAME;
                    var filter = InitFilter;
                    if (!string.IsNullOrEmpty(filter)) filter = " and (" + filter + ")";

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@text", text)
                    };
                    var tbl = V6BusinessHelper.Select(tableName, "*", LookupInfo_F_NAME + "=@text " + filter, "", "", plist).Data;

                    if (tbl != null && tbl.Rows.Count >= 1)
                    {
                        var oneRow = tbl.Rows[0];
                        _data = oneRow;
                        //V6ControlFormHelper.SetBrotherData(txt, _data, BrotherFields);
                        //SetNeighborValues();
                        return true;
                    }
                    else
                    {
                        _data = null;
                        //V6ControlFormHelper.SetBrotherData(this, _data, BrotherFields);
                        //SetNeighborValues();
                    }
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorMessage(ex.Message);
                return false;
            }
            return false;
        }

        private static void LoadAutoCompleteSource(TextBox txt)
        {
            //if (auto1 != null) return;
            if (Aldm_config.NoInfo) return;

            if (!string.IsNullOrEmpty(Aldm_config.TABLE_NAME) && !string.IsNullOrEmpty(Aldm_config.F_NAME) && auto1 == null)
            {
                try
                {
                    auto1 = new AutoCompleteStringCollection();

                    var selectTop = "";

                    if (!string.IsNullOrEmpty(Aldm_config.F_NAME))
                    {
                        var tableName = Aldm_config.TABLE_NAME;
                        var filter = InitFilter;
                        if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
                        var where = " 1=1 " + filter;

                        var tbl1 = V6BusinessHelper.Select(tableName,
                            selectTop + " [" + LookupInfo_F_NAME + "]",
                            where, "", "", null).Data;

                        for (int i = 0; i < tbl1.Rows.Count; i++)
                        {
                            auto1.Add(tbl1.Rows[i][0].ToString().Trim());
                        }
                        V6ControlsHelper.DisableLookup = true;
                        txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txt.AutoCompleteCustomSource = auto1;
                        txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        V6ControlsHelper.DisableLookup = false;
                    }
                }
                catch (Exception ex)
                {
                    WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".LoadAutoCompleteSource " + Aldm_config.TABLE_NAME, ex);
                    V6ControlsHelper.DisableLookup = false;
                }
            }

        }

        #endregion apply lookup

        private static OpenFileDialog excelOpenFileDialog = new OpenFileDialog()
        {
            Filter = "Excel|*.xls;*.xlsx",
            Multiselect = false,
            Title = V6Setting.IsVietnamese?"Chọn file Excel":"Choose Excel file"
        };

        /// <summary>
        /// Chọn một file Excel trong ổ đĩa. Nếu không chọn trả về rỗng.
        /// </summary>
        /// <para name="owner">Form hooặc control chủ gọi hàm này.</para>
        /// <returns></returns>
        public static string ChooseExcelFile(IWin32Window owner)
        {
            if (V6Setting.IsDesignTime) return "";
            if (excelOpenFileDialog.ShowDialog(owner) == DialogResult.OK)
            {
                return excelOpenFileDialog.FileName;
            }
            return "";
        }

        private static SaveFileDialog saveFileDialog = new SaveFileDialog()
        {
            Filter = "All file|*.*",
            Title = V6Setting.IsVietnamese ? "Chọn file lưu" : "Choose save file"
        };

        /// <summary>
        /// Chọn một file để lưu. Nếu không chọn trả về rỗng.
        /// </summary>
        /// <param name="owner">Form hooặc control chủ gọi hàm này.</param>
        /// <param name="filter">Lọc file, vd: All file|*.*</param>
        /// <returns></returns>
        public static string ChooseSaveFile(IWin32Window owner, string filter)
        {
            if (V6Setting.IsDesignTime) return "";
            if (string.IsNullOrEmpty(filter)) filter = "All file|*.*";
            else if (!filter.Contains("All file|*.*".ToUpper())) filter += "|All file|*.*";
            saveFileDialog.Filter = filter;
            if (saveFileDialog.ShowDialog(owner) == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            return "";
        }

        private static OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "All file|*.*",
            Title = V6Setting.IsVietnamese ? "Chọn file lưu" : "Choose save file"
        };

        

        /// <summary>
        /// Chọn một file để mở. Nếu không chọn trả về rỗng.
        /// </summary>
        /// <param name="owner">Form hooặc control chủ gọi hàm này.</param>
        /// <param name="filter">Lọc file, vd: All file|*.*</param>
        /// <returns></returns>
        public static string ChooseOpenFile(IWin32Window owner, string filter)
        {
            if (V6Setting.IsDesignTime) return "";
            if (string.IsNullOrEmpty(filter)) filter = "All file|*.*";
            else if (!filter.Contains("All file|*.*".ToUpper())) filter += "|All file|*.*";
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        /// <summary>
        /// Chọn một hình trong ổ đĩa. Nếu không chọn trả về null
        /// </summary>
        /// <param name="owner">Form hooặc control chủ gọi hàm này.</param>
        /// <returns></returns>
        public static Image ChooseImage(IWin32Window owner)
        {
            using (var box = new OpenFileDialog())
            {
                box.Filter = "JPG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                if (box.ShowDialog(owner) == DialogResult.OK)
                {
                    return LoadCopyImage(box.FileName);
                }
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra bảng dữ liệu có đủ những trường cần thiết hay không.
        /// </summary>
        /// <param name="dataTable">Bảng dữ liệu cần kiểm tra</param>
        /// <param name="columns">Các cột dữ liệu cần phải có</param>
        /// <returns></returns>
        public static bool CheckDataFields(DataTable dataTable, IList<string> columns)
        {
            return columns.All(column => dataTable.Columns.Contains(column));
        }

        /// <summary>
        /// Gán giá trị control theo config 
        /// </summary>
        /// <param name="control">Đối tượng được gán dữ liệu.</param>
        /// <param name="value">Giá trị để gán.</param>
        /// <param name="config">Status(Có sử dụng?):1;Override:1;NotEmpty(Phải có giá trị truyền vào):0;NoOverride(Chỉ gán nến control rỗng):0</param>
        public static void SetControlValue(Control control, object value, DefineInfo config)
        {
            try
            {
                if (!config.Status) return;
                if (config.Override)
                {
                    SetControlValue(control, value);
                }
                
                if (config.NotEmpty)
                {
                    if (value == null) return;
                    if (ObjectAndString.IsNumberType(value.GetType()))
                    {
                        if (ObjectAndString.ObjectToDecimal(value) == 0) return;
                    }
                    if (value.ToString().Trim() == "") return;
                    SetControlValue(control, value);
                }
                
                if (config.NoOverride)
                {
                    if (control is V6NumberTextBox)
                    {
                        var numTb = control as V6NumberTextBox;
                        if (numTb.Value == 0) SetControlValue(numTb, value);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(control.Text)) SetControlValue(control, value);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".SetControlValue " + control.Name + ":" + value, ex);
            }
        }
        
        public static void SetControlReadOnly(Control control, bool readOnly)
        {
            try
            {
                if (control is TextBox)
                {
                    var txt = control as TextBox;
                    txt.ReadOnly = readOnly;
                }
                else if (control is V6DateTimePicker)
                {
                    var dat = control as V6DateTimePicker;
                    dat.ReadOnly = readOnly;
                    //dat.Enabled = !readOnly;
                }
                else if (control is DateTimePicker
                    || control is CheckBox
                    || control is RadioButton
                    || control is ComboBox
                    || control is V6FormButton
                    || control is GioiTinhControl
                    )
                {
                    control.Enabled = !(readOnly);
                }
                else if (control is DataGridView)
                {
                    var dgv = (DataGridView)control;
                    dgv.ReadOnly = readOnly;
                }
                else if (control is TextBoxBase)
                {
                    ((TextBoxBase)control).ReadOnly = readOnly;
                }
            }
            catch (Exception ex)
            {
                WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".SetControlReadOnly " + control.Name + "." + control.AccessibleName, ex);
            }
        }


        public static void SetListControlReadOnlyByAccessibleNames(Control container, IList<string> accNameList, bool readOnly)
        {
            try
            {
                var listControl = GetListControlByAccessibleNames(container, accNameList);
                foreach (Control control in listControl)
                {
                    SetControlReadOnly(control, readOnly);
                    // Ẩn dấu check liên quan
                    if (control.Parent != null)
                    {
                        var chk = GetControlByName(control.Parent, "chk" + control.AccessibleName);
                        if (chk != null)
                        {
                            if(readOnly) chk.InvisibleTag();
                            else chk.VisibleTag();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                container.WriteExLog(container.GetType() + ".SetListControlReadOnlyByAccessibleNames", ex);
            }
        }
        
        public static void SetListControlVisibleByAccessibleNames(Control container, IList<string> accNameList, bool visible)
        {
            try
            {
                var listControl = GetListControlByAccessibleNames(container, accNameList);
                foreach (Control control in listControl)
                {
                    control.Visible = visible;
                    // Ẩn các control liên quan lbl, chk...
                    if (control.Parent != null)
                    {
                        var lbl = GetControlByName(control.Parent, "lbl" + control.AccessibleName);
                        if (lbl != null) lbl.Visible = visible;

                        var chk = GetControlByName(control.Parent, "chk" + control.AccessibleName);
                        if (chk != null)
                        {
                            if (visible) chk.Visible = true;
                            else chk.InvisibleTag();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                container.WriteExLog(container.GetType() + ".SetListControlVisibleByAccessibleNames", ex);
            }
        }

        /// <summary>
        /// Gán dữ liệu vào control các loại. Control null sẽ bỏ qua. Value null sẽ gán rỗng hoặc mặc định.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        public static void SetControlValue(Control control, object value)
        {
            if (control == null) return;
            if (value == null) value = "";
            var color = control as V6DateTimeColor;
            if (color != null)
            {
                color.Value = ObjectAndString.ObjectToDate(value);
            }
            else if (control is V6IndexComboBox)
            {
                ((V6IndexComboBox)control).SelectedIndex = ObjectAndString.ObjectToInt(value);
            }
            else if (control is ComboBox)
            {
                var com = control as ComboBox;
                try
                {
                    var VALUE = ObjectAndString.ObjectToString(value).Trim();
                    if (com.Items.Count > 0 && VALUE != "")
                    {
                        if (string.IsNullOrEmpty(com.ValueMember))
                        {
                            com.SelectedItem = VALUE;
                        }
                        else
                        {
                            com.SelectedValue = VALUE;
                        }
                    }
                    else
                    {
                        if (com.Items.Count > 0)
                            com.SelectedIndex = -1;
                    }
                }
                catch
                {
                    // ignored
                }
            }
            else if (control is PictureBox)
            {
                var pic = control as PictureBox;
                try
                {
                    var objectData = value;
                    Image picture = null;
                    if (objectData is Image) picture = (Image)objectData;
                    else if (objectData is byte[]) picture = Picture.ByteArrayToImage((byte[])objectData);

                    pic.Image = picture;
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog("V6ControlFormHelper.SetFormDataDicRecusive PictureBox " + ex.Message);
                }
            }
            else if (control is V6LookupTextBox)
            {
                ((V6LookupTextBox) control).SetValue(value);
            }
            else if (control is V6LookupProc)
            {
                ((V6LookupProc)control).SetValue(value);
            }
            else if (control is V6DateTimePicker)
            {
                var object_to_date = ObjectAndString.ObjectToFullDateTime(value);
                ((V6DateTimePicker)control).SetValue(object_to_date);
            }
            else if (control is DateTimePicker)
            {
                var object_to_date = ObjectAndString.ObjectToFullDateTime(value);
                ((DateTimePicker)control).Value = object_to_date;
            }
            else if (control is V6VvarTextBox) //!!!!.ChangeText()????
            {
                var vvarTextBox = control as V6VvarTextBox;
                vvarTextBox.SetDataRow(null);
                
                var text = ObjectAndString.ObjectToString(value).Trim();
                if (vvarTextBox.UseChangeTextOnSetFormData)
                    vvarTextBox.ChangeText(text);
                else vvarTextBox.Text = text;
            }
            else if (control is V6NumberTextBox)
            {
                var text_box = control as V6NumberTextBox;
                var value1 = ObjectAndString.ObjectToDecimal(value);
                if (text_box.UseChangeTextOnSetFormData)
                    text_box.ChangeValue(value1);
                else text_box.Value = value1;
            }
            else if (control is V6CheckTextBox)
            {
                ((V6CheckTextBox)control).SetStringValue(ObjectAndString.ObjectToString(value));
            }
            else if (control is V6ColorTextBox)
            {
                var text_box = control as V6ColorTextBox;
                var text = ObjectAndString.ObjectToString(value).Trim();
                if (text_box.UseChangeTextOnSetFormData)
                    text_box.ChangeText(text);
                else text_box.Text = text;
            }
            else if (control is V6ColorMaskedTextBox)
            {
                var text_box = control as V6ColorMaskedTextBox;
                var text = ObjectAndString.ObjectToString(value).Trim();
                if (text_box.UseChangeTextOnSetFormData)
                    text_box.ChangeText(text);
                else text_box.Text = text;
            }
            else if (control is CheckBox)
            {
                string value1 = value.ToString().Trim();
                if (value1 == "1" || value1.ToLower() == "true")
                {
                    ((CheckBox)control).Checked = true;
                }
                else
                {
                    ((CheckBox)control).Checked = false;
                }
            }
            else if (control is RadioButton)
            {
                ((RadioButton)control).Checked = ObjectAndString.ObjectToBool(value);
                if (value.ToString().Trim() == control.Text)
                {
                    ((RadioButton)control).Checked = true;
                }
            }
            else if (control is GioiTinhControl)
            {
                bool nam = ObjectAndString.ObjectToBool(value);
                if(nam) ((GioiTinhControl)control).Value = "1";
                else ((GioiTinhControl)control).Value = "0";
            }
            else
            {
                control.Text = ObjectAndString
                    .ObjectToString(value).Trim();
            }
        }

        /// <summary>
        /// Lấy dữ liệu đưa vào Dictionary.
        /// </summary>
        /// <param name="d">Dictionary chứa dữ liệu lấy được</param>
        /// <param name="control"></param>
        /// <returns>Dữ liệu chính của control.</returns>
        public static void FillControlValueToDictionary(IDictionary<string, object> d, Control control)
        {
            string cNAME = control is RadioButton ? control.Name.ToUpper() : control.AccessibleName.Trim().ToUpper();

            if (control is V6VvarTextBox)
            {
                d[cNAME] = ((V6VvarTextBox)control).Text;
                //return control.Text;
                return;
            }
            else if (control is V6NumberTextBox)
            {
                d[cNAME] = ((V6NumberTextBox)control).Value;
                //return ((V6NumberTextBox)control).Value;
                return;
            }
            if (control is V6DateTimeColor)
            {
                var color = control as V6DateTimeColor;
                d[cNAME] = color.Value;
                //return color.Value;
                return;
            }
            else if (control is V6DateTimePicker)
            {
                d[cNAME] = ((V6DateTimePicker)control).Date;
                //return ((V6DateTimePick)control).Value;
                return;
            }
            else if (control is DateTimePicker)
            {
                d[cNAME] = ((DateTimePicker)control).Value;
                //return ((DateTimePicker)control).Value;
                return;
            }
            else if (control is V6IndexComboBox)
            {
                d[cNAME] = ((V6IndexComboBox)control).SelectedIndex;
                //return ((V6IndexComboBox)control).SelectedIndex;
                return;
            }
            else if (control is V6ComboBox)
            {
                var cbo = control as V6ComboBox;
                d[cNAME] = cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
                //return cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
                return;
            }
            else if (control is ComboBox)
            {
                var cbo = control as ComboBox;
                d[cNAME] = cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
                //return cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
                return;
            }
            else if (control is PictureBox)
            {
                var pic = control as PictureBox;
                d[cNAME] = Picture.ToJpegByteArray(pic.Image);
                //return Picture.ToJpegByteArray(pic.Image);
                return;
            }
            else if (control is V6LookupTextBox)
            {
                V6LookupTextBox ltb = (V6LookupTextBox)control;
                d[cNAME] = ltb.Value;
                if (!string.IsNullOrEmpty(ltb.AccessibleName2))
                {
                    d[ltb.AccessibleName2.ToUpper()] = ltb.Text;
                }
                //return ltb.Value;
                return;
            }
            else if (control is V6CheckTextBox)
            {
                d[cNAME] = ((V6CheckTextBox)control).StringValue;
                //return ((V6CheckTextBox)control).StringValue;
                return;
            }
            else if (control is CheckBox)
            {
                d[cNAME] = ((CheckBox)control).Checked ? 1 : 0;
                //return ((CheckBox)control).Checked ? 1 : 0;
                return;
            }
            else if (control is RadioButton)
            {
                if (((RadioButton) control).Checked)
                {
                    d[cNAME] = control.Text;
                    //return true;
                    return;
                }
                else
                {
                    //return false;
                    return;
                }
            }
            else if (control is GioiTinhControl)
            {

                d[cNAME] = ((GioiTinhControl)control).Value;
                //return ((GioiTinhControl)control).Value;
                return;
            }
            

            d[cNAME] = control.Text;
            //return control.Text;
            return;
        }

        /// <summary>
        /// Lấy 1 giá trị của control gửi vào.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static object GetControlValue(Control control)
        {
            //string cNAME = control.AccessibleName.Trim().ToUpper();

            if (control is FilterLineBase)
            {
                var fl = control as FilterLineBase;
                return fl.ObjectValue;
            }
            else if (control is V6VvarTextBox)
            {
                return control.Text;
            }
            else if (control is V6NumberTextBox)
            {
                return ((V6NumberTextBox)control).Value;
            }
            if (control is V6DateTimeColor)
            {
                var color = control as V6DateTimeColor;
                return color.Value;
            }
            else if (control is V6DateTimePicker)
            {
                return ((V6DateTimePicker)control).Date;
            }
            else if (control is DateTimePicker)
            {
                return ((DateTimePicker)control).Value;
            }
            else if (control is V6IndexComboBox)
            {
                return ((V6IndexComboBox)control).SelectedIndex;
            }
            else if (control is V6ComboBox)
            {
                var cbo = control as V6ComboBox;
                return cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
            }
            else if (control is ComboBox)
            {
                var cbo = control as ComboBox;
                return cbo.DataSource != null ? cbo.SelectedValue : cbo.Text;
            }
            else if (control is PictureBox)
            {
                var pic = control as PictureBox;
                return Picture.ToJpegByteArray(pic.Image);
            }
            else if (control is V6LookupTextBox)
            {
                V6LookupTextBox ltb = (V6LookupTextBox)control;
                //d[cNAME] = ltb.Value;
                //if (!string.IsNullOrEmpty(ltb.AccessibleName2))
                //{
                //    d[ltb.AccessibleName2.ToUpper()] = ltb.Text;
                //}
                return ltb.Value;
            }
            else if (control is V6LookupProc)
            {
                V6LookupProc lp = (V6LookupProc)control;
                return lp.Value;
            }
            else if (control is V6CheckTextBox)
            {
                return ((V6CheckTextBox)control).StringValue;
            }
            else if (control is CheckBox)
            {
                return ((CheckBox)control).Checked ? 1 : 0;
            }
            else if (control is RadioButton)
            {
                if (((RadioButton)control).Checked)
                {
                    //d[cNAME] = control.Text;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (control is GioiTinhControl)
            {
                return ((GioiTinhControl)control).Value;
            }


            return control.Text;
        }

        /// <summary>
        /// Áp dụng code động cho event của control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventProgram"></param>
        /// <param name="All_Objects">Các đối tượng control</param>
        /// <param name="before">Phần cộng thêm cho tên hàm Event</param>
        public static void ApplyControlEventByAccessibleName(Control control, Type eventProgram, Dictionary<string, object> All_Objects, string before = "")
        {
            string NAME = control.AccessibleName;
            if (string.IsNullOrEmpty(NAME)) return;
            NAME = NAME.ToUpper();

            if (control is V6ColorTextBox)
            {
                var colorTB = control as V6ColorTextBox;
                colorTB.V6LostFocus += (sender)=>
                {
                    All_Objects["sender"] = sender;
                    V6ControlsHelper.InvokeMethodDynamic(eventProgram, NAME + "_V6LOSTFOCUS" + before, All_Objects);
                };
                colorTB.V6LostFocusNoChange += (sender) =>
                {
                    All_Objects["sender"] = sender;
                    V6ControlsHelper.InvokeMethodDynamic(eventProgram, NAME + "_V6LOSTFOCUSNOCHANGE" + before, All_Objects);
                };
            }

            control.GotFocus += (sender, e) =>
            {
                All_Objects["sender"] = sender;
                All_Objects["e"] = e;
                V6ControlsHelper.InvokeMethodDynamic(eventProgram, NAME + "_GOTFOCUS" + before, All_Objects);
            };
            control.LostFocus += (sender, e) =>
            {
                All_Objects["sender"] = sender;
                All_Objects["e"] = e;
                V6ControlsHelper.InvokeMethodDynamic(eventProgram, NAME + "_LOSTFOCUS" + before, All_Objects);
            };
            control.TextChanged += (sender, e) =>
            {
                All_Objects["sender"] = sender;
                All_Objects["e"] = e;
                V6ControlsHelper.InvokeMethodDynamic(eventProgram, NAME + "_TEXTCHANGED" + before, All_Objects);
            };
        }

        public static void ApplyDynamicFormControlEvents(Control thisForm, Type eventProgram, Dictionary<string, object> allObjects)
        {
            if (eventProgram == null) return;

            try
            {
                var all_control = V6ControlFormHelper.GetAllControls(thisForm);
                string error = "";
                foreach (Control control in all_control)
                {
                    try
                    {
                        V6ControlFormHelper.ApplyControlEventByAccessibleName(control, eventProgram, allObjects);
                    }
                    catch (Exception ex)
                    {
                        error += string.Format("{0}({1}) err: {2}", control.Name, control.AccessibleName, ex.Message);
                    }
                }

                if (error.Length > 0)
                {
                    thisForm.WriteToLog(thisForm.GetType() + ".ApplyFormControlEvents", error);
                }
            }
            catch (Exception ex)
            {
                thisForm.WriteExLog(thisForm.GetType() + ".ApplyFormControlEvents", ex);
            }
        }

        /// <summary>
        /// Tạo các control và sự kiện [động] của nó theo các thông tin định nghĩa [Alreport1].
        /// <para>Trong form chính cần có panel PanelAdvance hoặc v6TabControl1.</para>
        /// </summary>
        /// <param name="thisForm">Form chính.</param>
        /// <param name="ma_bc">TableName đối với danh mục, Ma_ct đối với chứng từ.</param>
        /// <param name="All_Objects">Các đối tượng dùng làm tham số trong các hàm động.</param>
        public static void CreateAdvanceFormControls(Control thisForm, string ma_bc, Dictionary<string, object> All_Objects)
        {
            Type Event_program2 = thisForm.GetType();
            DataTable Alreport1Data = null;
            Dictionary<string, DefineInfo> DefineInfo_Data = new Dictionary<string, DefineInfo>();
            Dictionary<string, Label> Label_Controls = new Dictionary<string, Label>();
            Dictionary<V6NumberTextBox, int> NumberTextBox_Decimals = new Dictionary<V6NumberTextBox, int>();
            Dictionary<V6ColorTextBox, int> V6ColorTextBox_MaxLength = new Dictionary<V6ColorTextBox, int>();
            Dictionary<string, Control> Input_Controls = new Dictionary<string, Control>();
            string using_text2 = "", method_text2 = "";

            Panel panel1 = null; //Phải get trên form theo tên nào đó. AdvanceControlsPanel
            panel1 = V6ControlFormHelper.GetControlByName(thisForm, "PanelAdvance") as Panel;
            if (panel1 == null)
            {
                //Tạo tab Advance nếu có tabControl1
                TabControl tabControl1 = V6ControlFormHelper.GetControlByName(thisForm, "v6TabControl1") as TabControl;
                if (tabControl1 == null) tabControl1 = V6ControlFormHelper.GetControlByName(thisForm, "tabControl1") as TabControl;
                if (tabControl1 != null)
                {
                    TabPage advanceTabPage = new TabPage("Advance");
                    advanceTabPage.BackColor = Color.FromArgb(246, 243, 226);
                    tabControl1.TabPages.Add(advanceTabPage);
                    panel1 = new Panel();
                    panel1.Name = "PanelAdvance";
                    panel1.Dock = DockStyle.Fill;
                    advanceTabPage.Controls.Add(panel1);
                }
            }

            if (panel1 == null) return;

            //Đưa form chính vào listObj
            All_Objects["thisForm"] = thisForm;
            //Tạo control động
            IDictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("MA_BC", ma_bc);
            Alreport1Data = V6BusinessHelper.Select(V6TableName.Alreport1, keys, "*", "", "Stt_Filter").Data;
            int i = 0;
            int baseTop = 10;
            int rowHeight = 25;
            foreach (DataRow row in Alreport1Data.Rows)
            {
                var define = row["Filter"].ToString().Trim();
                var define_M = row["Filter_M"].ToString().Trim();
                var defineInfo = new DefineInfo(define);
                var defineInfo_M = new DefineInfo(define_M);
                var AccessibleName_KEY = string.IsNullOrEmpty(defineInfo.AccessibleName)
                    ? defineInfo.Field.ToUpper()
                    : defineInfo.AccessibleName.ToUpper();
                //Bỏ qua nếu đã tồn tại control trên form.
                if (V6ControlFormHelper.GetControlByAccessibleName(thisForm, AccessibleName_KEY) != null) continue;

                DefineInfo_Data[AccessibleName_KEY.ToUpper()] = defineInfo;
                //Label
                var top = baseTop + i * rowHeight;

                var label = new V6Label();
                label.Name = "lbl" + defineInfo.Field;
                label.AutoSize = true;
                label.Left = 10;
                label.Top = top;
                label.Text = defineInfo.TextLang(V6Setting.IsVietnamese);
                label.Visible = defineInfo.Visible;
                panel1.Controls.Add(label);
                Label_Controls[defineInfo.Field.ToUpper()] = label;
                All_Objects[label.Name] = label;
                //Input
                // Các thuộc tính của input sẽ được gán sau cùng nếu chưa khởi tạo
                // AccessibleName = AccessibleName_KEY (AccessibleName hoặc Field)
                // Text = defineInfo.DefaultValue
                // Visible = defineInfo.Visible;
                // Luôn gán
                // Width = define.Width, // Left = 150, Top = top
                Control input = null;
                if (defineInfo.ControlType != null)
                {
                    //Tạo control theo loại.
                    if (defineInfo.ControlType.ToUpper() == "BUTTON")
                    {
                        input = new V6FormButton()
                        {
                            Name = "btn" + defineInfo.Field,
                            AccessibleName = "",
                            Text = defineInfo.TextLang(V6Setting.IsVietnamese),
                            UseVisualStyleBackColor = true
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "V6VVARTEXTBOX")
                    {
                        input = new V6VvarTextBox()
                        {
                            VVar = defineInfo.Vvar,
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "LOOKUPTEXTBOX")
                    {
                        input = new V6LookupTextBox()
                        {
                            Name = "txt" + defineInfo.Field,
                            Ma_dm = defineInfo.MA_DM, //Mã danh mục trong Aldm
                            AccessibleName = defineInfo.AccessibleName, //Trường get dữ liệu
                            AccessibleName2 = defineInfo.AccessibleName2, //Trường get text hiển thị
                            ValueField = defineInfo.Field, //Trường dữ liệu
                            ShowTextField = defineInfo.Field2, //Trường text hiển thị
                            CheckOnLeave = true,
                            CheckNotEmpty = defineInfo.NotEmpty,
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "LABEL")
                    {
                        input = new V6LabelTextBox()
                        {
                            Name = "lbt" + defineInfo.Field,
                        };
                    }
                    else if (defineInfo.ControlType.ToUpper() == "CHECKBOX")
                    {
                        input = new V6CheckBox()
                        {
                            Name = "chk" + defineInfo.Field
                        };
                    }
                    else
                    {
                        goto Next_1;
                    }

                    goto EndIf_1;
                }
            Next_1:
                if (ObjectAndString.IsDateTimeType(defineInfo.DataType))
                {
                    input = new V6DateTimeColor();
                }
                else if (ObjectAndString.IsNumberType(defineInfo.DataType))
                {
                    input = new V6NumberTextBox();
                    var nT = (V6NumberTextBox)input;
                    //nT.DecimalPlaces = defineInfo.Decimals;
                    NumberTextBox_Decimals[nT] = defineInfo.Decimals;
                }
                else
                {
                    input = new V6VvarTextBox()
                    {
                        VVar = defineInfo.Vvar,
                    };
                    var vV = (V6VvarTextBox)input;
                    if (defineInfo.ToUpper) vV.CharacterCasing = CharacterCasing.Upper;

                    var maxlength = 1;
                    if (!string.IsNullOrEmpty(defineInfo.LimitChars))
                    {
                        vV.LimitCharacters = defineInfo.LimitChars;
                        vV.MaxLength = maxlength;
                    }

                    var tT = (V6VvarTextBox)input;
                    tT.SetInitFilter(defineInfo.InitFilter);
                    tT.F2 = defineInfo.F2;
                }
            EndIf_1:

                if (input != null)
                {
                    //Thêm một số thuộc tính khác.
                    if (!(input is V6NumberTextBox) && !(input is V6DateTimeColor) && !(input is Button))
                    {
                        if (input is V6ColorTextBox)
                            V6ColorTextBox_MaxLength.Add((V6ColorTextBox)input, defineInfo.MaxLength);
                    }
                    if (input is V6ColorTextBox)
                    {
                        if (defineInfo.UseLimitChars0)
                        {
                            var tb = input as V6ColorTextBox;
                            tb.UseLimitCharacters0 = defineInfo.UseLimitChars0;
                            tb.LimitCharacters0 = defineInfo.LimitChars0;
                        }
                    }

                    //Bao lại các thuộc tính nếu chưa có.
                    if (input.AccessibleName == null) input.AccessibleName = AccessibleName_KEY;
                    if (string.IsNullOrEmpty(input.Name)) input.Name = "txt" + defineInfo.Field;
                    if (!string.IsNullOrEmpty(defineInfo.DefaultValue))
                    {
                        V6ControlFormHelper.SetControlValue(input, defineInfo.DefaultValue);
                        //input.Text = defineInfo.DefaultValue;
                    }
                    input.Enabled = defineInfo.Enabled;
                    input.Visible = defineInfo.Visible;
                    input.Width = string.IsNullOrEmpty(defineInfo.Width)
                        ? 150
                        : ObjectAndString.ObjectToInt(defineInfo.Width);
                    input.Left = 150;
                    input.Top = top;

                    panel1.Controls.Add(input);
                    Input_Controls[defineInfo.Field.ToUpper()] = input;
                    All_Objects[input.Name] = input;
                    //Lookup button
                    if (defineInfo_M.Visible)
                    {
                        LookupButton lButton = new LookupButton();
                        panel1.Controls.Add(lButton);
                        lButton.ReferenceControl = input;

                        lButton.Name = "lbt" + defineInfo.Field;

                        lButton.R_DataType = defineInfo_M.R_DataType;
                        //lButton.R_Value = defineInfo_M.R_Value;
                        //lButton.R_Vvar = defineInfo_M.R_Vvar;
                        //lButton.R_Stt_rec = defineInfo_M.R_Stt_rec;
                        lButton.R_Ma_ct = defineInfo_M.R_Ma_ct;

                        lButton.M_DataType = defineInfo_M.M_DataType;
                        lButton.M_Value = defineInfo_M.M_Value;
                        lButton.M_Vvar = defineInfo_M.M_Vvar;
                        lButton.M_Stt_Rec = defineInfo_M.M_Stt_Rec;
                        lButton.M_Ma_ct = defineInfo_M.M_Ma_ct;

                        lButton.M_Type = defineInfo_M.M_Type;
                        //lButton.M_User_id = defineInfo_M.M_User_id;
                        //lButton.M_Lan = defineInfo_M.V6Login.SelectedLanguage;

                        //lButton.LookupButtonF3Event += (sender, e) =>
                        //{
                        //    var hoaDonForm = ChungTuF3.GetChungTuControl(e.MaCt, "Name", e.Stt_rec);
                        //    hoaDonForm.ShowToForm(lButton, "Title", true, true, true);
                        //};
                    }

                    //Sự kiện của input
                    string DMETHOD = "" + row["DMETHOD"];
                    if (!string.IsNullOrEmpty(DMETHOD))
                    {
                        //Nhiều event
                        var xml = DMETHOD;
                        DataSet ds = new DataSet();
                        ds.ReadXml(new StringReader(xml));
                        if (ds.Tables.Count <= 0) break;

                        var data = ds.Tables[0];

                        foreach (DataRow event_row in data.Rows)
                        {
                            //nơi sử dụng: QuickReportManager, DynamicAddEditForm
                            try
                            {
                                var EVENT_NAME = event_row["event"].ToString().Trim().ToUpper();
                                var method_name = event_row["method"].ToString().Trim();

                                using_text2 += data.Columns.Contains("using") ? event_row["using"] : "";
                                method_text2 += data.Columns.Contains("content") ? event_row["content"] + "\n" : "";

                                //Make dynamic event and call
                                switch (EVENT_NAME)
                                {
                                    case "INIT":
                                        input.EnabledChanged += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "TEXTCHANGE":
                                    case "TEXTCHANGED":
                                        input.TextChanged += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "VALUECHANGE":
                                    case "VALUECHANGED":
                                        V6NumberTextBox numInput = input as V6NumberTextBox;
                                        if (numInput == null) break;

                                        numInput.StringValueChange += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "GOTFOCUS":
                                        input.GotFocus += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "LOSTFOCUS":
                                        input.LostFocus += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "V6LOSTFOCUS":
                                        var colorInput = input as V6ColorTextBox;
                                        if (colorInput == null) break;
                                        colorInput.V6LostFocus += (s) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["eventargs"] = null;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "KEYDOWN":
                                        input.KeyDown += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;

                                    case "CLICK":
                                        input.Click += (s, e) =>
                                        {
                                            if (Event_program2 == null) return;

                                            All_Objects["sender"] = s;
                                            All_Objects["e"] = e;
                                            V6ControlsHelper.InvokeMethodDynamic(Event_program2, method_name,
                                                All_Objects);
                                        };
                                        break;
                                } //end switch
                            }
                            catch (Exception exfor)
                            {
                                thisForm.WriteExLog(thisForm.GetType() + ".CreateFormControls ReadEventInForLoop", exfor);
                            }
                        } //end for
                    } //end if DMETHOD

                    //Add brother
                    int left = input.Right + 30;
                    if (input is V6VvarTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                    {
                        var tT = (V6VvarTextBox)input;
                        tT.BrotherFields = defineInfo.BField;
                        tT.BrotherFields2 = defineInfo.BField2;
                        if (!string.IsNullOrEmpty(defineInfo.ShowName)) tT.ShowName = defineInfo.ShowName == "1";
                        var txtB = new V6LabelTextBox();
                        txtB.Name = "txt" + defineInfo.BField;
                        txtB.AccessibleName = defineInfo.BField;
                        txtB.Top = top;
                        txtB.Left = left;
                        //txtB.Width = 300;
                        txtB.AutoSize = true;
                        txtB.ReadOnly = true;
                        txtB.TabStop = false;

                        All_Objects[txtB.Name] = txtB;
                        panel1.Controls.Add(txtB);
                        left = txtB.Right + 10;
                    }
                    if (input is V6LookupTextBox && !string.IsNullOrEmpty(defineInfo.BField))
                    {
                        var tT = (V6LookupTextBox)input;
                        tT.BrotherFields = defineInfo.BField;
                        var txtB = new V6LabelTextBox();
                        txtB.Name = "txt" + defineInfo.BField;
                        txtB.AccessibleName = defineInfo.BField;
                        txtB.Top = top;
                        txtB.Left = left;
                        txtB.Width = panel1.Width - txtB.Left - 10;
                        txtB.ReadOnly = true;
                        txtB.TabStop = false;

                        All_Objects[txtB.Name] = txtB;
                        panel1.Controls.Add(txtB);
                        left = txtB.Right + 10;
                    }
                    //Add description
                    var description = defineInfo.DescriptionLang(V6Setting.IsVietnamese);
                    if (!string.IsNullOrEmpty(description))
                    {
                        var labelD = new V6Label();
                        labelD.Name = "lblD" + defineInfo.Field;
                        labelD.AutoSize = true;
                        labelD.Left = left;
                        labelD.Top = top;
                        labelD.Text = description;
                        panel1.Controls.Add(labelD);
                        All_Objects[labelD.Name] = labelD;
                    }
                }
                i++;
            }
            Event_program2 = V6ControlsHelper.CreateProgram("EventNameSpace", "EventClass", "D" + ma_bc, using_text2,
                method_text2);
        }


        public static void OpenFileProcess(string file)
        {
            file = Path.GetFullPath(file);
            if (File.Exists(file))
            {
                Process.Start(file);
            }
            else
            {
                ShowMainMessage(V6Text.NotExist + " " + file);
            }
        }
    }
}
