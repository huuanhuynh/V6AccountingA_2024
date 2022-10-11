using System;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls.Forms.Editor;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Một cái nút bám theo một Control khác.
    /// </summary>
    public class DicEditButton : System.Windows.Forms.Label
    {
        public DicEditButton()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Category("V6")]
        [Description("Các gợi ý về giá trị có thể thêm vào. Ví dụ: PRINTVCZOOM:1")]
        public string[] KeyWordList { get; set; }

        /// <summary>
        /// Gán Control cần bám theo.
        /// </summary>
        [Category("V6")]
        [Description("Chọn một Control để bám theo.")]
        [TypeConverter(typeof(CConverter))]
        [DefaultValue(null)]
        public Control ReferenceControl
        {
            get { return _refControl; }
            set
            {
                DeConnectControl();
                _refControl = value;
                OnRefControlChanged(value);
            }
        }
        private Control _refControl;

        public event EventHandler RefControlChanged;
        protected virtual void OnRefControlChanged(Control refControl)
        {
            ConnectControl(refControl);
            var handler = RefControlChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        [Category("V6")]
        [DefaultValue(";")]
        [Description("Ký tự phân cách các phần tử.")]
        public string Separator_Item
        {
            get { return _itemSeparator;}
            set
            {
                if (value.Length == 1) _itemSeparator = value;
            }
        }
        private string _itemSeparator = ";";
        
        [Category("V6")]
        [DefaultValue(":")]
        [Description("Ký tự phân cách giữa từ KHÓA và Giá_trị.")]
        public string Separator_Value
        {
            get { return _valueSeparator; }
            set
            {
                if (value.Length == 1) _valueSeparator = value;
            }
        }
        private string _valueSeparator = ":";

        /// <summary>
        /// Kết nối với một Control
        /// </summary>
        /// <param name="iControl"></param>
        private void ConnectControl(Control iControl)
        {
            if (iControl != null)
            {
                FixThisSizeLocation(iControl);
                _refControl.SizeChanged += refControl_SizeChanged;
                _refControl.LocationChanged += refControl_LocationChanged;
                _refControl.VisibleChanged += _refControl_VisibleChanged;
                _refControl.KeyDown += _refControl_KeyDown;
            }
        }
        
        private void DeConnectControl()
        {
            if (_refControl != null)
            {
                _refControl.SizeChanged -= refControl_SizeChanged;
                _refControl.LocationChanged -= refControl_LocationChanged;
                _refControl.VisibleChanged -= _refControl_VisibleChanged;
                _refControl.KeyDown -= _refControl_KeyDown;
            }
        }

        void _refControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                DicEditButton_Click(this, new EventArgs());
            }
        }

        void _refControl_VisibleChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_refControl);
        }

        void refControl_SizeChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_refControl);
        }
        void refControl_LocationChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_refControl);
        }

        private void FixThisSizeLocation(Control refControl)
        {
            if (Parent != refControl.Parent)
                refControl.Parent.Controls.Add(this);
            Visible = refControl.Visible;
            Left = refControl.Right;
            Top = refControl.Top;
            Width = 21;
            Height = 21;
            Text = "";
        }
        
        internal class CConverter : ReferenceConverter
        {
            public CConverter()
                : base(typeof(Control))
            {
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DicEditButton
            // 
            this.Image = global::V6Controls.Properties.Resources.Edit24;
            this.Size = new System.Drawing.Size(21, 21);
            this.Click += new System.EventHandler(this.DicEditButton_Click);
            this.ResumeLayout(false);

        }

        [Description("Sự kiện xảy ra trước khi xử lý Click gốc.")]
        public event EventHandler Click0;
        protected virtual void OnClick0()
        {
            var handler = Click0;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        private void DicEditButton_Click(object sender, EventArgs e)
        {
            OnClick0();
            try
            {
                var source = ObjectAndString.StringToStringDictionary(ReferenceControl.Text, Separator_Item[0], Separator_Value[0]);
                DicEditForm form = new DicEditForm(source, this);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ReferenceControl.Text = form.GetString(Separator_Item, Separator_Value);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DicEditButton_Click", ex);
            }
        }

        
    }
}
