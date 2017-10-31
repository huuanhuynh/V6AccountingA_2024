using System.Windows.Forms;
using System.ComponentModel;

namespace V6Controls
{
    /// <summary>
    /// Kiểu số
    /// </summary>
    public class V6CheckTextBox : V6ColorTextBox
    {
        #region ==== Properties ====

        [Category("V6")]
        public bool Checked
        {
            get { return Text == TextView; }
            set { StringValue = value ? textValue : ""; }
        }
        
        [Category("V6")]
        public string StringValue
        {
            get
            {
                var s = Text == TextView? TextValue : "";
                return s;
            }
            protected set
            {
                if (value.ToUpper() == TextValue.ToUpper())
                    Text = TextView;
                else Text = "";
            }
        }

        public void SetStringValue(string s)
        {
            StringValue = s;
        }

        private string textValue = "x";
        [Category("V6")]
        [DefaultValue("x")]
        [Description("Value trả về cho StringValue nếu được chọn.")]
        public string TextValue
        {
            get { return textValue; }
            set { if(value!="") textValue = value; }
        }


        private string textView = "✔";
        [Category("V6")]
        [DefaultValue("✔")]
        [Description("Text hiển thị nếu được chọn.")]
        public string TextView
        {
            get { return textView; }
            set { if (value != "") textView = value; }
        }
        
        #endregion ==== Properties ====       

        public V6CheckTextBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // V6CurrencyTextBox
            // 
            TextAlign = HorizontalAlignment.Right;
            KeyDown += ChkTextBox_KeyDown;
            KeyPress += ChkTextBox_KeyPress;
            //TextChanged += V6CheckTextBox_TextChanged;
            ResumeLayout(false);
        }

        //void V6CheckTextBox_TextChanged(object sender, System.EventArgs e)
        //{
        //    ;
        //}

        public override string Query
        {
            get { return base.Query; }
        }

        //protected override void WndProc(ref Message m)
        //{
        //    // Trap WM_PASTE:
        //    if (m.Msg == 0x302 && Clipboard.ContainsText())
        //    {
        //        var clipboard = Clipboard.GetText().Replace("\n", "").Replace(" ","");
        //        decimal newValue = 0;
        //        if (decimal.TryParse(clipboard, out newValue))
        //        {
        //            Value = newValue;
        //        }
        //        return;
        //    }
        //    base.WndProc(ref m);
        //}
        
        
        /// <summary>
        /// Xử lý nút Backspace
        /// </summary>
        /// <param name="i">Vị trí con trỏ trên chuỗi số
        /// (không tính dấu cách phần ngàn và những thứ không liên quan)</param>
        private void DoBack()
        {
            try
            {
                Text = "";
            }
            catch
            { }
        }

        /// <summary>
        /// Xử lý nút Delete
        /// </summary>
        private void DoDelete()//Cần xử lý kỹ sls
        {
            try
            {
                Text = "";
            }
            catch
            { }            
        }

        private void DoChange()//Cần xử lý kỹ sls
        {
            try
            {
                if (StringValue == textValue) StringValue = "";
                else StringValue = textValue;
            }
            catch
            { }
        }
        
        //==================== Xử lý sự kiện =========================


        private void ChkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //chặn sự kiện của phím để dùng xử lý riêng
            e.Handled = true;
            if(ReadOnly) return;
            //int sls = SelectionStart;
            if (e.KeyCode == Keys.Back)
            {
                DoBack();
            }
            else if(e.KeyCode == Keys.Delete)
            {
                DoDelete();
            }
            else if (e.KeyCode == Keys.Space)
            {
                DoChange();
            }

        }

        private void ChkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ReadOnly) return;
            e.Handled = true;
        }

    }
   
}
