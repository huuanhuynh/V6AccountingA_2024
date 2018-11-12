using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6ReportControls;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Một cái nút bám theo một Control khác.
    /// </summary>
    public class LookupButton : System.Windows.Forms.Label
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
            Width = 21;
            Height = 21;
            Text = "";
        }

        [Category("V6")]
        [DefaultValue("1")]
        public string R_DataType { get; set; }
        [Category("V6")]
        public string R_Value {
            get
            {
                object value = V6ControlFormHelper.GetControlValue(_refControl);
                return ObjectAndString.ObjectToString(value);
            }
        }

        /// <summary>
        /// Vvar của control tham chiếu.
        /// </summary>
        [Category("V6")]
        public string R_Vvar {
            get
            {   
                var txt = _refControl as V6VvarTextBox;
                if (txt != null) return txt.VVar;
                var txl = _refControl as V6LookupTextBox;
                if (txl != null) return txl.Ma_dm;
                var txp = _refControl as V6LookupProc;
                if (txp != null) return txp.Ma_dm;
                var line = _refControl as FilterLineDynamic;
                if (line != null)
                {
                    if (line._vtextBox != null) return line._vtextBox.VVar;
                    if (line._lookupTextBox != null) return line._lookupTextBox.Ma_dm;
                    if (line._lookupProc != null) return line._lookupProc.Ma_dm;
                }
                var lineVvar = _refControl as FilterLineVvarTextBox;
                if (lineVvar != null) return lineVvar.Vvar;
                var lineLookup = _refControl as FilterLineLookupTextBox;
                if (lineLookup != null) return lineLookup.MA_DM;
                var lineProc = _refControl as FilterLineLookupProc;
                if (lineProc != null) return lineProc.MA_DM;
                return null;
            }
        }
        [Category("V6")]
        public string R_Stt_rec { get; set; }
        [Category("V6")]
        public string R_Ma_ct { get; set; }

        public string R_DataType2 { get; set; }
        [Category("V6")]
        public string R_Value2 { get; set; }
        /// <summary>
        /// Vvar của control tham chiếu.
        /// </summary>
        [Category("V6")]
        public string R_Vvar2 { get; set; }
        [Category("V6")]
        public string R_Stt_rec2 { get; set; }
        [Category("V6")]
        public string R_Ma_ct2 { get; set; }

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
        public int M_User_id { get { return V6Login.UserId; } }
        [Category("V6")]
        public string M_Lan { get { return V6Setting.Language; } }


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
            // LookupButton
            // 
            this.Image = global::V6Controls.Properties.Resources.Zoom20;
            this.Size = new System.Drawing.Size(21, 21);
            this.Click += new System.EventHandler(this.LookupButton_Click);
            this.ResumeLayout(false);

        }

        [Description("Sự kiện xảy ra trước khi xử lý Click gốc.")]
        public event EventHandler Click0;
        protected virtual void OnClick0()
        {
            var handler = Click0;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        private void LookupButton_Click(object sender, EventArgs e)
        {
            OnClick0();
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@R_DataType", R_DataType),
                    new SqlParameter("@R_Value", R_Value.Trim()),
                    new SqlParameter("@R_Vvar", R_Vvar),
                    new SqlParameter("@R_Stt_rec", R_Stt_rec),
                    new SqlParameter("@R_Ma_ct", R_Ma_ct),
                    
                    new SqlParameter("@R_DataType2", R_DataType2),
                    new SqlParameter("@R_Value2", R_Value2.Trim()),
                    new SqlParameter("@R_Vvar2", R_Vvar2),
                    new SqlParameter("@R_Stt_rec2", R_Stt_rec2),
                    new SqlParameter("@R_Ma_ct2", R_Ma_ct2),

                    new SqlParameter("@M_DataType", M_DataType),
                    new SqlParameter("@M_Value", M_Value),
                    new SqlParameter("@M_Vvar", M_Vvar),
                    new SqlParameter("@M_Stt_Rec", M_Stt_Rec),
                    new SqlParameter("@M_Ma_ct", M_Ma_ct),

                    new SqlParameter("@M_Type", M_Type),
                    new SqlParameter("@M_User_id", M_User_id),
                    new SqlParameter("@M_Lan", V6Login.SelectedLanguage),
                };
                var ds = V6BusinessHelper.ExecuteProcedure("V6LOOKUPCONTROL", plist);

                LookupButtonDataViewForm f = new LookupButtonDataViewForm(ds);
                f.Text = V6Setting.IsVietnamese ? "Tham chiếu dữ liệu" : "Reference viewer";
                f.LookupButtonF3Event += f_LookupButtonF3Event;
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LookupButton_Click", ex);
            }
        }

        public event LookupButtonEventHandler LookupButtonF3Event;
        protected virtual void OnLookupButtonEvent(object sender, LookupEventArgs e)
        {
            var handler = LookupButtonF3Event;
            if (handler != null) handler(sender, e);
        }

        void f_LookupButtonF3Event(object sender, LookupEventArgs e)
        {
            OnLookupButtonEvent(this, e);
        }

    }
}
