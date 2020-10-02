using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Ionic.Zip;

namespace V6Controls.Forms.Viewer
{
    public partial class XmlXslZipViewerForm : V6Form
    {
        public XmlXslZipViewerForm()
        {
            InitializeComponent();
        }

        private string _zipFile = null;
        public XmlXslZipViewerForm(string file, string title, bool autoPrint)
        {
            _zipFile = file;
            AutoPrint = autoPrint;
            InitializeComponent();
            Text = "Help " + title;
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                // Open zip
                string xmlFile = null, xslFile = null;
                string dir = _zipFile.Substring(0, _zipFile.Length - 4);
                using (ZipFile zip = ZipFile.Read(_zipFile))
                {
                    foreach (ZipEntry entry in zip)
                    {
                        if (entry.FileName.EndsWith(".xml")) xmlFile = Path.Combine(dir, entry.FileName);
                        if (entry.FileName.EndsWith(".xsl")) xslFile = Path.Combine(dir, entry.FileName);
                    }
                    
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    zip.ExtractAll(dir, ExtractExistingFileAction.OverwriteSilently);
                }
                // create html
                //Create a new XslTransform object.
                XslTransform xslt = new XslTransform();

//Load the stylesheet.
                xslt.Load(xslFile);

//Create a new XPathDocument and load the XML data to be transformed.
                XPathDocument mydata = new XPathDocument(xmlFile);

//Create an XmlTextWriter which outputs to the console.
                XmlWriter writer = new XmlTextWriter(dir + "\\htmlview.html", Encoding.UTF8);

//Transform the data and send the output to the console.
                xslt.Transform(mydata,null,writer, null);

                // Load html
                webBrowser1.Navigate(dir + "\\htmlview.html");
            }
            catch (Exception ex)
            {
                this.ShowErrorException("XmlXslZipViewerForm.MyInit", ex);
            }
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

        private void XmlXslZipViewerForm_Load(object sender, EventArgs e)
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
