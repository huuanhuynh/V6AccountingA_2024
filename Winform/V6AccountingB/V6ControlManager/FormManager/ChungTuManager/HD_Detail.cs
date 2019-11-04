using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Structs;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class HD_Detail : V6Control
    {
        public delegate void ClickHandle(object sender);

        public event ClickHandle ClickAdd;
        public event ClickHandle ClickEdit;
        public event ClickHandle ClickCancelEdit;
        /// <summary>
        /// Khi viết hàm cần quăng lỗi nếu không thành công (nhận biết false thay bool)
        /// </summary>
        public event HandleData AddHandle;
        /// <summary>
        /// Khi viết hàm cần quăng lỗi nếu không thành công (nhận biết false thay bool)
        /// </summary>
        public event HandleData EditHandle;
        public event ClickHandle DeleteHandle;

        public bool Loai_ck;

        [Category("V6")]
        [DefaultValue(true)]
        public bool ShowLblName
        {
            get { return lblName.Visible; }
            set { lblName.Visible  = value; }
        }
        
        [Category("V6")]
        public bool Sua_tien
        {
            get { return _sua_tien; }
            set
            {
                _sua_tien = value;
                if (_sua_tien)
                {
                    
                }
                else
                {
                    
                }
            }
        }
        private bool _sua_tien;

        [Category("V6")]
        [DefaultValue(true)]
        [Description("Tự động sắp xếp lại vị trí các control trong panel1 sau khi thay đổi visible.")]
        public bool AutoFixControlLocation { get { return _autoLocation; } set { _autoLocation = value; } }
        private bool _autoLocation = true;

        internal V6Mode _mode = V6Mode.Init;

        public V6Mode MODE
        {
            get { return _mode; }
            set
            {
                _mode = value;
                ChangeModeHandle();
                FixControlsLocation();
            }
        }

        public bool IsViewOrLock { get { return MODE == V6Mode.View || MODE == V6Mode.Lock || MODE == V6Mode.Init; } }

        private void ChangeModeHandle()
        {
            try
            {
                switch (_mode)
                {
                    case V6Mode.Add:
                        SetFormControlsReadOnly(false);
                        SetData(null);
                        btnMoi.Image = Properties.Resources.Cancel16;
                        //btnMoi.Text = V6Text.Cancel;
                        toolTip1.SetToolTip(btnMoi, V6Text.Cancel);
                        btnMoi.Enabled = true;
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        btnNhan.Enabled = true;
                        btnChucNang.Enabled = true;
                        break;

                    case V6Mode.Edit:
                        
                        SetFormControlsReadOnly(false);

                        btnMoi.Enabled = false;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = false;
                        btnNhan.Enabled = true;
                        btnChucNang.Enabled = true;

                        toolTip1.SetToolTip(btnSua, V6Text.Cancel);
                        btnSua.Image = Properties.Resources.Cancel16;
                        
                        btnNhan.Enabled = true;

                        break;
                    case V6Mode.Lock:
                        SetFormControlsReadOnly(true);
                        //V6ControlFormHelper.SetFormData(this, new SortedDictionary<string, object>());
                        btnMoi.Image = Properties.Resources.Add16;
                        //btnMoi.Text = V6Text.New;
                        btnSua.Image = Properties.Resources.Pencil16;
                        //btnSua.Text = V6Text.Edit;
                        btnMoi.Enabled = false;
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        btnNhan.Enabled = false;
                        btnChucNang.Enabled = false;
                        break;
                    case V6Mode.View:
                        SetFormControlsReadOnly(true);
                        btnMoi.Image = Properties.Resources.Add16;
                        //btnMoi.Text = V6Text.New;
                        toolTip1.SetToolTip(btnMoi, V6Text.New);
                        btnSua.Image = Properties.Resources.Pencil16;
                        //btnSua.Text = V6Text.Edit;
                        toolTip1.SetToolTip(btnSua, V6Text.Edit);
                        btnMoi.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnNhan.Enabled = false;
                        btnChucNang.Enabled = false;
                        break;
                    case V6Mode.Init:
                        SetFormControlsReadOnly(true);
                        SetData(null);
                        btnMoi.Image = Properties.Resources.Add16;
                        //btnMoi.Text = V6Text.New;
                        btnSua.Image = Properties.Resources.Pencil16;
                        //btnSua.Text = V6Text.Edit;
                        btnMoi.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnNhan.Enabled = false;
                        btnChucNang.Enabled = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChangeMode", ex);
            }
        }

        /// <summary>
        /// Giống như bấm nút mới, có sự kiện ClickAdd sảy ra khi mode là Add chứ không phải hủy.
        /// </summary>
        public void OnMoiClick()
        {
            DoAddButtonClick();
            if (MODE == V6Mode.Add)
            {
                ClickHandle handler = ClickAdd;
                if (handler != null) handler(this);

                //Set Carry
                V6ControlFormHelper.UseCarryValues(this);
            }
        }

        /// <summary>
        /// Đổi mode khi bấm nút mới trong detail_control, không sinh ra sự kiện.
        /// </summary>
        public void DoAddButtonClick()
        {
            //if (!btnMoi.Focused) btnMoi.Focus();
            if (MODE == V6Mode.Add)
            {
                //Bấm hủy
                //Đổi mode thành init
                MODE = V6Mode.Init;
            }
            else
            {
                //Bấm mới
                MODE = V6Mode.Add;
                
                //Set Carry
                //V6ControlFormHelper.UseCarryValues(this);
            }
        }
        

        /// <summary>
        /// Ngoai form goi khi da chac chan chuyen mod sua
        /// </summary>
        public void ChangeToEditMode()
        {
            MODE = V6Mode.Edit;
        }
        public void ChangeToViewMode()
        {
            MODE = V6Mode.View;
        }

        /// <summary>
        /// Đổi mode và sinh ra sự kiện ClickEdit
        /// </summary>
        public virtual void OnSuaClick()
        {
            if (!btnSua.Focused) btnSua.Focus();
            if (MODE == V6Mode.Edit)
            {
                //Bấm hủy, gọi sự kiện, đổi mode
                ClickHandle handler = ClickCancelEdit;
                if (handler != null) handler(this);
                MODE = V6Mode.View;
            }
            else
            {
                ClickHandle handler2 = ClickEdit;
                if (handler2 != null) handler2(this);
            }
        }

        private bool addhandle_ok;
        public void OnAddHandle()
        {
            HandleData handler = AddHandle;
            if (handler != null)
            {
                try
                {
                    //MODE = V6Mode.View;
                    SetFormControlsReadOnly(true);
                    handler(GetData());
                    addhandle_ok = true;
                }
                catch (Exception ex)
                {
                    addhandle_ok = false;
                    this.WriteExLog(GetType() + ".OnNhanClick", ex);
                }

            }
        }

        public V6Mode Old_mode = V6Mode.Init;
        /// <summary>
        /// Gọi sự kiện, giả lập bấm nút Nhận.
        /// </summary>
        public virtual void OnNhanClick()
        {
            if(!btnNhan.Focused) btnNhan.Focus();
            //Carry values
            V6ControlFormHelper.SetCarryValues(this);

            bool ok = false;
            Old_mode = MODE;
            if (MODE == V6Mode.Add)
            {
                OnAddHandle();
                ok = addhandle_ok;
            }
            else if (MODE == V6Mode.Edit)
            {
                HandleData handler = EditHandle;
                if (handler != null)
                {
                    try
                    {
                        MODE = V6Mode.View;
                        handler(GetData());
                        ok = true;
                    }
                    catch (Exception ex)
                    {
                        ok = false;
                        this.WriteExLog(GetType() + ".OnNhanClick", ex);
                    }
                }
            }

            if (ok)
            {
                //Carry values
                //V6ControlFormHelper.SetCarryValues(this);
                MODE = V6Mode.View;
                btnMoi.Focus();
            }
            else
            {
                //MODE = Old_mode;
                _mode = Old_mode;
                SetFormControlsReadOnly(false);
                //SetData(null);
                btnMoi.Image = Properties.Resources.Cancel16;
                //btnMoi.Text = V6Text.Cancel;
                toolTip1.SetToolTip(btnMoi, V6Text.Cancel);
                btnMoi.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnNhan.Enabled = true;
                btnChucNang.Enabled = true;
            }
        }

        /// <summary>
        /// <para>Lấy data của HD_Detail control.</para>
        /// <para>Hàm này được viết lại để bỏ qua tag "cancel" của control.</para>
        /// </summary>
        /// <returns></returns>
        public override SortedDictionary<string, object> GetData()
        {
            var t = Tag;
            Tag = null;
            var d = V6ControlFormHelper.GetFormDataDictionary( this );
            Tag = t;
            return d;
        }

        public void SetData(IDictionary<string, object> d)
        {
            try
            {
                var t = Tag;
                Tag = null;
                V6ControlFormHelper.SetFormDataDictionary(this, d);
                Tag = t;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetData", ex);
            }
        }

        public virtual void OnXoaClick()
        {
            if (!btnXoa.Focused) btnXoa.Focus();
            ClickHandle handler = DeleteHandle;
            if (handler != null) handler(this);
        }

        public HD_Detail()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            _p = new Point(0, 0);
            _p0 = new Point(0, 0);
            if (!V6Setting.IsVietnamese) lblName.AccessibleName = "TEN_VT2";
        }

        public string Vtype { get; set; }

        private Point _p, _p0;
        private int _fixControl = 2;

        [DefaultValue(2)]
        [Description("Các control đứng trước, đứng im, không mất đi khi kéo thanh trượt.")]
        public int FixControl
        {
            get { return _fixControl; }
            set { _fixControl = value; }
        }

        public List<Control> _panel1Controls = new List<Control>(); 
        public void AddControl(Control c)
        {
            if (panel0.Controls.Count <= FixControl)
            {
                AddFixControl(c);
            }
            else
            {
                _panel1Controls.Add(c);
                c.Location = _p;
                panelControls.Controls.Add(c);
                if (c.AccessibleName.ToUpper() == "TEN_VT")
                {
                    lblName.AccessibleName = null;
                }

                if (c.Tag == null || c.IsVisibleTag())
                {
                    c.TabStop = true;
                    if (c is V6ColorTextBox)
                    {
                        var vc = c as V6ColorTextBox;
                        Label l = new Label();
                        l.Name = "lbl" + c.AccessibleName;
                        l.Left = vc.Left;
                        l.Top = vc.Bottom -2;
                        l.Width = vc.Width;
                        l.Height = 13;
                        l.Font = new Font(Font.FontFamily, 7f);
                        l.ForeColor = Color.DarkGray;
                        l.Text = vc.GrayText;
                        vc.GrayTextChanged += (sender, e) =>
                        {
                            l.Text = ((V6ColorTextBox)sender).GrayText;
                        };
                        panelControls.Controls.Add(l);
                        vc.VisibleChanged += (sender, e) =>
                        {
                            l.Visible = ((Control)sender).Visible;
                            FixControlsLocation();
                        };
                        vc.LocationChanged += delegate
                        {
                            l.Left = vc.Left;
                        };
                        vc.Enter += (sender, e) =>
                        {
                            FixScrollBar((Control)sender);
                        };
                        vc.KeyDown += (sender, e) =>
                        {
                            if (vc is V6CheckTextBox)
                            {
                                e.Handled = true;
                                return;
                            }
                            if (e.KeyCode == Keys.F12)
                            {
                                var info = string.Format("AccName({0}), TabStop({1}), {2}",
                                    vc.AccessibleName, vc.ClientRectangle, vc.ToString());
                                V6ControlFormHelper.SetStatusText(info);
                            }
                        };
                    }
                    else if (c is V6DateTimePicker)
                    {
                        var vc = c as V6DateTimePicker;
                        Label l = new Label();
                        l.Name = "lbl" + c.AccessibleName;
                        l.Left = vc.Left;
                        l.Top = vc.Bottom -2;
                        l.Width = vc.Width;
                        l.Height = 13;
                        l.Font = new Font(Font.FontFamily, 7f);
                        l.ForeColor = Color.DarkGray;
                        l.Text = vc.TextTitle;
                        panelControls.Controls.Add(l);

                        vc.VisibleChanged += (sender, e)=>
                        {
                            l.Visible = ((Control)sender).Visible;
                            FixControlsLocation();
                        };
                        vc.LocationChanged += delegate
                        {
                            l.Left = vc.Left;
                        };
                        vc.Enter += (sender, e) =>
                        {
                            FixScrollBar((Control)sender);
                        };
                    }

                    _p = new Point(c.Right, 0);
                }
                else
                {
                    c.TabStop = false;
                }
            }
        }

        public void AddControl(AlctControls alctControls)
        {
            var c = alctControls.DetailControl;
            var lb = alctControls.LookupButton;

            if (panel0.Controls.Count <= FixControl)
            {
                AddFixControl(alctControls);
            }
            else
            {
                _panel1Controls.Add(c);
                panelControls.Controls.Add(c);
                if (lb != null)
                {
                    _panel1Controls.Add(lb);
                    panelControls.Controls.Add(lb);
                }
                c.Location = _p;
                

                if (c.AccessibleName.ToUpper() == "TEN_VT")
                {
                    lblName.AccessibleName = null;
                }

                if (c.Tag == null || c.IsVisibleTag())
                {
                    //c.TabStop = true;
                    if (c is V6ColorTextBox)
                    {
                        var vc = c as V6ColorTextBox;
                        Label l = new Label();
                        l.Name = "lbl" + c.AccessibleName;
                        l.Left = vc.Left;
                        l.Top = vc.Bottom - 2;
                        l.Width = vc.Width;
                        l.Height = 13;
                        l.Font = new Font(Font.FontFamily, 7f);
                        l.ForeColor = Color.DarkGray;
                        l.Text = vc.GrayText;
                        vc.GrayTextChanged += (sender, e) =>
                        {
                            l.Text = ((V6ColorTextBox)sender).GrayText;
                        };
                        panelControls.Controls.Add(l);
                        vc.VisibleChanged += (sender, e) =>
                        {
                            l.Visible = ((Control)sender).Visible;
                            FixControlsLocation();
                        };
                        vc.LocationChanged += delegate
                        {
                            l.Left = vc.Left;
                        };
                        vc.Enter += (sender, e) =>
                        {
                            FixScrollBar((Control)sender);
                        };
                        vc.KeyDown += (sender, e) =>
                        {
                            if (vc is V6CheckTextBox)
                            {
                                e.Handled = true;
                                return;
                            }
                            if (e.KeyCode == Keys.F12)
                            {
                                var info = string.Format("AccName({0}), TabStop({1}), {2}",
                                    vc.AccessibleName, vc.ClientRectangle, vc.ToString());
                                V6ControlFormHelper.SetStatusText(info);
                            }
                        };
                    }
                    else if (c is V6DateTimePicker)
                    {
                        var vc = c as V6DateTimePicker;
                        Label l = new Label();
                        l.Name = "lbl" + c.AccessibleName;
                        l.Left = vc.Left;
                        l.Top = vc.Bottom - 2;
                        l.Width = vc.Width;
                        l.Height = 13;
                        l.Font = new Font(Font.FontFamily, 7f);
                        l.ForeColor = Color.DarkGray;
                        l.Text = vc.TextTitle;
                        panelControls.Controls.Add(l);

                        vc.VisibleChanged += (sender, e) =>
                        {
                            l.Visible = ((Control)sender).Visible;
                            FixControlsLocation();
                        };
                        vc.LocationChanged += delegate
                        {
                            l.Left = vc.Left;
                        };
                        vc.Enter += (sender, e) =>
                        {
                            FixScrollBar((Control)sender);
                        };
                    }

                    if(lb == null) _p = new Point(c.Right, 0);
                    else _p = new Point(lb.Right, 0);
                }
                else
                {
                    c.TabStop = false;
                }
            }
        }

        public void Update_p(Point p)
        {
            _p = p;
        }

        private void FixPanel1Location()
        {
            panelControls.Left = 0 - hScrollBar1.Value;
        }

        private void FixScrollBar(Control sender)
        {
            var num = sender.Right - hScrollBar1.Value - panel2.Width;
            if (num > 0)
            {
                if (hScrollBar1.Value + num <= hScrollBar1.Maximum)
                    hScrollBar1.Value += num;
                else
                    hScrollBar1.Value = hScrollBar1.Maximum;
            }

            var num2 = hScrollBar1.Value - sender.Left;
            if (num2 > 0)
            {
                if (num2 < hScrollBar1.Value) hScrollBar1.Value -= num2;
                else hScrollBar1.Value = 0;
            }

            FixPanel1Location();
        }

        /// <summary>
        /// Sắp xếp lại vị trí các control trong panel1 sau khi thay đổi visible.
        /// </summary>
        public void FixControlsLocation()
        {
            if (!_autoLocation) return;

            _p = new Point(0, 0);
            panelControls.HorizontalScroll.Value = 10;
            foreach (Control c in _panel1Controls)
            {
                if (!c.Visible) continue;
                c.Left = _p.X;
                _p = new Point(c.Right, 0);
            }

            //panel1.HorizontalScroll.Value = 5;
            panelControls.Width = _p.X;
        }

        public void AddFixControl(Control c)
        {
            c.Location = _p0;
            panel0.Controls.Add(c);

            if (c.Tag == null || c.IsVisibleTag())//.Tag.ToString() != "hide")
            {
                c.TabStop = true;
                if (c is V6ColorTextBox)
                {
                    var vc = c as V6ColorTextBox;
                    Label l = new Label();
                    l.Name = "lbl" + c.AccessibleName;
                    l.Left = vc.Left;
                    l.Top = vc.Bottom -2;
                    l.Width = vc.Width;
                    l.Height = 13;
                    l.Font = new Font(Font.FontFamily, 7f);
                    l.ForeColor = Color.DarkGray;
                    l.Text = vc.GrayText;
                    vc.GrayTextChanged += (sender, e) =>
                    {
                        l.Text = ((V6ColorTextBox)sender).GrayText;
                    };
                    panel0.Controls.Add(l);
                    vc.VisibleChanged += delegate
                    {
                        l.Visible = vc.Visible;
                    };
                }
                else if (c is V6DateTimePicker)
                {
                    var vc = c as V6DateTimePicker;
                    Label l = new Label();
                    l.Name = "lbl" + c.AccessibleName;
                    l.Left = vc.Left;
                    l.Top = vc.Bottom -2;
                    l.Width = vc.Width;
                    l.Height = 13;
                    l.Font = new Font(Font.FontFamily, 7f);
                    l.ForeColor = Color.DarkGray;
                    l.Text = vc.TextTitle;
                    panel0.Controls.Add(l);

                    vc.VisibleChanged += delegate
                    {
                        l.Visible = vc.Visible;
                    };
                }

                _p0 = new Point(c.Right, 0);
            }
            else
            {
                c.TabStop = false;
            }
        }

        public void AddFixControl(AlctControls alctControls)
        {
            var c = alctControls.DetailControl;
            var lb = alctControls.LookupButton;

            panel0.Controls.Add(c);
            if (lb != null)
            {
                _panel1Controls.Add(lb);
                panelControls.Controls.Add(lb);
            }
            c.Location = _p0;

            if (c.Tag == null || c.IsVisibleTag())//.Tag.ToString() != "hide")
            {
                c.TabStop = true;
                if (c is V6ColorTextBox)
                {
                    var vc = c as V6ColorTextBox;
                    Label l = new Label();
                    l.Name = "lbl" + c.AccessibleName;
                    l.Left = vc.Left;
                    l.Top = vc.Bottom -2;
                    l.Width = vc.Width;
                    l.Height = 13;
                    l.Font = new Font(Font.FontFamily, 7f);
                    l.ForeColor = Color.DarkGray;
                    l.Text = vc.GrayText;
                    vc.GrayTextChanged += (sender, e) =>
                    {
                        l.Text = ((V6ColorTextBox)sender).GrayText;
                    };
                    panel0.Controls.Add(l);
                    vc.VisibleChanged += delegate
                    {
                        l.Visible = vc.Visible;
                    };
                }
                else if (c is V6DateTimePicker)
                {
                    var vc = c as V6DateTimePicker;
                    Label l = new Label();
                    l.Name = "lbl" + c.AccessibleName;
                    l.Left = vc.Left;
                    l.Top = vc.Bottom -2;
                    l.Width = vc.Width;
                    l.Height = 13;
                    l.Font = new Font(Font.FontFamily, 7f);
                    l.ForeColor = Color.DarkGray;
                    l.Text = vc.TextTitle;
                    panel0.Controls.Add(l);

                    vc.VisibleChanged += delegate
                    {
                        l.Visible = vc.Visible;
                    };
                }

                _p0 = new Point(c.Right, 0);
            }
            else
            {
                c.TabStop = false;
            }
        }

        /// <summary>
        /// Xóa bỏ tất cả các control
        /// </summary>
        public void RemoveControls()
        {
            try
            {
                _p = new Point(0, 0);
                _p0 = new Point(0, 0);

                for (int i = panel0.Controls.Count - 1; i >= 0; i--)
                {
                    var c = panel0.Controls[i];
                    if (c != lblName)
                    {
                        panel0.Controls.RemoveAt(i);
                    }
                }

                _panel1Controls.Clear();
                while (panelControls.Controls.Count > 0)
                {
                    panelControls.Controls.RemoveAt(0);
                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RemoveControls", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetFormControlsReadOnly(bool readOnly)//, bool buttonToo)
        {
            foreach (Control control in Controls)
            {
                V6ControlFormHelper.SetFormControlsReadOnly(control, readOnly);    
            }
        }


        public void AddContexMenu(ContextMenuStrip menu)
        {
            if (menu != null && menu.Items.Count > 0)
            {
                btnChucNang.Menu = menu;
                btnChucNang.Visible = true;
            }
        }


        public void FocusBtnThem()
        {
            btnMoi.Focus();
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            OnMoiClick();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            OnNhanClick();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            OnSuaClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            OnXoaClick();
        }

        private void btnThem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OnNhanClick();
        }

        private void panel0_SizeChanged(object sender, EventArgs e)
        {
            panel2.Left = panel0.Right;
            panel2.Width = Width - panel2.Left;
        }

        /// <summary>
        /// Hiện các control trong panel động theo AccessibleName
        /// </summary>
        /// <param name="strings"></param>
        public void ShowIDs(string[] strings)
        {
            foreach (Control control in panelControls.Controls)
            {
                if (string.IsNullOrEmpty(control.AccessibleName)) continue;
                foreach (string accessName in strings)
                {
                    if(control.AccessibleName.ToLower() == accessName.ToLower())
                        control.VisibleTag();
                }
            }
        }

        /// <summary>
        /// Ẩn các control trong panel động theo AccessibleName
        /// </summary>
        /// <param name="strings"></param>
        public void HideIDs(string[] strings)
        {
            foreach (Control control in panelControls.Controls)
            {
                if(string.IsNullOrEmpty(control.AccessibleName)) continue;
                foreach (string accessName in strings)
                {
                    if (control.AccessibleName.ToLower() == accessName.ToLower())
                        control.InvisibleTag();
                }
            }
        }
        
        /// <summary>
        /// Tự động kiểm tra Mode và focus vào đúng nơi cần.
        /// </summary>
        public void AutoFocus()
        {
            try
            {
                if (MODE != V6Mode.Add && MODE != V6Mode.Edit)
                {
                    FocusBtnThem();
                }
                else
                {
                    bool b = false;
                    foreach (Control c in panel0.Controls)
                    {
                        if (c.Focused)
                        {
                            b = true;
                            break;
                        }
                    }
                    if(!b)
                    foreach (Control c in panelControls.Controls)
                    {
                        if (c.Focused)
                        {
                            b = true;
                            break;
                        }
                    }

                    if(!b)
                    foreach (Control c in panel0.Controls)
                    {
                        if (c is TextBox)
                        {
                            c.Focus();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".SetFirstFieldFocus: " + ex.Message, "HD_Detail");
            }
        }

        public event EventHandler LabelNameTextChanged;

        private void lblName_TextChanged(object sender, EventArgs e)
        {
            OnLabelNameTextChanged();
        }

        protected virtual void OnLabelNameTextChanged()
        {
            var handler = LabelNameTextChanged;
            if (handler != null) handler(lblName, EventArgs.Empty);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            FixPanel1Location();
        }

        private void panel1and2_SizeChanged(object sender, EventArgs e)
        {
            hScrollBar1.LargeChange = panel2.Width / 2;
            var maxValue = panelControls.Width - panel2.Width;
            if (maxValue > 0)
            {
                hScrollBar1.Visible = true;
                hScrollBar1.Maximum = maxValue + hScrollBar1.LargeChange;
                if (hScrollBar1.Value > maxValue)
                {
                    hScrollBar1.Value = maxValue;
                }
                hScrollBar1.SmallChange = 100;

                FixPanel1Location();
            }
            else
            {
                hScrollBar1.Visible = false;
                hScrollBar1.Value = 0;
                panelControls.Left = 1;
            }
        }

        public void SetStruct(V6TableStruct tableStruct)
        {
            try
            {
                V6ControlFormHelper.SetFormStruct(panel0, tableStruct);
                V6ControlFormHelper.SetFormStruct(panelControls, tableStruct);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetStruct " + Name, ex);
            }
        }
    }
}
