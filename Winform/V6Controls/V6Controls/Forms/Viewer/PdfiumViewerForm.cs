using System;
using System.Drawing;
using System.Windows.Forms;
using PdfiumViewer;
using V6Init;

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
            try
            {
                var str = keyData.ToString();
                if (keyData == (Keys.Control | Keys.F))
                {
                    PdfMatches searchResult = null;
                    int searchCurrentIndex = -1;
                    FindForm ff = new FindForm();
                    ff.TopMost = true;
                    
                    ff.Find += delegate(string s, bool up, bool first)
                    {
                        if (first)
                        {
                            searchResult = pdfViewer1.Document.Search(s, false, false);
                            searchCurrentIndex = -1;
                        }

                        //int currentPage = pdfViewer1.Renderer.Page;
                        //var search = pdfViewer1.Document.Search(s, false, false);
                        if (searchResult == null || searchResult.Items.Count == 0)
                        {
                            ff.Text = V6Text.NotFound;
                            return;
                        }

                        if (up)
                        {
                            if (searchCurrentIndex == -1) searchCurrentIndex = searchResult.Items.Count;
                            if (searchCurrentIndex > 0)
                            {
                                GoToMatch(pdfViewer1, searchResult.Items[--searchCurrentIndex], ff);
                            }
                        }
                        else
                        {
                            if (searchCurrentIndex < searchResult.Items.Count - 1)
                            {
                                GoToMatch(pdfViewer1, searchResult.Items[++searchCurrentIndex], ff);
                            }
                        }

                    };
                    ff.Show(this);
                }
                else if (keyData == (Keys.Control | Keys.P))
                {
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.AllowSomePages = true;
                    printDialog.AllowSelection = true;
                    if (printDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        var pd = pdfViewer1.Document.CreatePrintDocument();
                        var ps = printDialog.PrinterSettings;
                        pd.PrinterSettings = ps;
                        pd.Print();
                    }
                }
                else if (keyData == Keys.OemMinus || (int) keyData == 109)
                {
                    pdfViewer1.Renderer.ZoomOut();
                }
                else if (keyData == Keys.Oemplus || (int) keyData == 107)
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
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "DoHotKey0", ex);
                return false;
            }
        }

        private void GoToMatch(PdfViewer pdfViewer, PdfMatch pdf_match, FindForm ff)
        {
            pdfViewer.Renderer.Page = pdf_match.Page;
            var pageSize = pdfViewer1.Document.PageSizes[pdf_match.Page];
            var pageLocation = new Point((int)pdf_match.Location.X, (int)(pageSize.Height - pdf_match.Location.Y));
            
            ff.Text = string.Format("page: {0} location: {1}% / {2}%", pdf_match.Page, (int)(pageLocation.X * 100 / pageSize.Width), (int)(pageLocation.Y * 100 / pageSize.Height));
        }
    }
}
