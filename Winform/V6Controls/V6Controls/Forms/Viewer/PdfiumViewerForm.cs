using System;
using System.Windows.Forms;
using PdfiumViewer;

namespace V6Controls.Forms.Viewer
{
    public partial class PdfiumViewerForm : V6Form
    {
        public PdfiumViewerForm()
        {
            InitializeComponent();
        }

        public PdfiumViewerForm(string file, string title)
        {
            InitializeComponent();
            Text = "Help " + title;
            pdfViewer1.Document = OpenDocument(file);
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private PdfDocument OpenDocument(string fileName)
        {
            try
            {
                return PdfDocument.Load(this, fileName);
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message, Text, 0, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            var str = keyData.ToString();
            if (keyData == Keys.OemMinus || (int)keyData == 109)
            {
                pdfViewer1.Renderer.ZoomOut();
            }
            else if (keyData == Keys.Oemplus || (int)keyData == 107)
            {
                pdfViewer1.Renderer.ZoomIn();
            }
            else if (keyData == Keys.NumPad0 || str == "D0")
            {
                pdfViewer1.ZoomMode = PdfViewerZoomMode.FitWidth;
                pdfViewer1.Renderer.Zoom = 1;
            }
            else if (keyData == Keys.PageUp)
            {
                pdfViewer1.Renderer.Page--;
            }
            else if (keyData == Keys.PageDown)
            {
                pdfViewer1.Renderer.Page++;
            }
            else if (keyData == Keys.Home)
            {
                pdfViewer1.Renderer.Page = 0;
            }
            else if (keyData == Keys.Escape)
            {
                Close();
            }
            else
            {
                return base.DoHotKey0(keyData);
            }
            return true;
        }
    }
}
