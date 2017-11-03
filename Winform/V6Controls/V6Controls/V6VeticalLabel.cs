using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace V6Controls
{
    public class V6VeticalLabel : Control
    {
        private string _hideText = "Show", _showText="Hide";
        private DrawMode _dm = DrawMode.BottomUp;
        private bool _transparentBG = false;
        private System.Drawing.Text.TextRenderingHint _renderMode = System.Drawing.Text.TextRenderingHint.SystemDefault;

        private System.ComponentModel.Container components = new System.ComponentModel.Container();

        /// <summary>
        /// VerticalLabel constructor
        /// </summary>
        public V6VeticalLabel()
        {
            base.CreateControl();
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            Resize += VerticalTextBox_Resize;
        }

        /// <summary>
        /// Dispose override method
        /// </summary>
        /// <param name="disposing">boolean parameter</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!((components == null)))
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public void PerformClick()
        {
            OnClick(new EventArgs());
        }

        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(24, 100);
        }

        /// <summary>
        /// OnPaint override. This is where the text is rendered vertically.
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            float vlblControlWidth;
            float vlblControlHeight;
            float vlblTransformX;
            float vlblTransformY;

            Color controlBackColor = BackColor;
            Pen labelBorderPen;
            SolidBrush labelBackColorBrush;

            if (_transparentBG)
            {
                labelBorderPen = new Pen(Color.Empty, 0);
                labelBackColorBrush = new SolidBrush(Color.Empty);
            }
            else
            {
                labelBorderPen = new Pen(controlBackColor, 0);
                labelBackColorBrush = new SolidBrush(controlBackColor);
            }

            SolidBrush labelForeColorBrush = new SolidBrush(base.ForeColor);
            base.OnPaint(e);
            vlblControlWidth = this.Size.Width;
            vlblControlHeight = this.Size.Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, vlblControlWidth, vlblControlHeight);
            e.Graphics.TextRenderingHint = this._renderMode;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var drawText = _isShow ? _showText : _hideText;
            int textWidth = (int) e.Graphics.MeasureString(drawText, Font).Width;
            //drawText += textWidth;
            int xStart = 3, yStart = 5;
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    break;
                case ContentAlignment.TopCenter:
                    if (textWidth < Height) xStart = (int) ((Height - textWidth)/2);
                    break;
                case ContentAlignment.TopRight:
                    if (textWidth < Height - 3) xStart = (int) (Height - textWidth - 3);
                    break;
                case ContentAlignment.MiddleLeft:
                    break;
                case ContentAlignment.MiddleCenter:
                    break;
                case ContentAlignment.MiddleRight:
                    break;
                case ContentAlignment.BottomLeft:
                    break;
                case ContentAlignment.BottomCenter:
                    break;
                case ContentAlignment.BottomRight:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (this.TextDrawMode == DrawMode.BottomUp)
            {
                vlblTransformX = 0;
                vlblTransformY = vlblControlHeight;
                e.Graphics.TranslateTransform(vlblTransformX, vlblTransformY);
                e.Graphics.RotateTransform(270);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.DrawString(drawText, Font, labelForeColorBrush, xStart, yStart);
            }
            else
            {
                vlblTransformX = vlblControlWidth;
                vlblTransformY = vlblControlHeight;
                e.Graphics.TranslateTransform(vlblControlWidth, 0);
                e.Graphics.RotateTransform(90);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                e.Graphics.DrawString(drawText, Font, labelForeColorBrush, xStart, yStart, StringFormat.GenericTypographic);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override CreateParams CreateParams //v1.10 
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // Turn on WS_EX_TRANSPARENT
                return cp;
            }
        }

        private void VerticalTextBox_Resize(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Graphics rendering mode. Supprot for antialiasing.
        /// </summary>
        [Category("Properties"), Description("Rendering mode.")]
        public System.Drawing.Text.TextRenderingHint RenderingMode
        {
            get { return _renderMode; }
            set { _renderMode = value; }
        }

        /// <summary>
        /// The text to be displayed in the control when hideMenu
        /// </summary>
        [Category("V6"), Browsable(true), Description("Text hiển thị khi hideMenu.")]
        [DefaultValue("Show")]
        public string HideText
        {
            get { return _hideText; }
            set
            {
                _hideText = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The text to be displayed in the control when showMenu
        /// </summary>
        [Category("V6"), Browsable(true), Description("Text hiển thị khi showMenu.")]
        [DefaultValue("Hide")]
        public string ShowText
        {
            get { return _showText; }
            set
            {
                _showText = value;
                Invalidate();
            }
        }

        private bool _isShow = true;

        [DefaultValue(true)]
        public bool IsShowing
        {
            get { return _isShow; }
            set
            {
                _isShow = value;
                Invalidate();
            }
        }

        [DefaultValue(ContentAlignment.TopLeft)]
        public ContentAlignment TextAlign { get { return _text_align; } set { _text_align = value; } }
        private ContentAlignment _text_align = ContentAlignment.TopLeft;

        /// <summary>
        /// 
        /// </summary>
        [Category("Properties"), Description("Whether the text will be drawn from Bottom or from Top.")]
        public DrawMode TextDrawMode
        {
            get { return _dm; }
            set { _dm = value; }
        }


        [Category("Properties"), Description("Whether the text will be drawn with transparent background or not.")]
        public bool TransparentBackground
        {
            get { return _transparentBG; }
            set { _transparentBG = value; }
        }
    }

    /// <summary>
    /// Text Drawing Mode
    /// </summary>
    public enum DrawMode
    {
        /// <summary>
        /// Text is drawn from bottom - up
        /// </summary>
        BottomUp = 1,

        /// <summary>
        /// Text is drawn from top to bottom
        /// </summary>
        TopBottom
    }
}





