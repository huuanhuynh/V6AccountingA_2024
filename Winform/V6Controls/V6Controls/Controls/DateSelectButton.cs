using System;
using System.ComponentModel;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    /// <summary>
    /// Nút chọn ngày tháng năm bám theo control.
    /// </summary>
    public class DateSelectButton : System.Windows.Forms.Label
    {
        public DateSelectButton()
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
                _refControl.VisibleChanged += _refControl_VisibleChanged;
            }
        }

        void _refControl_VisibleChanged(object sender, EventArgs e)
        {
            FixThisSizeLocation(_refControl);
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
            // DateSelectButton
            // 
            this.Image = global::V6Controls.Properties.Resources.Calendar3124;
            this.Size = new System.Drawing.Size(21, 21);
            this.Click += new System.EventHandler(this.DateSelectButton_Click);
            this.ResumeLayout(false);

        }

        [Description("Sự kiện xảy ra trước khi xử lý Click gốc.")]
        public event EventHandler Click0;
        protected virtual void OnClick0()
        {
            var handler = Click0;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        private void DateSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _refControl.Focus();
                OnClick0();
            }
            catch (Exception ex0)
            {
                this.ShowErrorException(GetType() + ".DateSelectButton_Click ex0", ex0);
            }
            try
            {
                DateSelectForm form = new DateSelectForm();
                form.SelectedDate =
                    ObjectAndString.ObjectToFullDateTime(V6ControlFormHelper.GetControlValue(_refControl)).Date;
                
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    if (_refControl is V6DateTimeColor)
                    {
                        ((V6DateTimeColor) _refControl).Value = form.SelectedDate;
                    }
                    else
                    {
                        _refControl.Text = ObjectAndString.ObjectToString(form.SelectedDate);
                    }

                    _refControl.Focus();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DateSelectButton_Click", ex);
            }
        }


    }
}
