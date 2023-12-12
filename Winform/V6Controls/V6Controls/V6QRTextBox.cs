using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using System.ComponentModel;
using System.Data.SqlClient;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    /// <summary>
    /// Lookup textBox
    /// </summary>
    public class V6QRTextBox : V6ColorTextBox
    {
        //constructor
        public V6QRTextBox()
        {
            InitializeComponent();
            //_upper = true;
            //AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // V6QRTextBox
            // 
            this.TextChanged += new System.EventHandler(this.V6QRTextBox_TextChanged);
            //this.Enter += new System.EventHandler(this.V6QRTextBox_Enter);
            //this. GotFocus += new System.EventHandler(this.V6QRTextBox_GotFocus);
            this.ResumeLayout(false);
        }

        protected override void V6ColorTextBox_Enter(object sender, EventArgs e)
        {
            base.V6ColorTextBox_Enter(sender, e);
            try
            {
                SelectAll();
            }
            catch (Exception)
            {
                
            }
        }
        
        /// <summary>
        /// Data_ID
        /// </summary>
        private string _text_data = "";
        public Dictionary<string, string> _data;
        //private Form _frm;
        
        public new Dictionary<string, string> Data
        {
            get
            {
                if (_data != null && _text_data == Text)
                {
                    return _data;
                }
                else
                {
                    GetData();
                }
                return _data;
            }
        }

        private void GetData()
        {
            try
            {
                _text_data = Text;
                if (string.IsNullOrEmpty(M_QRCODE_INFOS.SPLIT))
                {
                    _data = null;
                }
                else
                {
                    var ar = ObjectAndString.SplitStringBy(_text_data, M_QRCODE_INFOS.SPLIT[0], false);
                    _data = new Dictionary<string, string>();
                    foreach (var item in M_QRCODE_INFOS.FIELD_INDEX)
                    {
                        _data[item.Key] = ar[item.Value];
                    }
                }
            }
            catch
            {

            }
        }

        M_QRCODE_INFOS _M_QRCODE_INFOS = null;
        public M_QRCODE_INFOS M_QRCODE_INFOS
        {
            get
            {
                if (_M_QRCODE_INFOS == null || string.IsNullOrEmpty(_M_QRCODE_INFOS.SPLIT))
                    _M_QRCODE_INFOS = new M_QRCODE_INFOS(V6Options.GetValueNull("M_QRCODE_INFOS"));
                return _M_QRCODE_INFOS;
            }
        }


        private bool _qr_lostauto = true;
        /// <summary>
        /// Tự gọi v6lostfocus khi gắn data cho các control.
        /// </summary>
        [DefaultValue(true)]
        public bool QR_LostAuto
        {
            get { return _qr_lostauto; }
            set { _qr_lostauto = value; }
        }


        /// <summary>
        /// Tên các trường dữ liệu liên quan
        /// </summary>
        [Category("V6")]
        [DefaultValue("MA_KHO:MA_KHO_I;MA_VT:MA_SP...")]
        [Description("MA_KHO:MA_KHO_I;MA_VT:MA_SP... Định nghĩa gán dữ liệu cho các ô xung quanh khi quét xong (Enter) và is QR (~).")]
        public string BrotherFields { get; set; }
        /// <summary>
        /// Tên các trường dữ liệu liên quan khi dùng ngôn ngữ khác V
        /// </summary>
        [Category("V6")]
        [DefaultValue(null)]
        [Description("Các trường dữ liệu liên quan trường hợp ngôn ngữ khác V")]
        public string BrotherFields2 { get; set; }
        /// <summary>
        /// Ánh xạ với BrotherFields với tên trường khác.
        /// </summary>
        [Category("V6")]
        [DefaultValue(null)]
        [Description("Ánh xạ với BrotherFields để gán dữ liệu cho các control khác")]
        public string NeighborFields { get; set; }

        /// <summary>
        /// Gán lại brothers với _data sẵn có.
        /// </summary>
        public void SetBrotherFormData()
        {
            if (BrotherFields != null)
            {
                if (Data != null)
                {
                    V6ControlFormHelper.SetBrotherData(this, Data, BrotherFields, BrotherFields2);
                    SetNeighborValues();
                }
                else
                {
                    
                }
            }
        }

        private void SetNeighborValues()
        {
            try
            {
                if (string.IsNullOrEmpty(BrotherFields) || string.IsNullOrEmpty(NeighborFields))
                    return;

                var bList = BrotherFields.ToUpper().Split(',');
                var nList = NeighborFields.Split(',');
                var max = bList.Length > nList.Length ? bList.Length : nList.Length;
                IDictionary<string, string> neighbor_field = new Dictionary<string, string>();
                for (int i = 0; i < max; i++)
                {
                    neighbor_field.Add(nList[i].ToUpper(), bList[i].ToUpper());
                }
                if (_qr_lostauto) V6ControlFormHelper.SetNeighborData_V6Lost(this, _data, neighbor_field);
                else V6ControlFormHelper.SetNeighborData(this, _data, neighbor_field);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Hàm sử dụng hạn chế để sửa lỗi. (Đổi giá trị ghi nhận khi vào control).
        /// </summary>
        /// <param name="text"></param>
        public void SetGotFocusText(string text)
        {
            if (text == null) text = "";
            gotfocustext = text;
        }

        public int Int_Data(string field)
        {
            if (Data != null)
            {
                return Convert.ToInt32(_data[field]);
            }
            return 0;
        }
        

        /// <summary>
        /// Cờ đã xử lý khi bấm enter.
        /// </summary>
        private int _checkOnLeave_OnEnter = 0;
        private bool _checkOnLeave = true;
        [Description("Bật tắt kiểm tra tính hợp lệ của dữ liệu khi rời khỏi.")]
        [DefaultValue(true)]
        public bool CheckOnLeave
        {
            get { return _checkOnLeave; }
            set { _checkOnLeave = value; }
        }
        private bool _checkNotEmpty;
        [Description("Bật tắt kiểm tra phải có của dữ liệu khi rời khỏi.")]
        [DefaultValue(false)]
        public bool CheckNotEmpty
        {
            get { return _checkNotEmpty; }
            set { _checkNotEmpty = value; }
        }
        
        

        #region ==== Event ====
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_ENABLE:
                    // Prevent the message from reaching the control,
                    // so the colors don't get changed by the default procedure.
                    return; // <-- suppress WM_ENABLE message

            }
            // Trap WM_PASTE:
            // Xử lý dữ liệu clipboard trước khi dán
            if (m.Msg == Win32.WM_PASTE)
            {
                if (Clipboard.ContainsText())
                {
                    var clipboard = Clipboard.GetText();
                    try
                    {
                        clipboard = V6Tools.V6Convert.ObjectAndString.TrimSpecial(clipboard);
                        if (!string.IsNullOrEmpty(LimitCharacters0))
                        {
                            foreach (char c in _lmChars0)
                            {
                                clipboard = clipboard.Replace("" + c, "");
                            }
                        }
                        if (!string.IsNullOrEmpty(LimitCharacters))
                        {
                            for (int i = clipboard.Length - 1; i >= 0; i--)
                            {
                                char c = clipboard[i];
                                if (!_lmChars.Contains(c))
                                {
                                    clipboard = clipboard.Remove(i, 1);
                                }
                            }
                        }
                        Paste(clipboard);
                    }
                    catch (Exception ex)
                    {
                        this.WriteExLog(GetType() + ".WndProc WM_PASTE", ex);
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReadOnly || !Enabled)
            {
                if (e.KeyData == Keys.Enter) SendKeys.Send("{TAB}");
                return;
            }
            _checkOnLeave_OnEnter = 0;
            
            if (e.KeyData == Keys.Enter)
            {
                //Do check on leave
                Do_CheckOnLeave(new EventArgs());
                //Flag
                _checkOnLeave_OnEnter = 2;
                //Send Tab
                SendKeys.Send("{TAB}");
            }
            else
            {
                base.V6ColorTextBox_KeyDown(this, e);
            }
            
        }

        /// <summary>
        /// Override hoàn toàn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void V6ColorTextBox_LostFocus(object sender, EventArgs e)
        {
            DoCharacterCasing();
            lostfocustext = Text;
            if (EnableColorEffect)
            {
                BackColor = ReadOnly ? _leaveColorReadOnly : _leaveColor;
            }
            var textBox = (TextBox)sender;
            if (V6ControlsHelper.DisableLookup)
            {
                V6ControlsHelper.DisableLookup = false;
                return;
            }
            
            // Đã xử lý KeyDown Enter.
            if (_checkOnLeave_OnEnter == 1)
            {
                _checkOnLeave_OnEnter = 0;
                return;
            }
            else if (_checkOnLeave_OnEnter == 2)
            {
                _checkOnLeave_OnEnter--;
            }
            
            if (!Looking && gotfocustext != Text)
            {
                SetBrotherFormData();
                CallDoV6LostFocus();
            }
            else
            {
                CallDoV6LostFocusNoChange();
            }
            
            
        }

        private void Do_CheckOnLeave(EventArgs e)
        {
            if (_checkOnLeave && !ReadOnly && Visible && Enabled)
            {
                CheckIsV6QRcode();
            }
        }

        
        /// <summary>
        /// Bật chức năng kiểm tra barcode và sinh sự kiện.
        /// </summary>
        [DefaultValue(false)]
        public bool CheckQRCode { get; set; }
        [DefaultValue(false)]
        public bool IsV6QRcode
        {
            get { return IsThisQRcode(); }
        }

        /// <summary>
        /// Sự kiện xảy ra khi người dùng nhập xong một barcode.
        /// </summary>
        public event EventHandler InputBarcode;
        protected virtual void OnInputBarcode()
        {
            var handler = InputBarcode;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private bool IsThisQRcode()
        {
            try
            {
                return !string.IsNullOrEmpty(M_QRCODE_INFOS.SPLIT) && Text.Contains(M_QRCODE_INFOS.SPLIT);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra có phải là barcode hay không? Nếu đúng sinh sự kiện.
        /// </summary>
        private void CheckIsV6QRcode()
        {
            if (CheckQRCode && IsV6QRcode)
            {
                OnInputBarcode();
            }
        }
        

        #endregion event
        
        
        public void SetValue(object value)
        {
            try
            {
                SetBrotherFormData();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetValue", ex);
            }
        }
        
        public IDictionary<string, object> ParentData;
        

        /// <summary>
        /// Gán text bằng hàm này để xảy ra sự kiện V6LostFocus
        /// </summary>
        /// <param name="text">Giá trị mới</param>
        public override void ChangeText(string text)
        {
            var inText = Text;
            Text = text;
            SetBrotherFormData();
            if (inText != Text) CallDoV6LostFocus();
            else CallDoV6LostFocusNoChange();
        }
        
        
        private void V6QRTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (Focused && (_showName||_checkOnLeave))
            //{
            //    V6ControlsHelper.ShowVvarName(this);
            //}
        }

        

        
    }

    public class M_QRCODE_INFOS
    {
        //SPLIT:~;MA_VT:1;MA_LO:10
        public M_QRCODE_INFOS(string M_QRCODE_INFOS)
        {
            var DIC = ObjectAndString.StringToStringDictionary(M_QRCODE_INFOS);
            if (DIC.ContainsKey("SPLIT")) SPLIT = DIC["SPLIT"];
            FIELD_INDEX = new Dictionary<string, int>();
            foreach (var item in DIC)
            {
                try
                {
                    if (item.Key == "SPLIT")
                    {

                    }
                    else
                    {
                        FIELD_INDEX[item.Key.ToUpper()] = ObjectAndString.ObjectToInt(item.Value)-1;
                    }
                }
                catch
                {
                    
                }
            }
        }
        
        public string SPLIT { get; private set; }
        public Dictionary<string, int> FIELD_INDEX { get; private set; }
    }
}
