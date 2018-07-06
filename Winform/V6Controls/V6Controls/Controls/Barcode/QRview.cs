using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using V6Tools.V6Convert;

namespace V6Controls.Controls.Barcode
{
    public partial class QRview : UserControl
    {
        /// <summary>
        /// Giá trị của mã QR
        /// </summary>
        [DefaultValue("V6SOFT")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string QRvalue { get { return _value ?? ""; } set { _value = value; Invalidate();} }
        protected string _value = "V6SOFT";

        [DefaultValue(4)]
        public int PixelSize { get { return _pixel_size; } set { _pixel_size = value; Invalidate(); } }
        private int _pixel_size = 4;

        private QrCode _qrCode;
        private const int padding = 16;

        /// <summary>
        /// Đối tượng hình ảnh của QRcode được tạo để hiển thị.
        /// </summary>
        public Image Image
        {
            get
            {
                QrEncoder encode = new QrEncoder();
                encode.TryEncode(QRvalue, out _qrCode);
                GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(_pixel_size, QuietZoneModules.Two),
                    Brushes.Black, Brushes.White);
                int image_width = 1;
                int image_height = 1;
                if (_qrCode != null && _qrCode.Matrix != null)
                {
                    image_width = _qrCode.Matrix.Width * _pixel_size + padding;
                    image_height = _qrCode.Matrix.Height * _pixel_size + padding;
                }
                Image image = new Bitmap(image_width, image_height);
                Graphics graphics = Graphics.FromImage(image);
                if (_qrCode != null) renderer.Draw(graphics, _qrCode.Matrix);
                return image;
            }
        }

        /// <summary>
        /// Dữ liệu dạng mảng byte để đưa vào data.
        /// </summary>
        public byte[] ByteArrayValue { get { return Picture.ToBmpByteArray(Image); } }
        
        /// <summary>
        /// Đối tượng kết nối Text để hiển thị.
        /// </summary>
        [DefaultValue(null)]
        public Control LinkControl { get { return _linkControl; }
            set
            {
                //unlink
                if (_linkControl != null)
                {
                    _linkControl.TextChanged -= _linkControl_TextChanged;
                }

                //link
                if (value != null)
                {
                    _linkControl = value;
                    _linkControl.TextChanged += _linkControl_TextChanged;
                }
            }
        }

        void _linkControl_TextChanged(object sender, EventArgs e)
        {
            QRvalue = ((Control)sender).Text;
        }
        private Control _linkControl;

        public QRview()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(Image, new Point(0, 0));
        }

        private void QRview_Resize(object sender, EventArgs e)
        {
            if (AutoSize && _qrCode != null)
            {
                FixSize();
            }
        }

        private void FixSize()
        {
            try
            {
                Width = _qrCode.Matrix.Width * _pixel_size + padding;
                Height = _qrCode.Matrix.Height * _pixel_size + padding;
            }
            catch (Exception)
            {
                //
            }
        }

        private void QRview_AutoSizeChanged(object sender, EventArgs e)
        {
            if(AutoSize) FixSize();
        }
    }
}
