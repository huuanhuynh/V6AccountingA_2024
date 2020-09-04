using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using PdfiumViewer;
using V6Init;

namespace V6Controls.Forms.Viewer
{
    public partial class HtmlViewerForm : V6Form
    {
        public HtmlViewerForm()
        {
            InitializeComponent();
        }

        public HtmlViewerForm(string file, string title, bool autoPrint)
        {
            AutoPrint = autoPrint;
            InitializeComponent();
            Text = "Help " + title;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            webBrowser1.Navigate(file);
            
            //pdfViewer1.Document = OpenDocument(file);

        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Body.Style = "zoom:97%;margin: 5mm 5mm 5mm 5mm;";
            if (AutoPrint)
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Internet Explorer\PageSetup", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Print_Background", "yes", Microsoft.Win32.RegistryValueKind.String);
                        key.SetValue("footer", "");
                        key.SetValue("header", "");
                        key.SetValue("margin_left", 0.25);
                        key.SetValue("margin_top", 0.25);
                    }
                }

                //btnPrint.PerformClick();
                webBrowser1.Print();
                //Thread.Sleep(1000);
                //Close();
            }
        }

        private void HtmlViewerForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (AutoPrint)
                {
                    //while(webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    //{
                    //    //Application.DoEvents();
                    //    //Thread.Sleep(1000);
                    //}
                    //btnPrint.PerformClick();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public bool AutoPrint { get; set; }


        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                var str = keyData.ToString();
                if (keyData == (Keys.Control | Keys.P))
                {
                    btnPrint.PerformClick();
                }
                else if (keyData == Keys.OemMinus || (int) keyData == 109)
                {
                    //pdfViewer1.Renderer.ZoomOut();
                }
                else if (keyData == Keys.Oemplus || (int) keyData == 107)
                {
                    //pdfViewer1.Renderer.ZoomIn();
                }
                else if (keyData == Keys.NumPad0 || str == "D0")
                {
                    //pdfViewer1.ZoomMode = PdfViewerZoomMode.FitWidth;
                    //pdfViewer1.Renderer.Zoom = 1;
                }
                else if (keyData == Keys.PageUp)
                {
                    //pdfViewer1.Renderer.Page--;
                }
                else if (keyData == Keys.PageDown)
                {
                    //pdfViewer1.Renderer.Page++;
                }
                else if (keyData == Keys.Home)
                {
                    //pdfViewer1.Renderer.Page = 0;
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

        //private void GoToMatch(PdfViewer pdfViewer, PdfMatch pdf_match, FindForm ff)
        //{
        //    pdfViewer.Renderer.Page = pdf_match.Page;
        //    var pageSize = pdfViewer1.Document.PageSizes[pdf_match.Page];
        //    var pageLocation = new Point((int)pdf_match.Location.X, (int)(pageSize.Height - pdf_match.Location.Y));
            
        //    ff.Text = string.Format("page: {0} location: {1}% / {2}%", pdf_match.Page, (int)(pageLocation.X * 100 / pageSize.Width), (int)(pageLocation.Y * 100 / pageSize.Height));
        //}

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Internet Explorer\PageSetup", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Print_Background", "yes", Microsoft.Win32.RegistryValueKind.String);
                        key.SetValue("footer", "");
                        key.SetValue("header", "");
                        key.SetValue("margin_left", 0.25);
                        key.SetValue("margin_top", 0.25);
                    }
                }

                //string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
                //using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true)) {
                //    if (key != null) {
                //        //string old_footer = key.GetValue("footer");
                //        //string old_header = key.GetValue("header");
                //        key.SetValue("footer", "");
                //        key.SetValue("header", "");
                //        //Print();
                //        //key.SetValue("footer", old_footer);
                //        //key.SetValue("header", old_header);
                //    }
                //}
                webBrowser1.ShowPrintDialog();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + "btnPrint_Click", ex);
            }
        }

        
    }
}
