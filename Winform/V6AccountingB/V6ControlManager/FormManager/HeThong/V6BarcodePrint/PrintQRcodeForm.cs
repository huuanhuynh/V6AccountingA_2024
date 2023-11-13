using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using H;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;
using QRCoder;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;
using V6AccountingBusiness;

namespace V6ControlManager.FormManager.HeThong.V6BarcodePrint
{
	/// <summary>
	/// Summary description for Form.
	/// </summary>
	partial class PrintQRcodeForm : V6Form
	{
        QRcodePrintSetting _qrSetting;
		public DataTable Data { get; set; }
        public AlbcConfig _albcConfig;
        public string DBFpath = "";
        //public DataTable data;
        //public Setting setting;

        public PrintQRcodeForm( )
		{
			InitializeComponent( );
            MyInit();
		}

        public PrintQRcodeForm(DataTable data)
        {
            InitializeComponent();
            Data = data;
            MyInit();
        }

        public PrintQRcodeForm(DataTable data, AlbcConfig _albcConfig)
        {
            InitializeComponent();
            Data = data;
            this._albcConfig = _albcConfig;
            MyInit();
        }

        public void MyInit()
        {
            try
            {
                _qrSetting = new QRcodePrintSetting();
                if (_albcConfig.EXTRA_INFOR == null || _albcConfig.EXTRA_INFOR.Count == 0)
                {
                    GenDefaultSetting();
                }
                else
                {
                    LoadSetting();
                }
                

                cboECC.SelectedIndex = 0;
                propertyGrid1.SelectedObject = _qrSetting;
                NumberFormatInfo DecimalSeparatorFormat = new NumberFormatInfo { NumberDecimalSeparator = "." };

                if (_qrSetting.ForcePrint)
                {
                    Print();
                }
                else
                {
                    //Application.EnableVisualStyles();
                    //Application.EnableRTLMirroring( );
                    //Application.Run(new PrintQRcodeForm());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSetting()
        {
            try
            {
                _qrSetting.PageWidth = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("PageWidth"));
                _qrSetting.PageHeight = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("PageHeight"));
                _qrSetting.MarginLeft = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("MarginLeft"));
                _qrSetting.MarginRight = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("MarginRight"));
                _qrSetting.MarginTop = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("MarginTop"));
                _qrSetting.MarginBottom = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("MarginBottom"));

                _qrSetting.DistanceH = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("DistanceH"));
                _qrSetting.DistanceV = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("DistanceV"));



                _qrSetting.StampWidth = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("StampWidth"));
                _qrSetting.StampHeight = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("StampHeight"));
                _qrSetting.Scale = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("Scale"));

                _qrSetting.CodeTextFontSize = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("CodeTextFontSize"));
                _qrSetting.NameTextFontSize = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("NameTextFontSize"));
                _qrSetting.PriceTextFontSize = ObjectAndString.ObjectToFloat(_albcConfig.GET_EXTRA_INFOR("PriceTextFontSize"));
                _qrSetting.NameTextFontBold = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("NameTextFontBold"));
                _qrSetting.CodeTextFontBold = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("CodeTextFontBold"));
                _qrSetting.PriceTextFontBold = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("PriceTextFontBold"));
                _qrSetting.NameTextCanDrop = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("NameTextCanDrop"));
                _qrSetting.ShowCode = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("ShowName"));
                _qrSetting.ShowName = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("ShowName"));
                _qrSetting.ShowPrice = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("ShowPrice"));

                _qrSetting.ForcePrint = ObjectAndString.ObjectToBool(_albcConfig.GET_EXTRA_INFOR("ForcePrint"));

                _qrSetting.ThousandGroup = _albcConfig.GET_EXTRA_INFOR("ThousandGroup");
                _qrSetting.DecimalGroup = _albcConfig.GET_EXTRA_INFOR("DecimalGroup");
                _qrSetting.PriceDecimals = ObjectAndString.ObjectToInt(_albcConfig.GET_EXTRA_INFOR("PriceDecimals"));

                _qrSetting.CurrencyType = _albcConfig.GET_EXTRA_INFOR("CurrencyType");
                _qrSetting.AnotherCurrencySymbol = _albcConfig.GET_EXTRA_INFOR("AnotherCurrencySymbol");
                _qrSetting.LAN = _albcConfig.GET_EXTRA_INFOR("LAN");
                _qrSetting.UnitText = _albcConfig.GET_EXTRA_INFOR("UnitText");
            }
            catch (Exception ex)
            {
                V6Message.ShowErrorMessage("LoadSetting " + ex.Message, this);
            }
        }

        private void GenDefaultSetting()
        {
            try
            {
                _qrSetting.PageWidth = 120;
                _qrSetting.PageHeight = 80;
                _qrSetting.MarginLeft = 5;
                _qrSetting.MarginRight = 5;
                _qrSetting.MarginTop = 5;
                _qrSetting.MarginBottom = 5;

                _qrSetting.DistanceH = 5;
                _qrSetting.DistanceV = 5;

                _qrSetting.StampWidth = 50;
                _qrSetting.StampHeight = 50;
                _qrSetting.Scale = 1;

                _qrSetting.CodeTextFontSize = 8;
                _qrSetting.NameTextFontSize = 8;
                _qrSetting.PriceTextFontSize = 8;
                _qrSetting.NameTextFontBold = true;
                _qrSetting.CodeTextFontBold = true;
                _qrSetting.PriceTextFontBold = true;
                _qrSetting.NameTextCanDrop = true;
                _qrSetting.ShowCode = false;

                _qrSetting.ForcePrint = false;

                _qrSetting.ThousandGroup = " ";
                _qrSetting.DecimalGroup = ",";
                _qrSetting.PriceDecimals = 2;

                _qrSetting.CurrencyType = "1";
                _qrSetting.AnotherCurrencySymbol = "$";
                _qrSetting.LAN = "V";
                _qrSetting.UnitText = "VND";

                string EXTRA_INFOR = _qrSetting.ToStringDictionary();
                _albcConfig.DATA["EXTRA_INFOR"] = EXTRA_INFOR;
                _albcConfig.LoadExtraInfor();

                //_albcConfig.UpdateSimple("EXTRA_INFOR", EXTRA_INFOR);
                SaveSetting();
                
            }
            catch (Exception ex)
            {
                V6Message.ShowErrorMessage("GenDefaultSetting " + ex.Message, this);
            }
        }

        private void SaveSetting()
        {
            string EXTRA_INFOR = _qrSetting.ToStringDictionary();
            _albcConfig.DATA["EXTRA_INFOR"] = EXTRA_INFOR;
            _albcConfig.LoadExtraInfor();
            var data = new Dictionary<string, object>();
            data["EXTRA_INFOR"] = EXTRA_INFOR;
            var key = new Dictionary<string, object>();
            key["MA_FILE"] = _albcConfig.MA_FILE;
            V6BusinessHelper.UpdateSimple("ALBC", data, key);
        }

        private bool CheckData()
        {
            string errors = "";
            Dictionary<string, string> qrList = new Dictionary<string, string>();
	        foreach (DataRow row in Data.Rows)
	        {
	            var qr = row[0].ToString().Trim();
	            var code = row[1].ToString().Trim();
                if(qrList.ContainsKey(qr)) continue;
                qrList.Add(qr, code);
               
	        }
            if (errors.Length > 0)
            {
                this.ShowErrorMessage(errors);
                return false;
            }
	        return true;
	    }


        int barcodecount = 0;
        public void Print()
        {
            //SetBarcodeGeneralValues(_barcodeLib);
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            document.DocumentName = "V6QRcode";
            PaperSize psize = new PaperSize("V6",
                    (int)(_qrSetting.PageWidth / 25.4 * 100),
                    (int)(_qrSetting.PageHeight / 25.4 * 100));

            document.DefaultPageSettings.PaperSize = psize;
            
            //printview = new PrintPreviewDialog();
            //printview.Document = document;
            //printview.ShowDialog(this);

            PrintDialog printdialog1 = new PrintDialog( );
            printdialog1.AllowPrintToFile = true;
            printdialog1.PrintToFile = false;
            printdialog1.AllowSelection = false;
            printdialog1.AllowSomePages = true;

            printdialog1.UseEXDialog = true;
            printdialog1.PrinterSettings.DefaultPageSettings.PaperSize = psize;
            var tongSoTrang = TongSoTrang(psize);
            printdialog1.PrinterSettings.MaximumPage = tongSoTrang;
            printdialog1.PrinterSettings.FromPage = 1;
            printdialog1.PrinterSettings.ToPage = tongSoTrang;
            

            //printdialog1.AllowSelection = true;
            //printdialog1.AllowSomePages = true;
            //printdialog1.
            document.PrintPage += document_PrintPage;

            if (printdialog1.ShowDialog(this) == DialogResult.OK)
            {
                //printdialog1.Document = document;
                document.PrinterSettings = printdialog1.PrinterSettings;
                document.DefaultPageSettings.PaperSize = psize;
                from_page = printdialog1.PrinterSettings.FromPage;
                to_page = printdialog1.PrinterSettings.ToPage;

                //MessageBox.Show("Number of copies: " + document.PrinterSettings.Copies.ToString());
                
                document.Print();
            }
        }

        public void PrintView()
        {
            //SetBarcodeGeneralValues(_barcodeLib);
            barcodecount = 0;
            PrintDocument document = new PrintDocument();
            document.DocumentName = "V6Barcode";
            document.DefaultPageSettings.PaperSize
                = new PaperSize("V6",
                    (int)(_qrSetting.PageWidth / 25.4 * 100),
                    (int)(_qrSetting.PageHeight / 25.4 * 100));

            PrintPreviewDialog printview = new PrintPreviewDialog();
            printview.Width = 800;
            printview.Height = 600;
            
            printview.Document = document;
            document.PrintPage += document_PrintPage;
            printview.ShowIcon = false;
            //Tắt nút in.
            ((ToolStripButton)((ToolStrip)printview.Controls[1]).Items[0]).Enabled = false;
            printview.ShowDialog(this);
        }

        

        private int TongSoTrang(PaperSize psize)
        {
            int pagecount = 0;
            float yPos = 0f; yPos = _qrSetting.MarginTop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = _qrSetting.PageWidth;// e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = _qrSetting.PageHeight;// e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - _qrSetting.MarginLeft - _qrSetting.MarginRight + _qrSetting.DistanceV)
                / ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV));
            if (columnsPerPage == 0)
            {
                columnsPerPage = 1;
            }
            int barcodecount0 = 0;
            int lineInPageCount = 0;
            
            //Khi chưa in hết trang
            while (barcodecount0 < Data.Rows.Count)
            {
                //barcodecount++;
                pagecount++;
                lineInPageCount = 0;
                yPos = _qrSetting.MarginTop;
                do
                {
                    for (int i = 0; i < columnsPerPage; i++)
                    {
                        if (barcodecount0 < Data.Rows.Count)
                        {
                            barcodecount0++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    lineInPageCount++;
                    yPos += (float)_qrSetting.StampHeight * _qrSetting.Scale + _qrSetting.DistanceH;
                } while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)_qrSetting.StampHeight * _qrSetting.Scale);

            }

            return pagecount;
        }

	    private int from_page = 1;
	    private int to_page = 1;

        public void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Code in trong 1 trang
            
            float yPos = 0f; yPos = _qrSetting.MarginTop;
            float xPos = 0f;// xPos = e.PageSettings.Margins.Left;
            float pageWidthmm = e.PageBounds.Width * 25.4f / 100;
            float pageHeightmm = e.PageBounds.Height * 25.4f / 100;

            int columnsPerPage = (int)((pageWidthmm - _qrSetting.MarginLeft - _qrSetting.MarginRight + _qrSetting.DistanceV)
                / ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV));
            if (columnsPerPage == 0)
            {
                columnsPerPage = 1;
            }
            int lineInPageCount = 0;

            //Khi chưa in hết trang
            do
            {
                for (int i = 0; i < columnsPerPage; i++)
                {
                    if (barcodecount < Data.Rows.Count)
                    {
                        DataRow currentdata = Data.Rows[barcodecount];//Lấy thông tin tại barcodecount xong mới ++
                        barcodecount++;

                        var qr = currentdata[0].ToString().Trim();//QR value
                        var barcodeProductCode = currentdata[1].ToString().Trim();
                        var barcodeProductName = currentdata[2].ToString().Trim();
                        var barcodeProductPrice = Convert.ToDecimal(currentdata[3]);

                        //Tính vị trí x
                        xPos = _qrSetting.MarginLeft +
                            i * ((float)_qrSetting.StampWidth * _qrSetting.Scale + _qrSetting.DistanceV);

                        DrawBarcode(e.Graphics, new Point((int)xPos, (int)yPos),
                            qr, barcodeProductCode, barcodeProductName, barcodeProductPrice, _qrSetting.ECCLever);
                    }
                    else
                    {
                        break;
                    }
                }
                lineInPageCount++;
                yPos += (float)_qrSetting.StampHeight * _qrSetting.Scale + _qrSetting.DistanceH;
            }
            while (Math.Abs(yPos) < 0.001f || yPos < pageHeightmm - (float)_qrSetting.StampHeight * _qrSetting.Scale);

            if (barcodecount < Data.Rows.Count)
            {
                //Còn trang tiếp theo. Hàm này sẽ lặp lại.
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                //e.Cancel = true;
            }
        }
        
        private void butDraw_Click(object sender, EventArgs e)
		{
            UpdateValues();
            DrawSampleQR();
		}

        private void DrawSampleQR()
        {
            try
            {
                Graphics g = picBarcode.CreateGraphics();

                g.FillRectangle(new SolidBrush(SystemColors.Control), new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));

                DrawBarcode(g, new Point(0, 0), txtQRcode.Text, txtProductCode.Text, txtProductName.Text,
                    ObjectAndString.ObjectToDecimal(txtPrice.Text), _qrSetting.ECCLever);
                //g.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DrawSample " + ex.Message);
            }
        }
        

        public Bitmap RenderQrCode(string qr_code, Bitmap icon)
        {
            //Graphics g = picBarcode.CreateGraphics();

            //g.FillRectangle(new SolidBrush(SystemColors.Control),
            //    new Rectangle(0, 0, picBarcode.Width, picBarcode.Height));
            Bitmap result = null;
            string level = cboECC.SelectedItem.ToString(); // 0123 LMQH
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr_code, eccLevel))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                result = qrCode.GetGraphic(20, Color.Black, Color.White, icon, 15);
                //g.DrawImage(qr_bitmap, 0, 0, picBarcode.Width, picBarcode.Height);

                //this.pictureBoxQRCode.Size = new System.Drawing.Size(pictureBoxQRCode.Width, pictureBoxQRCode.Height);
                //Set the SizeMode to center the image.
                //this.pictureBoxQRCode.SizeMode = PictureBoxSizeMode.CenterImage;

                //pictureBoxQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            return result;
        }

        /// <summary>
        /// Vẽ QR lên graphics cùng các thông tin khác.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pt"></param>
        /// <param name="value">Giá trị QR</param>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="eccLevel"></param>
        public void DrawBarcode(Graphics g, Point pt, string value,
            string productCode, string productName, decimal productPrice,
            int eccLevel)
        {
            float width = (float)_qrSetting.StampWidth * (float)_qrSetting.Scale;
            float height = (float)_qrSetting.StampHeight * (float)_qrSetting.Scale;

            // Save the GraphicsState.
            GraphicsState gs = g.Save();

            // Set the PageUnit to Inch because all of our measurements are in inches.
            g.PageUnit = GraphicsUnit.Millimeter;

            // Set the PageScale to 1, so a millimeter will represent a true millimeter.
            g.PageScale = 1;

            SolidBrush brush = new SolidBrush(Color.Black);
            
            float xStart = pt.X;
            float yStart = pt.Y;
            float xEnd = xStart + width;
            float yEnd = yStart + height;

            Font name_font = new Font("Arial", _qrSetting.NameTextFontSize * _qrSetting.Scale,
                _qrSetting.NameTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font code_font = new Font("Arial", _qrSetting.CodeTextFontSize * _qrSetting.Scale,
                _qrSetting.CodeTextFontBold ? FontStyle.Bold : FontStyle.Regular);
            Font price_font = new Font("Arial", _qrSetting.PriceTextFontSize * _qrSetting.Scale,
                _qrSetting.PriceTextFontBold ? FontStyle.Bold : FontStyle.Regular);

            

            SizeF textSize1 = g.MeasureString(productCode, code_font);
            float code_Height = textSize1.Height;
            if (_qrSetting.CodeTextCanDrop && textSize1.Width > width)
            {
                code_Height *= 2;
            }
            if (!_qrSetting.ShowCode)
            {
                code_Height = 0;
            }
            SizeF textSize_name = g.MeasureString(productName, name_font);
            float name_Height = textSize_name.Height;
            if (_qrSetting.NameTextCanDrop && textSize_name.Width > width)
            {
                name_Height *= 2;
            }
            if (!_qrSetting.ShowName)
            {
                name_Height = 0;
            }
            float price_Height = g.MeasureString(productCode, price_font).Height;
            if (!_qrSetting.ShowPrice)
            {
                price_Height = 0;
            }

            float aWidth = g.MeasureString("A", code_font).Width;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            yStart += code_Height;
            // Vẽ QR vào vùng vẽ.
            Bitmap QR = RenderQrCode(value, null);
            // Tính toán kích thước QR
            float QR_width = width;
            float QR_height = height - code_Height - name_Height - price_Height;
            g.DrawImage(QR, xStart, yStart, QR_width, QR_height);
            // Vẽ mã sản phẩm trên cùng
            yStart -= code_Height;
            if (code_Height > 0) g.DrawString(productCode, code_font, brush, new RectangleF(xStart, yStart, width, code_Height), stringFormat);
            // Vẽ tên sản phẩm bên dưới.
            yStart += code_Height + QR_height;
            if (name_Height > 0) g.DrawString(productName, name_font, brush, new RectangleF(xStart, yStart, width, name_Height), stringFormat);
            //Vẽ phần giá bên dưới
            if (price_Height > 0) g.DrawString(_qrSetting.PriceText + " " +
                ObjectAndString.NumberToString(productPrice, _qrSetting.PriceDecimals, _qrSetting.DecimalGroup, _qrSetting.ThousandGroup, true)
                + " " + _qrSetting.UnitText,
                price_font, brush,
                new RectangleF(xStart + aWidth, pt.Y + height - price_Height, width, price_Height), stringFormat);
            
            // Restore the GraphicsState.
            g.Restore(gs);
        }

        public NumberFormatInfo numberformatinfo
            = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        

        private void frmEan13_Load(object sender, EventArgs e)
        {   
            LoadLanguage();
            btnDrawSample.PerformClick();
        }
        

        void UpdateValues()
        {
            //setting.SaveSetting();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            UpdateValues();
            if (CheckData())
            {
                Print();
            }
        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            UpdateValues();
            if (CheckData())
            {
                PrintView();
            }
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }

	}

    class QRcodePrintSetting
    {
        /// <summary>
        /// $
        /// </summary>
        [Category("TEXT")]
        public string AnotherCurrencySymbol { get; set; }
        [Category("TEXT")]
        public bool CodeTextFontBold { get; set; }
        [Category("TEXT")]
        public float CodeTextFontSize { get; set; }
        /// <summary>
        /// 1 VND 2 USD
        /// </summary>
        public string CurrencyType { get; set; }
        [Category("PAGE")]
        public float DistanceH { get; set; }
        [Category("PAGE")]
        public float DistanceV { get; set; }
        public bool ForcePrint { get; set; }
        public string LAN { get; set; }
        [Category("PAGE")]
        public float MarginBottom { get; set; }
        [Category("PAGE")]
        public float MarginLeft { get; set; }
        [Category("PAGE")]
        public float MarginRight { get; set; }
        [Category("PAGE")]
        public float MarginTop { get; set; }
        [Category("TEXT")]
        public bool CodeTextCanDrop { get; set; }
        [Category("TEXT")]
        public bool NameTextCanDrop { get; set; }
        [Category("TEXT")]
        public bool NameTextFontBold { get; set; }
        [Category("TEXT")]
        public float NameTextFontSize { get; set; }
        [Category("PAGE")]
        public float PageHeight { get; set; }
        [Category("PAGE")]
        public float PageWidth { get; set; }
        [Category("TEXT")]
        public bool PriceTextFontBold { get; set; }
        [Category("TEXT")]
        public float PriceTextFontSize { get; set; }
        public float Scale { get; set; }
        public float StampHeight { get; set; }
        public float StampWidth { get; set; }
        public string ThousandGroup { get; set; }
        [Category("TEXT")]
        public string DecimalGroup { get; set; }
        public int ECCLever { get; set; }
        /// <summary>
        /// Giá: Price:
        /// </summary>
        [Category("TEXT")]
        public string PriceText { get; set; }
        
        [Category("TEXT")]
        public int PriceDecimals { get; set; }
        /// <summary>
        /// VND
        /// </summary>
        [Category("TEXT")]
        public string UnitText { get; set; }
        [Category("TEXT")]
        public bool ShowCode { get; set; }
        [Category("TEXT")]
        public bool ShowName { get; set; }
        [Category("TEXT")]
        public bool ShowPrice { get; set; }

        public QRcodePrintSetting()
        {

        }
        public QRcodePrintSetting(IDictionary<string, string> dic)
        {

        }

        public string ToStringDictionary()
        {
            string result = "";
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(this, null);
                    if (value is bool)
                    {
                        value = (bool)value ? "1" : "0";
                    }
                    result += ";" + property.Name +  ":" + value;
                }
            }
            if (result.Length > 1) result = result.Substring(1);
            return result;
        }
    }
}

