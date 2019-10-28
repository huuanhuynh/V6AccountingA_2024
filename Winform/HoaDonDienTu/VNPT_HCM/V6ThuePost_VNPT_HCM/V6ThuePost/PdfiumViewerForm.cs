using System;
using System.Drawing;
using System.Windows.Forms;
using PdfiumViewer;
using V6Controls.Forms;

namespace V6ThuePost
{
    public partial class PdfiumViewerForm : Form
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

        private void PdfiumViewerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += timer_Tick;
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private int time_count = 0;
        void timer_Tick(object sender, EventArgs e)
        {
            if (time_count == 2)
            {
                ((Timer)sender).Stop();
                if (AutoClickPrint)
                {
                    AutoClickPrint = false;
                    PrintDocument();
                }
            }
            time_count++;
        }

        void pdfViewer1_Validated(object sender, EventArgs e)
        {
            if (AutoClickPrint)
            {
                AutoClickPrint = false;
                PrintDocument();
            }
        }
        
        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public bool AutoClickPrint { get; set; }

        private PdfDocument OpenDocument(string fileName)
        {
            try
            {
                return PdfDocument.Load(this, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                //Nếu đã thực hiện lệnh bên trên gửi xuống thì không chạy DoHotKey0
                //if (do_hot_key)
                //{
                //    do_hot_key = false;
                //    return base.ProcessCmdKey(ref msg, keyData);
                //}
                if (DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public virtual bool DoHotKey0(Keys keyData)
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
                            ff.Text = "NotFound";
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
                    PrintDocument();
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
                    //return base.DoHotKey0(keyData);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        /// <summary>
        /// Chọn máy in và một số tùy chọn rồi in.
        /// </summary>
        public void PrintDocument()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowPrintToFile = false;
            printDialog.AllowSomePages = true;
            
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                Program.WriteOldSelectPrinter(printDialog.PrinterSettings.PrinterName);
                var pd = pdfViewer1.Document.CreatePrintDocument();
                var ps = printDialog.PrinterSettings;
                pd.PrinterSettings = ps;
                pd.Print();
            }
        }

        /// <summary>
        /// In luôn ra máy in mặc định.
        /// </summary>
        public void PrintToDefaultPrinter()
        {
            pdfViewer1.Document.CreatePrintDocument().Print();
        }
        
        /// <summary>
        /// In luôn ra máy in mặc định.
        /// </summary>
        public void PrintToPrinter(string printerName)
        {
            var pd = pdfViewer1.Document.CreatePrintDocument();
            pd.PrinterSettings.PrinterName = printerName;
            pd.Print();
        }

        


    }
}
