﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace V6ThuePost
{
    public partial class V6Form : Form
    {
        
        /// <summary>
        /// Image index
        /// </summary>
        protected int ii = 0;
        protected bool _ready;
        public string ItemID { get { return m_itemId; } }
        protected string m_itemId = null;
        protected string _message = "";
        public bool Data_Loading, _load_data_success;
        public string _sttRec { get; set; }
        /// <summary>
        /// Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo
        /// </summary>
        [DefaultValue(null)]
        [Description("Phân biệt loại initfilter. 1 cập nhập số liệu, 2 danh mục, 3 số dư, 4 báo cáo")]
        public string FilterType { get; set; }

        [DefaultValue(null)]
        protected Image btnNhanImage
        {
            get { return _btnNhanImage; }
            set
            {
                if (_btnNhanImage == null) _btnNhanImage = value;
            }
        }
        private Image _btnNhanImage;

        protected ImageList waitingImages { get { return _waitingImages; } }
        public V6Form()
        {
            InitializeComponent();
            lblTopMessage.Top -= lblTopMessage.Height;
            MessageLable = lblTopMessage;
        }

        public bool IsReady { get { return _ready; } }

        public void Ready()
        {
            _ready = true;
        }

        private void V6Form_Load(object sender, EventArgs e)
        {
            //LoadLanguage();
        }

        //protected virtual void LoadLanguage()
        //{
        //    V6ControlFormHelper.SetFormText(this);
        //}

        protected bool do_hot_key;
        public virtual void DoHotKey (Keys keyData)
        {
            try
            {
                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                //Nếu đã thực hiện lệnh bên trên gửi xuống thì không chạy DoHotKey0
                if (do_hot_key)
                {
                    do_hot_key = false;
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if(DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public virtual bool DoHotKey0(Keys keyData)
        {
            return DoKeyCommand(this, keyData);
        }

        public static bool DoKeyCommand(Control container, Keys keyData)
        {
            try
            {
                string keyString = keyData.ToString();
                //SetStatusText(keyString);//Test !!!!!!!!!!!
                return ClickByTag(container, keyString);
            }
            catch
            {
                return false;
            }
        }

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
                    //var label = control as V6Label;
                    //if (label != null) label.PerformClick();
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

        //public void SetControlValue(Control control, object value)
        //{
        //    V6ControlFormHelper.SetControlValue(control, value);
        //}

        /// <summary>
        /// Lấy dữ liệu trên form.
        /// </summary>
        /// <returns></returns>
        //public virtual SortedDictionary<string, object> GetData()
        //{
        //    return V6ControlFormHelper.GetFormDataDictionary(this);
        //}

        #region ==== SHOW HIDE MESSAGE ====

        //public void ShowMainMessage(string message)
        //{
        //    V6ControlFormHelper.ShowMainMessage(message);
        //}

        public static Label MessageLable;
        private static Timer _topMessageTimer;
        private static int _topTime = -1;
        /// <summary>
        /// Hiển thị một thông báo trượt xuống từ góc trên bên trái của form.
        /// </summary>
        /// <param name="message"></param>
        public void ShowTopMessage(string message)
        {
            if (_topMessageTimer != null && _topMessageTimer.Enabled)
            {
                _topMessageTimer.Stop();
                //MessageLable.Top 
            }
            MessageLable.BringToFront();
            MessageLable.Text = message;
            
            _topMessageTimer = new Timer { Interval = 200 };
            _topMessageTimer.Tick += topMessageTimer_Tick;
            _topTime = -1;
            _topMessageTimer.Start();
            MessageLable.Visible = true;
        }

        static void topMessageTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _topTime++;
                
                if (_topTime < 10)//Hiện ra
                {
                    MessageLable.Top -= MessageLable.Top / 3;

                    if (MessageLable.Top == -1) MessageLable.Top = 0;
                    if (MessageLable.Top == -2) MessageLable.Top = -1;
                    return;
                }
                if (_topTime < 20)//Dừng lại
                {
                    return;
                }
                if (_topTime < 30)//Ẩn đi
                {
                    MessageLable.Top -= MessageLable.Bottom / 3;
                    if (MessageLable.Bottom == 1) MessageLable.Top = -MessageLable.Height;
                    if (MessageLable.Bottom == 2) MessageLable.Top = -MessageLable.Height + 1;
                    return;
                }
                _topMessageTimer.Stop();
            }
            catch// (Exception)
            {
                // ignored
            }
        }

        #endregion show hide message
    }
}
