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
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
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
using Data_Table = V6Tools.V6Export.Data_Table;

namespace V6Controls.Forms
{   
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

        #region ==== SHOW HIDE MESSAGE ====

        public static V6Label MessageLable;
        private static Timer _mainMessageTimer;
        private static int _mainTime = -1;
        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên phải chương trình.
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMainMessage(string message)
        {
            if (_mainMessageTimer != null && _mainMessageTimer.Enabled)
            {
                _mainMessageTimer.Stop();
                //MessageLable.Top 
            }

            MessageLable.Text = message;
            _mainMessageTimer = new Timer {Interval = 200};
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

        public static void ShowHelp(string itemId, string title = "")
        {
            var file = Path.Combine(Application.StartupPath, "V6HELP\\" + itemId + ".pdf");
            if (!File.Exists(file)) return;
            var ext = Path.GetExtension(file).ToLower();
            if (ext == ".pdf")
            {
                var view = new PdfiumViewerForm(file, title);
                view.Show();
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
        public static V6VeticalLable lblMenuMain { get; set; }
        //private static Timer _hide, _show;
        private static V6VeticalLable _sender;
        private static Control _menuPanel, _menuShowControl, _viewControl, _containerControl;
        private static Point _menuPanelLocation;
        private static bool _checkMove;
        private static bool _isMoving;
        private static string _selectedText = "";
        static Timer _hide, _show;

        public static void SetHideMenuLabel(V6VeticalLable label, string selectedText)
        {
            _sender = label;
            _selectedText = selectedText;
            if(_sender != null)
                _sender.HideText = _selectedText;
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

        public static void ShowHideMenu(V6VeticalLable sender, string selectText,
            Control panelMenuControl, Control panelMenuShowControl, Control panelViewControl,
            Control container, Point panelMenuLocation, bool isShow)
        {
            if (_isMoving) return;

            
            _sender = sender;
            _selectedText = selectText;
            _menuPanel = panelMenuControl;
            _menuShowControl = panelMenuShowControl;
            _viewControl = panelViewControl;
            _containerControl = container;
            _menuPanelLocation = panelMenuLocation;
            if (isShow)
            {
                _show = new Timer();
                _show.Tick += show_Tick;
                _show.Start();
            }
            else
            {
                _viewControl.Width = _containerControl.Width - 25;
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
                _sender.IsShowing = true;
                _viewControl.Width = _containerControl.Width - _menuPanel.Width - 25;
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
                ((Timer)sender).Stop();
                _isMoving = false;
                _sender.IsShowing = false;
                _sender.HideText = _selectedText;
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
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
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

                        c = CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus, carry);

                        break;
                    case "N4"://Gia nt

                        decimals = V6Options.M_IP_GIA_NT;

                        c = CreateNumberSoLuong(fcolumn, fcaption, decimals, width, fstatus, carry);

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
        /// <param name="control"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetFormValue(Control control, string name)
        {
            try
            {
                if (control == null) return null;
                object result = null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && control.AccessibleName.ToUpper() == name.ToUpper())
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
                        object o = GetFormValue(c, name);
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
        /// Tìm một control trên form (hoặc control) thông qua Name
        /// Nếu không có trả về null
        /// </summary>
        /// <param name="control">Control chứa hoặc chính control cần tìm.</param>
        /// <param name="name">Name của control cần tìm</param>
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
        /// Tìm một control trên form (hoặc control) thông qua accesibleName
        /// Nếu không có trả về null
        /// </summary>
        /// <param name="control">Control chứa hoặc chính control cần tìm.</param>
        /// <param name="accesibleName">AccessibleName của control cần tìm</param>
        /// <returns></returns>
        public static Control GetControlByAccesibleName(Control control, string accesibleName)
        {
            try
            {
                if (control == null) return null;

                if (!string.IsNullOrEmpty(control.AccessibleName)
                    && string.Equals(control.AccessibleName, accesibleName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return control;
                }
                return control.Controls.Count > 0 ?
                    (from Control c in control.Controls select GetControlByAccesibleName(c, accesibleName)).FirstOrDefault(o => o != null)
                    : null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetFormControl error!\n" + ex.Message);
            }
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
                        var color = control as V6DateTimeColor;
                        if (color != null)
                        {
                            d.Add(cNAME, color.Value);
                        }
                        else if (control is V6IndexComboBox)
                        {
                            d.Add(cNAME, ((V6IndexComboBox)control).SelectedIndex);
                        }
                        else if (control is V6ComboBox)
                        {
                            var cbo = control as V6ComboBox;
                            d.Add(cNAME, cbo.DataSource != null ? cbo.SelectedValue : cbo.Text);
                        }
                        else if (control is ComboBox)
                        {
                            var cbo = control as ComboBox;
                            d.Add(cNAME, cbo.DataSource != null ? cbo.SelectedValue : cbo.Text);
                        }
                        else if (control is PictureBox)
                        {
                            var pic = control as PictureBox;
                            d.Add(cNAME, Picture.ToJpegByteArray(pic.Image));
                        }
                        else if (control is V6NumberTextBox)
                        {
                            d.Add(cNAME, ((V6NumberTextBox)control).Value);
                        }
                        else if (control is V6CheckTextBox)
                        {
                            d.Add(cNAME, ((V6CheckTextBox)control).StringValue);
                        }
                        else if (control is CheckBox)
                        {
                            d.Add(cNAME, ((CheckBox)control).Checked ? 1 : 0);
                        }
                        else if (control is RadioButton)
                        {
                            if (((RadioButton)control).Checked)
                                d.Add(cNAME, control.Text);
                        }
                        else if (control is GioiTinhControl)
                        {
                            d.Add(cNAME, ((GioiTinhControl) control).Value);
                        }
                        else if (control is V6DateTimePick)
                        {
                            d.Add(cNAME, ((V6DateTimePick)control).Value);
                        }
                        else if (control is DateTimePicker)
                        {
                            d.Add(cNAME, ((DateTimePicker)control).Value);
                        }
                        else
                        {
                            d.Add(cNAME, control.Text);
                        }

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
        /// Tạo filterLine control từ chuỗi thông tin
        /// </summary>
        /// <param name="define">Chuỗi thông tin (field:ngay_ct1;textv:Từ ngày;textE:From;where_field:ngay_ct;type:D;loai_key:10;oper:and;sqltype:smalldatetime;limitchar:ABCabc123;defaultValue:m_ngay_ct1)</param>
        /// <returns></returns>
        public static FilterLineDynamic MadeLineDynamicControl(string define)
        {
            //string define = row["filter"].ToString().Trim();
            DefineInfo lineInfo = new DefineInfo(define);
            if (string.IsNullOrEmpty(lineInfo.Field)) return null;

            var lineControl = new FilterLineDynamic
            {
                FieldName = lineInfo.Field.ToUpper(),
                FieldCaption = V6Setting.IsVietnamese ? lineInfo.TextV : lineInfo.TextE,
                DefineInfo = lineInfo,
                Enabled = lineInfo.Enabled,
                Visible = lineInfo.Visible,

            };

            V6VvarTextBox vT = null;

            if (ObjectAndString.IsDateTimeType(lineInfo.DataType))
            {
                lineControl.AddDateTimePick();
            }
            else if (ObjectAndString.IsNumberType(lineInfo.DataType))
            {
                lineControl.AddNumberTextBox();
            }
            else// if (ObjectAndString.IsStringType(lineInfo.DataType))
            {
                if (string.IsNullOrEmpty(lineInfo.Vvar))
                {
                    lineControl.AddTextBox();
                }
                else
                {
                    vT = lineControl.AddVvarTextBox(lineInfo.Vvar, lineInfo.InitFilter);
                    //
                    vT.F2 = lineInfo.F2;
                }
            }
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

        #region ==== SET... ====
        /// <summary>
        /// Gán value cho vài control anh em trên form có AccessibleName nằm trong list
        /// </summary>
        /// <param name="control"></param>
        /// <param name="row"></param>
        /// <param name="fields"></param>
        public static void SetBrotherData(Control control, DataRow row, string fields)
        {
            if (string.IsNullOrEmpty(fields)) return;
            fields = "," + fields.ToLower() + ",";
            try
            {
                if (row == null) return;
                Control parent = control.Parent;
                if (parent != null)
                {
                    foreach (Control c in parent.Controls)
                    {
                        var NAME = c.AccessibleName;
                        if (!string.IsNullOrEmpty(NAME)
                            && row.Table.Columns.Contains(NAME) && fields.Contains("," + NAME.ToLower() + ","))
                        {
                            var color = c as V6DateTimeColor;
                            if (color != null)
                            {
                                color.Value = ObjectAndString.ObjectToDate(row[NAME]);
                            }
                            else if (c is V6IndexComboBox)
                            {
                                ((V6IndexComboBox)control).SelectedIndex = ObjectAndString.ObjectToInt(row[NAME]);
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                var value = ObjectAndString.ObjectToString(row[NAME]);
                                if (com.Items.Contains(value))
                                {
                                    com.SelectedText = value;
                                }
                                else
                                {
                                    com.Items.Add(value);
                                    com.SelectedText = value;
                                }
                            }
                            else if (c is V6DateTimePick)
                            {
                                var object_to_date = ObjectAndString.ObjectToDate(row[NAME]);
                                if (object_to_date != null)
                                    ((V6DateTimePick)c).Value = (DateTime)object_to_date;
                            }
                            else if (c is DateTimePicker)
                            {
                                var object_to_date = ObjectAndString.ObjectToDate(row[NAME]);
                                if (object_to_date != null)
                                    ((DateTimePicker)c).Value = (DateTime) object_to_date;
                            }
                            else if (c is V6NumberTextBox)
                            {
                                ((V6NumberTextBox)c).Value = ObjectAndString.ObjectToDecimal(row[NAME]);
                            }
                            else if (c is V6CheckTextBox)
                            {
                                ((V6CheckTextBox)c).SetStringValue(ObjectAndString.ObjectToString(row[NAME]));
                            }
                            else if (c is CheckBox)
                            {
                                string value = row[NAME].ToString().Trim();
                                if (value == "1" || value.ToLower() == "true")
                                {
                                    ((CheckBox)c).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox)c).Checked = false;
                                }
                            }
                            else if (c is RadioButton)
                            {
                                if (row[NAME].ToString().Trim() == c.Text)
                                {
                                    ((RadioButton)c).Checked = true;
                                }
                            }
                            else if (control is GioiTinhControl)
                            {
                                ((GioiTinhControl)control).Value = row[NAME].ToString().Trim();
                            }
                            else
                            {
                                c.Text = row[NAME].ToString().Trim();
                            }
                        }
                        else if (!string.IsNullOrEmpty(c.AccessibleName) &&
                                 fields.Contains("," + c.AccessibleName.ToLower() + ","))
                        {
                            //Gán rỗng hoặc mặc định
                            if (c is V6IndexComboBox)
                            {
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else if (c is V6DateTimePick)
                            {
                                ((V6DateTimePick)c).Value = DateTime.Now;
                            }
                            else if (c is DateTimePicker)
                            {
                                ((DateTimePicker) c).Value = DateTime.Now;
                            }
                            else if (c is V6NumberTextBox)
                            {
                                ((V6NumberTextBox) c).Value = 0;
                            }
                            else if (c is CheckBox)
                            {
                                ((CheckBox) c).Checked = false;
                            }
                            else if (c is RadioButton)
                            {
                                ((RadioButton) c).Checked = false;
                            }
                            else if (control is GioiTinhControl)
                            {
                                ((GioiTinhControl)control).Value = "";
                            }
                            else
                            {
                                c.Text = "";
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
                if (row == null) return;
                Control parent = control.Parent;
                if (parent != null)
                {
                    foreach (Control c in parent.Controls)
                    {
                        var aNAME = c.AccessibleName;
                        if (string.IsNullOrEmpty(aNAME)) continue;
                        aNAME = aNAME.ToUpper();
                        if (!neighbor_field.ContainsKey(aNAME)) continue;
                        var fieldName = neighbor_field[aNAME];

                        if (row.Table.Columns.Contains(fieldName))
                        {
                            var color = c as V6DateTimeColor;
                            if (color != null)
                            {
                                color.Value = ObjectAndString.ObjectToDate(row[fieldName]);
                            }
                            else if (c is V6IndexComboBox)
                            {
                                ((V6IndexComboBox) control).SelectedIndex = ObjectAndString.ObjectToInt(row[fieldName]);
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                var value = ObjectAndString.ObjectToString(row[fieldName]);
                                if (com.Items.Contains(value))
                                {
                                    com.SelectedText = value;
                                }
                                else
                                {
                                    com.Items.Add(value);
                                    com.SelectedText = value;
                                }
                            }
                            else if (c is V6DateTimePick)
                            {
                                var object_to_date = ObjectAndString.ObjectToDate(row[fieldName]);
                                if (object_to_date != null)
                                    ((V6DateTimePick) c).Value = (DateTime) object_to_date;
                            }
                            else if (c is DateTimePicker)
                            {
                                var object_to_date = ObjectAndString.ObjectToDate(row[fieldName]);
                                if (object_to_date != null)
                                    ((DateTimePicker) c).Value = (DateTime) object_to_date;
                            }
                            else if (c is V6NumberTextBox)
                            {
                                ((V6NumberTextBox) c).Value = ObjectAndString.ObjectToDecimal(row[fieldName]);
                            }
                            else if (c is V6CheckTextBox)
                            {
                                ((V6CheckTextBox) c).SetStringValue(ObjectAndString.ObjectToString(row[fieldName]));
                            }
                            else if (c is CheckBox)
                            {
                                string value = row[fieldName].ToString().Trim();
                                if (value == "1" || value.ToLower() == "true")
                                {
                                    ((CheckBox) c).Checked = true;
                                }
                                else
                                {
                                    ((CheckBox) c).Checked = false;
                                }
                            }
                            else if (c is RadioButton)
                            {
                                if (row[fieldName].ToString().Trim() == c.Text)
                                {
                                    ((RadioButton) c).Checked = true;
                                }
                            }
                            else if (control is GioiTinhControl)
                            {
                                ((GioiTinhControl)control).Value = row[fieldName].ToString().Trim();
                            }
                            else
                            {
                                c.Text = row[fieldName].ToString().Trim();
                            }

                        }
                        else // Có trong neighbor nhưng không có trong data
                        {
                            //Gán rỗng hoặc mặc định
                            if (c is V6IndexComboBox)
                            {
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else if (c is ComboBox)
                            {
                                //!!! can mo rong nhieu loai combobox
                                var com = c as ComboBox;
                                if (com.Items.Count > 0)
                                    com.SelectedIndex = 0;
                            }
                            else if (c is V6DateTimePick)
                            {
                                ((V6DateTimePick)c).Value = DateTime.Now;
                            }
                            else if (c is DateTimePicker)
                            {
                                ((DateTimePicker) c).Value = DateTime.Now;
                            }
                            else if (c is V6NumberTextBox)
                            {
                                ((V6NumberTextBox) c).Value = 0;
                            }
                            else if (c is CheckBox)
                            {
                                ((CheckBox) c).Checked = false;
                            }
                            else if (c is RadioButton)
                            {
                                ((RadioButton) c).Checked = false;
                            }
                            else if (control is GioiTinhControl)
                            {
                                ((GioiTinhControl)control).Value = "";
                            }
                            else
                            {
                                c.Text = "";
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
        /// Set các control nhập liệu thành readonly (hoặc không)
        /// </summary>
        /// <param name="control"></param>
        /// <param name="readonli"></param>
        public static void SetFormControlsReadOnly(Control control, bool readonli)
        {
            var tagString = string.Format(";{0};", control.Tag ?? "");

            var cancelall = control is DataGridView || tagString.Contains(";cancelall;");
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
                txt.ReadOnly = readonli || readonl2;
                if (disable) txt.Enabled = false;
                if (enable) txt.Enabled = true;
            }
            else if (control is V6DateTimePick)
            {
                var dat = control as V6DateTimePick;
                dat.ReadOnly = (readonli || readonl2);
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
                control.Enabled = !(readonli || readonl2);
                if (disable) control.Enabled = false;
                if (enable) control.Enabled = true;
            }
            else if (control is DataGridView)
            {
                var dgv = (DataGridView)control;
                dgv.ReadOnly = readonli || readonl2;

                if (disable) control.Enabled = false;
                if (enable) control.Enabled = true;
            }
            else if (control is RichTextBox)
            {
                ((RichTextBox)control).ReadOnly = readonli || readonl2;
            }
            
            CANCEL:
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    SetFormControlsReadOnly(c, readonli);
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
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var canceldata = tagString != "" && tagString.Contains(";canceldata;");
                if (canceldata) goto CANCELALL;

                {
                    #region === Gán giá trị ===

                    var box = control as V6ColorTextBox;
                    if (box != null)
                    {
                        //Gồm cả vvar, number va date (override)
                        box.CarryValue();
                    }
                    #endregion gán giá trị
                }

                //CANCEL:
                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetCarryValuesRecursive(c);
                    }
                }
            CANCELALL: ;
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
        /// Cancel all không còn tác dụng, còn canceldata
        /// </summary>
        /// <param name="control">Form cần điền dữ liệu, thường dùng từ khóa this</param>
        /// <param name="data">Lưu ý. nên dùng key UPPER</param>
        /// <param name="set_default">Nếu không có dữ liệu thì gán rỗng hoặc mặc định.</param>
        public static void SetFormDataDictionary(Control control, SortedDictionary<string, object> data, bool set_default = true)
        {
            try
            {
                _errors = "";
                SetFormDataDicRecursive(control, data, set_default);
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
        }
        
        /// <summary>
        /// Gán value cho một số control có AccessibleName trùng data
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        public static void SetSomeDataDictionary(Control control, IDictionary<string, object> data)
        {
            try
            {
                _errors = "";
                SetFormDataDicRecursive(control, data, false);
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
        }

        private static void SetFormDataDicRecursive(Control control, IDictionary<string, object> data, bool set_default = true)
        {
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var cancelall = control is DataGridView || tagString.Contains(";cancelall;");
                var canceldata = tagString.Contains(";canceldata;");
                var cancelset = tagString.Contains(";cancelset;");
                if (canceldata||cancelset||cancelall) goto CANCELALL;
                
                var NAME = control.AccessibleName;
                if (data != null && !string.IsNullOrEmpty(NAME) && data.ContainsKey(NAME.ToUpper()))
                {
                    NAME = NAME.ToUpper();

                    #region === Gán giá trị ===

                    var color = control as V6DateTimeColor;
                    if (color != null)
                    {
                        color.Value = ObjectAndString.ObjectToDate(data[NAME]);
                    }
                    else if (control is V6IndexComboBox)
                    {
                        ((V6IndexComboBox) control).SelectedIndex = ObjectAndString.ObjectToInt(data[NAME]);
                    }
                    else if (control is ComboBox)
                    {
                        var com = control as ComboBox;
                        try
                        {
                            var VALUE = ObjectAndString.ObjectToString(data[NAME]).Trim();
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
                            var objectData = data[NAME];
                            Image picture = null;
                            if (objectData is Image) picture = (Image) objectData;
                            else if (objectData is byte[]) picture = Picture.ByteArrayToImage((byte[])objectData);

                            pic.Image = picture;
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteToLog("V6ControlFormHelper.SetFormDataDicRecusive PictureBox " + ex.Message);
                        }
                    }
                    else if (control is V6DateTimePick)
                    {
                        var object_to_date = ObjectAndString.ObjectToDate(data[NAME]);
                        if (object_to_date != null)
                            ((V6DateTimePick) control).Value = (DateTime) object_to_date;
                    }
                    else if (control is DateTimePicker)
                    {
                        var object_to_date = ObjectAndString.ObjectToDate(data[NAME]);
                        if (object_to_date != null)
                            ((DateTimePicker) control).Value = (DateTime) object_to_date;
                    }
                    else if (control is V6VvarTextBox) //!!!!.ChangeText()????
                    {
                        var vvarTextBox = control as V6VvarTextBox;
                        vvarTextBox.SetDataRow(null);
                        var text = ObjectAndString.ObjectToString(data[NAME]).Trim();
                        if (vvarTextBox.UseChangeTextOnSetFormData)
                            vvarTextBox.ChangeText(text);
                        else vvarTextBox.Text = text;
                    }
                    else if (control is V6NumberTextBox)
                    {
                        var text_box = control as V6NumberTextBox;
                        var value = ObjectAndString.ObjectToDecimal(data[NAME]);
                        if (text_box.UseChangeTextOnSetFormData)
                            text_box.ChangeValue(value);
                        else text_box.Value = value;
                    }
                    else if (control is V6CheckTextBox)
                    {
                        ((V6CheckTextBox) control).SetStringValue(ObjectAndString.ObjectToString(data[NAME]));
                    }
                    else if (control is V6ColorTextBox)
                    {
                        var text_box = control as V6ColorTextBox;
                        var text = ObjectAndString.ObjectToString(data[NAME]).Trim();
                        if (text_box.UseChangeTextOnSetFormData)
                            text_box.ChangeText(text);
                        else text_box.Text = text;
                    }
                    else if (control is CheckBox)
                    {
                        string value = data[NAME].ToString().Trim();
                        if (value == "1" || value.ToLower() == "true")
                        {
                            ((CheckBox) control).Checked = true;
                        }
                        else
                        {
                            ((CheckBox) control).Checked = false;
                        }
                    }
                    else if (control is RadioButton)
                    {
                        if (data[NAME].ToString().Trim() == control.Text)
                        {
                            ((RadioButton) control).Checked = true;
                        }
                    }
                    else if (control is GioiTinhControl)
                    {
                        ((GioiTinhControl) control).Value = data[NAME].ToString().Trim();
                    }
                    else
                    {
                        control.Text = ObjectAndString
                            .ObjectToString(data[NAME]).Trim();
                    }
                }
                #endregion gán giá trị

                else if (set_default && !string.IsNullOrEmpty(NAME))
                {
                    #region === Gán rỗng hoặc mặc định ===

                    var con = control as V6DateTimeColor;
                    if (con != null)
                    {
                        con.Value = null;
                    }
                    var com = control as ComboBox;
                    if (com != null)
                    {
                        try
                        {
                            //!!! can mo rong nhieu loai combobox
                            if (com.Items.Count > 0)
                                com.SelectedIndex = -1;
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    else if (control is V6DateTimePick)
                    {
                        ((V6DateTimePick) control).Value = DateTime.Now;
                    }
                    else if (control is DateTimePicker)
                    {
                        ((DateTimePicker) control).Value = DateTime.Now;
                    }
                    else if (control is V6VvarTextBox)
                    {
                        ((V6VvarTextBox) control).SetDataRow(null);
                        ((V6VvarTextBox) control).Clear();
                    }
                    else if (control is V6NumberTextBox)
                    {
                        ((V6NumberTextBox) control).Value = 0;
                    }
                    else if (control is CheckBox)
                    {
                        ((CheckBox) control).Checked = false;
                    }
                    else if (control is RadioButton)
                    {
                        ((RadioButton) control).Checked = false;
                    }
                    else if (control is GioiTinhControl)
                    {
                        ((GioiTinhControl) control).Value = "";
                    }
                    else
                    {
                        control.Text = "";
                    }

                    #endregion gán rỗng
                }

                if (control.Controls.Count > 0)
                {
                    foreach (Control c in control.Controls)
                    {
                        SetFormDataDicRecursive(c, data, set_default);
                    }
                }
            CANCELALL: ;
            }
            catch (Exception ex)
            {
                _errors += "\r\nAccessibleName: " + control.AccessibleName
                    + "\r\nException: " + ex.Message;
            }
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
                var cancelall = control is DataGridView || tagString.Contains(";cancelall;");
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
                        //try
                        //{
                        //    var key = new SortedDictionary<string, object> { { "ma_dm", ma_dm } };
                        //    var selectResult = V6BusinessHelper.Select(V6TableName.Altt, key, "", "", "");
                        //    SetFormInfo(control, selectResult.Data, V6Setting.Language);
                        //}
                        //catch (Exception ex)
                        //{
                        //    control.ShowErrorException(parent.GetType() + ".Load user define info error!", ex);
                        //}
                    };
                    f.ShowDialog();
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

                var cancelall = control is DataGridView || tagString.Contains(";cancelall;");
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
                                        = Convert.ToInt32(V6Options.V6OptionValues[num.NumberFormatName]);
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
                                    num.DecimalPlaces = Convert.ToInt32(V6Options.V6OptionValues[num.NumberFormatName]);
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

                if (V6Options.V6OptionValues == null || V6Options.V6OptionValues.Count == 0) return;
                if (!V6Options.V6OptionValues.ContainsKey("M_TRAN_LANG") || V6Options.V6OptionValues["M_TRAN_LANG"] != "1") return;
                
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
                VVar = vvar, CheckOnLeave = checkOnLeave, CheckNotEmpty = checkNotEmpty,
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
        public static V6DateTimePick CreateDateTimePick(string accessibleName, string caption, int width, bool visible, bool carry = false)
        {
            return new V6DateTimePick
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
                    + "\r\nException: " + ex.Message
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
        /// Chọn máy in
        /// </summary>
        /// <param name="rpDoc"></param>
        /// <param name="printerName">Máy in chọn sẵn.</param>
        /// <returns>Tên máy in đã chọn in.</returns>
        public static string PrintRpt(ReportDocument rpDoc, string printerName)
        {
            PrintDialog pt = new PrintDialog();
            pt.PrinterSettings.PrinterName = printerName;
            pt.AllowPrintToFile = false;
            pt.AllowCurrentPage = true;
            pt.AllowSomePages = true;
            if (pt.ShowDialog() == DialogResult.OK)
            {
                PrintRptToPrinter(rpDoc,
                    pt.PrinterSettings.PrinterName,
                    pt.PrinterSettings.Copies,
                    pt.PrinterSettings.FromPage,
                    pt.PrinterSettings.ToPage);
                return pt.PrinterSettings.PrinterName;
            }
            return null;
        }
        public static void PrintRptToPrinter(ReportDocument rpDoc, string printerName, int copies, int startPage = 0, int endPage = 0)
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
                }
            }
            catch (Exception)
            {
                PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                throw;
            }
            PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
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
            if (control.Tag != null)
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
                string keyString = keyData.ToString();
                //SetStatusText(keyString);//Test !!!!!!!!!!!
                return ClickByTag(container, keyString);
            }
            catch
            {
                return false;
            }
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
            dgv.AutoGenerateColumns = false;
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

        public static void ExportExcelTemplate(DataTable data, DataTable tbl2,
            SortedDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
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
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var albc_row = Albc.GetRow(MAU, LAN, ReportFile);
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
                                                if (ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && tbl2 != null && tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (tbl2.Columns.Contains(matchKey))
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
                                            if (tbl2.Columns.Contains(content))
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

                            if (Data_Table.ToExcelTemplate(
                                ExcelTemplateFileFull, data, save.FileName, firstCell,
                                excelColumns.Replace("[", "").Replace("]", "").Split(excelColumns.Contains(";") ? ';' : ','),
                                parameters, V6Setting.V6_number_format_info,
                                insertRow, drawLine))
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + Data_Table.Message);
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
        }
        
        public static void ExportExcelTemplateD(DataTable data, DataTable tbl2, string MODE,
            SortedDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string ExcelTemplateFileFull, string defaultSaveName, string excelColumns, string excelHeaders)
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
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var albc_row = Albc.GetRow(MAU, LAN, ReportFile);
                        if (albc_row != null)
                        {
                            var firstCell = "A4"; //auto
                            bool drawLine = true, insertRow = true;
                            var xml_field = MODE == "V" ? "EXCEL2_VIEW" : "EXCEL2";
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
                                                if (ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && tbl2 != null && tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (tbl2.Columns.Contains(matchKey))
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
                                            if (tbl2.Columns.Contains(content))
                                            {
                                                parameters.Add(KEY, tbl2_row[content]);
                                            }
                                        }
                                    }
                                }

                                // Add parameter for Excel Columns Name
                                var firstCellRowIndex = Excel_File.GetExcelRow(firstCell);
                                var firstCellColIndex = Excel_File.GetExcelColumn(firstCell);
                                var headers = excelHeaders.Split(excelHeaders.Contains(";") ? ';' : ',');
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    var key = "" + Excel_File.ToExcelColumnString(firstCellColIndex + i + 1) + firstCellRowIndex;
                                    parameters.Add(key,headers[i]);
                                }

                            }
                            else
                            {
                                //Không có thông tin xml
                            }

                            if (Data_Table.ToExcelTemplate(
                                ExcelTemplateFileFull, data, save.FileName, firstCell,
                                excelColumns.Replace("[", "").Replace("]", "").Split(excelColumns.Contains(";") ? ';' : ','),
                                parameters, V6Setting.V6_number_format_info,
                                insertRow, drawLine))
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + Data_Table.Message);
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
        }
        
        public static void ExportExcelTemplateHTKK(DataTable data, DataTable tbl2,
            SortedDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string excelTemplateFile, string saveFileName)
        {
            
            if (data == null)
            {
                return;
            }
            if (!File.Exists(excelTemplateFile))
            {
                ShowWarningMessage("Không có file mẫu: " + excelTemplateFile);
                //return;
            }
            try
            {
                SortedDictionary<string, DataTable> datas = new SortedDictionary<string, DataTable>();
                
                if (!string.IsNullOrEmpty(saveFileName))
                {
                    try
                    {
                        var albc_row = Albc.GetRow(MAU, LAN, ReportFile);
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
                                            DataTable newData1 = data.Filter("ma_nh='1'");
                                            datas.Add(content, newData1);
                                        }
                                        else if (KEY == "C2")
                                        {
                                            DataTable newData2 = data.Filter("ma_nh='2'");
                                            datas.Add(content, newData2);
                                        }
                                        else if (KEY == "C3")
                                        {
                                            DataTable newData3 = data.Filter("ma_nh='3'");
                                            datas.Add(content, newData3);
                                        }
                                        else if (KEY == "C4")
                                        {
                                            DataTable newData4 = data.Filter("ma_nh='4'");
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
                                                if (ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && tbl2 != null && tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (tbl2.Columns.Contains(matchKey))
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
                                            if (tbl2.Columns.Contains(content))
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

                            
                            if (Data_Table.ToExcelTemplateHTKK(
                                excelTemplateFile, parameters, datas, excelColumnsHTKK.Split(','),
                                saveFileName, V6Setting.V6_number_format_info, insertRow, drawLine))
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + Data_Table.Message);
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
        }
        
        
        public static void ExportExcelTemplateONLINE(DataTable data, DataTable tbl2,
            SortedDictionary<string, object> ReportDocumentParameters, string MAU, string LAN,
            string ReportFile, string excelTemplateFile, string saveFileName)
        {
            
            if (data == null)
            {
                return;
            }
            if (!File.Exists(excelTemplateFile))
            {
                ShowWarningMessage("Không có file mẫu: " + excelTemplateFile);
                //return;
            }
            try
            {
                SortedDictionary<string, DataTable> datas = new SortedDictionary<string, DataTable>();
                
                if (!string.IsNullOrEmpty(saveFileName))
                {
                    try
                    {
                        var albc_row = Albc.GetRow(MAU, LAN, ReportFile);
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
                                            DataTable newData1 = data.Filter("ma_nh='1'");
                                            datas.Add(content, newData1);
                                        }
                                        else if (KEY == "C2")
                                        {
                                            DataTable newData2 = data.Filter("ma_nh='2'");
                                            datas.Add(content, newData2);
                                        }
                                        else if (KEY == "C3")
                                        {
                                            DataTable newData3 = data.Filter("ma_nh='3'");
                                            datas.Add(content, newData3);
                                        }
                                        else if (KEY == "C4")
                                        {
                                            DataTable newData4 = data.Filter("ma_nh='4'");
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
                                                if (ReportDocumentParameters.ContainsKey(MATCH_KEY))
                                                    content = content.Replace(match.Groups[0].Value,
                                                        ObjectAndString.ObjectToString(
                                                            ReportDocumentParameters[MATCH_KEY]));
                                            }
                                            parameters.Add(KEY, content);
                                        }
                                        else
                                        {
                                            var P_KEY = content.ToUpper();
                                            if (ReportDocumentParameters.ContainsKey(P_KEY))
                                            {
                                                parameters.Add(KEY, ReportDocumentParameters[P_KEY]);
                                            }
                                        }
                                    }
                                    else if (type == "2" && tbl2 != null && tbl2.Rows.Count > 0)//Lay value trong tbl2
                                    {
                                        var tbl2_row = tbl2.Rows[0];

                                        if (content.Contains("{") && content.Contains("}"))
                                        {
                                            var regex = new Regex("{(.+?)}");
                                            foreach (Match match in regex.Matches(content))
                                            {
                                                var matchKey = match.Groups[1].Value;
                                                if (tbl2.Columns.Contains(matchKey))
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
                                            if (tbl2.Columns.Contains(content))
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

                            
                            if (Data_Table.ToExcelTemplateHTKK(
                                excelTemplateFile, parameters, datas, excelColumnsONLINE.Split(','),
                                saveFileName, V6Setting.V6_number_format_info, insertRow, drawLine))
                            {
                                ShowInfoMessage(V6Text.ExportFinish, 500);
                            }
                            else
                            {
                                ShowInfoMessage(V6Text.ExportFail + Data_Table.Message);
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
                //dgv.HideAllColumns();
                //Hide some columns
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    var show = false;
                    foreach (string show_column in showColumns)
                    {
                        if (column.DataPropertyName.ToUpper() == show_column.ToUpper())
                        {
                            show = true;
                            break;
                        }
                    }
                    if (!show)
                    {
                        column.Visible = column.Frozen;
                    }
                }

                dgv.AutoGenerateColumns = false;
                var index = dgv.Columns.Cast<DataGridViewColumn>().Count(column => column.Frozen && column.Visible);
                for (int i = 0; i < showColumns.Length; i++)
                {
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
                                column.DefaultCellStyle.Format = fff[0];
                                if (fff.Length > 1)
                                    column.Width = ObjectAndString.ObjectToInt(fff[1]);
                                if (fff.Length > 2)
                                    column.ReadOnly = "R" == fff[2];
                            }
                            else if (column.ValueType == typeof (DateTime))
                            {
                                if (fff.Length > 0)
                                    column.Width = ObjectAndString.ObjectToInt(fff[0].Substring(1));
                                if (fff.Length > 1)
                                    column.ReadOnly = "R" == fff[1];
                            }
                            else if (column.ValueType == typeof (string))
                            {
                                if (fff.Length > 0)
                                    column.Width = ObjectAndString.ObjectToInt(fff[0].Substring(1));
                                if (fff.Length > 1)
                                    column.ReadOnly = "R" == fff[1];
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
        /// Định dạng gridview, sắp xếp thứ tự cột, gán header, format string.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="showFields">cách nhau bởi (,) hoặc (;)</param>
        /// <param name="formatStrings"></param>
        /// <param name="headerString"></param>
        public static void FormatGridViewAndHeader(DataGridView dgv, string showFields, string formatStrings, string headerString)
        {
            if (string.IsNullOrEmpty(showFields)) return;
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
                        if (back_color)
                        {
                            row.DefaultCellStyle.BackColor = color;
                        }
                    }
                }
            }
        }

        public static void FormatGridViewHideColumns(V6ColorDataGridView dataGridView1, string Mact)
        {
            if (!V6Login.IsAdmin)
            {
                dataGridView1.AutoGenerateColumns = false;
                var alctct = V6BusinessHelper.GetAlctCt(Mact);
                if (alctct != null && alctct.Rows.Count > 0)
                {
                    var GRD_HIDE = alctct.Rows[0]["GRD_HIDE"].ToString();
                    dataGridView1.HideColumns(ObjectAndString.SplitString(GRD_HIDE));
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

        /// <summary>
        /// Cập nhập dữ liệu lên một dòng của GridView
        /// </summary>
        /// <param name="row"></param>
        /// <param name="data"></param>
        public static void UpdateGridViewRow(DataGridViewRow row, SortedDictionary<string, object> data)
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
        public static void UpdateDKlistAll(SortedDictionary<string, object> dataAM, string[] fields, DataTable AD)
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
            string keyField1,string keyField2,string keyField3,
            string newKey1, string newKey2, string newKey3,
            string oldKey1, string oldKey2, string oldKey3,
            string uid)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@tableName", tableName.ToString()), 
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
        /// <param name="owner">Form đang gọi để chống chìm dialog.</param>
        public static void ShowDataEditorForm(DataTable data, string tableName, string showFields, string keys,
            bool allowAdd, bool allowDelete, bool showSum = true, IWin32Window owner = null)
        {
            var f = new DataEditorForm(data, tableName, showFields, keys, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language),
                allowAdd, allowDelete, showSum);
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
        /// <returns>DataEditorForm</returns>
        public static DataEditorForm MakeDataEditorForm(DataTable data, string tableName, string showFields, string keys,
            bool allowAdd, bool allowDelete, bool showSum = true)
        {
            var f = new DataEditorForm(data, tableName, showFields, keys, V6Text.Edit + " " + V6TableHelper.V6TableCaption(tableName, V6Setting.Language), allowAdd, allowDelete, showSum);
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

        /// <summary>
        /// Áp dụng bấm chuột giữa để hiển thị thông tin, chuột phải cho chức năng ngôn ngữ.
        /// </summary>
        /// <param name="control"></param>
        public static void ApplyControlTripleClick(Control control)
        {
            if (V6Setting.V6Special.Contains("Triple"))
            try
            {
                var tagString = string.Format(";{0};", control.Tag ?? "");
                var cancellang = control is DataGridView || tagString.Contains(";canceltriple;");
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

        static void c_MouseDClick(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            if (e.Button == MouseButtons.Middle)
            {
                var message = string.Format("Name({0}), AccessibleName({1}), AccessibleDescription({2}),",
                    control.Name,
                    control.AccessibleName,
                    control.AccessibleDescription);
                SetStatusText(message);
                Clipboard.SetText(message);
            }
            else if (e.Button == MouseButtons.Right && !string.IsNullOrEmpty(control.AccessibleDescription)
                && !control.AccessibleDescription.Contains(',') && !control.AccessibleDescription.Contains(';'))
            {
                if (string.IsNullOrEmpty(control.AccessibleDescription))
                {
                    var vf = (V6FormControl) FindParent<V6FormControl>(control);
                    if (vf != null) vf.ShowTopMessage("No AccessibleDescription.");
                }
                else
                {
                    if (sender is V6Label) return;
                    new FormChangeControlLanguageText(control).ShowDialog(control);
                }
            }
        }

        #region ==== APPLY LOOKUP ====

        private static TextBox txt;
        private static AldmConfig LookupInfo = null;
        private static AutoCompleteStringCollection auto1;
        private static string InitFilter = "";
        private static string LookupInfo_F_NAME;
        private static bool F2 = false;
        private static bool FilterStart = false;
        private static bool Looking = false;
        
        public static void ApplyLookup(TextBox textBox, string tablename, string fieldvalue)
        {
            if (textBox == null) return;
            txt = textBox;
            LookupInfo = V6ControlsHelper.GetAldmConfigByTableName(tablename);
            InitFilter = V6Login.GetInitFilter(LookupInfo.TABLE_NAME);
            
            if (!string.IsNullOrEmpty(fieldvalue)) LookupInfo_F_NAME = fieldvalue;
            else LookupInfo_F_NAME = LookupInfo == null ? null : LookupInfo.F_NAME;

            //txt.GotFocus += ApplyLookup_GotFocus;
            txt.KeyDown += ApplyLookup_KeyDown;
            txt.LostFocus += ApplyLookup_LostFocus;
            txt.Disposed += ApplyLookup_Disposed;
        }

        public static void ApplyNumberTextBox(Control control)
        {
            //var textBox = control as TextBox;
            //if (textBox == null) return;
            control.KeyPress += (sender, e) =>
            {
                //if (textBox.ReadOnly) return;

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
            };
        }

        private static void RemoveLookupResource()
        {
            LookupInfo = null;
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
                    if (txt.Text.Trim() != "")
                    {
                        if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                        {
                            if (ExistRowInTable(txt.Text.Trim()))
                            {
                                if (e.KeyCode == Keys.Enter)
                                {
                                    e.SuppressKeyPress = true;
                                    SendKeys.Send("{TAB}");
                                }
                            }
                            else
                            {
                                DoLookup();
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                    {
                        DoLookup();
                    }
                }
            }
            else if (e.KeyCode == Keys.F5 && !string.IsNullOrEmpty(LookupInfo_F_NAME))
            {
                //LoadAutoCompleteSource();
                DoLookup();
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (F2)
                {
                    DoLookup(true);
                }
            }
        }

        private static void ApplyLookup_LostFocus(object sender, EventArgs e)
        {
            if (LookupInfo.NoInfo) return;

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
                    if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                    {
                        if (ExistRowInTable(textBox.Text.Trim()))
                        {
                            //if (!Looking && gotfocustext != Text) CallDoV6LostFocus();
                            //else CallDoV6LostFocusNoChange();
                        }
                        else
                        {
                            DoLookup(false);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                {
                    DoLookup(false);
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

        private static void DoLookup(bool multi = false)
        {
            if (LookupInfo.NoInfo) return;
            //_frm = FindForm();
            var filter = InitFilter;
            if (!string.IsNullOrEmpty(InitFilter)) filter = "and " + filter;
            var parentData = new SortedDictionary<string, object>();
            var fStand = new V6LookupTextboxForm(parentData, txt.Text, LookupInfo, " 1=1 " + filter, LookupInfo_F_NAME, multi, FilterStart);
            Looking = true;
            fStand.ShowDialog();
        }

        private static void Lookup(bool multi = false)
        {
            DoLookup(multi);
        }

        private static string _text_data = "";
        private static DataRow _data;
        private static bool ExistRowInTable(string text)
        {
            try
            {
                _text_data = text;
                if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                {
                    string tableName = LookupInfo.TABLE_NAME;
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
            if (LookupInfo.NoInfo) return;

            if (!string.IsNullOrEmpty(LookupInfo.TABLE_NAME) && !string.IsNullOrEmpty(LookupInfo.F_NAME) && auto1 == null)
            {
                try
                {
                    auto1 = new AutoCompleteStringCollection();

                    var selectTop = "";

                    if (!string.IsNullOrEmpty(LookupInfo.F_NAME))
                    {
                        var tableName = LookupInfo.TABLE_NAME;
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
                    WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".LoadAutoCompleteSource " + LookupInfo.TABLE_NAME, ex);
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
        /// <returns></returns>
        public static string ChooseExcelFile()
        {
            if (V6Setting.IsDesignTime) return "";
            if (excelOpenFileDialog.ShowDialog() == DialogResult.OK)
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
        /// <param name="filter">Lọc file, vd: All file|*.*</param>
        /// <returns></returns>
        public static string ChooseSaveFile(string filter)
        {
            if (V6Setting.IsDesignTime) return "";
            if (string.IsNullOrEmpty(filter)) filter = "All file|*.*";
            else if (!filter.Contains("All file|*.*".ToUpper())) filter += "|All file|*.*";
            saveFileDialog.Filter = filter;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
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
        /// <param name="filter">Lọc file, vd: All file|*.*</param>
        /// <returns></returns>
        public static string ChooseOpenFile(string filter)
        {
            if (V6Setting.IsDesignTime) return "";
            if (string.IsNullOrEmpty(filter)) filter = "All file|*.*";
            else if (!filter.Contains("All file|*.*".ToUpper())) filter += "|All file|*.*";
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return "";
        }

        /// <summary>
        /// Chọn một hình trong ổ đĩa. Nếu không chọn trả về null
        /// </summary>
        /// <returns></returns>
        public static Image ChooseImage()
        {
            using (var box = new OpenFileDialog())
            {
                box.Filter = "JPG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                if (box.ShowDialog() == DialogResult.OK)
                {
                    return LoadCopyImage(box.FileName);
                }
            }
            return null;
        }

        
    }
}
