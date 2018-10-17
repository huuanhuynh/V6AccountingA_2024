using System;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Một cái nút bám theo một Control khác.
    /// </summary>
    class LookupButton : System.Windows.Forms.Label
    {
        public LookupButton()
        {
            InitializeComponent();
        }

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

        /// <summary>
        /// Kết nối với một Control
        /// </summary>
        /// <param name="iControl"></param>
        private void ConnectControl(Control iControl)
        {
            if (iControl != null)
            {
                //MyInit();
                FixThisSizeLocation(iControl);
                _refControl.SizeChanged += refControl_SizeChanged;
                iControl.LocationChanged += refControl_LocationChanged;
            }
        }
        private void DeConnectControl()
        {
            if (_refControl != null)
            {
                _refControl.SizeChanged -= refControl_SizeChanged;
                _refControl.LocationChanged -= refControl_LocationChanged;
            }
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
            Left = refControl.Right;
            Top = refControl.Top;
            Width = 25;
            Height = 25;
            Text = "";
        }

        [Category("V6")]
        public string R_DataType { get; set; }
        [Category("V6")]
        public string R_Value { get; set; }
        [Category("V6")]
        public string R_Vvar { get; set; }
        [Category("V6")]
        public string R_Stt_rec { get; set; }
        [Category("V6")]
        public string R_Ma_ct { get; set; }

        [Category("V6")]
        public string M_DataType { get; set; }
        [Category("V6")]
        public string M_Value { get; set; }
        [Category("V6")]
        public string M_Vvar { get; set; }
        [Category("V6")]
        public string M_Stt_Rec { get; set; }
        [Category("V6")]
        public string M_Ma_ct { get; set; }
        /// <summary>
        /// 1 Chứng từ, 2 Danh mục, 3 Báo cáo
        /// </summary>
        [Category("V6")]
        public string M_Type { get; set; }
        [Category("V6")]
        public string M_User_id { get; set; }
        [Category("V6")]
        public string M_Lan { get; set; }


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
            // MagnetButton
            // 
            this.Image = global::V6Controls.Properties.Resources.Search24;
            this.Click += new System.EventHandler(this.LookupButton_Click);
            this.ResumeLayout(false);

        }

        private void LookupButton_Click(object sender, EventArgs e)
        {
            object value = V6ControlFormHelper.GetControlValue(_refControl);
            if (_refControl is V6VvarTextBox)
            {
                var vVarTextBox = _refControl as V6VvarTextBox;
                var vVar = vVarTextBox.VVar;
            }
            this.ShowMessage(string.Format("Value: " + ObjectAndString.ObjectToString(value)));
        }
    }
}
